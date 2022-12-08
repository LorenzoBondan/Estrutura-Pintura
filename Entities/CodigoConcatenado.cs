using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concatenar
{
    class CodigoConcatenado
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Cor { get; set; }
        public int Medida1 { get; set; }
        public int Medida2 { get; set; }
        public int Medida3 { get; set; }

        public CodigoConcatenado(string codigo, string descricao, string cor, int medida1, int medida2, int medida3)
        {
            Codigo = codigo;
            Descricao = descricao;
            Cor = cor;
            Medida1 = medida1;
            Medida2 = medida2;
            Medida3 = medida3;
        }


        public CodigoConcatenado() { }


    }
}
