using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer_Lib;
namespace Transposer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            File file = new File();
            file.Load(".\\New Folder\\a.txt");
            string songline = "A B C D";
            List<string> fileContent = new List<string>();
            fileContent.Add(songline);
            file.SetFileContent(fileContent);
            file.Save();
            Song mysong = new Song(".\\New Folder\\a.txt");
            int max = 12;
            for (int i = 0; i < max; ++i)
            {
                mysong.TransposeUp();
                Console.WriteLine(mysong.ToString());
            }


                Console.ReadKey();
        }
    }
}
