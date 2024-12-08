using DaySix.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySix
{
    public class PatrolSimulator
    {
        private int _mapWidth;
        private string[] _map;
        private GuardDirection _direction;
        private int _guardX;
        private int _guardY;
        private bool _stuckInLoop;
        private bool _escaped;

        private Dictionary<string, bool> _traversalRecord { get; set; } = new Dictionary<string, bool>();
        private Dictionary<string, char> renderDictionary = new Dictionary<string, char>();
        private Dictionary<string, int> _contactedObstacles = new Dictionary<string, int>();
        public PatrolSimulator(string[] map, bool isObstacleSimulation, string obstacleIndex)
        {
            _map = map;
            _mapWidth = _map.FirstOrDefault()?.Length ?? 0;
            var guardPosition = string.Join("", map).IndexOf("^");
            _guardX = guardPosition % _mapWidth;
            _guardY = guardPosition / map.Length;        
            GenerateObstacleMap(obstacleIndex);
            _direction = GuardDirection.North;
        }

        private void GenerateObstacleMap(string obstacleLocation)
        {
            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _mapWidth; j++)
                {
                    if (_map[i][j] != '^' || _map[i][j] != '#')
                    {
                        _traversalRecord.Add($"{i}-{j}", false);
                    }
                    else
                    {
                        _traversalRecord.Add($"{i}-{j}", true);
                    }
                    renderDictionary.Add($"{i}-{j}", _map[i][j]);
                }
            }
            renderDictionary[obstacleLocation] = '0';
        }

        public PatrolStatistics SimulatePatrol()
        {
            var timesTouchedObstacle = 0;
            while (_guardY > 0 && _guardY < _map.Length && _guardX > 0 && _guardX < _mapWidth && !_stuckInLoop)
            {
                if (_contactedObstacles.Values.Any(x => x > 8))
                {
                    _stuckInLoop = true;
                }
                switch (_direction)
                {
                    case GuardDirection.North:
                        if (ObstaclePresentInNextCoordinate(_guardX, _guardY, 0, -1, ref timesTouchedObstacle))
                        {
                            _direction = GuardDirection.East;
                            renderDictionary[$"{_guardY}-{_guardX}"] = GetDirectionCharFromEnum(_direction);
                        }
                        else
                        {
                            MoveOneSpace(ref _guardX, ref _guardY, 0, -1);
                        }
                        break;
                    case GuardDirection.South:
                        if (ObstaclePresentInNextCoordinate(_guardX, _guardY, 0, 1, ref timesTouchedObstacle))
                        {
                            _direction = GuardDirection.West;
                            renderDictionary[$"{_guardY}-{_guardX}"] = GetDirectionCharFromEnum(_direction);
                        }
                        else
                        {
                            MoveOneSpace(ref _guardX, ref _guardY, 0, 1);
                        }
                        break;
                    case GuardDirection.East:
                        if (ObstaclePresentInNextCoordinate(_guardX, _guardY, 1, 0, ref timesTouchedObstacle))
                        {
                            _direction = GuardDirection.South;
                            renderDictionary[$"{_guardY}-{_guardX}"] = GetDirectionCharFromEnum(_direction);
                        }
                        else
                        {
                            MoveOneSpace(ref _guardX, ref _guardY, 1, 0);
                        }
                        break;
                    case GuardDirection.West:
                        if (ObstaclePresentInNextCoordinate(_guardX, _guardY, -1, 0, ref timesTouchedObstacle))
                        {
                            _direction = GuardDirection.North;
                            renderDictionary[$"{_guardY}-{_guardX}"] = GetDirectionCharFromEnum(_direction);
                        }
                        else
                        {
                            MoveOneSpace(ref _guardX, ref _guardY, -1, 0);
                        }
                        break;
                }

            }
            if (_stuckInLoop && _escaped)
            {
                _stuckInLoop = false;
            }
            return new PatrolStatistics
            {
                TraversedSteps = renderDictionary.Values.Where(x => x == 'X').ToList().Count,
                WasStuckInLoop = _stuckInLoop
            };
        }

        public void RenderMap()
        {
            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    Console.Write(renderDictionary[$"{i}-{j}"]);
                }
                Console.WriteLine("\n");
            }
        }

        bool ObstaclePresentInNextCoordinate(int x, int y, int xOffset, int yOffset, ref int timesTouchedObstacle)
        {
            x += xOffset;
            y += yOffset;
            var obstacleLocation = $"{y}-{x}";
            if (x >= _mapWidth || x < 0 || y >= _map.Length || y < 0)
            {
                _escaped = true;
                return false;
            }
            if (renderDictionary[obstacleLocation] == '#' || renderDictionary[obstacleLocation] == '0')
            {
                if (!_contactedObstacles.ContainsKey(obstacleLocation))
                {
                    _contactedObstacles.Add(obstacleLocation, 1);
                }
                else if (_contactedObstacles[obstacleLocation] > 0)
                {
                    _contactedObstacles[obstacleLocation] = _contactedObstacles[obstacleLocation] + 1;
                    timesTouchedObstacle = _contactedObstacles[obstacleLocation]; 
                }

                return true;
            }
            return false;
        }

        void MoveOneSpace(ref int x, ref int y, int xOffset, int yOffset)
        {
            if (y + yOffset < 0 || y + yOffset > _map.Length || x + xOffset > _mapWidth || x + xOffset < 0)
            {
                UpdateCoordinate(y, x, xOffset, yOffset);
                x += xOffset;
                y += yOffset;
                return;
            }
            UpdateCoordinate(y, x, xOffset, yOffset);
            x += xOffset;
            y += yOffset;
        }

        void UpdateCoordinate(int y, int x, int xOffset, int yOffset)
        {
            if (_traversalRecord[$"{y}-{x}"] && renderDictionary[$"{y}-{x}"] == 'X')
            {
                return;
            }
            _traversalRecord[$"{y}-{x}"] = true;
            renderDictionary[$"{y}-{x}"] = 'X';
            if (y + yOffset > 0 &&  y + yOffset < _map.Length || x + xOffset < _mapWidth && x + xOffset > 0)
            {
                renderDictionary[$"{y + yOffset}-{x + xOffset}"] = GetDirectionCharFromEnum(_direction);
            }
        }

        char GetDirectionCharFromEnum(GuardDirection direction)
        {
            switch (direction)
            {
                case GuardDirection.East:
                    return '>';
                case GuardDirection.West:
                    return '<';
                case GuardDirection.South:
                    return 'v';
                default:
                    return '^';
            }
        }
    }
}
