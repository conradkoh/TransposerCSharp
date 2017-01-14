using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer_Lib.Helpers
{
    public class Core
    {
        enum KEY { C, CSHARP, D, EFLAT, E, FSHARP, G, GSHARP, A, BFLAT, B, INVALID }
        enum NOTES { C1, C2, D1, D2, E1, F1, F2, G1, G2, A1, A2, B1, INVALID }
        enum DETAILS { VALID, INVALID }
        static string[] notes = { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
        static string[] notes_alias = { "B#", "Db", "D", "D#", "Fb", "E#", "Gb", "G", "Ab", "A", "A#", "Cb" };
        const string invalidCharacters = "cefhklopqrtvwxyznHKLOPQRTVWXYZIJNSU";
        const string blockDelimiterCharacterSet = "<>(){}[]:-!@$%^&*|\\`~/?\'\";_+=\n\r"; 
        //=========================================================
        //Transpose Algorithm Methods
        //=========================================================
        public static string TransposeLine(string input, int offset)
        {
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
        public static string TransposeBlock(string block, int offset)
        {
            string transposedBlock = block; //if all checks fail, default to original block passed in
            if (ShouldTranspose(block))
            {
                string[] tokens = block.Split(null); //null => white space assumed
                var dbg = tokens.Count();
                List<string> new_tokens = new List<string>();
                foreach (string chord in tokens)
                {
                    string new_chord = TransposeSlashChord(chord, offset);
                    new_tokens.Add(new_chord);
                }

                transposedBlock = String.Join(" ", new_tokens);
            }

            return transposedBlock;

        }
        public static string Blend(string chords_unformatted, string lyrics)
        {
            string[] lines_chords_unformatted = chords_unformatted.Split('\n');
            string[] lines_lyrics = lyrics.Split('\n');
            List<string> chords = new List<string>();
            //Add the chords to a chords array
            foreach (string line in lines_chords_unformatted)
            {
                string trimmed = line.Trim();
                if(trimmed == "" || trimmed == "\n")
                {
                    continue;
                }
                else if (Transposer_Lib.Helpers.Core.ContainsChords(line, 0.1))
                {
                    chords.Add(trimmed);
                }
            }

            List<string> output = new List<string>();
            //Insert the chords into the lines array
            int insert_line_index = 0;
            int chord_count = chords.Count;
            int lines_lyrics_count = lines_lyrics.Count();
            int max_lines = Math.Max(chord_count, lines_lyrics_count);
            for (int i = 0; i < chord_count; i++)
            {
                //insert the chord line
                if (i < chord_count)
                {
                    output.Add(chords[i]);
                }

                //insert preceding empty lines
                while ((insert_line_index < lines_lyrics_count) && (lines_lyrics[insert_line_index].Trim() == ""))
                {
                    output.Add(lines_lyrics[insert_line_index]);
                    insert_line_index++;
                }

                //insert a single lyric line
                if (insert_line_index < lines_lyrics_count)
                {
                    output.Add(lines_lyrics[insert_line_index]); //finally insert the lyric line
                    insert_line_index++;
                }

                //insert following empty lines
                while ((insert_line_index < lines_lyrics_count) && (lines_lyrics[insert_line_index].Trim() == ""))
                {
                    output.Add(lines_lyrics[insert_line_index]);
                    insert_line_index++;
                }
            }

            //Add the remaining lines that may not have been added
            while(insert_line_index < lines_lyrics_count)
            {
                output.Add(lines_lyrics[insert_line_index]);
                insert_line_index++;
            }

            return String.Join("\n", output);
        }


        //=========================================================
        //Internal Methods
        //=========================================================
        private static bool ContainsChords(string input, double tolerance)
        {
            //Checks if the function input has chords, within a specific threshold between 0 and 1. i.e 0.5 threshhold means more than half the blocks are chords
            int chordBlocks = 0;
            int notChordBlocks = 0;

            //Begin parsing
            int startIdx = 0;
            int endIdx = input.IndexOfAny(blockDelimiterCharacterSet.ToCharArray(), 0);
            while (endIdx != -1 && endIdx < input.Length && startIdx < input.Length)
            {
                string block = input.Substring(startIdx, endIdx - startIdx);
                if (ShouldTranspose(block))
                {
                    chordBlocks++;
                }
                else
                {
                    notChordBlocks++;
                }
                char blockDelimiter = input.ElementAt(endIdx);


                startIdx = endIdx + 1;
                endIdx = input.IndexOfAny(blockDelimiterCharacterSet.ToCharArray(), startIdx);
            }
            if (endIdx == -1 && !(startIdx > (input.Length - 1)))
            {
                string remainder = input.Substring(startIdx, input.Length - startIdx);
                if (ShouldTranspose(remainder))
                {
                    chordBlocks++;
                }
                else
                {
                    notChordBlocks++;
                }
            }
            bool containsChords = true;
            double total = chordBlocks + notChordBlocks;
            double ratio = notChordBlocks / total;
            if (ratio > tolerance)
            {
                containsChords = false;
            }
            return containsChords;
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
        private static bool IsValidSingleChord(string chord)
        {
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

                if (IsValidSingleDetail(detail) && chordEnum != NOTES.INVALID)
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

            int invalidCharIdx = input.IndexOfAny(invalidCharacters.ToCharArray());

            if (invalidCharIdx == -1)
            {
                invalidCharacterFound = false;
            }

            const int maxValidAug = 22;
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
