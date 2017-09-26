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

            cargarComboRol(cmbRolConsultarB);
            cargarComboEvento(cmbEventoConsultarB);
        }

        private void cargarComboEvento(DevComponents.DotNetBar.Controls.ComboBoxEx comboBox)
        {
            comboBox.DataSource = Enum.GetValues(typeof(EnumEvento)).Cast<EnumEvento>().ToList();
            if (comboBox.Items.Count != 0)
            {
                string evento = comboBox.SelectedValue.ToString();
            }
            else
            {
                comboBox.DataSource = null;
            }
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
            sideBarPanelBitacora.Text = I18n.obtenerString("InicioAdministrador", "bitacora");
            btnItemConsultarB.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            tabItemConsultarB.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            groupBoxConsultarB.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "buscarPor")).Append(Constantes.DOS_PUNTOS).ToString();
            radioButtonRolConsularB.Text = I18n.obtenerString("InicioAdministrador", "rol");
            radioButtonFechaConsultarB.Text = I18n.obtenerString("InicioAdministrador", "fecha");
            radioButtonEventoConsultarB.Text = I18n.obtenerString("InicioAdministrador", "evento");
            dataGridViewTextBoxColumnIdConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "id");
            dataGridViewTextBoxColumnRoleConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "rol");
            dataGridViewTextBoxColumnFechaConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "fecha");
            dataGridViewTextBoxColumnDetalleConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "detalle");
            
            lblHastaConsultarB.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "fechaHasta")).Append(Constantes.DOS_PUNTOS).ToString();
            lblDesdeConsultarB.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "fechaDesde")).Append(Constantes.DOS_PUNTOS).ToString();
            btnBuscarB.Text = I18n.obtenerString("InicioAdministrador", "buscar");
        }

        private void generarUsuariosStrings()
        {
            sideBarPanelUsuario.Text = I18n.obtenerString("InicioAdministrador", "usuarios");
            btnItemAgregar.Text = I18n.obtenerString("InicioAdministrador", "agregar");
            tabItemAgregar.Text = I18n.obtenerString("InicioAdministrador", "agregar");
            btnItemModificar.Text = I18n.obtenerString("InicioAdministrador", "editar");
            tabItemModificar.Text = I18n.obtenerString("InicioAdministrador", "editar");
            btnItemConsultar.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            tabItemConsultar.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            generarAgregarUsuariosStrings();
            generarModificarUsuariosStrings();
            generarConsultarUsuarioStrings();
        }



        private void generarAgregarUsuariosStrings()
        {
            lblApellido.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "apellido")).Append(Constantes.DOS_PUNTOS).ToString();
            lblNombre.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            lblDni.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "dni")).Append(Constantes.DOS_PUNTOS).ToString();
            lblFecNac.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "fecNac")).Append(Constantes.DOS_PUNTOS).ToString();
            lblDireccion.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "direccion")).Append(Constantes.DOS_PUNTOS).ToString();
            lblCiudad.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "ciudad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblTelefono.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "telefono")).Append(Constantes.DOS_PUNTOS).ToString();
            lblEmail.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "email")).Append(Constantes.DOS_PUNTOS).ToString();
            btnGuardar.Text = I18n.obtenerString("InicioAdministrador", "guardar");
            fecNacInvalida = I18n.obtenerString("Mensaje", "fecNacInvalida");
            formatoEmail = I18n.obtenerString("Mensaje", "formatoEmail");
            formatoDni = I18n.obtenerString("Mensaje", "formatoDni");
            camposRequeridos = I18n.obtenerString("Mensaje", "nombreApellidoDniEmailRequeridos");
            confirmar = I18n.obtenerString("Mensaje", "confirmar");
            msjExistente = I18n.obtenerString("Mensaje", "usuarioYaExiste");
            msjNuevo = I18n.obtenerString("Mensaje", "usuarioNuevo");
            msjConfirmar = I18n.obtenerString("Mensaje", "confirmar");
            msjInfo = I18n.obtenerString("Mensaje", "info");
            msjError = I18n.obtenerString("Mensaje", "error");
        }

        private void generarModificarUsuariosStrings()
        {
            lblIdM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "id")).Append(Constantes.DOS_PUNTOS).ToString();
            lblApellidoM.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "apellido")).Append(Constantes.DOS_PUNTOS).ToString();
            lblNombreM.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            lblDniM.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "dni")).Append(Constantes.DOS_PUNTOS).ToString();
            lblFecNacM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "fecNac")).Append(Constantes.DOS_PUNTOS).ToString();
            lblDireccionM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "direccion")).Append(Constantes.DOS_PUNTOS).ToString();
            lblCiudadM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "ciudad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblTelefonoM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "telefono")).Append(Constantes.DOS_PUNTOS).ToString();
            lblEmailM.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "email")).Append(Constantes.DOS_PUNTOS).ToString();
            lblEstadoM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "estado")).Append(Constantes.DOS_PUNTOS).ToString();
            radioGroupBuscar.Text = I18n.obtenerString("InicioAdministrador", "buscar");
            btnBuscarM.Text = I18n.obtenerString("InicioAdministrador", "buscar");
            btnGuardarM.Text = I18n.obtenerString("InicioAdministrador", "guardar");
            ningunRegistro = I18n.obtenerString("Mensaje", "ningunRegistro");
            confirmarBaja = I18n.obtenerString("Mensaje", "confirmarBaja");
        }

        private void generarConsultarUsuarioStrings()
        {
            groupBuscarC.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "buscarPor")).Append(Constantes.DOS_PUNTOS).ToString();
            radioIdC.Text = I18n.obtenerString("InicioAdministrador", "id");
            radioDniC.Text = I18n.obtenerString("InicioAdministrador", "dni");
            radioRolC.Text = I18n.obtenerString("InicioAdministrador", "rol");
            radioTodosC.Text = I18n.obtenerString("InicioAdministrador", "todos");
            btnBuscarC.Text = I18n.obtenerString("InicioAdministrador", "buscar");

            gridConsultaUsuario.Columns["HId"].HeaderText = I18n.obtenerString("InicioAdministrador", "id");
            gridConsultaUsuario.Columns["HApellido"].HeaderText = I18n.obtenerString("InicioAdministrador", "apellido");
            gridConsultaUsuario.Columns["HNombre"].HeaderText = I18n.obtenerString("InicioAdministrador", "nombre");
            gridConsultaUsuario.Columns["HDni"].HeaderText = I18n.obtenerString("InicioAdministrador", "dni");
            gridConsultaUsuario.Columns["HFecNac"].HeaderText = I18n.obtenerString("InicioAdministrador", "fecNac");
            gridConsultaUsuario.Columns["HDireccion"].HeaderText = I18n.obtenerString("InicioAdministrador", "direccion");
            gridConsultaUsuario.Columns["HCiudad"].HeaderText = I18n.obtenerString("InicioAdministrador", "ciudad");
            gridConsultaUsuario.Columns["HTelefono"].HeaderText = I18n.obtenerString("InicioAdministrador", "telefono");
            gridConsultaUsuario.Columns["HEmail"].HeaderText = I18n.obtenerString("InicioAdministrador", "email");
            gridConsultaUsuario.Columns["HRol"].HeaderText = I18n.obtenerString("InicioAdministrador", "rol");
            gridConsultaUsuario.Columns["HEstado"].HeaderText = I18n.obtenerString("InicioAdministrador", "estado");

        }

        private void generaraCriptoStrings()
        {
            sideBarPanelCripto.Text = I18n.obtenerString("InicioAdministrador", "criptografia");
            tabItemCripto.Text = I18n.obtenerString("InicioAdministrador", "criptografia");
            radioGroupOpcion.Text = I18n.obtenerString("InicioAdministrador", "opcion");
            radioEncriptar.Text = I18n.obtenerString("InicioAdministrador", "encriptar");
            radioDesencriptar.Text = I18n.obtenerString("InicioAdministrador", "desencriptar");
            lblTextoCripto.Text = I18n.obtenerString("InicioAdministrador", "texto") + ":";
            lblResCripto.Text = I18n.obtenerString("InicioAdministrador", "resultado") + ":";
        }

        private void generarDVStrings()
        {
            sideBarPanelDV.Text = I18n.obtenerString("InicioAdministrador", "dv");
            btnItemDVH.Text = I18n.obtenerString("InicioAdministrador", "dvhorizontal");
            tabItemDVH.Text = I18n.obtenerString("InicioAdministrador", "dvhorizontal");
            lblUsuarioDVH.Text = I18n.obtenerString("InicioAdministrador", "usuario") + ":";
            radioGroupDVH.Text = I18n.obtenerString("InicioAdministrador", "dvhUsuarios");
            radioUsuarioDVH.Text = I18n.obtenerString("InicioAdministrador", "usuario");
            radioTodosDVH.Text = I18n.obtenerString("InicioAdministrador", "todosUsuarios");
            btnVerificarDVH.Text = I18n.obtenerString("InicioAdministrador", "verificar");

            btnItemDVV.Text = I18n.obtenerString("InicioAdministrador", "dvVertical");
            tabItemDVV.Text = I18n.obtenerString("InicioAdministrador", "dvVertical");
            radioGroupDVV.Text = I18n.obtenerString("InicioAdministrador", "dvvUsuarios");
            btnVerificarDVV.Text = I18n.obtenerString("InicioAdministrador", "verificar");

            MSJ_VERIFICACION_EXITOSA = I18n.obtenerString("Mensaje", "verificacionExitosa");
            MSJ_VERIFICACION_ERRONEA = I18n.obtenerString("Mensaje", "verificacionErronea");
            MSJ_VERIFICACION_ERRONEA_TODOS = I18n.obtenerString("Mensaje", "verificacionErroneaTodos");
            MSJ_USUARIO_VACIO = I18n.obtenerString("Mensaje", "usuarioVacio");
        }

        private void generarGestionBDStrings()
        {
            sideBarPanelBD.Text = I18n.obtenerString("InicioAdministrador", "gestionBD");
            btnItemBackup.Text = I18n.obtenerString("InicioAdministrador", "backup");
            tabItemBackup.Text = I18n.obtenerString("InicioAdministrador", "backup");
            lblRutaBackup.Text = I18n.obtenerString("InicioAdministrador", "rutaBackup");
            btnBackup.Text = I18n.obtenerString("InicioAdministrador", "crearCopia");
            MSJ_BACKUP_EXITOSO = I18n.obtenerString("Mensaje", "backupExitoso");
            MSJ_BACKUP_ERROR = I18n.obtenerString("Mensaje", "backupError");
            MSJ_RUTA_BACKUP_VACIA = I18n.obtenerString("Mensaje", "rutaBackupVacia");

            btnItemRestore.Text = I18n.obtenerString("InicioAdministrador", "restore");
            tabItemRestore.Text = I18n.obtenerString("InicioAdministrador", "restore");
            lblRutaRestore.Text = I18n.obtenerString("InicioAdministrador", "rutaRestore");
            btnRestore.Text = I18n.obtenerString("InicioAdministrador", "restaurarCopia");

            MSJ_RESTORE_EXITOSO = I18n.obtenerString("Mensaje", "restoreExitoso");
            MSJ_RESTORE_ERROR = I18n.obtenerString("Mensaje", "restoreError");
            MSJ_RESTORE_INVALIDO = I18n.obtenerString("Mensaje", "restoreInvalido");
        }

        private void generarInicioAdministrador()
        {
            this.Text = I18n.obtenerString("InicioAdministrador", "inicioAdministradorForm");
            btnLogout.Text = I18n.obtenerString("InicioAdministrador", "logout");
            lblUsuario.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "usuario")).Append(Constantes.DOS_PUNTOS).ToString();
            msjInfo = I18n.obtenerString("Mensaje", "info");
            msjError = I18n.obtenerString("Mensaje", "error");
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
            tabItemConsultarB.Visible = false;
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
                gridConsultaUsuario.Visible = false;
            }
        }

        private void radioIdC_Click(object sender, EventArgs e)
        {
            if(radioIdC.Checked)
            {
                txtFiltroC.Text = "";
                txtFiltroC.Visible = true;
                cmbRolC.Visible = false;
                gridConsultaUsuario.Visible = false;
            }
        }

        private void radioRolC_Click(object sender, EventArgs e)
        {
            if(radioRolC.Checked)
            {
                txtFiltroC.Visible = false;
                cmbRolC.DataSource = Enum.GetValues(typeof(EnumRol)).Cast<EnumRol>().ToList();
                cmbRolC.Visible = true;
                gridConsultaUsuario.Visible = false;
            }
        }

        private void radioTodosC_Click(object sender, EventArgs e)
        {
            if(radioTodosC.Checked)
            {
                txtFiltroC.Visible = false;
                cmbRolC.Visible = false;
                gridConsultaUsuario.Visible = false;
            }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void radioRolConsultarB_Click(object sender, EventArgs e)
        {
            cmbEventoConsultarB.Visible = false;
            lblDesdeConsultarB.Visible = false;
            dateHastaConsultaB.Visible = false;
            lblHastaConsultarB.Visible = false;
            dateDesdeConsultaB.Visible = false;

            cmbRolConsultarB.Visible = true;

            dataConsultarB.Visible = false;
        }

        private void btnItemConsultarB_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = true;
            superTabCtrol.SelectedTab = tabItemConsultarB;

            radioButtonEventoConsultarB.Checked = true;

            dataConsultarB.Visible = false;

            radioEventoConsultarB_Click(null, null);
        }

        private void radioFechaConsultarB_Click(object sender, EventArgs e)
        {
            cmbEventoConsultarB.Visible = false;
            lblDesdeConsultarB.Visible = true;
            dateHastaConsultaB.Visible = true;
            lblHastaConsultarB.Visible = true;
            dateDesdeConsultaB.Visible = true;

            cmbRolConsultarB.Visible = false;

            dataConsultarB.Visible = false;
        }

        private void radioEventoConsultarB_Click(object sender, EventArgs e)
        {
            cmbRolConsultarB.Visible = false;
            lblDesdeConsultarB.Visible = false;
            dateHastaConsultaB.Visible = false;
            lblHastaConsultarB.Visible = false;
            dateDesdeConsultaB.Visible = false;

            cmbEventoConsultarB.Visible = true;

            dataConsultarB.Visible = false;

        }

        private void sideBarPanelBitacora_Click(object sender, EventArgs e)
        {
            superTabCtrol.Visible = true;

            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemModificar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;

            tabItemConsultarB.Visible = true;
            
            superTabCtrol.SelectedTab = tabItemConsultarB;

        }

        private void btnBuscarB_Click(object sender, EventArgs e)
        {
            List<Bitacora> listaBitacoras = new List<Bitacora>();

            if (radioButtonEventoConsultarB.Checked)
            {
                listaBitacoras = BitacoraBLL.obtenerBitacorasPorEvento(cmbEventoConsultarB.Text);
            } else if(radioButtonRolConsularB.Checked)
            {
                listaBitacoras = BitacoraBLL.obtenerBitacorasPorRol(cmbRolConsultarB.Text);
            }
            else if (radioButtonFechaConsultarB.Checked) 
            {
                if (!dateDesdeConsultaB.IsEmpty && !dateHastaConsultaB.IsEmpty )
                {
                    listaBitacoras = BitacoraBLL.obtenerBitacorasPorFechas(dateDesdeConsultaB.Value, dateHastaConsultaB.Value);
                }
            }


            if (listaBitacoras != null && listaBitacoras.Count > 0)
            {
                dataConsultarB.Visible = true;
                for (int i = 0; i < listaBitacoras.Count; i++)
                {
                    dataConsultarB.Rows.Add(1);
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnIdConsultarB"].Value = listaBitacoras[i].idBitacora.ToString();
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnRoleConsultarB"].Value = listaBitacoras[i].rol;
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnEventoConsultarB"].Value = listaBitacoras[i].evento;
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnFechaConsultarB"].Value = listaBitacoras[i].fecha;
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnDetalleConsultarB"].Value = listaBitacoras[i].detalle;
                    dataConsultarB.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
            }
            else
            {
                dataConsultarB.Visible = false;
                MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

           
        }

    }
}