using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DayOne
{
    public class TextMapper
    {
        public string PathName { get; set; }
        public List<List<int>> Lists { get; set; } = new List<List<int>>();

        public TextMapper(string pathName)
        {
            PathName = pathName;
            Lists = MapFileToData();
        }

        private List<List<int>> MapFileToData()
        {
            try
            {
                string[] lines = File.ReadAllLines(PathName);
                var leftList = new List<int>();
                var rightList = new List<int>();

                if (lines.Length > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        leftList.Add(int.Parse(line.First()));
                        rightList.Add(int.Parse(line.Last()));
                    }
                }
                else
                {
                    Console.WriteLine("The file is empty.");
                }
                return new List<List<int>>
                {
                    leftList, rightList
                };
            }
            catch (Exception e)
            {
                throw new FileNotFoundException(e.Message);
            }
        }
    }
}
