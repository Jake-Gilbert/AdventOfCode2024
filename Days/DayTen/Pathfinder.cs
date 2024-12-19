namespace DayTen
{
    public class Pathfinder
    {
        private Dictionary<(int, int), int> _grid = new Dictionary<(int, int), int>();
        private int _height = 0;
        private int _width = 0;

        public Pathfinder(Dictionary<(int, int), int> grid, int height, int width)
        {
            _grid = grid;
            _height = height;
            _width = width;
        }

        public Dictionary<(int, int), int> BreadthFirstSearch((int, int) start)
        {
            Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue(start);
            var distinctScore = 0;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (!visited.ContainsKey(node))
                {
                    var neighbours = ValidPathCoordinates(node, _grid[node] + 1);
                    visited.Add(node, _grid[node]);
                    if (neighbours.Count > 0)
                    {
                        foreach (var neighbour in neighbours)
                        {
                            if (_grid[neighbour] == 9)
                            {
                                distinctScore += 1;
                            }
                            queue.Enqueue(neighbour);
                        }
                    }
                }
            }
            return visited;
        }

        public Dictionary<(int, int), int> BreadthFirstSearchDistinct((int, int) start, out int distinctScore)
        {
            Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue(start);
            distinctScore = 0;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                //if (!visited.ContainsKey(node))
                //{
                    var neighbours = ValidPathCoordinates(node, _grid[node] + 1);
                    //visited.Add(node, _grid[node]);
                    if (neighbours.Count > 0)
                    {
                        foreach (var neighbour in neighbours)
                        {
                            if (_grid[neighbour] == 9)
                            {
                                distinctScore += 1;
                            }
                            queue.Enqueue(neighbour);
                        }
                   // }
                }
            }
            return visited;
        }

        public List<(int, int)> ValidPathCoordinates((int, int) currentCoordinate, int target)
        {
            var coordinates = new List<(int, int)>();
            if (Search(currentCoordinate, -1, 0, target, out var north))
            {
                coordinates.Add(north);
            }
            if (Search(currentCoordinate, 1, 0, target, out var south))
            {
                coordinates.Add(south);
            }
            if (Search(currentCoordinate, 0, 1, target, out var east))
            {
                coordinates.Add(east);
            }
            if (Search(currentCoordinate, 0, -1, target, out var west))
            {
                coordinates.Add(west);
            }
            return coordinates;
        }

        bool Search((int, int) coordinate, int rowOffset, int colOffset, int target, out (int, int) validRoute)
        {
            var row = coordinate.Item1 + rowOffset;
            var col = coordinate.Item2 + colOffset;// % 10;
            if (OutOfBounds(row, col))
            {
                validRoute = (-1, -1);
                return false;
            }
            if (_grid[(row, col)] == target)
            {
                validRoute = (row, col);
                return true;
            }
            validRoute = (-1, -1);
            return false;
        }

        private bool OutOfBounds (int row, int col)
        {
            return (row < 0 || row >= _height || col < 0 || col >= _width);
        }
    }
}
