using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telaDeLogin
{
    public partial class InfoProdutos : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        produto pesquisado = new produto();
        public InfoProdutos()
        {
            InitializeComponent();
            carregarTblProds();
            button2.Click += alterarProd;
            button1.Click += excluirProd;
            lblUser.Text += TelaLogin.logado.nome;
            button3.Click += voltarTela;
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            button4.Click += novaTela;
        }

        private void novaTela(object sender, EventArgs e)
        {
            new Alterar_produto_ingrediente().Show();
            this.Hide();
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new ProdutosID().Show();
            this.Hide();
        }

        private void excluirProd(object sender, EventArgs e)
        {
            int excluir = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            produto excluirP = new produto();
            itens_pedido abc = new itens_pedido();
            bd.itens_pedido.ToList().ForEach(q =>
            {
                if (q.id_produto == excluir)
                {
                    if (q.pedido.finalizado == false) // pedido nao finalizado
                    {
                        abc = q;
                    }
                }
            });
            if (abc.id_pedido > 0) // pedido tiver o produto e nao tiver finalizado
            {
                MessageBox.Show("Você não pode deletar um produto até que todos seus pedidos estejam finalizados. Pedido em aberto: " + abc.id_pedido.ToString());
            }
            else
            {
                ingrediente ingre = new ingrediente();

                bd.produto.ToList().ForEach(f =>
                {
                    if (f.id == excluir)
                    {
                        excluirP = f;
                        f.ingrediente.Remove(ingre);
                    }

                });
                bd.produto.Remove(excluirP);
                bd.SaveChanges();
                carregarTblProds();
                ClearData();
                MessageBox.Show("Excluído com sucesso!");
            }
        }

        private void alterarProd(object sender, EventArgs e)
        {
            string novoValorNome = textBox1.Text;
            string novoalorDescricao = textBox2.Text;
            int novoValorCategoria = Convert.ToInt32(comboBox1.SelectedIndex + 1);
            double novoValorPreco = Convert.ToDouble(textBox3.Text);

            int alterar = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            produto alterarP = new produto();
            bd.produto.ToList().ForEach(f =>
            {
                if (f.id == alterar)
                {
                    f.nome = novoValorNome;
                    f.descricao = novoalorDescricao;
                    f.categoria = novoValorCategoria;
                    f.preco = novoValorPreco;

                    dataGridView1.SelectedRows[0].Cells[1].Value = f.nome;
                    dataGridView1.SelectedRows[0].Cells[2].Value = f.descricao;
                    dataGridView1.SelectedRows[0].Cells[3].Value = f.categoria1;
                    dataGridView1.SelectedRows[0].Cells[4].Value = f.preco;

                    bd.SaveChanges();
                }
            });
            carregarTblProds();
            ClearData();
            MessageBox.Show("Alterado com sucesso!");
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearData();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void carregarTblProds()
        {
            dataGridView1.Rows.Clear();
            bd.produto.ToList().ForEach(m =>
            {
                if (m.categoria == 1)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Bebidas", m.preco);
                }
                if (m.categoria == 2)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Paes", m.preco);
                }
                if (m.categoria == 3)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Salgados", m.preco);
                }
                if (m.categoria == 4)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Lanches", m.preco);
                }
                if (m.categoria == 5)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Tortas", m.preco);
                }
                if (m.categoria == 6)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Bolos", m.preco);
                }
                if (m.categoria == 7)
                {
                    dataGridView1.Rows.Add(m.id, m.nome, m.descricao, "Doces", m.preco);
                }

            });
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
