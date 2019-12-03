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
    public partial class InfoEntregadores : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        entregador selected = new entregador();

        public InfoEntregadores()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            button2.Click += salvarEntregador;
            preencheCombo();
            button1.Click += mostrarDetalhes;
            button3.Click += voltarTela;
            button4.Click += alterarEntregador;
            button5.Click += excluirEntregador;
        }

        private void excluirEntregador(object sender, EventArgs e)
        {
            int excluir = Convert.ToInt32(label8.Text);
            entregador excluirE = new entregador();
            bd.entregador.ToList().ForEach(f =>
            {
                if (f.id == excluir)
                {
                    excluirE = f;
                }

            });
            bd.entregador.Remove(excluirE);
            bd.SaveChanges();
            ClearData();
        }

        private void alterarEntregador(object sender, EventArgs e)
        {
            string novoValorNome = textBox1.Text;
            string novoValorIdade = textBox2.Text;
            string novoValorTelefone = maskedTextBox2.Text;
            string novoValorSalario = maskedTextBox1.Text;
            string novoValorObservacao = textBox5.Text;


            int alterar = Convert.ToInt32(label8.Text);

            entregador alterarE = new entregador();
            bd.entregador.ToList().ForEach(f =>
            {
                if (f.id == alterar)
                {
                    f.nome = novoValorNome;
                    f.idade = novoValorIdade;
                    f.telefone = novoValorTelefone;
                    f.salario = Convert.ToDouble(novoValorSalario);
                    f.observacoes = novoValorObservacao;

                    bd.SaveChanges();
                }
            });
            ClearData();
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void mostrarDetalhes(object sender, EventArgs e)
        {
            string selecionado = comboBox1.SelectedItem.ToString();

            selected = bd.entregador.Where(u => u.nome.Equals(selecionado)).FirstOrDefault();

            if (selected != null)
            {
                textBox1.Text = selected.nome;
                textBox2.Text = selected.idade;
                maskedTextBox2.Text = selected.telefone;
                maskedTextBox1.Text = selected.salario.ToString();
                textBox5.Text = selected.observacoes;
                label8.Text = selected.id.ToString();
            }
            
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox5.Text = "";
            label8.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
        }

        private void preencheCombo()
        {
            comboBox1.Items.Clear();
            bd.entregador.ToList().ForEach(m =>
            {
                comboBox1.Items.Add(m.nome);
            });
        }

        private void salvarEntregador(object sender, EventArgs e)
        {
            entregador novo = new entregador()
            {
                nome = textBox1.Text,
                idade = textBox2.Text,
                telefone = maskedTextBox2.Text,
                salario = Convert.ToDouble(maskedTextBox1.Text),
                observacoes = textBox5.Text
            };
            bd.entregador.Add(novo);
            bd.SaveChanges();
            ClearData();
            preencheCombo();
        }







        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
