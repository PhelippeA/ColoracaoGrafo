using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoColoracaoGrafo.GrafoColoracao
{
    class Vertice
    {
        public string nomeCidade;
        public int numCidade, numCorVertice;
        public List<int> adjacencia;  
        public Vertice(string nomeCidade, int numCidade, int numCorVertice) 
        {
            this.nomeCidade = nomeCidade;
            this.numCidade = numCidade;
            this.numCorVertice = numCorVertice;
            adjacencia = new List<int>();              //   armazena o número refente a outra cidade
        }
    }
}
