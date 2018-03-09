using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Base;
using BLL.IBLL;
using BLL;
using OEVApp.i18n;
using Utils;
using Entities;
using Base.BLL;
using DevComponents.DotNetBar.Metro;

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
        ISeguridad seguridad = new Seguridad();
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        IRolBLL rolBLL = new RolBLL();
        IPatenteBLL patenteBLL = new PatenteBLL();
        IFamiliaBLL familiaBLL = new FamiliaBLL();
        IFamiliaPatenteBLL familiaPatenteBLL = new FamiliaPatenteBLL();
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
        String requerido = null;
        String elementoNuevo = null;
        String elementoExistente = null;
        Usuario usuarioLogueado = null;
        Rol rolUsrLogueado = null;
        
        public Administrador(Usuario usuario, string idioma,List<Familia> familiasPermitidas, List<Patente> patentesPermitidas)
        {
            InitializeComponent();
            lblUsuarioLogueado.Text = usuario.id + " - " + usuario.nombre + " " + usuario.apellido;
            this.usuarioLogueado = usuario;
            rolUsrLogueado = rolBLL.obtenerRolPorId(Convert.ToInt32(usuarioBLL.obtnerRolPorIdUsuario(usuario.id)));
            cargarFuncionalidades(familiasPermitidas, patentesPermitidas);
            cargarCombos();
            I18n.cargarIdioma((EnumIdioma)Enum.Parse(typeof(EnumIdioma), idioma));
            generarInicioAdministrador();
            generarGestionBDStrings();
            generarDVStrings();
            generaraCriptoStrings();
            generarUsuariosStrings();
            generarBitacoraStrings();
            generarPermisosStrings();
           
        }

        public Administrador()
        {
            // TODO: Complete member initialization
        }

        private void cargarFuncionalidades(List<Familia> familiasPermitidas, List<Patente> patentesPermitidas)
        {

            List<Patente> listaPatentes = patenteBLL.obtenerPatentes();
            List<Patente> noPermPatentes = listaPatentes.Where(l => !patentesPermitidas.Any(p => l.id == p.id)).ToList();
            Dictionary<String, BaseItem> patentesItems = new Dictionary<string, BaseItem>();
            patentesItems.Add("COPIA BD", btnItemBackup);
            patentesItems.Add("RESTAURAR BD", btnItemRestore);
            patentesItems.Add("HORIZONTAL", btnItemDVH);
            patentesItems.Add("VERTICAL", btnItemDVV);
            patentesItems.Add("AGREGAR USUARIO", btnItemAgregar);
            patentesItems.Add("EDITAR USUARIO", btnItemEditar);
            patentesItems.Add("CONSULTAR USUARIO", btnItemConsultar);
            patentesItems.Add("CONSULTAR BITACORA", btnItemConsultarB);
            patentesItems.Add("FUNCIONES", btnItemFunc);
            patentesItems.Add("ASIGNAR", btnItemAsignar);
            patentesItems.Add("CONSULTAR PERMISOS", btnItemPermConsultar);

            //foreach (String item in patentesItems.Keys)
            //{
            //    foreach (Patente pat in noPermPatentes)
            //    {
            //        if (item.Contains(pat.descripcion))
            //            patentesItems[item].Visible = false;
            //    }
            //}
            //////noPermPatentes.ForEach(n => sideBar1.ExpandedPanel.SubItems.Remove(patentesItems[n.descripcion].Name));

            List<Familia> listaFamilias = familiaBLL.obtenerFamilias();
            List<Familia> noPermFamilias = listaFamilias.Where(l => !familiasPermitidas.Any(f => l.id == f.id)).ToList();          
            Dictionary<String,SideBarPanelItem> familiasSideBars = new Dictionary<string,SideBarPanelItem>();
            familiasSideBars.Add("GESTION BD", sideBarPanelBD);
            familiasSideBars.Add("DIGITO VERIFICADOR", sideBarPanelDV);
            familiasSideBars.Add("CRIPTOGRAFIA", sideBarPanelCripto);
            familiasSideBars.Add("GESTION USUARIOS", sideBarPanelUsuario);
            familiasSideBars.Add("BITACORA", sideBarPanelBitacora);
            familiasSideBars.Add("PERMISOS", sideBarPanelPermisos);

            //noPermFamilias.ForEach(n => sideBar1.Panels.Remove(familiasSideBars[n.descripcion].Name));

        }

        private void generarPermisosStrings()
        {
            sideBarPanelPermisos.Text = I18n.obtenerString("InicioAdministrador", "permisos");
            btnItemFunc.Text = I18n.obtenerString("InicioAdministrador", "funciones");
            btnItemAsignar.Text = I18n.obtenerString("InicioAdministrador", "asignar");
            btnItemPermConsultar.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            //tab Funciones
            tabItemFunc.Text = I18n.obtenerString("InicioAdministrador", "funciones");
            radioGroupABM.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "opciones")).Append(Constantes.DOS_PUNTOS).ToString();
            radioAgregar.Text = I18n.obtenerString("InicioAdministrador", "agregar");
            radioEditar.Text = I18n.obtenerString("InicioAdministrador", "editar");
            radioConsultar.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            radioGroupPatFam.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "seleccionar")).Append(Constantes.DOS_PUNTOS).ToString();
            radioPatente.Text = I18n.obtenerString("InicioAdministrador", "patente");
            radioFamilia.Text = I18n.obtenerString("InicioAdministrador", "familia");
            radioRol.Text = I18n.obtenerString("InicioAdministrador", "rol");
            lblFuncGestion.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "gestionFunciones")).Append(Constantes.DOS_PUNTOS).ToString();
            lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
            lblFuncNombre.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioAdministrador", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            btnFuncGuardar.Text = I18n.obtenerString("InicioAdministrador", "guardar");
            btnFuncBuscar.Text = I18n.obtenerString("InicioAdministrador", "buscar");
            gridFuncionesHeaderPatente.HeaderText = I18n.obtenerString("InicioAdministrador", "patente");
            gridFuncionesHeaderEstadoPatente.Text = I18n.obtenerString("InicioAdministrador", "estadoPatente");
            gridFuncionesHeaderFamilia.HeaderText = I18n.obtenerString("InicioAdministrador", "familia");
            gridFuncionesHeaderEstadoFamilia.Text = I18n.obtenerString("InicioAdministrador", "estadoFamilia");
            gridFuncionesHeaderRol.HeaderText = I18n.obtenerString("InicioAdministrador", "rol");
            gridFuncionesHeaderEstadoRol.Text = I18n.obtenerString("InicioAdministrador", "estadoRol");
            requerido = I18n.obtenerString("Mensaje", "requerido");
            elementoNuevo = I18n.obtenerString("Mensaje", "elementoNuevo");
            elementoExistente = I18n.obtenerString("Mensaje", "elementoExistente");
            //tab Asignar
            tabItemAsignar.Text = I18n.obtenerString("InicioAdministrador", "asignar");
            radioGroupOp1.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "a")).Append(Constantes.DOS_PUNTOS).ToString();
            radioOp1Usuario.Text = I18n.obtenerString("InicioAdministrador", "usuario");
            radioOp1Rol.Text = I18n.obtenerString("InicioAdministrador", "rol");
            radioOp1Patente.Text = I18n.obtenerString("InicioAdministrador", "patente");
            lblPermitir.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "usuario")).Append(Constantes.DOS_PUNTOS).ToString();
            switchBtn.OnText = I18n.obtenerString("InicioAdministrador", "permitir");
            switchBtn.OffText = I18n.obtenerString("InicioAdministrador", "denegar");
            radioGroupOp2.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "accederA")).Append(Constantes.DOS_PUNTOS).ToString();
            radioOp2Familia.Text = I18n.obtenerString("InicioAdministrador", "familia");
            radioOp2Patente.Text = I18n.obtenerString("InicioAdministrador", "patente");
            radioOp2Rol.Text = I18n.obtenerString("InicioAdministrador", "rol");
            lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "familia")).Append(Constantes.DOS_PUNTOS).ToString();
            lblDescripcion.ForeColor = Color.Navy;
            lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "usuario"), I18n.obtenerString("InicioAdministrador", "familia"));
            btnAsignarGuardar.Text = I18n.obtenerString("InicioAdministrador", "guardar");
            //tab Consultar Permisos
            tabItemPermConsultar.Text = I18n.obtenerString("InicioAdministrador", "consultar");
            radioGroupAsignar.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "asignar")).Append(Constantes.DOS_PUNTOS).ToString();
            radioPermCUsuario.Text = I18n.obtenerString("InicioAdministrador", "usuario");
            radioPermCRol.Text = I18n.obtenerString("InicioAdministrador", "rol");
            radioPermCPatente.Text = I18n.obtenerString("InicioAdministrador", "patente");
            radioPermCFamilia.Text = I18n.obtenerString("InicioAdministrador", "familia");
            btnPermCBuscar.Text = I18n.obtenerString("InicioAdministrador", "buscar");
            gridPermConsultarHeaderUsuario.Text = I18n.obtenerString("InicioAdministrador", "usuario");
            gridPermConsultarHeaderRol.Text = I18n.obtenerString("InicioAdministrador", "rol");
            gridPermConsultarHeaderPatente.Text = I18n.obtenerString("InicioAdministrador", "patente");
            gridPermConsultarHeaderFamilia.Text = I18n.obtenerString("InicioAdministrador", "familia");
        }

        private void cargarCombos()
        {
            comboBoxRolM.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            cmbEventoConsultarB.DataSource = Enum.GetValues(typeof(EnumEvento)).Cast<EnumEvento>().ToList();
            cmbRolConsultarB.DataSource = rolBLL.obtenerRoles().Select(r => r.descripcion).ToList();
            comboBoxRol.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            comboOp1.DataSource = usuarioBLL.obtenerUsuarios().Where(u => u.estado == true).Select(u => u.apellido + "" + u.nombre + " - DNI: " + u.dni).ToList();
            comboOp2.DataSource = familiaBLL.obtenerFamilias().Where(f => f.estado == true).Select(f => f.descripcion).ToList();
            comboPermC.DataSource = usuarioBLL.obtenerUsuarios().Where(u => u.estado == true).Select(u => u.apellido + "" + u.nombre + " - DNI: " + u.dni).ToList();
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
            //dataGridViewTextBoxColumnIdConsultarB.HeaderText = I18n.obtenerString("InicioAdministrador", "id");
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
            btnItemEditar.Text = I18n.obtenerString("InicioAdministrador", "editar");
            tabItemEditar.Text = I18n.obtenerString("InicioAdministrador", "editar");
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
            gridConsultaUsuario.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            gridConsultaUsuario.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
            gridConsultaUsuario.AutoSize = true;

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
                bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.RESTORE_BD, "Ruta de restore: " + rutaRestore);
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_RESTORE_EXITOSO, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lblValorRutaRestore.Text = "";
                rutaRestore = "";
                btnRestore.Enabled = false;
            }
            catch (Excepcion ne)
            {
                bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_RESTORE, ne.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_RESTORE_ERROR, msjError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sideBarPanelBD_Click(object sender, EventArgs e)
        {
            //superTabCtrol.Visible = true;
            //tabItemBackup.Visible = true;
            //tabItemRestore.Visible = true;
            //tabItemDVH.Visible = false;
            //tabItemDVV.Visible = false;
            //tabItemCripto.Visible = false;
            //tabItemAgregar.Visible = false;
            //tabItemEditar.Visible = false;
            //tabItemConsultar.Visible = false;
            //tabItemConsultarB.Visible = false;
            //tabItemFunc.Visible = false;
            //tabItemAsignar.Visible = false;
            //tabItemPermConsultar.Visible = false;
            //superTabCtrol.SelectedTab = tabItemBackup;
        }

        private void btnItemBackup_Click(object sender, EventArgs e)
        {
            if (btnItemRestore.Visible == false)
                tabItemRestore.Visible = false;
            superTabCtrolAdmin.Visible = true;
            tabItemBackup.Visible = true;
            //tabItemRestore.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemBackup;
            limpiarCampos();
        }

        private void btnItemRestore_Click(object sender, EventArgs e)
        {
            if (btnItemBackup.Visible == false)
                tabItemBackup.Visible = false;
            superTabCtrolAdmin.Visible = true;
            //tabItemBackup.Visible = true;
            tabItemRestore.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemRestore;
            limpiarCampos();
        }

        private void sideBarPanelDV_Click(object sender, EventArgs e)
        {
            //superTabCtrol.Visible = true;
            //tabItemDVH.Visible = true;
            //tabItemDVV.Visible = true;
            //tabItemBackup.Visible = false;
            //tabItemRestore.Visible = false;
            //tabItemCripto.Visible = false;
            //tabItemAgregar.Visible = false;
            //tabItemEditar.Visible = false;
            //tabItemConsultar.Visible = false;
            //tabItemConsultarB.Visible = false;
            //tabItemFunc.Visible = false;
            //tabItemAsignar.Visible = false;
            //tabItemPermConsultar.Visible = false;
            //superTabCtrol.SelectedTab = tabItemDVH;
        }

        private void btnItemDVH_Click(object sender, EventArgs e)
        {
            if (btnItemDVV.Visible == false)
                tabItemDVV.Visible = false;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = true;
            //tabItemDVV.Visible = true;
            tabItemCripto.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemDVH;
            limpiarCampos();
        }

        private void btnItemDVV_Click(object sender, EventArgs e)
        {
            if (btnItemDVH.Visible == false)
                tabItemDVH.Visible = false;
            superTabCtrolAdmin.Visible = true;
            //tabItemDVH.Visible = true;
            tabItemDVV.Visible = true;
            tabItemCripto.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemDVV;
            limpiarCampos();
        }

        private void sideBarPanelCripto_Click(object sender, EventArgs e)
        {
            superTabCtrolAdmin.Visible = true;
            tabItemCripto.Visible = true;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemCripto;
            limpiarCampos();
        }

        private void sideBarPanelUsuario_Click(object sender, EventArgs e)
        {
            //superTabCtrol.Visible = true;
            //tabItemAgregar.Visible = true;
            //tabItemEditar.Visible = true;
            //tabItemConsultar.Visible = true;
            //tabItemCripto.Visible = false;
            //tabItemBackup.Visible = false;
            //tabItemRestore.Visible = false;
            //tabItemDVH.Visible = false;
            //tabItemDVV.Visible = false;
            //tabItemConsultarB.Visible = false;
            //tabItemFunc.Visible = false;
            //tabItemAsignar.Visible = false;
            //tabItemPermConsultar.Visible = false;
            //superTabCtrol.SelectedTab = tabItemAgregar;
            //limpiarCampos();
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
                bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.BACKUP_BD, detalle);
                BitacoraBLL.registrarBitacora(bitacora);
                MessageBox.Show(MSJ_BACKUP_EXITOSO + rutaBackup + "\\" + backup, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rutaBackup = "";
                lblValorRutaBackup.Text = "";
                btnBackup.Enabled = false;
            }
            catch (Excepcion ne)
            {
                bitacora = new Bitacora(usuarioLogueado.id, usuarioBLL.obtnerRolPorIdUsuario(Convert.ToInt32(lblUsuarioLogueado.Text)), DateTime.Now.Date, Constantes.EXCEPCION_BLL_BACKUP, ne.ToString());
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
                        bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.DVH, "Error en legajo: " + txtUsuarioDVH.Text);
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
                        legajos = +usr.id + " - ";
                    }
                    bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.DVH, "Error en legajos: " + legajos);
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
            Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.LOGOUT, "");
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
                //usuario.idRol = rolBLL.obtenerRolPorDesc(this.comboBoxRol.SelectedItem.ToString()).id;

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
                            usuario.id = usuarioBLL.agregarUsuario(usuario);
                            //Asocia al nuevo usuario con el rol seleccionado
                            Rol rol = rolBLL.obtenerRolPorDesc(this.comboBoxRol.SelectedItem.ToString());
                            rol.Agregar(usuario);
                            mensaje = String.Format(msjNuevo, usuario.apellido, usuario.nombre, usuario.id);
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
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + "Usuario", ex.ToString());
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
            comboBoxRol.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            txtResCripto.Text = "";
            txtTextoCripto.Text = "";
            txtUsuarioDVH.Text = "";
            txtFiltro.Text = "";
            txtFuncNombre.Text = "";
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
            if (btnItemEditar.Visible == false)
                tabItemEditar.Visible = false;
            else if (tabItemEditar.Visible == true)
                tabItemEditar.Visible = true;
            if (btnItemConsultar.Visible == false)
                tabItemConsultar.Visible = false;
            else if (btnItemConsultar.Visible == true)
                tabItemConsultar.Visible = true;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            //tabItemEditar.Visible = true;
            //tabItemConsultar.Visible = true;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemAgregar;
            limpiarCampos();
        }

        private void btnItemModificar_Click(object sender, EventArgs e)
        {
            if (btnItemAgregar.Visible == false)
                tabItemAgregar.Visible = false;
            else if (btnItemAgregar.Visible == true)
                tabItemAgregar.Visible = true;
            if (btnItemConsultar.Visible == false)
                tabItemConsultar.Visible = false;
            else if (btnItemConsultar.Visible == true)
                tabItemConsultar.Visible = true;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            //tabItemAgregar.Visible = true;
            tabItemEditar.Visible = true;
            //tabItemConsultar.Visible = true;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemEditar;
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
            String filtro = (!String.IsNullOrEmpty(txtFiltro.Text)) ? txtFiltro.Text : null;
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
                txtIdM.Text = usuario.id.ToString();
                txtApellidoM.Text = usuario.apellido;
                txtNombreM.Text = usuario.nombre;
                txtDniM.Text = usuario.dni;
                dateFecNacM.Text = usuario.fecNac.ToShortDateString();
                comboBoxRolM.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
                comboBoxRolM.SelectedItem = usuarioBLL.obtnerRolPorIdUsuario(usuario.id);
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
                Usuario usuario = new Usuario(Int32.Parse(txtIdM.Text));
                usuario.apellido = txtApellidoM.Text;
                usuario.nombre = txtNombreM.Text;
                usuario.fecNac = dateFecNacM.Value;
                //usuario.idRol = rolBLL.obtenerRolPorDesc(codRolM).id;
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
                        confirmarBaja = String.Format(I18n.obtenerString("Mensaje", "confirmarBaja"), I18n.obtenerString("Mensaje", "usuario"));
                        DialogResult siNoBaja = MessageBox.Show(confirmarBaja, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoBaja.Equals(DialogResult.Yes))
                        {
                            usuarioBLL.actualizarUsuario(usuario);
                            Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.BAJA_USUARIO, "Usuario: " + usuario.id);
                            BitacoraBLL.registrarBitacora(bitacora);
                        }
                    }else{
                        DialogResult siNoRes = MessageBox.Show(confirmar, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            codRolM = this.comboBoxRolM.SelectedValue.ToString().Trim();
                            Rol nuevoRol = rolBLL.obtenerRolPorId(rolBLL.obtenerRolPorDesc(codRolM).id);
                            Usuario viejoUsr = usuarioBLL.obtenerUsuarioPorId(usuario.id);
                            Rol viejoRol = rolBLL.obtenerRolPorId(Int32.Parse(usuarioBLL.obtnerRolPorIdUsuario(viejoUsr.id)));
                            if (nuevoRol.id != viejoRol.id)
                            {
                                //Asocia al usuario con el nuevo rol seleccionado
                                nuevoRol.Agregar(usuario);
                                //Elimina la asociacion del usuario con el rol anterior
                                viejoRol.Eliminar(viejoUsr);
                            }
                            usuarioBLL.actualizarUsuario(usuario);
                        }
                    }
                }catch(Exception ex){
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_UPD + " usuario", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private void comboRolM_SelectionChangeCommited(object sender, EventArgs e)
        {
           codRolM = this.comboBoxRolM.SelectedValue.ToString().Trim();
        }

        private void tabItemAgregar_Click(object sender, EventArgs e)
        {
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemEditar.Visible = true;
            tabItemConsultar.Visible = true;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemAgregar;
            limpiarCampos();
        }

        private void tabItemModificar_Click(object sender, EventArgs e)
        {
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemEditar.Visible = true;
            tabItemConsultar.Visible = true;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemEditar;
            ocultarCampos();
            limpiarCampos();
        }

        private void btnItemConsultar_Click(object sender, EventArgs e)
        {
            if (btnItemAgregar.Visible == false)
                tabItemAgregar.Visible = false;
            else if (btnItemAgregar.Visible == true)
                tabItemAgregar.Visible = true;
            if (btnItemEditar.Visible == false)
                tabItemEditar.Visible = false;
            else if (btnItemEditar.Visible == true)
                btnItemEditar.Visible = true;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            //tabItemAgregar.Visible = true;
            //tabItemEditar.Visible = true;
            tabItemConsultar.Visible = true;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemConsultar;
            limpiarCampos();
            gridConsultaUsuario.Visible = false;
            cmbRolC.Visible = false;
        }

        private void tabItemConsultar_Click(object sender, EventArgs e)
        {
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = true;
            tabItemEditar.Visible = true;
            tabItemConsultar.Visible = true;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemConsultar;
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
                    gridConsultaUsuario.Rows[i].Cells["hId"].Value = usrList[i].id.ToString();
                    gridConsultaUsuario.Rows[i].Cells["hApellido"].Value = usrList[i].apellido;
                    gridConsultaUsuario.Rows[i].Cells["hNombre"].Value = usrList[i].nombre;
                    gridConsultaUsuario.Rows[i].Cells["hDni"].Value = usrList[i].dni;
                    gridConsultaUsuario.Rows[i].Cells["hFecNac"].Value = usrList[i].fecNac.ToShortDateString();
                    gridConsultaUsuario.Rows[i].Cells["hDireccion"].Value = usrList[i].domicilio;
                    gridConsultaUsuario.Rows[i].Cells["hCiudad"].Value = usrList[i].ciudad;
                    gridConsultaUsuario.Rows[i].Cells["hTelefono"].Value = usrList[i].telefono;
                    gridConsultaUsuario.Rows[i].Cells["hEmail"].Value = usrList[i].email;
                    gridConsultaUsuario.Rows[i].Cells["hRol"].Value = rolBLL.obtenerRolPorId(Int32.Parse(usuarioBLL.obtnerRolPorIdUsuario(usrList[i].id))).descripcion;
                    
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
                cmbRolC.DataSource = rolBLL.obtenerRoles().Select(r => r.descripcion).ToList();
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
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = true;
            tabItemFunc.Visible = false;
            tabItemAsignar.Visible = false;
            tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.SelectedTab = tabItemConsultarB;

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
            //superTabCtrol.Visible = true;

            //tabItemDVH.Visible = false;
            //tabItemDVV.Visible = false;
            //tabItemCripto.Visible = false;
            //tabItemAgregar.Visible = false;
            //tabItemEditar.Visible = false;
            //tabItemConsultar.Visible = false;
            //tabItemBackup.Visible = false;
            //tabItemRestore.Visible = false;
            //tabItemConsultarB.Visible = true;
            //tabItemFunc.Visible = false;
            //tabItemAsignar.Visible = false;
            //tabItemPermConsultar.Visible = false;
            //superTabCtrol.SelectedTab = tabItemConsultarB;

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
                dataConsultarB.Rows.Clear();
                for (int i = 0; i < listaBitacoras.Count; i++)
                {
                    dataConsultarB.Rows.Add(1);
                    //dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnIdConsultarB"].Value = listaBitacoras[i].idBitacora.ToString();
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnRoleConsultarB"].Value = listaBitacoras[i].rol;
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnEventoConsultarB"].Value = listaBitacoras[i].evento;
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnFechaConsultarB"].Value = listaBitacoras[i].fecha.Date.ToLongDateString();
                    dataConsultarB.Rows[i].Cells["dataGridViewTextBoxColumnDetalleConsultarB"].Value = listaBitacoras[i].detalle;
                    dataConsultarB.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    dataConsultarB.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                    dataConsultarB.AutoSize = true;
                }
            }
            else
            {
                dataConsultarB.Visible = false;
                MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

           
        }

        private void sideBarPanelPermisos_Click(object sender, EventArgs e)
        {
            //superTabCtrol.Visible = true;
            //tabItemAgregar.Visible = false;
            //tabItemEditar.Visible = false;
            //tabItemConsultar.Visible = false;
            //tabItemCripto.Visible = false;
            //tabItemBackup.Visible = false;
            //tabItemRestore.Visible = false;
            //tabItemDVH.Visible = false;
            //tabItemDVV.Visible = false;
            //tabItemConsultarB.Visible = false;
            //tabItemFunc.Visible = true;
            //tabItemAsignar.Visible = true;
            //tabItemPermConsultar.Visible = true;
            //superTabCtrol.SelectedTab = tabItemFunc;
            //limpiarCampos();
        }

        private void btnItemFunc_Click(object sender, EventArgs e)
        {
            if (btnItemAsignar.Visible == false)
                tabItemAsignar.Visible = false;
            if (btnItemPermConsultar.Visible == false)
                tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            tabItemFunc.Visible = true;
            //tabItemAsignar.Visible = true;
            //tabItemPermConsultar.Visible = true;
            superTabCtrolAdmin.SelectedTab = tabItemFunc;
            limpiarCampos();
        }

        private void btnItemAsignar_Click(object sender, EventArgs e)
        {
            if (btnItemFunc.Visible == false)
                tabItemFunc.Visible = false;
            if (btnItemPermConsultar.Visible == false)
                tabItemPermConsultar.Visible = false;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            //tabItemFunc.Visible = true;
            tabItemAsignar.Visible = true;
            //tabItemPermConsultar.Visible = true;
            superTabCtrolAdmin.SelectedTab = tabItemAsignar;
            limpiarCampos();
        }

        private void btnItemPermCons_Click(object sender, EventArgs e)
        {
            if (btnItemAsignar.Visible == false)
                tabItemAsignar.Visible = false;
            if (btnItemFunc.Visible == false)
                tabItemFunc.Visible = false;
            superTabCtrolAdmin.Visible = true;
            tabItemDVH.Visible = false;
            tabItemDVV.Visible = false;
            tabItemBackup.Visible = false;
            tabItemRestore.Visible = false;
            tabItemCripto.Visible = false;
            tabItemAgregar.Visible = false;
            tabItemEditar.Visible = false;
            tabItemConsultar.Visible = false;
            tabItemConsultarB.Visible = false;
            //tabItemFunc.Visible = true;
            //tabItemAsignar.Visible = true;
            tabItemPermConsultar.Visible = true;
            superTabCtrolAdmin.SelectedTab = tabItemPermConsultar;
            limpiarCampos();
        }

        private void radioFuncAgregar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            if (radioAgregar.Checked)
            {
                lblFuncTodos.Visible = false;
                if (radioPatente.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "agregar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioFamilia.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "familia"), I18n.obtenerString("InicioAdministrador", "agregar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioRol.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "agregar"))).Append(Constantes.DOS_PUNTOS).ToString();
                btnFuncBuscar.Visible = false;
                //btnFuncGuardar.Location = new Point(btnFuncBuscar.Location.X, btnFuncBuscar.Location.Y);
                btnFuncGuardar.Visible = true;
                gridFunciones.Visible = false;
            }
        }

        private void radioFuncEditar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            if (radioEditar.Checked)
            {
                lblFuncTodos.Visible = false;
                if (radioPatente.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioFamilia.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "familia"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioRol.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
                //btnFuncBuscar.Location = new Point(btnFuncGuardar.Location.X, btnFuncGuardar.Location.Y);
                btnFuncBuscar.Visible = true;
                //btnFuncGuardar.Location = new Point(btnFuncBuscar.Location.X + 130, btnFuncBuscar.Location.Y);
                btnFuncGuardar.Visible = true;
                gridFunciones.Visible = false;
            }
        }

        private void radioFuncConsultar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            if (radioConsultar.Checked)
            {
                lblFuncTodos.Visible = true;
                lblFuncTodos.Text = I18n.obtenerString("Mensaje", "obtenerTodos");
                if (radioPatente.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "consultar"))).Append(Constantes.DOS_PUNTOS).ToString();
                    
                else if (radioFamilia.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "familia"), I18n.obtenerString("InicioAdministrador", "consultar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioRol.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "consultar"))).Append(Constantes.DOS_PUNTOS).ToString();
                    btnFuncGuardar.Visible = false;
                    //btnFuncBuscar.Location = new Point(355, 20);
                    btnFuncBuscar.Visible = true;
                    gridFunciones.Visible = false;
            }

        }

        private void btnFuncBuscar_Click(object sender, EventArgs e)
        {
            if (radioConsultar.Checked && radioPatente.Checked)
                buscarPatente(Constantes.CONSULTAR);
            else if (radioConsultar.Checked && radioFamilia.Checked)
                buscarFamilia(Constantes.CONSULTAR);
            else if (radioConsultar.Checked && radioRol.Checked)
                buscarRol(Constantes.CONSULTAR);
            else if (radioEditar.Checked && radioPatente.Checked)
                buscarPatente(Constantes.EDITAR);
            else if (radioEditar.Checked && radioFamilia.Checked)
                buscarFamilia(Constantes.EDITAR);
            else if (radioEditar.Checked && radioRol.Checked)
                buscarRol(Constantes.EDITAR);
        }

        private void buscarRol(String accion)
        {
            if (!String.IsNullOrEmpty(txtFuncNombre.Text) && (Constantes.CONSULTAR == accion || Constantes.EDITAR == accion))
            {
                Rol rol = rolBLL.obtenerRolPorDesc(txtFuncNombre.Text.ToUpper());
                if (rol == null)
                {
                    MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                        gridFunciones.Visible = true;
                        gridFunciones.Rows.Clear();
                        gridFunciones.Rows.Add(1);
                        gridFunciones.Columns["gridFuncionesHeaderPatente"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderFamilia"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderRol"].Visible = true;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].Visible = true;
                        gridFunciones.Rows[0].Cells["gridFuncionesHeaderRol"].Value = rol.descripcion;
                        gridFunciones.Rows[0].Cells["gridFuncionesHeaderEstadoRol"].Value = rol.estado;
                        gridFunciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        gridFunciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                        gridFunciones.AutoSize = true;
                        gridFunciones.EditMode = DataGridViewEditMode.EditOnEnter;
                        gridFunciones.Columns["gridFuncionesHeaderRol"].ReadOnly = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].ReadOnly = false;
                }
            }
            else if (String.IsNullOrEmpty(txtFuncNombre.Text) && Constantes.CONSULTAR == accion)
            {
                List<Rol> listaRoles = rolBLL.obtenerRoles();
                if (listaRoles.Count > 0)
                {
                    gridFunciones.Rows.Clear();
                    for (int i = 0; i < listaRoles.Count; i++)
                    {
                            gridFunciones.Visible = true;

                            gridFunciones.Rows.Add(1);
                            gridFunciones.Columns["gridFuncionesHeaderPatente"].Visible = false;
                            gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].Visible = false;
                            gridFunciones.Columns["gridFuncionesHeaderFamilia"].Visible = false;
                            gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].Visible = false;
                            gridFunciones.Columns["gridFuncionesHeaderRol"].Visible = true;
                            gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].Visible = true;
                            gridFunciones.Rows[i].Cells["gridFuncionesHeaderRol"].Value = listaRoles[i].descripcion;
                            gridFunciones.Rows[i].Cells["gridFuncionesHeaderEstadoRol"].Value = listaRoles[i].estado;
                            gridFunciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                            gridFunciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                            gridFunciones.AutoSize = true;
                    }
                }
            }
            else if (String.IsNullOrEmpty(txtFuncNombre.Text) && Constantes.EDITAR == accion)
                MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "funcion"), Constantes.ROL, Constantes.EDITAR), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buscarFamilia(String accion)
        {
            if (!String.IsNullOrEmpty(txtFuncNombre.Text) && (Constantes.CONSULTAR == accion || Constantes.EDITAR == accion))
            {
                Familia familia = familiaBLL.obtenerFamiliaPorDesc(txtFuncNombre.Text.ToUpper());
                if (familia == null)
                {
                    MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                        gridFunciones.Visible = true;
                        gridFunciones.Rows.Clear();
                        gridFunciones.Rows.Add(1);
                        gridFunciones.Columns["gridFuncionesHeaderRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderPatente"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderFamilia"].Visible = true;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].Visible = true;
                        gridFunciones.Rows[0].Cells["gridFuncionesHeaderFamilia"].Value = familia.descripcion;
                        gridFunciones.Rows[0].Cells["gridFuncionesHeaderEstadoFamilia"].Value = familia.estado;
                        gridFunciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        gridFunciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                        gridFunciones.AutoSize = true;
                        gridFunciones.EditMode = DataGridViewEditMode.EditOnEnter;
                        gridFunciones.Columns["gridFuncionesHeaderFamilia"].ReadOnly = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].ReadOnly = false;
                }
            }
            else if (String.IsNullOrEmpty(txtFuncNombre.Text) && Constantes.CONSULTAR == accion)
            {
                List<Familia> listaFamilias = familiaBLL.obtenerFamilias();
                if (listaFamilias.Count > 0)
                {
                    gridFunciones.Rows.Clear();
                    for (int i = 0; i < listaFamilias.Count; i++)
                    {
                            gridFunciones.Visible = true;

                            gridFunciones.Rows.Add(1);
                            gridFunciones.Columns["gridFuncionesHeaderRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderPatente"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderFamilia"].Visible = true;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].Visible = true;
                            gridFunciones.Rows[i].Cells["gridFuncionesHeaderFamilia"].Value = listaFamilias[i].descripcion;
                            gridFunciones.Rows[i].Cells["gridFuncionesHeaderEstadoFamilia"].Value = listaFamilias[i].estado;
                            gridFunciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                            gridFunciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                           gridFunciones.AutoSize = true;
                    }
                }else
                    MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (String.IsNullOrEmpty(txtFuncNombre.Text) && Constantes.EDITAR == accion)
                MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "funcion"), Constantes.FAMILIA, Constantes.EDITAR), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buscarPatente(String accion)
        {
            if (!String.IsNullOrEmpty(txtFuncNombre.Text) && (Constantes.CONSULTAR==accion || Constantes.EDITAR==accion))
            {
                    Patente patente = patenteBLL.obtenerPatentePorDesc(txtFuncNombre.Text.ToUpper());
                    if (patente == null)
                    {
                        MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        gridFunciones.Visible = true;
                        gridFunciones.Rows.Clear();
                        gridFunciones.Rows.Add(1);
                        gridFunciones.Columns["gridFuncionesHeaderRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderFamilia"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderPatente"].Visible = true;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].Visible = true;
                        gridFunciones.Rows[0].Cells["gridFuncionesHeaderPatente"].Value = patente.descripcion;
                        gridFunciones.Rows[0].Cells["gridFuncionesHeaderEstadoPatente"].Value = patente.estado;
                        gridFunciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        gridFunciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                        gridFunciones.AutoSize = true;
                        gridFunciones.EditMode = DataGridViewEditMode.EditOnEnter;
                        gridFunciones.Columns["gridFuncionesHeaderPatente"].ReadOnly = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].ReadOnly = false;
                    }
            }
            else if (String.IsNullOrEmpty(txtFuncNombre.Text) && Constantes.CONSULTAR==accion)
            {
                List<Patente> listaPatentes = patenteBLL.obtenerPatentes();
                if (listaPatentes.Count > 0)
                {
                    gridFunciones.Rows.Clear();
                    for (int i = 0; i < listaPatentes.Count; i++)
                    {
                        gridFunciones.Visible = true; 
                        gridFunciones.Rows.Add(1);
                        gridFunciones.Columns["gridFuncionesHeaderRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoRol"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderFamilia"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoFamilia"].Visible = false;
                        gridFunciones.Columns["gridFuncionesHeaderPatente"].Visible = true;
                        gridFunciones.Columns["gridFuncionesHeaderEstadoPatente"].Visible = true;
                        gridFunciones.Rows[i].Cells["gridFuncionesHeaderPatente"].Value = listaPatentes[i].descripcion;
                        gridFunciones.Rows[i].Cells["gridFuncionesHeaderEstadoPatente"].Value = listaPatentes[i].estado;
                        gridFunciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        gridFunciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                        gridFunciones.AutoSize = true;
                    }
                }else
                    MessageBox.Show(ningunRegistro, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if (String.IsNullOrEmpty(txtFuncNombre.Text) && Constantes.EDITAR == accion)
                MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "funcion"), Constantes.PATENTE, Constantes.EDITAR), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFuncGuardar_Click(object sender, EventArgs e)
        {

            if (radioAgregar.Checked && radioPatente.Checked)
                guardarPatente(Constantes.AGREGAR);
            else if (radioAgregar.Checked && radioFamilia.Checked)
                guardarFamilia(Constantes.AGREGAR);
            else if (radioAgregar.Checked && radioRol.Checked)
                guardarRol(Constantes.AGREGAR);
            else if(radioEditar.Checked && radioPatente.Checked)
                guardarPatente(Constantes.EDITAR);
            else if (radioAgregar.Checked && radioFamilia.Checked)
                guardarFamilia(Constantes.EDITAR);
            else if (radioAgregar.Checked && radioRol.Checked)
                guardarRol(Constantes.EDITAR);
        }

        private void guardarRol(String accion)
        {
            String mensaje = null;
            String campoRequerido = null;
            if (!String.IsNullOrEmpty(txtFuncNombre.Text))
            {
                if(txtFuncNombre.Text==Constantes.ADMINISTRADOR)
                    MessageBox.Show(I18n.obtenerString("Mensaje", "editarAdmin"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Rol rol = new Rol();
                rol.descripcion = txtFuncNombre.Text.ToUpper();
                rol.estado = true;
                try
                {
                    Rol rolRes = rolBLL.obtenerRolPorDesc(rol.descripcion);
                    if (rolRes == null && Constantes.AGREGAR == accion)
                    {
                        DialogResult siNoRes = MessageBox.Show(confirmar, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            int res = rolBLL.agregarRol(rol);
                            mensaje = String.Format(elementoNuevo, "Rol " + rol.descripcion + " ");
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else if (rolRes != null && Constantes.AGREGAR == accion)
                    {
                        mensaje = String.Format(elementoExistente, "Rol " + rolRes.descripcion + " ");
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else if (rolRes != null && Constantes.EDITAR == accion)
                    {
                        rolRes.descripcion = gridFunciones.Rows[0].Cells["gridFuncionesHeaderRol"].Value.ToString().ToUpper();
                        rolRes.estado = Boolean.Parse(gridFunciones.Rows[0].Cells["gridFuncionesHeaderEstadoRol"].Value.ToString());
                        rolBLL.actualizarRol(rolRes);
                        MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), Constantes.ROL), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    limpiarCampos();
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Rol", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
            else
            {
                campoRequerido = String.Format(requerido, "Nombre");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void guardarFamilia(String accion)
        {
            String mensaje = null;
            String campoRequerido = null;
            if (!String.IsNullOrEmpty(txtFuncNombre.Text))
            {
                Familia familia = new Familia();
                familia.descripcion = txtFuncNombre.Text.ToUpper();
                familia.estado = true;
                try
                {
                    Familia famRes = familiaBLL.obtenerFamiliaPorDesc(familia.descripcion);
                    if (famRes == null  && Constantes.AGREGAR == accion)
                    {
                        DialogResult siNoRes = MessageBox.Show(confirmar, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            int res = familiaBLL.agregarFamilia(familia);
                            mensaje = String.Format(elementoNuevo, "Familia " + familia.descripcion + " ");
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    } else if (famRes != null && Constantes.AGREGAR == accion)
                    {
                        mensaje = String.Format(elementoExistente, "Familia " + famRes.descripcion + " ");
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (famRes != null && Constantes.EDITAR == accion){
                        famRes.descripcion = gridFunciones.Rows[0].Cells["gridFuncionesHeaderFamilia"].Value.ToString().ToUpper();
                        famRes.estado = Boolean.Parse(gridFunciones.Rows[0].Cells["gridFuncionesHeaderEstadoFamilia"].Value.ToString());
                        familiaBLL.actualizarFamilia(famRes);
                        MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), Constantes.FAMILIA), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    limpiarCampos();
                } 
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Familia", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
            else
            {
                campoRequerido = String.Format(requerido, "Nombre");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void guardarPatente(String accion)
        {
            String mensaje = null;
            String campoRequerido = null;
            if (!String.IsNullOrEmpty(txtFuncNombre.Text))
            {
                Patente patente = new Patente();
                patente.descripcion = txtFuncNombre.Text.ToUpper();
                patente.estado = true;
                try
                {
                    Patente patRes = patenteBLL.obtenerPatentePorDesc(patente.descripcion);
                    if (patRes == null && Constantes.AGREGAR == accion)
                    {
                        DialogResult siNoRes = MessageBox.Show(confirmar, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            int res = patenteBLL.agregarPatente(patente);
                            mensaje = String.Format(elementoNuevo, "Patente " + patente.descripcion + " ");
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else if (patRes != null && Constantes.AGREGAR == accion)
                    {
                        mensaje = String.Format(elementoExistente, "Patente " + patRes.descripcion + " ");
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (patRes != null && Constantes.EDITAR == accion)
                    {
                        patRes.descripcion = gridFunciones.Rows[0].Cells["gridFuncionesHeaderPatente"].Value.ToString().ToUpper();
                        patRes.estado = Boolean.Parse(gridFunciones.Rows[0].Cells["gridFuncionesHeaderEstadoPatente"].Value.ToString());
                        patenteBLL.actualizarPatente(patRes);
                        MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), Constantes.PATENTE), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    limpiarCampos();
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Patente", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
            else
            {
                campoRequerido = String.Format(requerido, "Nombre");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void radioFuncPatente_Click(object sender, EventArgs e)
        {
            obtenerMensajeFuncion();
        }

        private void obtenerMensajeFuncion()
        {
            if (radioConsultar.Checked)
            {
                lblFuncTodos.Visible = true;
                lblFuncTodos.Text = I18n.obtenerString("Mensaje", "obtenerTodos");
                if (radioPatente.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "consultar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioFamilia.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "familia"), I18n.obtenerString("InicioAdministrador", "consultar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioRol.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "consultar"))).Append(Constantes.DOS_PUNTOS).ToString();
                btnFuncGuardar.Visible = false;
                btnFuncBuscar.Visible = true;
                gridFunciones.Visible = false;
            }
            else if (radioEditar.Checked)
            {
                lblFuncTodos.Visible = false;
                if (radioPatente.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioFamilia.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "familia"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioRol.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "editar"))).Append(Constantes.DOS_PUNTOS).ToString();
                //btnFuncBuscar.Location = new Point(355, 20);
                btnFuncBuscar.Visible = true;
                //btnFuncGuardar.Location = new Point(355, 63);
                btnFuncGuardar.Visible = true;
                gridFunciones.Visible = false;
            }
            else if (radioAgregar.Checked)
            {
                lblFuncTodos.Visible = false;
                if (radioPatente.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "agregar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioFamilia.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "familia"), I18n.obtenerString("InicioAdministrador", "agregar"))).Append(Constantes.DOS_PUNTOS).ToString();
                else if (radioRol.Checked)
                    lblFuncInfo.Text = new StringBuilder(String.Format(I18n.obtenerString("Mensaje", "funcion"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "agregar"))).Append(Constantes.DOS_PUNTOS).ToString();
                btnFuncBuscar.Visible = false;
                btnFuncGuardar.Visible = true;
                gridFunciones.Visible = false;
            }
        }

        private void radioFuncFamilia_Click(object sender, EventArgs e)
        {
            obtenerMensajeFuncion();
        }

        private void radioFuncRol_Click(object sender, EventArgs e)
        {
            obtenerMensajeFuncion();
        }

        private void radioAsigUsuario_Click(object sender, EventArgs e)
        {
            radioOp2Patente.Visible = true;
            radioOp2Rol.Visible = false;
            lblPermitir.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "usuario")).Append(Constantes.DOS_PUNTOS).ToString();
            comboOp1.DataSource = usuarioBLL.obtenerUsuarios().Where(u => u.estado == true).Select(u => u.apellido + " " + u.nombre + " - DNI: " + u.dni).ToList();
            obtenerMensajeAsignar();
            if (radioOp2Familia.Checked)
            {
                lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "familia")).Append(Constantes.DOS_PUNTOS).ToString();
                comboOp2.DataSource = familiaBLL.obtenerFamilias().Where(f => f.estado == true).Select(f => f.descripcion).ToList();
            }
            else if (radioOp2Patente.Checked)
            {
                lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "patente")).Append(Constantes.DOS_PUNTOS).ToString();
                comboOp2.DataSource = patenteBLL.obtenerPatentes().Where(p => p.estado == true).Select(p => p.descripcion).ToList();
            }
            //else if (radioOp2Rol.Checked)
            //    comboAcceder.DataSource = rolBLL.obtenerRoles().Select(r => r.descripcion).ToList();
        }

        private void radioAsigRol_Click(object sender, EventArgs e)
        {
            radioOp2Rol.Visible = false;
            radioOp2Familia.Checked = true;
            radioOp2Patente.Visible = true;
            lblPermitir.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "rol")).Append(Constantes.DOS_PUNTOS).ToString();
            comboOp1.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            obtenerMensajeAsignar();
            if (radioOp2Familia.Checked)
            {
                lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "familia")).Append(Constantes.DOS_PUNTOS).ToString();
                comboOp2.DataSource = familiaBLL.obtenerFamilias().Where(f => f.estado == true).Select(f => f.descripcion).ToList();
            }
            else if (radioOp2Patente.Checked)
            {
                lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "patente")).Append(Constantes.DOS_PUNTOS).ToString();
                comboOp2.DataSource = patenteBLL.obtenerPatentes().Where(p => p.estado == true).Select(p => p.descripcion).ToList();
            }
        }

        private void radioAsigPatente_Click(object sender, EventArgs e)
        {
            radioOp2Familia.Checked = true;
            radioOp2Patente.Visible = false;
            radioOp2Rol.Visible = false;
            lblPermitir.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "patente")).Append(Constantes.DOS_PUNTOS).ToString();
            comboOp1.DataSource = patenteBLL.obtenerPatentes().Where(p => p.estado == true).Select(p => p.descripcion).ToList();
            obtenerMensajeAsignar();
            if (radioOp2Familia.Checked)
            {
                lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "familia")).Append(Constantes.DOS_PUNTOS).ToString();
                comboOp2.DataSource = familiaBLL.obtenerFamilias().Where(f => f.estado == true).Select(f => f.descripcion).ToList();
            }
            else if (radioOp2Rol.Checked)
            {
                lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "rol")).Append(Constantes.DOS_PUNTOS).ToString();
                comboOp2.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            }
        }

        private void radioAFamilia_Click(object sender, EventArgs e)
        {
            lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "familia")).Append(Constantes.DOS_PUNTOS).ToString();
            comboOp2.DataSource = familiaBLL.obtenerFamilias().Where(f => f.estado == true).Select(f => f.descripcion).ToList();
            obtenerMensajeAsignar();
        }

        private void radioAPatente_Click(object sender, EventArgs e)
        {
            if (radioOp1Patente.Checked)
                radioOp2Patente.Visible = false;
            comboOp2.DataSource = patenteBLL.obtenerPatentes().Where(p => p.estado == true).Select(p => p.descripcion).ToList();
            lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "patente")).Append(Constantes.DOS_PUNTOS).ToString();
            obtenerMensajeAsignar();
        }

        private void radioARol_Click(object sender, EventArgs e)
        {
            if (radioOp1Rol.Checked)
                radioOp2Rol.Visible = false;
            else if (radioOp1Usuario.Checked)
                radioOp2Rol.Visible = false;
            comboOp2.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            lblAcceder.Text = new StringBuilder(I18n.obtenerString("InicioAdministrador", "rol")).Append(Constantes.DOS_PUNTOS).ToString();
            obtenerMensajeAsignar();
        }

        private void obtenerMensajeAsignar()
        {
            lblDescripcion.ForeColor = Color.Navy;
            if (radioOp1Usuario.Checked && radioOp2Familia.Checked && switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "usuario"), I18n.obtenerString("InicioAdministrador", "familia"));
            else if (radioOp1Usuario.Checked && radioOp2Patente.Checked && switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "usuario"), I18n.obtenerString("InicioAdministrador", "patente"));
            else if (radioOp1Rol.Checked && radioOp2Familia.Checked && switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "familia"));
            else if (radioOp1Rol.Checked && radioOp2Patente.Checked && switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "patente"));
            else if (radioOp1Patente.Checked && radioOp2Familia.Checked && switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "familia"));
            else if (radioOp1Usuario.Checked && radioOp2Familia.Checked && !switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "usuario"), I18n.obtenerString("InicioAdministrador", "familia"));
            else if (radioOp1Usuario.Checked && radioOp2Patente.Checked && !switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "usuario"), I18n.obtenerString("InicioAdministrador", "patente"));
            else if (radioOp1Rol.Checked && radioOp2Familia.Checked && !switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "familia"));
            else if (radioOp1Rol.Checked && radioOp2Patente.Checked && !switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "rol"), I18n.obtenerString("InicioAdministrador", "patente"));
            else if (radioOp1Patente.Checked && radioOp2Familia.Checked && !switchBtn.Value)
                lblDescripcion.Text = String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "patente"), I18n.obtenerString("InicioAdministrador", "familia"));
        }

        private void btnAsigGuardar_click(object sender, EventArgs e)
        {
            try
            {
                    //permitir accesos
                    if (radioOp1Usuario.Checked && radioOp2Familia.Checked && switchBtn.Value)
                    {
                       String dni = comboOp1.SelectedItem.ToString().Split(new string[] { " - DNI: " }, StringSplitOptions.None)[1];
                       Usuario usuario = usuarioBLL.obtenerUsuarioPorDni(dni);
                       Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboOp2.SelectedItem.ToString());
                        DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "usuario") + " "+ usuario.apellido.ToUpper() + " " + usuario.nombre.ToUpper(), I18n.obtenerString("InicioAdministrador", "familia") + " " + familia.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            usuario.PermitirAcceso(familia);
                            MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsjPlus"), I18n.obtenerString("InicioAdministrador", "usuario"), usuario.apellido + " " + usuario.nombre, I18n.obtenerString("InicioAdministrador", "familia"), familia.descripcion)
                                , msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioOp1Usuario.Checked && radioOp2Patente.Checked && switchBtn.Value)
                    {
                        String dni = comboOp1.SelectedItem.ToString().Split(new string[] { " - DNI: " }, StringSplitOptions.None)[1];
                        Usuario usuario = usuarioBLL.obtenerUsuarioPorDni(dni);
                        Patente patente = patenteBLL.obtenerPatentePorDesc(comboOp2.SelectedItem.ToString());
                        DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "usuario") + " "+ usuario.apellido.ToUpper() + " " + usuario.nombre.ToUpper(), I18n.obtenerString("InicioAdministrador", "patente") + " " + patente.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            usuario.PermitirAcceso(patente);
                            MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsjPlus"), I18n.obtenerString("InicioAdministrador", "usuario"), usuario.apellido + " " + usuario.nombre, I18n.obtenerString("InicioAdministrador", "patente"), patente.descripcion),
                                msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        }
                    else if (radioOp1Rol.Checked && radioOp2Familia.Checked && switchBtn.Value)
                    {
                        Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboOp2.SelectedItem.ToString());
                        Rol rol = rolBLL.obtenerRolPorDesc(comboOp1.SelectedItem.ToString());
                         DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "rol") + " "+ rol.descripcion, I18n.obtenerString("InicioAdministrador", "familia") + " " + familia.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (siNoRes.Equals(DialogResult.Yes))
                         {
                             rol.PermitirAcceso(familia);
                             MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsjPlus"), I18n.obtenerString("InicioAdministrador", "rol"), rol.descripcion, I18n.obtenerString("InicioAdministrador", "familia"), familia.descripcion),
                                 msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                    }
                    else if (radioOp1Rol.Checked && radioOp2Patente.Checked && switchBtn.Value)
                    {
                        Patente patente = patenteBLL.obtenerPatentePorDesc(comboOp2.SelectedItem.ToString());
                        Rol rol = rolBLL.obtenerRolPorDesc(comboOp1.SelectedItem.ToString());
                         DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "usuario") + " "+ rol.descripcion, I18n.obtenerString("InicioAdministrador", "patente") + " " + patente.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (siNoRes.Equals(DialogResult.Yes))
                         {
                             rol.PermitirAcceso(patente);
                             MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsjPlus"), I18n.obtenerString("InicioAdministrador", "rol"), rol.descripcion, I18n.obtenerString("InicioAdministrador", "patente"), patente.descripcion),
                                 msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                    }
                    else if (radioOp1Patente.Checked && radioOp2Familia.Checked && switchBtn.Value)
                    {
                        Patente patente = patenteBLL.obtenerPatentePorDesc(comboOp1.SelectedItem.ToString());
                        Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboOp2.SelectedItem.ToString());
                         DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsj"), I18n.obtenerString("InicioAdministrador", "patente") + " "+ patente.descripcion, I18n.obtenerString("InicioAdministrador", "familia") + " " + familia.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (siNoRes.Equals(DialogResult.Yes))
                         {
                             familia.Add(patente);
                             MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "permitirMsjPlus"), I18n.obtenerString("InicioAdministrador", "patente"), patente.descripcion, I18n.obtenerString("InicioAdministrador", "familia"), familia.descripcion),
                                 msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                    }
                    //Denegar accesos
                    else if (radioOp1Usuario.Checked && radioOp2Familia.Checked && !switchBtn.Value)
                    {
                        String dni = comboOp1.SelectedItem.ToString().Split(new string[] { " - DNI: " }, StringSplitOptions.None)[1];
                        Usuario usuario = usuarioBLL.obtenerUsuarioPorDni(dni);
                        Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboOp2.SelectedItem.ToString());
                         DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "usuario") + " "+ usuario.apellido.ToUpper() + " " + usuario.nombre.ToUpper(), I18n.obtenerString("InicioAdministrador", "familia") + " " + familia.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (siNoRes.Equals(DialogResult.Yes))
                         {
                             usuario.DenegarAcceso(familia);
                             MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsjPlus"), I18n.obtenerString("InicioAdministrador", "usuario"), usuario.apellido + " " + usuario.nombre, I18n.obtenerString("InicioAdministrador", "familia"), familia.descripcion),
                                 msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                    }
                    else if (radioOp1Usuario.Checked && radioOp2Patente.Checked && !switchBtn.Value)
                    {
                        String dni = comboOp1.SelectedItem.ToString().Split(new string[] { " - DNI: " }, StringSplitOptions.None)[1];
                        Usuario usuario = usuarioBLL.obtenerUsuarioPorDni(dni);
                        Patente patente = patenteBLL.obtenerPatentePorDesc(comboOp2.SelectedItem.ToString());
                        DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "usuario") + " "+ usuario.apellido.ToUpper() + " " + usuario.nombre.ToUpper(), I18n.obtenerString("InicioAdministrador", "patente") + " " + patente.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            usuario.DenegarAcceso(patente);
                            MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsjPlus"), I18n.obtenerString("InicioAdministrador", "usuario"), usuario.apellido + " " + usuario.nombre, I18n.obtenerString("InicioAdministrador", "patente"), patente.descripcion),
                                msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioOp1Rol.Checked && radioOp2Familia.Checked && !switchBtn.Value)
                    {
                        Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboOp2.SelectedItem.ToString());
                        Rol rol = rolBLL.obtenerRolPorDesc(comboOp1.SelectedItem.ToString());
                        DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "rol") + " "+ rol.descripcion, I18n.obtenerString("InicioAdministrador", "familia") + " " + familia.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            rol.DenegarAcceso(familia);
                            MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsjPlus"), I18n.obtenerString("InicioAdministrador", "rol"), rol.descripcion, I18n.obtenerString("InicioAdministrador", "familia"), familia.descripcion),
                                msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioOp1Rol.Checked && radioOp2Patente.Checked && !switchBtn.Value)
                    {
                        Patente patente = patenteBLL.obtenerPatentePorDesc(comboOp2.SelectedItem.ToString());
                        Rol rol = rolBLL.obtenerRolPorDesc(comboOp1.SelectedItem.ToString());
                        DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "rol") + " "+ rol.descripcion, I18n.obtenerString("InicioAdministrador", "patente") + " " + patente.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            rol.DenegarAcceso(patente);
                            MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsjPlus"), I18n.obtenerString("InicioAdministrador", "rol"), rol.descripcion, I18n.obtenerString("InicioAdministrador", "patente"), patente.descripcion),
                                msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioOp1Patente.Checked && radioOp2Familia.Checked && !switchBtn.Value)
                    {
                        Patente patente = patenteBLL.obtenerPatentePorDesc(comboOp1.SelectedItem.ToString());
                        Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboOp2.SelectedItem.ToString());
                        DialogResult siNoRes = MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsj"), I18n.obtenerString("InicioAdministrador", "patente") + " "+ patente.descripcion, I18n.obtenerString("InicioAdministrador", "familia") + " " + familia.descripcion + Constantes.INTERROGATION),
                            msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            familia.Remove(patente);
                            MessageBox.Show(String.Format(I18n.obtenerString("Mensaje", "denegarMsjPlus"), I18n.obtenerString("InicioAdministrador", "patente"), patente.descripcion, I18n.obtenerString("InicioAdministrador", "familia"), familia.descripcion),
                                msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + "Permisos", ex.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
            }
        }

        private void radioPermCUsuario_Click(object sender, EventArgs e)
        {
            comboPermC.DataSource = usuarioBLL.obtenerUsuarios().Where(u => u.estado == true).Select(u => u.apellido + "" + u.nombre + " - DNI: " + u.dni).ToList();
        }

        private void radioPermCRol_Click(object sender, EventArgs e)
        {
            comboPermC.DataSource = rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
        }

        private void radioPermCPatente_Click(object sender, EventArgs e)
        {
            comboPermC.DataSource = patenteBLL.obtenerPatentes().Where(p => p.estado == true).Select(p => p.descripcion).ToList();
        }

        private void radioPermCFamilia_Click(object sender, EventArgs e)
        {
            comboPermC.DataSource = familiaBLL.obtenerFamilias().Where(f => f.estado == true).Select(f => f.descripcion).ToList();
        }

        private void btnPermCBuscar_Click(object sender, EventArgs e)
        {
            if (radioPermCUsuario.Checked)
            {
                gridPermConsultar.Visible = true;
                String dni = comboPermC.SelectedItem.ToString().Split(new string[] { " - DNI: " }, StringSplitOptions.None)[1];
                Usuario usuario = usuarioBLL.obtenerUsuarioPorDni(dni);
                Rol rol = rolBLL.obtenerRolPorId(Int32.Parse(usuarioBLL.obtnerRolPorIdUsuario(usuario.id)));
                List<Patente> listaPatentes = usuarioBLL.obtenerPatentesPorId(usuario.id);
                gridPermConsultar.Rows.Clear();
                for (int i = 0; i < listaPatentes.Count; i++)
			   {
                    gridPermConsultar.Rows.Add(1);
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderUsuario"].Value = usuario.apellido + " " + usuario.nombre + " - DNI: " + usuario.dni;
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderRol"].Value = rol.descripcion;
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderPatente"].Value = listaPatentes[i].descripcion;
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderFamilia"].Value = familiaBLL.obtenerFamiliaPorIdPat(listaPatentes[i].id).descripcion;
                    gridPermConsultar.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    gridPermConsultar.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                    gridPermConsultar.AutoSize = true;
                }
            }
            else if (radioPermCRol.Checked)
            {
                gridPermConsultar.Visible = true;
                Rol rol = rolBLL.obtenerRolPorDesc(comboPermC.SelectedItem.ToString());
                List<Usuario> listaUsuario = usuarioBLL.obtenerUsuariosPorRol(rol.descripcion);
                gridPermConsultar.Rows.Clear();
                for (int i = 0; i < listaUsuario.Count; i++)
                {
                    List<Patente> listaPatentes = usuarioBLL.obtenerPatentesPorId(listaUsuario[i].id);
                    for (int j = 0; j < listaPatentes.Count; j++)
                    {
                        gridPermConsultar.Rows.Add(1);
                        gridPermConsultar.Rows[i+j].Cells["gridPermConsultarHeaderUsuario"].Value = listaUsuario[i].apellido + " " + listaUsuario[i].nombre + " - DNI: " + listaUsuario[i].dni;
                        gridPermConsultar.Rows[i+j].Cells["gridPermConsultarHeaderRol"].Value = rol.descripcion;
                        gridPermConsultar.Rows[i+j].Cells["gridPermConsultarHeaderPatente"].Value = listaPatentes[j].descripcion;
                        gridPermConsultar.Rows[i+j].Cells["gridPermConsultarHeaderFamilia"].Value = familiaBLL.obtenerFamiliaPorIdPat(listaPatentes[j].id).descripcion;
                        gridPermConsultar.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        gridPermConsultar.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                        gridPermConsultar.AutoSize = true;
                    }
                }
            }
            else if (radioPermCPatente.Checked)
            {
                gridPermConsultar.Visible = true;
                Patente patente = patenteBLL.obtenerPatentePorDesc(comboPermC.SelectedItem.ToString());
                List<Usuario> listaUsuarios = patenteBLL.obtenerUsuariosPorIdPat(patente.id);
                gridPermConsultar.Rows.Clear();
                for (int i = 0; i < listaUsuarios.Count; i++)
                {
                    gridPermConsultar.Rows.Add(1);
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderUsuario"].Value = listaUsuarios[i].apellido + " " + listaUsuarios[i].nombre + " - DNI: " + listaUsuarios[i].dni;
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderRol"].Value = rolBLL.obtenerRolPorId(Int32.Parse(usuarioBLL.obtnerRolPorIdUsuario(listaUsuarios[i].id))).descripcion;
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderPatente"].Value = patente.descripcion;
                    gridPermConsultar.Rows[i].Cells["gridPermConsultarHeaderFamilia"].Value = familiaBLL.obtenerFamiliaPorIdPat(patente.id).descripcion;
                    gridPermConsultar.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    gridPermConsultar.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                    gridPermConsultar.AutoSize = true;
                }
            }
            else if (radioPermCFamilia.Checked)
            {
                gridPermConsultar.Visible = true;
                gridPermConsultar.Rows.Clear();
                Familia familia = familiaBLL.obtenerFamiliaPorDesc(comboPermC.SelectedItem.ToString());
                List<Patente> listaPatente = patenteBLL.obtenerPatentesPorIdFam(familia.id);
                for (int i = 0; i < listaPatente.Count; i++)
                {
                    List<Usuario> listaUsuarios = patenteBLL.obtenerUsuariosPorIdPat(listaPatente[i].id);
                    for (int j = 0; j < listaUsuarios.Count; j++)
                    {
                        gridPermConsultar.Rows.Add(1);
                        gridPermConsultar.Rows[i + j].Cells["gridPermConsultarHeaderUsuario"].Value = listaUsuarios[j].apellido + " " + listaUsuarios[j].nombre + " - DNI: " + listaUsuarios[j].dni;
                        gridPermConsultar.Rows[i + j].Cells["gridPermConsultarHeaderRol"].Value = rolBLL.obtenerRolPorId(Int32.Parse(usuarioBLL.obtnerRolPorIdUsuario(listaUsuarios[j].id))).descripcion;
                        gridPermConsultar.Rows[i + j].Cells["gridPermConsultarHeaderPatente"].Value = listaPatente[i].descripcion;
                        gridPermConsultar.Rows[i+j].Cells["gridPermConsultarHeaderFamilia"].Value = familia.descripcion;
                        gridPermConsultar.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                        gridPermConsultar.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
                        gridPermConsultar.AutoSize = true;
                    }
                    
                }
            }
        }
    }
}