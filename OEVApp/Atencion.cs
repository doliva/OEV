using Base;
using Base.BLL;
using BLL;
using BLL.IBLL;
using Entities;
using OEVApp.i18n;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace OEVApp
{
    public partial class Atencion : DevComponents.DotNetBar.Metro.MetroForm
    {
        Usuario usuarioLogueado = null;
        Rol rolUsrLogueado = null;
        IUsuarioBLL usuarioBLL = new UsuarioBLL();
        IBLLCliente clienteBLL = new BLLCliente();
        IRolBLL rolBLL = new RolBLL();
        IBLLProducto productoBLL = new BLLProducto();
        IBLLCalendario calendarioBLL = new BLLCalendario();
        IBLLAlojamiento alojamientoBLL = new BLLAlojamiento();
        IBLLTraslado trasladoBLL = new BLLTraslado();
        IBLLInstructor instructorBLL = new BLLInstructor();
        IBLLReserva reservaBLL = new BLLReserva();
        String msjInfo = null;
        String msjConfirmar = null;
        String msjError = null;
        String campoRequerido = null;

        public Atencion(Usuario usuario, string idioma, List<Familia> familiasPermitidas, List<Patente> patentesPermitidas)
        {
            InitializeComponent();
            lblUsuarioLogueado.Text = usuario.id + " - " + usuario.nombre + " " + usuario.apellido;
            this.usuarioLogueado = usuario;
            rolUsrLogueado = rolBLL.obtenerRolPorId(Convert.ToInt32(usuarioBLL.obtnerRolPorIdUsuario(usuario.id)));
        }

        public Atencion()
        {
            // TODO: Complete member initialization
        }

        private void btnItemAgregarCliente_Click(object sender, EventArgs e)
        {
            if (btnItemAgregarCliente.Visible == false)
            {
                tabItemCliAgregar.Visible = false;
                tabItemCliConsultar.Visible = false;
                tabItemCliEditar.Visible = false;
            }
            else if (btnItemAgregarCliente.Visible == true)
            {
                tabItemCliAgregar.Visible = true;
                tabItemCliConsultar.Visible = true;
                tabItemCliEditar.Visible = true;
            }
            tabItemEvPaqReservar.Visible = false;
            tabItemACalConsultar.Visible = false;
            tabItemACalEditar.Visible = false;
            tabItemACAgregar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemACConsultar.Visible = false;
            //tabItemEvPaqResEditar.Visible = false;
            //tabItemEvPaqResConsultar.Visible = false;
            superTabControlAtencion.Visible = true;
            superTabControlAtencion.SelectedTab = tabItemCliAgregar;
            limpiarCampos();
        }

        private void btnItemConsultarCliente_Click(object sender, EventArgs e)
        {
            if (btnItemConsultarCliente.Visible == false)
            {
                tabItemCliAgregar.Visible = false;
                tabItemCliConsultar.Visible = false;
                tabItemCliEditar.Visible = false;
            }
            else if (btnItemConsultarCliente.Visible == true)
            {
                tabItemCliAgregar.Visible = true;
                tabItemCliConsultar.Visible = true;
                tabItemCliEditar.Visible = true;
            }
            tabItemEvPaqReservar.Visible = false;
            tabItemACalConsultar.Visible = false;
            tabItemACalEditar.Visible = false;
            tabItemACAgregar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemACConsultar.Visible = false;
            //tabItemEvPaqResEditar.Visible = false;
            //tabItemEvPaqResConsultar.Visible = false;
            superTabControlAtencion.Visible = true;
            superTabControlAtencion.SelectedTab = tabItemCliConsultar;
            limpiarCampos();
        }

        private void btnItemEditarCliente_Click(object sender, EventArgs e)
        {
            if (btnItemEditarCliente.Visible == false)
            {
                tabItemCliAgregar.Visible = false;
                tabItemCliConsultar.Visible = false;
                tabItemCliEditar.Visible = false;
            }
            else if (btnItemEditarCliente.Visible == true)
            {
                tabItemCliAgregar.Visible = true;
                tabItemCliConsultar.Visible = true;
                tabItemCliEditar.Visible = true;
            }
            tabItemEvPaqReservar.Visible = false;
            tabItemACalConsultar.Visible = false;
            tabItemACalEditar.Visible = false;
            tabItemACAgregar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemACConsultar.Visible = false;
            //tabItemEvPaqResEditar.Visible = false;
            //tabItemEvPaqResConsultar.Visible = false;
            superTabControlAtencion.Visible = true;
            superTabControlAtencion.SelectedTab = tabItemCliEditar;
            limpiarCampos();
        }

        private void btnItemReservar_Click(object sender, EventArgs e)
        {
            if (btnItemReservas.Visible == false)
            {
                tabItemEvPaqReservar.Visible = false;
                //tabItemEvPaqResEditar.Visible = false;
                //tabItemEvPaqResConsultar.Visible = false;
            }
            else if (btnItemReservas.Visible == true)
            {
                tabItemEvPaqReservar.Visible = true;
                //tabItemEvPaqResEditar.Visible = true;
                //tabItemEvPaqResConsultar.Visible = true;
            }
            tabItemCliAgregar.Visible = false;
            tabItemCliConsultar.Visible = false;
            tabItemCliEditar.Visible = false;
            tabItemACalConsultar.Visible = false;
            tabItemACalEditar.Visible = false;
            tabItemACAgregar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemACConsultar.Visible = false;
            superTabControlAtencion.Visible = true;
            superTabControlAtencion.SelectedTab = tabItemEvPaqReservar;
            limpiarCampos();
        }

        private void btnItemResEditar_Click(object sender, EventArgs e)
        {

            tabItemEvPaqReservar.Visible = true;
            tabItemACalConsultar.Visible = false;
            tabItemACalEditar.Visible = false;
            tabItemACAgregar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemACConsultar.Visible = false;
        }

        private void btnItemResCancelar_Click(object sender, EventArgs e)
        {

            tabItemEvPaqReservar.Visible = true;
            tabItemACalConsultar.Visible = false;
            tabItemACalEditar.Visible = false;
            tabItemACAgregar.Visible = false;
            tabItemAPAgregar.Visible = false;
            tabItemACEditar.Visible = false;
            tabItemAPEditar.Visible = false;
            tabItemACConsultar.Visible = false;
            tabItemAPConsultar.Visible = false;
            tabItemProvAgregar.Visible = false;
            tabItemProvEditar.Visible = false;
            tabItemProvConsultar.Visible = false;
            tabItemACConsultar.Visible = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
            Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.LOGOUT, "");
            BitacoraBLL.registrarBitacora(bitacora);
        }

        private void limpiarCampos()
        {
            //agregar cliente
            txtCliANombre.ResetText();
            txtCliAApellido.ResetText();
            txtCliACiudad.ResetText();
            txtCliADireccion.ResetText();
            txtCliAPasaporte.ResetText();
            txtCliADni.ResetText();
            txtCliAEmail.ResetText();
            txtCliATelefono.ResetText();
            txtCliACelular.ResetText();
            //consultar cliente
            dataGridCliC.Visible = false;
            txtCliCFiltro.ResetText();
            //editar cliente
            lblCliEId.Visible = false;
            radioCliEDni.Checked = true;
            txtCliEFiltro.ResetText();
            txtCliEPasaporte.ResetText();
            txtCliEDni.ResetText();
            txtCliENombre.ResetText();
            txtCliEApellido.ResetText();
            txtCliECiudad.ResetText();
            txtCliEDireccion.ResetText();
            txtCliEEmail.ResetText();
            txtCliETelefono.ResetText();
            txtCliECelular.ResetText();
            //reservar evento/paquete
            gridPage2EqPaqCliente.Rows.Clear();
        }

        private void mostrarCampos()
        {
            //editar cliente
            lblCliENombre.Visible = true;
            lblCliEApellido.Visible = true;
            lblCliEDni.Visible = true;
            lblCliEPasaporte.Visible = true;
            lblCliEEmail.Visible = true;
            lblCliETelefono.Visible = true;
            lblCliECelular.Visible = true;
            lblCliEDireccion.Visible = true;
            lblCliECiudad.Visible = true;
            checkCliEEstado.Visible = true;
            txtCliENombre.Visible = true;
            txtCliEApellido.Visible = true;
            txtCliEDni.Visible = true;
            txtCliEPasaporte.Visible = true;
            txtCliEEmail.Visible = true;
            txtCliETelefono.Visible = true;
            txtCliECelular.Visible = true;
            txtCliEDireccion.Visible = true;
            txtCliECiudad.Visible = true;
        }

        //Agregar un nuevo cliente
        private void btnCliAGuardar_Click(object sender, EventArgs e)
        {
            String nombre = (!String.IsNullOrEmpty(txtCliANombre.Text.Trim())) ? txtCliANombre.Text.Trim() : "";
            String apellido = (!String.IsNullOrEmpty(txtCliAApellido.Text.Trim())) ? txtCliAApellido.Text.Trim() : "";
            String dni = (!String.IsNullOrEmpty(txtCliADni.Text.Trim())) ? txtCliADni.Text.Trim() : "";
            String pasaporte = (!String.IsNullOrEmpty(txtCliAPasaporte.Text.Trim())) ? txtCliAPasaporte.Text.Trim() : "";
            String email = (!String.IsNullOrEmpty(txtCliAEmail.Text.Trim())) ? txtCliAEmail.Text.Trim() : "";
            if (String.IsNullOrEmpty(nombre))
            {
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "Nombre");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if(String.IsNullOrEmpty(apellido)){
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "Apellido");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if (String.IsNullOrEmpty(dni) && String.IsNullOrEmpty(pasaporte)){
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "DNI o Pasaporte");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if(String.IsNullOrEmpty(email)){
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "Email");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esNumeroValido(dni))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoDni"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Validacion.esEmailValido(email))
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "formatoEmail"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            else
            {
                Cliente cliente = new Cliente();
                cliente.nombre = nombre;
                cliente.apellido = apellido;
                cliente.dni = dni;
                cliente.email = email;
                cliente.domicilio = (!String.IsNullOrEmpty(txtCliADireccion.Text.Trim())) ? txtCliADireccion.Text.Trim() : "";
                cliente.ciudad = (!String.IsNullOrEmpty(txtCliACiudad.Text.Trim())) ? txtCliACiudad.Text.Trim() : "";
                cliente.telefono = (!String.IsNullOrEmpty(txtCliATelefono.Text.Trim())) ? txtCliATelefono.Text.Trim() : "";
                cliente.celular = (!String.IsNullOrEmpty(txtCliACelular.Text.Trim())) ? txtCliACelular.Text.Trim() : "";
                cliente.pasaporte = (!String.IsNullOrEmpty(txtCliAPasaporte.Text.Trim())) ? txtCliAPasaporte.Text.Trim() : "";
                cliente.estado = true;

                String mensaje = null;
                try
                {
                    Cliente cliRes = clienteBLL.obtenerClientePorDniPasaporte(cliente.dni, cliente.pasaporte);
                    if (cliRes == null)
                    {

                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            cliente.idCliente = clienteBLL.agregarCliente(cliente);
                            mensaje = String.Format(I18n.obtenerString("Mensaje", "clienteNuevo"), cliente.apellido, cliente.nombre, cliente.dni);
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "clienteYaExiste"), cliente.apellido, cliente.nombre, cliente.dni);
                        MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Cliente", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        //Buscar clientes para consultar sus datos
        private void btnCliCBuscar_Click(object sender, EventArgs e)
        {
            dataGridCliC.Rows.Clear();
            Cliente cliente = new Cliente();
            List<Cliente> clienteLista = new List<Cliente>();
            if (radioCliCDni.Checked && !String.IsNullOrEmpty(txtCliCFiltro.Text.Trim()) && Validacion.esNumeroValido(txtCliCFiltro.Text.Trim()))
            {
                cliente = clienteBLL.obtenerClientePorDniPasaporte(txtCliCFiltro.Text.Trim(), "");
                if (cliente != null)
                clienteLista.Add(cliente);
            }
            else if (radioCliCPasaporte.Checked && !String.IsNullOrEmpty(txtCliCFiltro.Text.Trim()))
            {
                cliente = clienteBLL.obtenerClientePorDniPasaporte("", txtCliCFiltro.Text.Trim());
                if(cliente!=null)
                clienteLista.Add(cliente);
            }
            else if (radioCliCApellido.Checked && !String.IsNullOrEmpty(txtCliCFiltro.Text))
            {
                clienteLista = clienteBLL.obtenerClientePorApellido(txtCliCFiltro.Text);
            }
            if (clienteLista != null && clienteLista.Count > 0)
            {
                dataGridCliC.Visible = true;
                for (int i = 0; i < clienteLista.Count; i++)
                {

                    dataGridCliC.Rows.Add(1);
                    dataGridCliC.Rows[i].Cells["HCliCPasaporte"].Value = clienteLista[i].pasaporte;
                    dataGridCliC.Rows[i].Cells["HCliCDni"].Value = clienteLista[i].dni;
                    dataGridCliC.Rows[i].Cells["HCliCApellido"].Value = clienteLista[i].apellido;
                    dataGridCliC.Rows[i].Cells["HCliCNombre"].Value = clienteLista[i].nombre;
                    dataGridCliC.Rows[i].Cells["HCliCDireccion"].Value = clienteLista[i].domicilio;
                    dataGridCliC.Rows[i].Cells["HCliCCiudad"].Value = clienteLista[i].ciudad;
                    dataGridCliC.Rows[i].Cells["HCliCEmail"].Value = clienteLista[i].email;
                    dataGridCliC.Rows[i].Cells["HCliCTelefono"].Value = clienteLista[i].telefono;
                    dataGridCliC.Rows[i].Cells["HCliCCelular"].Value = clienteLista[i].celular;
                    dataGridCliC.Rows[i].Cells["HCliCEstado"].Value = clienteLista[i].estado;
                    dataGridCliC.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                }
            }
            else
            {
                dataGridCliC.Visible = false;
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Buscar un cliente para editar sus datos
        private void btnCliEBuscar_Click(object sender, EventArgs e)
        {
            Cliente cli = new Cliente();
            String filtro = (!String.IsNullOrEmpty(txtCliEFiltro.Text)) ? txtCliEFiltro.Text : null;
            if (radioCliEDni.Checked && filtro != null)
            {
                cli = clienteBLL.obtenerClientePorDniPasaporte(filtro, "");
            }
            else if (radioCliEPasaporte.Checked && filtro != null)
            {
                cli = clienteBLL.obtenerClientePorDniPasaporte("", filtro);
            }
            if (cli != null)
            {
                lblCliEId.Text = cli.idCliente.ToString();
                txtCliEPasaporte.Text = cli.pasaporte;
                txtCliEApellido.Text = cli.apellido;
                txtCliENombre.Text = cli.nombre;
                txtCliEDni.Text = cli.dni;
                txtCliEDireccion.Text = cli.domicilio;
                txtCliECiudad.Text = cli.ciudad;
                txtCliETelefono.Text = cli.telefono;
                txtCliECelular.Text = cli.celular;
                txtCliEEmail.Text = cli.email;
                checkCliEEstado.Checked = (cli.estado == true) ? true : false;

                mostrarCampos();
            }
            else
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Guardar los datos de un cliente editado
        private void btnCliEGuardar_Click(object sender, EventArgs e)
        {
            String nombre = (!String.IsNullOrEmpty(txtCliENombre.Text.Trim())) ? txtCliENombre.Text.Trim() : "";
            String apellido = (!String.IsNullOrEmpty(txtCliEApellido.Text.Trim())) ? txtCliEApellido.Text.Trim() : "";
            String dni = (!String.IsNullOrEmpty(txtCliEDni.Text.Trim())) ? txtCliEDni.Text.Trim() : "";
            String pasaporte = (!String.IsNullOrEmpty(txtCliEPasaporte.Text.Trim())) ? txtCliEPasaporte.Text.Trim() : "";
            String email = (!String.IsNullOrEmpty(txtCliEEmail.Text.Trim())) ? txtCliEEmail.Text.Trim() : "";
            if (String.IsNullOrEmpty(nombre))
            {
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "Nombre");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrEmpty(apellido))
            {
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "Apellido");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrEmpty(dni) && String.IsNullOrEmpty(pasaporte))
            {
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "DNI o Pasaporte");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrEmpty(email))
            {
                campoRequerido = String.Format(I18n.obtenerString("Mensaje", "requerido"), "Email");
                MessageBox.Show(campoRequerido, msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Cliente cli = new Cliente();
                cli.idCliente = Int32.Parse(lblCliEId.Text);
                cli.nombre = nombre;
                cli.apellido = apellido;
                cli.dni = dni;
                cli.pasaporte = pasaporte;
                cli.email = email;
                cli.domicilio = (!String.IsNullOrEmpty(txtCliEDireccion.Text.Trim())) ? txtCliEDireccion.Text.Trim() : "";
                cli.ciudad = (!String.IsNullOrEmpty(txtCliECiudad.Text.Trim())) ? txtCliECiudad.Text.Trim() : "";
                cli.telefono = (!String.IsNullOrEmpty(txtCliETelefono.Text.Trim())) ? txtCliETelefono.Text.Trim() : "";
                cli.celular = (!String.IsNullOrEmpty(txtCliECelular.Text.Trim())) ? txtCliECelular.Text.Trim() : "";
                cli.estado = checkCliEEstado.Checked;
                
                String mensaje = null;
                try
                {
                    if (!cli.estado)
                    {
                        mensaje = String.Format(I18n.obtenerString("Mensaje", "confirmarBaja"), I18n.obtenerString("Mensaje", "cliente"));
                        DialogResult siNoBaja = MessageBox.Show(mensaje, msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoBaja.Equals(DialogResult.Yes))
                        {
                            clienteBLL.actualizarCliente(cli);
                        }
                    }
                    else
                    {
                        DialogResult siNoRes = MessageBox.Show(I18n.obtenerString("Mensaje", "confirmar"), msjConfirmar, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (siNoRes.Equals(DialogResult.Yes))
                        {
                            clienteBLL.actualizarCliente(cli);
                            mensaje = String.Format(I18n.obtenerString("Mensaje", "elementoActualizado"), I18n.obtenerString("InicioDirector", "cliente"));
                            MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_UPD + " Cliente", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }

        //Al hacer click "Siguiente" en la bienvenida del wizard, se cargan los eventos/paquetes
        private void WizPage0EvPaqReservar_NextClick(object sender, CancelEventArgs e)
        {
            cargarEventosPaquetes();
        }

        private void cargarEventosPaquetes()
        {
            Int32 anio = intInpPage1EvPaqResAnio.Value;
            String mensaje = null;
            if (calendarioBLL.existeCalendario(anio))
            {
                List<Calendario> calendarios = calendarioBLL.obtenerCalendarios(anio).Where(cal => DateTime.Compare( DateTime.Today.AddDays(3), cal.producto.fechaSalida)<=0).ToList();
                List<Calendario> calFiltrado = new List<Calendario>();

                foreach (Calendario item in calendarios)
                {
                    DateTime fechaSalida = item.producto.fechaSalida;
                    Calendario cal = item;
                    cal.producto = productoBLL.obtenerProductoPorId(item.producto.idProducto);
                    cal.producto.fechaSalida = fechaSalida;
                    cal.alojamiento = alojamientoBLL.obtenerAlojamientos().SingleOrDefault(a => a.id == item.alojamiento.id);
                    cal.instructor = instructorBLL.obtenerInstructorPorLegajo(item.instructor.legajo);
                    cal.traslado = trasladoBLL.obtenerTraslados().SingleOrDefault(t => t.id == item.traslado.id);
                    calFiltrado.Add(cal);
                }
                if (radioPage1EventoRes.Checked)
                {
                    List<Calendario> eventosCal = calFiltrado.Where(s => s.producto.tipoProducto == EnumProducto.EVENTO.ToString()).ToList();
                    GenerarGrillaEventosPaquetes(eventosCal);
                }
                else if (radioPage1PaqueteRes.Checked)
                {
                    List<Calendario> paquetesCal = calFiltrado.Where(s => s.producto.tipoProducto == EnumProducto.PAQUETE.ToString()).ToList();
                    GenerarGrillaEventosPaquetes(paquetesCal);
                }
            }
            else
            {
                mensaje = String.Format(I18n.obtenerString("Mensaje", "elementoInexistente"), I18n.obtenerString("InicioAtencion", "calendario") + " " + anio.ToString() + " ");
                MessageBox.Show(mensaje, msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                WizPage0Reserva.NavigateBack();
            }
        }

        //Genera la grilla de eventos/paquetes
        private void GenerarGrillaEventosPaquetes(List<Calendario> calLista)
        {
            if (calLista != null && calLista.Count > 0)
            {
                dataGridPage1EvPaqReservar.Rows.Clear();
               
                for (int i = 0; i < calLista.Count; i++)
                {
                    dataGridPage1EvPaqReservar.Rows.Add(1);
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1IdCalendario"].Value = calLista[i].idCalendario;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1TipoProducto"].Value = calLista[i].producto.tipoProducto;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Nombre"].Value = calLista[i].producto.nombre;
                    String[] splitDestino = calLista[i].producto.destino.Split(',');
                    String destinoStr = null;
                    for (int j = 0; j < splitDestino.Length; j++)
                    {
                        destinoStr += obtenerDestinoDic()[splitDestino[j]] + "\r\n";
                    }
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Destino"].Value = destinoStr;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Precio"].Value = calLista[i].producto.precio;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Duracion"].Value = calLista[i].producto.duracion;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Actividades"].Value = calLista[i].producto.actividades.Aggregate((a, b) => a + ',' + b);
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Cupo"].Value = calLista[i].cupo;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1FechaSalida"].Value = calLista[i].producto.fechaSalida.ToShortDateString();
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1FechaRegreso"].Value = calLista[i].producto.fechaSalida.AddDays(calLista[i].producto.duracion - 1).ToShortDateString();
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Instructor"].Value = calLista[i].instructor.apellido + " " + calLista[i].instructor.nombre;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Alojamiento"].Value = calLista[i].alojamiento.categoria + ", " + calLista[i].alojamiento.razonSocial;
                    dataGridPage1EvPaqReservar.Rows[i].Cells["HPage1Traslado"].Value = calLista[i].traslado.vehiculo + ", " + calLista[i].traslado.razonSocial;
                    dataGridPage1EvPaqReservar.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

                }
            }
            else
            {
                MessageBox.Show(I18n.obtenerString("Mensaje", "ningunRegistro"), msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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

        //Al hacer click "radio Eventos" se carga la grilla con los eventos del calendario seleccionado
        private void radioPage1EventoRes_CheckedChanged(object sender, EventArgs e)
        {
            cargarEventosPaquetes();
        }

        //Al hacer click "radio Paquetes" se carga la grilla con los paquetes del calendario seleccionado
        private void radioPage1PaqueteRes_CheckedChange(object sender, EventArgs e)
        {
            cargarEventosPaquetes();
        }

        //Al hacer click "Siguiente" desde la seleccion de eventos/paquetes, se ingresan los clientes para reservar
        private void WizPage1EvPaqReservar_NextClick(object sender, CancelEventArgs e)
        {
            int rowIndex = -1;
            for (int i = 0; i < dataGridPage1EvPaqReservar.SelectedRows.Count; i++)
            {
                rowIndex = dataGridPage1EvPaqReservar.SelectedRows[i].Index;
            }
            List<String> data = new List<String>();
            data.Add("Tipo de Producto: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1TipoProducto"].Value.ToString());
            data.Add("Destino: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Destino"].Value.ToString());
            data.Add("Precio: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Precio"].Value.ToString());
            data.Add("Duracion: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Duracion"].Value.ToString());
            data.Add("Actividades: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Actividades"].Value.ToString());
            data.Add("Cupo: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Cupo"].Value.ToString());
            data.Add("Fecha de Salida: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1FechaSalida"].Value.ToString());
            data.Add("Fecha de Regreso: " +dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1FechaRegreso"].Value.ToString());
            data.Add("Instructor: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Instructor"].Value.ToString());
            data.Add("Alojamiento: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Alojamiento"].Value.ToString());
            data.Add("Traslado: " + dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1Traslado"].Value.ToString());
            richTxtPage2EvPaqDetalle.Text = String.Join(", ", data);
            lblPage2EvPaqResProducto.Text = dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1TipoProducto"].Value.ToString();
            txtPage1EvPaqResIdCalendario.Text = dataGridPage1EvPaqReservar.Rows[rowIndex].Cells["HPage1IdCalendario"].Value.ToString();
            gridPage2EqPaqCliente.Rows.Clear();
        }

        //Cancelar la reserva de evento/paquete
        private void WizPage1EvPaqReservar_CancelClick(object sender, CancelEventArgs e)
        {
            WizPage0Reserva.SelectedPage = WizPage0EvPaqReservar;
        }
        //Al hacer click "Siguiente" desde el ingreso de los clientes, se verifica el cupo, se registran los clientes y confirman los datos
        private void WizPage2EvPaqReservar_NextClick(object sender, CancelEventArgs e)
        {
            //valida los datos ingresados en la grilla de clientes
            List<Cliente> clientes = new List<Cliente>();
            Boolean esValido = true;
            for (int i = 0; i < gridPage2EqPaqCliente.Rows.Count -1; i++)
            {
                Cliente cli = new Cliente();
                if (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Nombre"].Value == null)
                    esValido = false;
                else if (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Apellido"].Value == null)
                    esValido = false;
                else if (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Dni"].Value == null && gridPage2EqPaqCliente.Rows[i].Cells["HPage2Pasaporte"].Value == null)
                    esValido = false;
                else if (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Email"].Value == null )
                    esValido = false;
                esValido = Validacion.esNumeroValido(gridPage2EqPaqCliente.Rows[i].Cells["HPage2Dni"].Value.ToString());
                esValido = Validacion.esEmailValido(gridPage2EqPaqCliente.Rows[i].Cells["HPage2Email"].Value.ToString());
                if (esValido)
                {
                    cli.idCliente = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2IdCliente"].Value != null) ? (Int32)gridPage2EqPaqCliente.Rows[i].Cells["HPage2IdCliente"].Value : 0;
                    cli.dni = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Dni"].Value != null) ? gridPage2EqPaqCliente.Rows[i].Cells["HPage2Dni"].Value.ToString() : "";
                    cli.pasaporte = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Pasaporte"].Value != null) ? gridPage2EqPaqCliente.Rows[i].Cells["HPage2Pasaporte"].Value.ToString() : "";
                    cli.apellido = gridPage2EqPaqCliente.Rows[i].Cells["HPage2Apellido"].Value.ToString();
                    cli.nombre = gridPage2EqPaqCliente.Rows[i].Cells["HPage2Nombre"].Value.ToString();
                    cli.email = gridPage2EqPaqCliente.Rows[i].Cells["HPage2Email"].Value.ToString();
                    cli.telefono = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Telefono"].Value != null) ? gridPage2EqPaqCliente.Rows[i].Cells["HPage2Telefono"].Value.ToString() : "";
                    cli.celular = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Celular"].Value != null) ? gridPage2EqPaqCliente.Rows[i].Cells["HPage2Celular"].Value.ToString() : "";
                    cli.domicilio = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Domicilio"].Value != null) ? gridPage2EqPaqCliente.Rows[i].Cells["HPage2Domicilio"].Value.ToString() : "";
                    cli.ciudad = (gridPage2EqPaqCliente.Rows[i].Cells["HPage2Ciudad"].Value != null) ? gridPage2EqPaqCliente.Rows[i].Cells["HPage2Ciudad"].Value.ToString() : "";
                    clientes.Add(cli);
                } 
            }
            Int32 cupo =  calendarioBLL.obtenerCalendarioPorId(Int32.Parse(txtPage1EvPaqResIdCalendario.Text)).cupo;
            Int32 cantidadReservas = clientes.Count;
            if (esValido && cantidadReservas<=cupo)
            {
                //carga los clientes ingresados en la grilla del paso siguiente
                richTxtPage3EvPaqDetalle.Text = richTxtPage2EvPaqDetalle.Text;
                gridPage3EqPaqCliente.Rows.Clear();
                for (int i = 0; i < clientes.Count; i++)
                {
                    gridPage3EqPaqCliente.Rows.Add(1);
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3IdCliente"].Value = clientes[i].idCliente;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Codigo"].Value = generarCodigoReserva();
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Dni"].Value = clientes[i].dni;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Pasaporte"].Value = clientes[i].pasaporte;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Apellido"].Value = clientes[i].apellido;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Nombre"].Value = clientes[i].nombre;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Email"].Value = clientes[i].email;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Telefono"].Value = clientes[i].telefono;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Celular"].Value = clientes[i].celular;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Domicilio"].Value = clientes[i].domicilio;
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3Ciudad"].Value = clientes[i].ciudad;
                    gridPage3EqPaqCliente.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                } 
            }
            else
            {
                if (!esValido)
                {
                    MessageBox.Show(I18n.obtenerString("Mensaje", "nombreApellidoDniEmailRequeridos") + " - " +
                                    I18n.obtenerString("Mensaje", "formatoDni") + " - " +
                                    I18n.obtenerString("Mensaje", "formatoEmail"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }else
                    MessageBox.Show(I18n.obtenerString("Mensaje", "cupoInsuficiente"), msjError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                WizPage0Reserva.NavigateBack();
            }
        }

        //Al ingresar el DNI de un cliente verifica la existencia y autocompleta los datos
        private void gridPage2EqPaqCliente_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gridPage2EqPaqCliente.Columns["HPage2Dni"].Index && e.RowIndex >= 0)
            {
                if (gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Dni"].Value != null)
                {
                    Cliente cliente = clienteBLL.obtenerClientePorDniPasaporte(gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Dni"].Value.ToString(), "");
                    if (cliente != null)
                    {
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2IdCliente"].Value = cliente.idCliente;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Dni"].Value = cliente.dni;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Pasaporte"].Value = cliente.pasaporte;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Apellido"].Value = cliente.apellido;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Nombre"].Value = cliente.nombre;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Email"].Value = cliente.email;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Telefono"].Value = cliente.telefono;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Celular"].Value = cliente.celular;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Domicilio"].Value = cliente.domicilio;
                        gridPage2EqPaqCliente.Rows[e.RowIndex].Cells["HPage2Ciudad"].Value = cliente.ciudad;
                    }
                }
            }
        }

        //Cancelar la reserva de evento/paquete
        private void WizPage2EvPaqReservar_CancelClick(object sender, CancelEventArgs e)
        {
            WizPage0Reserva.SelectedPage = WizPage0EvPaqReservar;
        }

        private String generarCodigoReserva()
        {
            string alfabetoMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string alfabetoMin = "abcdefghijklmnopqrstuvwxyz";
            string numeros = "1234567890";
            string caracteres = alfabetoMay + alfabetoMin + numeros;
            int longitud = 6;
            string codigo = string.Empty;
            for (int i = 0; i < longitud; i++)
            {
                string caracter = string.Empty;
                do
                {
                    int indice = new Random().Next(0, caracteres.Length);
                    caracter = caracteres.ToCharArray()[indice].ToString();
                } while (codigo.IndexOf(caracter) != -1);
                codigo += caracter;
            }
            return  codigo;
        }

        //Registrar en bd las reservas generadas de eventos/paquetes
        private void WizPage3EvPaqReservar_NextClick(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < gridPage3EqPaqCliente.Rows.Count; i++)
            {
                Cliente cli = new Cliente();
                cli.idCliente = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3IdCliente"].Value != null) ? Int32.Parse(gridPage3EqPaqCliente.Rows[i].Cells["HPage3IdCliente"].Value.ToString()) : 0;
                cli.dni = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Dni"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Dni"].Value.ToString() : "";
                cli.pasaporte = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Pasaporte"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Pasaporte"].Value.ToString() : "";
                cli.apellido = gridPage3EqPaqCliente.Rows[i].Cells["HPage3Apellido"].Value.ToString();
                cli.nombre = gridPage3EqPaqCliente.Rows[i].Cells["HPage3Nombre"].Value.ToString();
                cli.email = gridPage3EqPaqCliente.Rows[i].Cells["HPage3Email"].Value.ToString();
                cli.telefono = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Telefono"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Telefono"].Value.ToString() : "";
                cli.celular = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Celular"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Celular"].Value.ToString() : "";
                cli.domicilio = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Domicilio"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Domicilio"].Value.ToString() : "";
                cli.ciudad = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Ciudad"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Ciudad"].Value.ToString() : "";

                if (cli.idCliente == 0)
                {
                    cli.estado = true;
                    cli.idCliente = clienteBLL.agregarCliente(cli);
                    gridPage3EqPaqCliente.Rows[i].Cells["HPage3IdCliente"].Value = cli.idCliente;
                    limpiarCampos();
                }

                Reserva res = new Reserva();
                res.idCalendario = Int32.Parse(txtPage1EvPaqResIdCalendario.Text);
                res.codigo = gridPage3EqPaqCliente.Rows[i].Cells["HPage3Codigo"].Value.ToString();
                res.fecha = DateTime.Now;
                res.estado = "INICIADA";
                res.importe = productoBLL.obtenerProductoPorId(calendarioBLL.obtenerCalendarioPorId(res.idCalendario).producto.idProducto).precio;
                res.tipoPago = "";
                res.cantidad = 1;
                res.idCliente = cli.idCliente;
                try
                {
                    //insertar nueva reserva y venta
                    reservaBLL.agregarReserva(res);
                    //actualizar cupos en calendario
                    Int32 cupoActual = calendarioBLL.obtenerCalendarioPorId(res.idCalendario).cupo;
                    calendarioBLL.actualizarCalendarioCupo(res.idCalendario, cupoActual - res.cantidad);
                }
                catch (Exception ex)
                {
                    Bitacora bitacora = new Bitacora(usuarioLogueado.id, rolUsrLogueado.descripcion, DateTime.Now.Date, Constantes.EXCEPCION_BLL_INS + " Reserva", ex.ToString());
                    BitacoraBLL.registrarBitacora(bitacora);
                }
            }
        }
              
        //Fin del wizard para realizar reservas de eventos/paquetes
        private void WizPage3EvPaqReservar_FinishClick(object sender, CancelEventArgs e)
        {
            limpiarCampos();
            WizPage0Reserva.SelectedPage = WizPage0EvPaqReservar;
        }

        //Cancelar la reserva de evento/paquete
        private void WizPage3EvPaqReservar_CancelClick(object sender, CancelEventArgs e)
        {
            WizPage0Reserva.SelectedPage = WizPage0EvPaqReservar;
        }

        //Finalizar la reserva de evento/paquete
        private void WizPage4EvPaqReservar_FinishClick(object sender, CancelEventArgs e)
        {
            WizPage0Reserva.SelectedPage = WizPage0EvPaqReservar;
        }

        //Emitir un PDF  con las reservas de eventos/paquetes generadas
        private void btnPage4EvPaqResPDF_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridPage3EqPaqCliente.Rows.Count; i++)
            {
                Cliente cli = new Cliente();
                cli.dni = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Dni"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Dni"].Value.ToString() : "";
                cli.pasaporte = (gridPage3EqPaqCliente.Rows[i].Cells["HPage3Pasaporte"].Value != null) ? gridPage3EqPaqCliente.Rows[i].Cells["HPage3Pasaporte"].Value.ToString() : "";
                cli = clienteBLL.obtenerClientePorDniPasaporte(cli.dni, cli.pasaporte);
                Reserva res = reservaBLL.obtenerReservaPorCodigo(gridPage3EqPaqCliente.Rows[i].Cells["HPage3Codigo"].Value.ToString());
                String tipoProducto = productoBLL.obtenerProductoPorId(calendarioBLL.obtenerCalendarioPorId(res.idCalendario).producto.idProducto).tipoProducto;
                PDFUtil.GenerarReservasEvPaq(res, tipoProducto, cli.apellido + " " + cli.nombre, cli.dni, richTxtPage3EvPaqDetalle.Text);
            }
            MessageBox.Show("PDFs generados en: C:\\OEV_PDF", msjInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }










    }
}
