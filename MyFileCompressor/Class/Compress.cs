using System.IO;
using System.IO.Compression;

namespace MyFileCompressor.Class
{
    public static class Compress
    {
        public static long Compressing(File file)
        {
            using (FileStream fileStream = file.Info.OpenRead())
            {
                using (FileStream compressedFileStream = System.IO.File.Create(file.Info.FullName + ".gz"))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                       CompressionMode.Compress))
                    {
                        fileStream.CopyTo(compressionStream);
                    }
                }
                FileInfo result = new FileInfo(file.Path + ".gz");
                return result.Length;
            }
        }
    }
}
