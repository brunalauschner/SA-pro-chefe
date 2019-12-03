using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telaDeLogin
{
    public partial class Clientes : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        public clients teste = new clients();
        clients pesquisado = new clients();
        endereco pesquisa = new endereco();
        endereco confirma = new endereco();
        public int ArquivoId { get; set; }

        public Clientes()
        {
            InitializeComponent();

            button2.Visible = false;
            button3.Visible = false;
            btnInfocli.Visible = false;
            btnSalvarNovoEnd.Visible = false;
            btnSalvarCliente.Visible = false;
            button5.Visible = false;
            button8.Visible = false;

            lblUser.Text += TelaLogin.logado.nome;
            btnSalvarCliente.Click += salvarCliente;
            btnFotoCliente.Click += addFoto;
            btnSalvarNovoEnd.Click += salvarNovoende;
            btnInfocli.Click += abrirNovaTela;
            button1.Click += pesquisarCliente;
            button2.Click += alterarCliente;
            button3.Click += excluirCliente;
            button4.Click += maisOpcoes;
            button5.Click += menosOpcoes;
            button6.Click += voltarTela;
            carregaEnderecos();
            button7.Click += pesquisarEndereco;
            button8.Click += limpardados;
            pictureBox3.Click += novatelaInfo;
        }

        private void limpardados(object sender, EventArgs e)
        {
            ClearData();
        }

        private void novatelaInfo(object sender, EventArgs e)
        {
            new InformacoesClientes().Show();
        }

        private void pesquisarEndereco(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "(  )      -")
            {
                MessageBox.Show("Confira se o campo Telefone está preenchido corretamente!");
            }
            else
            {
                pesquisado = bd.clients.Where(u => u.telefone.Equals(maskedTextBox1.Text)).FirstOrDefault();
                pesquisa = bd.endereco.Where(a => a.id_clientsend.Equals(pesquisado.id_cliente)).FirstOrDefault();

                confirma = bd.endereco.Where(d => d.logradouro.Equals(comboBox1.SelectedItem.ToString())).FirstOrDefault();

                if (pesquisa != null)
                {
                    maskedTextBox4.Text = confirma.cep;
                    textBox6.Text = confirma.logradouro;
                    textBox7.Text = confirma.numero;
                    textBox8.Text = confirma.complemento;
                    textBox9.Text = confirma.bairro;
                    textBox10.Text = confirma.uf;
                    textBox11.Text = confirma.cidade;
                    maskedTextBox1.Enabled = false;
                    maskedTextBox4.Enabled = false;
                    privarTelefone2();
                    privarCEP2();

                }
                else
                {
                    MessageBox.Show("Endereço não achado.");
                }
            }
        }

        private void carregaEnderecos()
        {
            comboBox1.DataSource = bd.endereco.Select(c => c.logradouro).ToList();
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void menosOpcoes(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            btnInfocli.Visible = false;
            btnSalvarNovoEnd.Visible = false;
            btnSalvarCliente.Visible = false;
            button4.Visible = true;
            button5.Visible = false;
            button8.Visible = false;
        }

        private void maisOpcoes(object sender, EventArgs e)
        {
            button2.Visible = true;
            button3.Visible = true;
            btnInfocli.Visible = true;
            btnSalvarNovoEnd.Visible = true;
            btnSalvarCliente.Visible = true;
            button4.Visible = false;
            button5.Visible = true;
            button8.Visible = true;
        }

        private void excluirCliente(object sender, EventArgs e)
        {
            string excluir = maskedTextBox1.Text;
            clients excluirCl = new clients();
            endereco excluirEnd = new endereco();

            bd.clients.ToList().ForEach(f =>
            {
                if (f.telefone == excluir)
                {
                    excluirCl = f;

                    bd.endereco.ToList().ForEach(a =>
                    {
                        if (a.id_clientsend == f.id_cliente)
                        {
                            excluirEnd = a;
                            bd.endereco.Remove(excluirEnd);
                        }
                    });
                    bd.clients.Remove(excluirCl);
                }

            });

            bd.SaveChanges();
            ClearData();
            MessageBox.Show("Excluído com sucesso!");
        }

        private void alterarCliente(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "(  )      -")
            {
                MessageBox.Show("Confira se o campo Telefone está preenchido corretamente!");
            }
            else
            {
                string antigoCEP = privarCEP();
                string antigoTelefon = privarTelefone();
                string novoValorNome = textBox1.Text;
                string novoValorTelefone = maskedTextBox1.Text;
                string novoValorCPF = maskedTextBox2.Text;
                string novoValorCNPJ = maskedTextBox3.Text;
                string novoValorCEP = maskedTextBox4.Text;
                string novoValorLogradouro = textBox6.Text;
                string novoValorNumero = textBox7.Text;
                string novoValorComplemento = textBox8.Text;
                string novoValorBairro = textBox9.Text;
                string novoValorUF = textBox10.Text;
                string novoValorCidade = textBox11.Text;

                Image img = pictureBox1.Image;
                byte[] novaFoto;
                ImageConverter converter = new ImageConverter();
                novaFoto = (byte[])converter.ConvertTo(img, typeof(byte[]));


                mesa alterarMe = new mesa();
                bd.clients.ToList().ForEach(f =>
                {
                    if (f.telefone == antigoTelefon)
                    {
                        DialogResult dialog = MessageBox.Show("Alterar os dados pessoais?", "Confirmação", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            f.nome = novoValorNome;
                            f.telefone = novoValorTelefone;
                            f.cpf = novoValorCPF;
                            f.cnpj = novoValorCNPJ;
                            f.imagem = novaFoto;
                            bd.SaveChanges();
                            MessageBox.Show("Alterado com sucesso!");
                        }
                        else if (dialog == DialogResult.No)
                        {
                            DialogResult dialog2 = MessageBox.Show("Alterar o endereço?", "Confirmação", MessageBoxButtons.YesNo);
                            if (dialog2 == DialogResult.Yes)
                            {
                                bd.endereco.ToList().ForEach(a =>
                                                       {
                                                           if (a.id_clientsend == f.id_cliente)
                                                           {
                                                               if (a.cep == antigoCEP)
                                                               {
                                                                   a.cep = novoValorCEP;
                                                                   a.logradouro = novoValorLogradouro;
                                                                   a.numero = novoValorNumero;
                                                                   a.complemento = novoValorComplemento;
                                                                   a.bairro = novoValorBairro;
                                                                   a.uf = novoValorUF;
                                                                   a.cidade = novoValorCidade;
                                                               }
                                                           }
                                                           bd.SaveChanges();
                                                       });
                                bd.SaveChanges();
                                MessageBox.Show("Alterado com sucesso!");
                            }
                            else if (dialog2 == DialogResult.No)
                            {

                            }
                        }


                    }
                });
                bd.SaveChanges();
                ClearData();
            }
        }

        private void pesquisarCliente(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "")
            {
                MessageBox.Show("Confira se o campo Telefone está preenchido corretamente!");
            }
            else
            {
                pesquisado = bd.clients.Where(u => u.telefone.Equals(maskedTextBox1.Text)).FirstOrDefault();
                if (pesquisado != null)
                {
                    textBox1.Text = pesquisado.nome;
                    maskedTextBox1.Text = pesquisado.telefone;
                    maskedTextBox2.Text = pesquisado.cpf;
                    maskedTextBox3.Text = pesquisado.cnpj;
                    
                    if(pesquisado.imagem != null)
                    {
                        clients obj;
                        obj = bd.clients.FirstOrDefault(a => a.id_cliente == pesquisado.id_cliente);
                        if (obj == null) return;
                        var stream = new MemoryStream(obj.imagem);
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado. Verifique se o número de telefone que você digitou está correto.");
                }
            }
        }

        private string privarCEP()
        {
            string antigoCEP = maskedTextBox4.Text;
            maskedTextBox4.Enabled = true;
            return antigoCEP;
        }
        private void privarCEP2()
        {
            privarCEP();
        }

        private string privarTelefone()
        {
            string antigoTelefone = maskedTextBox1.Text;
            maskedTextBox1.Enabled = true;
            return antigoTelefone;

        }
        private void privarTelefone2()
        {
            privarTelefone();
        }

        private void abrirNovaTela(object sender, EventArgs e)
        {
            new infoClientes().Show();
        }

        private void salvarNovoende(object sender, EventArgs e)
        {
            teste = bd.clients.Where(u => u.telefone.Equals(maskedTextBox1.Text)).FirstOrDefault();
            if (teste != null)
            {
                endereco novoend = new endereco()
                {
                    cep = maskedTextBox4.Text,
                    logradouro = textBox6.Text,
                    numero = textBox7.Text,
                    complemento = textBox8.Text,
                    bairro = textBox9.Text,
                    uf = textBox10.Text,
                    cidade = textBox11.Text,
                    id_clientsend = teste.id_cliente
                };
                bd.endereco.Add(novoend);
                bd.SaveChanges();
                MessageBox.Show("Salvo com sucesso!");
                ClearData();
            }
        }




        private void addFoto(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }

        }

        private void ClearData()
        {
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            maskedTextBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            pictureBox1.Image = null;
        }

        private void salvarCliente(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            if (string.IsNullOrWhiteSpace(textBox1.Text) || maskedTextBox1.Text == "(  )      -" || maskedTextBox4.Text == "     -" ||
                string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Preencha os campos obrigatórios corretamente!");
            }
            else
            {
                clients novo = new clients()
                {
                    nome = textBox1.Text,
                    telefone = maskedTextBox1.Text,
                    cpf = maskedTextBox2.Text,
                    cnpj = maskedTextBox3.Text,
                    imagem = arr
                };

                bd.clients.Add(novo);
                bd.SaveChanges();

                endereco novin = new endereco()
                {
                    cep = maskedTextBox4.Text,
                    logradouro = textBox6.Text,
                    numero = textBox7.Text,
                    complemento = textBox8.Text,
                    bairro = textBox9.Text,
                    uf = textBox10.Text,
                    cidade = textBox11.Text,
                    id_clientsend = novo.id_cliente

                };
                bd.endereco.Add(novin);
                bd.SaveChanges();

                MessageBox.Show("Salvo com sucesso!");
                ClearData();
            }
        }

    }
}
