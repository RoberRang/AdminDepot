using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using AdminDepot.ModelosDTOs;

namespace AdminDepot
{
    public partial class frmProvincias : Form
    {
        private readonly string cadenaConexion;
        private int accion;
        public frmProvincias()
        {
            InitializeComponent();
            cadenaConexion = ConfigurationManager.ConnectionStrings["Depots"].ToString();
            accion = 0;
        }

        private void Provincias_Load(object sender, EventArgs e)
        {
            llenarDataGridView();
        }
        private void llenarDataGridView()
        {
            dgvProvincias.DataSource = null;
            ServicioProvincia servicioProvincia = new ServicioProvincia(cadenaConexion);
            //DataSet ds = new DataSet();
            //ds = provinciaDTO.DarProvincias();
            //dgvProvincias.DataSource = ds;
            //dgvProvincias.DataMember = ds.Tables[0].TableName;
            dgvProvincias.DataSource = servicioProvincia.ObtenerProvincias();
            dgvProvincias.Columns["id"].Visible = false;
            gbxAM.Enabled = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            gbxAM.Enabled = true;
            limpiarControlesTexto();
            accion = 1;
            txtNombre.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCapital.Text.Equals(""))
                return;
            if (txtNombre.Text.Equals(""))
                return;
            if (txtSuperficie.Text.Equals(""))
                return;
            grabaProvincia();
        }

        private void grabaProvincia()
        {
            ejecutarAccionBase();
            llenarDataGridView();
            limpiarControlesTexto();
        }

        private void ejecutarAccionBase()
        {
            ServicioProvincia provinciaDTO = new ServicioProvincia(cadenaConexion);
            provinciaDTO.Capital = txtCapital.Text;
            provinciaDTO.Nombre = txtNombre.Text;
            provinciaDTO.Superficie = decimal.Parse(txtSuperficie.Text);
            if (accion == 1)
                provinciaDTO.GrabarProvincia();
            if (accion == 2)
            {
                provinciaDTO.Id = int.Parse(dgvProvincias.SelectedRows[0].Cells["Id"].Value.ToString());
                provinciaDTO.ActualizarProvincia();
            }
            if (accion == 3)
            {
                provinciaDTO.Id = int.Parse(dgvProvincias.SelectedRows[0].Cells["Id"].Value.ToString());
                provinciaDTO.EliminarProvincia();
            }
        }

        private void limpiarControlesTexto()
        {
            txtCapital.Text = "";
            txtNombre.Text = "";
            txtSuperficie.Text = "";
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var dr = (DataRowView)dgvProvincias.CurrentRow.DataBoundItem;

            txtNombre.Text = dgvProvincias.SelectedRows[0].Cells["Nombre"].Value.ToString();
            txtCapital.Text = dgvProvincias.SelectedRows[0].Cells["Capital"].Value.ToString();
            txtSuperficie.Text = dgvProvincias.SelectedRows[0].Cells["Superficie"].Value.ToString();
            gbxAM.Enabled = true;
            accion = 2;
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var dr = (DataRowView)dgvProvincias.CurrentRow.DataBoundItem;
            string mensaje = "Desea Eliminar el registro seleccionado";
            DialogResult dialogResult = MessageBox.Show(mensaje, "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                txtNombre.Text = dgvProvincias.SelectedRows[0].Cells["Nombre"].Value.ToString();
                txtCapital.Text = dgvProvincias.SelectedRows[0].Cells["Capital"].Value.ToString();
                txtSuperficie.Text = dgvProvincias.SelectedRows[0].Cells["Superficie"].Value.ToString();
                accion = 3;
                ejecutarAccionBase();
                llenarDataGridView();
            }
            limpiarControlesTexto();
        }
    }
}
