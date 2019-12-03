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
    public partial class Operacional : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        public Operacional()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            montaMesas();
            btnCadastroOp.Click += abrirNovaTela;
            picBoxOPDelivery.Click += chamarDelivery;
            pictureBox11.Click += avisoPedido;
            button1.Click += sairPrograma;
        }
        private void sairPrograma(object sender, EventArgs e)
        {
            new TelaLogin().Show();
            this.Hide();
        }

        private void avisoPedido(object sender, EventArgs e)
        {
            new Delivery().Show();
            this.Hide();
        }

        private void chamarDelivery(object sender, EventArgs e)
        {
            new PedidoDelivery().Show();
            this.Hide();
        }

        private void abrirNovaTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void montaMesas()
        {
            bd.mesa.ToList().ForEach(m =>
            {

                Panel p = new Panel();
                p.Width = 100;
                p.Height = 100;
                p.BackColor = Color.DarkKhaki;
                p.Name = m.id.ToString();
                p.Click += selecionaMesa;

                Label nomeMesa = new Label();
                nomeMesa.Text = $"Mesa {m.id}";
                p.Controls.Add(nomeMesa);

                PictureBox foto = new PictureBox();
                foto.Image = Properties.Resources.iconfinder_architecture_interior_27_809104;
                p.Controls.Add(foto);

                flowLayoutPanel1.Controls.Add(p);

            });
        }

        private void selecionaMesa(object sender, EventArgs e)
        {
            Panel mesaSelecionada = (Panel)sender;
            mesa selecionada = bd.mesa.Find(Convert.ToInt32(mesaSelecionada.Name));
            new Pedidos(selecionada).Show();
            this.Hide();

        }







        private void Operacional_Load(object sender, EventArgs e)
        {

        }
    }
}
