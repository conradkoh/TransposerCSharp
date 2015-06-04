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
        private string _fileDirectory;
        private string _fileName;
        private string _filePath;
        private List<string> _fileContent = new List<string>();
        private Stack<string> _debugInfo = new Stack<string>();
        public File()
        {
            SetFilePath("Default Directory\\Default.txt");
            Load(_filePath);
            
        }

        public File(string filePath)
        
        {
            try
            {
                SetFilePath(filePath);
                Load(filePath);
            }
            catch (Exception e)
            {

            }
            
        }
        public void Save()
        {
            WriteToFile();
        }

        public void Load(string filePath)
        {
            _filePath = filePath;
            string fileDirectory = new System.IO.FileInfo(filePath).Directory.FullName;
            string fileName = new System.IO.FileInfo(filePath).Name;
            try
            {
                if (System.IO.Directory.Exists(fileDirectory))
                {
                    LoadFromFile();
                }
                else
                {
                    _debugInfo.Push("File directory does not exist. Creating Directory.");
                    System.IO.Directory.CreateDirectory(fileDirectory);
                    System.IO.FileStream file = System.IO.File.Create(fileName);
                    file.Close();
                }
                
            }
            catch (Exception e)
            {
                _debugInfo.Push("File load failed, creating new file.");
                System.IO.FileStream file = System.IO.File.Create(filePath);
                file.Close();
                
            }
        }

        public string GetFileName()
        {
            return _fileName;
        }
        public List<string> GetFileContent()
        {
            return _fileContent;
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
            
            string fileDirectory = new System.IO.FileInfo(filePath).Directory.FullName;
            string fileName = new System.IO.FileInfo(filePath).Name;
            if (System.IO.Directory.Exists(fileDirectory) && System.IO.File.Exists(fileName)) 
            {
                _filePath = filePath;
                _fileName = fileName;
                _fileDirectory = fileDirectory;
            }
            
        }
        public void SetDirectory(string directory)
        {
            if (System.IO.Directory.Exists(directory))
            {
                _fileDirectory = directory;
                _filePath = _fileDirectory + _SEPARATOR + _fileName;
            }
        }
        public void SetFileName(string filename)
        {
            _fileName = filename;
            _filePath = _fileDirectory + _SEPARATOR + _fileName;
        }
        private void LoadFromFile()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
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
        private void WriteToFile()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath);
            foreach (string currentLine in _fileContent)
            {
                file.Write(currentLine);
                file.Write(System.Environment.NewLine);
            }
            
            file.Close();
            return;
        }
    }

}
