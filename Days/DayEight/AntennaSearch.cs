using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayEight
{
    public class AntennaSearch
    {
        public Dictionary<char, List<string>> AntennaLocations = new Dictionary<char, List<string>>();
        private string[] _grid = Array.Empty<string>();
        private List<string> _renderGrid = new List<string>();
        private Dictionary<string, bool> _frequencyPositions = new Dictionary<string, bool>();
        public AntennaSearch(string[] grid)
        {
            _grid = grid;
            Scan();
        }

        private void Scan()
        {
            for (int i = 0; i < _grid.Length; i++)
            {
                var row = "";
                for (int j =0; j < _grid.First().Length; j++)
                {
                    var characterAtLocation = _grid[i][j];
                    row += characterAtLocation.ToString();
                    if (characterAtLocation != '.')
                    {
                        if (!AntennaLocations.ContainsKey(characterAtLocation))
                        {
                            AntennaLocations.Add(characterAtLocation, new List<string> { $"{i}-{j}" });
                        }
                        else
                        {
                            AntennaLocations[characterAtLocation].Add($"{i}-{j}");
                        }
                    }
                }
                _renderGrid.Add(row);
            }
            RenderSatelliteMap();
        }

        public int CalculateFreuqencyPositions()
        {
            foreach (var location in AntennaLocations.Keys)
            {
                var sameFreqLocations = AntennaLocations[location].Select(x => x).ToList();
                FireSignalAtAllNeighbours(sameFreqLocations);
            }
            RenderSatelliteMap();
            return _frequencyPositions.Count;
        }

        public void FireSignalAtAllNeighbours(List<string> sameFreqLocations)
        {
            var ignoreList = new List<string>();
            for (int i =0; i < sameFreqLocations.Count; i++)
            {
                if (!ignoreList.Contains(sameFreqLocations[i]))
                {
                    var othersOfSameFrequency = sameFreqLocations.Where(x => x != sameFreqLocations[i] && !ignoreList.Contains(x)).ToList();
                    AddAllPossibleAntinodes(sameFreqLocations[i], othersOfSameFrequency);
                }
                ignoreList.Add(sameFreqLocations[i]);
            }
        }

        private void AddAllPossibleAntinodes(string satellite, List<string> others)
        {
            var current = satellite.Split("-").Select(x => int.Parse(x)).ToArray();
            foreach(var other in others)
            {
                var otherPos = other.Split("-").Select(x => int.Parse(x)).ToArray();
                CreateAntiNode(current, otherPos);
            }
        }

        //Part One
        /*public void CreateAntiNode(int[] curr, int[] opp )
        {
            var distanceX = Math.Abs(curr[0] - opp[0]);
            var distanceY = Math.Abs(curr[1] - opp[1]);

            var coordinatesToTry =  DetermineCoordinatesToTry(curr, opp, distanceX, distanceY);
                
            foreach(var co in coordinatesToTry)
            {
                var arr = co.Split(".");
                var x = int.Parse(arr[0]);
                var y = int.Parse(arr[1]);
                if (x < 0 || x >= _renderGrid.Count || y < 0 || y >= _renderGrid.First().Count())
                {
                    continue;
                }

                var distanceA = (int) Math.Sqrt(Math.Pow(x - curr[0], 2) + Math.Pow(y - curr[1], 2));
                var distanceB = (int) Math.Sqrt(Math.Pow(x - opp[0], 2) + Math.Pow(y - opp[1], 2));

                if (distanceA / distanceB == 2 || distanceB / distanceA  == 2)
                {
                    var row = _renderGrid[x].ToCharArray();
                    var temp = row[y];
                    row[y] = '#';
                    _renderGrid[x] = string.Join("", row);
                    if (temp != '#')
                    {
                        _antinodeCount++;
                    }
                }

            }
        }*/

        public void CreateAntiNode(int[] curr, int[] opp)
        {
            var distanceY = Math.Abs(curr[0] - opp[0]);
            var distanceX = Math.Abs(curr[1] - opp[1]);

            if (curr[1] > opp[1])
            {
                DrawCoordinatesInLine(curr[0], curr[1], -distanceY, distanceX);
                DrawCoordinatesInLine(opp[0], opp[1], distanceY, -distanceX);
            }
            else
            {
                DrawCoordinatesInLine(curr[0], curr[1], -distanceY, -distanceX);
                DrawCoordinatesInLine(opp[0], opp[1], distanceY, distanceX);
            }
            if (!_frequencyPositions.ContainsKey($"{curr[0]}-{curr[1]}"))
            {
                _frequencyPositions.Add($"{curr[0]}-{curr[1]}", true);
            }
            if (!_frequencyPositions.ContainsKey($"{opp[0]}-{opp[1]}"))
            {
                _frequencyPositions.Add($"{opp[0]}-{opp[1]}", true);
            }
        }

        /*private bool TwoOrMoreInLine(int y, int x, int yDiff, int xDiff)
        {
            x += xDiff;
            y += yDiff;
            while (InBounds(x, y))
            {
                if (!_frequencyPositions.ContainsKey($"{y}-{x}"))
                {
                    _frequencyPositions.Add($"{y}-{x}", true);
                }
                else
                {
                    var row = _renderGrid[y].ToCharArray();
                    var temp = row[x];
                    if (temp == _currentAntenna)
                    {
                        return true;
                    }
                }

                x += xDiff;
                y += yDiff;
            }
            return false;*/

        private void DrawCoordinatesInLine(int y, int x, int yDiff, int xDiff)
        {
            x += xDiff;
            y += yDiff;
            if (InBounds(x, y))
            {
                var row = _renderGrid[y].ToCharArray();
                var temp = row[x];

                if (!_frequencyPositions.ContainsKey($"{y}-{x}"))
                {
                    _frequencyPositions.Add($"{y}-{x}", true);
                    row[x] = '#';
                    _renderGrid[y] = string.Join("", row);
                }
                DrawCoordinatesInLine(y, x, yDiff, xDiff);
            }
            return;
        }

        private bool InBounds(int currentX, int currentY)
        {
            return (currentX >= 0 && currentX < _renderGrid.First().Length && currentY >= 0 && currentY < _renderGrid.Count);
        }

        //PartOne
        public string[] DetermineCoordinatesToTry(int[] curr, int[] opp, int diffX, int diffY)
        {
            var coordinatesToTry = new string[2];
            if (curr[1] > opp[1])
            {
                coordinatesToTry[0] = $"{curr[0] - diffX}.{curr[1] + diffY}";
                coordinatesToTry[1] = $"{opp[0] + diffX}.{opp[1] - diffY}";
            }
            else
            {
                coordinatesToTry[0] = $"{curr[0] - diffX}.{curr[1] - diffY}";
                coordinatesToTry[1] = $"{opp[0] + diffX}.{opp[1] + diffY}";
            }
            return coordinatesToTry;
        }

        public void RenderSatelliteMap()
        {
            Console.Clear();
            foreach(var row in _renderGrid)
            {
                Console.WriteLine(row);
            }
        }
    }
}
