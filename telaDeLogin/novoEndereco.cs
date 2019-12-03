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
    public partial class infoClientes : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();

        public infoClientes()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            carregarTblClientes();
            carregarTblEnderecos();
            button1.Click += voltarTela;
        }

        private void voltarTela(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void carregarTblEnderecos()
        {
            dataGridView2.Rows.Clear();
            bd.endereco.ToList().ForEach(n =>
            {
                dataGridView2.Rows.Add(n.id_clientsend, n.cep, n.logradouro, n.numero, n.complemento, n.bairro, n.uf, n.cidade);
            });
        }

        private void carregarTblClientes()
        {
            dataGridView1.Rows.Clear();

            bd.clients.ToList().ForEach(m =>
            {
                dataGridView1.Rows.Add(m.id_cliente, m.nome, m.telefone, m.cpf, m.cnpj);
            });
        }















        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
