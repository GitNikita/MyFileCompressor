using MyFileCompressor.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = MyFileCompressor.Class.File;

namespace MyFileCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Укажите путь к файлу");
            var defaultPath = @"C:\Users\nikit\Temp";
            DirectoryInfo directorySelected = new DirectoryInfo(defaultPath);
            FileInfo[] files = directorySelected.GetFiles();

            var i = 0;
            foreach (var file in files)
            {
                Console.WriteLine($"{i}, {file.Name}, {file.Length}");
                i++;
            }

            Console.WriteLine("Введите порядковый номер файла");
            var NumberFile = Int16.Parse(Console.ReadLine());

            
            File f = new File(files[NumberFile].FullName);

            Console.WriteLine(f.ToString());

            f.SizeByteAfterCompress = Compress.Compressing(f);

            Console.WriteLine(f.ToString());

            //File f1 = new File(f.FullName + "dddd.gz");
            //Decompress.Decompressing(f1);
        }
    }
}
