using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaBattleGame;
using static SeaBattleGame.MainWindow;

namespace SeaBattleGame.Utils
{
    class Ground
    {

        public Cell[][] GetGroundByMap(int[][] Map)
        {
            Cell[][] Ground = new Cell[10][];
            for (int i = 1; i < Map.Length - 1; i++)
            {
                Ground[i - 1] = new Cell[10];
                for (int j = 1; j < Map[i].Length - 1; j++)
                {
                    string state = "";
                    string colour = "White";
                    if (Map[i][j] == 1)
                    {
                        state = "";
                        colour = "Blue";
                    }
                    Cell cell = new Cell(i, j, state, colour, "Gray");
                    Ground[i - 1][j - 1] = cell;
                }
            }
            return Ground;
        }

        public Cell[][] GetClosedGround()
        {
            Cell[][] Ground = new Cell[10][];
            for (int i = 0; i < 10; i++)
            {
                Ground[i] = new Cell[10];
                for (int j = 0; j < 10; j++)
                {
                    Cell cell = new Cell(i, j, "", "White", "Gray");
                    Ground[i][j] = cell;
                }
            }
            return Ground;
        }

    }
}
