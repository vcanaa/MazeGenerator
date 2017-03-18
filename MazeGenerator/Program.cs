using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    class Program
    {
        static byte[,] maze;
        static int n = 20;
        static Random rand = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                maze = new byte[n, n];

                GenerateSteps(0, 0, n - 1, n - 1);
                
                Broaden(1);
                Broaden(1);
                for (int i = 2; i < 20; i+=2 )
                {
                    Broaden(i);
                    Broaden(i);
                }

                PrintMaze();

                Console.ReadKey();
            }
        }

        static void GenerateSteps(int x0, int y0, int x1, int y1){
            int xp = x0;
            int yp = y0;
            int step;
            maze[x0, y0] = 1;
            while (xp != x1 || yp != y1) {
                step = rand.Next(Math.Min(x1 - xp, 1), x1 - xp + 1);
                while (step > 0)
                {
                    xp++;
                    maze[xp, yp] = 1;
                    step--;
                }

                step = rand.Next(Math.Min(y1 - yp, 1), y1 - yp + 1);
                while (step > 0)
                {
                    yp++;
                    maze[xp, yp] = 1;
                    step--;
                }
            }
        }

        static void Broaden(int x)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == x)
                    {
                        for(int k = -1; k <2; k++){
                            for(int l = -1; l < 2; l++){
                                if (rand.Next(2) < 1 && (k+l) % 2 == 1)
                                {
                                    maze[Limit(i + k, 0, n - 1), Limit(j + l, 0, n-1)] += 2;
                                }
                            }
                        }
                        
                    }
                }
            }
        }

        static int Limit(int v, int min, int max)
        {
            if (v >= max) return max;
            if (v <= min) return min;
            return v;
        } 
        static void PrintMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j] == 0)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
