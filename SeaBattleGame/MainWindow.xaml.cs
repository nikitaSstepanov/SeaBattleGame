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
using SeaBattleGame.Utils;

namespace SeaBattleGame
{

    public partial class MainWindow : Window
    {

        private Map MapManager = new Map();
        private Ground GroundManager = new Ground();
        private Random randomizer = new Random();
        public SeaGround Player1Map;
        public SeaGround Player2Map;
        private int[][] Map1;
        private int[][] Map2;
        private string Mode;
        private string Step;
        private int Player1BoatsCnt;
        private int Player2BoatsCnt;
        private int Points1Cnt;
        private int Points2Cnt;
        private int[][] Points;
                
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
            Points = MapManager.GetPoints();
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
            Map2 = MapManager.GenerateMap(1);
            Player2Map.Map = GroundManager.GetClosedGround();
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
                Map1 = MapManager.GenerateMap(0);
                Player1Map.Map = GroundManager.GetGroundByMap(Map1);
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
                if ((Map2[cell.X + 1][cell.Y + 1] == 1) && (Player2Map.Map[cell.X][cell.Y].Content != "X"))
                {
                    Player2Map.Map[cell.X][cell.Y].Content = "X";
                    Points2Cnt -= 1;
                    if (Points2Cnt == 0)
                    {
                        FinishGame("Игрок №1");
                    }
                }
                else if((Player2Map.Map[cell.X][cell.Y].Content != ".") && (Player2Map.Map[cell.X][cell.Y].Content != "X"))
                {
                    Player2Map.Map[cell.X][cell.Y].Content = ".";
                    Player2Map.Map[cell.X][cell.Y].Colour = "LightGreen";
                    Step = "Игрок №2";
                    PlayerStep.Text = Step;
                    BotStep();
                }
                else
                {
                    return;
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
