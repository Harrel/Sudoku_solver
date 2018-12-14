using sudoku_solver.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.algoritms
{

    /*
     * Interface used for sudoku solvers
     * 
     * Solve method is called in order to solve a puzzle
     * 
     * returns true if puzzle was succesfully solved
     */
    public interface ISolver
    {
        bool Solve(Board b);

    }
}
