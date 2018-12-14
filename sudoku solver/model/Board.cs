using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.model
{
    public class Board
    {
        public List<List<Space>> Spaces {get; private set;}

        public int MaxValue { get; private set; }
        public bool Solved { get; set; }
        public List<Space> FlatListSpaces
        {
            get { return this.getFlatListSpaces(); }
        }


        private List<Space> _flatListSpaces;

        public Board(List<List<Space>> spaces)
        {
            this.Spaces = spaces;
            this.MaxValue = spaces[0][0].maxValue;
        }

        private List<Space> getFlatListSpaces()
        {
            if(_flatListSpaces == null)
            {
                _flatListSpaces = new List<Space>();

                foreach(List<Space> l in this.Spaces)
                {
                    _flatListSpaces.AddRange(l);
                }
            }

            return _flatListSpaces;
        }


    }
}
