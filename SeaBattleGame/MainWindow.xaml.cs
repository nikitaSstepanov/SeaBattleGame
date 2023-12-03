using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SeaBattleGame
{

    public partial class MainWindow : Window
    {

        public SeaGround Player1Map;
        public SeaGround Player2Map;
        private int[][] Map1;
        private int[][] Map2;
        private string Mode;
        private string Step;
        private int Player1BoatsCnt;
        private int Player2BoatsCnt;
        private int[][] Points = GetPoints();
        private int[][][] Boats1;
        private int[][][] Boats2;
        private int Points1Cnt;
        private int Points2Cnt;
        Random randomizer = new Random();

        public MainWindow()
        {
            InitializeComponent();
            StartPanel.Visibility = Visibility.Visible;
        }

        private void Init()
        {
            ChooseModePanel.Visibility = Visibility.Visible;
            Player1Map = new SeaGround(new Cell[10][], "Player");
            Player2Map = new SeaGround(new Cell[10][], "");
            Player1BoatsCnt = 10;
            Player2BoatsCnt = 10;
            Step = "Игрок №1";
            Points1Cnt = 20;
            Points2Cnt = 20;
        }

        private void StartGame()
        {
            Player1Ground.ItemsSource = Player1Map.Map;
            Player2Ground.ItemsSource = Player2Map.Map;
            Player1Cnt.Text = (Player1BoatsCnt).ToString();
            Player2Cnt.Text = (Player2BoatsCnt).ToString();
            PlayerStep.Text = Step;
            Player1GroundGrid.Visibility = Visibility.Visible;
            Player2GroundGrid.Visibility = Visibility.Visible;
            InformationPanel.Visibility = Visibility.Visible;
        }

        private void PrepareGameBot()
        {
            PcChooseCreationModePanel.Visibility = Visibility.Visible;
            Map2 = GenerateMap();
            Player2Map.Map = GetClosedGround();
            Player2Map.UserType = "Bot";
        }

        private void BotStep()
        {
            while (true)
            {
                int PointIndex = randomizer.Next(0, 99);
                int[] point = Points[PointIndex];
                if (Map1[point[0] + 1][point[1] + 1] == 1)
                {
                    if (Player1Map.Map[point[0]][point[1]].Content != "X")
                    {
                        Player1Map.Map[point[0]][point[1]].Content = "X";
                        Points1Cnt -= 1;
                        if (Points1Cnt == 0)
                        {
                            FinishGame("Бот");
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (Player1Map.Map[point[0]][point[1]].Content != ".")
                {
                    Player1Map.Map[point[0]][point[1]].Content = ".";
                    Player1Map.Map[point[0]][point[1]].Colour = "LightGreen";
                    Step = "Игрок №1";
                    PlayerStep.Text = Step;
                    break;
                }
                else 
                {
                    continue;
                }
            }
        }

        private void FinishGame(string Winner)
        {
            Step = "Finish";
            Player1GroundGrid.Visibility = Visibility.Collapsed;
            Player2GroundGrid.Visibility = Visibility.Collapsed;
            InformationPanel.Visibility = Visibility.Collapsed;
            WinnerText.Text = $"Победил {Winner}";
            WinnerPanel.Visibility = Visibility.Visible;
        }

        private int[][] GenerateMap()
        {
            int[][] Map = GetEmptyMap();
            int MaxLengthBoat = 4;
            int[][] PointsClone = new int[100][];
            Array.Copy(Points, PointsClone, Points.Length);
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int maxL = MaxLengthBoat;
                    while (true)
                    {
                        if (maxL == 0)
                        {
                            break;
                        }
                        int PointIndex = randomizer.Next(0, PointsClone.Length);
                        int[] point = PointsClone[PointIndex];
                        if (Map[point[0]][point[1]] == 0)
                        {
                            Map[point[0]][point[1]] = 1;
                            maxL -= 1;
                        }
                        else
                        {
                            continue;
                        }
                        if (MaxLengthBoat == 2)
                        {
                            if (Map[point[0] - 1][point[1]] == 0)
                            {
                                Map[point[0] - 1][point[1]] = 1;
                                maxL -= 1;
                            }
                            else if (Map[point[0]][point[1] - 1] == 0)
                            {
                                Map[point[0]][point[1] - 1] = 1;
                                maxL -= 1;
                            }
                            else if (Map[point[0] + 1][point[1]] == 0)
                            {
                                Map[point[0] + 1][point[1]] = 1;
                                maxL -= 1;
                            }
                            else if (Map[point[0]][point[1] + 1] == 0)
                            {
                                Map[point[0]][point[1] + 1] = 1;
                                maxL -= 1;
                            }
                            else
                            {
                                Map[point[0]][point[1]] = 0;
                                maxL = MaxLengthBoat;
                                continue;
                            }
                        }
                        else if (MaxLengthBoat == 3)
                        {
                            if (Map[point[0] - 1][point[1]] == 0)
                            {
                                Map[point[0] - 1][point[1]] = 1;
                                maxL -= 1;
                                if (Map[point[0] - 2][point[1]] == 0)
                                {
                                    Map[point[0] - 2][point[1]] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else if (Map[point[0] + 1][point[1]] == 0)
                                {
                                    Map[point[0] + 1][point[1]] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else
                                {
                                    Map[point[0] - 1][point[1]] = 0;
                                    maxL += 1;
                                }
                            }
                            if (Map[point[0]][point[1] - 1] == 0)
                            {
                                Map[point[0]][point[1] - 1] = 1;
                                maxL -= 1;
                                if (Map[point[0]][point[1] - 2] == 0)
                                {
                                    Map[point[0]][point[1] - 2] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else if (Map[point[0]][point[1] + 1] == 0)
                                {
                                    Map[point[0]][point[1] + 1] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else
                                {
                                    Map[point[0]][point[1] - 1] = 0;
                                    maxL += 1;
                                }
                            }
                            if (Map[point[0] + 1][point[1]] == 0)
                            {
                                Map[point[0] + 1][point[1]] = 1;
                                maxL -= 1;
                                if (Map[point[0] + 2][point[1]] == 0)
                                {
                                    Map[point[0] + 2][point[1]] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else if (Map[point[0] - 1][point[1]] == 0)
                                {
                                    Map[point[0] - 1][point[1]] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else
                                {
                                    Map[point[0] + 1][point[1]] = 0;
                                    maxL += 1;
                                }
                            }
                            if (Map[point[0]][point[1] + 1] == 0)
                            {
                                Map[point[0]][point[1] + 1] = 1;
                                maxL -= 1;
                                if (Map[point[0]][point[1] + 2] == 0)
                                {
                                    Map[point[0]][point[1] + 2] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else if (Map[point[0]][point[1] - 1] == 0)
                                {
                                    Map[point[0]][point[1] - 1] = 1;
                                    maxL -= 1;
                                    continue;
                                }
                                else
                                {
                                    Map[point[0]][point[1] + 1] = 0;
                                    maxL += 1;
                                }
                            }
                            else
                            {
                                Map[point[0]][point[1]] = 0;
                                maxL = MaxLengthBoat;
                                continue;
                            }
                        }
                        else if (MaxLengthBoat == 4)
                        {
                            if (Map[point[0] - 1][point[1]] == 0)
                            {
                                Map[point[0] - 1][point[1]] = 1;
                                maxL -= 1;
                                if (Map[point[0] - 2][point[1]] == 0)
                                {
                                    Map[point[0] - 2][point[1]] = 1;
                                    maxL -= 1;
                                    if (Map[point[0] - 3][point[1]] == 0)
                                    {
                                        Map[point[0] - 3][point[1]] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else if (Map[point[0] + 1][point[1]] == 0)
                                    {
                                        Map[point[0] + 1][point[1]] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else
                                    {
                                        Map[point[0] - 1][point[1]] = 0;
                                        Map[point[0] - 2][point[1]] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            if (Map[point[0]][point[1] - 1] == 0)
                            {
                                Map[point[0]][point[1] - 1] = 1;
                                maxL -= 1;
                                if (Map[point[0]][point[1] - 2] == 0)
                                {
                                    Map[point[0]][point[1] - 2] = 1;
                                    maxL -= 1;
                                    if (Map[point[0]][point[1] - 3] == 0)
                                    {
                                        Map[point[0]][point[1] - 3] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else if (Map[point[0]][point[1] + 1] == 0)
                                    {
                                        Map[point[0]][point[1] + 1] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else
                                    {
                                        Map[point[0]][point[1] - 1] = 0;
                                        Map[point[0]][point[1] - 2] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            if (Map[point[0] + 1][point[1]] == 0)
                            {
                                Map[point[0] + 1][point[1]] = 1;
                                maxL -= 1;
                                if (Map[point[0] + 2][point[1]] == 0)
                                {
                                    Map[point[0] + 2][point[1]] = 1;
                                    maxL -= 1;
                                    if (Map[point[0] + 3][point[1]] == 0)
                                    {
                                        Map[point[0] + 3][point[1]] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else if (Map[point[0] - 1][point[1]] == 0)
                                    {
                                        Map[point[0] - 1][point[1]] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else
                                    {
                                        Map[point[0] + 1][point[1]] = 0;
                                        Map[point[0] + 2][point[1]] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            if (Map[point[0]][point[1] + 1] == 0)
                            {
                                Map[point[0]][point[1] + 1] = 1;
                                maxL -= 1;
                                if (Map[point[0]][point[1] + 2] == 0)
                                {
                                    Map[point[0]][point[1] + 2] = 1;
                                    maxL -= 1;
                                    if (Map[point[0]][point[1] + 3] == 0)
                                    {
                                        Map[point[0]][point[1] + 3] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else if (Map[point[0]][point[1] - 1] == 0)
                                    {
                                        Map[point[0]][point[1] - 1] = 1;
                                        maxL -= 1;
                                        continue;
                                    }
                                    else
                                    {
                                        Map[point[0]][point[1] + 1] = 0;
                                        Map[point[0]][point[1] + 2] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            else
                            {
                                Map[point[0]][point[1]] = 0;
                                maxL = MaxLengthBoat;
                                continue;
                            }
                        }
                    }
                }
                MaxLengthBoat -= 1;
            }
            return Map;
        } 

        private Cell[][] GetClosedGround()
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

        private Cell[][] GetGroundByMap(int[][] Map)
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

        private int[][] GetEmptyMap()
        {
            int[][] Map = new int[12][];
            for (int i = 0; i < 12; i++)
            {
                Map[i] = new int[12];
                for (int j = 0; j < 12; j++)
                {
                    if ((i == 0) || (i == 11))
                    {
                        Map[i][j] = 2;
                    }
                    else if ((j == 0) || (j == 11))
                    {
                        Map[i][j] = 2;
                    }
                    else
                    {
                        Map[i][j] = 0;
                    }
                }
            }
            return Map;
        }

        private static int[][] GetPoints()
        {
            int[][] Points = {};
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int[] point = { i, j };
                    Array.Resize(ref Points, Points.Length + 1);
                    Points[Points.Length - 1] = point;
                }
            }
            return Points;
        }

        public class SeaGround
        {
            public Cell[][] Map { get; set; }
            public string UserType { get; set; }
            public SeaGround(Cell[][] GrMap, string Type)
            {
                Map = GrMap;
                UserType = Type;
            }
        }
        
        public class Cell : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            public int X { get; set; }
            public int Y { get; set; }
            private string State = "";
            public string Content 
            { 
                get { return State; }
                set
                {
                    State = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Content"));
                } 
            }
            private string ColourState = "";
            public string Colour
            {
                get { return ColourState; }
                set
                {
                    ColourState = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Colour"));
                }
            }
            private string BorderState = "";
            public string BorderColour
            {
                get { return BorderState; }
                set
                {
                    BorderState = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("BorderColour"));
                }
            }
            public Cell(int x, int y, string state, string colourState, string borderState)
            {
                X = x;
                Y = y;
                State = state;
                ColourState = colourState;
                BorderState = borderState;
            }
        }

        private void ChooseGameModeCLick(object sender, RoutedEventArgs e)
        {
            ChooseModePanel.Visibility = Visibility.Collapsed;
            Button button = (sender as Button);
            if (button.Name.ToString() == "PC")
            {
                PrepareGameBot();
                Mode = "PC";
            }
        }

        private void PcChooseCreationMode(object sender, RoutedEventArgs e)
        {
            PcChooseCreationModePanel.Visibility = Visibility.Collapsed;
            Button button = (sender as Button);
            if (button.Name.ToString() == "Random")
            {
                Map1 = GenerateMap();
                Player1Map.Map = GetGroundByMap(Map1);
                StartGame();
            }
        }

        private void Player1MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mode == "PC")
            {
                return;
            }
        }

        private void Player2MouseDown(object sender, MouseButtonEventArgs e)
        {
            Cell cell = ((sender as FrameworkElement).DataContext as Cell);
            if ((Mode == "PC") && (Step == "Игрок №1"))
            {
                if (Map2[cell.X + 1][cell.Y + 1] == 1)
                {
                    Player2Map.Map[cell.X][cell.Y].Content = "X";
                    Points2Cnt -= 1;
                    if (Points2Cnt == 0)
                    {
                        FinishGame("Игрок №1");
                    }
                }
                else
                {
                    Player2Map.Map[cell.X][cell.Y].Content = ".";
                    Player2Map.Map[cell.X][cell.Y].Colour = "LightGreen";
                    Step = "Игрок №2";
                    PlayerStep.Text = Step;
                    BotStep();
                }
            }
            else
            {
                return;
            }
        }

        private void RestartGameClick(object sender, RoutedEventArgs e)
        {
            WinnerPanel.Visibility = Visibility.Collapsed;
            Init();
        }

        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            StartPanel.Visibility = Visibility.Collapsed;
            Init();
        }

    }

}
