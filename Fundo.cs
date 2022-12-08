using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concatenar
{
    public class Fundo
    {
        public long Codigo { get; set; }
        public string Descricao { get; set; }

        public Fundo() { }

        public Fundo(long codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        //public HashSet<Fundo> CodigosFundos = new HashSet<Fundo>();

        public int RetirarMedida1(string descricao)
        {
            // descricao
            string descricaoCor = descricao.ToString().Trim();
            string novadescricaoCor = descricaoCor.Replace("+ COR", "+COR").Trim(); // HÁ DUAS POSSIBILIDADES: "+COR" OU "+ COR"
            novadescricaoCor = novadescricaoCor.Replace("+COR", " - ").Trim();

            // cor
            int posicaoTraco = novadescricaoCor.LastIndexOf("-");
            string cor = novadescricaoCor.Substring(posicaoTraco + 1).Trim();

            // medidas
            int medida1 = 0;
            int medida2 = 0;
            int medida3 = 0;

            for (int posicao = 0; posicao < novadescricaoCor.Length; posicao++)
            {
                string caracter = novadescricaoCor.Substring(posicao, 1);
                if (caracter.All(char.IsDigit) && novadescricaoCor.Substring(posicao + 2, 1) == "X")
                {
                    string EntradaMedidas = novadescricaoCor.Substring(posicao - 2).Trim();
                    int EspacoSaidaMedidas = EntradaMedidas.IndexOf(" ");
                    string Medidas = EntradaMedidas.Substring(0, EspacoSaidaMedidas).Trim();

                    // medida 1 
                    int primeiroX = Medidas.IndexOf("X");
                    medida1 = int.Parse(Medidas.Substring(0, primeiroX).Trim());

                    // medida 2 
                    int segundoX = Medidas.LastIndexOf("X");
                    medida2 = int.Parse(Medidas.Substring(primeiroX + 1, segundoX - primeiroX - 1).Trim());

                    // medida 3
                    medida3 = int.Parse(Medidas.Substring(segundoX + 1).Trim());

                    break;
                }
            }
            return medida1;
        }

        public int RetirarMedida2(string descricao)
        {
            // descricao
            string descricaoCor = descricao.ToString().Trim();
            string novadescricaoCor = descricaoCor.Replace("+ COR", "+COR").Trim(); // HÁ DUAS POSSIBILIDADES: "+COR" OU "+ COR"
            novadescricaoCor = novadescricaoCor.Replace("+COR", " - ").Trim();

            // cor
            int posicaoTraco = novadescricaoCor.LastIndexOf("-");
            string cor = novadescricaoCor.Substring(posicaoTraco + 1).Trim();

            // medidas
            int medida1 = 0;
            int medida2 = 0;
            int medida3 = 0;

            for (int posicao = 0; posicao < novadescricaoCor.Length; posicao++)
            {
                string caracter = novadescricaoCor.Substring(posicao, 1);
                if (caracter.All(char.IsDigit) && novadescricaoCor.Substring(posicao + 2, 1) == "X")
                {
                    string EntradaMedidas = novadescricaoCor.Substring(posicao - 2).Trim();
                    int EspacoSaidaMedidas = EntradaMedidas.IndexOf(" ");
                    string Medidas = EntradaMedidas.Substring(0, EspacoSaidaMedidas).Trim();

                    // medida 1 
                    int primeiroX = Medidas.IndexOf("X");
                    medida1 = int.Parse(Medidas.Substring(0, primeiroX).Trim());

                    // medida 2 
                    int segundoX = Medidas.LastIndexOf("X");
                    medida2 = int.Parse(Medidas.Substring(primeiroX + 1, segundoX - primeiroX - 1).Trim());

                    // medida 3
                    medida3 = int.Parse(Medidas.Substring(segundoX + 1).Trim());

                    break;
                }
            }
            return medida2;

        }

        public int RetirarMedida3(string descricao)
        {
            // descricao
            string descricaoCor = descricao.ToString().Trim();
            string novadescricaoCor = descricaoCor.Replace("+ COR", "+COR").Trim(); // HÁ DUAS POSSIBILIDADES: "+COR" OU "+ COR"
            novadescricaoCor = novadescricaoCor.Replace("+COR", " - ").Trim();

            // cor
            int posicaoTraco = novadescricaoCor.LastIndexOf("-");
            string cor = novadescricaoCor.Substring(posicaoTraco + 1).Trim();

            // medidas
            int medida1 = 0;
            int medida2 = 0;
            int medida3 = 0;

            for (int posicao = 0; posicao < novadescricaoCor.Length; posicao++)
            {
                string caracter = novadescricaoCor.Substring(posicao, 1);
                if (caracter.All(char.IsDigit) && novadescricaoCor.Substring(posicao + 2, 1) == "X")
                {
                    string EntradaMedidas = novadescricaoCor.Substring(posicao - 2).Trim();
                    int EspacoSaidaMedidas = EntradaMedidas.IndexOf(" ");
                    string Medidas = EntradaMedidas.Substring(0, EspacoSaidaMedidas).Trim();

                    // medida 1 
                    int primeiroX = Medidas.IndexOf("X");
                    medida1 = int.Parse(Medidas.Substring(0, primeiroX).Trim());

                    // medida 2 
                    int segundoX = Medidas.LastIndexOf("X");
                    medida2 = int.Parse(Medidas.Substring(primeiroX + 1, segundoX - primeiroX - 1).Trim());

                    // medida 3
                    medida3 = int.Parse(Medidas.Substring(segundoX + 1).Trim());

                    break;
                }
            }
            return medida3;

        }
    }
}
