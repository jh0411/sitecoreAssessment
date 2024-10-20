using System;
using System.Collections.Generic;

class Program
{
    // 8 possible directions (up, down, left, right, and diagonals)
    private static readonly int[] dRow = { -1, -1, -1, 0, 1, 1, 1, 0 };
    private static readonly int[] dCol = { -1, 0, 1, 1, 1, 0, -1, -1 };
    private static readonly string[] directionNames =
        { "Up-Left", "Up", "Up-Right", "Right", "Down-Right", "Down", "Down-Left", "Left" };



    // Recursive method to find the safe path through the minefield
    private static bool findPath(int[,] minefield, int row, int col,
                                int snifferRow, int snifferCol,
                                int allyRow, int allyCol,
                                bool[,] visited, List<(int, int)> path)
    {
        //Check if a position (row, col) is within bounds
        bool isInBounds(int x, int y)
        {
            return x >= 0 && x < row && y >= 0 && y < col;
        }

        // Base case: Sniffer Pup reaches the destination
        if (snifferRow == row - 1 && snifferCol == col - 1)
        {
            Console.WriteLine($"Ally position: ({snifferRow}, {snifferCol}).");
            Console.WriteLine("Sniffer pup and Ally reached the end");
            Console.WriteLine();
            return true;
        }

        // Explore 8 possible directions
        for (int i = 0; i < 8; i++)
        {
            int newRow = snifferRow + dRow[i];
            int newCol = snifferCol + dCol[i];

            // Check if the new position is within bounds, not visited, and safe
            if (isInBounds(newRow, newCol) && !visited[newRow, newCol] && minefield[newRow, newCol] != 1)
            {

                // Mark the position as visited
                visited[newRow, newCol] = true;

                // Move Ally to Sniffer Pup's previous position
                allyRow = snifferRow;
                allyCol = snifferCol;

                // Ensure Ally does not overlap with Sniffer Pup
                if (allyRow == newRow && allyCol == newCol)
                {
                    visited[newRow, newCol] = false;  // Reset visited status
                    continue;
                }

                // Log movements
                Console.WriteLine($"Direction: {directionNames[i]}");
                Console.WriteLine($"Sniffer Pup position: ({newRow}, {newCol}).");
                Console.WriteLine($"Ally position: ({allyRow}, {allyCol}).");
                Console.WriteLine();

                // Add current Sniffer position to the path
                path.Add((newRow, newCol));

                // Recursive exploration
                if (findPath(minefield, row, col, newRow, newCol, allyRow, allyCol, visited, path))
                {
                    return true;
                }

                // Backtrack if no valid path found
                path.RemoveAt(path.Count - 1);
                visited[newRow, newCol] = false;

                Console.WriteLine($"Backtracking from ({newRow}, {newCol}).");
                Console.WriteLine();
            }
        }

        return false;  // No valid path found from this position
    }

    public static void Main()
    {
        // Minefield setup (0: safe, 1: bomb)
        int n = 6, m = 6;
        int[,] minefield =
        {
                {0, 0, 0, 0, 0, 0 },
                {1, 0, 1, 1, 1, 1 },
                {0, 1, 0, 1, 1, 1 },
                {0, 1, 1, 0, 0, 1 },
                {0, 1, 0, 0, 0, 1 },
                {1, 1, 1, 1, 0, 0 }
        };

        // Track visited cells and path
        bool[,] visited = new bool[n, m];
        List<(int, int)> path = new List<(int, int)>();

        // Start from the top-left corner
        int snifferRow = 0, snifferCol = 0;
        int allyRow = 0, allyCol = 0;
        visited[0, 0] = true;

        //Draw out the minefield
        for (int i = 0; i < minefield.GetLength(0); ++i)
        {
            for (int j = 0; j < minefield.GetLength(1); ++j)
                Console.Write(minefield[i, j] + " ");
            Console.Write("\n");
        }

        Console.WriteLine();

        bool result = findPath(minefield, n, m, snifferRow, snifferCol, allyRow, allyCol, visited, path);

        if (result)
        {
            Console.WriteLine("Safe path found:");
            foreach (var pos in path)
            {
                Console.WriteLine($"({pos.Item1}, {pos.Item2})");
            }
        }
        else
        {
            Console.WriteLine("No safe path found.");
        }
    }
}
