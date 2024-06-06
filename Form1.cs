using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace MedidoPeso {
    public partial class Form1 : Form {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        private string stringConnection = "Server=DESKTOP-7KUBS2C\\SQLEXPRESS;Database=PESO_IMC;Trusted_Connection=True;";
        private string query;
        public Form1() {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e) {
            using (connection = new SqlConnection(stringConnection)) 

            try { 
                connection.Open();
                if(!string.IsNullOrEmpty(txtCNome.Text) && !string.IsNullOrEmpty(txtAltura.Text) && !string.IsNullOrEmpty(txtIdade.Text) && !string.IsNullOrEmpty(txtPeso.Text)
                    && !string.IsNullOrEmpty(txtMeta.Text) && !string.IsNullOrEmpty(txtSenha.Text)) {
                    query = "INSERT INTO USUARIO (NOME,PESO,ALTURA,IDADE,META,SENHA) VALUES " +
                        "(@NOME,@PESO,@ALTURA,@IDADE,@META,@SENHA)";
                    using (command = new SqlCommand(query, connection))
                    command.Parameters.AddWithValue("@NOME",txtCNome.Text);
                    command.Parameters.AddWithValue("@PESO",txtPeso.Text);
                    command.Parameters.AddWithValue("@ALTURA",txtAltura.Text);
                    command.Parameters.AddWithValue("@IDADE",txtIdade.Text);
                    command.Parameters.AddWithValue("@META",txtMeta.Text);
                    command.Parameters.AddWithValue("@SENHA",txtSenha.Text);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    MessageBox.Show("Usuario Cadastrado", "Sucesso!");

                }
                else {
                    MessageBox.Show("Preencha os campos vazios", "AVISO!");
                }

            }catch(Exception ex) {
                MessageBox.Show("Erro de conexão:" + ex.Message,"AVISO!");
            }
            finally{
                connection.Close();
                    txtCNome.Text = "";
                    txtAltura.Text = "";
                    txtIdade.Text = "";
                    txtMeta.Text = "";
                    txtPeso.Text = "";
                    txtSenha.Text = "";
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e) {
            query = "SELECT IDUSUARIO, NOME, SENHA FROM USUARIO WHERE NOME = @NOME AND SENHA = @SENHA";
            connection = new SqlConnection(stringConnection);
            command = new SqlCommand(query,connection);
            
           
            try {
                connection.Open();
                if(!string.IsNullOrEmpty(txtLNome.Text) && !string.IsNullOrEmpty(txtLSenha.Text)) {
                    command.Parameters.AddWithValue("@NOME", txtLNome.Text);
                    command.Parameters.AddWithValue("@SENHA", txtLSenha.Text);
                    command.ExecuteNonQuery();
                    adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach(DataRow rows in  table.Rows) {
                        if (rows["NOME"].ToString() == txtLNome.Text && rows["SENHA"].ToString() == txtLSenha.Text) {
                            Close();
                            Thread thread = new Thread(() => Application.Run(new Informacoes(uint.Parse(rows["IDUSUARIO"].ToString()))));
                            thread.Start();
                        }
                        else {
                            MessageBox.Show("Campos vazios", "AVISO!");
                        }

                    }





                }


            } catch (Exception ex) {

            } finally { connection.Close();
                command.Dispose();
            }
        }
    }
}
