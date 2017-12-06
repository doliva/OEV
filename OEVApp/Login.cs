using BLL;
using BLL.IBLL;
using OEVApp.i18n;
using Base;
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
using Base.BLL;

namespace OEVApp
{
    public partial class Login : Form
    {

        String msjClaveIncorrecta = null;
        String msjUsuarioClaveVacios = null;
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        ISeguridad seguridad = new Seguridad();
        string idioma = null;
        IRolBLL rolBLL = new RolBLL();
        IFamiliaBLL familiaBLL = new FamiliaBLL();
        IPatenteBLL patenteBLL = new PatenteBLL();

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
            lblUsuario.Text = new StringBuilder(I18n.obtenerString("LoginForm","usuario")).Append(Constantes.DOS_PUNTOS).ToString();
            lblClave.Text = new StringBuilder(I18n.obtenerString("LoginForm", "clave")).Append(Constantes.DOS_PUNTOS).ToString();
            msjClaveIncorrecta = I18n.obtenerString("Mensaje", "claveIncorrecta");
            msjUsuarioClaveVacios = I18n.obtenerString("Mensaje", "usuarioClaveVacios");
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
                if (usr != null && claveEncript==usr.clave && Int32.Parse(usuario)== usr.id)
                {
                    String idRolString = (usuarioBLL.obtnerRolPorIdUsuario(usr.id)!=null) ? usuarioBLL.obtnerRolPorIdUsuario(usr.id) : null;
                    if (idRolString != null)
                    {
                        Rol rol = rolBLL.obtenerRolPorId(Int32.Parse(idRolString));
                        List<Familia> listaFamilias = familiaBLL.obtenerFamilias().Where(f => f.estado==true).ToList();
                        List<Patente> listaPatentes = patenteBLL.obtenerPatentes().Where(p => p.estado==true).ToList();
                        List<Familia> familiasPermitidas = listaFamilias.Where(f => rol.TieneAcceso(f) == true).ToList();
                        List<Patente> patentesPermitidas = listaPatentes.Where(p => rol.TieneAcceso(p) == true).ToList();
                        //foreach (Familia fam in listaFamilias)
                        //{
                        //    if (rol.TieneAcceso(fam))
                        //        familiasPermitidas.Add(fam);
                        //}

                        Administrador admin = new Administrador();
                        Director director = new Director();
                 
                        if (rol.descripcion.Trim() == admin.GetType().Name.ToUpper())
                        {
                            admin = new Administrador(usr, idioma, familiasPermitidas, patentesPermitidas);
                            admin.Show();
                        }
                        else if (rol.descripcion.Trim() == director.GetType().Name.ToUpper())
                        {
                            director = new Director(usr, idioma, familiasPermitidas, patentesPermitidas);
                            director.Show();
                        }
                        //string dv = seguridad.generarSHA(usr.estado.ToString() + usr.email + usr.clave);

                        Bitacora bitacora = new Bitacora(Convert.ToInt16(usuario), rol.descripcion, DateTime.Now, EnumEvento.LOGIN.ToString(), "");
                        BitacoraBLL.registrarBitacora(bitacora);
                    }
                    
                    //admin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(msjClaveIncorrecta, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(msjUsuarioClaveVacios, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
