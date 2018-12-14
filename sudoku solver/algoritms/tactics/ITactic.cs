using sudoku_solver.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.algoritms.tactics
{
    interface ITactic
    {

        bool Execute(List<Space> completed, Board b);
    }
}
