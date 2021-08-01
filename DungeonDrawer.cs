using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCSharp
{
	abstract class DungeonObject
    {
        protected int sizeX;
        protected int sizeY;

        protected int[,] dungeonMap;

        protected bool dUp;
        protected bool dDown;
        protected bool dLeft;
        protected bool dRight;

        virtual public void Create() { }
        virtual public void Draw(int x, int y) { }
    }

    class Room : DungeonObject
    {
        public Room(int sizeX, int sizeY, bool dUp, bool dDown, bool dLeft, bool dRight)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            this.dUp = dUp;
            this.dDown = dDown;
            this.dLeft = dLeft;
            this.dRight = dRight;

            this.dungeonMap = new int[sizeY, sizeX];

            Create();
        }

        public override void Create()
        {
            for (int i = 0; i < sizeY; ++i)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if ((i == 0 && !dUp) || (i == sizeY - 1 && !dDown)
                        || (j == 0 && !dLeft) || (j == sizeX - 1 && !dRight))
                        dungeonMap[i, j] = 1;
                    else
                        dungeonMap[i, j] = 0;
                }
            }
        }

        public override void Draw(int x, int y)
        {
            for (int i = 0; i < sizeY; ++i)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < sizeX; j++)
                {
                    if (dungeonMap[i, j] == 1)
                        Console.ForegroundColor = ConsoleColor.White;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(dungeonMap[i, j]);
                }
            }
        }
    }

    class Corridor : DungeonObject
    {
        public Corridor(int sizeX, int sizeY, bool dUp, bool dDown, bool dLeft, bool dRight)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            this.dUp = dUp;
            this.dDown = dDown;
            this.dLeft = dLeft;
            this.dRight = dRight;

            this.dungeonMap = new int[sizeY, sizeX];

            Create();
        }

        public override void Create()
        {
            int x = 0;
            int y = 0;

            if (dUp && (!dLeft && !dRight))
                x = 4;

            for (int i = 0; i < sizeY; ++i)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (x != 0 && j > sizeX - x - 1 && j < x)
                    {
                        if ((i == sizeX - x && !dUp) || (i == x && !dDown) ||
                            (j == sizeX - x && !dLeft) || (j == x - 1 && !dRight))
                            dungeonMap[i, j] = 1;
                        else
                            dungeonMap[i, j] = 4;

                        if (i == 0 || i == sizeX - 2)
                            for (int z = 0; z < sizeX; ++z)
                                if (dungeonMap[i, z] == 0)
                                    dungeonMap[i, z] = 1;
                    }
                    else if (y != 0 && i > sizeY - y - 1 && i < y)
                    {
                        if ((i == sizeY - y && !dUp) || (i == y - 1))
                            dungeonMap[i, j] = 1;
                        else
                            dungeonMap[i, j] = 9;

                        if (j == 0)
                            for (int z = 0; z < sizeY; ++z)
                                if (dungeonMap[z, j] == 0)
                                    dungeonMap[z, j] = 1;
                    }
                    if (x == 0 && y == 0)
                    {
                        if ((i == 0 && !dUp) || (i == sizeY - 1 && !dDown) ||
                            (j == 0 && !dLeft) || (j == sizeX - 1 && !dRight))
                            dungeonMap[i, j] = 1;
                        else
                            dungeonMap[i, j] = 2;
                    }
                }
            }
        }

        public override void Draw(int x, int y)
        {
            for (int i = 0; i < sizeY; ++i)
            {
                Console.SetCursorPosition(x, y + i);
                for (int j = 0; j < sizeX; j++)
                {
                    if (dungeonMap[i, j] == 1)
                        Console.ForegroundColor = ConsoleColor.White;
                    else if (dungeonMap[i, j] == 0)
                        Console.ForegroundColor = ConsoleColor.Black;
                    else
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(dungeonMap[i, j]);
                }
            }
        }
    }

    class DungeonDrawer
    {
        public DungeonGenerator gd;
        private AreaType[,] dungeonMap;

        int sizeX;
        int sizeY;

        public DungeonDrawer(int width, int length)
        {
            sizeX = width;
            sizeY = length;
            gd = new DungeonGenerator(width, length);
            dungeonMap = gd.Init();
        }

        public void Draw()
        {
            int d = 4;

            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            int k = 0;
            bool haveCorridor = false;
            bool mustCorridor = false;

            DungeonObject dungeonObject = null;

            for (int i = 0; i < sizeY; ++i)
            {
                k = 0;
                for (int j = 0; j < sizeX; ++j)
                {
                    if (dungeonMap[i, j].Type == 1 || dungeonMap[i, j].Type == 2)
                    {
                        ++k;

                        up = false;
                        down = false;
                        left = false;
                        right = false;

                        if (k == 1 && (j + 1 == sizeX || (j + 1 <= sizeX && dungeonMap[i, j + 1].Type == 0)))
                        {
                            right = false;
                            left = false;
                            up = true;
                            haveCorridor = true;
                        }
                        else if (i != 0 && dungeonMap[i, j].Type == 2 && (j + 1 <= sizeX && dungeonMap[i, j + 1].Type != 2) && dungeonMap[i - 1, j].Type != 0)
                        {
                            if (j + 1 != sizeX)
                                if (dungeonMap[i, j + 1].Type != 0)
                                    right = true;

                            if (j != 0)
                                if (dungeonMap[i, j - 1].Type != 0)
                                    left = true;

                            up = true;
                            haveCorridor = true;
                        }
                        else if (j + 1 == sizeX || (j + 1 <= sizeX && dungeonMap[i, j + 1].Type == 0))
                        {
                            left = true;
                            right = false;
                        }
                        else if (k == 1)
                        {
                            right = true;

                            if (i != 0 && dungeonMap[i, j].Type == 2 && (j + 1 <= sizeX && dungeonMap[i, j + 1].Type != 2) && dungeonMap[i - 1, j].Type != 0 && !haveCorridor)
                            {
                                up = true;
                                haveCorridor = true;
                            }
                            else if (i != 0 && dungeonMap[i - 1, j].Type == 2 && !haveCorridor)
                            {
                                up = true;
                                haveCorridor = true;
                            }
                        }
                        else if (k > 1)
                        {
                            right = true;
                            left = true;

                            if (i != 0 && dungeonMap[i, j].Type == 2 && (j + 1 <= sizeX && dungeonMap[i, j + 1].Type != 2) && dungeonMap[i - 1, j].Type != 0 && !haveCorridor)
                            {
                                up = true;
                                haveCorridor = true;
                            }
                            else if (i != 0 && dungeonMap[i - 1, j].Type == 2 && !haveCorridor)
                            {
                                up = true;
                                haveCorridor = true;
                            }
                        }

                        if (haveCorridor == false && i > 0 && j < sizeX)
                        {
                            for (int z = j; (z < sizeX); ++z)
                            {
                                if (dungeonMap[i, z].Type == 0)
                                    break;

                                if (dungeonMap[i - 1, z].Type != 0)
                                    mustCorridor = false;
                            }

                            if ((j + 1 != sizeX || dungeonMap[i, j + 1].Type == 0) && mustCorridor == false)
                                if (dungeonMap[i - 1, j].Type != 0)
                                    mustCorridor = true;

                            if ((j + 1 == sizeX) && mustCorridor == false)
                                if (dungeonMap[i - 1, j].Type != 0)
                                    mustCorridor = true;

                            if (mustCorridor)
                            {
                                up = true;
                                haveCorridor = true;
                            }
                        }

                        switch (dungeonMap[i, j].Type)
                        {
                            case 1:
                                dungeonObject = new Room(5, 5, up, down, left, right);
                                break;
                            case 2:
                                dungeonObject = new Corridor(5, 5, up, down, left, right);
                                break;
                        }
                        dungeonObject.Draw(j * d, i * d + 1);

                    }
                    else
                    {
                        k = 0;
                        haveCorridor = false;
                        mustCorridor = false;
                    }
                }
                k = 0;
                haveCorridor = false;
                mustCorridor = false;
            }
            Console.WriteLine();
        }
    }
}