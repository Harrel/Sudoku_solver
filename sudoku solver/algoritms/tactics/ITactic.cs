using sudoku_solver.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.algoritms.tactics
{

    /*
     * ITactic is used by ISolvers in order to solve a puzzle
     * 
     * Execute method carries out its tactic in solving puzzles
     * 
     * returns true if it succesfully was able to change the puzzle
     */

    interface ITactic
    {

        bool Execute(List<Space> completed, Board b);
    }
}
