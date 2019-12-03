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
    public partial class confirmarPedidoDelivery : Form
    {
        clients selecionada;
        pedido selecionado;
        endereco selected;
        itens_pedido selee;
        
        pro_chefeEntities2 bd = new pro_chefeEntities2();

        public confirmarPedidoDelivery(clients cliente_s, pedido pedids, endereco ends, itens_pedido itens)
        {
            selecionada = cliente_s;
            selecionado = pedids;
            selected = ends;
            selee = itens;
            InitializeComponent();
            preencher();
            preencheCombo();
            button1.Click += confirmar;
            button3.Click += voltar;
        }

        private void voltar(object sender, EventArgs e)
        {
            new Delivery().Show();
            this.Hide();
        }
        private void confirmar(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("O campo entregador está vazio!");
            }
            else
            {
                string nome_entregador = comboBox1.SelectedItem.ToString();
                entregador aaa = bd.entregador.Where(y => y.nome.Equals(nome_entregador)).FirstOrDefault();
                if (aaa != null)
                {
                    bd.pedido.ToList().ForEach(w =>
                    {
                        if (label13.Text == w.id.ToString())
                        {
                            w.id_entregador = aaa.id;
                            bd.SaveChanges();
                        }
                    });
                    bd.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro. Tente novamente");
                }
                bd.SaveChanges();
            }
        }

        private void preencheCombo()
        {
            comboBox1.Items.Clear();
            bd.entregador.ToList().ForEach(m =>
            {
                comboBox1.Items.Add(m.nome);
            });
        }
        
        private void preencher()
        {
            //cliente
            label4.Text = $"Cliente: {selecionada.nome}";
            label5.Text = $"Telefone: {selecionada.telefone}";
            label6.Text = $"CEP: {selected.cep}";
            label7.Text = $"Logradouro: {selected.logradouro}";
            label8.Text = $"Número: {selected.numero}";
            label9.Text = $"Complemento: {selected.complemento}";
            label10.Text = $"Bairro: {selected.bairro}";
            label11.Text = $"UF: {selected.uf}";
            label12.Text = $"Cidade: {selected.cidade}";
            //pedido
            produto teste = bd.produto.Where(q => q.id.Equals(selee.id_produto)).FirstOrDefault();
            label13.Text = selecionado.id.ToString();
            label14.Text = $"Data: {selecionado.data}";
            label15.Text = $"Produto: {teste.nome}";
            label16.Text = $"Quantidade: {selee.quantidade}";
            label17.Text = $"Total: {(selee.quantidade*teste.preco)}";
        }
    }
}
