namespace DayTwo
{
    public class TextMapper
    {
        public string PathName { get; set; }
        public Dictionary<int, List<int>> Reports { get; set; } = new Dictionary<int, List<int>>();

        public TextMapper(string pathName)
        {
            PathName = pathName;
            Reports = MapFileToData();
        }

        private Dictionary<int, List<int>> MapFileToData()
        {
            try
            {
                string[] lines = File.ReadAllLines(PathName);
                var reportsList = new Dictionary<int, List<int>>();

                if (lines.Length > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var line = lines[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        var reportList = new List<int>();
                        foreach (var value in line)
                        {
                            reportList.Add(int.Parse(value));
                        }
                        reportsList.Add(i, reportList);
                    }
                }
                else
                {
                    Console.WriteLine("The file is empty.");
                }
                return reportsList;
            }
            catch (Exception e)
            {
                throw new FileNotFoundException(e.Message);
            }
        }
    }
}
