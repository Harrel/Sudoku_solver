using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver
{
    public class Space
    {
        public int Value
        {
            get
            { return this._value; }
            set
            { this.setValue(value); }
        }
        public string ValueString { get { return getValueString(); } }
        public int initialValue { get; private set; }
        public int maxValue { get; private set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public bool IsDone { get; set; }

        private int _value;

        public List<int> Posibilities { get; private set; }

        public Space(int value, int maxValue)
        {
            Posibilities = new List<int>(maxValue);
            this.maxValue = maxValue;
            this.initialValue = value;
            this.Value = value;

            this.setPosibilities();
            this.IsDone = false;
        }

        private void setValue(int v)
        {
            this._value = v;
            IsDone = true;
        }
        private void setPosibilities()
        {
            if (Value < 0)
            {
                for (int i = 1; i <= maxValue; i++)
                {
                    if (i != Value)
                    {
                        Posibilities.Add(i);
                    }

                }
            }
            
        }

        public void reset()
        {
            if(initialValue < 1)
            {
                Value = initialValue;
            }
            
        }

        private string getValueString()
        {
            if(Value > -1)
            {
                return Value.ToString();
            }
            else
            {
                return "-";
            }
        }

        public bool RemovePosibility(int pos)
        {
            //remove a possible number
            int size = Posibilities.Count;
            Posibilities.RemoveAll(item => item == pos);

            return size > Posibilities.Count;
        }
    }
}
