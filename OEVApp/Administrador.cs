using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Seguridad;
using BLL.IBLL;
using BLL;
using OEVApp.i18n;
using Utils.Bitacora;
using Utils.BackupRestoreBD;
using Utils;
using Entities;
using Utils.Excepciones;

namespace OEVApp
{
    public partial class Administrador : DevComponents.DotNetBar.Metro.MetroForm
    {
        String rutaBackup;
        String rutaRestore;
        String msjInfo = null;
        String msjError = null;
        String MSJ_BACKUP_EXITOSO = null;
        String MSJ_BACKUP_ERROR = null;
        String MSJ_RESTORE_EXITOSO = null;
        String MSJ_RESTORE_ERROR = null;
        String MSJ_RUTA_BACKUP_VACIA = null;
        String MSJ_RESTORE_INVALIDO = null;
        String MSJ_VERIFICACION_EXITOSA = null;
        String MSJ_VERIFICACION_ERRONEA = null;
        String MSJ_VERIFICACION_ERRONEA_TODOS = null;
        String MSJ_USUARIO_VACIO = null;
        ISeguridad seguridad = new Seguridad.Seguridad();
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        String fecNacInvalida = null;
        String formatoEmail = null;
        String formatoDni = null;
        String camposRequeridos = null;
        String confirmar = null;
        String msjExistente = null;
        String msjNuevo = null;
        String msjConfirmar = null;
        String confirmarBaja = null;
        String ningunRegistro = null;
        String codRolM = null;
        
        public Administrador(string usuario, string idioma)
        {
            InitializeComponent();
            lblUsuarioLogueado.Text = usuario;
            cargarComboRol(comboBoxRol);
            I18n.cargarIdioma((EnumIdioma)Enum.Parse(typeof(EnumIdioma), idioma));
            generarInicioAdministrador();
            generarGestionBDStrings();
            generarDVStrings();
            generaraCriptoStrings();
            generarUsuariosStrings();
            generarBitacoraStrings();
        }

        private void cargarComboRol(DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxRol)
        {
            comboBoxRol.DataSource = Enum.GetValues(typeof(EnumRol)).Cast<EnumRol>().ToList();
            if (comboBoxRol.Items.Count != 0)
            {
                string rol = comboBoxRol.SelectedValue.ToString();
            }
            else
            {
                comboBoxRol.DataSource = null;
            }
        }

        private void generarBitacoraStrings()
        {
            sideBarPanelBitacora.Text = I18n.obtenerString("InicioAdministrador", "sideBarPanelBitacora");
            btnItemConsultarB.Text = I18n.obtenerString("InicioAdministrador", "btnItemConsultarB");
            tabItemConsultarB.Text = I18n.obtenerString("InicioAdministrador", "tabItemConsultarB");
            groupBoxConsultarB.Text = I18n.obtenerString("InicioAdministrador", "groupBoxConsultarB");
            radioButtonRolConsularB.Text = I18n.obtenerString("InicioAdministrador", "radioButtonRolConsularB");
            radioButtonFechaConsultarB.Text = I18n.obtenerString("InicioAdministrador", "radioButtonFechaConsultarB");
            radioButtonEventoConsultarB.Text = I18n.obtenerString("InicioAdministrador", "radioButtonEventoConsultarB");
            dataGridViewTextBoxColumnIdConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "dataGridViewTextBoxColumnIdConsultarB");
            dataGridViewTextBoxColumnRoleConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "dataGridViewTextBoxColumnRoleConsultarB");
            dataGridViewTextBoxColumnFechaConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "dataGridViewTextBoxColumnFechaConsultarB");
            dataGridViewTextBoxColumnDetalleConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "dataGridViewTextBoxColumnDetalleConsultarB");
        }

        private void generarUsuariosStrings()
        {
            sideBarPanelUsuario.Text = I18n.obtenerString("InicioAdministrador", "sideBarPanelUsuario");
            btnItemAgregar.Text = I18n.obtenerString("InicioAdministrador", "btnItemAgregar");
            tabItemAgregar.Text = I18n.obtenerString("InicioAdministrador", "tabItemAgregar");
            btnItemModificar.Text = I18n.obtenerString("InicioAdministrador", "btnItemModificar");
            tabItemModificar.Text = I18n.obtenerString("InicioAdministrador", "tabItemModificar");
            btnItemConsultar.Text = I18n.obtenerString("InicioAdministrador", "btnItemConsultar");
            tabItemConsultar.Text = I18n.obtenerString("InicioAdministrador", "tabItemConsultar");
            generarAgregarUsuariosStrings();
            generarModificarUsuariosStrings();
            generarConsultarUsuarioStrings();
        }



        private void generarAgregarUsuariosStrings()
        {
            lblApellido.Text = I18n.obtenerString("InicioAdministrador", "lblApellido");
            lblNombre.Text = I18n.obtenerString("InicioAdministrador", "lblNombre");
            lblDni.Text = I18n.obtenerString("InicioAdministrador", "lblDni");
            lblFecNac.Text = I18n.obtenerString("InicioAdministrador", "lblFecNac");
            lblDireccion.Text = I18n.obtenerString("InicioAdministrador", "lblDireccion");
            lblCiudad.Text = I18n.obtenerString("InicioAdministrador", "lblCiudad");
            lblTelefono.Text = I18n.obtenerString("InicioAdministrador", "lblTelefono");
            lblEmail.Text = I18n.obtenerString("InicioAdministrador", "lblEmail");
            btnGuardar.Text = I18n.obtenerString("InicioAdministrador", "btnGuardar");
            fecNacInvalida = I18n.obtenerString("InicioAdministrador", "fecNacInvalida");
            formatoEmail = I18n.obtenerString("InicioAdministrador", "formatoEmail");
            formatoDni = I18n.obtenerString("InicioAdministrador", "formatoDni");
            camposRequeridos = I18n.obtenerString("InicioAdministrador", "NOMBRE_APELLIDO_DNI_EMAIL_REQUERIDOS");
            confirmar = I18n.obtenerString("InicioAdministrador", "confirmar");
            msjExistente = I18n.obtenerString("InicioAdministrador", "USUARIO_YA_EXISTE");
            msjNuevo = I18n.obtenerString("InicioAdministrador", "USUARIO_NUEVO");
            msjConfirmar = I18n.obtenerString("InicioAdministrador", "mensajeBoxConfirmar");
            msjInfo = I18n.obtenerString("InicioAdministrador", "mensajeBoxInfo");
            msjError = I18n.obtenerString("InicioAdministrador", "mensajeBoxError");
        }

        private void generarModificarUsuariosStrings()
        {
            lblIdM.Text = I18n.obtenerString("InicioAdministrador", "lblId");
            lblApellidoM.Text = I18n.obtenerString("InicioAdministrador", "lblApellido");
            lblNombreM.Text = I18n.obtenerString("InicioAdministrador", "lblNombre");
            lblDniM.Text = I18n.obtenerString("InicioAdministrador", "lblDni");
            lblFecNacM.Text = I18n.obtenerString("InicioAdministrador", "lblFecNac");
            lblDireccionM.Text = I18n.obtenerString("InicioAdministrador", "lblDireccion");
            lblCiudadM.Text = I18n.obtenerString("InicioAdministrador", "lblCiudad");
            lblTelefonoM.Text = I18n.obtenerString("InicioAdministrador", "lblTelefono");
            lblEmailM.Text = I18n.obtenerString("InicioAdministrador", "lblEmail");
            lblEstadoM.Text = I18n.obtenerString("InicioAdministrador", "lblEstado");
            radioGroupBuscar.Text = I18n.obtenerString("InicioAdministrador", "btnBuscar");
            btnBuscarM.Text = I18n.obtenerString("InicioAdministrador", "btnBuscar");
            btnGuardarM.Text = I18n.obtenerString("InicioAdministrador", "btnGuardar");
            ningunRegistro = I18n.obtenerString("InicioAdministrador", "NINGUN_REGISTRO");
            confirmarBaja = I18n.obtenerString("InicioAdministrador", "confirmarBaja");
        }

        private void generarConsultarUsuarioStrings()
        {
            groupBuscarC.Text = I18n.obtenerString("InicioAdministrador", "btnBuscar");
            radioIdC.Text = I18n.obtenerString("InicioAdministrador", "radioId");
            radioDniC.Text = I18n.obtenerString("InicioAdministrador", "radioDni");
            radioRolC.Text = I18n.obtenerString("InicioAdministrador", "radioRol");
            radioTodosC.Text = I18n.obtenerString("InicioAdministrador", "radioTodos");
            btnBuscarC.Text = I18n.obtenerString("InicioAdministrador", "btnBuscar");

            gridConsultaUsuario.Columns["HId"].HeaderText = I18n.obtenerString("InicioAdministrador", "hId");
            gridConsultaUsuario.Columns["HApellido"].HeaderText = I18n.obtenerString("InicioAdministrador", "hApellido");
            gridConsultaUsuario.Columns["HNombre"].HeaderText = I18n.obtenerString("InicioAdministrador", "hNombre");
            gridConsultaUsuario.Columns["HDni"].HeaderText = I18n.obtenerString("InicioAdministrador", "hDni");
            gridConsultaUsuario.Columns["HFecNac"].HeaderText = I18n.obtenerString("InicioAdministrador", "hFecNac");
            gridConsultaUsuario.Columns["HDireccion"].HeaderText = I18n.obtenerString("InicioAdministrador", "hDireccion");
            gridConsultaUsuario.Columns["HCiudad"].HeaderText = I18n.obtenerString("InicioAdministrador", "hCiudad");
            gridConsultaUsuario.Columns["HTelefono"].HeaderText = I18n.obtenerString("InicioAdministrador", "hTelefono");
            gridConsultaUsuario.Columns["HEmail"].HeaderText = I18n.obtenerString("InicioAdministrador", "hEmail");
            gridConsultaUsuario.Columns["HRol"].HeaderText = I18n.obtenerString("InicioAdministrador", "hRol");
            gridConsultaUsuario.Columns["HEstado"].HeaderText = I18n.obtenerString("InicioAdministrador", "hEstado");

        }

        private void generaraCriptoStrings()
        {
            sideBarPanelCripto.Text = I18n.obtenerString("InicioAdministrador", "sideBarPanelCripto");
            tabItemCripto.Text = I18n.obtenerString("InicioAdministrador", "tabItemCripto");
            radioGroupOpcion.Text = I18n.obtenerString("InicioAdministrador", "radioGroupOpcion");
            radioEncriptar.Text = I18n.obtenerString("InicioAdministrador", "radioEncriptar");
            radioDesencriptar.Text = I18n.obtenerString("InicioAdministrador", "radioDesencriptar");
            lblTextoCripto.Text = I18n.obtenerString("InicioAdministrador", "lblTextoCripto");
            lblResCripto.Text = I18n.obtenerString("InicioAdministrador", "lblResCripto");
        }

        private void generarDVStrings()
        {
            sideBarPanelDV.Text = I18n.obtenerString("InicioAdministrador", "sideBarPanelDV");
            btnItemDVH.Text = I18n.obtenerString("InicioAdministrador", "btnItemDVH");
            tabItemDVH.Text = I18n.obtenerString("InicioAdministrador", "tabItemDVH");
            lblUsuarioDVH.Text = I18n.obtenerString("InicioAdministrador", "lblUsuario");
            radioGroupDVH.Text = I18n.obtenerString("InicioAdministrador", "radioGroupDVH");
            radioUsuarioDVH.Text = I18n.obtenerString("InicioAdministrador", "radioUsuarioDVH");
            radioTodosDVH.Text = I18n.obtenerString("InicioAdministrador", "radioTodosDVH");
            btnVerificarDVH.Text = I18n.obtenerString("InicioAdministrador", "btnVerificarDVH");

            btnItemDVV.Text = I18n.obtenerString("InicioAdministrador", "btnItemDVV");
            tabItemDVV.Text = I18n.obtenerString("InicioAdministrador", "tabItemDVV");
            radioGroupDVV.Text = I18n.obtenerString("InicioAdministrador", "radioGroupDVV");
            btnVerificarDVV.Text = I18n.obtenerString("InicioAdministrador", "btnVerificarDVV");

            MSJ_VERIFICACION_EXITOSA = I18n.obtenerString("InicioAdministrador", "MSJ_VERIFICACION_EXITOSA");
            MSJ_VERIFICACION_ERRONEA = I18n.obtenerString("InicioAdministrador", "MSJ_VERIFICACION_ERRONEA");
            MSJ_VERIFICACION_ERRONEA_TODOS = I18n.obtenerString("InicioAdministrador", "MSJ_VERIFICACION_ERRONEA_TODOS");
            MSJ_USUARIO_VACIO = I18n.obtenerString("InicioAdministrador", "MSJ_USUARIO_VACIO");
        }

        private void generarGestionBDStrings()
        {
            sideBarPanelBD.Text = I18n.obtenerString("InicioAdministrador", "sideBarPanelBD");
            btnItemBackup.Text = I18n.obtenerString("InicioAdministrador", "btnItemBackup");
            tabItemBackup.Text = I18n.obtenerString("InicioAdministrador", "tabItemBackup");
            lblRutaBackup.Text = I18n.obtenerString("InicioAdministrador", "lblRutaBackup");
            btnBackup.Text = I18n.obtenerString("InicioAdministrador", "btnBackup");
            MSJ_BACKUP_EXITOSO = I18n.obtenerString("InicioAdministrador", "MSJ_BACKUP_EXITOSO");
            MSJ_BACKUP_ERROR = I18n.obtenerString("InicioAdministrador", "MSJ_BACKUP_ERROR");
            MSJ_RUTA_BACKUP_VACIA = I18n.obtenerString("InicioAdministrador", "MSJ_RUTA_BACKUP_VACIA");

            btnItemRestore.Text = I18n.obtenerString("InicioAdministrador", "btnItemRestore");
            tabItemRestore.Text = I18n.obtenerString("InicioAdministrador", "tabItemRestore");
            lblRutaRestore.Text = I18n.obtenerString("InicioAdministrador", "lblRutaRestore");
            btnRestore.Text = I18n.obtenerString("InicioAdministrador", "btnRestore");

            MSJ_RESTORE_EXITOSO = I18n.obtenerString("InicioAdministrador", "MSJ_RESTORE_EXITOSO");
            MSJ_RESTORE_ERROR = I18n.obtenerString("InicioAdministrador", "MSJ_RESTORE_ERROR");
            MSJ_RESTORE_INVALIDO = I18n.obtenerString("InicioAdministrador", "MSJ_RESTORE_INVALIDO");
        }

        private void generarInicioAdministrador()
        {
            this.Text = I18n.obtenerString("InicioAdministrador", "InicioAdministradorForm");
            btnLogout.Text = I18n.obtenerString("InicioAdministrador", "btnLogout");
            lblUsuario.Text = I18n.obtenerString("InicioAdministrador", "lblUsuario");
            msjInfo = I18n.obtenerString("InicioAdministrador", "mensajeBoxInfo");
            msjError = I18n.obtenerString("InicioAdministrador", "mensajeBoxError");
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            try
            {
                BackupRestoreBLL.obtenerRestore(rutaRestore);
                bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.RESTORE_BD, "Ruta de restore: " + rutaRestore);
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_RESTORE_EXITOSO, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblValorRutaRestore.Text = "";
                rutaRestore = "";
                btnRestore.Enabled = false;
            }
            catch (Excepcion ne)
            {
                bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.EXCEPCION_BLL_RESTORE, ne.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_RESTORE_ERROR, msjError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sideBarPanelBD_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemBackup.Visible = true;
            tabItemRestore.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemBackup;
        }

        private void btnItemBackup_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemBackup.Visible = true;
            tabItemRestore.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemBackup;
        }

        private void btnItemRestore_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemBackup.Visible = true;
            tabItemRestore.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemRestore;
        }

        private void sideBarPanelDV_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = true;
            tabItemDVV.Visible = true;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemDVH;
        }

        private void btnItemDVH_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = true;
            tabItemDVV.Visible = true;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemDVH;
            limpiarCampos();
        }

        private void btnItemDVV_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = true;
            tabItemDVV.Visible = true;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemDVV;
        }

        private void sideBarPanelCripto_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemCripto.Visible = true;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            superTabCtrol.SelectedTab = tabItemCripto;
            limpiarCampos();
        }

        private void sideBarPanelUsuario_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            tabItemCripto.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            superTabCtrol.SelectedTab = tabItemAgregar;
            limpiarCampos();
        }

        private void btnRutaBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowseRutaBackup.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(folderBrowseRutaBackup.SelectedPath))
                {
                    rutaBackup = folderBrowseRutaBackup.SelectedPath;
                    lblValorRutaBackup.Text = rutaBackup;
                    btnBackup.Enabled = true;
                }
                else
                {
                    MessageBox.Show(MSJ_RUTA_BACKUP_VACIA, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            try
            {
                String backup = BackupRestoreBLL.crearBackup(rutaBackup);
                //string detalle = "Ruta de backup: " + rutaBackup + "\\" + backup;
                string detalle = rutaBackup;
                bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.BACKUP_BD, detalle);
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_BACKUP_EXITOSO + rutaBackup + "\\" + backup, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rutaBackup = "";
                lblValorRutaBackup.Text = "";
                btnBackup.Enabled = false;
            }
            catch (Excepcion ne)
            {
                bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.EXCEPCION_BLL_BACKUP, ne.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_BACKUP_ERROR, msjError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRutaRestore_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileRutaRestore.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(openFileRutaRestore.FileName) && Validacion.esBackupValido(openFileRutaRestore.FileName))
                {
                    rutaRestore = openFileRutaRestore.FileName;
                    lblValorRutaRestore.Text = rutaRestore;
                    btnRestore.Enabled = true;
                }
                else
                {
                    MessageBox.Show(MSJ_RESTORE_INVALIDO, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void radioUsuarioDVH_CheckedChanged(object sender, EventArgs e)
        {
            lblUsuarioDVH.Visible = true;
            txtUsuarioDVH.Text = "";
            txtUsuarioDVH.Visible = true;
        }

        private void radioTodosDVH_CheckedChanged(object sender, EventArgs e)
        {
            lblUsuarioDVH.Visible = false;
            txtUsuarioDVH.Visible = false;
        }

        private void btnVerificarDVH_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            if (radioUsuarioDVH.Checked)
            {
                if (!String.IsNullOrEmpty(txtUsuarioDVH.Text) && Validacion.esNumeroValido(txtUsuarioDVH.Text))
                {
                    if (validarUsuario(txtUsuarioDVH.Text))
                        MessageBox.Show(MSJ_VERIFICACION_EXITOSA, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.DVH, "Error en legajo: " + txtUsuarioDVH.Text);
                        BitacoraBLL.registrarBitacora(bitacora);
                        MessageBox.Show(MSJ_VERIFICACION_ERRONEA, msjError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(MSJ_USUARIO_VACIO, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioTodosDVH.Checked)
            {
                List<Usuario> listaUsuarios = seguridad.verificarHorizontal();
                if (listaUsuarios.Count == 0)
                {
                    MessageBox.Show(MSJ_VERIFICACION_EXITOSA, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    String legajos = "";
                    foreach (Usuario usr in listaUsuarios)
                    {
                        legajos = +usr.idUsuario + " - ";
                    }
                    bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.DVH, "Error en legajos: " + legajos);
                    BitacoraBLL.registrarBitacora(bitacora);
                    MessageBox.Show(MSJ_VERIFICACION_ERRONEA_TODOS + legajos, msjError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVerificarDVV_Click(object sender, EventArgs e)
        {
            if (seguridad.verificarConsistencia())
                MessageBox.Show(MSJ_VERIFICACION_EXITOSA, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(MSJ_VERIFICACION_ERRONEA, msjError, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void radioEncriptar_CheckedChanged(object sender, EventArgs e)
        {
            lblResCripto.Visible = false;
            txtResCripto.Visible = false;
        }

        private void radioDesencriptar_CheckedChanged(object sender, EventArgs e)
        {
            lblResCripto.Visible = false;
            txtResCripto.Visible = false;
        }

        private void btnCripto_Click(object sender, EventArgs e)
        {
            if (radioEncriptar.Checked)
            {
                txtResCripto.Text = seguridad.Encriptar(txtTextoCripto.Text);
                txtResCripto.BackColor = Color.LightBlue;
                lblResCripto.Visible = true;
                txtResCripto.Visible = true;
            }
            else if (radioDesencriptar.Checked)
            {
                txtResCripto.Text = seguridad.DesEncriptar(txtTextoCripto.Text);
                txtResCripto.BackColor = Color.LightBlue;
                lblResCripto.Visible = true;
                txtResCripto.Visible = true;
            }
        }

        private Boolean validarUsuario(string user)
        {
            Usuario usuario = usuarioBLL.obtenerUsuarioPorId(Int32.Parse(user));
            String cadena = usuario.estado.ToString() + usuario.email + usuario.clave;
            String digitoVerificador = seguridad.generarSHA(cadena);
            if (digitoVerificador.Equals(usuario.digitoVerificador))
                return true;
            return false;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
            Bitacora bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.LOGOUT, "");
            BitacoraBLL.registrarBitacora(bitacora);
        }

        private void btnGuardar_click(object sender, EventArgs e)
        {
            if(verificarCamposRequeridos(txtNombre.Text, txtApellido.Text, txtDni.Text, txtEmail.Text)){
                Usuario usuario = new Usuario();
                usuario.apellido = txtApellido.Text;
                usuario.ciudad = (!String.IsNullOrEmpty(txtCiudad.Text)) ? txtCiudad.Text : "";
                usuario.clave = seguridad.Encriptar(txtDni.Text);
                usuario.dni = txtDni.Text;
                usuario.fecNac = (dateFecNac.Value != null) ? dateFecNac.Value : new DateTime();
                usuario.domicilio = (!String.IsNullOrEmpty(txtDireccion.Text)) ? txtDireccion.Text : "";
                usuario.email = txtEmail.Text;
                usuario.estado = true;
                usuario.nombre = txtNombre.Text;
                usuario.telefono = txtTelefono.Text;
                usuario.rol = this.comboBoxRol.SelectedItem.ToString();

                String cadena = usuario.estado.ToString() + usuario.email.ToString() + usuario.clave;
                usuario.digitoVerificador = seguridad.generarSHA(cadena);
                String mensaje = null;
                try
                {
                    Usuario usrRes = usuarioBLL.obtenerUsuarioPorDni(usuario.dni);
                    if (usrRes == null)
                    {
                        
                        DialogResult siNoRes = MessageBox.Show(confirmar, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            usuario.idUsuario = usuarioBLL.agregarUsuario(usuario);
                            mensaje = String.Format(msjNuevo, usuario.apellido, usuario.nombre, usuario.idUsuario);
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        mensaje = String.Format(msjExistente, usrRes.apellido, usrRes.nombre, usrRes.dni);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.EXCEPCION_BLL_INS + "Usuario", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            dateFecNac.Value = new DateTime();
            comboBoxRol.DataSource = Enum.GetValues(typeof(EnumRol)).Cast<EnumRol>().ToList();
            txtResCripto.Text = "";
            txtTextoCripto.Text = "";
            txtUsuarioDVH.Text = "";
            txtFiltro.Text = "";
        }

        private Boolean verificarCamposRequeridos(String txtNom, String txtApe, String txtDni, String txtEmail)
        {
            String nombre = (!String.IsNullOrEmpty(txtNom)) ? txtNom : null;

            String apellido = (!String.IsNullOrEmpty(txtApe)) ? txtApe : null;
            String dni = (!String.IsNullOrEmpty(txtDni)) ? txtDni : null;
            if (!Validacion.esNumeroValido(dni))
            {
                MessageBox.Show(formatoDni, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            String email = (!String.IsNullOrEmpty(txtEmail)) ? txtEmail : null;
            if (!Validacion.esEmailValido(email))
            {
                MessageBox.Show(formatoEmail, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(apellido) || String.IsNullOrEmpty(dni) || String.IsNullOrEmpty(email))
            {
                MessageBox.Show(camposRequeridos, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void comboRol_SelectionChangeCommited(object sender, EventArgs e)
        {
            string codRol = this.comboBoxRol.SelectedValue.ToString();
        }

        private void btnItemAgregar_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            superTabCtrol.SelectedTab = tabItemAgregar;
            limpiarCampos();
        }

        private void btnItemModificar_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            superTabCtrol.SelectedTab = tabItemModificar;
            ocultarCampos();
            limpiarCampos();
        }

        private void ocultarCampos()
        {
            txtIdM.Visible = false;
            txtNombreM.Visible = false;
            txtApellidoM.Visible = false;
            txtDniM.Visible = false;
            txtDireccionM.Visible = false;
            txtCiudadM.Visible = false;
            txtEmailM.Visible = false;
            txtTelefonoM.Visible = false;
            dateFecNacM.Visible = false;
            comboBoxRolM.Visible = false;
            chkBoxEstadoM.Visible = false;
            btnGuardarM.Visible = false;
            lblIdM.Visible = false;
            lblApellidoM.Visible = false;
            lblCiudadM.Visible = false;
            lblDireccionM.Visible = false;
            lblDniM.Visible = false;
            lblEmailM.Visible = false;
            lblFecNacM.Visible = false;
            lblNombreM.Visible = false;
            lblRolM.Visible = false;
            lblTelefonoM.Visible = false;
            lblEstadoM.Visible = false;
        }

        private void mostrarCampos()
        {
            txtIdM.Visible = true;
            txtNombreM.Visible = true;
            txtApellidoM.Visible = true;
            txtDniM.Visible = true;
            txtDireccionM.Visible = true;
            txtCiudadM.Visible = true;
            txtEmailM.Visible = true;
            txtTelefonoM.Visible = true;
            dateFecNacM.Visible = true;
            comboBoxRolM.Visible = true;
            chkBoxEstadoM.Visible = true;
            btnGuardarM.Visible = true;
            lblIdM.Visible = true;
            lblApellidoM.Visible = true;
            lblCiudadM.Visible = true;
            lblDireccionM.Visible = true;
            lblDniM.Visible = true;
            lblEmailM.Visible = true;
            lblFecNacM.Visible = true;
            lblNombreM.Visible = true;
            lblRolM.Visible = true;
            lblTelefonoM.Visible = true;
            lblEstadoM.Visible = true;
        }

        private void btnBuscar_click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            String filtro = (!String.IsNullOrEmpty(txtFiltro.Text)) ? txtFiltro.Text : null; ;
            if (radioDni.Checked && filtro != null)
            {
                usuario = usuarioBLL.obtenerUsuarioPorDni(txtFiltro.Text);
            }
            else if (radioId.Checked && filtro != null)
            {
                usuario = usuarioBLL.obtenerUsuarioPorId(Convert.ToInt32(txtFiltro.Text));
            }
            if (usuario != null)
            {
                txtIdM.Text = usuario.idUsuario.ToString();
                txtApellidoM.Text = usuario.apellido;
                txtNombreM.Text = usuario.nombre;
                txtDniM.Text = usuario.dni;
                dateFecNacM.Text = usuario.fecNac.ToShortDateString();
                comboBoxRolM.DataSource = Enum.GetValues(typeof(EnumRol)).Cast<EnumRol>().ToList();
                codRolM = usuario.rol;
                comboBoxRolM.SelectedItem = (EnumRol)Enum.Parse(typeof(EnumRol), codRolM);
                txtDireccionM.Text = usuario.domicilio;
                txtCiudadM.Text = usuario.ciudad;
                txtTelefonoM.Text = usuario.telefono;
                txtEmailM.Text = usuario.email;
                lblClaveM.Text = usuario.clave;
                chkBoxEstadoM.Checked = (usuario.estado == true) ? true : false;
                mostrarCampos();
            }
            else
            {
                MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarM_click(object sender, EventArgs e)
        {
            if(verificarCamposRequeridos(txtNombreM.Text, txtApellidoM.Text, txtDniM.Text, txtEmailM.Text)){
                Usuario usuario = new Usuario();
                usuario.idUsuario = Int32.Parse(txtIdM.Text);
                usuario.apellido = txtApellidoM.Text;
                usuario.nombre = txtNombreM.Text;
                usuario.fecNac = dateFecNacM.Value;
                usuario.rol = codRolM;
                usuario.domicilio = txtDireccionM.Text;
                usuario.ciudad = txtCiudadM.Text;
                usuario.telefono = txtTelefonoM.Text;
                usuario.email = txtEmailM.Text;
                usuario.estado = chkBoxEstadoM.Checked;
                usuario.dni = txtDniM.Text;
                String cadena = usuario.estado.ToString() + usuario.email.ToString() + lblClaveM.Text;
                usuario.digitoVerificador = seguridad.generarSHA(cadena);
                usuario.clave = lblClaveM.Text;

                try{
                    if(!usuario.estado){
                        DialogResult siNoBaja = MessageBox.Show(confirmarBaja, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoBaja.Equals(DialogResult.Yes))
                        {
                            usuarioBLL.actualizarUsuario(usuario);
                            Bitacora bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.BAJA_USUARIO, "Usuario: " + usuario.idUsuario);
                            BitacoraBLL.registrarBitacora(bitacora);
                        }
                    }else{
                        DialogResult siNoRes = MessageBox.Show(confirmar, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                            usuarioBLL.actualizarUsuario(usuario);
                    }
                }catch(Exception ex){
                    Bitacora bitacora = new Bitacora(Convert.ToInt16(lblUsuarioLogueado.Text), Constantes.ROL_ADMINISTRADOR, DateTime.Now, Constantes.EXCEPCION_BLL_UPD + " usuario", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private void comboRolM_SelectionChangeCommited(object sender, EventArgs e)
        {
           codRolM = this.comboBoxRolM.SelectedValue.ToString();
        }

        private void tabItemAgregar_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            superTabCtrol.SelectedTab = tabItemAgregar;
            limpiarCampos();
        }

        private void tabItemModificar_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            superTabCtrol.SelectedTab = tabItemModificar;
            ocultarCampos();
            limpiarCampos();
        }

        private void btnItemConsultar_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            superTabCtrol.SelectedTab = tabItemConsultar;
            limpiarCampos();
            gridConsultaUsuario.Visible = false;
            cmbRolC.Visible = false;
        }

        private void tabItemConsultar_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemModificar.Visible = true;
            tabItemConsultar.Visible = true;
            superTabCtrol.SelectedTab = tabItemConsultar;
            limpiarCampos();
            gridConsultaUsuario.Visible = false;
            cmbRolC.Visible = false;
        }

        private void btnBuscarC_Click(object sender, EventArgs e)
        {
            gridConsultaUsuario.Rows.Clear();
            Usuario usuario = new Usuario();
            List<Usuario> usrList = new List<Usuario>();
            if (radioDniC.Checked && !String.IsNullOrEmpty(txtFiltroC.Text))
            {
                usuario = usuarioBLL.obtenerUsuarioPorDni(txtFiltroC.Text);
                if(usuario!=null)
                    usrList.Add(usuario);
            }
            else if (radioIdC.Checked && !String.IsNullOrEmpty(txtFiltroC.Text))
            {
                usuario = usuarioBLL.obtenerUsuarioPorId(Int32.Parse(txtFiltroC.Text));
                if (usuario != null)
                    usrList.Add(usuario);
            }
            else if (radioRolC.Checked)
            {
                string rol = cmbRolC.SelectedItem.ToString();
                usrList = usuarioBLL.obtenerUsuariosPorRol(rol);
            }
            else if (radioTodosC.Checked)
            {
                usrList = usuarioBLL.obtenerUsuarios();
            }
            if (usrList != null && usrList.Count > 0)
            {
                gridConsultaUsuario.Visible = true;
                for (int i = 0; i < usrList.Count; i++)
                {
                    gridConsultaUsuario.Rows.Add(1);
                    gridConsultaUsuario.Rows[i].Cells["hId"].Value = usrList[i].idUsuario.ToString();
                    gridConsultaUsuario.Rows[i].Cells["hApellido"].Value = usrList[i].apellido;
                    gridConsultaUsuario.Rows[i].Cells["hNombre"].Value = usrList[i].nombre;
                    gridConsultaUsuario.Rows[i].Cells["hDni"].Value = usrList[i].dni;
                    gridConsultaUsuario.Rows[i].Cells["hFecNac"].Value = usrList[i].fecNac.ToShortDateString();
                    gridConsultaUsuario.Rows[i].Cells["hDireccion"].Value = usrList[i].domicilio;
                    gridConsultaUsuario.Rows[i].Cells["hCiudad"].Value = usrList[i].ciudad;
                    gridConsultaUsuario.Rows[i].Cells["hTelefono"].Value = usrList[i].telefono;
                    gridConsultaUsuario.Rows[i].Cells["hEmail"].Value = usrList[i].email;
                    gridConsultaUsuario.Rows[i].Cells["hRol"].Value = usrList[i].rol;
                    gridConsultaUsuario.Rows[i].Cells["hEstado"].Value = usrList[i].estado;
                    gridConsultaUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
            }
            else
            {
                gridConsultaUsuario.Visible = false;
                MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void radioDniC_Click(object sender, EventArgs e)
        {
            if (radioDniC.Checked)
            {
                txtFiltroC.Text = "";
                txtFiltroC.Visible = true;
                cmbRolC.Visible = false;
            }
        }

        private void radioIdC_Click(object sender, EventArgs e)
        {
            if(radioIdC.Checked)
            {
                txtFiltroC.Text = "";
                txtFiltroC.Visible = true;
                cmbRolC.Visible = false;
            }
        }

        private void radioRolC_Click(object sender, EventArgs e)
        {
            if(radioRolC.Checked)
            {
                txtFiltroC.Visible = false;
                cmbRolC.DataSource = Enum.GetValues(typeof(EnumRol)).Cast<EnumRol>().ToList();
                cmbRolC.Visible = true;
            }
        }

        private void radioTodosC_Click(object sender, EventArgs e)
        {
            if(radioTodosC.Checked)
            {
                txtFiltroC.Visible = false;
                cmbRolC.Visible = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButtonFechaConsultarB_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}