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
    public partial class ProdutosID : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        List<categoria> categorias;

        public ProdutosID()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            btnSalvarProduto.Click += salvarProduto;
            btnFotoProduto.Click += addFotoProd;
            btnDescartarProduto.Click += limparCampos;
            categorias = bd.categoria.ToList();
            btnProdutosIngredientesID.Click += abrirTelaIng;
            button1.Click += novaTelaAlterarouExcluir;
            button2.Click += voltarTela;
            carregaCheckList();
        }

        private void carregaCheckList()
        {
            checkedListBox1.Items.Clear();
            bd.ingrediente.ToList().ForEach(m =>
            {
                checkedListBox1.Items.Add(m.nome);
            });
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void novaTelaAlterarouExcluir(object sender, EventArgs e)
        {
            new InfoProdutos().Show();
            this.Hide();
        }

        private void abrirTelaIng(object sender, EventArgs e)
        {
            new ProdutosIng().Show();
            this.Visible = false;
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            comboBox2.Text = "";
            pictureBox1.Image = null;
        }

        private void limparCampos(object sender, EventArgs e)
        {
            ClearData();
        }
        private void addFotoProd(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }

        }

        private void salvarProduto(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            categoria selecionada = categorias.ElementAt(comboBox2.SelectedIndex);

            produto novo = new produto()
            {
                nome = textBox1.Text,
                descricao = textBox4.Text,
                imagem = arr,
                categoria = selecionada.id,
                preco = Convert.ToDouble(maskedTextBox1.Text)
            };
            bd.produto.Add(novo);

            bd.ingrediente.ToList().ForEach(q => 
            {
                if (checkedListBox1.GetItemChecked(q.id-1))
                {
                    novo.ingrediente.Add(q);
                }
            });
            bd.SaveChanges();

            MessageBox.Show("Produto salvo com sucesso!");
            ClearData();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
