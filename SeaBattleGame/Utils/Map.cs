using System;

namespace SeaBattleGame.Utils
{
    class Map
    {

        private int[][][] Boats1 = new int[10][][];
        private int[][][] Boats2 = new int[10][][];
        private Random randomizer = new Random();

        public int[][] GenerateMap(int UserId)
        {
            int[][] Map = GetEmptyMap();
            int MaxLengthBoat = 4;
            int[][] PointsClone = GetPoints();
            string VorH = "V";
            int[][][] Boats = new int[10][][];
            int ShipIndex = 0;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int maxL = MaxLengthBoat;
                    int[][] Ship = new int[MaxLengthBoat + 1][];
                    while (true)
                    {
                        if (maxL == 0)
                        {
                            break;
                        }
                        int PointIndex = randomizer.Next(0, PointsClone.Length);
                        int[] point = PointsClone[PointIndex];
                        if (ValidatePoint(Map, point, point))
                        {
                            Map[point[0]][point[1]] = 1;
                            maxL -= 1;
                        }
                        else
                        {
                            continue;
                        }
                        if (MaxLengthBoat == 1)
                        {
                            Ship[0] = point;
                            Ship[1] = new int[1] { 1 };
                        }
                        else if (MaxLengthBoat == 2)
                        {
                            if (ValidatePoint(Map, new int[2] { point[0] - 1, point[1] }, point) && VorH == "V")
                            {
                                Map[point[0] - 1][point[1]] = 1;
                                VorH = "H";
                                Ship[0] = point;
                                Ship[1] = new int[2] { point[0] - 1, point[1] };
                                Ship[2] = new int[1] { 2 };
                                break;
                            }
                            else if (ValidatePoint(Map, new int[2] { point[0], point[1] - 1 }, point) && VorH == "H")
                            {
                                Map[point[0]][point[1] - 1] = 1;
                                VorH = "V";
                                Ship[0] = point;
                                Ship[1] = new int[2] { point[0], point[1] - 1 };
                                Ship[2] = new int[1] { 2 };
                                break;
                            }
                            else if (ValidatePoint(Map, new int[2] { point[0] + 1, point[1] }, point) && VorH == "V")
                            {
                                Map[point[0] + 1][point[1]] = 1;
                                VorH = "H";
                                Ship[0] = point;
                                Ship[1] = new int[2] { point[0] + 1, point[1] };
                                Ship[2] = new int[1] { 2 };
                                break;
                            }
                            else if (ValidatePoint(Map, new int[2] { point[0], point[1] + 1 }, point) && VorH == "H")
                            {
                                Map[point[0]][point[1] + 1] = 1;
                                VorH = "V";
                                Ship[0] = point;
                                Ship[1] = new int[2] { point[0], point[1] + 1 };
                                Ship[2] = new int[1] { 2 };
                                break;
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
                            if (ValidatePoint(Map, new int[2] { point[0] - 1, point[1] }, point) && VorH == "V")
                            {
                                Map[point[0] - 1][point[1]] = 1;
                                maxL -= 1;
                                if (ValidatePoint(Map, new int[2] { point[0] - 2, point[1] }, new int[2] { point[0] - 1, point[1] }))
                                {
                                    Map[point[0] - 2][point[1]] = 1;
                                    VorH = "H";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0] - 1, point[1] };
                                    Ship[2] = new int[2] { point[0] - 2, point[1] };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else if (ValidatePoint(Map, new int[2] { point[0] + 1, point[1] }, new int[2] { point[0] - 1, point[1] }))
                                {
                                    Map[point[0] + 1][point[1]] = 1;
                                    VorH = "H";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0] - 1, point[1] };
                                    Ship[2] = new int[2] { point[0] + 1, point[1] };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else
                                {
                                    Map[point[0] - 1][point[1]] = 0;
                                    maxL += 1;
                                }
                            }
                            if (ValidatePoint(Map, new int[2] { point[0], point[1] - 1 }, point) && VorH == "H")
                            {
                                Map[point[0]][point[1] - 1] = 1;
                                maxL -= 1;
                                if (ValidatePoint(Map, new int[2] { point[0], point[1] - 2 }, new int[2] { point[0], point[1] - 1 }))
                                {
                                    Map[point[0]][point[1] - 2] = 1;
                                    VorH = "V";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0], point[1] - 1 };
                                    Ship[2] = new int[2] { point[0], point[1] - 2 };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else if (ValidatePoint(Map, new int[2] { point[0], point[1] + 1 }, new int[2] { point[0], point[1] - 1 }))
                                {
                                    Map[point[0]][point[1] + 1] = 1;
                                    VorH = "V";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0], point[1] - 1 };
                                    Ship[2] = new int[2] { point[0], point[1] + 1 };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else
                                {
                                    Map[point[0]][point[1] - 1] = 0;
                                    maxL += 1;
                                }
                            }
                            if (ValidatePoint(Map, new int[2] { point[0] + 1, point[1] }, point) && VorH == "V")
                            {
                                Map[point[0] + 1][point[1]] = 1;
                                maxL -= 1;
                                if (ValidatePoint(Map, new int[2] { point[0] + 2, point[1] }, new int[2] { point[0] + 1, point[1] }))
                                {
                                    Map[point[0] + 2][point[1]] = 1;
                                    VorH = "H";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0] + 1, point[1] };
                                    Ship[2] = new int[2] { point[0] + 2, point[1] };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else if (ValidatePoint(Map, new int[2] { point[0] - 1, point[1] }, new int[2] { point[0] + 1, point[1] }))
                                {
                                    Map[point[0] - 1][point[1]] = 1;
                                    VorH = "H";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0] + 1, point[1] };
                                    Ship[2] = new int[2] { point[0] - 1, point[1] };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else
                                {
                                    Map[point[0] + 1][point[1]] = 0;
                                    maxL += 1;
                                }
                            }
                            if (ValidatePoint(Map, new int[2] { point[0], point[1] + 1 }, point) && VorH == "H")
                            {
                                Map[point[0]][point[1] + 1] = 1;
                                maxL -= 1;
                                if (ValidatePoint(Map, new int[2] { point[0], point[1] + 2 }, new int[2] { point[0], point[1] + 1 }))
                                {
                                    Map[point[0]][point[1] + 2] = 1;
                                    VorH = "V";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0], point[1] + 1 };
                                    Ship[2] = new int[2] { point[0], point[1] + 2 };
                                    Ship[3] = new int[1] { 3 };
                                    break;
                                }
                                else if (ValidatePoint(Map, new int[2] { point[0], point[1] - 1 }, new int[2] { point[0], point[1] + 1 }))
                                {
                                    Map[point[0]][point[1] - 1] = 1;
                                    VorH = "V";
                                    Ship[0] = point;
                                    Ship[1] = new int[2] { point[0], point[1] + 1 };
                                    Ship[2] = new int[2] { point[0], point[1] - 1 };
                                    Ship[3] = new int[1] { 3 };
                                    break;
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
                            if (Map[point[0] - 1][point[1]] == 0 && VorH == "V")
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
                                        VorH = "H";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0] - 1, point[1] };
                                        Ship[2] = new int[2] { point[0] - 2, point[1] };
                                        Ship[3] = new int[2] { point[0] - 3, point[1] };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else if (Map[point[0] + 1][point[1]] == 0)
                                    {
                                        Map[point[0] + 1][point[1]] = 1;
                                        VorH = "H";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0] - 1, point[1] };
                                        Ship[2] = new int[2] { point[0] - 2, point[1] };
                                        Ship[3] = new int[2] { point[0] + 1, point[1] };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else
                                    {
                                        Map[point[0] - 1][point[1]] = 0;
                                        Map[point[0] - 2][point[1]] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            if (Map[point[0]][point[1] - 1] == 0 && VorH == "H")
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
                                        VorH = "V";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0], point[1] - 1 };
                                        Ship[2] = new int[2] { point[0], point[1] - 2 };
                                        Ship[3] = new int[2] { point[0], point[1] - 3 };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else if (Map[point[0]][point[1] + 1] == 0)
                                    {
                                        Map[point[0]][point[1] + 1] = 1;
                                        VorH = "V";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0], point[1] - 1 };
                                        Ship[2] = new int[2] { point[0], point[1] - 2 };
                                        Ship[3] = new int[2] { point[0], point[1] + 1 };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else
                                    {
                                        Map[point[0]][point[1] - 1] = 0;
                                        Map[point[0]][point[1] - 2] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            if (Map[point[0] + 1][point[1]] == 0 && VorH == "V")
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
                                        VorH = "H";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0] + 1, point[1] };
                                        Ship[2] = new int[2] { point[0] + 2, point[1] };
                                        Ship[3] = new int[2] { point[0] + 3, point[1] };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else if (Map[point[0] - 1][point[1]] == 0)
                                    {
                                        Map[point[0] - 1][point[1]] = 1;
                                        VorH = "H";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0] + 1, point[1] };
                                        Ship[2] = new int[2] { point[0] + 2, point[1] };
                                        Ship[3] = new int[2] { point[0] - 1, point[1] };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else
                                    {
                                        Map[point[0] + 1][point[1]] = 0;
                                        Map[point[0] + 2][point[1]] = 0;
                                        maxL += 2;
                                    }
                                }
                            }
                            if (Map[point[0]][point[1] + 1] == 0 && VorH == "H")
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
                                        VorH = "V";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0], point[1] + 1 };
                                        Ship[2] = new int[2] { point[0], point[1] + 2 };
                                        Ship[3] = new int[2] { point[0], point[1] + 3 };
                                        Ship[4] = new int[1] { 4 };
                                        break;
                                    }
                                    else if (Map[point[0]][point[1] - 1] == 0)
                                    {
                                        Map[point[0]][point[1] - 1] = 1;
                                        VorH = "V";
                                        Ship[0] = point;
                                        Ship[1] = new int[2] { point[0], point[1] + 1 };
                                        Ship[2] = new int[2] { point[0], point[1] + 2 };
                                        Ship[3] = new int[2] { point[0], point[1] - 1 };
                                        Ship[4] = new int[1] { 4 };
                                        break;
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
                    Boats[ShipIndex] = Ship;
                    ShipIndex += 1;
                }
                MaxLengthBoat -= 1;
            }
            if (UserId == 0)
            {
                Boats1 = Boats;
            }
            else
            {
                Boats2 = Boats;
            }
            return Map;
        }

        private bool ValidatePoint(int[][] Map, int[] point, int[] exception)
        {
            if (Map[point[0]][point[1]] == 0)
            {
                if (Map[point[0] - 1][point[1]] == 0 ||
                    point[0] - 1 == exception[0] && point[1] == exception[1])
                {
                    if (Map[point[0]][point[1] - 1] == 0 ||
                       point[0] == exception[0] && point[1] - 1 == exception[1])
                    {
                        if (Map[point[0] + 1][point[1]] == 0 ||
                            point[0] + 1 == exception[0] && point[1] == exception[1])
                        {
                            if (Map[point[0]][point[1] + 1] == 0 ||
                                point[0] == exception[0] && point[1] + 1 == exception[1])
                            {
                                if (Map[point[0] - 1][point[1] - 1] == 0)
                                {
                                    if (Map[point[0] - 1][point[1] + 1] == 0)
                                    {
                                        if (Map[point[0] + 1][point[1] - 1] == 0)
                                        {
                                            if (Map[point[0] + 1][point[1] + 1] == 0)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int[][] GetEmptyMap()
        {
            int[][] Map = new int[12][];
            for (int i = 0; i < 12; i++)
            {
                Map[i] = new int[12];
                for (int j = 0; j < 12; j++)
                {
                    if (i == 0 || i == 11)
                    {
                        Map[i][j] = 2;
                    }
                    else if (j == 0 || j == 11)
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

        public int[][] GetPoints()
        {
            int[][] Points = { };
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

        public Status CheckBoats(int[] point, int UserId)
        {
            int[][][] Boats;
            Status result = new Status(false);
            bool Flag = false;
            if (UserId == 0)
            {
                Boats = Boats1;
            }
            else
            {
                Boats = Boats2;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < Boats[i].Length - 1; j++)
                {
                    if ((Boats[i][j][0] == (point[0] + 1)) &&
                        (Boats[i][j][1] == (point[1] + 1)))
                    {
                        Boats[i][Boats[i].Length - 1][0] -= 1;
                        if (Boats[i][Boats[i].Length - 1][0] == 0)
                        {
                            result.IsKilled = true;
                            result.Ship = Boats[i];
                            Flag = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (Flag == true)
                {
                    break;
                }
            }
            if (UserId == 0)
            {
                Boats1 = Boats;
            }
            else
            {
                Boats2 = Boats;
            }
            return result;
        }

    }

    class Status
    {
        public bool IsKilled;
        public int[][] Ship;
        public Status(bool status)
        {
            this.IsKilled = status;
        }
    }

}
