using System;
using System.IO;

namespace BRConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("No directory");
                return;
            }

            var directory = new DirectoryInfo(args[0]);
            if (!directory.Exists)
            {
                Console.WriteLine("Directory does not exist!");
                return;
            }

            var files = directory.GetFiles("*.BR5", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                using var br5 = new FileStream(file.FullName, FileMode.Open);
                using var wma = new FileStream(file.FullName.Replace(".BR5", ".wma"), FileMode.CreateNew);
                int tmp = 0;

                while ((tmp = br5.ReadByte()) != -1)
                {
                    wma.WriteByte((byte)~tmp);
                }
            }            
        }
    }
}
