using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudoku_solver.model;

namespace sudoku_solver.algoritms.tactics
{
    class SingleEliminationTactic : ITactic
    {

        
        /*
         * Basic tactic scans board for spaces with only one posibility
         * If found, it is filled in and returns true
         */
        public SingleEliminationTactic()
        {

        }

        public bool Execute(List<Space> completed, Board b)
        {
            bool success = false;

            //assemble list of all spaces
            //get a space with only one posibility
            Space current = b.FlatListSpaces.Find(e => e.Posibilities.Count == 1);
            
            //check if anything was found
            success = current != null;
            if(success)
            {
                //set value
                current.Value = current.Posibilities[0];
                current.Posibilities.Clear();

                //move space to completed spaces list
                completed.Add(current);
                CommonActions.RemoveSpecificPosibility(current.YCoord, current.XCoord, current.Value, b);
            }


            return success;
        }
    }
}
