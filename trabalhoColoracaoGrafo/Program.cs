using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoColoracaoGrafo
{
    class Program
    {
        static void Main(string[] args)
        {
            //GrafoColoracao.Menu.menu();
            GrafoColoracao.Funcoes.leArquivoTxt("grafo5V.txt");

            Console.ReadKey();
        }
    }
}
