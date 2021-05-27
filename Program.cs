using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{
    class Program
    {
        static void Goster(char[,] matris)
        {
            for (int i = 0; i < matris.GetLength(0); i++)
            {
                for (int j = 0; j < matris.GetLength(1); j++)
                {
                    Console.Write(" " + matris[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            char[,] yol = new char[10, 10];
            int puan = 0;
            for (int i = 0; i < yol.GetLength(0); i++)
            {
                for (int j = 0; j < yol.GetLength(1); j++)
                {
                    yol[i, j] = '0';
                }
            }
            int yol1, yol2, yol3;
            Random rnd = new Random();
            while (true)
            {
                yol1 = rnd.Next(0, 10);
                yol2 = rnd.Next(0, 10);
                yol3 = rnd.Next(0, 10);
                if (yol1 != yol2 && yol2 != yol3)
                {
                    break;
                }
            }
            int konum = yol.GetLength(1) - 1;
            yol[9, yol1] = '1';
            yol[9, yol2] = '1';
            yol[9, yol3] = '1';
            for (int i = yol.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < yol.GetLength(1); j++)
                {
                    if (yol[i, j] == '1')
                    {
                        if (i - 1 >= 0)
                        {
                            int yon = rnd.Next(0, 5);
                            if (yon == 0 && j - 1 > 0)
                            {
                                if (i == 9)
                                {
                                    j--;
                                }
                                else
                                {
                                    yol[i, j - 1] = '1';
                                    j--;
                                }
                            }
                            else if (yon == 1 && j + 1 < yol.GetLength(1))
                            {
                                if (i == 9)
                                {
                                    j--;
                                }
                                else
                                    yol[i, j + 1] = '1';

                            }
                            else
                            {
                                yol[i - 1, j] = '1';
                            }
                        }
                    }
                }
            }
            int bomba1x = 0, bomba1y = 0, bomba2x = 0, bomba2y = 0;
            for (int i = 5; i < yol.GetLength(0); i++)
            {
                for (int j = 0; j < yol.GetLength(1); j++)
                {
                    int bomba = rnd.Next(0, 2);
                    if (bomba == 1 && yol[i, j] == '1')
                    {
                        if (bomba1x == 0)
                        {
                            bomba1x = i;
                            bomba1y = j;
                        }
                        else
                        {
                            bomba2x = i;
                            bomba2y = j;
                            break;
                        }
                    }
                }
                if (bomba2x != 0)
                {
                    break;
                }
            }
            bool goster = false;
            //Console.WriteLine("Bomba konumları:\nBomba 1: {0} x {1} \n Bomba 2: {2} x {3}", bomba1x, bomba1y, bomba2x, bomba2y);
            Goster(yol);
            Console.WriteLine("Bir yol seçiniz 1 - 2 - 3 :");
            int secim = Convert.ToInt32(Console.ReadLine());
            int x, y;
            x = yol.GetLength(1) - 1;
            if (secim == 1)
            {
                y = yol1;
            }
            else if (secim == 2)
            {
                y = yol2;
            }
            else
            {
                y = yol3;
            }
            yol[x, y] = 'K';
            char yedek1 = yol[bomba1x, bomba1y], yedek2 = yol[bomba2x, bomba2y];
            while (true)
            {
                if (goster)
                {
                    yedek1 = yol[bomba1x, bomba1y];
                    yedek2 = yol[bomba2x, bomba2y];
                    yol[bomba1x, bomba1y] = '2';
                    yol[bomba2x, bomba2y] = '2';
                }
                Goster(yol);
                Console.WriteLine("Bir yöne gidiniz: (w-a-s-d)");
                char yon = Convert.ToChar(Console.ReadLine());
                if (yon == 'G')
                {
                    if (goster)
                    {
                        goster = false;
                        yol[bomba1x, bomba1y] = yedek1;
                        yol[bomba2x, bomba2y] = yedek2;
                        Console.WriteLine("Bombalar gizleniyor.");
                    }
                    else
                    {
                        goster = true;
                        Console.WriteLine("Bombalar gösteriliyor.");
                    }
                }
                else
                {
                    if (yon == 'w')
                    {
                        if (yol[x - 1, y] == '1' || yol[x - 1, y] == '2')
                        {
                            yol[x, y] = '1';
                            puan += 5;
                            x--;
                        }
                        else
                        {
                            puan--;
                            Console.WriteLine("Geçersiz hareket.");
                        }
                    }
                    else if (yon == 'a')
                    {
                        if (yol[x, y - 1] == '1' || yol[x, y - 1] == '2')
                        {
                            puan += 5;
                            yol[x, y] = '1';
                            y--;
                        }
                        else
                        {
                            puan--;
                            Console.WriteLine("Geçersiz hareket.");
                        }
                    }
                    else if (yon == 'd')
                    {
                        if (yol[x, y + 1] == '1' || yol[x, y + 1] == '2')
                        {
                            puan += 5;
                            yol[x, y] = '1';
                            y++;
                        }
                        else
                        {
                            puan--;
                            Console.WriteLine("Geçersiz hareket.");
                        }
                    }
                    else if (yon == 's')
                    {
                        if (yol[x + 1, y] == '1' || yol[x + 1, y] == '2')
                        {
                            puan += 5;
                            yol[x, y] = '1';
                            x++;
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz hareket.");
                            puan--;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz Hareket.");
                    }
                    if ((x == bomba1x && y == bomba1y) || (x == bomba2x && y == bomba2y))
                    {
                        Console.WriteLine("Bombaya bastınız ve kaybettiniz!\nPuanınız: {0}", puan);
                        break;
                    }
                    yol[x, y] = 'K';
                    if ((y == yol1 || y == yol2 || y == yol3) && x == yol.GetLength(0) - 1)
                    {
                        yol[x, y] = '1';
                        Console.WriteLine("Bir yol seçiniz 1 - 2 - 3 :");
                        secim = Convert.ToInt32(Console.ReadLine());
                        x = yol.GetLength(1) - 1;
                        if (secim == 1)
                        {
                            y = yol1;
                        }
                        else if (secim == 2)
                        {
                            y = yol2;
                        }
                        else
                        {
                            y = yol3;
                        }
                        yol[x, y] = 'K';
                    }
                    if (x == 0)
                    {
                        Console.WriteLine("Tebrikler kazandınız! Puanınz: {0}", puan);
                        break;
                    }
                }

            }
            Console.ReadKey();
        }
    }
}
