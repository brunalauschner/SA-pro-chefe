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
    public partial class Cadastro : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        

        public Cadastro()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            button1.Click += sairPrograma;
            picBoxCadMesas.Click += chamarMesas;
            picBoxCadClientes.Click += chamarClientes;
            picBoxCadProdutos.Click += chamarProdutos;
            btnCaOperacional.Click += chamarOperacional;
            pictureBox4.Click += chamarIngredientes;
            pictureBox5.Click += chamarEntregador;
            pictureBox6.Click += chamarNovoUsuario;
        }

        private void sairPrograma(object sender, EventArgs e)
        {
            new TelaLogin().Show();
            this.Hide();
        }

        private void chamarNovoUsuario(object sender, EventArgs e)
        {

            if (TelaLogin.logado.tipo_usuario == 1)
            {
                new NovoUsuario().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário inválido! Entre como administrador para acessar esta aba.");
            }

            
        }

        private void chamarEntregador(object sender, EventArgs e)
        {
            new InfoEntregadores().Show();
            this.Hide();
        }

        private void chamarIngredientes(object sender, EventArgs e)
        {
            new ProdutosIng().Show();
            this.Hide();
        }

        private void chamarOperacional(object sender, EventArgs e)
        {
            new Operacional().Show();
            this.Hide();
        }

        private void chamarProdutos(object sender, EventArgs e)
        {
            new ProdutosID().Show();
            this.Hide();
        }

        private void chamarClientes(object sender, EventArgs e)
        {
            new Clientes().Show();
            this.Hide();
        }

        private void chamarMesas(object sender, EventArgs e)
        {
            new Mesas().Show();
            this.Hide();
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {

        }
    }
}
