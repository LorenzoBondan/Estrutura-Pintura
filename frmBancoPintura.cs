using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Concatenar
{
    public partial class frmBancoPintura  : MetroFramework.Forms.MetroForm
    {
        public frmBancoPintura()
        {
            InitializeComponent();

            #region CUSTOMIZAÇÃO DO DATAGRIDVIEW

            // linhas alternadas
            datagridBancoPintura.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            // linha selecionada
            datagridBancoPintura.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            datagridBancoPintura.DefaultCellStyle.SelectionForeColor = Color.White;

            // fonte
            //dataGridView2.DefaultCellStyle.Font = new Font("Century Gothic",8);

            // bordas
            datagridBancoPintura.CellBorderStyle = DataGridViewCellBorderStyle.None;

            // cabeçalho
            datagridBancoPintura.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8);

            datagridBancoPintura.EnableHeadersVisualStyles = false; // habilita a edição do cabeçalho

            datagridBancoPintura.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(211, 84, 21);
            datagridBancoPintura.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            datagridBancoPintura.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 84, 21);
            datagridBancoPintura.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            #endregion
        }

        private void frmBancoPintura_Load(object sender, EventArgs e)
        {
            //string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            //string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";
            //SqlCeEngine db = new SqlCeEngine(strConnection);
            //if (!File.Exists(baseDados))
            //{
            //    db.CreateDatabase();
            //}
            //db.Dispose();
            //SqlCeConnection conexao = new SqlCeConnection();
            //conexao.ConnectionString = strConnection;
            //try
            //{
            //    conexao.Open();
            //    SqlCeCommand comando = new SqlCeCommand();
            //    comando.Connection = conexao;

            //    // tive que colocar como string pois alguns codigos ultrapassavam o limite de INT
            //    comando.CommandText = "CREATE TABLE tabelaPinturaBrilhante1F (codigo NVARCHAR(20) NOT NULL PRIMARY KEY, cor NVARCHAR(100), descricao NVARCHAR(100))";
            //    comando.ExecuteNonQuery();

            //    label10.Text = "Tabela criada.";
            //}
            //catch (Exception ex)
            //{
            //    label10.Text = ex.Message;

            //}
            //finally
            //{
            //    conexao.Close();
            //}

            AtualizarTabela();
        }

        public void AtualizarTabela()
        {
            datagridBancoPintura.Rows.Clear();

            string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);

            try
            {
                string query = "SELECT * FROM tabelaPinturaBrilhante1F";


                DataTable dados = new DataTable();

                SqlCeDataAdapter adaptador = new SqlCeDataAdapter(query, strConnection);

                conexao.Open();

                adaptador.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                    datagridBancoPintura.Rows.Add(linha.ItemArray);
                }
            }
            catch (Exception ex)
            {
                datagridBancoPintura.Rows.Clear();
                label10.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnImportarCSVPinturas_Click(object sender, EventArgs e)
        {
            string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
            string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);
            var linenumber = 0;

            try
            {
                conexao.Open();

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog1.FileName;

                    using (StreamReader reader = new StreamReader(filename))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (linenumber != 0)
                            {
                                //int id = new Random((int)DateTime.Now.Ticks).Next(0,1000000) + 1;
                                var values = line.Split(';');
                                var sql = "INSERT INTO tabelaPinturaBrilhante1F VALUES (" + values[0].ToString().Trim() + " , '" + values[1].ToString().Trim() + "'  , '" + values[2].ToString().Trim() + "' )";
                                //var sql = "INSERT INTO tabelacadastroaluminios VALUES ('" + int.Parse(values[0].ToString()) + "' , '" + values[1].ToString().Trim() + "' , '" + values[2].ToString().Trim() + "' , '" + double.Parse(values[3],CultureInfo.InvariantCulture) + "' ,  '" + double.Parse(values[4],CultureInfo.InvariantCulture) + "'  )";

                                var cmd = new SqlCeCommand();
                                cmd.CommandText = sql;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Connection = conexao;

                                cmd.ExecuteNonQuery();

                            }
                            linenumber++;
                        }
                    }
                    MessageBox.Show("Produtos importados com sucesso!");
                }

                conexao.Close();
            }
            catch (Exception ex)
            {

                label10.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
                AtualizarTabela();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // PROPRIEDADES DO DATAGRID: FULLROWSELECT

            DialogResult dialogResult = MessageBox.Show("Deseja excluir o item selecionado?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                string baseDados = @"\\paris\eng\Usuarios\Lorenzo\BasePinturaSQLServer.sdf";
                string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

                SqlCeConnection conexao = new SqlCeConnection(strConnection);

                try
                {
                    conexao.Open();

                    SqlCeCommand comando = new SqlCeCommand();
                    comando.Connection = conexao;

                    string codigo = (string)datagridBancoPintura.SelectedRows[0].Cells[0].Value;

                    comando.CommandText = "DELETE FROM tabelaPinturaBrilhante1F WHERE codigo = '" + codigo + "'";
                    comando.ExecuteNonQuery();

                    comando.Dispose();

                    MessageBox.Show("Registro excluído do banco de dados!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                    AtualizarTabela();
                }
            }
        }
    }
}
