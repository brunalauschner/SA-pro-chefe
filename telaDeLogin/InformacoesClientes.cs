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
    public partial class InformacoesClientes : Form
    {
        pro_chefeEntities2 bd = new pro_chefeEntities2();
        public InformacoesClientes()
        {
            InitializeComponent();
            button1.Click += fechartela;
        }
        
        private void fechartela(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
