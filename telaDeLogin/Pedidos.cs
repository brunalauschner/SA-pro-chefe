using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using telaDeLogin.helpers;

namespace telaDeLogin
{
    public partial class Pedidos : Form
    {
        mesa selecionada;
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        List<produto> produtos = new List<produto>();
        double tota = 0.0;
        produto pesquisado = new produto();
        produto pesquisados = new produto();
        categoria pesquisada = new categoria();
        public Pedidos()
        {
            InitializeComponent();
        }

        public Pedidos(mesa mesaSelecionada)
        {
            selecionada = mesaSelecionada;
            InitializeComponent();
            label1.Text = $"Mesa {selecionada.id}";
            button1.Click += voltar;
            carregaCategorias();
            carregaTblProdutos();
            button2.Click += pesquisarProduto;
            button3.Click += incluirProduto;
            button4.Click += abrirTelaResumo;
            button1.Click += voltarTela;
            lblUser.Text += TelaLogin.logado.nome;
            button5.Click += mostrarTudo;
            totalMesaPed();
            textBox1.Text = "";
            checkBox1.Click += ocuparMesa;
            verCheck();
        }

        private void verCheck()
        {
            bd.mesa.ToList().ForEach(p =>
            {
                if (p.id == selecionada.id)
                {
                    if (p.disponivel == false)
                    {
                        checkBox1.Checked = true;
                    }
                }
            });
        }

        private void ocuparMesa(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bd.mesa.ToList().ForEach(p =>
                {
                    if (p.id == selecionada.id)
                    {
                        p.disponivel = false;
                        selecionada.disponivel = false;
                    }
                });
            }
            else
            {
                bd.mesa.ToList().ForEach(p =>
                {
                    if (p.id == selecionada.id)
                    {
                        p.disponivel = true;
                        selecionada.disponivel = true;
                    }
                });
            }
            bd.SaveChanges();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void mostrarTudo(object sender, EventArgs e)
        {
            carregaTblProdutos();
            comboBox1.Text = "";
            textBox1.Text = "";
        }

        private void totalMesaPed()
        {
            tota = 0.0;
            bd.itens_pedido.ToList().ForEach(m =>
            {
                if (m.pedido.id_mesa == selecionada.id)
                {
                    if (m.id_produto == m.produto.id)
                    {
                        if (m.pedido.finalizado == false)
                        {
                            tota = (m.produto.preco * m.quantidade) + tota;
                        }
                    }
                }

            });

            label2.Text = "Total da Mesa: " + tota.ToString();
        }
        private void voltarTela(object sender, EventArgs e)
        {
            if (!FormsClass.aberto("Operacional"))
            {
                new Operacional().Show();
            }
            this.Hide();
        }

        private void abrirTelaResumo(object sender, EventArgs e)
        {
            new ResumoMesa(selecionada).Show();
            this.Hide();
        }

        private void incluirProduto(object sender, EventArgs e)
        {
            int idProduto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            produto p = bd.produto.Find(idProduto);
            int quant = Convert.ToInt32(numericUpDown1.Value);

            pedido novo = new pedido()
            {
                data = DateTime.Now,
                id_mesa = selecionada.id,
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
            MessageBox.Show("Pedido inserido com sucesso");
            totalMesaPed();
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

                }
                else
                {
                    MessageBox.Show("Produto não encontrado");
                    textBox1.Text = "";
                }
            }
            comboBox1.Text = "";
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
        }

        private void voltar(object sender, EventArgs e)
        {
            if (!FormsClass.aberto("Operacional"))
            {
                new Operacional().Show();
            }
            this.Hide();
        }
    }
}
