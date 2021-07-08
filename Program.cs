﻿using System;

namespace PathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new char[,]
            {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', '#', '#', ' ', ' ', ' ', '#', '#', ' ', ' ', '#', ' ', ' ', '#', '#'},
                {'#', ' ', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#'},
                {'S', ' ', '#', ' ', '#', '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', ' ', '#', ' ', '#', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', '#', ' ', '#', '#', ' ', '#', ' ', '#', '#', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', 'E'},
                {'#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#', ' ', '#', '#', ' ', '#'},
                {'#', '#', '#', ' ', ' ', '#', ' ', '#', ' ', '#', ' ', '#', '#', ' ', '#'},
                {'#', ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', '#'},
                {'#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', '#', '#', '#', ' ', '#'},
                {'#', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#'},
                {'#', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
            };

            var solverFactory = new SolverFactory(Finder.BreadthFirstSearch, maze);
            var solver = solverFactory.GetSolver();            

            Output.PrintMaze(solver.ToSolve());
        }        
    }
}
