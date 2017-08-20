using BLL;
using BLL.IBLL;
using Entities;
using OEVApp.i18n;
using Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Utils;
using Utils.Bitacora;

namespace OEVApp
{
    public partial class Login : Form
    {

        String _claveIncorrecta = null;
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        ISeguridad seguridad = new Seguridad.Seguridad();
        string idioma = null;

        public Login()
        {
            InitializeComponent();
            I18n.cargarIdioma(EnumIdioma.Español);
            idioma = EnumIdioma.Español.ToString();
            generarStrings();
            cargarComboIdioma(cbbIdioma);
        }
       
        private void generarStrings()
        {
            lblUsuario.Text = I18n.obtenerString("LoginForm","lblUsuario");
            lblClave.Text = I18n.obtenerString("LoginForm", "lblClave");
            _claveIncorrecta = I18n.obtenerString("LoginForm", "claveIncorrecta");
        }

        private void cargarComboIdioma(ComboBox cbbIdioma)
        {
            cbbIdioma.DataSource = Enum.GetValues(typeof(EnumIdioma)).Cast<EnumIdioma>().ToList();
            if (cbbIdioma.Items.Count != 0)
            {
                string idIdioma = cbbIdioma.SelectedValue.ToString();
            }
            else
            {
                cbbIdioma.DataSource = null;
            }
        }


        private void cbbIdioma_SelectionChangeCommitted(object sender, EventArgs e)
        {
            I18n.cargarIdioma((EnumIdioma)Enum.Parse(typeof(EnumIdioma), this.cbbIdioma.SelectedValue.ToString()));
            generarStrings();
            idioma = this.cbbIdioma.SelectedValue.ToString();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuario.Text;
            String clave = txtClave.Text;

            if (usuario.Trim() != "" && clave.Trim() != "")
            {
                Usuario usr = usuarioBLL.obtenerUsuarioPorId(Int32.Parse(usuario));
                string claveEncript = seguridad.Encriptar(txtClave.Text);
                if (usr != null && claveEncript==usr.clave && Int32.Parse(usuario)== usr.idUsuario)
                {
                    Administrador inicio = new Administrador(usuario, idioma);
                    string dv = seguridad.generarSHA(usr.estado.ToString() + usr.email + usr.clave);
                    
                    Bitacora bitacora = new Bitacora(Convert.ToInt16(usuario), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.LOGIN, "");
                    BitacoraBLL.registrarBitacora(bitacora);
                    inicio.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario y/o clave invalidos", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Usuario y/o clave vacias", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
