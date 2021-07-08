using System;

namespace PathFinder
{
    public static class Output
    {
        public static void PrintMaze(char[,] maze)
        {
            Console.Write(new string('-', maze.GetLength(0) * 2 + 2));

            for (int i = 0; i < maze.GetLength(0); i++)
            {                
                Console.WriteLine();
                Console.Write("|");

                for (int j = 0; j < maze.GetLength(1); j++)
                {                    
                    Console.Write($" {maze[i, j]}");
                }
                Console.Write("|");
            }

            Console.WriteLine();
            Console.Write(new string('-', maze.GetLength(0) * 2 + 2));  
            Console.WriteLine();          
        }

        public static void PrintMaze(object[,] maze)
        {
            Console.Write(new string('-', maze.GetLength(0) * 2 + 2));

            for (int i = 0; i < maze.GetLength(0); i++)
            {                
                Console.WriteLine();
                Console.Write("|");

                for (int j = 0; j < maze.GetLength(1); j++)
                {                    
                    Console.Write($" {maze[i, j]}");
                }
                Console.Write("|");
            }

            Console.WriteLine();
            Console.Write(new string('-', maze.GetLength(0) * 2 + 2));  
            Console.WriteLine();          
        }
    }
}