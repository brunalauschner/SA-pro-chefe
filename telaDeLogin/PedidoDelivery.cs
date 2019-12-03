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
    public partial class PedidoDelivery : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        List<produto> produtos = new List<produto>();
        produto pesquisado = new produto();
        categoria pesquisada = new categoria();
        public PedidoDelivery()
        {
            InitializeComponent();
            carregaTblProdutos();
            carregaCategorias();
            button1.Click += pesquisarProduto;
            button2.Click += incluirProduto;
            button3.Click += voltar;
            lblUser.Text += TelaLogin.logado.nome;
            textBox1.Text = "";
            button4.Click += chamarInfoCli;
            button5.Click += mostrarTudo;
        }

        private void mostrarTudo(object sender, EventArgs e)
        {
            carregaTblProdutos();
            comboBox1.Text = "";
            textBox1.Text = "";
        }

        private void chamarInfoCli(object sender, EventArgs e)
        {
            new infoClientes().Show();
        }

        private void voltar(object sender, EventArgs e)
        {
            new Operacional().Show();
            this.Hide();
        }

        private void pesquisarProduto(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                pesquisado = bd.produto.Where(u => u.nome.Equals(textBox1.Text)).FirstOrDefault();
                if (pesquisado != null)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(pesquisado.id, pesquisado.nome, pesquisado.descricao, pesquisado.preco);
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Produto não encontrado");
                    textBox1.Text = "";
                }
            }
            else
            {
                pesquisada = bd.categoria.Where(u => u.id.Equals(comboBox1.SelectedIndex + 1)).FirstOrDefault();
                if (pesquisada != null)
                {
                    dataGridView1.Rows.Clear();
                    bd.produto.ToList().ForEach(m =>
                    {
                        if (m.categoria == pesquisada.id)
                        {
                            dataGridView1.Rows.Add(m.id, m.nome, m.descricao, m.preco);
                        }
                    });
                    textBox1.Text = "";
                    comboBox1.Text = "";

                }
                else
                {
                    MessageBox.Show("Nenhum produto encontrado nesta categoria.");
                    textBox1.Text = "";
                    comboBox1.Text = "";
                }
            }
        }

        private void incluirProduto(object sender, EventArgs e)
        {
            int idProduto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            produto p = bd.produto.Find(idProduto);
            int quant = Convert.ToInt32(numericUpDown1.Value);
            double tota = 0;
            int idcliente = 0;

            bd.clients.ToList().ForEach(f =>
            {
                if (f.telefone == maskedTextBox1.Text)
                {
                    idcliente = f.id_cliente;
                }
            });
            if (idcliente != 0)
            {
                pedido novo = new pedido()
                {
                    data = DateTime.Now,
                    id_cliente_ped = idcliente,
                    finalizado = false
                };
                bd.pedido.Add(novo);
                bd.SaveChanges();

                itens_pedido novin = new itens_pedido()
                {
                    id_pedido = novo.id,
                    id_produto = p.id,
                    quantidade = quant
                };
                bd.itens_pedido.Add(novin);
                bd.SaveChanges();
                bd.itens_pedido.ToList().ForEach(m =>
                {
                    if (m.id_pedido == novo.id)
                    {
                        if (m.id_produto == m.produto.id)
                        {
                            tota = (m.produto.preco * m.quantidade);
                        }
                    }

                });
                MessageBox.Show("Total do pedido: " + tota.ToString());
                MessageBox.Show("Pedido Delivery inserido com sucesso");
                numericUpDown1.Value = 0;
                maskedTextBox1.Text = null;
            }
            else
            {
                MessageBox.Show("O cliente não está cadastrado no sistema. Cadastre o cliente para prosseguir.");
            }
        }

        private void carregaTblProdutos()
        {
            dataGridView1.Rows.Clear();
            bd.produto.ToList().ForEach(m =>
            {
                dataGridView1.Rows.Add(m.id, m.nome, m.descricao, m.preco);
            });
        }
        private void carregaCategorias()
        {
            comboBox1.Items.Clear();
            bd.categoria.ToList().ForEach(m =>
            {
                comboBox1.Items.Add(m.nome);
            });
            //comboBox1.DataSource = bd.categoria.Select(c => c.nome).ToList();
        }
    }
}
