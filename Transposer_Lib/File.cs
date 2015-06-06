using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transposer_Lib
{
    public class File
    {
        private const string _SEPARATOR = "\\";
        public static string _defaultDIR = System.IO.Directory.GetCurrentDirectory() + "\\Default Directory";

        private string _filePath;

        private List<string> _fileContent = new List<string>();
        private Stack<string> _debugInfo = new Stack<string>();
        public File()
        {
        }

        public File(string filePath){
            Load(filePath);
        }

        public void Save()
        {
            WriteToFile(_filePath);
        }

        public void SaveAs(string filePath)
        {
            WriteToFile(filePath);
        }

        public void Append(string input)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath);
            file.Close();
        }

        public void Load(string filePath)
        {
            try
            {
                SetFilePath(filePath);
                _fileContent.Clear();
                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                string line;
                do
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        _fileContent.Add(line);
                    }
                } while (line != null);
                file.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                CreateNewFile(filePath);
            }
            
            return;
        }

        private void CreateNewFile(string filePath)
        {
            string fileDirectory = new System.IO.FileInfo(filePath).Directory.FullName;
            string fileName = new System.IO.FileInfo(filePath).Name;
            System.IO.Directory.CreateDirectory(fileDirectory);
            System.IO.FileStream file = System.IO.File.Create(filePath);
            file.Close();
        }
        public string GetFileName()
        {
            string output = new System.IO.FileInfo(_filePath).Name;
            return output;
        }
        public string GetFilePath()
        {
            return _filePath;
        }

        public List<string> GetFileContent()
        {
            return new List<string>(_fileContent);
        }
        public void SetFileContent(List<string> fileContent)
        {
            _fileContent = fileContent;
        }
        public void SetFileContent(string fileContent)
        {

            _fileContent = new List<string>();
            _fileContent.Add(fileContent);
        }
        public void SetFilePath(string filePath)
        {
            string fileDirectory = _defaultDIR;
            try
            {
                fileDirectory = new System.IO.FileInfo(filePath).Directory.FullName;
            }
            catch (Exception e)
            {

            }
            string fileName = "unknown.file";
            //try
            //{
                fileName = new System.IO.FileInfo(filePath).Name;
            //}
            //catch (Exception e)
            //{

            //}
            _filePath = fileDirectory + _SEPARATOR + fileName;
        }
        public void SetDirectory(string directory)
        {
            string fileName = new System.IO.FileInfo(_filePath).Name;
            System.IO.Directory.CreateDirectory(directory);
            _filePath = directory + _SEPARATOR + fileName;
        }

        public void SetRelativeSubDirectory(string directory)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string resultantDirectory = currentDirectory + _SEPARATOR + directory + _SEPARATOR;
            string filename = new System.IO.FileInfo(_filePath).Name;
            _filePath = resultantDirectory + filename;
        }
        public void SetFileName(string filename)
        {
            string directory = _defaultDIR;
            try
            {
                directory = new System.IO.FileInfo(_filePath).Directory.FullName;
            }
            catch (Exception e)
            {
                _debugInfo.Push("Specified directory doesn't exist. Using default directory");
            }
            _filePath = directory + _SEPARATOR + filename;
        }
        private void Reload()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
            _fileContent.Clear();
            string line;
            do
            {
                line = file.ReadLine();
                if (line != null)
                {
                    _fileContent.Add(line);
                }
            } while (line != null);
            file.Close();
            return;
        }
        private void WriteToFile(string filePath)
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
                foreach (string currentLine in _fileContent)
                {
                    file.Write(currentLine);
                    file.Write(System.Environment.NewLine);
                }

                file.Close();
            }
            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException)
                {
                    System.IO.File.Create(filePath).Close();
                    System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
                    foreach (string currentLine in _fileContent)
                    {
                        file.Write(currentLine);
                        file.Write(System.Environment.NewLine);
                    }

                    file.Close();
                }
                if (e is System.IO.DirectoryNotFoundException)
                {
                    SetDirectory(_defaultDIR);
                    System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath);
                    foreach (string currentLine in _fileContent)
                    {
                        file.Write(currentLine);
                        file.Write(System.Environment.NewLine);
                    }

                    file.Close();
                }
            }
           
            return;
        }
    }

}
