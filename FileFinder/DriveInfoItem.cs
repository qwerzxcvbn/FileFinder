using System;
using System.IO;

namespace FileFinder
{
    public class DriveInfoItem
    {
        public string DriveName { get; set; }
        public string DriveVolumeLabel { get; set; }
        public string DriveFormat { get; set; }
        public string DriveTypeString { get; set; }
        public long TotalFreeSpaceGb { get; set; }
        public long TotalSizeGb { get; set; }
        public long AvailableFreeSpaceGb { get; set; }

        public DriveInfoItem(DriveInfo driveInfo)
        {
            if (driveInfo == null)
            {
                throw new ArgumentNullException("driveInfo", "Ошибка: параметр не может быть null!");
            }

            DriveName = driveInfo.Name;
            DriveVolumeLabel = driveInfo.VolumeLabel;
            DriveFormat = driveInfo.DriveFormat;
            DriveTypeString = GetDriveTypeAsString(driveInfo.DriveType);

            TotalFreeSpaceGb = GetSizeInGigabytes(driveInfo.TotalFreeSpace);
            TotalSizeGb = GetSizeInGigabytes(driveInfo.TotalSize);
            AvailableFreeSpaceGb = GetSizeInGigabytes(driveInfo.AvailableFreeSpace);
        }

        // Переводит размер из байт в Гигабайты
        private long GetSizeInGigabytes(long size)
        {
            return size / 1_073_741_824;
        }

        private string GetVolumeSizeString()
        {
            return string.Format("Объём: {0}Гб, Всего свободно: {1}Гб, Доступно: {2}Гб", TotalSizeGb, TotalFreeSpaceGb, AvailableFreeSpaceGb);
        }

        public override string ToString()
        {
            return GetReadableDriveName() + ": " + DriveTypeString + ", " + DriveFormat + ", " + GetVolumeSizeString();
        }

        private string GetReadableDriveName()
        {
            if (DriveVolumeLabel == null || DriveVolumeLabel.Length == 0)
            {
                return "[" + DriveName + "]";
            }
            return "[" + DriveVolumeLabel + "] " + DriveName;
        }

        private string GetDriveTypeAsString(DriveType driveType)
        {
            switch (driveType)
            {
                case DriveType.Fixed:
                    return "Фиксированный диск";
                case DriveType.Network:
                    return "Сетевой диск";
                case DriveType.Removable:
                    return "Съёмный диск";
                case DriveType.Ram:
                    return "ОЗУ";
                case DriveType.NoRootDirectory:
                    return "Без корневого каталога";
                case DriveType.CDRom:
                    return "CD-ROM";
                case DriveType.Unknown:
                default:
                    return "Неизвестно";
            }
        }
    }
}