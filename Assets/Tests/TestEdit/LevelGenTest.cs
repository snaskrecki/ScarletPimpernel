using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LevelGenTest
    {
        private const int MAX_GRID_VAL = 31;
        private const int DOOR_ROOM = 1 << 6;

        [Test]
        public void LevelGenTestCorrectValuesInGrid()
        {
            const int n = 50;
            const int side = 101;
            int[,] grid = LevelGenerator.GenerateGraph(n, side);
            var start = new Tuple<int, int>(-1, -1);
            int grid_val;

            for (int x = 0; x < side; x++)
            {
                for (int y = 0; y < side; y++)
                {
                    grid_val = Math.Min(grid[x, y], grid[x, y] ^ DOOR_ROOM);
                    Assert.IsTrue(grid_val <= MAX_GRID_VAL);

                    if (start.Item1 == -1 && grid_val > 0)
                    {
                        start = new Tuple<int, int>(x, y);
                    }
                }
            }

            grid_val = Math.Min(grid[start.Item1, start.Item2], grid[start.Item1, start.Item2] ^ DOOR_ROOM);
            Assert.IsTrue(0 < grid_val && grid_val <= MAX_GRID_VAL);
        }

        int[] dx = new int[] { 0, 1, 0, -1 };
        int[] dy = new int[] { 1, 0, -1, 0 };
        const int DIRECTIONS = 4;

        [Test]
        public void LevelGenTestConnectivity()
        {
            const int n = 70;
            const int side = 131;
            int[,] grid = LevelGenerator.GenerateGraph(n, side);
            var start = new Tuple<int, int>(-1, -1);

            for (int x = 0; start.Item1 == -1 && x < side; x++)
            {
                for (int y = 0; y < side; y++)
                {
                    if (start.Item1 == -1 && grid[x, y] > 0)
                    {
                        start = new Tuple<int, int>(x, y);
                        break;
                    }
                }
            }

            bool[,] seen = new bool[side, side];

            Queue<Tuple<int, int>> q = new Queue<Tuple<int, int>>();
            q.Enqueue(start);

            while (q.Count > 0)
            {
                var v = q.Dequeue();
                seen[v.Item1, v.Item2] = true;

                for (int i = 0; i < DIRECTIONS; i++)
                {
                    var u = new Tuple<int, int>(v.Item1 + dx[i], v.Item2 + dy[i]);

                    if (!LevelGenerator.InBounds(u, side) || seen[u.Item1, u.Item2]) continue;

                    //there is a door between neighbouring rooms
                    if (((grid[v.Item1, v.Item2] & (1 << i)) != 0) && ((grid[u.Item1, u.Item2] & (1 << ((i + 2) % 4))) != 0))
                    {
                        q.Enqueue(u);
                    }
                }
            }

            for (int x = 0; x < side; x++)
            {
                for (int y = 0; y < side; y++)
                {
                    if (grid[x, y] == 0)
                    {
                        Assert.IsFalse(seen[x, y]);
                    }
                    else
                    {
                        Assert.IsTrue(seen[x, y]);
                    }
                }
            }
        }

        [Test]
        public void LevelGenTestCorrectRoomConnections()
        {
            const int n = 85;
            const int side = 137;
            int[,] grid = LevelGenerator.GenerateGraph(n, side);

            for (int x = 0; x < side; x++)
            {
                for (int y = 0; y < side; y++)
                {
                    for (int i = 0; i < DIRECTIONS; i++)
                    {
                        var u = new Tuple<int, int>(x + dx[i], y + dy[i]);

                        if (!LevelGenerator.InBounds(u, side)) continue;

                        if ((grid[x, y] & (1 << i)) != 0)
                        {
                            Assert.IsTrue((grid[u.Item1, u.Item2] & (1 << ((i + 2) % 4))) != 0);
                        }
                    }
                }
            }
        }

        [Test]
        public void LevelGenTestExactlyOneDoorPerLevel()
        {
            const int n = 45;
            const int side = 201;
            int[,] grid = LevelGenerator.GenerateGraph(n, side);
            bool foundDoor = false;

            for (int x = 0; x < side; x++)
            {
                for (int y = 0; y < side; y++)
                {
                    if ((grid[x, y] & DOOR_ROOM) != 0)
                    {
                        Assert.IsFalse(foundDoor);
                        foundDoor = true;
                    }
                }
            }

            Assert.IsTrue(foundDoor);
        }

        [Test]
        public void LevelGenTestInBounds()
        {
            Assert.IsTrue(LevelGenerator.InBounds(new Tuple<int, int>(0, 0), 3));
            Assert.IsTrue(LevelGenerator.InBounds(new Tuple<int, int>(7, 7), 8));
            Assert.IsTrue(LevelGenerator.InBounds(new Tuple<int, int>(3, 4), 5));

            Assert.IsFalse(LevelGenerator.InBounds(new Tuple<int, int>(9, 9), 9));
            Assert.IsFalse(LevelGenerator.InBounds(new Tuple<int, int>(42, 43), 43));
            Assert.IsFalse(LevelGenerator.InBounds(new Tuple<int, int>(54, 53), 54));
            Assert.IsFalse(LevelGenerator.InBounds(new Tuple<int, int>(-1, 53), 54));
            Assert.IsFalse(LevelGenerator.InBounds(new Tuple<int, int>(87, -1), 88));
        }
    }
}
