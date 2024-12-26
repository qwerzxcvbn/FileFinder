using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileFinder {
    public partial class FrmFileFinderMain : Form {
        class FileSearchInfo {
            public long FilesTotalCount { get; set; } = 0;
            public long FilesProcessedCount { get; set; } = 0;
            public string SearchDirectory { get; set; }
            public long FilesFound { get; set; } = 0;
            public string FileNameMask { get; set; } = "";
            public List<string> FoundFiles = new List<string>();
        }

        enum BackgroundWorkerMode {
            Estimate,
            Search
        }
        public bool IsSearchRunning { get; set; } = false;
        private FileSearchInfo FileSearchInfoHolder { get; set; } = new FileSearchInfo();
        public FrmFileFinderMain() 
        {
            InitializeComponent();
        }


        // Обработка нажатия на кнопку "Начать поиск" / "Прервать"
        private void ButtonStartSearch_Click(object sender, EventArgs e) {
            if (IsSearchRunning) {
          
                // Запретить повторные нажатия на кнопку "Прервать"
                ButtonStartSearch.Enabled = false;

                if (BackgroundWorkerEstimateSearchTime.IsBusy) {
                    BackgroundWorkerEstimateSearchTime.CancelAsync();
                }
                if (BackgroundWorkerSearchFiles.IsBusy) {
                    BackgroundWorkerSearchFiles.CancelAsync();
                }
            } else {
                FileSearchInfoHolder.FileNameMask = TextBoxFileName.Text;
                ProgressBarMain.Value = 0;
                ListViewFoundFiles.Groups.Clear();
                ListViewFoundFiles.Groups.Add(new ListViewGroup("listViewGroupFiles", "Найденные файлы"));

                FileSearchInfoHolder.FoundFiles.Clear();

                ProgressBarMain.Visible = true;

                if (FileSearchInfoHolder.FilesTotalCount == 0) {
                    ProgressBarMain.Style = ProgressBarStyle.Marquee;
                    LabelProgress.Text = "Подсчёт количества файлов в системе и оценка примерного времени... Найдено файлов:";
                    LabelProgress.Visible = true;
                    LabelFilesCount.Visible = true;
                    LabelFilesCount.Left = LabelProgress.Right + 10;                    
                    SetIsSearchRunningAndUpdateButtonStartState(true);
                    BackgroundWorkerEstimateSearchTime.RunWorkerAsync(FileSearchInfoHolder);
                } else {                    
                    StartSearchFilesByFileName();
                }
            }
        }


        // Метод запускает поиск файлов 
        private void StartSearchFilesByFileName() {
            LabelProgress.Text = "Поиск файла по маске *" + FileSearchInfoHolder.FileNameMask + "* в каталоге '" + FileSearchInfoHolder.SearchDirectory + "'...";
            LabelFilesCount.Visible = false;            
            ProgressBarMain.Style = ProgressBarStyle.Continuous;
            FileSearchInfoHolder.FilesFound = 0;
            FileSearchInfoHolder.FilesProcessedCount = 0;
            SetIsSearchRunningAndUpdateButtonStartState(true);
            BackgroundWorkerSearchFiles.RunWorkerAsync(FileSearchInfoHolder);
        }


        // Метод для выполнения основной работы для элемента BackgroundWorker
        private void BackgroundWorkerEstimateSearchTime_DoWork(object sender, DoWorkEventArgs e) {
            if (e.Argument is FileSearchInfo fileInfo) {
                if (BackgroundWorkerEstimateSearchTime.CancellationPending) {
                    e.Cancel = true;                    
                } else {
                    CalculateFilesCountRecursively(FileSearchInfoHolder.SearchDirectory, BackgroundWorkerMode.Estimate, fileInfo);
                    if (BackgroundWorkerEstimateSearchTime.CancellationPending) {
                        e.Cancel = true;
                    }
                }                
            }            
        }

        private void CalculateFilesCountRecursively(string parentDirectory, BackgroundWorkerMode workerMode, FileSearchInfo fileInfoHolder) {
            try {
                IEnumerable<string> subdirectories = Directory.EnumerateDirectories(parentDirectory, "*", SearchOption.TopDirectoryOnly);
                IEnumerable<string> files = Directory.EnumerateFiles(parentDirectory);

                if (workerMode == BackgroundWorkerMode.Estimate) {                 
                    if (BackgroundWorkerEstimateSearchTime.CancellationPending) {
                        return;
                    }

                    fileInfoHolder.FilesTotalCount += files.LongCount();
                    BackgroundWorkerEstimateSearchTime.ReportProgress(10);                                        
                } else if (workerMode == BackgroundWorkerMode.Search) {
                    
                    if (BackgroundWorkerSearchFiles.CancellationPending) {
                        return;
                    }

                    foreach (string file in files) {
                        if (file.Contains(fileInfoHolder.FileNameMask)) {
                            fileInfoHolder.FoundFiles.Add(file);
                            FileSearchInfoHolder.FilesFound++;
                        }
                    }

                    List<string> foundFiles = new List<string>(fileInfoHolder.FoundFiles);

                    fileInfoHolder.FilesProcessedCount += files.LongCount();
                    int progress = (int)(fileInfoHolder.FilesProcessedCount * 100 / fileInfoHolder.FilesTotalCount);                    
                    BackgroundWorkerSearchFiles.ReportProgress(progress, foundFiles);
                    fileInfoHolder.FoundFiles.Clear();
                }

                if (subdirectories.LongCount() > 0) {
                    foreach (string subdirectory in subdirectories) {
                        CalculateFilesCountRecursively(subdirectory, workerMode, fileInfoHolder);
                    }
                }
            } catch (UnauthorizedAccessException unauthorizedAccessException) {

            } catch (DirectoryNotFoundException directoryNotFoundException) {

            } catch (Exception otherException) {

            }
        }

        private void BackgroundWorkerEstimateSearchTime_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                SetIsSearchRunningAndUpdateButtonStartState(false);

                LabelProgress.Text = "Оценка времени поиска была прервана.";
                FileSearchInfoHolder.FilesTotalCount = 0;
                FileSearchInfoHolder.FilesFound = 0;
                ProgressBarMain.Visible = false;
                LabelFilesCount.Text = "0";
                LabelFilesCount.Visible = false;
            } else {
                StartSearchFilesByFileName();
            }            
        }

        private void BackgroundWorkerEstimateSearchTime_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            LabelFilesCount.Text = FileSearchInfoHolder.FilesTotalCount.ToString();
        }

        private void UpdateSearchDirectoryFromSelectedDrive() {
            FileSearchInfoHolder.SearchDirectory = (ComboBoxDrives.SelectedItem as DriveInfoItem).DriveName;
            UpdateSearchPathReadonlyTextBox(FileSearchInfoHolder.SearchDirectory);
        }

        // Загружает в выпадающий список все доступные в системе диски 
        private void LoadAvailableDrivesInfo() {
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            foreach (var driveInfo in driveInfos) {
                ComboBoxDrives.Items.Add(new DriveInfoItem(driveInfo));                
            }
            ComboBoxDrives.SelectedIndex = 0;
        }

        private void FrmFileFinderMain_Load(object sender, EventArgs e) {
            LabelProgress.Visible = false;
            LabelFilesCount.Visible = false;
            ProgressBarMain.Visible = false;
            this.DoubleBuffered = true;

            LoadAvailableDrivesInfo();
            UpdateSearchDirectoryFromSelectedDrive();
        }

        private void BackgroundWorkerSearchFiles_DoWork(object sender, DoWorkEventArgs e) {
            if (e.Argument is FileSearchInfo fileInfo) {
                if (BackgroundWorkerSearchFiles.CancellationPending) {
                    e.Cancel = true;
                } else {
                    CalculateFilesCountRecursively(FileSearchInfoHolder.SearchDirectory, BackgroundWorkerMode.Search, fileInfo);
                    if (BackgroundWorkerSearchFiles.CancellationPending) {
                        e.Cancel = true;
                    }
                }                
            }
        }

        // Событие обработки изменения прогресса для элемента BackgroundWorker
        private void BackgroundWorkerSearchFiles_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            ProgressBarMain.Value = e.ProgressPercentage;
            List<string> foundFiles = (List<string>)e.UserState;

            ListViewGroup group = ListViewFoundFiles.Groups["listViewGroupFiles"];

            foreach (string fileName in foundFiles) {
                long fileSizeInBytes = -1;
                try {
                    FileInfo fileInfo = new FileInfo(fileName);
                    fileSizeInBytes = fileInfo.Length;
                } catch (FileNotFoundException fileNotFoundException) {

                }
                
                ListViewItem value = new ListViewItem(new string[] { fileName, fileSizeInBytes.ToString() }, 0, group);
                ListViewFoundFiles.Items.Add(value);
            }

            FileSearchInfoHolder.FoundFiles.Clear();            
        }

        // Обработка изменения текста в текстовом поле TextBoxFileName.    
        private void TextBoxFileName_TextChanged(object sender, EventArgs e) {
            ButtonStartSearch.Enabled = !"".Equals(TextBoxFileName.Text.Trim());
        }

        // Метод выбирает один из доступных в системе дисков 
        private void SelectDriveBySearchPath(string searchPath) {
            int commaSlashPosition = searchPath.IndexOf(":\\");
            if (commaSlashPosition >= 0) {
                string driveLetterFromPath = searchPath.Substring(0, commaSlashPosition + 2);
                
                foreach (var item in ComboBoxDrives.Items) {
                    if (item is DriveInfoItem driveInfoItem) {
                        if (driveInfoItem.DriveName.Equals(driveLetterFromPath)) {
                            ComboBoxDrives.SelectedItem = item;
                            break;
                        }
                    }
                }
            } else {
                MessageBox.Show("Ошибка: невозможно найти диск, соответствующий выбранному пути", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSearchPathReadonlyTextBox(string searchPath) {
            if (!searchPath.Equals(TextBoxSearchPath.Text) && FileSearchInfoHolder.FilesTotalCount > 0) {
                FileSearchInfoHolder.FilesTotalCount = 0;
            }
            TextBoxSearchPath.Text = searchPath;
        }


        // Обработка нажатия на кнопку "Обзор" 
        private void ButtonSelectSearchDirectory_Click(object sender, EventArgs e) {
            DialogResult dialogResult = FolderBrowserDialogSelectSearchDirectory.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                string selectedPath = FolderBrowserDialogSelectSearchDirectory.SelectedPath;
                if (!selectedPath.EndsWith("\\")) {
                    selectedPath += "\\";
                }                
                FileSearchInfoHolder.SearchDirectory = selectedPath;
                UpdateSearchPathReadonlyTextBox(selectedPath);
                SelectDriveBySearchPath(selectedPath);                               
            }
        }


        // Переключает свойство IsSearchRunning
        // обновляет текст кнопки ButtonStart и её состояние   
        private void SetIsSearchRunningAndUpdateButtonStartState(bool isRunning) {
            if (isRunning) {
                ButtonStartSearch.Text = "&Прервать";
            } else {
                ButtonStartSearch.Text = "&Начать поиск";
                ButtonStartSearch.Enabled = true;
            }
            IsSearchRunning = isRunning;
        }

        private void BackgroundWorkerSearchFiles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            SetIsSearchRunningAndUpdateButtonStartState(false);

            if (e.Cancelled) {
                LabelProgress.Text = "Операция поиска прервана.";
                ProgressBarMain.Visible = false;
                LabelFilesCount.Text = "0";
                LabelFilesCount.Visible = false;
            } else {
                LabelProgress.Text = "Поиск по маске *" + FileSearchInfoHolder.FileNameMask + "* в каталоге '" + FileSearchInfoHolder.SearchDirectory + "' завершён. Найдено файлов: ";
                LabelFilesCount.Left = LabelProgress.Right + 10;
                LabelFilesCount.Text = FileSearchInfoHolder.FilesFound.ToString();
                LabelFilesCount.Visible = true;
            }            
        }

       
        // Запускает выбранный файл
        private void StartSelectedFileUsingShellExecute(string pathToFile) {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = pathToFile;
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);
        }

        // Обработка двойного клика по одному из найденных файлов
        private void ListViewFoundFiles_DoubleClick(object sender, EventArgs e) {
            var selectedItems = ListViewFoundFiles.SelectedItems;
            if (selectedItems.Count > 0) {
                var selectedItem = selectedItems[0];
                StartSelectedFileUsingShellExecute(selectedItem.Text);
            }
        }

        // Изменение пути поиска при перевыборе диска в выпадающем списке
        private void ComboBoxDrives_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedItem = ComboBoxDrives.SelectedItem;
            if (selectedItem is DriveInfoItem driveInfoItem) {
                string selectedPath = driveInfoItem.DriveName;
                FileSearchInfoHolder.SearchDirectory = selectedPath;
                UpdateSearchPathReadonlyTextBox(selectedPath);
            }
        }
    }
}
