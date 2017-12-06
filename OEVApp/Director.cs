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
using Base.BLL;
using Utils;
using Entities;
using OEVApp.i18n;
using BLL;
using BLL.IBLL;

namespace OEVApp
{
    public partial class Director : DevComponents.DotNetBar.Metro.MetroForm
    {
        Usuario usuarioLogueado = null;
        Rol rolUsrLogueado = null;
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        IRolBLL rolBLL = new RolBLL();
        IPatenteBLL patenteBLL = new PatenteBLL();
        IFamiliaBLL familiaBLL = new FamiliaBLL();
        IBLLTraslado trasladoBLL = new BLLTraslado();
        IBLLAlojamiento alojamientoBLL = new BLLAlojamiento();
        IBLLInstructor instructorBLL = new BLLInstructor();
        String msjInfo = null;
        String msjConfirmar = null;
        String msjError = null;

        public Director(Usuario usuario, string idioma, List<Familia> familiasPermitidas, List<Patente> patentesPermitidas)
        {
            InitializeComponent();
            lblUsuarioLogueado.Text = usuario.id + " - " + usuario.nombre + " " + usuario.apellido;
            this.usuarioLogueado = usuario;
            rolUsrLogueado = rolBLL.obtenerRolPorId(Convert.ToInt32(usuarioBLL.obtnerRolPorIdUsuario(usuario.id)));
            cargarFuncionalidades(familiasPermitidas, patentesPermitidas);
            cargarCombos();
            I18n.cargarIdioma((EnumIdioma)Enum.Parse(typeof(EnumIdioma), idioma));
            generarDirectorStrings();
            generarCalendarioStrings();
            generarActividadesStrings();
            generarProveedoresStrings();
            generarAlojamientoStrings();
            generarTrasladoStrings();
            generarInstructorStrings();
        }

        public Director()
        {
            // TODO: Complete member initialization
        }

        private void cargarFuncionalidades(List<Familia> familiasPermitidas, List<Patente> patentesPermitidas)
        {

            List<Patente> listaPatentes = patenteBLL.obtenerPatentes();
            List<Patente> noPermPatentes = listaPatentes.Where(l => !patentesPermitidas.Any(p => l.id == p.id)).ToList();
            Dictionary<String, BaseItem> patentesItems = new Dictionary<string, BaseItem>();
            patentesItems.Add("AGREGAR CALENDARIO", btnItemAgregarC);
            patentesItems.Add("EDITAR CALENDARIO", btnItemEditarC);
            patentesItems.Add("CONSULTAR CALENDARIO", btnItemConsultarC);
            patentesItems.Add("CURSO DE ENTRENAMIENTO", btnItemActCursoEnt);
            patentesItems.Add("PAQUETE-EVENTO", btnItemActPaquete);
            patentesItems.Add("AGREGAR ALOJAMIENTO", btnItemAgregarA);
            patentesItems.Add("EDITAR ALOJAMIENTO", btnItemEditarA);
            patentesItems.Add("CONSULTAR ALOJAMIENTO", btnItemConsultarA);
            patentesItems.Add("AGREGAR INSTRUCTOR", btnItemAgregarI);
            patentesItems.Add("EDITAR INSTRUCTOR", btnItemEditarI);
            patentesItems.Add("CONSULTAR INSTRUCTOR", btnItemConsultarI);
            patentesItems.Add("AGREGAR TRASLADO", btnItemAgregarT);
            patentesItems.Add("EDITAR TRASLADO", btnItemEditarT);
            patentesItems.Add("CONSULTAR TRASLADO", btnItemConsultarT);
            patentesItems.Add("PROVEEDORES REPORTE", btnItemProveedores);
            patentesItems.Add("RESERVAS REPORTE", btnItemReservas);
            patentesItems.Add("VENTAS REPORTE", btnItemVentas);

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
            Dictionary<String, SideBarPanelItem> familiasSideBars = new Dictionary<string, SideBarPanelItem>();
            familiasSideBars.Add("CALENDARIO", sideBarPanelCalendario);
            familiasSideBars.Add("ACTIVIDADES", sideBarPanelActividad);
            familiasSideBars.Add("ALOJAMIETOS", sideBarPanelAlojamiento);
            familiasSideBars.Add("INSTRUCTORES", sideBarPanelInstructores);
            familiasSideBars.Add("TRASLADOS", sideBarPanelTraslados);
            familiasSideBars.Add("REPORTES", sideBarPanelReporte);

            //noPermFamilias.ForEach(n => sideBar1.Panels.Remove(familiasSideBars[n.descripcion].Name));

        }

        private void cargarCombos()
        {
            comboACDificultadA.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboAPDificultadA.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboACNombreE.DataSource = new List<Producto>();//rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            comboACDificultadE.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboAPNombreE.DataSource = new List<Producto>();//rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            comboAPDificultadE.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboACDificultad.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboAPDestinoC.DataSource = new List<Producto>();//rolBLL.obtenerRoles().Where(r => r.estado == true).Select(r => r.descripcion).ToList();
            comboAPDificultadC.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
        }

        private void generarDirectorStrings()
        {
            this.Text = I18n.obtenerString("InicioDirector", "inicioDirectorForm");
            btnLogout.Text = I18n.obtenerString("InicioDirector", "logout");
            lblUsuario.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "usuario")).Append(Constantes.DOS_PUNTOS).ToString();
            msjInfo = I18n.obtenerString("Mensaje", "info");
            msjConfirmar = I18n.obtenerString("Mensaje", "confirmar");
            msjError = I18n.obtenerString("Mensaje", "error");
        }

        private void generarCalendarioStrings()
        {
            sideBarPanelCalendario.Text = I18n.obtenerString("InicioDirector", "calendario");
            //Agregar
            btnItemAgregarC.Text = I18n.obtenerString("InicioDirector", "agregar");
            
            //Editar
            btnItemEditarC.Text = I18n.obtenerString("InicioDirector", "editar");
            tabItemACEditar.Text = I18n.obtenerString("InicioDirector", "editar");
            //Consultar
            btnItemConsultarC.Text = I18n.obtenerString("InicioDirector", "consultar");
            tabItemACConsultar.Text = I18n.obtenerString("InicioDirector", "consultar");
        }

        private void generarActividadesStrings()
        {
            sideBarPanelActividad.Text = I18n.obtenerString("InicioDirector", "actividad");
            btnItemActCursoEnt.Text = I18n.obtenerString("InicioDirector", "cursoEntrenamiento");
            generarCursoEntrenamientoStrings();
        }

        private void generarCursoEntrenamientoStrings()
        {
            //Agregar 
            tabItemACAgregar.Text = I18n.obtenerString("InicioDirector", "agregar");
            //Editar
            tabItemACEditar.Text = I18n.obtenerString("InicioDirector", "editar");
            btnACBuscarE.Text = btnACBuscarC.Text = I18n.obtenerString("InicioDirector", "buscar");
            //Consultar
            tabItemACConsultar.Text = I18n.obtenerString("InicioDirector", "consultar");
            gridViewACC.Columns["HNombre"].HeaderText = I18n.obtenerString("InicioDirector", "nombre");
            gridViewACC.Columns["HDescripcion"].HeaderText = I18n.obtenerString("InicioDirector", "descripcion");
            gridViewACC.Columns["HPrecio"].HeaderText = I18n.obtenerString("InicioDirector", "precio");
            gridViewACC.Columns["HDias"].HeaderText = I18n.obtenerString("InicioDirector", "dias");
            gridViewACC.Columns["HHoraInicio"].HeaderText = I18n.obtenerString("InicioDirector", "horaInicio");
            gridViewACC.Columns["HHoraFin"].HeaderText = I18n.obtenerString("InicioDirector", "horaFin");
            //Agregar + Editar + Consultar
            lblACCursoA.Text = lblACCursoE.Text = lblACCursoC.Text = I18n.obtenerString("InicioDirector", "cursoEntrenamiento");
            lblACNombreA.Text = lblACNombreE.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACDescA.Text = lblACDescE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "descripcion")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACDificultadA.Text = lblACDificultadE.Text = lblACDificultadC.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "dificultad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACPrecioA.Text = lblACPrecioE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "precio")).Append(Constantes.DOS_PUNTOS).ToString();
            groupBoxACDiasA.Text = groupBoxACDiasE.Text = groupACEntrenC.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "dias")).Append(Constantes.DOS_PUNTOS).ToString();
            checkACLunA.Text = checkACLunE.Text = I18n.obtenerString("InicioDirector", "lunes");
            checkACMarA.Text = checkACMarE.Text = I18n.obtenerString("InicioDirector", "martes");
            checkACMieA.Text = checkACMieE.Text = I18n.obtenerString("InicioDirector", "miercoles");
            checkACJueA.Text = checkACJueE.Text = I18n.obtenerString("InicioDirector", "jueves");
            checkACVieA.Text = checkACVieE.Text = I18n.obtenerString("InicioDirector", "viernes");
            lblACHoraIniA.Text = lblACHoraIniE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "horaInicio")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACHoraFinA.Text = lblACHoraFinE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "horaFin")).Append(Constantes.DOS_PUNTOS).ToString();
            groupACEntrenA.Text = groupACEntrenE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "entrenamientoDe")).Append(Constantes.DOS_PUNTOS).ToString();
            radioACMontA.Text = radioACMontE.Text = radioACMontC.Text = I18n.obtenerString("InicioDirector", "montania");
            radioACRunA.Text = radioACRunE.Text = radioACRunC.Text = I18n.obtenerString("InicioDirector", "running");
            radioACTrekA.Text = radioACTrekE.Text = radioACTrekC.Text = I18n.obtenerString("InicioDirector", "trekking");
            radioACBikeA.Text = radioACBikeE.Text = radioACBikeC.Text = I18n.obtenerString("InicioDirector", "bike");
            radioACAuxA.Text = radioACAuxE.Text = radioACAuxC.Text = I18n.obtenerString("InicioDirector", "auxilios");
            radioACGpsA.Text = radioACGpsE.Text = radioACGpsC.Text = I18n.obtenerString("InicioDirector", "orientacion");
            btnACGuardarA.Text = btnACGuardarE.Text = I18n.obtenerString("InicioDirector", "guardar");

        }

        private void generarProveedoresStrings()
        {
            lblProvAgrRazon.Text = lblProvEdiRazon.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "razonSocial")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrCuit.Text = lblProvEdiCuit.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "cuit")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrDireccion.Text = lblProvEdiDireccion.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "direccion")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrCiudad.Text = lblProvEdiCiudad.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "ciudad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrEmail.Text = lblProvEdiEmail.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "email")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrTelefono.Text = lblProvEdiTelefono.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "telefono")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrTarifa.Text = lblProvEdiTarifa.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tarifa")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrCapacidad.Text = lblProvEdiCapacidad.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "capacidad")).Append(Constantes.DOS_PUNTOS).ToString();
            btnProvAgrGuardar.Text = btnProvEdiGuardar.Text = I18n.obtenerString("InicioDirector", "guardar");
        }

        private void generarAlojamientoStrings()
        {
            sideBarPanelAlojamiento.Text = I18n.obtenerString("InicioDirector", "alojamiento");
            btnItemAgregarA.Text = I18n.obtenerString("InicioDirector", "agregar");
            btnItemEditarA.Text = I18n.obtenerString("InicioDirector", "editar");
            btnItemConsultarA.Text = I18n.obtenerString("InicioDirector", "consultar");
            lblProvAgr.Text = lblProvEdi.Text = I18n.obtenerString("InicioDirector", "alojamiento");
            lblProvAgrTipo.Text = lblProvEdiTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoAlojamiento")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrServicios.Text = lblProvEdiServicios.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "servicios")).Append(Constantes.DOS_PUNTOS).ToString();
            
        }

        private void generarTrasladoStrings()
        {
            sideBarPanelTraslados.Text = I18n.obtenerString("InicioDirector", "traslado");
            btnItemAgregarT.Text = I18n.obtenerString("InicioDirector", "agregar");
            btnItemEditarT.Text = I18n.obtenerString("InicioDirector", "editar");
            btnItemConsultarT.Text = I18n.obtenerString("InicioDirector", "consultar");
            lblProvAgr.Text = lblProvEdi.Text = I18n.obtenerString("InicioDirector", "traslado");
            lblProvAgrTipo.Text = lblProvEdiTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoTraslado")).Append(Constantes.DOS_PUNTOS).ToString();
        }

        private void generarInstructorStrings()
        {
            sideBarPanelInstructores.Text = I18n.obtenerString("InicioDirector", "instructor");
            btnItemAgregarI.Text = I18n.obtenerString("InicioDirector", "agregar");
            btnItemEditarI.Text = I18n.obtenerString("InicioDirector", "editar");
            btnItemConsultarI.Text = I18n.obtenerString("InicioDirector", "consultar");
            //agregar
            lblInstAgrApellido.Text = lblInstEditApellido.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "apellido")).Append(Constantes.DOS_PUNTOS).ToString();
            lblInstAgrNombre.Text = lblInstEditNombre.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            lblInstAgrDni.Text = lblInstEditDni.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "dni")).Append(Constantes.DOS_PUNTOS).ToString();
            lblInstAgrEmail.Text = lblInstEditEmail.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "email")).Append(Constantes.DOS_PUNTOS).ToString();
            lblInstAgrDireccion.Text = lblInstEditDireccion.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "direccion")).Append(Constantes.DOS_PUNTOS).ToString();
            lblInstAgrCiudad.Text = lblInstEditCiudad.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "ciudad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblInstAgrTelefono.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "telefono")).Append(Constantes.DOS_PUNTOS).ToString();
            dataGridInstAgr.Columns["HInstAgreEspecialidad"].HeaderText = I18n.obtenerString("InicioDirector", "especialidad");
            dataGridInstAgr.Columns["HInstAgreExperiencia"].HeaderText = I18n.obtenerString("InicioDirector", "experiencia");
            dataGridInstAgr.Columns["HInstAgreTarifa"].HeaderText = I18n.obtenerString("InicioDirector", "tarifa");
            btnInstAgrGuardar.Text = btnInstEditGuardar.Text = I18n.obtenerString("InicioDirector", "guardar");
            //editar
            lblInstEditLegajo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "legajo")).Append(Constantes.DOS_PUNTOS).ToString();
            radioGroupInstEditBuscar.Text = radioGroupInstCons.Text = I18n.obtenerString("InicioDirector", "buscar");
            radioInstEditDni.Text = radioInstConsDni.Text = I18n.obtenerString("InicioDirector", "dni");
            radioInstEditLegajo.Text = I18n.obtenerString("InicioDirector", "legajo");
            dataGridInstEdit.Columns["HInstEditEspecialidad"].HeaderText = I18n.obtenerString("InicioDirector", "especialidad");
            dataGridInstEdit.Columns["HInstEditExperiencia"].HeaderText = I18n.obtenerString("InicioDirector", "experiencia");
            dataGridInstEdit.Columns["HInstEditTarifa"].HeaderText = I18n.obtenerString("InicioDirector", "tarifa");
            checkInstEditEstado.Text = I18n.obtenerString("InicioDirector", "estado");
            btnInstEditBuscar.Text = btnInstConsBuscar.Text = I18n.obtenerString("InicioDirector", "buscar");
            //consultar
            lblInstCons.Text = I18n.obtenerString("InicioDirector", "instructor");
            radioInstConsApellido.Text = I18n.obtenerString("InicioDirector", "apellido");
            radioInstConsEspecialidad.Text = I18n.obtenerString("InicioDirector", "especialidad");
            dataGridInstCons.Columns["HInstConsLegajo"].HeaderText = I18n.obtenerString("InicioDirector", "legajo");
            dataGridInstCons.Columns["HInstConsEspecialidad"].HeaderText = I18n.obtenerString("InicioDirector", "especialidad");
            dataGridInstCons.Columns["HInstConsDni"].HeaderText = I18n.obtenerString("InicioDirector", "dni");
            dataGridInstCons.Columns["HInstConsApellido"].HeaderText = I18n.obtenerString("InicioDirector", "apellido");
            dataGridInstCons.Columns["HInstConsNombre"].HeaderText = I18n.obtenerString("InicioDirector", "nombre");
            dataGridInstCons.Columns["HInstConsExperiencia"].HeaderText = I18n.obtenerString("InicioDirector", "experiencia");
            dataGridInstCons.Columns["HInstConsTarifa"].HeaderText = I18n.obtenerString("InicioDirector", "tarifa");
            dataGridInstCons.Columns["HInstConsDireccion"].HeaderText = I18n.obtenerString("InicioDirector", "direccion");
            dataGridInstCons.Columns["HInstConsCiudad"].HeaderText = I18n.obtenerString("InicioDirector", "ciudad");
            dataGridInstCons.Columns["HInstConsEmail"].HeaderText = I18n.obtenerString("InicioDirector", "email");
            dataGridInstCons.Columns["HInstConsTelefono"].HeaderText = I18n.obtenerString("InicioDirector", "telefono");
            dataGridInstCons.Columns["HInstConsEstado"].HeaderText = I18n.obtenerString("InicioDirector", "estado");
        }

        private void limpiarCampos()
        {
            txtACNombreA.Text = "";
            richTxtACDescA.Text = "";
            doubleInACPrecioA.Text = "";
            checkACLunA.Checked = false;
            checkACMarA.Checked = false;
            checkACMieA.Checked = false;
            checkACJueA.Checked = false;
            checkACVieA.Checked = false;
            radioACMontA.Checked = true;

            radioAPEventoA.Checked = true;
            txtAPNombreA.Text = "";
            txtAPDestinoA.Text = "";
            doubleInAPPrecioA.Text = "";
            richTxtAPItinerarioA.Text = "";
            //ckdListBoxAPA
            radioAPCiclismoA.Checked = true;
            radioAPRunA.Checked = false;
            radioAPBikeA.Checked = false;

            richTxtACDescE.Text = "";
            checkACLunE.Checked = false;
            checkACMarE.Checked = false;
            checkACMieE.Checked = false;
            checkACJueE.Checked = false;
            checkACVieE.Checked = false;
            radioACMontE.Checked = true;

            radioAPEventoE.Checked = true;
            txtAPDestinoE.Text = "";
            richTxtAPItinerarioE.Text = "";
            //ckdListBoxAPE
            radioAPCiclismoE.Checked = true;

            radioACMontC.Checked = true;
            radioAPEventoC.Checked = true;

            txtProvAgrRazon.Text = "";
            txtProvAgrCuit.Text = "";
            txtProvAgrDireccion.Text = "";
            txtProvAgrCiudad.Text = "";
            txtProvAgrEmail.Text = "";
            txtProvAgrTelefono.Text = "";
            doubleInProvAgrTarifa.Text = "";
            integerInProvAgrCap.Text = "";
            richTxtProvAgrServicios.Text = "";

            txtProvEdiFiltro.Text = "";
            txtProvEdiRazon.Text = "";
            txtProvEdiCuit.Text = "";
            txtProvEdiDireccion.Text = "";
            txtProvEdiCiudad.Text = "";
            txtProvEdiEmail.Text = "";
            txtProvEdiTelefono.Text = "";
            doubleInProvEdiTarifa.Text = "";
            integerInProvEdiCap.Text = "";
            richTxtProvEdiServicios.Text = "";
            lblEdiId.Text = "";

            txtInstAgrApellido.Text = "";
            txtInstAgrNombre.Text = "";
            txtInstAgrDni.Text = "";
            txtInstAgrDireccion.Text = "";
            txtInstAgrCiudad.Text = "";
            txtInstAgrEmail.Text = "";
            txtInstAgrTelefono.Text = "";
            dataGridInstAgr.Rows.Clear();

            txtInstEditFiltro.Text = "";
            txtInstEditLegajo.Text = "";
            txtInstEditNombre.Text = "";
            txtInstEditApellido.Text = "";
            txtInstEditDni.Text = "";
            txtInstEditEmail.Text = "";
            txtInstEditTelefono.Text = "";
            txtInstEditDireccion.Text = "";
            txtInstEditCiudad.Text = "";
            checkInstEditEstado.Checked = false;
            dataGridInstEdit.Rows.Clear();

            txtInstConsFiltro.Text = "";
            radioInstConsDni.Checked = true;
            dataGridInstCons.Rows.Clear();
        }

        private void btnItemCalA_Click(object sender, EventArgs e)
        {
            
        }

        private void btnItemCalC_Click(object sender, EventArgs e)
        {

        }

        private void btnItemCalE_Click(object sender, EventArgs e)
        {

        }

        private void btnItemActCursoEnt_Click(object sender, EventArgs e)
        {

            tabItemACAgregar.Visible = true;
            tabItemACEditar.Visible = true;
            tabItemACConsultar.Visible = true;   
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            superTabControlDir.Visible = true;
            superTabControlDir.SelectedTab = tabItemACAgregar;
            limpiarCampos();
        }

        private void btnItemActPaquete_Click(object sender, EventArgs e)
        {
            tabItemAPAgregar.Visible = true;
            tabItemAPEditar.Visible = true;
            tabItemAPConsultar.Visible = true; 
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            superTabControlDir.Visible = true;
            superTabControlDir.SelectedTab = tabItemAPAgregar;
            limpiarCampos();
        }

        private void btnItemAgregarA_Click(object sender, EventArgs e)
        {
            lblProvAgr.Text = I18n.obtenerString("InicioDirector", "alojamiento");
            if (btnItemEditarA.Visible == false)
                tabItemProvEditar.Visible = false;
            else if (btnItemEditarA.Visible == true)
                tabItemProvEditar.Visible = true;
             if (btnItemConsultarA.Visible == false)
                 tabItemProvConsultar.Visible = false;
             else if (btnItemConsultarA.Visible == true)
                 tabItemProvConsultar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
            lblProvAgrTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoAlojamiento")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrServicios.Visible = true;
            richTxtProvAgrServicios.Visible = true;
            tabItemProvAgregar.Visible = true;
            superTabControlDir.SelectedTab = tabItemProvAgregar;
            limpiarCampos();
        }

        private void btnItemConsultarA_Click(object sender, EventArgs e)
        {
            lblProvCons.Text = I18n.obtenerString("InicioDirector", "alojamiento");
            if (btnItemAgregarA.Visible == false)
                tabItemProvAgregar.Visible = false;
            else if (btnItemAgregarA.Visible == true)
            {
                comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
                lblProvAgrTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoAlojamiento")).Append(Constantes.DOS_PUNTOS).ToString();
                lblProvAgrServicios.Visible = true;
                richTxtProvAgrServicios.Visible = true;
                tabItemProvAgregar.Visible = true;
            }
            if (btnItemEditarA.Visible == false)
                tabItemProvEditar.Visible = false;
            else if (btnItemEditarA.Visible == true)
            {
                comboBoxProvEdiTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
                lblProvEdiTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoAlojamiento")).Append(Constantes.DOS_PUNTOS).ToString();
                lblProvEdiServicios.Visible = true;
                richTxtProvEdiServicios.Visible = true;
                tabItemProvEditar.Visible = true;
            }
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboProvConsFiltro.Visible = false;
            dataGridProvCons.Visible = false;
            radioProvConsCuit.Checked = true;
            txtProvConsFiltro.Text = "";
            radioProvConsTipo.Text = I18n.obtenerString("InicioDirector", "tipoAlojamiento");
            tabItemProvConsultar.Visible = true;
            superTabControlDir.SelectedTab = tabItemProvConsultar;
            limpiarCampos();
        }

        private void btnItemEditarA_Click(object sender, EventArgs e)
        {
            lblProvEdi.Text = I18n.obtenerString("InicioDirector", "alojamiento");
            if (btnItemAgregarA.Visible == false)
                tabItemProvAgregar.Visible = false;
            else if (btnItemAgregarA.Visible == true)
            {
                comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
                lblProvAgrTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoAlojamiento")).Append(Constantes.DOS_PUNTOS).ToString();
                lblProvAgrServicios.Visible = true;
                richTxtProvAgrServicios.Visible = true;
                tabItemProvAgregar.Visible = true;
            }
            if (btnItemConsultarA.Visible == false)
                tabItemProvConsultar.Visible = false;
            else if (btnItemConsultarA.Visible == true)
                tabItemProvConsultar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            tabItemProvEditar.Visible = true;
            superTabControlDir.SelectedTab = tabItemProvEditar;
            limpiarCampos();
        }

        private void btnItemAgregarT_Click(object sender, EventArgs e)
        {
            lblProvAgr.Text = I18n.obtenerString("InicioDirector", "traslado");
            if (btnItemEditarT.Visible == false)
                tabItemProvEditar.Visible = false;
            else if (btnItemEditarT.Visible == true)
                tabItemProvEditar.Visible = true;
            if (btnItemConsultarT.Visible == false)
                tabItemProvConsultar.Visible = false;
            else if (btnItemConsultarT.Visible == true)
                tabItemProvConsultar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumVehiculo)).Cast<EnumVehiculo>().ToList();
            lblProvAgrTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoTraslado")).Append(Constantes.DOS_PUNTOS).ToString();
            lblProvAgrServicios.Visible = false;
            richTxtProvAgrServicios.Visible = false;
            tabItemProvAgregar.Visible = true;
            superTabControlDir.SelectedTab = tabItemProvAgregar;
            limpiarCampos();
        }

        private void btnItemConsultarT_Click(object sender, EventArgs e)
        {
            lblProvCons.Text = I18n.obtenerString("InicioDirector", "traslado");
            if (btnItemAgregarT.Visible == false)
                tabItemProvAgregar.Visible = false;
            else if (btnItemAgregarT.Visible == true)
            {
                comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumVehiculo)).Cast<EnumVehiculo>().ToList();
                lblProvAgrTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoTraslado")).Append(Constantes.DOS_PUNTOS).ToString();
                lblProvAgrServicios.Visible = false;
                richTxtProvAgrServicios.Visible = false;
                tabItemProvAgregar.Visible = true;
            }
            if (btnItemEditarT.Visible == false)
                tabItemProvEditar.Visible = false;
            else if (btnItemEditarT.Visible == true)
            {
                comboBoxProvEdiTipo.DataSource = Enum.GetValues(typeof(EnumVehiculo)).Cast<EnumVehiculo>().ToList();
                lblProvEdiTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoTraslado")).Append(Constantes.DOS_PUNTOS).ToString();
                lblProvEdiServicios.Visible = true;
                richTxtProvEdiServicios.Visible = true;
                tabItemProvEditar.Visible = true;
            }
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboProvConsFiltro.Visible = false;
            dataGridProvCons.Visible = false;
            txtProvConsFiltro.Text = "";
            radioProvConsCuit.Checked = true;
            radioProvConsTipo.Text = I18n.obtenerString("InicioDirector", "tipoTraslado");
            tabItemProvConsultar.Visible = true;
            superTabControlDir.SelectedTab = tabItemProvConsultar;
            limpiarCampos();
        }

        private void btnItemEditarT_Click(object sender, EventArgs e)
        {
            lblProvEdi.Text = I18n.obtenerString("InicioDirector", "traslado");
            if (btnItemAgregarT.Visible == false)
                tabItemProvAgregar.Visible = false;
            else if (btnItemAgregarT.Visible == true)
            {
                comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumVehiculo)).Cast<EnumVehiculo>().ToList();
                lblProvAgrTipo.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "tipoTraslado")).Append(Constantes.DOS_PUNTOS).ToString();
                lblProvAgrServicios.Visible = false;
                richTxtProvAgrServicios.Visible = false;
                tabItemProvAgregar.Visible = true;
            }
            if (btnItemConsultarT.Visible == false)
                tabItemProvConsultar.Visible = false;
            else if (btnItemConsultarT.Visible == true)
                tabItemProvConsultar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            tabItemProvEditar.Visible = true;
            superTabControlDir.SelectedTab = tabItemProvEditar;
            limpiarCampos();
        }

        private void btnProvGuardar_Click(object sender, EventArgs e)
        {
            String razonSocial = (!String.IsNullOrEmpty(txtProvAgrRazon.Text)) ? txtProvAgrRazon.Text : null;
            String cuit = (!String.IsNullOrEmpty(txtProvAgrCuit.Text)) ? txtProvAgrCuit.Text : null;
            String email = (!String.IsNullOrEmpty(txtProvAgrEmail.Text)) ? txtProvAgrEmail.Text : null;
            if (verificarCampos(razonSocial, cuit, email))
            {
                if (lblProvAgr.Text == I18n.obtenerString("InicioDirector", "alojamiento"))
                {
                    guardarAlojamiento(razonSocial, cuit, email);

                }
                else if (lblProvAgr.Text == I18n.obtenerString("InicioDirector", "traslado"))
                {
                    guardarTraslado(razonSocial, cuit, email);
                }
            }

        }

        private void guardarTraslado(String razonSocial, String cuit, String email)
        {
            Traslado tras = new Traslado();
            tras.razonSocial = razonSocial;
            tras.cuit = cuit;
            tras.email = email;
            tras.direccion = (!String.IsNullOrEmpty(txtProvAgrDireccion.Text)) ? txtProvAgrDireccion.Text : "";
            tras.ciudad = (!String.IsNullOrEmpty(txtProvAgrCiudad.Text)) ? txtProvAgrCiudad.Text : "";
            tras.telefono = (!String.IsNullOrEmpty(txtProvAgrTelefono.Text)) ? txtProvAgrTelefono.Text : "";
            tras.tarifa = (!String.IsNullOrEmpty(doubleInProvAgrTarifa.Text)) ? Double.Parse(doubleInProvAgrTarifa.Text) : 0;
            tras.capacidad = (!String.IsNullOrEmpty(integerInProvAgrCap.Text)) ? Int32.Parse(integerInProvAgrCap.Text) : 0;
            tras.vehiculo = comboBoxProvAgrTipo.SelectedItem.ToString();
            String mensaje = null;
            try
            {
                Traslado trasRes = trasladoBLL.obtenerTrasladoPorCuit(tras.cuit);
                if (trasRes == null)
                {

                    DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (siNoRes.Equals(DialogResult.Yes))
                    {
                        tras.id = trasladoBLL.agregarTraslado(tras);
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "proveedorNuevo"), I18n.obtenerString("InicioDirector", "traslado"), tras.razonSocial, tras.cuit);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                }
                else
                {
                    mensaje = String.Format(I18n.obtenerString("Mensaje", "proveedorYaExiste"), I18n.obtenerString("InicioDirector", "traslado"), tras.razonSocial, tras.cuit);
                    MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Traslado", ex.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
            }

        }

        private void guardarAlojamiento(String razonSocial, String cuit, String email)
        {
            Alojamiento aloj = new Alojamiento();
            aloj.razonSocial = razonSocial;
            aloj.cuit = cuit;
            aloj.email = email;
            aloj.direccion = (!String.IsNullOrEmpty(txtProvAgrDireccion.Text)) ? txtProvAgrDireccion.Text : "";
            aloj.ciudad = (!String.IsNullOrEmpty(txtProvAgrCiudad.Text)) ? txtProvAgrCiudad.Text : "";
            aloj.telefono = (!String.IsNullOrEmpty(txtProvAgrTelefono.Text)) ? txtProvAgrTelefono.Text : "";
            aloj.tarifa = (!String.IsNullOrEmpty(doubleInProvAgrTarifa.Text)) ? Double.Parse(doubleInProvAgrTarifa.Text) : 0;
            aloj.categoria = comboBoxProvAgrTipo.SelectedItem.ToString();
            aloj.capacidad = (!String.IsNullOrEmpty(integerInProvAgrCap.Text)) ? Int32.Parse(integerInProvAgrCap.Text) : 0;
            aloj.servicios = (!String.IsNullOrEmpty(richTxtProvAgrServicios.Text)) ? richTxtProvAgrServicios.Text : ""; 
            String mensaje = null;
            try
            {
                Alojamiento alojRes = alojamientoBLL.obtenerAlojamientoPorCuit(aloj.cuit);
                if (alojRes == null)
                {

                    DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (siNoRes.Equals(DialogResult.Yes))
                    {
                        aloj.id = alojamientoBLL.agregarAlojamiento(aloj);
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "proveedorNuevo"), I18n.obtenerString("InicioDirector", "alojamiento"), aloj.razonSocial, aloj.cuit);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarCampos();
                    }
                }
                else
                {
                    mensaje = String.Format(I18n.obtenerString("Mensaje", "proveedorYaExiste"), I18n.obtenerString("InicioDirector", "alojamiento"), aloj.razonSocial, aloj.cuit);
                    MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Alojamiento", ex.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
            }
        }

        private Boolean verificarCampos(string razonSocial, string cuit, string email)
        {
            

            if (String.IsNullOrEmpty(razonSocial) || String.IsNullOrEmpty(cuit) || String.IsNullOrEmpty(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "razonCuitEmailRequeridos"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!Validacion.ValidaCuit(cuit))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoCuit"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!Validacion.esEmailValido(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoEmail"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
            Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.LOGOUT, "");
            BitacoraBLL.registrarBitacora(bitacora);
        }

        private void btnProvEdiBuscar_Click(object sender, EventArgs e)
        {
            if (lblProvEdi.Text == I18n.obtenerString("InicioDirector", "alojamiento"))
            {
                Alojamiento alo = new Alojamiento();
                String filtro = (!String.IsNullOrEmpty(txtProvEdiFiltro.Text)) ? txtProvEdiFiltro.Text : null;
                if (radioEdiCuit.Checked && filtro != null)
                {
                    alo = alojamientoBLL.obtenerAlojamientoPorCuit(filtro);
                }
                else if (radioEdiRazon.Checked && filtro != null)
                {
                    alo = alojamientoBLL.obtenerAlojamientoPorRazon(filtro);
                }
                if (alo != null)
                {
                    txtProvEdiRazon.Text = alo.razonSocial;
                    txtProvEdiCuit.Enabled = false;
                    txtProvEdiCuit.Text = alo.cuit;
                    txtProvEdiDireccion.Text = alo.direccion;
                    txtProvEdiCiudad.Text = alo.ciudad;
                    txtProvEdiEmail.Text = alo.email;
                    txtProvEdiTelefono.Text = alo.telefono;
                    doubleInProvEdiTarifa.Value = alo.tarifa;
                    comboBoxProvEdiTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
                    comboBoxProvEdiTipo.SelectedItem = (EnumCategoria)Enum.Parse(typeof(EnumCategoria), alo.categoria);
                    integerInProvEdiCap.Value = alo.capacidad;
                    richTxtProvEdiServicios.Text = alo.servicios;
                    lblEdiId.Text = alo.id.ToString();
                }else
                {
                    MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (lblProvEdi.Text == I18n.obtenerString("InicioDirector", "traslado"))
            {
                Traslado tras = new Traslado();
                String filtro = (!String.IsNullOrEmpty(txtProvEdiFiltro.Text)) ? txtProvEdiFiltro.Text : null;
                if (radioEdiCuit.Checked && filtro != null)
                {
                    tras = trasladoBLL.obtenerTrasladoPorCuit(filtro);
                }
                else if (radioEdiRazon.Checked && filtro != null)
                {
                    tras = trasladoBLL.obtenerTrasladoPorRazon(filtro);
                }
                if (tras != null)
                {
                    txtProvEdiRazon.Text = tras.razonSocial;
                    txtProvEdiCuit.Enabled = false;
                    txtProvEdiCuit.Text = tras.cuit;
                    txtProvEdiDireccion.Text = tras.direccion;
                    txtProvEdiCiudad.Text = tras.ciudad;
                    txtProvEdiEmail.Text = tras.email;
                    txtProvEdiTelefono.Text = tras.telefono;
                    doubleInProvEdiTarifa.Value = tras.tarifa;
                    comboBoxProvEdiTipo.DataSource = Enum.GetValues(typeof(EnumVehiculo)).Cast<EnumVehiculo>().ToList();
                    comboBoxProvEdiTipo.SelectedItem = (EnumVehiculo)Enum.Parse(typeof(EnumVehiculo), tras.vehiculo);
                    integerInProvEdiCap.Value = tras.capacidad;
                    lblProvEdiServicios.Visible = false;
                    richTxtProvEdiServicios.Visible = false;
                    lblEdiId.Text = tras.id.ToString();
                }
                else
                {
                    MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnProvEdiGuardar_Click(object sender, EventArgs e)
        {
            String razonSocial = (!String.IsNullOrEmpty(txtProvEdiRazon.Text)) ? txtProvEdiRazon.Text : null;
            String cuit = (!String.IsNullOrEmpty(txtProvEdiCuit.Text)) ? txtProvEdiCuit.Text : null;
            String email = (!String.IsNullOrEmpty(txtProvEdiEmail.Text)) ? txtProvEdiEmail.Text : null;
            String mensaje = null;
            if (verificarCampos(razonSocial, cuit, email))
            {
                if (lblProvEdi.Text == I18n.obtenerString("InicioDirector", "alojamiento"))
                {
                    Alojamiento aloj = new Alojamiento();
                    aloj.razonSocial = razonSocial;
                    aloj.cuit = cuit;
                    aloj.email = email;
                    aloj.direccion = (!String.IsNullOrEmpty(txtProvEdiDireccion.Text)) ? txtProvEdiDireccion.Text : "";
                    aloj.ciudad = (!String.IsNullOrEmpty(txtProvEdiCiudad.Text)) ? txtProvEdiCiudad.Text : "";
                    aloj.telefono = (!String.IsNullOrEmpty(txtProvEdiTelefono.Text)) ? txtProvEdiTelefono.Text : "";
                    aloj.tarifa = (!String.IsNullOrEmpty(doubleInProvEdiTarifa.Text)) ? Double.Parse(doubleInProvEdiTarifa.Text) : 0;
                    aloj.categoria = comboBoxProvEdiTipo.SelectedItem.ToString();
                    aloj.capacidad = (!String.IsNullOrEmpty(integerInProvEdiCap.Text)) ? Int32.Parse(integerInProvEdiCap.Text) : 0;
                    aloj.servicios = (!String.IsNullOrEmpty(richTxtProvEdiServicios.Text)) ? richTxtProvEdiServicios.Text : "";
                    aloj.id = (!String.IsNullOrEmpty(lblEdiId.Text)) ? Int32.Parse(lblEdiId.Text) : 0;
                    try
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (siNoRes.Equals(DialogResult.Yes))
                            {
                                
                                alojamientoBLL.actualizarAlojamiento(aloj);
                                mensaje = String.Format(I18n.obtenerString("Mensaje", "proveedorActualizado"), I18n.obtenerString("InicioDirector", "alojamiento"), aloj.razonSocial, aloj.cuit);
                                MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                limpiarCampos();
                            }
                     
                    }
                    catch (Exception ex)
                    {
                        Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_UPD + " alojamiento", ex.ToString());
                        BitacoraBLL.registrarBitacora(bitacora);
                    }
                }
                else if (lblProvEdi.Text == I18n.obtenerString("InicioDirector", "traslado"))
                {
                    Traslado tras = new Traslado();
                    tras.razonSocial = razonSocial;
                    tras.cuit = cuit;
                    tras.email = email;
                    tras.direccion = (!String.IsNullOrEmpty(txtProvEdiDireccion.Text)) ? txtProvEdiDireccion.Text : "";
                    tras.ciudad = (!String.IsNullOrEmpty(txtProvEdiCiudad.Text)) ? txtProvEdiCiudad.Text : "";
                    tras.telefono = (!String.IsNullOrEmpty(txtProvEdiTelefono.Text)) ? txtProvEdiTelefono.Text : "";
                    tras.tarifa = (!String.IsNullOrEmpty(doubleInProvEdiTarifa.Text)) ? Double.Parse(doubleInProvEdiTarifa.Text) : 0;
                    tras.capacidad = (!String.IsNullOrEmpty(integerInProvEdiCap.Text)) ? Int32.Parse(integerInProvEdiCap.Text) : 0;
                    tras.vehiculo = comboBoxProvEdiTipo.SelectedItem.ToString();
                    tras.id = (!String.IsNullOrEmpty(lblEdiId.Text)) ? Int32.Parse(lblEdiId.Text) : 0;
                    try
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {

                            trasladoBLL.actualizarTraslado(tras);
                            mensaje = String.Format(I18n.obtenerString("Mensaje", "proveedorActualizado"), I18n.obtenerString("InicioDirector", "traslado"), tras.razonSocial, tras.cuit);
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }

                    }
                    catch (Exception ex)
                    {
                        Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_UPD + " traslado", ex.ToString());
                        BitacoraBLL.registrarBitacora(bitacora);
                    }
                }
            }
        }

        private void radioProvConsCuit_Click(object sender, EventArgs e)
        {
            txtProvConsFiltro.Text = "";
            txtProvConsFiltro.Visible = true;
            comboProvConsFiltro.Visible = false;
        }

        private void radioProvConsRazon_Click(object sender, EventArgs e)
        {
            txtProvConsFiltro.Text = "";
            txtProvConsFiltro.Visible = true;
            comboProvConsFiltro.Visible = false;
        }

        private void radioProvConsCiudad_Click(object sender, EventArgs e)
        {
            txtProvConsFiltro.Visible = false;
            if (lblProvCons.Text == I18n.obtenerString("InicioDirector", "alojamiento"))
                comboProvConsFiltro.DataSource = alojamientoBLL.obtenerAlojamientos().GroupBy(a => a.ciudad).Select(g => g.First().ciudad).ToList();
            else if (lblProvCons.Text == I18n.obtenerString("InicioDirector", "traslado"))
                comboProvConsFiltro.DataSource = trasladoBLL.obtenerTraslados().GroupBy(t => t.ciudad).Select(g => g.First().ciudad).ToList();
            comboProvConsFiltro.Visible = true;
        }

        private void radioProvConsTipo_Click(object sender, EventArgs e)
        {
            txtProvConsFiltro.Visible = false;
            if (lblProvCons.Text == I18n.obtenerString("InicioDirector", "alojamiento"))
                comboProvConsFiltro.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();
            else if (lblProvCons.Text == I18n.obtenerString("InicioDirector", "traslado"))
                comboProvConsFiltro.DataSource = Enum.GetValues(typeof(EnumVehiculo)).Cast<EnumVehiculo>().ToList();
            comboProvConsFiltro.Visible = true;
        }

        private void btnProvConsBuscar_Click(object sender, EventArgs e)
        {
            dataGridProvCons.Rows.Clear();
            if (lblProvCons.Text == I18n.obtenerString("InicioDirector", "alojamiento"))
            {
                Alojamiento alo = new Alojamiento();
                List<Alojamiento> aloLista = new List<Alojamiento>();
                if (radioProvConsCuit.Checked && !String.IsNullOrEmpty(txtProvConsFiltro.Text))
                {
                    alo = alojamientoBLL.obtenerAlojamientoPorCuit(txtProvConsFiltro.Text);
                    aloLista.Add(alo);
                }
                else if (radioProvConsRazon.Checked && !String.IsNullOrEmpty(txtProvConsFiltro.Text))
                {
                    alo = alojamientoBLL.obtenerAlojamientoPorRazon(txtProvConsFiltro.Text);
                    aloLista.Add(alo);
                }
                else if (radioProvConsCiudad.Checked)
                {
                    String ciudad = comboProvConsFiltro.SelectedItem.ToString();
                    aloLista = alojamientoBLL.obtenerAlojamientos().Where(a => a.ciudad == ciudad).ToList();
                }
                else if (radioProvConsTipo.Checked)
                {
                    String categoria = comboProvConsFiltro.SelectedItem.ToString();
                    aloLista = alojamientoBLL.obtenerAlojamientos().Where(a => a.categoria == categoria).ToList();
                }
                if (aloLista != null && aloLista.Count > 0)
                {
                    dataGridProvCons.Visible = true;
                    dataGridProvCons.Columns[3].HeaderText = I18n.obtenerString("InicioDirector", "tipoAlojamiento");
                    for (int i = 0; i < aloLista.Count; i++)
                    {
                        dataGridProvCons.Rows.Add(1);
                        dataGridProvCons.Rows[i].Cells["HProvConsCuit"].Value = aloLista[i].cuit;
                        dataGridProvCons.Rows[i].Cells["HProvConsRazon"].Value = aloLista[i].razonSocial;
                        dataGridProvCons.Rows[i].Cells["HProvConsDireccion"].Value = aloLista[i].direccion;
                        dataGridProvCons.Rows[i].Cells["HProvConsCiudad"].Value = aloLista[i].ciudad;
                        dataGridProvCons.Rows[i].Cells["HProvConsCategoria"].Value = aloLista[i].categoria;
                        dataGridProvCons.Rows[i].Cells["HProvConsTarifa"].Value = Constantes.MONEDA + aloLista[i].tarifa.ToString();
                        dataGridProvCons.Rows[i].Cells["HProvConsCapacidad"].Value = aloLista[i].capacidad.ToString();
                        dataGridProvCons.Rows[i].Cells["HProvConsEmail"].Value = aloLista[i].email;
                        dataGridProvCons.Rows[i].Cells["HProvConsTelefono"].Value = aloLista[i].telefono;
                        dataGridProvCons.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    }
                }
                else
                {
                    dataGridProvCons.Visible = false;
                    MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else if (lblProvCons.Text == I18n.obtenerString("InicioDirector", "traslado"))
            {
                Traslado tras = new Traslado();
                List<Traslado> trasLista = new List<Traslado>();
                if (radioProvConsCuit.Checked && !String.IsNullOrEmpty(txtProvConsFiltro.Text))
                {
                    tras = trasladoBLL.obtenerTrasladoPorCuit(txtProvConsFiltro.Text);
                    trasLista.Add(tras);
                }
                else if (radioProvConsRazon.Checked && !String.IsNullOrEmpty(txtProvConsFiltro.Text))
                {
                    tras = trasladoBLL.obtenerTrasladoPorRazon(txtProvConsFiltro.Text);
                    trasLista.Add(tras);
                }
                else if (radioProvConsCiudad.Checked)
                {
                    String ciudad = comboProvConsFiltro.SelectedItem.ToString();
                    trasLista = trasladoBLL.obtenerTraslados().Where(a => a.ciudad == ciudad).ToList();
                }
                else if (radioProvConsTipo.Checked)
                {
                    String vehiculo = comboProvConsFiltro.SelectedItem.ToString();
                    trasLista = trasladoBLL.obtenerTraslados().Where(a => a.vehiculo == vehiculo).ToList();
                }
                if (trasLista != null && trasLista.Count > 0)
                {
                    dataGridProvCons.Columns[3].HeaderText = I18n.obtenerString("InicioDirector", "tipoTraslado");
                    dataGridProvCons.Visible = true;
                    for (int i = 0; i < trasLista.Count; i++)
                    {
                        dataGridProvCons.Rows.Add(1);
                        dataGridProvCons.Rows[i].Cells["HProvConsCuit"].Value = trasLista[i].cuit;
                        dataGridProvCons.Rows[i].Cells["HProvConsRazon"].Value = trasLista[i].razonSocial;
                        dataGridProvCons.Rows[i].Cells["HProvConsDireccion"].Value = trasLista[i].direccion;
                        dataGridProvCons.Rows[i].Cells["HProvConsCiudad"].Value = trasLista[i].ciudad;   
                        dataGridProvCons.Rows[i].Cells["HProvConsCategoria"].Value = trasLista[i].vehiculo;
                        dataGridProvCons.Rows[i].Cells["HProvConsTarifa"].Value = Constantes.MONEDA + trasLista[i].tarifa.ToString();
                        dataGridProvCons.Rows[i].Cells["HProvConsCapacidad"].Value = trasLista[i].capacidad.ToString();
                        dataGridProvCons.Rows[i].Cells["HProvConsEmail"].Value = trasLista[i].email;
                        dataGridProvCons.Rows[i].Cells["HProvConsTelefono"].Value = trasLista[i].telefono;
                        dataGridProvCons.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    }
                }
                else
                {
                    dataGridProvCons.Visible = false;
                    MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void radioInstConsDni_Click(object sender, EventArgs e)
        {
            txtInstConsFiltro.Text = "";
            txtInstConsFiltro.Visible = true;
            comboInstConsFiltro.Visible = false;
        }

        private void radioInstConsApellido_Click(object sender, EventArgs e)
        {
            txtInstConsFiltro.Text = "";
            txtInstConsFiltro.Visible = true;
            comboInstConsFiltro.Visible = false;
        }

        private void radioInstConsEspecialidad_Click(object sender, EventArgs e)
        {
            txtInstConsFiltro.Visible = false;
            comboInstConsFiltro.DataSource = instructorBLL.obtenerEspecialidades().Select(es => es.descripcion).ToList();
            comboInstConsFiltro.Visible = true;
        }

        private void btnInstConsBuscar_Click(object sender, EventArgs e)
        {
            dataGridInstCons.Rows.Clear();
            Instructor inst = new Instructor();
            List<Instructor> instLista = new List<Instructor>();
            if (radioInstConsDni.Checked && !String.IsNullOrEmpty(txtInstConsFiltro.Text))
            {
                inst = instructorBLL.obtenerInstructorPorDni(txtInstConsFiltro.Text);
                instLista.Add(inst);
                
            }
            else if (radioInstConsApellido.Checked && !String.IsNullOrEmpty(txtInstConsFiltro.Text))
            {
                inst = instructorBLL.obtenerInstructorPorApellido(txtInstConsFiltro.Text);
                instLista.Add(inst);
            }
            else if (radioInstConsEspecialidad.Checked)
            {
                String especialidad = comboInstConsFiltro.SelectedItem.ToString();
                instLista = instructorBLL.obtenerInstructoresPorEspecialidad(especialidad);
                
            }
            if (instLista != null && instLista.Count > 0)
            {
                dataGridInstCons.Visible = true;
                for (int i = 0; i < instLista.Count; i++)
                {

                            dataGridInstCons.Rows.Add(1);
                            dataGridInstCons.Rows[i].Cells["HInstConsLegajo"].Value = instLista[i].legajo.ToString();
                            dataGridInstCons.Rows[i].Cells["HInstConsDni"].Value = instLista[i].dni;
                            dataGridInstCons.Rows[i].Cells["HInstConsApellido"].Value = instLista[i].apellido;
                            dataGridInstCons.Rows[i].Cells["HInstConsNombre"].Value = instLista[i].nombre;
                            dataGridInstCons.Rows[i].Cells["HInstConsDireccion"].Value = instLista[i].domicilio;
                            dataGridInstCons.Rows[i].Cells["HInstConsCiudad"].Value = instLista[i].ciudad;
                            dataGridInstCons.Rows[i].Cells["HInstConsEmail"].Value = instLista[i].email;
                            dataGridInstCons.Rows[i].Cells["HInstConsTelefono"].Value = instLista[i].telefono;
                            dataGridInstCons.Rows[i].Cells["HInstConsEstado"].Value = instLista[i].estado;

                            dataGridInstCons.Rows[i].Cells["HInstConsExperiencia"].Value = instLista[i].especialidadLista[0].experiencia;
                            dataGridInstCons.Rows[i].Cells["HInstConsTarifa"].Value = instLista[i].especialidadLista[0].tarifa.ToString();
                            dataGridInstCons.Rows[i].Cells["HInstConsEspecialidad"].Value = instLista[i].especialidadLista[0].descripcion;
                            dataGridInstCons.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
            }
            else
            {
                dataGridProvCons.Visible = false;
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnItemConsultarI_Click(object sender, EventArgs e)
        {
            if (btnItemAgregarI.Visible == false)
                tabItemInstAgregar.Visible = false;
            else if (btnItemAgregarI.Visible == true)
                tabItemInstAgregar.Visible = true;
            if (btnItemEditarI.Visible == false)
                tabItemInstEditar.Visible = false;
            else if (btnItemEditarI.Visible == true)
                tabItemInstEditar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboInstConsFiltro.Visible = false;
            dataGridProvCons.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            dataGridInstCons.Visible = false;
            comboInstConsFiltro.Visible = false;
            tabItemInstAgregar.Visible = true;
            tabItemInstEditar.Visible = true;
            tabItemInstConsultar.Visible = true;
            superTabControlDir.SelectedTab = tabItemInstConsultar;
            limpiarCampos();
        }

        private void btnItemAgregarI_Click(object sender, EventArgs e)
        {
            if (btnItemEditarI.Visible == false)
                tabItemInstEditar.Visible = false;
            else if (btnItemEditarI.Visible == true)
                tabItemInstEditar.Visible = true;
            if (btnItemConsultarI.Visible == false)
                tabItemInstConsultar.Visible = false;
            else if (btnItemConsultarI.Visible == true)
                tabItemInstConsultar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboInstConsFiltro.Visible = false;
            dataGridProvCons.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemInstAgregar.Visible = true;
            tabItemInstEditar.Visible = true;
            tabItemInstConsultar.Visible = true;
            superTabControlDir.SelectedTab = tabItemInstAgregar;
            limpiarCampos();
        }

        private void btnInstAgrGuardar_Click(object sender, EventArgs e)
        {
            String nombre = (!String.IsNullOrEmpty(txtInstAgrNombre.Text)) ? txtInstAgrNombre.Text : null;
            String apellido = (!String.IsNullOrEmpty(txtInstAgrApellido.Text)) ? txtInstAgrApellido.Text : null;
            String dni = (!String.IsNullOrEmpty(txtInstAgrDni.Text)) ? txtInstAgrDni.Text : null; 
            String email = (!String.IsNullOrEmpty(txtInstAgrEmail.Text)) ? txtInstAgrEmail.Text : null;
            if (String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(apellido) || String.IsNullOrEmpty(dni) || String.IsNullOrEmpty(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "nombreApellidoDniEmailRequeridos"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esNumeroValido(dni))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoDni"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esEmailValido(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoEmail"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(dataGridInstAgr.Rows.Count < 1){
                MessageBox.Show(I18n.obtenerString("Mensaje", "especialidadesRequeridas"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Instructor inst = new Instructor();
                inst.nombre = nombre;
                inst.apellido = apellido;
                inst.dni = dni;
                inst.email = email;
                inst.domicilio = (!String.IsNullOrEmpty(txtInstAgrDireccion.Text)) ? txtInstAgrDireccion.Text : "";
                inst.ciudad = (!String.IsNullOrEmpty(txtInstAgrCiudad.Text)) ? txtInstAgrCiudad.Text : "";
                inst.telefono = (!String.IsNullOrEmpty(txtInstAgrTelefono.Text)) ? txtInstAgrTelefono.Text : "";
                inst.estado = true;
                List<Especialidad> listaEsp = new List<Especialidad>();

                foreach (DataGridViewRow Datarow in dataGridInstAgr.Rows)
                {
                    if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null && Datarow.Cells[2].Value != null)
                    {
                        Especialidad esp = new Especialidad();
                        esp.descripcion = Datarow.Cells[0].Value.ToString();
                        esp.experiencia = Int32.Parse(Datarow.Cells[1].Value.ToString());
                        esp.tarifa = Double.Parse(Datarow.Cells[2].Value.ToString());
                        listaEsp.Add(esp);
                    }
                }
                inst.especialidadLista = listaEsp;
                String mensaje = null;
                try
                {
                    Instructor instRes = instructorBLL.obtenerInstructorPorDni(inst.dni);
                    if (instRes == null)
                    {

                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            inst.legajo = instructorBLL.agregarInstructor(inst);
                            mensaje = String.Format(I18n.obtenerString("Mensaje", "instructorNuevo"), inst.apellido, inst.nombre, inst.legajo);
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "instructorYaExiste"), inst.apellido, inst.nombre, inst.dni);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Instructor", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private void btnItemEditarI_Click(object sender, EventArgs e)
        {
            if (btnItemAgregarI.Visible == false)
                tabItemInstAgregar.Visible = false;
            else if (btnItemAgregarI.Visible == true)
                tabItemInstAgregar.Visible = true;
            if (btnItemConsultarI.Visible == false)
                tabItemInstConsultar.Visible = false;
            else if (btnItemConsultarI.Visible == true)
                tabItemInstConsultar.Visible = true;
            superTabControlDir.Visible = true;
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemInstConsultar.Visible = false;
            comboInstConsFiltro.Visible = false;
            dataGridProvCons.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            lblInstEditLegajo.Visible = false;
            lblInstEditNombre.Visible = false;
            lblInstEditApellido.Visible = false;
            lblInstEditDni.Visible = false;
            lblInstEditDireccion.Visible = false;
            lblInstEditCiudad.Visible = false;
            lblInstEditEmail.Visible = false;
            lblInstEditTelefono.Visible = false;
            txtInstEditLegajo.Visible = false;
            txtInstEditNombre.Visible = false;
            txtInstEditApellido.Visible = false;
            txtInstEditDni.Visible = false;
            txtInstEditDireccion.Visible = false;
            txtInstEditCiudad.Visible = false;
            txtInstEditEmail.Visible = false;
            txtInstEditTelefono.Visible = false;
            checkInstEditEstado.Visible = false;
            dataGridInstEdit.Visible = false;
            btnInstEditGuardar.Visible = false;
            tabItemInstAgregar.Visible = true;
            tabItemInstEditar.Visible = true;
            tabItemInstConsultar.Visible = true;
            superTabControlDir.SelectedTab = tabItemInstEditar;
            limpiarCampos();
        }

        private void btnInstEditBuscar_Click(object sender, EventArgs e)
        {
            Instructor inst = new Instructor();
            String filtro = (!String.IsNullOrEmpty(txtInstEditFiltro.Text)) ? txtInstEditFiltro.Text : null;
            if (radioInstEditDni.Checked && filtro != null)
            {
                inst = instructorBLL.obtenerInstructorPorDni(filtro);
            }
            else if (radioInstEditLegajo.Checked && filtro != null)
            {
                inst = instructorBLL.obtenerInstructorPorLegajo(Convert.ToInt32(filtro));
                inst.especialidadLista = instructorBLL.obtenerEspecialidadesPorLegajo(Convert.ToInt32(filtro));
            }
            if (inst != null)
            {
                txtInstEditLegajo.Text = inst.legajo.ToString();
                txtInstEditApellido.Text = inst.apellido;
                txtInstEditNombre.Text = inst.nombre;
                txtInstEditDni.Text = inst.dni;
                txtInstEditDireccion.Text = inst.domicilio;
                txtInstEditCiudad.Text = inst.ciudad;
                txtInstEditTelefono.Text = inst.telefono;
                txtInstEditEmail.Text = inst.email;
                checkInstEditEstado.Checked = (inst.estado == true) ? true : false;

                if (inst.especialidadLista != null && inst.especialidadLista.Count > 0)
                {
                    dataGridInstEdit.Visible = true;
                    for (int i = 0; i < inst.especialidadLista.Count; i++)
                    {

                        dataGridInstEdit.Rows.Add(1);
                        dataGridInstEdit.Rows[i].Cells["HInstEditTarifa"].Value = inst.especialidadLista[i].tarifa.ToString();
                        dataGridInstEdit.Rows[i].Cells["HInstEditExperiencia"].Value = inst.especialidadLista[i].experiencia;
                        dataGridInstEdit.Rows[i].Cells["HInstEditEspecialidad"].Value = inst.especialidadLista[i].descripcion;
                        dataGridInstEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    }
                }
                else
                {
                    dataGridInstEdit.Visible = false;
                    MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                mostrarCampos();
            }
            else
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mostrarCampos()
        {
            //edit instructor
            lblInstEditLegajo.Visible = true;
            lblInstEditNombre.Visible = true;
            lblInstEditApellido.Visible = true;
            lblInstEditDni.Visible = true;
            lblInstEditDireccion.Visible = true;
            lblInstEditCiudad.Visible = true;
            lblInstEditEmail.Visible = true;
            lblInstEditTelefono.Visible = true;
            txtInstEditLegajo.Visible = true;
            txtInstEditNombre.Visible = true;
            txtInstEditApellido.Visible = true;
            txtInstEditDni.Visible = true;
            txtInstEditDireccion.Visible = true;
            txtInstEditCiudad.Visible = true;
            txtInstEditEmail.Visible = true;
            txtInstEditTelefono.Visible = true;
            checkInstEditEstado.Visible = true;
            dataGridInstEdit.Visible = true;
            btnInstEditGuardar.Visible = true;
        }

        private void ocultarCampos()
        {
            //edit instructor
            lblInstEditLegajo.Visible = false;
            lblInstEditNombre.Visible = false;
            lblInstEditApellido.Visible = false;
            lblInstEditDni.Visible = false;
            lblInstEditDireccion.Visible = false;
            lblInstEditCiudad.Visible = false;
            lblInstEditEmail.Visible = false;
            lblInstEditTelefono.Visible = false;
            txtInstEditLegajo.Visible = false;
            txtInstEditNombre.Visible = false;
            txtInstEditApellido.Visible = false;
            txtInstEditDni.Visible = false;
            txtInstEditDireccion.Visible = false;
            txtInstEditCiudad.Visible = false;
            txtInstEditEmail.Visible = false;
            txtInstEditTelefono.Visible = false;
            checkInstEditEstado.Visible = false;
            dataGridInstEdit.Visible = false;
            btnInstEditGuardar.Visible = false;
        }

        private void btnInstEditGuardar_Click(object sender, EventArgs e)
        {
            String nombre = (!String.IsNullOrEmpty(txtInstEditNombre.Text.Trim())) ? txtInstEditNombre.Text.Trim() : null;
            String apellido = (!String.IsNullOrEmpty(txtInstEditApellido.Text.Trim())) ? txtInstEditApellido.Text.Trim() : null;
            String dni = (!String.IsNullOrEmpty(txtInstEditDni.Text.Trim())) ? txtInstEditDni.Text.Trim() : null;
            String email = (!String.IsNullOrEmpty(txtInstEditEmail.Text.Trim())) ? txtInstEditEmail.Text.Trim() : null;
            if (String.IsNullOrEmpty(nombre) || String.IsNullOrEmpty(apellido) || String.IsNullOrEmpty(dni) || String.IsNullOrEmpty(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "nombreApellidoDniEmailRequeridos"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esNumeroValido(dni))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoDni"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esEmailValido(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoEmail"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dataGridInstEdit.Rows.Count < 1)
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "especialidadesRequeridas"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Instructor inst = new Instructor();
                inst.legajo = Int32.Parse(txtInstEditLegajo.Text);
                inst.nombre = nombre;
                inst.apellido = apellido;
                inst.dni = dni;
                inst.email = email;
                inst.domicilio = (!String.IsNullOrEmpty(txtInstEditDireccion.Text.Trim())) ? txtInstEditDireccion.Text.Trim() : "";
                inst.ciudad = (!String.IsNullOrEmpty(txtInstEditCiudad.Text.Trim())) ? txtInstEditCiudad.Text.Trim() : "";
                inst.telefono = (!String.IsNullOrEmpty(txtInstEditTelefono.Text.Trim())) ? txtInstEditTelefono.Text.Trim() : "";
                inst.estado = checkInstEditEstado.Checked;
                List<Especialidad> listaEsp = new List<Especialidad>();

                foreach (DataGridViewRow Datarow in dataGridInstEdit.Rows)
                {
                    if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null && Datarow.Cells[2].Value != null)
                    {
                        Especialidad esp = new Especialidad();
                        esp.legajo = inst.legajo; 
                        esp.descripcion = Datarow.Cells[0].Value.ToString();
                        esp.codigo = instructorBLL.obtenerEspecialidades().Single(s => s.descripcion == esp.descripcion).codigo;
                        esp.experiencia = Int32.Parse(Datarow.Cells[1].Value.ToString());
                        esp.tarifa = Double.Parse(Datarow.Cells[2].Value.ToString());
                        listaEsp.Add(esp);
                    }
                }
                inst.especialidadLista = listaEsp;
                String mensaje = null;
                try
                {
                    if (!inst.estado)
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "confirmarBaja"), I18n.obtenerString("Mensaje", "instructor"));
                        DialogResult siNoBaja = MessageBox.Show(mensaje, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoBaja.Equals(DialogResult.Yes))
                        {
                            instructorBLL.actualizarInstructor(inst);
                        }
                    }
                    else
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            instructorBLL.actualizarInstructor(inst);
                        }
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), I18n.obtenerString("InicioDirector", "instructor"));
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Instructor", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private void radioInstEditLegajo_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            ocultarCampos();
        }

        private void radioInstEditDni_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            ocultarCampos();
        }




    }
}