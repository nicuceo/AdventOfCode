using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var rawInput = File.ReadAllLines("input.txt");

            var principalCells = rawInput.Select(line => new Cell(line)).ToList();

            var maxX = principalCells.Max(cell => cell.X) + 2;
            var maxY = principalCells.Max(cell => cell.Y) + 2;

            var grid = new Cell[maxX, maxY];

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var currentCell = principalCells.FirstOrDefault(cell => cell.X == x && cell.Y == y);
                    if (currentCell != null)
                    {
                        grid[x, y] = currentCell;
                    }
                }
            }

            var part1 = GetPart1(maxX, maxY, grid, principalCells);
            var part2 = GetPart2(maxX, maxY, grid, principalCells); 

            Console.WriteLine("Part1: " + part1);
            Console.WriteLine("Part2: " + part2);

            Console.ReadKey();
        }

        private static int GetPart2(int maxX, int maxY, Cell[,] grid, List<Cell> principalCells)
        {
            var regionCount = 0;
            var maxDistance = 10000;
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var currentCellDistance = 0;
                    foreach (var principalCell in principalCells)
                    {
                        var currentDistance = GetDistance(principalCell, x, y);
                        currentCellDistance += currentDistance;

                        if (currentCellDistance > maxDistance)
                        {
                            break;
                        }
                    }

                    if (currentCellDistance < maxDistance)
                    {
                        regionCount++;
                    }
                }
            }

            return regionCount;
        }

        private static int GetPart1(int maxX, int maxY, Cell[,] grid, List<Cell> principalCells)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (grid[x, y] != null)
                    {
                        continue;
                    }

                    var minDistance = int.MaxValue;
                    var value = Guid.Empty;
                    Cell parentCell = new Cell();

                    foreach (var principalCell in principalCells)
                    {
                        var currentDistance = GetDistance(principalCell, x, y);

                        if (currentDistance <= minDistance)
                        {
                            if (minDistance == currentDistance)
                            {
                                value = Guid.Empty;
                            }
                            else
                            {
                                value = principalCell.Value;
                                minDistance = currentDistance;
                                parentCell = principalCell;
                            }
                        }
                    }

                    var cell = new Cell
                    {
                        X = x,
                        Y = y,
                        Value = value,
                        IsPrincipal = false,
                        Distance = minDistance
                    };
                    grid[x, y] = cell;

                    if (value != Guid.Empty)
                    {
                        parentCell.Children.Add(cell);
                    }
                }
            }

            var infiniteCellValues = new List<Guid>();

            for (int x = 0; x < maxX; x++)
            {
                var minYValue = grid[x, 0].Value;
                var maxYValue = grid[x, maxY - 1].Value;

                if (!infiniteCellValues.Contains(minYValue) && minYValue != Guid.Empty)
                {
                    infiniteCellValues.Add(minYValue);
                }

                if (!infiniteCellValues.Contains(maxYValue) && maxYValue != Guid.Empty)
                {
                    infiniteCellValues.Add(maxYValue);
                }
            }

            for (int y = 0; y < maxY; y++)
            {
                var minXValue = grid[0, y].Value;
                var maxXValue = grid[maxX - 1, y].Value;

                if (!infiniteCellValues.Contains(minXValue) && minXValue != Guid.Empty)
                {
                    infiniteCellValues.Add(minXValue);
                }

                if (!infiniteCellValues.Contains(maxXValue) && maxXValue != Guid.Empty)
                {
                    infiniteCellValues.Add(maxXValue);
                }
            }

            var maxCount = 0;

            foreach (var principalCell in principalCells)
            {
                if (!infiniteCellValues.Contains(principalCell.Value) && principalCell.Children.Count > maxCount)
                {
                    maxCount = principalCell.Children.Count;
                }
            }

            return maxCount + 1;
        }

        private static int GetDistance(Cell principalCell, int x, int y)
        {
            var xDistance = principalCell.X - x;
            var yDistance = principalCell.Y - y;


            if (xDistance < 0)
            {
                xDistance *= -1;
            }
            if (yDistance < 0)
            {
                yDistance *= -1;
            }

            return xDistance + yDistance;
        }
    }
}
