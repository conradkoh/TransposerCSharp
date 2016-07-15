using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transposer_Lib;
namespace Transposer_Lib
{
    public class Song
    {
        enum KEY { C, CSHARP, D, EFLAT, E, FSHARP, G, GSHARP, A, BFLAT, B, INVALID }
        enum NOTES { C1, C2, D1, D2, E1, F1, F2, G1, G2, A1, A2, B1, INVALID }
        enum DETAILS { VALID, INVALID }
        static string[] notes = new string[12];
        static string[] notes_alias = new string[12];
        static bool isInitalized = false;
        public static string songDIR = System.IO.Directory.GetCurrentDirectory() + "\\Songs";

        File songFile = new File();
        List<string> songLines;
        string expectedFilePath;
        
        public Song(string fileName)
        {
            Initialize();
            songFile.Load(fileName);
            songFile.SetDirectory(songDIR);
            songFile.Save();
            songLines = songFile.GetFileContent();
            expectedFilePath = songFile.GetFilePath();
        }
        private static void Initialize()
        {
            if (!isInitalized)
            {
                notes[(int)NOTES.C1] = "C";
		        notes[(int)NOTES.C2] = "C#";
		        notes[(int)NOTES.D1] = "D";
		        notes[(int)NOTES.D2] = "Eb";
		        notes[(int)NOTES.E1] = "E";
		        notes[(int)NOTES.F1] = "F";
		        notes[(int)NOTES.F2] = "F#";
		        notes[(int)NOTES.G1] = "G";
		        notes[(int)NOTES.G2] = "G#";
		        notes[(int)NOTES.A1] = "A";
		        notes[(int)NOTES.A2] = "Bb";
		        notes[(int)NOTES.B1] = "B";

		        notes_alias[(int)NOTES.C1] = "B#";
		        notes_alias[(int)NOTES.C2] = "Db";
		        notes_alias[(int)NOTES.D1] = "D";
		        notes_alias[(int)NOTES.D2] = "D#";
		        notes_alias[(int)NOTES.E1] = "Fb";
		        notes_alias[(int)NOTES.F1] = "E#";
		        notes_alias[(int)NOTES.F2] = "Gb";
		        notes_alias[(int)NOTES.G1] = "G";
		        notes_alias[(int)NOTES.G2] = "Ab";
		        notes_alias[(int)NOTES.A1] = "A";
		        notes_alias[(int)NOTES.A2] = "A#";
		        notes_alias[(int)NOTES.B1] = "Cb";
		
	    	    isInitalized = true;
            }
        }

        public void TransposeByOffset(int offset)
        {
            List<string> newSongLines = new List<string>();
            foreach (string line in songLines)
            {
                string transposedLine = TransposeLine(line, 1);
                newSongLines.Add(transposedLine);
            }
            songLines = newSongLines;

            //transpose file name
            expectedFilePath = TransposeOnlyLastBracketed(expectedFilePath, offset);
            return;
        }

        public void TransposeUp()
        {
            //transpose file content
            List<string> newSongLines = new List<string>();
            foreach (string line in songLines)
            {
                string transposedLine = TransposeLine(line, 1);
                newSongLines.Add(transposedLine);
            }
            songLines = newSongLines;

            //transpose file name
            expectedFilePath = TransposeOnlyLastBracketed(expectedFilePath, 1);

            return;
        }
        public void TransposeDown()
        {
            //transpose file content
            List<string> newSongLines = new List<string>();
            foreach (string line in songLines)
            {
                string transposedLine = TransposeLine(line, -1);
                newSongLines.Add(transposedLine);
            }
            songLines = newSongLines;

            //transpose file name
            expectedFilePath = TransposeOnlyLastBracketed(expectedFilePath, -1);

            return;
        }
        public override string ToString()
        {
            string result = new System.IO.FileInfo(expectedFilePath).Name;
            result = System.IO.Path.GetFileNameWithoutExtension(result) + System.Environment.NewLine;
            //string result = songFile.GetFileName() + System.Environment.NewLine;
            //result += songFile.GetFilePath() + System.Environment.NewLine;
            result = result + String.Join(System.Environment.NewLine, songLines);
            return result;
        }
        public string GetTitle(){
            string filename = songFile.GetFileName();
            string title = filename;
            if (!String.IsNullOrWhiteSpace(filename))
            {
                int periodIdx = filename.IndexOf(".");
                if (periodIdx != -1)
                {
                    title = filename.Substring(0, periodIdx);
                }
            }
            return title;
        }
        public string GetFileName()
        {
            return songFile.GetFileName();
        }
        public string GetFilePath()
        {
            return songFile.GetFilePath();
        }
        public string GetSongDirectory()
        {
            return songDIR;
        }

        public void SaveTransposed()
        {
            songFile.SetFilePath(expectedFilePath);
            songFile.SetFileContent(songLines);
            songFile.Save();
        }
        //=========================================================
        //Transpose Algorithm Methods
        //=========================================================
        public static string TransposeLine(string input, int offset)
        {
            string blockDelimiterCharacterSet = "<>(){}[]:-!@$%^&*|\\`~/?\'\";_+=\n\r";
            //int location = 0;
            //string scope = input;
            //string output = "";
            //string suffix = input;
            //int startIdx = 0;
            //while (location != -1 && startIdx < input.Length && startIdx != -1)
            //{
            //    location = input.IndexOfAny(bracketCharacterSet.ToCharArray(), startIdx);
            //    if (location != -1)
            //    {
            //        string prefix = input.Substring(startIdx, location - startIdx);
            //        int newStartIdx = location + 1;
            //        if (newStartIdx != -1 && newStartIdx < input.Length)
            //        {
            //            suffix = input.Substring(newStartIdx, input.Length - newStartIdx);
            //            output += TransposeBlock(prefix, offset);
            //            char bracket = input.ElementAt(location);
            //            output += bracket;
            //        }
            //        startIdx = newStartIdx;
            //    }
            //}
            //output += TransposeBlock(suffix, offset);

            int startIdx = 0;
            int endIdx = input.IndexOfAny(blockDelimiterCharacterSet.ToCharArray(), 0);
            string output = "";
            while (endIdx != -1 && endIdx < input.Length && startIdx < input.Length)
            {
                string scope = input.Substring(startIdx, endIdx - startIdx);
                char blockDelimiter = input.ElementAt(endIdx);
                output += TransposeBlock(scope, offset);
                output += blockDelimiter;

                startIdx = endIdx + 1;
                endIdx = input.IndexOfAny(blockDelimiterCharacterSet.ToCharArray(), startIdx);
            }
            if (endIdx == -1 && !(startIdx > (input.Length - 1)))
            {
                string remainder = input.Substring(startIdx, input.Length - startIdx);
                output += TransposeBlock(remainder, offset);
            }
            return output;
        }

        public static string TransposeBlock(string input, int offset)
        {
            string transposedLine = input;
            if (ShouldTranspose(input))
            {
                string[] tokens = input.Split(null); //null => white space assumed
                var dbg = tokens.Count();
                List<string> new_tokens = new List<string>();
                foreach (string chord in tokens)
                {
                    string new_chord = TransposeSlashChord(chord, offset);
                    new_tokens.Add(new_chord);
                }

                transposedLine = String.Join(" ", new_tokens);
            }
            
            return transposedLine;

        }
        private static bool ShouldTranspose(string songLine)
        {
            string[] tokens = songLine.Split(null);
            bool shouldTranspose = true;
            int validChordCount = 0;
            int whitespacecount = 0;

            foreach (string chord in tokens)
            {
                if (IsValidChord(chord))
                {   
                     validChordCount++;
                }
                if (String.IsNullOrWhiteSpace(chord))
                {
                    whitespacecount++;
                }
            }

            int tokenCount = tokens.Count() - whitespacecount;

            //if (validChordCount < 2   && tokenCount > 6)
            //{
            //    shouldTranspose = false;
            //} 
            if (validChordCount != tokenCount)
            {
                shouldTranspose = false;
            }

            return shouldTranspose;

        }
        private static string TransposeSlashChord(string input, int offset)
        {
            string output = input;
            int slashIdx = input.IndexOf("/");
            if (slashIdx != -1)
            {
                string prefix = input.Substring(0, slashIdx);
                int suffixLength = input.Length - slashIdx - 1;
                string suffix = input.Substring(slashIdx + 1, suffixLength);
                string new_prefix = TransposeSlashChord(prefix, offset);
                string new_suffix = TransposeSlashChord(suffix, offset);
                output = new_prefix + "/" + new_suffix;
            }
            else
            {
                output = TransposeSingle(input, offset);
            }

            return output;
        }
        private static string TransposeSingle(string input, int offset)
        {
            string chord = IsolateChord(input);
            string details = IsolateDetails(input);
            NOTES note = GetNoteEnum(chord);
            if (note != NOTES.INVALID && IsValidSingleDetail(details))
            {
                int noteIdx = (int)note;
                
                noteIdx = (noteIdx + (offset % 12) + 12) % 12;
                string transposedChord = notes[noteIdx];
                string result = transposedChord + details;
                return result;
            }
            else
            {
                return input;
            }

        }
        private static NOTES GetNoteEnum(string input)
        {
            Initialize();
            NOTES result = NOTES.INVALID;
            for (int i = 0; i < 12; i++)
            {
                if (notes[i] == input || notes_alias[i] == input)
                {
                    result = (NOTES)i;
                }
            }

            return result;
        }
        private static string IsolateChord(string input)
        {
            if (input.Length <= 1)
            {
                return input;
            }
            else
            {
                if (input[1] == '#' || input[1] == 'b')
                {
                    return input.Substring(0, 2);
                }
                else
                {
                    return Convert.ToString(input[0]);
                }

            }
        }
        private static string IsolateDetails(string input)
        {
            if (input.Length <= 1)
            {
                return "";
            }
            else
            {
                if (input[1] == '#' || input[1] == 'b')
                {
                    return input.Substring(2, input.Length - 2);
                }
                else
                {
                    return input.Substring(1, input.Length - 1);
                }

            }
        }
        private static bool IsValidChord(string chord)
        {
            bool isValid = true;
            string[] tokens = chord.Split('/');
            foreach (string token in tokens)
            {
                if (!IsValidSingleChord(token))
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        private static bool IsValidSingleChord(string chord){
            bool isValid = false;
            if (String.IsNullOrWhiteSpace(chord))
            {
                return isValid;
            }
            else
            {
                string chordBase = IsolateChord(chord);
                string detail = IsolateDetails(chord);
                NOTES chordEnum = GetNoteEnum(chordBase);
                
                if (IsValidSingleDetail(detail) && chordEnum!=NOTES.INVALID)
                {
                    isValid = true;
                }
                return isValid;
            }
            
        }
        private static bool IsValidSingleDetail(string input)
        {
            bool isValidAug = false;
            bool invalidCharacterFound = true;
            bool isEmptyAug = false;

            if (input == "")
            {
                isEmptyAug = true;
            }

            string invalidCharacters = "cefhklopqrtvwxyznHKLOPQRTVWXYZIJNSU";
            int invalidCharIdx = input.IndexOfAny(invalidCharacters.ToCharArray());

            if (invalidCharIdx == -1)
            {
                invalidCharacterFound = false;
            }

            const int maxValidAug = 22;
            //string[] validAugmentations = new string[maxValidAug];
            List<string> validAugmentations = new List<string>();
            validAugmentations.Add("1");
            validAugmentations.Add("2");
            validAugmentations.Add("3");
            validAugmentations.Add("4");
            validAugmentations.Add("5");
            validAugmentations.Add("6");
            validAugmentations.Add("7");
            validAugmentations.Add("8");
            validAugmentations.Add("9");
            validAugmentations.Add("10");
            validAugmentations.Add("11");
            validAugmentations.Add("12");
            validAugmentations.Add("13");
            validAugmentations.Add("#");
            validAugmentations.Add("b");
            validAugmentations.Add("add");
            validAugmentations.Add("aug");
            validAugmentations.Add("dim");
            validAugmentations.Add("m");
            validAugmentations.Add("maj");
            validAugmentations.Add("sus");
            //validAugmentations[0] = "1";
            //validAugmentations[1] = "2";
            //validAugmentations[2] = "3";
            //validAugmentations[3] = "4";
            //validAugmentations[4] = "5";
            //validAugmentations[5] = "6";
            //validAugmentations[6] = "7";
            //validAugmentations[7] = "8";
            //validAugmentations[8] = "9";
            //validAugmentations[9] = "#";
            //validAugmentations[10] = "b";
            //validAugmentations[11] = "add";
            //validAugmentations[12] = "aug";
            //validAugmentations[13] = "dim";
            //validAugmentations[14] = "m";
            //validAugmentations[15] = "min";
            //validAugmentations[16] = "maj";
            //validAugmentations[17] = "sus";
            //validAugmentations[18] = "10";
            //validAugmentations[19] = "11";
            //validAugmentations[20] = "12";
            //validAugmentations[21] = "13";

            foreach (var augmentation in validAugmentations)
            {
                if (input.IndexOf(augmentation) != -1)
                {
                    isValidAug = true;
                }
            }

            bool validityCheck = false;
            if (isValidAug && !invalidCharacterFound)
            {
                validityCheck = true;
            }
            else if (isEmptyAug)
            {
                validityCheck = true;
            }

            return validityCheck;
            
            
        }
        
        //==========================================================
        //Transposer Specific Methods
        //=========================================================

        private static string TransposeOnlyLastBracketed(string input, int offset){
            int startIdx = input.LastIndexOfAny("([{".ToCharArray());
            int endIdx = input.LastIndexOfAny(")]}".ToCharArray());
            string prefix;
            string suffix;
            string output = input;
            if (startIdx != -1 && endIdx != -1)
            {
                prefix = input.Substring(0, startIdx);
                suffix = input.Substring(endIdx + 1, input.Length - endIdx - 1);
                string content = input.Substring(startIdx + 1, endIdx - startIdx - 1);
                content = TransposeLine(content, offset);

                output = prefix + "[" + content + "]" + suffix;
            }

            return output;
            
        }
    }
}
