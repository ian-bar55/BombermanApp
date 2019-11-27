using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombermanApp
{
    class Utilitario
    {
         public static bool validarEntrada(int R, int C, int N) //Valida se os numeros de linhas, colunas e segundos da entrada estao de acordo com os ranges do escopo.
         {
            if (R < 1 || C < 1 || R > 200 || C > 200 || N < 1 || N > 1000000000)
                return false;
            else
                return true;
         }

    }
}
