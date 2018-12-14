using sudoku_solver.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.algoritms
{
    public interface ISolver
    {
        bool Solve(Board b);

    }
}
