using Concatenar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Globalization;

namespace Concatenar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            #region CUSTOMIZAÇÃO DO DATAGRIDVIEW

            // linhas alternadas
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            dataExport.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            // linha selecionada
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.White;
            dataExport.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            dataExport.DefaultCellStyle.SelectionForeColor = Color.White;

            // fonte
            //dataGridView2.DefaultCellStyle.Font = new Font("Century Gothic",8);

            // bordas
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataExport.CellBorderStyle = DataGridViewCellBorderStyle.None;

            // cabeçalho
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8, FontStyle.Bold);
            dataExport.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8, FontStyle.Bold);

            dataGridView2.EnableHeadersVisualStyles = false; // habilita a edição do cabeçalho
            dataExport.EnableHeadersVisualStyles = false;

            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(211, 84, 21);
            dataExport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(211, 84, 21);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataExport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            #endregion
        }

        string dataImplantacao;

        public List<string> PinturasAltoBrilho = new List<string>();
        public List<string> PinturasAcetinadas = new List<string>();

        public List<string> PinturasAcetinadasVidros = new List<string>();

        public List<string> PinturasAltoBrilhoPaineis = new List<string>();
        public List<string> PinturasAcetinadasPaineis = new List<string>();

        // codigos dos fundos
        public HashSet<Fundo> CodigosFundos = new HashSet<Fundo>();

        public List<string> ChapasMDF = new List<string>();
        public string faces;

        private void btnCarregarCSV_Click(object sender, EventArgs e)
        {
            // VERIFICAÇÃO
            if (rbAcetinada.Checked == false && rbAltoBrilho.Checked == false && rbAcetinadaVidros.Checked == false)
            {
                MessageBox.Show("Selecione Alto Brilho ou Acetinada.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rb1Face.Checked == false && rb2Faces.Checked == false)
            {
                MessageBox.Show("Selecione 1 ou 2 Faces.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ###
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dataExport.Rows.Clear();
                dataGridView2.Rows.Clear();

                PinturasAltoBrilho.Clear();
                PinturasAcetinadas.Clear();

                PinturasAcetinadasVidros.Clear();

                PinturasAltoBrilhoPaineis.Clear();
                PinturasAcetinadasPaineis.Clear();

                ChapasMDF.Clear();

                CodigosFundos.Clear();

                try
                {
                    string filename = openFileDialog1.FileName;
                    Concatenar(filename);

                    TrazerCodigoPintura();
                    TrazerCodigoPinturaAcetinada();

                    if (rbAcetinadaVidros.Checked)
                    {
                        TrazerCodigoPinturaAcetinadaVidro();
                    }

                    MontarArquivo();

                    MessageBox.Show("Procedimento finalizado com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Concatenar(string filePath)
        {
            CodigosFundos.Clear();

            DataTable datatable = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(filePath);

            string primeiralinha = lines[0];
            string[] nomesCores = primeiralinha.Split(';');

            int colunas = nomesCores.Length;

            string segundalinha = lines[1];
            string[] codigosCores = segundalinha.Split(';');

            List<CodigoConcatenado> list = new List<CodigoConcatenado>();

            for (int i = 2; i < lines.Length; i++) // <<< ---- antes tinha colunas, o certo é linhas
            {
                //if (i == lines.Length)
                //{
                //    break;
                //}
                string[] dataWords = lines[i].Split(';');

                // ADICIONANDO A REFERENCIA NA LISTA PARA JUNTÁ-LA COM A REFERENCIA DO FUNDO
                long referencia = long.Parse(dataWords[0].ToString());
                string descricao = dataWords[1].ToString();
                Fundo fundo = new Fundo(referencia,descricao);
                CodigosFundos.Add(fundo);
                //

                for (int j = 2; j < codigosCores.Length; j++) // essas são as colunas
                {
                    // codigo
                    string codigoCor = dataWords[0].ToString() + codigosCores[j].ToString();
                    

                    // descricao
                    string descricaoCor = dataWords[1].ToString() + nomesCores[j].ToString().Trim();
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

                    list.Add(new CodigoConcatenado(codigoCor, novadescricaoCor, cor, medida1, medida2, medida3));
                    
                }
                
            }

            foreach (CodigoConcatenado item in list)
            {
                dataGridView2.Rows.Add(item.Codigo, item.Descricao, item.Cor, item.Medida1, item.Medida2, item.Medida3);
            }

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dataExport.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataExport.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dataExport.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataExport.Rows.Count; i++)
                {
                    for (int j = 0; j < dataExport.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dataExport.Rows[i].Cells[j].Value;
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }

        private void btnExemplo_Click(object sender, EventArgs e)
        {
            frmExemploImportacao f = new frmExemploImportacao();
            f.Show();
        }

        private void btnBancoPintura_Click(object sender, EventArgs e)
        {
            frmBancoPintura f = new frmBancoPintura();
            f.Show();
        }

        public void TrazerCodigoPintura()
        {
            string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);
            conexao.Open();

            try
            {
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    string cor = linha.Cells[2].Value.ToString();
                    if (cor.LastIndexOf(" ") > 0)
                    {
                        cor = cor.Substring(0, cor.LastIndexOf(" "));
                    }

                    string query = "SELECT codigo FROM tabelaPinturaBrilhante1F WHERE cor = '" + cor + "'  ";

                    SqlCeCommand comando = new SqlCeCommand(query, conexao);

                    string corPintura = (string)comando.ExecuteScalar();

                    // se não encontrar, é pq é tintometrico
                    if (corPintura == null)
                    {
                        cor = cor + " TINTOMETRICO";

                        query = "SELECT codigo FROM tabelaPinturaBrilhante1F WHERE cor = '" + cor + "'  ";

                        comando = new SqlCeCommand(query, conexao);

                        corPintura = (string)comando.ExecuteScalar();
                    }
                    //linha.Cells[6].Value = corPintura;
                    PinturasAltoBrilho.Add(corPintura);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            // << 
            //int i = 0;
            //foreach (string p in PinturasAltoBrilho)
            //{
            //    dataGridView2[7,i].Value = p;
            //    i++;
            //}
        }

        public void TrazerCodigoPinturaAcetinada()
        {
            string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);
            conexao.Open();

            try
            {
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    string cor = linha.Cells[2].Value.ToString();
                    if (cor.LastIndexOf(" ") > 0)
                    {
                        cor = cor.Substring(0, cor.LastIndexOf(" "));
                    }

                    string query = "SELECT codigo FROM tabelaPinturaAcetinada1F WHERE cor = '" + cor + "'  ";

                    SqlCeCommand comando = new SqlCeCommand(query, conexao);

                    string corPintura = (string)comando.ExecuteScalar();

                    // se não encontrar, é pq é tintometrico
                    if (corPintura == null)
                    {
                        cor += " TINTOMETRICO";

                        query = "SELECT codigo FROM tabelaPinturaAcetinada1F WHERE cor = '" + cor + "'  ";

                        comando = new SqlCeCommand(query, conexao);

                        corPintura = (string)comando.ExecuteScalar();
                    }

                    if (cor == "BIANCO" && rbAltoBrilho.Checked && rb2Faces.Checked)
                    {
                        // regra criada pois bianco 2F usa 79992133
                        corPintura = "79992133";
                    }

                    //linha.Cells[7].Value = corPintura;
                    PinturasAcetinadas.Add(corPintura);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void TrazerCodigoPinturaAcetinadaVidro()
        {
            string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);
            conexao.Open();

            try
            {
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    string cor = linha.Cells[2].Value.ToString();
                    if (cor.LastIndexOf(" ") > 0)
                    {
                        cor = cor.Substring(0, cor.LastIndexOf(" "));
                    }

                    string query = "SELECT codigo FROM tabelaPinturaAcetinadaVidros1F WHERE cor = '" + cor + "'  ";

                    SqlCeCommand comando = new SqlCeCommand(query, conexao);

                    string corPintura = (string)comando.ExecuteScalar();

                    // se não encontrar, é pq é tintometrico
                    if (corPintura == null)
                    {
                        cor += " TINTOMETRICO";

                        query = "SELECT codigo FROM tabelaPinturaAcetinadaVidros1F WHERE cor = '" + cor + "'  ";

                        comando = new SqlCeCommand(query, conexao);

                        corPintura = (string)comando.ExecuteScalar();
                    }

                    //linha.Cells[7].Value = corPintura;
                    PinturasAcetinadasVidros.Add(corPintura);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }


        public void MontarArquivo()
        {
            dataImplantacao = Interaction.InputBox("Data de implantação:", ""); // dialog result

            if (dataImplantacao == "")
            {
                return;
            }

            try
            {
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    string complementoFilho = "";
                    // 1F -> 99 | 2F -> 55
                    if (rb1Face.Checked)
                    {
                        complementoFilho = "99";
                        faces = "1";
                        if (rbEspecial.Checked)
                        {
                            complementoFilho = "77";
                        }
                    }
                    else if (rb2Faces.Checked)
                    {
                        complementoFilho = "55";
                        faces = "2";
                        if (rbEspecial.Checked)
                        {
                            complementoFilho = "44";
                        }
                    }

                    // ### ALTO BRILHO
                    if (rbAltoBrilho.Checked)
                    {
                        // seq 10 -> referencia + 99
                        string codigopai = linha.Cells[0].Value.ToString();
                        int seq = 10;
                        string codigofilho = codigopai.Substring(0, 6) + complementoFilho;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "n", 0, 100, "", "", 1, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // seq 20 -> trazer o codigo da pintura equivalente
                        //codigofilho = linha.Cells[6].Value.ToString();
                        codigofilho = PinturasAltoBrilho[linha.Index].ToString();
                        seq += 10;
                        double medida1 = double.Parse(linha.Cells[3].Value.ToString());
                        double medida2 = double.Parse(linha.Cells[4].Value.ToString());
                        double medida3 = double.Parse(linha.Cells[5].Value.ToString());
                        double qte = medida1 / 1000 * medida2 / 1000;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // 2F -> 2ª Pintura
                        if (rb2Faces.Checked)
                        {
                            codigofilho = PinturasAcetinadas[linha.Index].ToString();
                            seq += 10;
                            dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);
                        }

                        // seq final -> pintura borda fundo - 7999099
                        codigofilho = "7999099";
                        seq += 10;
                        qte = (medida1 / 1000 + medida2 / 1000) * 2 * (medida3 / 1000);
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        if (rbPoliester.Checked)
                        {
                            // ultima sequencia -> poliester - 79991099
                            codigofilho = "79991099";
                            seq += 10;
                            qte = medida1 / 1000 * medida2 / 1000;
                            dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);
                        }
                    }

                    // ### ACETINADA
                    if (rbAcetinada.Checked)
                    {
                        // seq 10 -> referencia + 99
                        string codigopai = linha.Cells[0].Value.ToString();
                        int seq = 10;
                        string codigofilho = codigopai.Substring(0, 6) + complementoFilho;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "n", 0, 100, "", "", 1, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // seq 20 -> trazer o codigo da pintura equivalente
                        //codigofilho = linha.Cells[6].Value.ToString();
                        codigofilho = PinturasAcetinadas[linha.Index].ToString();
                        seq += 10;
                        double medida1 = double.Parse(linha.Cells[3].Value.ToString());
                        double medida2 = double.Parse(linha.Cells[4].Value.ToString());
                        double medida3 = double.Parse(linha.Cells[5].Value.ToString());
                        double qte = medida1 / 1000 * medida2 / 1000;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // 2F -> 2ª Pintura
                        if (rb2Faces.Checked)
                        {
                            codigofilho = PinturasAcetinadas[linha.Index].ToString();
                            seq += 10;
                            dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);
                        }

                        // seq final -> pintura borda fundo - 7999099
                        codigofilho = "7999099";
                        seq += 10;
                        qte = (medida1 / 1000 + medida2 / 1000) * 2 * (medida3 / 1000);
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        if (rbPoliester.Checked)
                        {
                            // ultima sequencia -> poliester - 79991099
                            codigofilho = "79991099";
                            seq += 10;
                            qte = medida1 / 1000 * medida2 / 1000;
                            dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);
                        }
                    }

                    // ### VIDRO ACETINADA
                    if (rbAcetinadaVidros.Checked)
                    {
                        // seq 10 -> referencia + 99
                        string codigopai = linha.Cells[0].Value.ToString();
                        int seq = 10;
                        string codigofilho = codigopai.Substring(0, 6) + complementoFilho;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "n", 0, 100, "", "", 1, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // seq 20 -> trazer o codigo da pintura equivalente
                        //codigofilho = linha.Cells[6].Value.ToString();
                        codigofilho = PinturasAcetinadasVidros[linha.Index].ToString();
                        seq += 10;
                        double medida1 = double.Parse(linha.Cells[3].Value.ToString());
                        double medida2 = double.Parse(linha.Cells[4].Value.ToString());
                        double medida3 = double.Parse(linha.Cells[5].Value.ToString());
                        double qte = medida1 / 1000 * medida2 / 1000;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // seq 30 -> trazer o codigo da pintura equivalente
                        //codigofilho = linha.Cells[6].Value.ToString();
                        codigofilho = PinturasAcetinadasVidros[linha.Index].ToString();
                        seq += 10;
                        qte = medida1 / 1000 * medida2 / 1000;
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        // seq final -> pintura borda fundo - 7999099
                        codigofilho = "7999099";
                        seq += 10;
                        qte = (medida1 / 1000 + medida2 / 1000) * 2 * (medida3 / 1000);
                        dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                        if (rbPoliester.Checked)
                        {
                            // ultima sequencia -> poliester - 79991099
                            codigofilho = "79991099";
                            seq += 10;
                            qte = medida1 / 1000 * medida2 / 1000;
                            dataExport.Rows.Add(1, codigopai, seq, codigofilho, "", "s", 0, 100, "", "", qte, "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo está errado no arquivo de importação.\nNome de alguma cor ou falta o +COR na descrição de algum item.\n\n" + ex.Message);
            }
        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            new ExportHelper().ExportarCSV(dataExport);
            MessageBox.Show(@"Arquivo exportado para:" + "\n" + @"\\paris\eng\Engenharia de Produto\Codificação de Itens\ITENS NOVOS\ ","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBancoPinturaAcetinada1F_Click(object sender, EventArgs e)
        {
            frmBancoPinturaAC f = new frmBancoPinturaAC();
            f.Show();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnInstrucoes_Click(object sender, EventArgs e)
        {
            frmInstrucoes f = new frmInstrucoes();
            f.Show();
        }

        private void btnBancoPinturaCriare_Click(object sender, EventArgs e)
        {
            frmBancoPinturaVidro f = new frmBancoPinturaVidro();
            f.Show();
        }

        private void rbAcetinadaVidros_CheckedChanged(object sender, EventArgs e)
        {
            // só existe acetinada vidros com 2 Faces
            if (rbAcetinadaVidros.Checked)
            {
                rb2Faces.Checked = true;
                rb1Face.Enabled = false;
            }
            else
            {
                rb1Face.Enabled = true;
            }
            
        }

        private void btnEstruturaFundos_Click(object sender, EventArgs e)
        {
            if (dataExport.Rows.Count == 0)
            {
                MessageBox.Show("Carregue um arquivo CSV.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string p = Interaction.InputBox("Perda:\n\n(Utilize VÍRGULA ao invés de ponto para separar as casas decimais).",""); // dialog result

            if (p == "")
            {
                MessageBox.Show("Insira uma perda.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            double perda = double.Parse(p);

            dataExport.Rows.Clear();

            string complementoFilho = "";
            string codigofilho = "";
            // 1F -> 99 | 2F -> 55
            if (rb1Face.Checked)
            {
                complementoFilho = "99";
                faces = "1";
                if (rbEspecial.Checked)
                {
                    complementoFilho = "77";
                }
            }
            else if (rb2Faces.Checked)
            {
                complementoFilho = "55";
                faces = "2";
                if (rbEspecial.Checked)
                {
                    complementoFilho = "44";
                }
            }

            // procurando e trazendo o codigo da chapa sem criar um metodo antes

            string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);
            conexao.Open();

            try
            {
                foreach (Fundo item in CodigosFundos)
                {

                    string codigopai = item.Codigo.ToString() + complementoFilho;
                    string desc = item.Descricao.ToString();
                    int medida1 = item.RetirarMedida1(desc);
                    int medida2 = item.RetirarMedida2(desc);
                    int medida3 = item.RetirarMedida3(desc);
                    double qteliq = (double.Parse(medida1.ToString()) / 1000 * double.Parse(medida2.ToString()) / 1000);
                    double qte = qteliq / (qteliq / (qteliq) - perda / 100);

                    string query = "SELECT codigo FROM tabelaChapaMDF WHERE espessura = '" + medida3 + "' AND faces = '" + faces + "' ";

                    SqlCeCommand comando = new SqlCeCommand(query, conexao);

                    codigofilho = (string)comando.ExecuteScalar();

                    dataExport.Rows.Add(1, codigopai, 10, codigofilho, "", "n", perda, 100, "", "", qte.ToString("F10"), "?", dataImplantacao, "31/12/9999", "", 0, "", "", "", "", 4, 1, "N", 0);

                }

                MessageBox.Show("Estrutura dos fundos montada.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        private void btnExemploLifeColors_Click(object sender, EventArgs e)
        {
            frmExemploLifeColors f = new frmExemploLifeColors();
            f.Show();
        }

        private void btnChapaMDF_Click(object sender, EventArgs e)
        {
            frmChapaMDF f = new frmChapaMDF();
            f.Show();
        }
    }
}
