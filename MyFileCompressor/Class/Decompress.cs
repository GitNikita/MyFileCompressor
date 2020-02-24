using System;
using System.IO;
using System.IO.Compression;

namespace MyFileCompressor.Class
{
    /// <summary>
    /// Статический класс для вызова декомпрессии 
    /// </summary>
    public static class Decompress
    {
        public static void Decompressing(File file)
        {
            using (FileStream originalFileStream = file.Info.OpenRead())
            {
                var currentFileName = file.Path;
                var newFileName = currentFileName.Remove(currentFileName.Length - file.Info.Extension.Length);

                using (FileStream decompressedFileStream = System.IO.File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine($"Decompressed: {currentFileName}");
                    }
                }
            }
        }
    }
}
