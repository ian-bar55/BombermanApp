using System;
using System.Collections.Generic;

namespace BombermanApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int R = 0;
            int C = 0;
            int N = 0;
            string flag = null;
            Grid grid = new Grid();
            List<Bomb> bombasEarly = new List<Bomb>(); //lista de bombas que devem estar no grid de entrada e que detonarao primeiro           
            List<Bomb> bombasLate = new List<Bomb>(); //lista de bombas que detonarao depois das bombas na lista acima
            List<Obstaculo> obstaculosNoGrid = new List<Obstaculo>(); //lista de obstaculos que devem estar no grid de entrada
            Bomberman bomberman = new Bomberman();
            bool entradaValida = false;
            bool flagSaida = false;
            string exitProgramFlag = null;
            Console.WriteLine("BOMBERMAN APP\n");
            Console.WriteLine("Bem vindo ao Grid Retangular de Bomberman!\n");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Vamos iniciar definindo o tamanho do grid.\n");

            while (entradaValida == false)
            {
                Console.WriteLine("Por favor, informe o numero de LINHAS do grid. Ele deve estar entre 1 e 200.\n");
                R = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Agora, informe o numero de COLUNAS do grid. Ele deve estar entre 1 e 200 \n");
                C = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Digite o numero de segundos que deseja que a simulacao dure. Ele deve estar entre 1 e 1000000000.");
                N = Convert.ToInt32(Console.ReadLine());

                if (Utilitario.validarEntrada(R, C, N) == false) 
                {
                    Console.WriteLine("Valores de entrada invalidos. Vamos comecar novamente.\n");
                }
                else
                {
                    entradaValida = true;
                }
            }

            /* foram inseridos intervalos entre as mensagens para melhorar a experiencia de leitura para o usuario*/
            Console.WriteLine("Entendido. O grid tera " + R + " linhas e " + C + " colunas\n");
            System.Threading.Thread.Sleep(1000); 
            Console.WriteLine("Muito bem! Agora, falta definir o estado de entrada do grid.\n");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Todos os espacos do grid comecam vazios. Voce pode inserir uma bomba ou um obstaculo.\n");
            System.Threading.Thread.Sleep(1000);

            while (flag != "I" && flagSaida == false) //duas condicoes, que garantem que so se pode iniciar a simulacao depois de se plantar ao menos 1 bomba e depois de se digitar a letra I.
            {               
                Console.WriteLine("Para inserir uma bomba, digite 'B'\n");              
                Console.WriteLine("Para inserir um obstaculo, digite 'O'\n");
                if(bombasEarly.Count > 0)
                    Console.WriteLine("Digite 'I' para iniciar a simulacao.\n");

                flag = (Console.ReadLine());
      
                    switch (flag)
                    {
                        case "B":

                            Console.WriteLine("Por favor, digite a LINHA onde a bomba ficara.\n");
                            int linhaBomba = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Agora, digite a COLUNA onde a bomba ficara.\n");
                            int colunaBomba = Convert.ToInt32(Console.ReadLine());
                                                       
                            Bomb bomba = new Bomb(linhaBomba, colunaBomba);
                            bombasEarly.Add(bomba); //insere na lista de bombas que devem estar no grid de entrada a bomba criada acima.

                            Console.WriteLine("A bomba foi plantada no grid!\n");
                            Console.WriteLine("------------------------------------------------------------\n");

                            break;

                        case "O":

                            Console.WriteLine("Por favor, digite a LINHA onde o obstaculo ficara.\n");
                            int linhaObstaculo = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Agora, digite a COLUNA onde o obstaculo ficara.\n");
                            int colunaObstaculo = Convert.ToInt32(Console.ReadLine());

                            Obstaculo obstaculo = new Obstaculo(linhaObstaculo, colunaObstaculo);
                            obstaculosNoGrid.Add(obstaculo); //insere na lista de obstaculos que devem estar no grid de entrada o obstaculo criado acima.

                            Console.WriteLine("O obstaculo foi inserido no grid!\n");
                            Console.WriteLine("------------------------------------------------------------\n");

                            break;

                        case "I":

                            if (bombasEarly.Count > 0) //so se pode digitar I para iniciar o programa assim que for colocada a primeira bomba. 
                                flagSaida = true;
                            else
                                Console.WriteLine("Desculpe. Nao entendi o que voce digitou. Vamos comecar de novo.\n"); //se for digitado I antes de colocar uma bomba, o console pede novo digito 

                            break;

                        default:

                            Console.WriteLine("Desculpe. Nao entendi o que voce digitou. Vamos comecar de novo.\n");
                            break;
                    }                           
            }

            Console.Clear();

            grid.matriz = grid.gridBuilder(R, C, bombasEarly, obstaculosNoGrid); //cria o grid com o numero de linhas, numero de colunas, bombas e obstaculos que devem ser inseridos.

            Console.WriteLine("Este sera o estado inicial do grid: \n");

            Console.WriteLine(R + " " + C + " " + N + "\n"); //primeira linha da entrada conforme o escopo
            grid.imprimirGrid();

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("------------------------------------------------------------\n");

            Console.WriteLine("INICIANDO SIMULACAO...\n");

                var contador = new System.Threading.CountdownEvent(N); //aqui crio um contador para que a cada acao de bomberman seja diminuido um segundo do contador

                if (contador.CurrentCount == N) //esse if esta aqui para executar o primeiro segundo da execucao. O grid continua o mesmo da entrada no segundo 1.
                {
                    System.Threading.Thread.Sleep(1000);
                    contador.Signal();

                }
               
                while (contador.CurrentCount > 0) // executa a simulacao ate os segundos se esgotarem
                {
                    if (bombasLate.Count != 0) //esse if filtra se o while foi executado uma vez ao menos, para manter a lista bombsEarly intacta ate a primeira detonacao.
                    {
                        foreach (Bomb bomba in bombasLate) 
                             bombasEarly.Add(bomba);     // apos o while acabar, as bombas que deveriam explodir depois se tornam as que explodirao em seguida/primeiro, 
                    }                                    // e as bombas que encherao o grid se tornarao as bombas que explodirao depois.        

                    bombasLate = bomberman.listarBombasLate(grid.matriz, bombasLate);
                    grid.matriz = bomberman.encherGridDeBombas(grid.matriz);

                    System.Threading.Thread.Sleep(1000);
                    contador.Signal();

                    if (contador.CurrentCount == 0) //verifica se o contador diminuiu para zero no meio da execucao
                        continue;

                    grid.matriz = Bomb.Detonar(grid.matriz, bombasEarly); //retorna o grid depois das bombas em bombasEarly explodirem
                    bombasEarly = new List<Bomb>(); //esvazia a lista de bombasEarly
                    System.Threading.Thread.Sleep(1000);
                    contador.Signal();                
                }
 
                Console.WriteLine("O tempo da simulacao acabou!\n");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Veja como ficou a matriz apos " + N + " segundos:\n");

                grid.imprimirGrid();

                Console.WriteLine("SIMULACAO FINALIZADA\n");
                Console.WriteLine("------------------------------------------------------------\n");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Obrigado por fazer uma simulacao no Grid Retangular de Bomberman.\n");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Digite qualquer caractere para sair do programa.\n");

            while (exitProgramFlag == null) 
            {
                exitProgramFlag = Console.ReadLine(); 
            }
        }
}
}
