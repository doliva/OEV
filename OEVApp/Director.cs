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
        IBLLProducto productoBLL = new BLLProducto();
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
            formatTimeInputs();
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
            comboACNombreE.DataSource = productoBLL.obtenerCursos().Select(r => r.nombre).ToList();
            comboAPNombreE.DataSource = productoBLL.obtenerProductos().Where(t => t.tipoProducto== EnumProducto.EVENTO.ToString()).Select(r => r.nombre).ToList();
            comboACDificultadC.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboAPDificultadC.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            comboBoxProvAgrTipo.DataSource = Enum.GetValues(typeof(EnumCategoria)).Cast<EnumCategoria>().ToList();

            checkedListAPDestinoA.Items.AddRange(obtenerDestinoDic().Values.ToArray());
            checkedListAPDestinoC.Items.AddRange(obtenerDestinoDic().Values.ToArray());
        }

        private void formatTimeInputs()
        {
            dTInputACHoraInicioA.CustomFormat = "HH:mm";
            dTInputACHoraFinA.CustomFormat = "HH:mm";
            dTInputACHoraInicioE.CustomFormat = "HH:mm";
            dTInputACHoraFinE.CustomFormat = "HH:mm";
            //dateAPInicioA.CustomFormat = "dd'/'MM'/'yyyy HH:mm";
            //dateAPFinA.CustomFormat = "dd'/'MM'/'yyyy HH:mm";
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
            btnItemActPaquete.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "paquete")).Append(" - ").Append(I18n.obtenerString("InicioDirector", "evento")).ToString();
            generarCursoEntrenamientoStrings();
            generarPaqueteEventoStrings();
        }

        private void generarCursoEntrenamientoStrings()
        {
            //Agregar 
            tabItemACAgregar.Text = I18n.obtenerString("InicioDirector", "agregar");
            //Editar
            tabItemACEditar.Text = I18n.obtenerString("InicioDirector", "editar");
            btnACBuscarC.Text = I18n.obtenerString("InicioDirector", "buscar");
            //Consultar
            tabItemACConsultar.Text = I18n.obtenerString("InicioDirector", "consultar");
            gridViewACC.Columns["HNombre"].HeaderText = I18n.obtenerString("InicioDirector", "nombre");
            gridViewACC.Columns["HActividad"].HeaderText = I18n.obtenerString("InicioDirector", "actividad");
            gridViewACC.Columns["HPrecio"].HeaderText = I18n.obtenerString("InicioDirector", "precio");
            gridViewACC.Columns["HDias"].HeaderText = I18n.obtenerString("InicioDirector", "dias");
            gridViewACC.Columns["HHoraInicio"].HeaderText = I18n.obtenerString("InicioDirector", "horaInicio");
            gridViewACC.Columns["HHoraFin"].HeaderText = I18n.obtenerString("InicioDirector", "horaFin");
            gridViewACC.Columns["HEstado"].HeaderText = I18n.obtenerString("InicioDirector", "estado");
            //Agregar + Editar + Consultar
            lblACCursoA.Text = lblACCursoE.Text = lblACCursoC.Text = groupACEntrenC.Text = I18n.obtenerString("InicioDirector", "cursoEntrenamiento");
            btnACNombreA.Text = I18n.obtenerString("InicioDirector", "nombre");
            lblACNombreE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACActvidadE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "actividad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACDescA.Text = lblACDescE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "descripcion")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACDificultadA.Text = lblACDificultadE.Text = lblACDificultadC.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "dificultad")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACPrecioA.Text = lblACPrecioE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "precio")).Append(" " + Constantes.MONEDA).Append(Constantes.DOS_PUNTOS).ToString();
            groupBoxACDiasA.Text = groupBoxACDiasE.Text =  new StringBuilder(I18n.obtenerString("InicioDirector", "dias")).Append(Constantes.DOS_PUNTOS).ToString();
            checkACLunA.Text = checkACLunE.Text = I18n.obtenerString("InicioDirector", "lunes");
            checkACMarA.Text = checkACMarE.Text = I18n.obtenerString("InicioDirector", "martes");
            checkACMieA.Text = checkACMieE.Text = I18n.obtenerString("InicioDirector", "miercoles");
            checkACJueA.Text = checkACJueE.Text = I18n.obtenerString("InicioDirector", "jueves");
            checkACVieA.Text = checkACVieE.Text = I18n.obtenerString("InicioDirector", "viernes");
            lblACHoraIniA.Text = lblACHoraIniE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "horaInicio")).Append(Constantes.DOS_PUNTOS).ToString();
            lblACHoraFinA.Text = lblACHoraFinE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "horaFin")).Append(Constantes.DOS_PUNTOS).ToString();
            groupACEntrenA.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "entrenamientoDe")).Append(Constantes.DOS_PUNTOS).ToString();
            radioACMontA.Text = chkACMontC.Text = I18n.obtenerString("InicioDirector", "montania");
            radioACRunA.Text = chkACRunC.Text = I18n.obtenerString("InicioDirector", "running");
            radioACTrekA.Text = chkACTrekC.Text = I18n.obtenerString("InicioDirector", "trekking");
            radioACBikeA.Text = chkACBikeC.Text = I18n.obtenerString("InicioDirector", "bike");
            radioACAuxA.Text = chkACAuxC.Text = I18n.obtenerString("InicioDirector", "auxilios");
            radioACGpsA.Text = chkACGpsC.Text = I18n.obtenerString("InicioDirector", "orientacion");
            btnACGuardarA.Text = btnACGuardarE.Text = I18n.obtenerString("InicioDirector", "guardar");

        }

        private void generarPaqueteEventoStrings()
        {
            //Agregar - editar - consultar
            groupAPModalidadA.Text = groupAPModalidadE.Text = I18n.obtenerString("InicioDirector", "modalidad");
            //lblAPNombreE.Text = new StringBuilder(Constantes.MANDATORY).Append(I18n.obtenerString("InicioDirector", "nombre")).Append(Constantes.DOS_PUNTOS).ToString();
            groupAPDestinoA.Text = lblAPDestinoE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "destino")).Append(Constantes.DOS_PUNTOS).ToString();
            lblAPPrecioA.Text = lblAPPrecioE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "precio")).Append(Constantes.DOS_PUNTOS).ToString();
            lblAPItinerarioA.Text = lblAPItinerarioE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "itinerario")).Append(Constantes.DOS_PUNTOS).ToString();
            lblAPDificultadA.Text = lblAPDificultadE.Text = lblAPDificultadC.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "dificultad")).Append(Constantes.DOS_PUNTOS).ToString();
            //lblAPFechaInicioA.Text = lblAPFechaInicioE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "fechaInicio")).Append(Constantes.DOS_PUNTOS).ToString();
            //lblAPFechaFinA.Text = lblAPFechaFinE.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "fechaFin")).Append(Constantes.DOS_PUNTOS).ToString();
            groupAPActividadA.Text = new StringBuilder(I18n.obtenerString("InicioDirector", "evento")).Append(Constantes.DOS_PUNTOS).ToString();
            btnAPGuardarA.Text = btnACGuardarE.Text = I18n.obtenerString("InicioDirector", "guardar");
            //consultar
            gridViewAPC.Columns["APHNombre"].HeaderText = I18n.obtenerString("InicioDirector", "nombre");
            gridViewAPC.Columns["APHPrecio"].HeaderText = I18n.obtenerString("InicioDirector", "precio");
            gridViewAPC.Columns["APHActividades"].HeaderText = I18n.obtenerString("InicioDirector", "actividades");
            //gridViewAPC.Columns["APHFechaInicio"].HeaderText = I18n.obtenerString("InicioDirector", "fechaInicio");
            //gridViewAPC.Columns["APHFechaFin"].HeaderText = I18n.obtenerString("InicioDirector", "fechaFin");
            btnAPBuscarC.Text = I18n.obtenerString("InicioDirector", "buscar");
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
            //agregar curso
            txtACNombreA.ResetText();
            comboACDificultadA.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            radioACMontA.Checked = true;
            richTxtACDescA.ResetText();
            doubleInACPrecioA.ResetText();
            checkACLunA.Checked = false;
            checkACMarA.Checked = false;
            checkACMieA.Checked = false;
            checkACJueA.Checked = false;
            checkACVieA.Checked = false;
            radioACMontA.Checked = true;
            txtACNombreA.ResetText();
            //agregar paquete/evento
            radioAPEventoA.Checked = true;
            comboAPDificultadA.DataSource = Enum.GetValues(typeof(EnumDificultad)).Cast<EnumDificultad>().ToList();
            txtAPNombreA.ResetText();
            doubleInAPPrecioA.ResetText();
            intInputAPDuracionA.ResetText();
            while (checkedListAPActividadA.CheckedIndices.Count > 0)
                checkedListAPActividadA.SetItemChecked(checkedListAPActividadA.CheckedIndices[0], false);
            //checkedListAPActividadA.ClearSelected();
            while (checkedListAPDestinoA.CheckedIndices.Count > 0)
                checkedListAPDestinoA.SetItemChecked(checkedListAPDestinoA.CheckedIndices[0], false);
            //checkedListAPDestinoA.ClearSelected();
            richTxtAPItinerarioA.ResetText();
            //editar curso
            richTxtACDescE.ResetText();
            doubleInACPrecioE.ResetText();
            checkACLunE.Checked = false;
            checkACMarE.Checked = false;
            checkACMieE.Checked = false;
            checkACJueE.Checked = false;
            checkACVieE.Checked = false;
            chkACEstadoE.Checked = false;
            //editar paquete/evento
            radioAPEventoE.Checked = true;
            txtAPActividadE.ResetText();
            txtAPDestinoE.ResetText();
            txtAPDificultadE.ResetText();
            doubleInAPPrecioE.ResetText();
            txtAPDuracionE.ResetText();
            chkAPEstadoE.Checked = false;
            richTxtAPItinerarioE.ResetText();

            //consultar curso
            gridViewACC.Rows.Clear();
            //consultar paquete/evento
            doubleAPPrecioDesdeC.ResetText();
            doubleAPPrecioHastaC.ResetText();
            while (checkedListAPActividadE.CheckedIndices.Count > 0)
                checkedListAPActividadE.SetItemChecked(checkedListAPActividadE.CheckedIndices[0], false);
            while (checkedListAPDestinoC.CheckedIndices.Count > 0)
                checkedListAPDestinoC.SetItemChecked(checkedListAPDestinoC.CheckedIndices[0], false);
            gridViewAPC.Rows.Clear();
            //agregar proveedor
            txtProvAgrRazon.ResetText();
            txtProvAgrCuit.ResetText();
            txtProvAgrDireccion.ResetText();
            txtProvAgrCiudad.ResetText();
            txtProvAgrEmail.ResetText();
            txtProvAgrTelefono.ResetText();
            doubleInProvAgrTarifa.ResetText();
            integerInProvAgrCap.ResetText();
            richTxtProvAgrServicios.ResetText();
            //editar proveedor
            txtProvEdiFiltro.ResetText();
            txtProvEdiRazon.ResetText();
            txtProvEdiCuit.ResetText();
            txtProvEdiDireccion.ResetText();
            txtProvEdiCiudad.ResetText();
            txtProvEdiEmail.ResetText();
            txtProvEdiTelefono.ResetText();
            doubleInProvEdiTarifa.ResetText();
            integerInProvEdiCap.ResetText();
            richTxtProvEdiServicios.ResetText();
            lblEdiId.ResetText();
            //consultar proveedor
            radioProvConsCuit.Checked = true;
            txtProvConsFiltro.ResetText();
            dataGridProvCons.Rows.Clear();
            //agregar instructor
            txtInstAgrApellido.ResetText();
            txtInstAgrNombre.ResetText();
            txtInstAgrDni.ResetText();
            txtInstAgrDireccion.ResetText();
            txtInstAgrCiudad.ResetText();
            txtInstAgrEmail.ResetText();
            txtInstAgrTelefono.ResetText();
            dataGridInstAgr.Rows.Clear();
            //editar instructor
            txtInstEditFiltro.ResetText();
            radioInstEditDni.Checked = true;
            txtInstEditLegajo.ResetText();
            txtInstEditNombre.ResetText();
            txtInstEditApellido.ResetText();
            txtInstEditDni.ResetText();
            txtInstEditEmail.ResetText();
            txtInstEditTelefono.ResetText();
            txtInstEditDireccion.ResetText();
            txtInstEditCiudad.ResetText();
            checkInstEditEstado.Checked = false;
            dataGridInstEdit.Rows.Clear();
            //consultar instructor
            txtInstConsFiltro.ResetText();
            radioInstConsDni.Checked = true;
            dataGridInstCons.Rows.Clear();
        }

        private void btnItemCalA_Click(object sender, EventArgs e)
        {
            if (btnItemEditarC.Visible == false)
            {
                tabItemACalAgregar.Visible = false;
                //tabItemACEditar.Visible = false;
                //tabItemACConsultar.Visible = false;
            }
            else if (btnItemConsultarC.Visible == true)
            {
                tabItemACalAgregar.Visible = true;
                //tabItemACEditar.Visible = true;
                //tabItemACConsultar.Visible = true;
            }
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemInstAgregar.Visible = false;
            tabItemInstEditar.Visible = false;
            tabItemInstConsultar.Visible = false;
            gridViewACC.Visible = false;
            superTabControlDir.Visible = true;
            superTabControlDir.SelectedTab = tabItemACalAgregar;
            limpiarCampos();
        }

        private void btnItemCalC_Click(object sender, EventArgs e)
        {

        }

        private void btnItemCalE_Click(object sender, EventArgs e)
        {

        }

        private void btnItemActCursoEnt_Click(object sender, EventArgs e)
        {
            if (btnItemActCursoEnt.Visible == false)
            {
                tabItemACAgregar.Visible = false;
                tabItemACEditar.Visible = false;
                tabItemACConsultar.Visible = false;
            }
            else if (btnItemActCursoEnt.Visible == true)
            {
                tabItemACAgregar.Visible = true;
                tabItemACEditar.Visible = true;
                tabItemACConsultar.Visible = true;
            }
            tabItemAPAgregar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemInstAgregar.Visible = false;
            tabItemInstEditar.Visible = false;
            tabItemInstConsultar.Visible = false;
            gridViewACC.Visible = false;
            superTabControlDir.Visible = true;
            superTabControlDir.SelectedTab = tabItemACAgregar;
            limpiarCampos();
        }

        private void btnItemActPaquete_Click(object sender, EventArgs e)
        {
            if (btnItemActPaquete.Visible == false)
            {
                tabItemAPAgregar.Visible = false;
                tabItemAPEditar.Visible = false;
                tabItemAPConsultar.Visible = false;
            }
            else if (btnItemActPaquete.Visible == true)
            {
                tabItemAPAgregar.Visible = true;
                tabItemAPEditar.Visible = true;
                tabItemAPConsultar.Visible = true;
            }
            tabItemACAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemInstAgregar.Visible = false;
            tabItemInstEditar.Visible = false;
            tabItemInstConsultar.Visible = false;
            gridViewAPC.Visible = false;
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
            txtProvConsFiltro.ResetText();
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
            tabItemInstAgregar.Visible = false;
            tabItemInstEditar.Visible = false;
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
            txtProvConsFiltro.ResetText();
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
            tabItemInstAgregar.Visible = false;
            tabItemInstEditar.Visible = false;
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
            txtProvConsFiltro.ResetText();
            txtProvConsFiltro.Visible = true;
            comboProvConsFiltro.Visible = false;
        }

        private void radioProvConsRazon_Click(object sender, EventArgs e)
        {
            txtProvConsFiltro.ResetText();
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
            txtInstConsFiltro.ResetText();
            txtInstConsFiltro.Visible = true;
            comboInstConsFiltro.Visible = false;
        }

        private void radioInstConsApellido_Click(object sender, EventArgs e)
        {
            txtInstConsFiltro.ResetText();
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
            radioInstEditLegajo.Checked = true;
            ocultarCampos();
        }

        private void radioInstEditDni_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            ocultarCampos();
        }

        private void btnACGuardarA_Click(object sender, EventArgs e)
        {
            DateTime horaInicio = dTInputACHoraInicioA.Value;
            DateTime horaFin = dTInputACHoraFinA.Value;
            if (!checkACLunA.Checked && !checkACMarA.Checked && !checkACMieA.Checked && !checkACJueA.Checked && !checkACVieA.Checked)
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "diasRequeridos"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esHorarioValido(horaInicio, horaFin))      
                MessageBox.Show(I18n.obtenerString("Mensaje", "horarioInvalido"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                Producto curso = new Producto();
                curso.estado = true;
                curso.destino = "";
                btnACNombreA_Click(sender, e);
                curso.nombre = txtACNombreA.Text;
                curso.precio = doubleInACPrecioA.Value;
                curso.duracion = 0;
                curso.descripcion = richTxtACDescA.Text.Trim();
                curso.tipoProducto = EnumProducto.CURSO.ToString();
                curso.dificultad = comboACDificultadA.SelectedItem.ToString();
                curso.horario = getHorarios(Constantes.AGREGAR);
                curso.actividades = getActividadesRadio(Constantes.AGREGAR);

                String mensaje = null;
                try
                {
                    Producto prodRes = productoBLL.obtenerProductoPorNombre(curso.nombre);
                    if (prodRes == null)
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            curso.idProducto = productoBLL.agregarProducto(curso);
                            mensaje = String.Format(I18n.obtenerString("Mensaje", "productoNuevo"), I18n.obtenerString("InicioDirector","cursoEntrenamiento"), curso.actividades[0], curso.nombre);
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "productoYaExiste"), I18n.obtenerString("InicioDirector", "cursoEntrenamiento"), curso.nombre);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Producto " + curso.tipoProducto, ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private List<String> getActividadesRadio(String accion)
        {
            List<String> actividades = new List<String>();
            if (accion == Constantes.AGREGAR)
            {
                if (radioACMontA.Checked)
                    actividades.Add(Constantes.MONT);
                else if (radioACRunA.Checked)
                    actividades.Add(Constantes.RUN);
                else if (radioACTrekA.Checked)
                    actividades.Add(Constantes.TREK);
                else if (radioACBikeA.Checked)
                    actividades.Add(Constantes.BIKE);
                else if (radioACAuxA.Checked)
                    actividades.Add(Constantes.AUX);
                else if (radioACGpsA.Checked)
                    actividades.Add(Constantes.GPS);
            }
            if (accion == Constantes.CONSULTAR)
            {
                if (chkACMontC.Checked)
                    actividades.Add(Constantes.MONT);
                if (chkACRunC.Checked)
                    actividades.Add(Constantes.RUN);
                if (chkACTrekC.Checked)
                    actividades.Add(Constantes.TREK);
                if (chkACBikeC.Checked)
                    actividades.Add(Constantes.BIKE);
                if (chkACAuxC.Checked)
                    actividades.Add(Constantes.AUX);
                if (chkACGpsC.Checked)
                    actividades.Add(Constantes.GPS);
            }
            return actividades;
        }

        private static Dictionary<String, String> obtenerActividadesDic()
        {
            Dictionary<String, String> actividadesDic = new Dictionary<string, string>();
            actividadesDic.Add("MONT", Constantes.MONT);
            actividadesDic.Add("RUN", Constantes.RUN);
            actividadesDic.Add("TREK", Constantes.TREK);
            actividadesDic.Add("BIKE", Constantes.BIKE);
            actividadesDic.Add("AUX", Constantes.AUX);
            actividadesDic.Add("GPS", Constantes.GPS);
            actividadesDic.Add("CAB", Constantes.CAB);
            actividadesDic.Add("CAN", Constantes.CAN);
            actividadesDic.Add("CIC", Constantes.CIC);
            actividadesDic.Add("HIELO", Constantes.HIELO);
            actividadesDic.Add("ROCA", Constantes.ROCA);
            actividadesDic.Add("KAY", Constantes.KAY);
            return actividadesDic;
        }

        private String obtenerActividadesString()
        {
            StringBuilder actividad = new StringBuilder();
            for (int i = 0; i < checkedListAPActividadA.CheckedItems.Count; i++)
            {
                actividad.Append(obtenerActividadesDic().FirstOrDefault(x => x.Value == checkedListAPActividadA.CheckedItems[i].ToString().ToUpper()).Key.ToString() + "_");
            }
            return actividad.ToString();
        }
        private Horario getHorarios(String accion)
        {
            Horario horario = new Horario();
            StringBuilder dias = new StringBuilder();
            if (accion == Constantes.AGREGAR)
            {
                horario.horaInicio = dTInputACHoraInicioA.Value;
                horario.horaFin = dTInputACHoraFinA.Value;
                if (checkACLunA.Checked)
                    dias.Append("L");
                if (checkACMarA.Checked)
                    dias.Append("M");
                if (checkACMieA.Checked)
                    dias.Append("X");
                if (checkACJueA.Checked)
                    dias.Append("J");
                if (checkACVieA.Checked)
                    dias.Append("V");
            }
            if (accion == Constantes.EDITAR)
            {
                horario.horaInicio = dTInputACHoraInicioE.Value;
                horario.horaFin = dTInputACHoraFinE.Value;
                if (checkACLunE.Checked)
                    dias.Append("L");
                if (checkACMarE.Checked)
                    dias.Append("M");
                if (checkACMieE.Checked)
                    dias.Append("X");
                if (checkACJueE.Checked)
                    dias.Append("J");
                if (checkACVieE.Checked)
                    dias.Append("V");
            }
            
            horario.dia = dias.ToString();
            return horario;
        }

        private void btnACNombreA_Click(object sender, EventArgs e)
        {
            String dificultad = comboACDificultadA.SelectedItem.ToString().Substring(0, 3);
            String actividad = null;
            StringBuilder dias = new StringBuilder();
            String horario = dTInputACHoraInicioA.Value.ToString("HHmm") + ":" + dTInputACHoraFinA.Value.ToString("HHmm");
            
            if (radioACMontA.Checked)
                actividad = "MONT";
            else if (radioACRunA.Checked)
                actividad = "RUNN";
            else if (radioACTrekA.Checked)
                actividad = "TREK";
            else if (radioACBikeA.Checked)
                actividad = "BIKE";
            else if (radioACAuxA.Checked)
                actividad = "RCP";
            else if (radioACGpsA.Checked)
                actividad = "GPS";

            if (checkACLunA.Checked)
                dias.Append("L");
            if (checkACMarA.Checked)
                dias.Append("M");
            if (checkACMieA.Checked)
                dias.Append("X");
            if (checkACJueA.Checked)
                dias.Append("J");
            if (checkACVieA.Checked)
                dias.Append("V");
            //TREK_MED_LXV_0800:1030
            txtACNombreA.Text = new StringBuilder(actividad)
                                .Append(Constantes.SEPARADOR)
                                .Append(dificultad)
                                .Append(Constantes.SEPARADOR)
                                .Append((!String.IsNullOrEmpty(dias.ToString()) ? dias.ToString() : " "))
                                .Append(Constantes.SEPARADOR)
                                .Append(horario).ToString();
            
        }

        private void comboACNombreE_SelectedChange(object sender, EventArgs e)
        {
            if (comboACNombreE.SelectedItem != null)
            {
                Producto curso = productoBLL.obtenerCursos().Where(r => r.nombre == comboACNombreE.SelectedItem.ToString()).Single();
                lblACIdE.Text = curso.idProducto.ToString();
                txtACActividadE.Text = curso.actividades.Aggregate((a, b) => a + ',' + b);
                txtACDificultadE.Text = curso.dificultad;
                if (curso.horario.dia.Contains("L"))
                    checkACLunE.Checked = true;
                if (curso.horario.dia.Contains("M"))
                    checkACMarE.Checked = true;
                if (curso.horario.dia.Contains("X"))
                    checkACMieE.Checked = true;
                if (curso.horario.dia.Contains("J"))
                    checkACJueE.Checked = true;
                if (curso.horario.dia.Contains("V"))
                    checkACVieE.Checked = true;

                dTInputACHoraInicioE.Value = curso.horario.horaInicio;
                dTInputACHoraFinE.Value = curso.horario.horaFin;
                doubleInACPrecioE.Text = curso.precio.ToString();
                chkACEstadoE.Checked = curso.estado;
                richTxtACDescE.Text = curso.descripcion;
            }
            else
            {
                comboACNombreE.Text = "No hay cursos registrados";
            }
        }

        private void tabItemACEditar_Click(object sender, EventArgs e)
        {
            comboACNombreE_SelectedChange(sender, e);
        }

        private void btnACGuardarE_Click(object sender, EventArgs e)
        {
            Producto curso = new Producto();
            curso.idProducto = Int32.Parse(lblACIdE.Text);
            curso.nombre = comboACNombreE.SelectedItem.ToString();
            curso.actividades = txtACActividadE.Text.Split(',').ToList();
            curso.dificultad = txtACDificultadE.Text.Trim();
            curso.tipoProducto = EnumProducto.CURSO.ToString();
            curso.horario = getHorarios(Constantes.EDITAR);
            curso.precio = doubleInACPrecioE.Value;
            curso.estado = chkACEstadoE.Checked;
            curso.descripcion = (!String.IsNullOrEmpty(richTxtACDescE.Text.Trim())) ? richTxtACDescE.Text.Trim() : "";
            curso.destino = "";
            curso.duracion = 0;

            String mensaje = null;
            try
            {
                if (!curso.estado)
                {
                    mensaje = String.Format(I18n.obtenerString("Mensaje", "confirmarBaja"), I18n.obtenerString("Mensaje", "cursoEntrenamiento"));
                    DialogResult siNoBaja = MessageBox.Show(mensaje, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (siNoBaja.Equals(DialogResult.Yes))
                    {
                        productoBLL.actualizarProducto(curso);
                    }
                }
                else
                {
                    DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (siNoRes.Equals(DialogResult.Yes))
                    {
                        productoBLL.actualizarProducto(curso);
                    }
                    mensaje = String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), I18n.obtenerString("InicioDirector", "cursoEntrenamiento"));
                    MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_UPD + " Producto " + curso.tipoProducto, ex.ToString());
                BitacoraBLL.registrarBitacora(bitacora);
            }
        }

        private void btnACBuscarC_Click(object sender, EventArgs e)
        {
            String filtroDificultad = comboACDificultadC.SelectedItem.ToString();
            Double filtroPrecioDesde = doubleACPrecioDesdeC.Value;
            Double filtroPrecioHasta = doubleACPrecioHastaC.Value;
            List<String> filtroActividades = getActividadesRadio(Constantes.CONSULTAR);

            List<Producto> listaProductos = productoBLL.obtenerCursos();
            List<Producto> cursosFiltrados = new List<Producto>();

            foreach (Producto item in listaProductos)
            {
                if (item.dificultad == filtroDificultad)
                {
                    if ((filtroPrecioDesde <= item.precio) && (item.precio <= filtroPrecioHasta))
                    {
                        foreach (String act in filtroActividades)
                        {
                            if (item.actividades[0] == act)
                            {
                                    cursosFiltrados.Add(item);
                            }
                        }
                    }
                }
            }

            gridViewACC.Rows.Clear();
            if (cursosFiltrados != null && cursosFiltrados.Count > 0)
            {
                gridViewACC.Visible = true;
                for (int i = 0; i < cursosFiltrados.Count; i++)
                {
                    gridViewACC.Rows.Add(1);
                    gridViewACC.Rows[i].Cells["HNombre"].Value = cursosFiltrados[i].nombre;
                    gridViewACC.Rows[i].Cells["HActividad"].Value = cursosFiltrados[i].actividades[0];
                    gridViewACC.Rows[i].Cells["HPrecio"].Value = Constantes.MONEDA + cursosFiltrados[i].precio.ToString();
                    gridViewACC.Rows[i].Cells["HDias"].Value = cursosFiltrados[i].horario.dia;
                    gridViewACC.Rows[i].Cells["HHoraInicio"].Value = cursosFiltrados[i].horario.horaInicio.ToString("HH:mm");
                    gridViewACC.Rows[i].Cells["HHoraFin"].Value = cursosFiltrados[i].horario.horaFin.ToString("HH:mm");
                    gridViewACC.Rows[i].Cells["HEstado"].Value = cursosFiltrados[i].estado;
                    gridViewACC.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
            }
            else
            {
                gridViewACC.Visible = false;
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAPGuardarA_Click(object sender, EventArgs e)
        {
            //DateTime fechaInicio = dateAPInicioA.Value;
            //DateTime fechaFin = dateAPFinA.Value;
            if (checkedListAPActividadA.CheckedItems.Count == 0)
                MessageBox.Show(I18n.obtenerString("Mensaje", "actividadRequerida"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //else if (!Validacion.esFechaValida(fechaInicio, fechaFin))
            //    MessageBox.Show(I18n.obtenerString("Mensaje", "fechaInvalida"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (checkedListAPDestinoA.CheckedItems.Count == 0)
                MessageBox.Show(I18n.obtenerString("Mensaje", "destinoRequerido"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                Producto prod = new Producto();

                prod.estado = true;
                prod.destino = obtenerDestinosString(checkedListAPDestinoA).Remove(obtenerDestinosString(checkedListAPDestinoA).Length - 1, 1);
                btnAPNombreA_Click(sender, e);
                prod.nombre = txtAPNombreA.Text;
                prod.duracion = intInputAPDuracionA.Value;
                prod.precio = doubleInAPPrecioA.Value;
                prod.descripcion = richTxtAPItinerarioA.Text;
                prod.tipoProducto = (radioAPEventoA.Checked ? EnumProducto.EVENTO.ToString() : EnumProducto.PAQUETE.ToString());
                prod.dificultad = comboAPDificultadA.SelectedItem.ToString();
                prod.actividades = checkedListAPActividadA.CheckedItems.Cast<String>().ToList();

                String mensaje = null;
                try
                {
                    Producto prodRes = productoBLL.obtenerProductoPorNombre(prod.nombre);
                    if (prodRes == null)
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            prod.idProducto = productoBLL.agregarProducto(prod);
                            mensaje = String.Format(I18n.obtenerString("Mensaje", "productoNuevo"), I18n.obtenerString("InicioDirector", prod.tipoProducto.ToLower()), String.Join(",",prod.actividades), prod.nombre);
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "productoYaExiste"), I18n.obtenerString("InicioDirector", prod.tipoProducto.ToLower()), prod.nombre);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Producto " + prod.tipoProducto, ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private String obtenerDestinosString(CheckedListBox checkedList)
        {
            StringBuilder destinos = new StringBuilder();
            for (int i = 0; i < checkedList.CheckedItems.Count; i++)
            {
                destinos.Append(obtenerDestinoDic().FirstOrDefault(x => x.Value == checkedList.CheckedItems[i].ToString()).Key.ToString() + ",");
            }
            return destinos.ToString();
        }

        private Dictionary<String, String> obtenerDestinoDic()
        {
            Dictionary<String, String> destino = new Dictionary<String, String>();
            destino.Add("BA_BAL", "Balcarce, Buenos Aires");
            destino.Add("BA_CAÑ", "Cañuelas, Buenos Aires");
            destino.Add("BA_OTA", "Otamendi, Buenos Aires");
            destino.Add("BA_VEN", "Sierra de la Ventana, Buenos Aires");
            destino.Add("BA_TIG", "Tigre, Buenos Aires");

            destino.Add("CTC_AMB", "Ambato, Catamarca");
            destino.Add("CTC_BEL", "Belén, Catamarca");
            destino.Add("CTC_FIA", "Fiambalá, Catamarca");

            destino.Add("COR_CAP", "Capilla del Monte, Córdoba");
            destino.Add("COR_GIG", "Los Gigantes, Córdoba");
            destino.Add("COR_COC", "Los Cocos, Córdoba");
            destino.Add("COR_BEL", "Villa General Belgrano, Córdoba");

            destino.Add("FMA_MON", "Monte Lindo, Formosa");

            destino.Add("JUJ_HUM", "Humahuaca, Jujuy");
            destino.Add("JUJ_TIL", "Tilcara, Jujuy");

            destino.Add("LRJ_TAL", "Talampaya, La Rioja");

            destino.Add("MDZ_ACO", "Aconcagua, Mendoza");
            destino.Add("MDZ_PEN", "Cerro Penitentes, Mendoza");
            destino.Add("MDZ_PER", "Ciudad Perdida, Mendoza");
            destino.Add("MDZ_SOS", "El Sosneado, Mendoza");
            destino.Add("MDZ_INC", "Puente del Inca, Mendoza");
            destino.Add("MDZ_MAL", "Malargüe, Mendoza");
            destino.Add("MDZ_RAF", "San Rafael, Mendoza");
            destino.Add("MDZ_TUN", "Tunuyán, Mendoza");

            destino.Add("NQN_SMA", "San Martín de los Andes, Neuquén");

            destino.Add("RNG_BAR", "Bariloche, Río Negro");
            destino.Add("RNG_BOL", "El Bolsón, Río Negro");

            destino.Add("SLA_COB", "San Antonio de los Cobres, Salta");
            destino.Add("SLA_CAL", "Valles Calchaquíes, Salta");

            destino.Add("SNJ_CAL", "Calingasta, San Juan");

            destino.Add("STC_CHA", "El Chaltén, Santa Cruz");

            return destino;
        }

        private void btnAPNombreA_Click(object sender, EventArgs e)
        {
            String dificultad = comboAPDificultadA.SelectedItem.ToString().Substring(0, 3);
            String actividad = String.IsNullOrEmpty(obtenerActividadesString()) ? "" : obtenerActividadesString().Remove(obtenerActividadesString().Length - 1, 1);
            String destinos = String.IsNullOrEmpty(obtenerDestinosString(checkedListAPDestinoA)) ? "" : obtenerDestinosString(checkedListAPDestinoA).Remove(obtenerDestinosString(checkedListAPDestinoA).Length - 1, 1);
            //String fechas = dateAPInicioA.Value.ToString("ddMMyyyy") + ":" + dateAPFinA.Value.ToString("ddMMyyyy");

            //CAB_CIC#FAC#BA_TIG#2
            txtAPNombreA.Text = new StringBuilder(actividad.ToString()).Append("#")
                                .Append(dificultad).Append("#")
                                .Append(destinos.ToString())
                                .Append("#").Append(intInputAPDuracionA.Value).ToString();
            
        }

        private void comboAPNombreE_SelectedChange(object sender, EventArgs e)
        {
            if (comboAPNombreE.SelectedItem != null)
            {
                Producto prod = productoBLL.obtenerProductos().Where(r => r.nombre == comboAPNombreE.SelectedItem.ToString()).Single();
                lblAPIdE.Text = prod.idProducto.ToString();
                txtAPActividadE.Text = prod.actividades.Aggregate((a, b) => a + ',' + b);
                String[] splitDestino = prod.destino.Split(',');
                String destinoStr = null;
                for (int i = 0; i < splitDestino.Length; i++)
                {
                    destinoStr += obtenerDestinoDic()[splitDestino[i]] + "\r\n";
                }
                txtAPDestinoE.Text = destinoStr.Remove(destinoStr.Length - 2, 2);
                txtAPDificultadE.Text = prod.dificultad;
                //dateAPInicioE.CustomFormat =  "dd/MM/yyyy HH:mm";
                //dateAPInicioE.Value = prod.fecha.fechaInicio;
                //dateAPFinE.CustomFormat = "dd/MM/yyyy HH:mm";
                //dateAPFinE.Value = prod.fecha.fechaFin;
                doubleInAPPrecioE.Text = prod.precio.ToString();
                txtAPDuracionE.Text = prod.duracion.ToString();
                chkAPEstadoE.Checked = prod.estado;
                richTxtAPItinerarioE.Text = prod.descripcion;
            }
            else
            {
                comboAPNombreE.Text = "No hay cursos registrados";
            }
        }

        private void btnAPGuardarE_Click(object sender, EventArgs e)
        {
            Producto prod = productoBLL.obtenerProductoPorNombre(comboAPNombreE.SelectedItem.ToString());
            if (prod.idProducto == Int32.Parse(lblAPIdE.Text))
            {
                prod.precio = doubleInAPPrecioE.Value;
                prod.estado = chkAPEstadoE.Checked;
                prod.descripcion = (!String.IsNullOrEmpty(richTxtAPItinerarioE.Text.Trim())) ? richTxtAPItinerarioE.Text.Trim() : "";

                String mensaje = null;
                try
                {
                    if (!prod.estado)
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "confirmarBaja"), I18n.obtenerString("InicioDirector", prod.tipoProducto.ToLower()));
                        DialogResult siNoBaja = MessageBox.Show(mensaje, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoBaja.Equals(DialogResult.Yes))
                        {
                            productoBLL.actualizarProducto(prod);
                        }
                    }
                    else
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            productoBLL.actualizarProducto(prod);
                        }
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), I18n.obtenerString("InicioDirector", prod.tipoProducto.ToLower()));
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_UPD + " Producto " + prod.tipoProducto, ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        private void radioAPEventoE_Click(object sender, EventArgs e)
        {
            comboAPNombreE.DataSource = productoBLL.obtenerProductos().Where(t => t.tipoProducto == EnumProducto.EVENTO.ToString()).Select(r => r.nombre).ToList();
        }

        private void radioAPPaqueteE_Click(object sender, EventArgs e)
        {
            comboAPNombreE.DataSource = productoBLL.obtenerProductos().Where(t => t.tipoProducto == EnumProducto.PAQUETE.ToString()).Select(r => r.nombre).ToList();
        }

        private void tabItemAPEditar_Click(object sender, EventArgs e)
        {
            comboAPNombreE_SelectedChange(sender, e);
        }

        private void btnAPBuscarC_Click(object sender, EventArgs e)
        {
            
            String filtroDificultad = comboAPDificultadC.SelectedItem.ToString();
            Double filtroPrecioDesde = doubleAPPrecioDesdeC.Value;
            Double filtroPrecioHasta = doubleAPPrecioHastaC.Value;
            List<String> filtroActividades = checkedListAPActividadE.CheckedItems.Cast<String>().ToList();
            String destino = obtenerDestinosString(checkedListAPDestinoC).Remove(obtenerDestinosString(checkedListAPDestinoC).Length - 1, 1);

            List<Producto> listaProductos = productoBLL.obtenerProductos();
            List<Producto> prodsFiltrados = new List<Producto>();

            foreach (Producto item in listaProductos)
            {
                if (item.dificultad == filtroDificultad && destino.Contains(item.destino))
                {
                    if ((filtroPrecioDesde <= item.precio) && (item.precio <= filtroPrecioHasta))
                    {
                        foreach (String act in filtroActividades)
                        {
                            if (item.actividades.Contains(act))
                            {
                                prodsFiltrados.Add(item);
                            }
                        }
                    }
                }
            }

            gridViewAPC.Rows.Clear();
            prodsFiltrados = prodsFiltrados.GroupBy(x => x.nombre).Select(y => y.First()).ToList();
            if (prodsFiltrados != null && prodsFiltrados.Count > 0)
            {
                gridViewAPC.Visible = true;
                for (int i = 0; i < prodsFiltrados.Count; i++)
                {
                    gridViewAPC.Rows.Add(1);
                    gridViewAPC.Rows[i].Cells["APHTipoProducto"].Value = prodsFiltrados[i].tipoProducto;
                    gridViewAPC.Rows[i].Cells["APHNombre"].Value = prodsFiltrados[i].nombre;
                    String[] splitDestino = prodsFiltrados[i].destino.Split(',');
                    String destinoStr = null;
                    for (int j = 0; j < splitDestino.Length; j++)
                    {
                        destinoStr += obtenerDestinoDic()[splitDestino[j]] + "\r\n";
                    }
                    gridViewAPC.Rows[i].Cells["APHDestino"].Value = destinoStr;
                    gridViewAPC.Rows[i].Cells["APHPrecio"].Value = Constantes.MONEDA + prodsFiltrados[i].precio.ToString();
                    gridViewAPC.Rows[i].Cells["APHDuracion"].Value = prodsFiltrados[i].duracion;
                    gridViewAPC.Rows[i].Cells["APHActividades"].Value = prodsFiltrados[i].actividades.Aggregate((a, b) => a + ',' + b);
                    gridViewAPC.Rows[i].Cells["APHEstado"].Value = prodsFiltrados[i].estado;
                    gridViewAPC.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
            }
            else
            {
                gridViewAPC.Visible = false;
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }

}