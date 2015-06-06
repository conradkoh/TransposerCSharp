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
        
        public Song(string fileName)
        {
            Initialize();
            songFile.Load(fileName);
            songFile.SetDirectory(songDIR);
 //           songFile.Save();
            songLines = songFile.GetFileContent();
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
        public void TransposeUp()
        {
            List<string> newSongLines = new List<string>();
            foreach (string line in songLines)
            {
                string transposedLine = TransposeLine(line, 1);
                newSongLines.Add(transposedLine);
            }

            songLines = newSongLines;
        }
        public void TransposeDown()
        {
            List<string> newSongLines = new List<string>();
            foreach (string line in songLines)
            {
                string transposedLine = TransposeLine(line, -1);
                newSongLines.Add(line);
            }

            songLines = newSongLines;
            return;
        }
        public override string ToString()
        {
            string result = songFile.GetFileName() + System.Environment.NewLine;
            result += songFile.GetFilePath() + System.Environment.NewLine;
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

        public string GetSongDirectory()
        {
            return songDIR;
        }

        //=========================================================
        //Transpose Algorithm Methods
        //=========================================================
        public static string TransposeLine(string input, int offset)
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

            foreach (string chord in tokens)
            {
                if (IsValidChord(chord))
                {
                    validChordCount++;
                }
            }

            if (validChordCount < 2   && tokens.Count() > 6)
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
            if (note != NOTES.INVALID && IsValidDetail(details))
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

        private static bool IsValidChord(string chord){
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
                
                if (IsValidDetail(detail) && chordEnum!=NOTES.INVALID)
                {
                    isValid = true;
                }
                return isValid;
            }
            
        }
        private static bool IsValidDetail(string input)
        {
            bool isValidAug = false;
            bool invalidCharacterFound = true;
            bool isEmptyAug = false;

            if (input == "")
            {
                isEmptyAug = true;
            }

            string invalidCharacters = "cefhklopqrtvwxyzHKLOPQRTVWXYZIJMNSU";
            int invalidCharIdx = input.IndexOfAny(invalidCharacters.ToCharArray());

            if (invalidCharIdx == -1)
            {
                invalidCharacterFound = false;
            }

            const int maxValidAug = 22;
            string[] validAugmentations = new string[maxValidAug];

            validAugmentations[0] = "1";
            validAugmentations[1] = "2";
            validAugmentations[2] = "3";
            validAugmentations[3] = "4";
            validAugmentations[4] = "5";
            validAugmentations[5] = "6";
            validAugmentations[6] = "7";
            validAugmentations[7] = "8";
            validAugmentations[8] = "9";
            validAugmentations[9] = "#";
            validAugmentations[10] = "b";
            validAugmentations[11] = "add";
            validAugmentations[12] = "aug";
            validAugmentations[13] = "dim";
            validAugmentations[14] = "m";
            validAugmentations[15] = "min";
            validAugmentations[16] = "maj";
            validAugmentations[17] = "sus";
            validAugmentations[18] = "10";
            validAugmentations[19] = "11";
            validAugmentations[20] = "12";
            validAugmentations[21] = "13";

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
    }
}
