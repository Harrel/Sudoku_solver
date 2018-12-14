using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sudoku_solver.factories
{
    public class SpaceFactory
    {
        /*
         * generates all the spaces based on a loaded file
         */
        public int maxvalue { get; set; }
        private string defaultLocation;

        public SpaceFactory()
        {
            this.defaultLocation = AppDomain.CurrentDomain.BaseDirectory + @"..\..\samples\1.txt";
        }


        /*
         * loads 1.txt found in samples
         * 
         * returns 2d list of spaces
         */
        public List<List<Space>> LoadDefault()
        {
            return GenerateSpaces(defaultLocation, 9);
        }

        /*
         * loads file from given location and generates spaces
         * 
         * if file loading fails, 1.txt is loaded
         * 
         * returns 2d list of spaces
         */ 
        public List<List<Space>> GenerateSpaces(string location, int maxValue)
        {
            List<List<Space>> spaces = new List<List<Space>>();
            this.maxvalue = maxValue;
            string[] file = loadFile(location);
            int y = 0;
            for (int i = 0; i < file.Length; i++)
            {
                //check if line is relevant 
                if (file[i].Length > 0)
                {
                    //split each line into individual spaces
                    List<Space> row = new List<Space>();

                    //remove whitspaces
                    string edit = file[i].Replace(" ", "");
                    for(int x = 0; x < edit.Length; x++)
                    {
                        row.Add(getSpace(edit[x], y, x));
                    }

                    spaces.Add(row);
                    y++;
                }
            }
            return spaces;
        }


        private string[] loadFile(string location)
        {
            string[] file;

            try
            {
                file = System.IO.File.ReadAllLines(location);
            }
            catch(Exception e)
            {
                //load default file
                file = System.IO.File.ReadAllLines(defaultLocation);
            }

            return file;
        }

        private Space getSpace(char value, int y, int x)
        {
            Space s;
            if (y > 8 || x > 8)
            {
                Console.WriteLine("coord too high! x:{0} y:{1}", x, y);
            }
            //convert to int 
            //if value is not a number, -1 is returned indicating empty space
            int number = (int)Char.GetNumericValue(value);
            s = new Space(number, maxvalue);
            s.YCoord = y;
            s.XCoord = x;

            return s;
        }
    }
}
