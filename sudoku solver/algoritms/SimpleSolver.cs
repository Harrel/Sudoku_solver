using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudoku_solver.algoritms.tactics;
using sudoku_solver.model;

namespace sudoku_solver.algoritms
{
    public class SimpleSolver : ISolver
    {

        private List<ITactic> tactics;


        public SimpleSolver()
        {
            this.tactics = new List<ITactic>();


            //add all tactics used by this solver
            //sequence will determine which tactic is first etc.
            this.tactics.Add(new SingleEliminationTactic());
            this.tactics.Add(new SinglePossibilityTactic());

        }

        public bool Solve(Board b)
        {
            if(b.Solved)
            {
                return true;
            }
            printBoard(b);
            int failures = 0;
            List<Space> sortedByPosi = b.FlatListSpaces;
            List<Space> completed = sortedByPosi.FindAll(e => e.Posibilities.Count == 0);
            
            //set posibilities
            CommonActions.UpdatePosibilities(b);
            while (sortedByPosi.Count  != completed.Count)
            {

                bool changed = false;

                for(int i = 0; i < this.tactics.Count && !changed; i++)
                {
                    changed = tactics[i].Execute(completed, b);
                }

                if(!changed)
                {
                    //could not solve
                    break;
                }

            }


            printBoard(b);
            printCompleted(completed);
            Console.WriteLine("Total failures: {0}", failures);
            b.Solved = sortedByPosi.Count == completed.Count;
            return b.Solved;
        }

        private void printCompleted(List<Space> l)
        {
            int[,] board = new int[9,9];

            foreach(Space s in l)
            {
                board[s.YCoord, s.XCoord] = s.Value;
            }

            Console.WriteLine("================");
            for (int y = 0; y < 9; y++)
            {
                if (y == 3 || y == 6)
                {
                    Console.WriteLine("");
                }

                string line = "";
                for (int x = 0; x < 9; x++)
                {
                    if (x == 3 || x == 6)
                    {
                        line += " ";
                    }
                    line += board[y,x];
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("================");

        }


        private void printBoard(Board b)
        {
            Console.WriteLine("================");
            for(int y = 0; y < b.Spaces.Count; y++)
            {
                if (y == 3 || y == 6)
                {
                    Console.WriteLine("");
                }

                string line = "";
                for(int x = 0; x < b.Spaces[y].Count; x++)
                {
                    if(x == 3 || x == 6)
                    {
                        line += " ";
                    }
                    line += b.Spaces[y][x].Value;
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("================");
        }

        //private bool checkPosibilityPairs(Board b, List<Space> open, List<Space> closed)
        //{
        //    bool success = false;

        //    //go through all squares and check if a possibility occurs twice
        //    //if there a possibility that occurs twice check if the corrisponding space share a column or row
        //    Dictionary<int, List<string>> occurences;

        //    for (int keyIter = 0; keyIter < squares.Keys.ToList().Count && !success; keyIter++)
        //    //foreach (string key in squares.Keys.ToList())
        //    {
        //        string key = squares.Keys.ToList()[keyIter];
        //        occurences = new Dictionary<int, List<string>>();
        //        for (int i = 1; i <= b.MaxValue; i++)
        //        {
        //            occurences.Add(i, new List<string>());
        //        }

        //        //collect posibilities
        //        foreach(string coord in squares[key])
        //        {
        //            string[] split = coord.Split(',');
        //            int x = Int32.Parse(split[0]);
        //            int y = Int32.Parse(split[1]);

        //            foreach(int p in b.spaces[y][x].Posibilities)
        //            {
        //                occurences[p].Add("" + x + y);
        //            }

        //            //search for possibility that occurs twice
        //            KeyValuePair<int,List<string>> found = occurences.Where(o => o.Value.Count == 2).FirstOrDefault();

        //            if(!found.Equals(default(KeyValuePair<int, List<string>>)))
        //            {
        //                //check if occurences share a row or column
        //                int x1 = (int)Char.GetNumericValue(occurences[found.Key][0][0]);
        //                int y1 = (int)Char.GetNumericValue(occurences[found.Key][0][1]);
        //                int x2 = (int)Char.GetNumericValue(occurences[found.Key][1][0]);
        //                int y2 = (int)Char.GetNumericValue(occurences[found.Key][1][1]);
        //                bool shrdCol = x1 == x2;
        //                bool shrdRow = y1 == y2;
        //                if (shrdRow || shrdCol)
        //                {
        //                    success = true;
        //                    //remove the possibility from the shared row or column
        //                    if(shrdRow)
        //                    {
        //                        // shared row
        //                        // iterate row removing posibility
        //                        for(int i = 0; i < b.MaxValue; i++)
        //                        {
        //                            b.spaces[y1][i].RemovePosibility(found.Key);
        //                        }
        //                    }
        //                    else if(shrdCol)
        //                    {
        //                        // shared column
        //                        // iterate row removing posibility
        //                        for (int i = 0; i < b.MaxValue; i++)
        //                        {
        //                            b.spaces[i][x1].RemovePosibility(found.Key);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    return success;
        //}

        //private bool checkNakedPairs(Board b, List<Space> open, List<Space> closed)
        //{
        //    bool done = false;

        //    //check each row square and column for naked pairs
        //    //a naked pair is when two spaces have the same possibilities
            
        //    for(int i = 0; i < b.MaxValue && done; i++)
        //    {
        //        //row
        //        //the key consists of the posibilities in a space
        //        //the connected value is a list containing the coords of the space(s) that have the same possibilities
        //        Dictionary<string, List<string>> rowPairs = new Dictionary<string, List<string>>();
        //        for (int e = 0; e < b.MaxValue; e++)
        //        {
        //            var current = b.spaces[i][e];
        //            string posi = string.Join("", current.Posibilities.ToArray());
        //            string coords = "" + current.XCoord + current.YCoord;
        //            if (!rowPairs.ContainsKey(posi))
        //            {
        //                rowPairs[posi].Add(coords);
        //            }
        //            else
        //            {
        //                List<string> value = new List<string>();
        //                value.Add(coords);
        //                rowPairs.Add(posi, value);
        //            }

        //        }
        //        //check if a pair was found

        //        KeyValuePair<string,List<string>> pair = rowPairs.Where(p => p.Key.Length == 2 && p.Value.Count == 2).FirstOrDefault();
        //        if(!pair.Equals(default(KeyValuePair<string, List<string>>)))
        //        {
        //            //pair found
        //            done = true;

        //            int xSkip1 = (int)Char.GetNumericValue(pair.Value[0][0]);
        //            int xSkip2 = (int)Char.GetNumericValue(pair.Value[1][0]);
        //            int posi1 = (int)Char.GetNumericValue(pair.Key[0]);
        //            int posi2 = (int)Char.GetNumericValue(pair.Key[1]);

        //            //update row posibilities

        //            for(int x=0; x < b.MaxValue; x++)
        //            {
        //                if(x != xSkip1 && x != xSkip2)
        //                {
        //                    b.spaces[i][x].RemovePosibility(posi1);
        //                    b.spaces[i][x].RemovePosibility(posi2);
        //                }
        //            }
        //        }
        //    }

        //    return done;
        //}
    }
}
