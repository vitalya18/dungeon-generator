using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCSharp
{
	public struct AreaType
	{
		public int Type;
		public bool Reach;
	};

	class DungeonGenerator
    {
        int sizeX;
        int sizeY;

        int maxLineRoom;
        int procentRoom;

        int x1;
        int l2;

        Random rand;

        public AreaType[,] dungeonMap;

        public DungeonGenerator(int x, int y, int procentRoom = 4)
        {
            this.sizeX = x;
            this.sizeY = y;

            this.procentRoom = procentRoom;

            this.maxLineRoom = (int)(sizeX * (procentRoom * 0.1));
            this.dungeonMap = new AreaType[sizeY, sizeX];

            this.rand = new Random();
        }

        private void SetRoom()
        {
            int k = 0;
            int j = 0;
            for (int i = 0; i < sizeY; i++)
            {
                for (j = 0; j < sizeX; j++)
                {
                    if (rand.Next(0, 2) == 1 && k < maxLineRoom)
                    {
                        ++k;
                        dungeonMap[i, j].Type = 1;
                    }
                    else
                        dungeonMap[i, j].Type = 0;
                    dungeonMap[i, j].Reach = false;
                }
                k = 0;
            }

            j = 0;
            do
            {
                j++;
            } while (dungeonMap[0, j - 1].Type != 1 && j - 1 < sizeX - 1);
            if ((j - 1) == (sizeX - 1) && dungeonMap[0, j - 1].Type == 0)
                dungeonMap[0, j - 1].Type = 1;
            dungeonMap[0, j - 1].Reach = true;
        }

        private void SetCorridor()
        {
            for (int i = 0; i < sizeY; i++)
                for (int j = 0; j < sizeX; j++)
                    if ((dungeonMap[i, j].Type == 1) && (dungeonMap[i, j].Reach == false))
                    {
                        Find2Near(i, j);
                        if ((x1 != j) && (l2 != i))
                            CreateCornPath(i, j);
                        else
                            CreateLinePath(i, j);
                    }
        }

        //Find nearest (Only find)
        private void Find2Near(int i, int j)
        {
            bool flag = true;
            int i1 = 1, j1 = 1;
            x1 = -5;
            l2 = -5;
            while (flag)
            {
                //Find up
                if ((i - i1) >= 0 && (dungeonMap[i - i1, j].Reach || dungeonMap[i - i1, j].Type == 2))
                {
                    l2 = i - i1;
                    x1 = j;
                    return;
                }
                //Left
                if ((j - j1) >= 0 && (dungeonMap[i, j - j1].Reach || dungeonMap[i, j - j1].Type == 2))
                {
                    l2 = i;
                    x1 = j - j1;
                    return;
                }
                //Right
                if ((j + j1) < sizeX && (dungeonMap[i, j + j1].Reach || dungeonMap[i, j + j1].Type == 2))
                {
                    l2 = i;
                    x1 = j + j1;
                    return;
                }
                //Left-Up
                if ((j - j1) >= 0 && (i - i1) >= 0 && (dungeonMap[i - i1, j - j1].Reach || dungeonMap[i - i1, j - j1].Type == 2))
                {
                    l2 = i - i1;
                    x1 = j - j1;
                    return;
                }
                //Right-Up
                if ((j + j1) < sizeX && (i - i1) >= 0 && (dungeonMap[i - i1, j + j1].Reach || dungeonMap[i - i1, j + j1].Type == 2))
                {
                    l2 = i - i1;
                    x1 = j + j1;
                    return;
                }
                if ((x1 != -5) && (l2 != -5))
                    flag = false;
                if ((i1 == (sizeY - 1)) && (j1 == sizeX - 1))
                    flag = false;
                if (j1 == (sizeX - 1))
                {
                    i1++;
                    j1 = 0;
                }
                j1++;
            }
        }

        private void CreateLinePath(int y2, int x2)
        {
            int i = y2;
            int j = x2;
            if (x1 == x2)
            {
                y2--;
                while (y2 != l2)
                {
                    dungeonMap[y2, x2].Type = 2;
                    y2--;
                }
                dungeonMap[i, j].Reach = true;
                return;
            }
            if (l2 == y2)
                if (x1 < x2)
                {
                    x2--;
                    while (x1 != x2)
                    {
                        dungeonMap[y2, x2].Type = 2;
                        x2--;

                    }
                    dungeonMap[i, j].Reach = true;
                    return;
                }
                else
                {
                    x2++;
                    while (x1 != x2)
                    {
                        dungeonMap[y2, x2].Type = 2;
                        x2++;
                    }
                    dungeonMap[i, j].Reach = true;
                    return;
                }
        }

        private void CreateCornPath(int y2, int x2)
        {
            int i = y2;
            int j = x2;

            if (rand.Next(0, 2) == 0)
            {
                //Part 1
                y2--;
                while (y2 != l2)
                {
                    dungeonMap[y2, x2].Type = 2;
                    y2--;
                }
                //Part 2
                dungeonMap[y2, x2].Type = 2;
                if (x1 < x2)
                {
                    x2--;
                    while (x1 != x2)
                    {
                        dungeonMap[y2, x2].Type = 2;
                        x2--;
                    }
                    dungeonMap[i, j].Reach = true;
                    return;
                }
                else
                {
                    x2++;
                    while (x1 != x2)
                    {
                        dungeonMap[y2, x2].Type = 2;
                        x2++;
                    }
                }
                dungeonMap[i, j].Reach = true;
                return;
            }
            else
            {
                //Part 1
                if (x1 < x2)
                {
                    x2--;
                    while (x1 != x2)
                    {
                        dungeonMap[y2, x2].Type = 2;
                        x2--;
                    }
                }
                else
                {
                    x2++;
                    while (x1 != x2)
                    {
                        dungeonMap[y2, x2].Type = 2;
                        x2++;
                    }
                }
                //Part 2
                dungeonMap[y2, x2].Type = 2;
                y2--;
                while (y2 != l2)
                {
                    dungeonMap[y2, x2].Type = 2;
                    y2--;
                }
                dungeonMap[i, j].Reach = true;
                return;
            }
        }

		public AreaType[,] Init()
		{
			SetRoom();
			SetCorridor();
			return dungeonMap;
		}

        public void Output()
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (dungeonMap[i, j].Type == 1 || dungeonMap[i, j].Type == 2)
                    {
                        switch (dungeonMap[i, j].Type)
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 1:
                                if (dungeonMap[i, j].Reach)
                                    Console.ForegroundColor = ConsoleColor.Green;
                                else
                                    Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                        }
                        Console.Write(dungeonMap[i, j].Type + " ");
                    }
                    else
                        Console.Write("  ");

                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
	}
}