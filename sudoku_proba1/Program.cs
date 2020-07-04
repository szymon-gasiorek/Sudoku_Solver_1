using System;
using System.Collections.Generic;
using System.IO;

namespace sudoku_proba1
{
    class Solver
    {
        List<int> zakres_liczb = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
 
        int[,] sudoku1 = new int[9, 9]
        {
                {5 , 3 , 0,      0 , 7 , 0,      0 , 0 , 0},
                {6 , 0 , 0,      1 , 9 , 5,      0 , 0 , 0},
                {0 , 9 , 8,      0 , 0 , 0,      0 , 6 , 0},

                {8 , 0 , 0,      0 , 6 , 0,      0 , 0 , 3},
                {4 , 0 , 0,      8 , 0 , 3,      0 , 0 , 1},
                {7 , 0 , 0,      0 , 2 , 0,      0 , 0 , 6},

                {0 , 6 , 0,      0 , 0 , 0,      2 , 8 , 0},
                {0 , 0 , 0,      4 , 1 , 9,      0 , 0 , 5},
                {0 , 0 , 0,      0 , 8 , 0,      0 , 7 , 9}
        };
        int[,] sudoku = new int[9, 9];

        public void wczytaj_sudoku()
        {
            // wczytuje dane do tablicy sudoku z pliku 'sudoku.txt'
            try
            {
                using (StreamReader dane = new StreamReader("sudoku.txt"))
                {
                    for (int i =0 ; i < 9; i++)
                    {
                        string[] liczby = dane.ReadLine().Split(' ');

                        for (int j = 0; j < 9 ; j++)
                        {
                            sudoku[i, j] = Convert.ToInt32(liczby[j]);                           
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Blad ! ");
                Console.WriteLine(e.Message);
            }       
        }
        public bool spr_rzad(int y,  int liczba)
        {
            //sprawdzanie czy w rzedzie nie ma juz takiej samej liczby
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[y, i] == liczba)
                {                 
                    return false;               
                }
            }                    
              return true;
        }

        public bool spr_kolumna( int x, int liczba)
        {
            //sprawdzanie czy w kolumnie nie ma juz takiej samej liczby
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i, x] == liczba)
                {                   
                    return false;
                }
            }      
            return true;
        }

        public bool spr_blok(int y, int x,  int liczba)
        {
            //sprawdzanie czy w jednym bloku 3x3 nie ma takiej samej liczby
            int blok_x = x/3*3;
            int blok_y = y/3*3;

            for (int i = blok_y; i < blok_y+3 ; i++)
            {
                for (int j = blok_x; j < blok_x+3 ; j++)
                {
                    if (sudoku[i, j] == liczba)
                    {                    
                        return false;
                    }
                }
            }          
            return true;
        }

        public bool spr_wszystko(int y, int x, int liczba)
        {
            //sprawdza wszystkie poprzednie warunki
            if (spr_rzad(y, liczba) == true && spr_kolumna(x, liczba) == true && spr_blok(y, x, liczba) == true)
            {
                return true;
            }
            else
            {
                return false;
            }        
        }

        public bool rozwiazanie_okienka(int y, int x )
        {
            // rozwiazanie pojedynczego pola
            foreach (int n in zakres_liczb)
            {
                if (spr_wszystko(y, x, zakres_liczb[n-1]) == true)
                {
                    sudoku[y, x] = n;
                    if (rozwiazanie_calosc())
                    {
                        return true;
                    }                   
                }   
            }

            sudoku[y, x] = 0;
            return false;
        }

        public bool rozwiazanie_calosc()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j] == 0)
                    {
                        return rozwiazanie_okienka(i,j);
                    }
                }
            }
            return true;
        }

        public void pokaz_sudoku()
        {
            //wyswietlenie sudoku
               for (int i = 0 ; i < 9 ; i++)
               {
                   if (i % 3 == 0 && i != 0)
                   {
                       Console.WriteLine("--------------------------");
                   }

                   for (int j = 0 ; j < 9 ; j++)
                   {
                       if (j % 3 == 0 && j != 0)
                       {
                           Console.Write(" |  ");                    
                       }

                       if (j == 8)
                       {
                           Console.Write(sudoku[i, j] + " ");
                           Console.WriteLine("");
                       }

                       else
                       {
                           Console.Write(sudoku[i, j] + " ");                      
                       }     
                   }
               }
        }
        
            static void Main(string[] args)
            {
                Console.SetWindowSize(50, 28);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Title = "Sudoku solver";
                Solver pr = new Solver();

                pr.wczytaj_sudoku();
                Console.WriteLine("                            Przed rozwiazaniem");

                pr.pokaz_sudoku();       
                pr.rozwiazanie_calosc();

                Console.WriteLine();
                Console.WriteLine("                            Po rozwiazaniu ");
                Console.WriteLine();

                pr.pokaz_sudoku();
                Console.ReadKey();


            }
    }
}
  
    