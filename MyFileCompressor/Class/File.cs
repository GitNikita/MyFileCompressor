using System.IO;

namespace MyFileCompressor.Class
{
    /// <summary>
    /// Файл используемый в компрессии 
    /// </summary>
    public class File
    {
        /// <summary>
        /// Короткое название файла
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Короткое название файла
        /// </summary>
        public string FullNameGz { get; private set; }
        /// <summary>
        /// Полное название файла
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Размер до компрессии в байтах
        /// </summary>
        public long? SizeByteBeforeCompress { get; private set; }

        /// <summary>
        /// Размер после компрессии в байтах
        /// </summary>
        public long? SizeByteAfterCompress { get; set; }

        /// <summary>
        /// Свойство для получения информации из файла
        /// </summary>
        public FileInfo Info { get; private set; }

        /// <summary>
        /// То на сколько можно поделить файл, исп. для параллельности компрессии
        /// </summary>
        public int MinPart { get; private set; }
        /// <summary>
        /// Процент успеха по компрессии
        /// </summary>
        public int CompressionRatio { get; private set; }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="path"></param>
        public File(string path)
        {
            FullName = path;
            Info = new FileInfo(path);
            SizeByteBeforeCompress = Info.Length;
            Name = Info.Name;
            FullNameGz = FullName + ".gz";
            MinPart = SizeByteBeforeCompress <= 1024 ? 1 : (int)( SizeByteBeforeCompress / 1024 );
        }

        /// <summary>
        /// Получаем инфу по файлу
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Название файла:{Name}, размер до сжатия: {SizeByteBeforeCompress}, " +
                $"размер после сжатия: {SizeByteAfterCompress}, процент сжатия: {CompressionRatio}" +
                $"разделение на {MinPart} частей";
        }
    }
}

