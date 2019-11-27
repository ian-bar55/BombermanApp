using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanApp
{
    class Bomberman
    {
        public List<Bomb> listarBombasLate(string[,] grid, List<Bomb> bombasLate) //Preenche a lista bombasLate, que explodirao apos as bombas da lista bombasEarly.
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != "X" && grid[i, j] != "O")
                    {
                        Bomb bomba = new Bomb(i, j);
                        bombasLate.Add(bomba);
                    }

                }
            }

            return bombasLate;
        }
        public string[,] encherGridDeBombas(string[,] grid) //Este metodo preenche todos os espacos . (vazio) do grid com espacos O (bomba).
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != "X" && grid[i,j] != "O")
                    {
                        grid[i, j] = "O";
                    }
                        
                }
            }

            return grid;
        }
        

    }
}
