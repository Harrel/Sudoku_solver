using sudoku_solver.algoritms;
using sudoku_solver.model;
using sudoku_solver.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;

namespace sudoku_solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ISolver solver;
        public Board MyBoard { get; private set; }
        private List<List<TextBlock>> blocks;
        const int maxValue = 9;
        private SpaceFactory fac;
        public int MyProperty { get; set; }
        private Stopwatch timer;

        public MainWindow()
        {
            

            solver = new SimpleSolver();
            fac = new SpaceFactory();
            MyBoard = new Board(fac.LoadDefault());
            //setupBlocks();

            timer = new Stopwatch();

            InitializeComponent();
            setupBlocks();
            RefreshScreen();
        }



        private void setupBlocks()
        {
            this.blocks = new List<List<TextBlock>>();
            string nameBase = "sp";
            for(int y = 0; y < maxValue; y++)
            {
                List<TextBlock> row = new List<TextBlock>();
                for (int x = 0; x < maxValue; x++)
                {
                    TextBlock b = (TextBlock)this.FindName(nameBase + x + y);
                    b.MouseDown += this.toolTipUpdate;
                    row.Add(b);
                }

                this.blocks.Add(row);
            }
            
        }

        void toolTipUpdate(object sender, RoutedEventArgs e)
        {

            var t = sender as TextBlock;

            string num = new string(t.Name.Where(c => char.IsDigit(c)).ToArray());
            int x = (int)Char.GetNumericValue(num[0]);
            int y = (int)Char.GetNumericValue(num[1]);

            this.tooltips.Text = "posibilities: " + String.Join("; ", this.MyBoard.Spaces[y][x].Posibilities); 

        }

        private void buttonClick(object sender, RoutedEventArgs e)
        {
            //start solving 
            timer.Reset();
            timer.Start();

           bool success = solver.Solve(this.MyBoard);
            
            timer.Stop();
            int status;
            if(success)
            {
                status = 1;
            }
            else
            {
                status = 2;
            }
            
            RefreshScreen(status);
            

        }

        public void RefreshScreen()
        {
            RefreshScreen(0);
        }

        public void RefreshScreen(int status)
        {
            //update timer text
            timerBlock.Text = "Time elapsed: " + timer.ElapsedMilliseconds.ToString() + " ms";

            if(status == 1)
            {
                timerBlock.Text += " | Success";
            }
            else if(status == 2)
            {
                timerBlock.Text += " | Failure";
            }

            //update grid
            for (int y = 0; y < maxValue; y++)
            {
                
                for (int x = 0; x < maxValue; x++)
                {
                    this.blocks[y][x].Text = this.MyBoard.Spaces[y][x].ValueString;
                }

                
            }

        }

        public void StartSolving()
        {

        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            //open file dialogue
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";

            if(dialog.ShowDialog() == true)
            {
                this.MyBoard = new Board(fac.GenerateSpaces(dialog.FileName, 9));
                RefreshScreen();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            // close program
            this.Close();
        }
    }
}
