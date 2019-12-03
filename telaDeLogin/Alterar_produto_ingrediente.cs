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
    public partial class Alterar_produto_ingrediente : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        produto selected = new produto();

        public Alterar_produto_ingrediente()
        {
            InitializeComponent();
            carregaCheckList();
            carregarTblProds();
            button1.Click += alterar;
            button2.Click += voltar;
        }

        private void voltar(object sender, EventArgs e)
        {
            new InfoProdutos().Show();
            this.Hide();
        }

        private void carregarTblProds()
        {
            dataGridView1.Rows.Clear();
            bd.produto.ToList().ForEach(m =>
            {
                dataGridView1.Rows.Add(m.id, m.nome);
            });
        }

        private void alterar(object sender, EventArgs e)
        {
            int excluir = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            ingrediente ingre = new ingrediente();

            bd.produto.ToList().ForEach(f =>
            {
                if (f.id == excluir)
                {
                    bd.ingrediente.ToList().ForEach(i =>
                    {
                        i.produto.Remove(f);
                    });
                }
            });
            bd.SaveChanges();

            string selecionado = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            selected = bd.produto.Where(u => u.nome.Equals(selecionado)).FirstOrDefault();

            bd.ingrediente.ToList().ForEach(q =>
            {
                if (checkedListBox1.GetItemChecked(q.id - 1))
                {
                    bd.produto.ToList().ForEach(z =>
                    {
                        if (z.id == selected.id)
                        {
                            z.ingrediente.Add(q);
                        }
                    });
                }
            });
            bd.SaveChanges();
            MessageBox.Show("Alterado com sucesso!");

            //Pra desmarcar os itens da checklist
            foreach (int i in checkedListBox1.CheckedIndices)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void carregaCheckList()
        {
            checkedListBox1.Items.Clear();
            bd.ingrediente.ToList().ForEach(m =>
            {
                checkedListBox1.Items.Add(m.nome);
            });
        }


    }
}
