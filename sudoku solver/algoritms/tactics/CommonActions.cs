using sudoku_solver.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.algoritms.tactics
{


    /*
     * Static class that contains various common resources
     */
    class CommonActions
    {

        /* 
         * Dictionary containing the coordinates of each square found on the board
         * uses the coordinates of the top left space of each square as the key
         */
        public readonly static Dictionary<string, List<string>> squares = new Dictionary<string, List<string>>
        {
            { "0,0", new List<string> { "0,0", "1,0", "2,0", "0,1", "1,1", "2,1", "0,2", "1,2", "2,2" } },
            { "3,0", new List<string> { "3,0", "4,0", "5,0", "3,1", "4,1", "5,1", "3,2", "4,2", "5,2" } },
            { "6,0", new List<string> { "6,0", "7,0", "8,0", "6,1", "7,1", "8,1", "6,2", "7,2", "8,2" } },

            { "0,3", new List<string> { "0,3", "1,3", "2,3", "0,4", "1,4", "2,4", "0,5", "1,5", "2,5" } },
            { "3,3", new List<string> { "3,3", "4,3", "5,3", "3,4", "4,4", "5,4", "3,5", "4,5", "5,5" } },
            { "6,3", new List<string> { "6,3", "7,3", "8,3", "6,4", "7,4", "8,4", "6,5", "7,5", "8,5" } },

            { "0,6", new List<string> { "0,6", "1,6", "2,6", "0,7", "1,7", "2,7", "0,8", "1,8", "2,8" } },
            { "3,6", new List<string> { "3,6", "4,6", "5,6", "3,7", "4,7", "5,7", "3,8", "4,8", "5,8" } },
            { "6,6", new List<string> { "6,6", "7,6", "8,6", "6,7", "7,7", "8,7", "6,8", "7,8", "8,8" } },
        };


        /*
         * searches the board for spaces with values and updates the posibilities of surounding spaces
         */
        static public void UpdatePosibilities(Board b)
        {
            for (int y = 0; y < b.Spaces.Count; y++)
            {
                for (int x = 0; x < b.Spaces[y].Count; x++)
                {
                    if (b.Spaces[y][x].Value > 0)
                    {
                        //remove posibility from all spaces in the same row, column and square
                        RemoveSpecificPosibility(y, x, b.Spaces[y][x].Value, b);
                    }
                }
            }
        }


        /*
         * Removes the given posibility from spaces around a specific space
         */
        static public void RemoveSpecificPosibility(int y, int x, int value, Board b)
        {
            //collect coords of square
            //
            if (y > 8 || x > 8)
            {
                Console.WriteLine("coord too high! x:{0} y:{1}", x, y);
            }
            //determine square by calculating the coordinates of the top left space
            int sX = 3 * (x / 3);
            int sY = 3 * (y / 3);
            
            //retrieve coordinates contained in the square
            List<string> square = squares[sX + "," + sY];

            for (int i = 0; i < b.Spaces.Count; i++)
            {
                //iterate column
                //check if its not current space
                if (i != y)
                {
                    b.Spaces[i][x].RemovePosibility(value);
                }
                //iterate row
                //check if its not current space
                if (i != x)
                {
                    b.Spaces[y][i].RemovePosibility(value);
                }
                //iterate square

                //get coordinates
                string[] coords = square[i].Split(',');
                int squareX = Int32.Parse(coords[0]);
                int squareY = Int32.Parse(coords[1]);

                //check if coords don't overlap with space handled in row or column
                if (y != squareY || x != squareX)
                {
                    b.Spaces[squareY][squareX].RemovePosibility(value);
                }
            }
        }


    }
}
