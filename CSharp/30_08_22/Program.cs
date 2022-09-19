using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSarp
{
    class Program
    {
        public static void Main(string[] args)
        {
            void readFiles(string path, string mask, TextWriter write)
            {
                DirectoryInfo dinfo = new DirectoryInfo(path);

                if (dinfo.Exists)
                {
                    // Получить массив файлов в текущей папке
                    try
                    {
                        FileInfo[] files = dinfo.GetFiles(mask);
                        foreach (FileInfo current in files)
                            write.WriteLine(current.Name);

                        // Получить массив подпапок в текущей папке
                        DirectoryInfo[] dirs = dinfo.GetDirectories();
                        foreach (DirectoryInfo current in dirs)
                        {
                            if (new string[] { "bin", "obj" }.Contains(current.Name))
                                Directory.Delete(current.FullName, true);
                            else
                                readFiles(path + "/" + current.Name, mask, write);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("Path is not exists");
            }




            string filename = "/home/omon/ITStep_PD111/CSharp/data.log";
            string path = "/home/omon/ITStep_PD111/CSharp";
            StreamWriter writer = new StreamWriter(filename, false, Encoding.Default);
            readFiles(path, "*.cs", writer);
            writer.Close();
        }
    }
}