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
    public partial class Delivery : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        clients pesquisado = new clients();
        endereco pesquisa = new endereco();
        clients selecionado = new clients();
        pedido seleciona = new pedido();
        endereco select = new endereco();
        itens_pedido sele = new itens_pedido();
        public Delivery()
        {
            InitializeComponent();
            carregarTblPedidos();
            carregarNomeUsuario();
            button3.Click += voltarTela;
            checkBox1.Click += carregaTbl;
            checkBox2.Click += carregaTbl;
            button1.Click += finalizarPedido;
            button4.Click += carregatb;
            button2.Visible = false;
            dataGridView2.RowHeaderMouseClick += dataGridView2_RowHeaderMouseClick;
            button2.Click += confirmarPedido;
        }

        private void carregarNomeUsuario()
        {
            if (TelaLogin.logado != null)
            {
                lblUser.Text += TelaLogin.logado.nome;
            }
            else if (TelaLogin.logado4 != null)
            {
                lblUser.Text += TelaLogin.logado4.nome;
            }
        }


        private void confirmarPedido(object sender, EventArgs e)
        {
            new confirmarPedidoDelivery(selecionado, seleciona, select, sele).Show();
            this.Hide();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString() == "Sim")
            {
                button2.Visible = true;
                string a = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                string b = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                int d = Convert.ToInt32(b);
                selecionado = bd.clients.Where(c => c.telefone.Equals(a)).FirstOrDefault();
                seleciona = bd.pedido.Where(k => k.id.Equals(d)).FirstOrDefault();
                select = bd.endereco.Where(c => c.clients.telefone.Equals(a)).FirstOrDefault();
                sele = bd.itens_pedido.Where(l => l.id_pedido.Equals(d)).FirstOrDefault();
            }
            else
            {
                button2.Visible = false;
            }
        }

        private void carregatb(object sender, EventArgs e)
        {
            carregaNovaTbl();
        }

        private void carregaNovaTbl()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            string telcliente = "";
            dataGridView2.Rows.Clear();

            bd.itens_pedido.ToList().ForEach(a =>
            {
                bd.clients.ToList().ForEach(f =>
                {
                    if (a.pedido.id_cliente_ped == f.id_cliente)
                    {
                        telcliente = f.telefone;
                    }
                });

                if (a.pedido.id_mesa == null) // delivery
                {
                    if (a.pedido.finalizado == false) // nao finalizado
                    {
                        if (a.pedido.id_entregador == null)
                        {
                            dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Não / ---");
                        }
                        else
                        {
                            dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                        }
                    }
                    else if (a.pedido.finalizado == true) // finalizado
                    {
                        if (a.pedido.id_entregador == null)
                        {
                            dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Sim", (a.produto.preco * a.quantidade), "Não / ---");
                        }
                        else
                        {
                            dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Sim", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                        }
                    }
                }
                else // na mesa
                {
                    if (a.pedido.id_mesa != null)
                    {
                        if (a.pedido.finalizado == false)
                        {
                            dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Não", a.pedido.id_mesa, "Não", (a.produto.preco * a.quantidade), "-------");
                        }
                        else if (a.pedido.finalizado == true)
                        {
                            dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Não", a.pedido.id_mesa, "Sim", (a.produto.preco * a.quantidade), "-------");
                        }
                    }
                }
            });
        }


        private void finalizarPedido(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
            bd.pedido.ToList().ForEach(f =>
            {
                if (f.id == idPedido)
                {
                    f.finalizado = true;
                }
            });
            bd.SaveChanges();
            carregaNovaTbl();
        }

        private void carregaTbl(object sender, EventArgs e)
        {
            carregarTblPedidos();
        }

        private void voltarTela(object sender, EventArgs e)
        {
            if (TelaLogin.logado != null)
            {
                new Operacional().Show();
                this.Hide();
            }
            else
            {
                new TelaLogin().Show();
                this.Hide();
            }
        }

        private void carregarTblPedidos()
        {
            string telcliente = "";
            dataGridView2.Rows.Clear();

            bd.itens_pedido.ToList().ForEach(a =>
           {
               bd.clients.ToList().ForEach(f =>
               {
                   if (a.pedido.id_cliente_ped == f.id_cliente)
                   {
                       telcliente = f.telefone;
                   }
               });
               if (checkBox1.Checked) // apenas delivery 
               {
                   if (a.pedido.id_mesa == null) // id da mesa nulo
                   {
                       if (checkBox2.Checked) // apenas deliverys nao finalizados
                       {
                           if (a.pedido.id_mesa == null && a.pedido.finalizado == false)
                           {
                               if (a.pedido.id_entregador == null)
                               {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Não / ---");
                               }
                               else
                               {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                               }
                           }
                       }

                       else // apenas deliverys (nao finalizados e finalizados)
                       {
                           if (a.pedido.id_mesa == null && a.pedido.finalizado == false)
                           {
                               if (a.pedido.id_entregador == null)
                               {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Não / ---");
                               }
                               else
                               {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                               }
                           }
                           else if (a.pedido.id_mesa == null && a.pedido.finalizado == true)
                           {
                               if (a.pedido.id_entregador == null)
                               {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Sim", (a.produto.preco * a.quantidade), "Não / ---");
                               }
                               else
                               {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Sim", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                               }
                           }
                       }
                   }
               }

               else if (checkBox2.Checked) // apenas nao finalizados
               {
                   if (checkBox1.Checked)
                   {
                       if (a.pedido.id_mesa == null && a.pedido.finalizado == false)
                       {
                           if (a.pedido.id_entregador == null)
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Não / ---");
                           }
                           else
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                           }
                       }
                   }
                   else // todos pedidos apenas nao finalizados
                   {
                       if (a.pedido.id_mesa != null && a.pedido.finalizado == false) // na mesa e nao finalizado
                       {
                           if (a.pedido.id_entregador == null)
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Não", a.pedido.id_mesa, "Não", (a.produto.preco * a.quantidade), "-------");
                           }
                           else
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Não", a.pedido.id_mesa, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                           }
                       }
                       else if (a.pedido.id_mesa == null && a.pedido.finalizado == false) // delivery nao finalizado
                       {
                           if (a.pedido.id_entregador == null)
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Não / ---");
                           }
                           else
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                           }
                       }
                   }
               }

               else // todos pedidos (nenhuma checkbox)
               {
                   if (a.pedido.id_mesa == null) // delivery
                   {
                       if (a.pedido.finalizado == false) // nao finalizado
                       {
                           if (a.pedido.id_entregador == null)
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Não / ---");
                           }
                           else
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Não", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                           }
                       }
                       else if (a.pedido.finalizado == true) // finalizado
                       {
                           if (a.pedido.id_entregador == null)
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Sim", (a.produto.preco * a.quantidade), "Não / ---");
                           }
                           else
                           {
                               dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Sim", telcliente, "Sim", (a.produto.preco * a.quantidade), "Sim / " + a.pedido.entregador.nome);
                           }
                       }
                   }
                   else // na mesa
                   {
                       if (a.pedido.id_mesa != null)
                       {
                           if (a.pedido.finalizado == false)
                           {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Não", a.pedido.id_mesa, "Não", (a.produto.preco * a.quantidade), "-------");
                               
                           }
                           else if (a.pedido.finalizado == true)
                           {
                                   dataGridView2.Rows.Add(a.id_pedido, a.pedido.data, a.produto.nome, a.quantidade, "Não", a.pedido.id_mesa, "Sim", (a.produto.preco * a.quantidade), "-------");
                           }
                       }
                   }
               }
           });
        }


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void LblUser_Click(object sender, EventArgs e)
        {

        }
    }
}
