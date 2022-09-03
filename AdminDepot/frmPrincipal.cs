using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminDepot
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnProvincias_Click(object sender, EventArgs e)
        {
            Form formProvincias = new frmProvincias();
            formProvincias.ShowDialog();
        }
    }
}
