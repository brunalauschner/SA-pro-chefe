using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telaDeLogin
{
    public partial class NovoUsuario : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        List<tipo_usuario> tipos;

        public NovoUsuario()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            tipos = bd.tipo_usuario.ToList();
            button1.Click += salvarUsuario;
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            button2.Click += alterarUsuario;
            button3.Click += excluirUsuario;
            carregarTblUsuario();
            button4.Click += voltarTela;
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void excluirUsuario(object sender, EventArgs e)
        {
            int excluir = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            usuario excluirU = new usuario();

            if (TelaLogin.logado.id == excluir)
            {
                bd.usuario.ToList().ForEach(f =>
                {
                    if (f.id == excluir)
                    {
                        excluirU = f;
                    }

                });
                bd.usuario.Remove(excluirU);
                bd.SaveChanges();
                carregarTblUsuario();
                ClearData();
                MessageBox.Show("Excluído com sucesso!");
                new TelaLogin().Show();
                this.Hide();
            }
            else
            {
                bd.usuario.ToList().ForEach(f =>
                {
                    if (f.id == excluir)
                    {
                        excluirU = f;
                    }

                });
                bd.usuario.Remove(excluirU);
                bd.SaveChanges();
                carregarTblUsuario();
                ClearData();
                MessageBox.Show("Excluído com sucesso!");
            }
        }

        private void carregarTblUsuario()
        {
            dataGridView1.Rows.Clear();
            bd.usuario.ToList().ForEach(m =>
            {
                if (m.tipo_usuario.Equals(1))
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.login, m.senha, "Administrador");
                }
                if (m.tipo_usuario.Equals(2))
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.login, m.senha, "Cliente");
                }
                if (m.tipo_usuario.Equals(3))
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.login, m.senha, "Garçom");
                }
                if (m.tipo_usuario.Equals(4))
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.login, m.senha, "Entregador");
                }
            });
        }

        private void alterarUsuario(object sender, EventArgs e)
        {
            string novoValorNome = textBox1.Text;
            string novoValorLogin = textBox2.Text;
            string novoValorSenha = textBox3.Text;
            int novoValorTipo = Convert.ToInt32(comboBox1.SelectedIndex+1);

            int alterar = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            usuario alterarU = new usuario();
            bd.usuario.ToList().ForEach(f =>
            {
                if (f.id == alterar)
                {
                    f.nome = novoValorNome;
                    f.login = novoValorLogin;
                    f.senha = novoValorSenha;
                    f.tipo_usuario = novoValorTipo;

                    dataGridView1.SelectedRows[0].Cells[1].Value = f.nome;
                    dataGridView1.SelectedRows[0].Cells[2].Value = f.login;
                    dataGridView1.SelectedRows[0].Cells[3].Value = f.senha;
                    dataGridView1.SelectedRows[0].Cells[4].Value = f.tipo_usuario;

                    bd.SaveChanges();
                }
            });
            carregarTblUsuario();
            ClearData();
            MessageBox.Show("Alterado com sucesso!");
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearData();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }

        private void salvarUsuario(object sender, EventArgs e)
        {
            tipo_usuario selecionado = tipos.ElementAt((comboBox1.SelectedIndex));

            usuario novo = new usuario() {
                nome = textBox1.Text,
                login = textBox2.Text,
                senha = textBox3.Text,
                tipo_usuario = selecionado.id
            };
            bd.usuario.Add(novo);
            bd.SaveChanges();
            carregarTblUsuario();
            ClearData();
            MessageBox.Show("Salvo com sucesso!");
        }
    }
}
