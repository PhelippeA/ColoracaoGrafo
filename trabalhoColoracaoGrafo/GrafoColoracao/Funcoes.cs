using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoColoracaoGrafo.GrafoColoracao
{
    class Funcoes
    {
        //OPERAÇÃO 1 -- EXIBIR GRAFO  
        public void exibirGrafo(List<Vertice> v)
        {
            Console.WriteLine("\n\n1) -- EXIBIR GRAFO\n\n");
            Console.WriteLine("--------------------------");
            if (v.Count != 0)
            {
                for (int i = 0; i < v.Count; i++)
                {
                    getCor(v[i].numCorVertice);    //  MOSTRAR A CIDADE COM A SUA COR ESPECÍFICA
                    Console.Write($"{v[i].nomeCidade}");
                    Console.ResetColor();
                    Console.Write(":  ");
                    cidadesAdjacentes(v, i);    //  LISTA DAS CIDADES ADJACENTES
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("GRAFO VAZIO !");
            Console.WriteLine("--------------------------");
        }
        //OPERAÇÃO 2 -- INSERIR VÉRTICE
        public List<Vertice> inserirVertice(List<Vertice> grafo)
        {
            Console.WriteLine("\n\n2) -- INSERIR CIDADE\n\n");
            Console.WriteLine("Informe o nome da cidade a ser inserida:");
            string nomeC = Console.ReadLine();

            //CORREÇÃO AO USUÁRIO
            if (grafo.Count != 0)
            {
                for (int i = 0; i < grafo.Count; i++)
                {
                    while (grafo[i].nomeCidade.ToLower() == nomeC.ToLower())
                    {
                        Console.WriteLine($"{nomeC} já existe!" + "\nInforme outro nome para a cidade:");
                        nomeC = Console.ReadLine();
                    }
                }
            }
            grafo.Add(new Vertice(nomeC, grafo.Count + 1, 2));

            exibirCidades(grafo);    //   LISTA DAS CIDADES
            Console.WriteLine("\nCidade inserida!");
            return grafo;
        }
        //OPERAÇÃO 3 -- REMOVER VÉRTICE 
        public List<Vertice> removerVertice(List<Vertice> grafo)
        {
            Console.WriteLine("\n\n3) -- REMOVER CIDADE\n\n");

            if (grafo.Count == 0)
            {
                Console.WriteLine("O grafo está vazio!");
                Console.WriteLine("Você precisa inserir ao menos uma cidade.");
                return grafo;
            }
            exibirCidades(grafo);    //   LISTA DAS CIDADES

            Console.WriteLine("Informe o número da cidade a ser removida:");
            int numC = int.Parse(Console.ReadLine());

            //CORREÇÃO AO USUÁRIO
            while (numC < 1 || numC > grafo.Count)
            {
                Console.Clear();
                Console.WriteLine("3) -- REMOVER CIDADE\n\n");
                exibirCidades(grafo);
                Console.WriteLine("Número inválido!");
                Console.WriteLine("Informe o número da cidade a ser removida:");
                numC = int.Parse(Console.ReadLine());
            }
            for (int i = 0; i < grafo.Count; i++)   // Remove as adjacências que contem esta cidade
                removerAdjacencia(grafo[i].adjacencia, numC);
            grafo.RemoveAt(numC - 1);    // Remove a cidade
            grafo = ajustarNumGrafo(grafo, numC);
            grafo = ajustarCor(grafo);
            exibirCidades(grafo);    //   LISTA DAS CIDADES
            Console.WriteLine("\nCidade removida!");
            return grafo;
        }
        //OPERAÇÃO 4 -- INSERIR ARESTA
        public List<Vertice> inserirAresta(List<Vertice> grafo)
        {
            Console.WriteLine("\n\n4) -- INSERIR RODOVIA\n\n");
            exibirCidades(grafo);

            int[] cidades = confereCidades(grafo.Count);

            if (verificarAdjBool(grafo, cidades[0], cidades[1]))
            {
                Console.WriteLine($"Já existe uma rodovia entre {grafo[cidades[0] - 1].nomeCidade} e {grafo[cidades[1] - 1].nomeCidade}!");
                return grafo;
            }

            grafo[cidades[0] - 1].adjacencia.Add(cidades[1]);
            grafo[cidades[1] - 1].adjacencia.Add(cidades[0]);
            grafo[cidades[0] - 1].adjacencia.Sort();
            grafo[cidades[1] - 1].adjacencia.Sort();

            grafo = ajustarCor(grafo);
            Console.WriteLine("\nRodovia inserida!");
            return grafo;
        }
        //OPERAÇÃO 5 -- REMOVER ARESTA
        public List<Vertice> removerAresta(List<Vertice> grafo)
        {
            Console.WriteLine("\n\n5) -- REMOVER RODOVIA\n\n");

            if (grafo.Count < 2)
            {
                Console.WriteLine("O grafo não possui duas cidades!");
                return grafo;
            }
            exibirCidades(grafo);

            int[] cidades = confereCidades(grafo.Count);

            if (!verificarAdjBool(grafo, cidades[0], cidades[1]))
            {
                Console.WriteLine($"Não existe uma rodovia entre {grafo[cidades[0] - 1].nomeCidade} e {grafo[cidades[1] - 1].nomeCidade}!");
                return grafo;
            }

            grafo[cidades[0] - 1].adjacencia = removerAdjacencia(grafo[cidades[0] - 1].adjacencia, cidades[1]);
            grafo[cidades[1] - 1].adjacencia = removerAdjacencia(grafo[cidades[1] - 1].adjacencia, cidades[0]);
            grafo = ajustarCor(grafo);
            Console.WriteLine("\nRodovia removida!");
            return grafo;
        }
        //OPERAÇÃO 6 -- VERIFICAR ADJACÊNCIA
        public void verificarAdjacencia(List<Vertice> grafo)
        {
            Console.WriteLine("\n\n6) -- VERIFICAR RODOVIA\n\n");

            if (grafo.Count < 2)
                Console.WriteLine("O grafo não possui duas cidades!");

            else
            {
                exibirCidades(grafo);
                int[] cidades = confereCidades(grafo.Count);

                if (verificarAdjBool(grafo, cidades[0], cidades[1]))
                    Console.WriteLine($"Existe uma rodovia ligada entre {grafo[cidades[0] - 1].nomeCidade} e {grafo[cidades[1] - 1].nomeCidade}!");
                else
                    Console.WriteLine($"Não existe rodovia ligada entre {grafo[cidades[0] - 1].nomeCidade} e {grafo[cidades[1] - 1].nomeCidade}!");
            }
        }
        //OPERAÇÃO 7 -- GERAR NOVO GRAFO
        Random r = new Random();
        enum Cidades { Betim, Contagem, Belo_Horizonte, Ipatinga, Uberaba, Nova_Lima, Ouro_Preto, Formiga, Diamantina };
        public List<Vertice> gerarGrafo(List<Vertice> grafoAtual)
        {
            Console.WriteLine("\n\n7 -- GERAR NOVO GRAFO\n\n");

            if (grafoAtual.Count > 0)
            {
                Console.WriteLine("Já existe um grafo atual!");
                Console.WriteLine("Você tem certeza que quer gerar um novo?");
                Console.WriteLine("\n1 -- SIM         2 -- NÃO");
                Console.WriteLine("\nOpção:");
                int op = int.Parse(Console.ReadLine());

                while (op != 1 && op != 2)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n7 -- GERAR NOVO GRAFO\n\n");
                    Console.WriteLine("Já existe um grafo atual!");
                    Console.WriteLine("Você tem certeza que quer gerar um novo?");
                    Console.WriteLine("\n1 -- SIM         2 -- NÃO");
                    Console.WriteLine("\nOpção:");
                    op = int.Parse(Console.ReadLine());
                }
                if (op == 2)
                    return grafoAtual;
            }
            grafoAtual.Clear();
            for (int i = 0; i < r.Next(3, 9); i++)
            {
                int sorteio = r.Next(1, 3), sorteio2 = r.Next(1, 3);  //sorteio 1 -> adicionar cidade   sorteio 2 -> adicionar rodovia
                if (sorteio == 1)
                {
                    grafoAtual.Add(new Vertice($"{(Cidades)i}", grafoAtual.Count + 1, 0)); // adiciona cidade aleatoria
                    if (grafoAtual.Count > 1 && sorteio2 == 1)   // adicionando rodovias
                    {
                        for (int j = 0; j < grafoAtual.Count; j++)
                        {
                            int sorteio3 = r.Next(1, 3);  //sorteio 3 -> adicionar x rodovias
                            if (sorteio3 == 1 && grafoAtual[grafoAtual.Count - 1].numCidade != grafoAtual[j].numCidade)
                            {
                                grafoAtual[grafoAtual.Count - 1].adjacencia.Add(grafoAtual[j].numCidade);
                                grafoAtual[j].adjacencia.Add(grafoAtual[grafoAtual.Count - 1].numCidade);
                            }

                        }
                    }
                }
            }
            grafoAtual = ajustarCor(grafoAtual);
            Console.WriteLine("\nGrafo gerado!");
            return grafoAtual;
        }
        //--------------------------------------------------------------------
        public bool verificarAdjBool(List<Vertice> vertices, int vertice1, int vertice2)
        {
            for (int i = 0; i < vertices[vertice1 - 1].adjacencia.Count; i++)
                if (vertices[vertice1 - 1].adjacencia[i] == vertice2)
                    return true;
            return false;
        }
        public List<int> removerAdjacencia(List<int> adjacentes, int numeroCid)
        {
            for (int i = 0; i < adjacentes.Count; i++)
                if (adjacentes[i] == numeroCid)
                    adjacentes.RemoveAt(i);
            return adjacentes;
        }
        public int[] confereCidades(int quantVert)
        {
            int[] cidades = new int[2];
            Console.WriteLine("Informe o número da primeira cidade:");
            cidades[0] = int.Parse(Console.ReadLine());

            //CORREÇÃO AO USUÁRIO
            while (cidades[0] < 1 || cidades[0] > quantVert)
            {
                Console.WriteLine("Cidade inválida!");
                Console.WriteLine("Informe o número da primeira cidade:");
                cidades[0] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Informe o número da segunda cidade:");
            cidades[1] = int.Parse(Console.ReadLine());

            //CORREÇÃO AO USUÁRIO
            while (cidades[1] < 1 || cidades[1] > quantVert || cidades[0] == cidades[1])
            {
                Console.WriteLine("Cidade inválida!");
                Console.WriteLine("Informe o número da segunda cidade:");
                cidades[1] = int.Parse(Console.ReadLine());
            }
            return cidades;
        }
        public void cidadesAdjacentes(List<Vertice> grafo, int posic)
        {
            ajustarNumGrafo(grafo, grafo[posic].numCidade);
            for (int i = 0; i < grafo[posic].adjacencia.Count; i++)
            {
                getCor(grafo[grafo[posic].adjacencia[i] - 1].numCorVertice);    //  MOSTRAR COR DAS CIDADES ADJACENTES
                Console.Write($"{grafo[grafo[posic].adjacencia[i] - 1].nomeCidade}"); // MOSTRAR NOME DAS CIDADES ADJACENTES
                Console.ResetColor();
                Console.Write(", ");
            }
        }
        public void exibirCidades(List<Vertice> grafo)
        {
            Console.WriteLine("\nCidades:  ");
            for (int i = 0; i < grafo.Count; i++)
            {
                getCor(grafo[i].numCorVertice);    //  MOSTRAR A CIDADE COM A SUA COR ESPECÍFICA
                Console.WriteLine($"{grafo[i].numCidade} -- {grafo[i].nomeCidade}");
                Console.ResetColor();
            }
            Console.WriteLine("\n");
        }
        public List<Vertice> ajustarNumGrafo(List<Vertice> grafo, int numeroCidade)
        {
            for (int i = grafo.Count - 1; i >= 0; i--)
            {
                grafo[i].adjacencia.Sort();
                if (grafo[i].numCidade > grafo.Count)
                    grafo[i].numCidade--;
                if (i != grafo.Count - 1 && grafo[i].numCidade == grafo[i + 1].numCidade)
                    grafo[i].numCidade--;
                for (int j = grafo[i].adjacencia.Count - 1; j >= 0; j--)
                {
                    if (grafo[i].adjacencia[j] > grafo.Count || grafo[i].adjacencia[j] == grafo[i].numCidade)
                        grafo[i].adjacencia[j]--;
                    if (j != grafo[i].adjacencia.Count - 1 && grafo[i].adjacencia[j] == grafo[i].adjacencia[j + 1])
                        grafo[i].adjacencia[j]--;
                }

            }
            return grafo;
        }
        public List<Vertice> ajustarCor(List<Vertice> grafo)
        {
            for (int i = 0; i < grafo.Count; i++)
                grafo[i].numCorVertice = 0;              // ATRIBUI TODAS AS CORES A COR "0"
            for (int i = 0; i < grafo.Count; i++)
                for (int j = 0; j < grafo[i].adjacencia.Count; j++)
                    if (grafo[i].numCorVertice == grafo[grafo[i].adjacencia[j] - 1].numCorVertice)// CASO A COR SEJA A MESMA DO VERTICE ADJACENTE
                        grafo[grafo[i].adjacencia[j] - 1].numCorVertice++;    // O NÚMERO DA COR DA CIDADE ADJ AUMENTA 1 
            return grafo;
        }
        public static void getCor(int cor)
        {
            cor++;
            foreach (var elemento in Enum.GetValues(typeof(ConsoleColor))) 
            {
                if((int)elemento == cor) {
                    Console.ForegroundColor = (ConsoleColor)elemento;
                }
            }
        }
        public static void leArquivoTxt(string caminho)
        {
            StreamReader reader = new StreamReader(caminho);
            string line;
            int counter = 0;

            while((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
                counter++;
            }

        }
    }
}