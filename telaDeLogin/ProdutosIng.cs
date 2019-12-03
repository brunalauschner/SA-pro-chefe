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
    public partial class ProdutosIng : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        ingrediente selected = new ingrediente();

        public ProdutosIng()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            button1.Click += salvarIngrediente;
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            btnProdutosIdentificacaoIng.Click += abrirTelaID;
            button2.Click += voltarTela;
            carregaTabelaIng();
            button3.Click += alterarIng;
            button4.Click += excluirIng;
            textBox3.Visible = false;
        }



        private void excluirIng(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Essa ação irá retirar o ingrediente escolhido da lista de ingredientes dos produtos que o continham. Deseja continuar?", "Confirmação", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                MessageBox.Show("a");

                int excluir = Convert.ToInt32(textBox3.Text);
                ingrediente excluirI = new ingrediente();

                produto pro = new produto();

                bd.ingrediente.ToList().ForEach(f =>
                {
                    if (f.id == excluir)
                    {
                        excluirI = f;
                        f.produto.Remove(pro);
                    }
                });

                bd.ingrediente.Remove(excluirI);
                bd.SaveChanges();
                ClearData();
                carregaTabelaIng();
                MessageBox.Show("Excluído com sucesso");
            }
            else if (dialog == DialogResult.No)
            {
                MessageBox.Show("Produto não excluído");
            }
        }

        private void alterarIng(object sender, EventArgs e)
        {
            string novoValorNome = textBox1.Text;
            string novoValorObs = textBox2.Text;

            int alterar = Convert.ToInt32(textBox3.Text);

            ingrediente alterarI = new ingrediente();
            bd.ingrediente.ToList().ForEach(f =>
            {
                if (f.id == alterar)
                {
                    f.nome = novoValorNome;
                    f.observacoes = novoValorObs;

                    bd.SaveChanges();
                }
            });
            ClearData();
            carregaTabelaIng();
        }

        private void carregaTabelaIng()
        {
            dataGridView1.Rows.Clear();
            bd.ingrediente.ToList().ForEach(a =>
            {
                dataGridView1.Rows.Add(a.id, a.nome, a.observacoes);
            });
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void abrirTelaID(object sender, EventArgs e)
        {
            new ProdutosID().Show();
            this.Visible = false;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearData();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void salvarIngrediente(object sender, EventArgs e)
        {
            ingrediente novo = new ingrediente()
            {
                nome = textBox1.Text,
                observacoes = textBox2.Text
            };
            bd.ingrediente.Add(novo);
            bd.SaveChanges();
            ClearData();
            carregaTabelaIng();
        }









        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProdutosIng_Load(object sender, EventArgs e)
        {

        }
    }
}
