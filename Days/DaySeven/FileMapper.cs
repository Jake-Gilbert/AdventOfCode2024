using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySeven
{
    public class FileMapper
    {
        private string[] _fileArray;
        public long[] LeftSide { get; set; } = new long[0];
        public List<string[]> RightSide { get; set; } = new List<string[]>(); 
        public FileMapper(string fileName)
        {
            _fileArray = File.ReadAllLines(fileName);
            PopulateArrays();
        }

        private void PopulateArrays()
        {
            LeftSide = _fileArray.Select(x => long.Parse(x.Split(":")[0])).ToArray();
            RightSide = _fileArray.Select(x => x.Split(": ").Last()
                                         .Split(" ").ToArray()).ToList();
            Console.WriteLine();
        }           
    }
}
