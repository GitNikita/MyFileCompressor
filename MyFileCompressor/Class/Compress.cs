using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace MyFileCompressor.Class
{
    /// <summary>
    /// Статический класс для вызова компрессии 
    /// </summary>
    public static class Compress
    {
        public static long Compressing(File file)
        {
            int countThreads;
            long finishLenght;

            countThreads = 1;//Environment.ProcessorCount;

            using (FileStream mainStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream gzFileStream = new FileStream(file.FullNameGz, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (GZipStream gzFileCompressStream = new GZipStream(gzFileStream, CompressionMode.Compress))
                    {
                        long size = mainStream.Length / countThreads;
                        Thread[] threads = new Thread[countThreads];
                        for (int i = 0; i < countThreads; i++)
                        {
                            long start = i * size;
                            long partSize = size + (i == countThreads - 1 ? mainStream.Length % countThreads : 0);

                            threads[i] = new Thread(() => WriteFilePart(file, start, partSize));
                        }
                        foreach (Thread t in threads)
                        {
                            t.Start();
                        }

                        foreach (Thread t in threads)
                        {
                            t.Join();
                        }

                        finishLenght = gzFileStream.Length;
                    }
                }
            }
            return finishLenght;
        }

        static void WriteFilePart(File file, long start, long partSize)
        {
            using (FileStream mainStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream gzFileStream = new FileStream(file.FullNameGz, FileMode.Open, FileAccess.Write, FileShare.Write))
                {
                    using (GZipStream gzFileCompressStream = new GZipStream(gzFileStream, CompressionMode.Compress))
                    {
                        mainStream.Position = start;
                        gzFileStream.Position = start;

                        const int bufferSize = 1024;

                        byte[] buffer = new byte[Math.Min(bufferSize, partSize)];
                        while (partSize > 0)
                        {
                            int bytesRead = mainStream.Read(buffer, 0, bufferSize);
                            if (bytesRead == 0) break;

                            gzFileCompressStream.Write(buffer, 0, bytesRead);
                            partSize -= bytesRead;
                        }
                    }
                }
            }
        }
    }
}