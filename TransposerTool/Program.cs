using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TransposerTool
{
    class Program
    {
        const string MESSAGE_HELP = "Sample usage is: \ntranspose a.txt 2\nwhere 2 is the number of semitones you wish to transpose the content of a.txt by";
        const string MESSAGE_ERROR_FILE_NOT_FOUND = "The file specified was not found.";
        static void Main(string[] args)
        {
            
            if(args.Count() == 2)
            {
                string filename_input = args[0];
                string offset_s = args[1];

                if (File.Exists(filename_input))
                {
                    int offset;
                    bool isInt = Int32.TryParse(offset_s, out offset);
                    if (isInt)
                    {
                        //filename is valid and integer for offset is given
                        Transposer_Lib.Song song = new Transposer_Lib.Song(filename_input);
                        song.TransposeByOffset(offset);
                        Console.WriteLine(song.ToString());      
                    }
                    else
                    {
                        //second argument is not an integer
                        Console.Write(MESSAGE_HELP);
                    }
                }
                else
                {
                    Console.WriteLine(MESSAGE_ERROR_FILE_NOT_FOUND);
                    Console.WriteLine(MESSAGE_HELP);
                }
            }
            else
            {
                foreach(string arg in args)
                {
                    Console.WriteLine(MESSAGE_HELP);
                    Console.WriteLine(arg);
                }
            }
        }
    }
}
