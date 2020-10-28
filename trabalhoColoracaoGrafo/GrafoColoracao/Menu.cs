using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoColoracaoGrafo.GrafoColoracao
{
    class Menu
    {
        public static void menu() 
        {
            int op;
            List<Vertice> grafo = new List<Vertice>();
            Funcoes F = new Funcoes();
            Console.Title = "Heurística da Coloração";
            do 
            {
                Console.Clear();
                op = lerOp();
                Console.Clear();
                switch (op) 
                {
                    case 1:
                        F.exibirGrafo(grafo);
                        break;
                    case 2:
                        grafo = F.inserirVertice(grafo);
                        break;
                    case 3:
                        grafo = F.removerVertice(grafo);
                        break;
                    case 4:
                        grafo = F.inserirAresta(grafo);
                        break;
                    case 5:
                        grafo = F.removerAresta(grafo);
                        break;
                    case 6:
                        F.verificarAdjacencia(grafo);
                        break;
                    case 7:
                        grafo = F.gerarGrafo(grafo);
                        break;
                }
                Console.WriteLine("\n<Pressione qualquer tecla para continuar.>");
                Console.ReadKey();
            } while (op != 8);
        }
        public static int lerOp() 
        {
            Console.WriteLine("\nMenu de opções");
            Console.WriteLine("1 - Exibir grafo");
            Console.WriteLine("2 - Inserir cidade (vértice)");
            Console.WriteLine("3 - Remover cidade (vértice)");
            Console.WriteLine("4 - Inserir rodovia (aresta)");
            Console.WriteLine("5 - Remover rodovia (aresta)");
            Console.WriteLine("6 - Verificar rodovia (adjacência)");
            Console.WriteLine("7 - Gerar novo grafo");
            Console.WriteLine("8 - Sair");
            Console.Write("\nOpção: ");

            return int.Parse(Console.ReadLine());
        }
    }
}
