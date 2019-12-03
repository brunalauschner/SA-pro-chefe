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
    public partial class TelaLogin : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        public static usuario logado = new usuario();
        public static usuario logado2 = new usuario();
        public static usuario logado3 = new usuario();
        public static usuario logado4 = new usuario();

        public TelaLogin()
        {
            InitializeComponent();
            btnTLEntrar.Click += entrar;
            btnTLSair.Click += sair;
            button1.Click += novaTela;
            checkBox1.Click += mostrarSenha;
        }

        private void mostrarSenha(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                senhaEntrar.UseSystemPasswordChar = false;
            }
            else
            {
                senhaEntrar.UseSystemPasswordChar = true;
            }
        }

        private void ClearData()
        {
            nomeEntrar.Text = "";
            senhaEntrar.Text = "";
            senhaEntrar.PasswordChar = 'b'; 
        }

        private void novaTela(object sender, EventArgs e)
        {
            logado = bd.usuario.Where(u => u.login.Equals(nomeEntrar.Text)
            && u.senha.Equals(senhaEntrar.Text) && u.tipo_usuario == 1).FirstOrDefault();
            
            logado2 = bd.usuario.Where(u => u.login.Equals(nomeEntrar.Text)
            && u.senha.Equals(senhaEntrar.Text) && u.tipo_usuario == 2).FirstOrDefault();

            logado3 = bd.usuario.Where(u => u.login.Equals(nomeEntrar.Text)
            && u.senha.Equals(senhaEntrar.Text) && u.tipo_usuario == 3).FirstOrDefault();

            logado4 = bd.usuario.Where(a => a.login.Equals(nomeEntrar.Text)
            && a.senha.Equals(senhaEntrar.Text) && a.tipo_usuario == 4).FirstOrDefault();

            if (logado != null)
            {
                new NovoUsuario().Show();
                this.Hide();
            }
            else if (logado2 != null)
            {
                MessageBox.Show("Usuário inválido! O usuário deve ser um Administrador.");
            }
            else if (logado3 != null)
            {
                MessageBox.Show("Usuário inválido! O usuário deve ser um Administrador.");
            }
            else if(logado4 != null)
            {
                MessageBox.Show("Usuário inválido! O usuário deve ser um Administrador.");
            }
            else
            {
                MessageBox.Show("Login ou senha inválidos!");
                
            }
            ClearData();
        }

        private void sair(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void entrar(object sender, EventArgs e)
        {
            logado = bd.usuario.Where(u => u.login.Equals(nomeEntrar.Text)
            && u.senha.Equals(senhaEntrar.Text)
            && (u.tipo_usuario == 3 || u.tipo_usuario == 1)).FirstOrDefault();
            
            logado2 = bd.usuario.Where(u => u.login.Equals(nomeEntrar.Text)
            && u.senha.Equals(senhaEntrar.Text)
            && u.tipo_usuario == 2).FirstOrDefault();
            
            logado4 = bd.usuario.Where(a => a.login.Equals(nomeEntrar.Text)
            && a.senha.Equals(senhaEntrar.Text) && a.tipo_usuario == 4).FirstOrDefault();

            if (logado != null)
            {
                new Operacional().Show();
                this.Hide();
                
            }
            else if (logado2 != null)
            {
                MessageBox.Show("Usuário inválido! O usuário deve ser Administrador/Garçom/Entregador.");
            }
            else if (logado4 != null)
            {
                new Delivery().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login ou senha inválidos!");
            }
            ClearData();
        }

        

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
