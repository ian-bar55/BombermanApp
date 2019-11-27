using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanApp
{
    class Grid
    {
        public string[,] matriz;

        public string[,] gridBuilder(int linhas, int colunas, List<Bomb> bombasNoGrid, List<Obstaculo> obstaculosNoGrid) //Este metodo cria o grid ja colocando as bombas e obstaculos.
        {
            string[,] grid = new string[linhas, colunas];
            
            foreach(Bomb bomba in bombasNoGrid)
            {
                grid[bomba.linha, bomba.coluna] = "O";
            }

            foreach(Obstaculo obstaculo in obstaculosNoGrid)
            {
                grid[obstaculo.linha, obstaculo.coluna] = "X";
            }

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if(grid[i,j] != "O" && grid[i,j] != "X")
                        grid[i, j] = ".";
                }
            }

            this.matriz = grid;

            return grid;
        }

        public void imprimirGrid() //Este metodo imprime o grid no console.
        {
            for (int i = 0; i < this.matriz.GetLength(0); i++)
            {
                for (int j = 0; j < this.matriz.GetLength(1); j++)
                {
                    Console.Write(this.matriz[i, j]);
                }
                Console.Write("\n");
            }
        }

        
    }
}
