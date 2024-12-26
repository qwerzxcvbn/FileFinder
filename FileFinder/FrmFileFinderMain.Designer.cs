namespace FileFinder {
    partial class FrmFileFinderMain {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Найденные файлы", System.Windows.Forms.HorizontalAlignment.Left);
            this.LabelFileName = new System.Windows.Forms.Label();
            this.TextBoxFileName = new System.Windows.Forms.TextBox();
            this.LabelFoundFilesList = new System.Windows.Forms.Label();
            this.ListViewFoundFiles = new System.Windows.Forms.ListView();
            this.columnHeaderFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelProgress = new System.Windows.Forms.Label();
            this.ProgressBarMain = new System.Windows.Forms.ProgressBar();
            this.ButtonStartSearch = new System.Windows.Forms.Button();
            this.BackgroundWorkerSearchFiles = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorkerEstimateSearchTime = new System.ComponentModel.BackgroundWorker();
            this.LabelFilesCount = new System.Windows.Forms.Label();
            this.LabelDrive = new System.Windows.Forms.Label();
            this.ComboBoxDrives = new System.Windows.Forms.ComboBox();
            this.LabelSearchPath = new System.Windows.Forms.Label();
            this.TextBoxSearchPath = new System.Windows.Forms.TextBox();
            this.ButtonSelectSearchDirectory = new System.Windows.Forms.Button();
            this.FolderBrowserDialogSelectSearchDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // LabelFileName
            // 
            this.LabelFileName.AutoSize = true;
            this.LabelFileName.Location = new System.Drawing.Point(14, 91);
            this.LabelFileName.Name = "LabelFileName";
            this.LabelFileName.Size = new System.Drawing.Size(392, 13);
            this.LabelFileName.TabIndex = 0;
            this.LabelFileName.Text = "Полное имя файла, часть имени файла или часть пути, содержащего файл:";
            // 
            // TextBoxFileName
            // 
            this.TextBoxFileName.Location = new System.Drawing.Point(15, 107);
            this.TextBoxFileName.Name = "TextBoxFileName";
            this.TextBoxFileName.Size = new System.Drawing.Size(632, 20);
            this.TextBoxFileName.TabIndex = 1;
            this.TextBoxFileName.TextChanged += new System.EventHandler(this.TextBoxFileName_TextChanged);
            // 
            // LabelFoundFilesList
            // 
            this.LabelFoundFilesList.AutoSize = true;
            this.LabelFoundFilesList.Location = new System.Drawing.Point(14, 146);
            this.LabelFoundFilesList.Name = "LabelFoundFilesList";
            this.LabelFoundFilesList.Size = new System.Drawing.Size(146, 13);
            this.LabelFoundFilesList.TabIndex = 2;
            this.LabelFoundFilesList.Text = "Список найденных файлов:";
            // 
            // ListViewFoundFiles
            // 
            this.ListViewFoundFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFileName,
            this.columnHeaderFileSize});
            this.ListViewFoundFiles.FullRowSelect = true;
            this.ListViewFoundFiles.GridLines = true;
            listViewGroup1.Header = "Найденные файлы";
            listViewGroup1.Name = "listViewGroupFiles";
            this.ListViewFoundFiles.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.ListViewFoundFiles.HideSelection = false;
            this.ListViewFoundFiles.Location = new System.Drawing.Point(15, 172);
            this.ListViewFoundFiles.MultiSelect = false;
            this.ListViewFoundFiles.Name = "ListViewFoundFiles";
            this.ListViewFoundFiles.Size = new System.Drawing.Size(773, 270);
            this.ListViewFoundFiles.TabIndex = 3;
            this.ListViewFoundFiles.UseCompatibleStateImageBehavior = false;
            this.ListViewFoundFiles.View = System.Windows.Forms.View.Details;
            this.ListViewFoundFiles.DoubleClick += new System.EventHandler(this.ListViewFoundFiles_DoubleClick);
            // 
            // columnHeaderFileName
            // 
            this.columnHeaderFileName.Text = "Имя файла";
            this.columnHeaderFileName.Width = 650;
            // 
            // columnHeaderFileSize
            // 
            this.columnHeaderFileSize.Text = "Размер файла, байт";
            this.columnHeaderFileSize.Width = 115;
            // 
            // LabelProgress
            // 
            this.LabelProgress.AutoSize = true;
            this.LabelProgress.Location = new System.Drawing.Point(14, 454);
            this.LabelProgress.Name = "LabelProgress";
            this.LabelProgress.Size = new System.Drawing.Size(59, 13);
            this.LabelProgress.TabIndex = 4;
            this.LabelProgress.Text = "Прогресс:";
            // 
            // ProgressBarMain
            // 
            this.ProgressBarMain.Location = new System.Drawing.Point(15, 470);
            this.ProgressBarMain.MarqueeAnimationSpeed = 30;
            this.ProgressBarMain.Name = "ProgressBarMain";
            this.ProgressBarMain.Size = new System.Drawing.Size(773, 23);
            this.ProgressBarMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBarMain.TabIndex = 5;
            // 
            // ButtonStartSearch
            // 
            this.ButtonStartSearch.Enabled = false;
            this.ButtonStartSearch.Location = new System.Drawing.Point(653, 105);
            this.ButtonStartSearch.Name = "ButtonStartSearch";
            this.ButtonStartSearch.Size = new System.Drawing.Size(135, 23);
            this.ButtonStartSearch.TabIndex = 6;
            this.ButtonStartSearch.Text = "&Начать поиск";
            this.ButtonStartSearch.UseVisualStyleBackColor = true;
            this.ButtonStartSearch.Click += new System.EventHandler(this.ButtonStartSearch_Click);
            // 
            // BackgroundWorkerSearchFiles
            // 
            this.BackgroundWorkerSearchFiles.WorkerReportsProgress = true;
            this.BackgroundWorkerSearchFiles.WorkerSupportsCancellation = true;
            this.BackgroundWorkerSearchFiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerSearchFiles_DoWork);
            this.BackgroundWorkerSearchFiles.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerSearchFiles_ProgressChanged);
            this.BackgroundWorkerSearchFiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerSearchFiles_RunWorkerCompleted);
            // 
            // BackgroundWorkerEstimateSearchTime
            // 
            this.BackgroundWorkerEstimateSearchTime.WorkerReportsProgress = true;
            this.BackgroundWorkerEstimateSearchTime.WorkerSupportsCancellation = true;
            this.BackgroundWorkerEstimateSearchTime.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerEstimateSearchTime_DoWork);
            this.BackgroundWorkerEstimateSearchTime.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerEstimateSearchTime_ProgressChanged);
            this.BackgroundWorkerEstimateSearchTime.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerEstimateSearchTime_RunWorkerCompleted);
            // 
            // LabelFilesCount
            // 
            this.LabelFilesCount.AutoSize = true;
            this.LabelFilesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelFilesCount.Location = new System.Drawing.Point(774, 454);
            this.LabelFilesCount.Name = "LabelFilesCount";
            this.LabelFilesCount.Size = new System.Drawing.Size(14, 13);
            this.LabelFilesCount.TabIndex = 7;
            this.LabelFilesCount.Text = "0";
            // 
            // LabelDrive
            // 
            this.LabelDrive.AutoSize = true;
            this.LabelDrive.Location = new System.Drawing.Point(14, 9);
            this.LabelDrive.Name = "LabelDrive";
            this.LabelDrive.Size = new System.Drawing.Size(97, 13);
            this.LabelDrive.TabIndex = 8;
            this.LabelDrive.Text = "Диск для поиска:";
            // 
            // ComboBoxDrives
            // 
            this.ComboBoxDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDrives.FormattingEnabled = true;
            this.ComboBoxDrives.Location = new System.Drawing.Point(126, 6);
            this.ComboBoxDrives.Name = "ComboBoxDrives";
            this.ComboBoxDrives.Size = new System.Drawing.Size(662, 21);
            this.ComboBoxDrives.TabIndex = 9;
            this.ComboBoxDrives.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDrives_SelectedIndexChanged);
            // 
            // LabelSearchPath
            // 
            this.LabelSearchPath.AutoSize = true;
            this.LabelSearchPath.Location = new System.Drawing.Point(14, 41);
            this.LabelSearchPath.Name = "LabelSearchPath";
            this.LabelSearchPath.Size = new System.Drawing.Size(94, 13);
            this.LabelSearchPath.TabIndex = 10;
            this.LabelSearchPath.Text = "Путь для поиска:";
            // 
            // TextBoxSearchPath
            // 
            this.TextBoxSearchPath.Location = new System.Drawing.Point(15, 57);
            this.TextBoxSearchPath.Name = "TextBoxSearchPath";
            this.TextBoxSearchPath.ReadOnly = true;
            this.TextBoxSearchPath.Size = new System.Drawing.Size(632, 20);
            this.TextBoxSearchPath.TabIndex = 11;
            // 
            // ButtonSelectSearchDirectory
            // 
            this.ButtonSelectSearchDirectory.Location = new System.Drawing.Point(653, 55);
            this.ButtonSelectSearchDirectory.Name = "ButtonSelectSearchDirectory";
            this.ButtonSelectSearchDirectory.Size = new System.Drawing.Size(135, 23);
            this.ButtonSelectSearchDirectory.TabIndex = 12;
            this.ButtonSelectSearchDirectory.Text = "&Обзор...";
            this.ButtonSelectSearchDirectory.UseVisualStyleBackColor = true;
            this.ButtonSelectSearchDirectory.Click += new System.EventHandler(this.ButtonSelectSearchDirectory_Click);
            // 
            // FrmFileFinderMain
            // 
            this.AcceptButton = this.ButtonStartSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 523);
            this.Controls.Add(this.ButtonSelectSearchDirectory);
            this.Controls.Add(this.TextBoxSearchPath);
            this.Controls.Add(this.LabelSearchPath);
            this.Controls.Add(this.ComboBoxDrives);
            this.Controls.Add(this.LabelDrive);
            this.Controls.Add(this.LabelFilesCount);
            this.Controls.Add(this.ButtonStartSearch);
            this.Controls.Add(this.ProgressBarMain);
            this.Controls.Add(this.LabelProgress);
            this.Controls.Add(this.ListViewFoundFiles);
            this.Controls.Add(this.LabelFoundFilesList);
            this.Controls.Add(this.TextBoxFileName);
            this.Controls.Add(this.LabelFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFileFinderMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиска файлов";
            this.Load += new System.EventHandler(this.FrmFileFinderMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelFileName;
        private System.Windows.Forms.TextBox TextBoxFileName;
        private System.Windows.Forms.Label LabelFoundFilesList;
        private System.Windows.Forms.ListView ListViewFoundFiles;
        private System.Windows.Forms.ColumnHeader columnHeaderFileName;
        private System.Windows.Forms.ColumnHeader columnHeaderFileSize;
        private System.Windows.Forms.Label LabelProgress;
        private System.Windows.Forms.ProgressBar ProgressBarMain;
        private System.Windows.Forms.Button ButtonStartSearch;
        private System.ComponentModel.BackgroundWorker BackgroundWorkerSearchFiles;
        private System.ComponentModel.BackgroundWorker BackgroundWorkerEstimateSearchTime;
        private System.Windows.Forms.Label LabelFilesCount;
        private System.Windows.Forms.Label LabelDrive;
        private System.Windows.Forms.ComboBox ComboBoxDrives;
        private System.Windows.Forms.Label LabelSearchPath;
        private System.Windows.Forms.TextBox TextBoxSearchPath;
        private System.Windows.Forms.Button ButtonSelectSearchDirectory;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialogSelectSearchDirectory;
    }
}

