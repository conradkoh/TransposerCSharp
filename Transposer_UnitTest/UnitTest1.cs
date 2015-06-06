using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transposer_Lib;
namespace Transposer_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Chinese_Text_Array()
        {
            string input = "莊子";
            string actual = "莊";
            Assert.AreEqual(input[0], actual[0]);
            Assert.IsTrue('莊' == input[0]);
        }
        [TestMethod]
        public void TRANSPOSELINE()
        {
            string actual;
            string expected;
            //Testing the partition where index > 0 & index < 12
            actual = Transposer_Lib.Song.TransposeLine("A  B  C  D", 1);
            expected = "Bb  C  C#  Eb";
            Assert.AreEqual(expected, actual);
            
            actual = Transposer_Lib.Song.TransposeLine("Asus/Em7dim  Bmaj/Fmin5dim  C#/Gb  Db/A#m7", 1);
            expected = "Bbsus/Fm7dim  Cmaj/F#min5dim  D/G  D/Bm7";
            Assert.AreEqual(expected, actual);

            //Testing the partition where index < 0
            actual = Transposer_Lib.Song.TransposeLine("Asus/Em7dim  Bmaj/Fmin5dim  C#/Gb  Db/A#m7", -1);
            expected = "G#sus/Ebm7dim  Bbmaj/Emin5dim  C/F  C/Am7";
            Assert.AreEqual(expected, actual);

            actual = Transposer_Lib.Song.TransposeLine("Asus/Em7dim  Bmaj/Fmin5dim  C#/Gb  Db/A#m7", -25);
            expected = "G#sus/Ebm7dim  Bbmaj/Emin5dim  C/F  C/Am7";
            Assert.AreEqual(expected, actual);
            

            //Testing the partition where the input is a chinese character
            actual = Transposer_Lib.Song.TransposeLine("莊子", 1);
            expected = "莊子";
            Assert.AreEqual(expected, actual);

            //Testing the partition where the first letter is has an invalid chord
            actual = Transposer_Lib.Song.TransposeLine("Hello I Jesus Killer Like Miss Now Other Pink Quartz Rink Single Timbre Under Violent Wax Xylophone Yes Zinc", 1);
            expected = "Hello I Jesus Killer Like Miss Now Other Pink Quartz Rink Single Timbre Under Violent Wax Xylophone Yes Zinc";
            Assert.AreEqual(expected, actual);


            //Testing the partition where the first letter is a valid chord
            actual = Transposer_Lib.Song.TransposeLine("Absinthe Brainy Character Diminish Ether Family Grace", 1);
            expected = "Absinthe Brainy Character Diminish Ether Family Grace";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MODULO_ARITHMETIC()
        {
            int expected;
            int actual;
            int input;

            input = -71;
            expected = -1;
            actual = input % 7;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FILETEST()
        {
            File file = new File();
            file.Load("file.txt");
            string input;
            string input2;
            string input3;
            string expected;
            string actual;
            List<string> setContent = new List<string>();
            List<string> fileContent = new List<string>();
            
            //Testing the partition with one input line
            input = "this is a test input";
            expected = "this is a test input";
            file.SetFileContent(input);
            fileContent.Clear();
            fileContent = file.GetFileContent();
            actual = fileContent.First();
            Assert.AreEqual(expected, actual);

            //Testing the partition with 3 input lines
            input = "this is a test input";
            input2 = "this is a test input2";
            input3 = "this is a test input3";
            expected = "this is a test input\nthis is a test input2\nthis is a test input3";
            setContent.Clear();
            setContent = new List<string>();
            setContent.Add(input);
            setContent.Add(input2);
            setContent.Add(input3);

            file.SetFileContent(setContent);
            fileContent = file.GetFileContent();
            actual = string.Join("\n", fileContent);
            Assert.AreEqual(expected, actual);

            //Testing using a foreach loop to interate and add
            string[] inputArr = new string[3];
            inputArr[0] = "line 1";
            inputArr[1] = "line 2";
            inputArr[2] = "line 3";
            setContent.Clear();
            foreach (string line in inputArr)
            {
                setContent.Add(line);
            }
            file.SetFileContent(setContent);
            file.Save();
            fileContent = file.GetFileContent();
            actual = string.Join("\n", fileContent);
            expected = "line 1\nline 2\nline 3";
            Assert.AreEqual(expected, actual);

            //Testing the save method
            file.Load("file.txt");
            file.Save();
            file.Load("file.txt");
            fileContent = file.GetFileContent();
            List<string> actualFileContent = new List<string>();
            actualFileContent.Add("line 1");
            actualFileContent.Add("line 2");
            actualFileContent.Add("line 3");
            actual = String.Join(System.Environment.NewLine, actualFileContent);;
            expected = String.Join(System.Environment.NewLine, fileContent);
            Assert.AreEqual(expected, actual);

            //Testing the set directory method, check original file
            file.Load("file.txt");
            file.SetDirectory(".\\Test Directory");
            file.Save();
            file.Load("file.txt");
            fileContent = file.GetFileContent();
            actualFileContent = new List<string>();
            actualFileContent.Add("line 1");
            actualFileContent.Add("line 2");
            actualFileContent.Add("line 3");
            actual = String.Join(System.Environment.NewLine, actualFileContent); ;
            expected = String.Join(System.Environment.NewLine, fileContent);
            Assert.AreEqual(expected, actual);

            //Testing the set directory method, check new file;
            file.Load("file.txt");
            fileContent = file.GetFileContent();
            file.Load(".\\Test Directory\\file.txt");
            actualFileContent = file.GetFileContent();
            actual = String.Join(System.Environment.NewLine, fileContent);
            expected = String.Join(System.Environment.NewLine, actualFileContent);
            Assert.AreEqual(expected, actual);




        }

        [TestMethod]

        public void SONG_CLASS()
        {
            Song song;
            string[] filenames = new string[3];
            string input;
            string actual;
            string expected;

            filenames[0] = "a.txt";
            song = new Song(filenames[0]);
            actual = song.GetTitle();
            expected = "a";
            Assert.AreEqual(expected, actual);
        }
    }
}
