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
    public partial class Mesas : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        public Mesas()
        {
            InitializeComponent();
            lblUser.Text += TelaLogin.logado.nome;
            btnSalvarMesa.Click += salvarMesa;
            carregarTblMesas();
            dataGridView1.RowHeaderMouseClick += dataGridView1_RowHeaderMouseClick;
            btnAlterarMesa.Click += alterarMesa;
            btnExcluirMesa.Click += excluirMesa;
            button1.Click += voltarTela;
            textBox1.Visible = false;
        }

        private void voltarTela(object sender, EventArgs e)
        {
            new Cadastro().Show();
            this.Hide();
        }

        private void excluirMesa(object sender, EventArgs e)
        {
            int excluir = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            mesa excluirMe = new mesa();
            bd.mesa.ToList().ForEach(f =>
            {
                if (f.id == excluir)
                {
                    excluirMe = f;
                }

            });
            bd.mesa.Remove(excluirMe);
            bd.SaveChanges();
            carregarTblMesas();
            ClearData();
        }

        private void alterarMesa(object sender, EventArgs e)
        {
            int novoValorCapacidade = Convert.ToInt32(textBox3.Text);
            bool novoValorDisponivel = Convert.ToBoolean(comboBox1.SelectedIndex);
            string novoValorObservacao = textBox2.Text;

            int alterar = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            mesa alterarMe = new mesa();
            bd.mesa.ToList().ForEach(f =>
            {
                if (f.id == alterar)
                {
                    f.capacidade = novoValorCapacidade;
                    f.disponivel = novoValorDisponivel;
                    f.observacao = novoValorObservacao;

                    dataGridView1.SelectedRows[0].Cells[1].Value = f.capacidade;
                    dataGridView1.SelectedRows[0].Cells[2].Value = f.disponivel;
                    dataGridView1.SelectedRows[0].Cells[3].Value = f.observacao;

                    bd.SaveChanges();
                }
            });
            carregarTblMesas();
            ClearData();
        }

        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearData();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void carregarTblMesas()
        {
            dataGridView1.Rows.Clear();
            bd.mesa.ToList().ForEach(m =>
            {
                if (m.disponivel.Equals(true))
                {
                    dataGridView1.Rows.Add(m.id, m.capacidade, "Disponível", m.observacao);
                }
                if (m.disponivel.Equals(false))
                {
                    dataGridView1.Rows.Add(m.id, m.capacidade, "Ocupado", m.observacao);
                }

            });
        }

        private void salvarMesa(object sender, EventArgs e)
        {
            mesa table = new mesa()
            {
                capacidade = Convert.ToInt32(textBox3.Text),
                disponivel = Convert.ToBoolean(comboBox1.SelectedIndex),
                observacao = textBox2.Text
            };
            bd.mesa.Add(table);
            bd.SaveChanges();
            carregarTblMesas();
            ClearData();
        }
    }
}
