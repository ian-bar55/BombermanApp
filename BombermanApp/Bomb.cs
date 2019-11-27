using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanApp
{
    class Bomb
    {
        public int linha;  //linha onde a bomba esta
        public int coluna; //coluna onde a bomba esta

        
        public Bomb(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public static string[,] Detonar(string[,] grid,List<Bomb> bombasEarly) //Detona as bombas, esvaziando a linha e a coluna de cada bomba ate encontrar um obstaculo.
        {
            foreach(Bomb bomba in bombasEarly)
            {
                for (int i = bomba.linha + 1; i < grid.GetLength(0); i++)
                {
                    if (grid[i, bomba.coluna] == "X")
                    {
                        break;
                    }
                    grid[i, bomba.coluna] = ".";
                }
                for (int i = bomba.linha - 1; i >= 0; i--)
                {
                    if (grid[i, bomba.coluna] == "X")
                    {
                        break;
                    }
                    grid[i, bomba.coluna] = ".";
                }
                for (int j = bomba.coluna + 1; j < grid.GetLength(1); j++)
                {
                    if (grid[bomba.linha, j] == "X")
                    {
                        break;
                    }
                    grid[bomba.linha, j] = ".";
                }
                for (int j = bomba.coluna - 1; j >= 0; j--)
                {
                    if (grid[bomba.linha, j] == "X")
                    {
                        break;
                    }
                    grid[bomba.linha, j] = ".";
                }
                grid[bomba.linha, bomba.coluna] = ".";
            }
                     
            return grid;
        }

    }
}
