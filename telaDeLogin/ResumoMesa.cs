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
    public partial class ResumoMesa : Form
    {
        mesa selecionada;
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        double tota = 0.0;
        public ResumoMesa(mesa Mselecionada)
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            selecionada = Mselecionada;
            label3.Text = $"Mesa {selecionada.id}";
            carregaTblPedidos();
            button1.Click += voltarTela;
            totalMesaPed();
            button2.Click += finalizar;
            checkBox1.Click += carregaTbl;
        }

        private void carregaTbl(object sender, EventArgs e)
        {
            carregaTblPedidos();
        }

        private void finalizar(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            bd.pedido.ToList().ForEach(f =>
            {
                if (f.id == idPedido)
                {
                    f.finalizado = true;
                }
            });
            bd.SaveChanges();
            carregaTblPedidos();
            totalMesaPed();
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
            new Pedidos(selecionada).Show();
            this.Hide();
        }

        private void carregaTblPedidos()
        {
            dataGridView1.Rows.Clear();

            bd.itens_pedido.ToList().ForEach(m =>
            {
                if (m.pedido.id_mesa == selecionada.id)
                {
                    if (checkBox1.Checked)
                    {
                        if (m.pedido.finalizado == false)
                        {
                            dataGridView1.Rows.Add(m.pedido.id, m.produto.nome, m.quantidade, "Não");
                        }
                        else if(m.pedido.finalizado == true)
                        {
                            dataGridView1.Rows.Add(m.pedido.id, m.produto.nome, m.quantidade, "Sim");
                        }
                    }
                    else
                    {
                        if (m.pedido.finalizado == false)
                        {
                            dataGridView1.Rows.Add(m.pedido.id, m.produto.nome, m.quantidade, "Não");
                        }
                    }

                }
            });
        }
    }
}
