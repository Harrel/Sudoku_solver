using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudoku_solver.model;

namespace sudoku_solver.algoritms.tactics
{
    class SinglePossibilityTactic : ITactic
    {

        public SinglePossibilityTactic()
        {

        }

        public bool Execute(List<Space> completed, Board b)
        {
            //this method counts all the possible numbers in each row, column and square
            //should a specific number only have one possibilty, it can be filled into it's respective space
            bool done = false;

            //Dictionary<int, List<string>> rowOc = ;
            //Dictionary<int, List<string>> columnOc = new Dictionary<int, List<string>>();
            //Dictionary<int, List<string>> squareOc = new Dictionary<int, List<string>>();
            List<Dictionary<int, List<string>>> occurenceDictionaries = new List<Dictionary<int, List<string>>>();

            occurenceDictionaries.Add(new Dictionary<int, List<string>>());
            occurenceDictionaries.Add(new Dictionary<int, List<string>>());
            occurenceDictionaries.Add(new Dictionary<int, List<string>>());

            //initialise occurence dictionaries
            for (int i = 1; i <= b.MaxValue; i++)
            {
                //rows
                occurenceDictionaries[0].Add(i, null);
                //columns
                occurenceDictionaries[1].Add(i, null);
                //squares
                occurenceDictionaries[2].Add(i, null);

            }

            List<int> keys = occurenceDictionaries[0].Keys.ToList();
            for (int i = 0; i < b.Spaces.Count && !done; i++)
            {

                string log = "";
                //reset dictionaries
                foreach (int key in keys)
                {

                    //rows
                    occurenceDictionaries[0][key] = new List<string>();
                    log += key + ":" + occurenceDictionaries[0][key].Count + " ";
                    //columns
                    occurenceDictionaries[1][key] = new List<string>();
                    //squares
                    occurenceDictionaries[2][key] = new List<string>();
                }

                Console.WriteLine(log);
                Console.WriteLine("");


                string squareKey = CommonActions.squares.Keys.ToList()[i];
                for (int e = 0; e < b.Spaces[i].Count; e++)
                {
                    //scan the row
                    foreach (int posi in b.Spaces[i][e].Posibilities)
                    {

                        occurenceDictionaries[0][posi].Add("" + e + i);
                    }


                    //scan the column
                    foreach (int posi in b.Spaces[e][i].Posibilities)
                    {
                        occurenceDictionaries[1][posi].Add("" + i + e);
                    }

                    ////scan the square
                    string[] coords = CommonActions.squares[squareKey][e].Split(',');
                    int sX = Int32.Parse(coords[0]);
                    int sY = Int32.Parse(coords[1]);

                    foreach (int posi in b.Spaces[sY][sX].Posibilities)
                    {
                        occurenceDictionaries[2][posi].Add("" + sX + sY);
                    }

                }

                //find unique occurences
                //rows
                foreach (int key in keys)
                {
                    //check row column and square
                    //done = this.findUniqueOccurences(completed, b, occurenceDictionaries, key);


                    for (int index = 0; index < occurenceDictionaries.Count; index++)
                    {
                        if (occurenceDictionaries[index][key].Count == 1)
                        {
                            //unique occurence found


                            //place value in space
                            int x = (int)Char.GetNumericValue(occurenceDictionaries[index][key][0][0]);
                            int y = (int)Char.GetNumericValue(occurenceDictionaries[index][key][0][1]);

                            Space current = b.Spaces[y][x];

                            if (!completed.Contains(current))
                            {
                                done = true;

                                current.Value = key;
                                current.Posibilities.Clear();
                                //current.Posibilities.Add(key);
                                completed.Add(current);
                                //open.Remove(current);
                                CommonActions.RemoveSpecificPosibility(current.YCoord, current.XCoord, key, b);

                            }
                        }
                    }
                }
            }

            return done;
        }
    }
}
