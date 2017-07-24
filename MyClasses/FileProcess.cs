using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyClasses
{
    public class FileProcess
    {
        public bool FileExists(string fileName) //public method that takes a string
        {
            if (string.IsNullOrEmpty(fileName)) //if the string is null or empty
            {
                throw new ArgumentNullException("fileName"); //throw an ArgumentException with the fileName
            }

            return File.Exists(fileName); //otherwise return the fileName
        }
    }
}
