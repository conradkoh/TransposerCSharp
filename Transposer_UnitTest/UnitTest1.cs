using System;
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
    }
}
