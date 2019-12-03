using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace telaDeLogin.helpers
{
    class FormsClass
    {
        public static bool aberto(String janela)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name.Equals(janela))
                {
                    frm.Visible = true;
                    return true;
                }
            }
            return false;
        }
    }
}
