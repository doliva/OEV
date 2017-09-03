namespace OEVApp
{
    partial class Director
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sideBarPanelDirector = new DevComponents.DotNetBar.SideBar();
            this.sideBarPanelCalendario = new DevComponents.DotNetBar.SideBarPanelItem();
            this.sideBarPanelProducto = new DevComponents.DotNetBar.SideBarPanelItem();
            this.sideBarPanelAlojamiento = new DevComponents.DotNetBar.SideBarPanelItem();
            this.sideBarPanelInstructores = new DevComponents.DotNetBar.SideBarPanelItem();
            this.sideBarPanelTraslados = new DevComponents.DotNetBar.SideBarPanelItem();
            this.sideBarPanelReporte = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItemAgregarC = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemConsultarC = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemModificarC = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemAgregarP = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemConsultarP = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemModificarP = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemAgregarA = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemConsultarA = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemModificarA = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemAgregarI = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemConsultarI = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemModificarI = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemAgregarT = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemConsultarT = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemModificarT = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemProveedores = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemReservas = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemVentas = new DevComponents.DotNetBar.ButtonItem();
            this.SuspendLayout();
            // 
            // sideBarPanelDirector
            // 
            this.sideBarPanelDirector.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.sideBarPanelDirector.Appearance = DevComponents.DotNetBar.eSideBarAppearance.Flat;
            this.sideBarPanelDirector.BackColor = System.Drawing.Color.White;
            this.sideBarPanelDirector.ExpandedPanel = this.sideBarPanelInstructores;
            this.sideBarPanelDirector.ForeColor = System.Drawing.Color.Black;
            this.sideBarPanelDirector.Location = new System.Drawing.Point(23, 35);
            this.sideBarPanelDirector.Name = "sideBarPanelDirector";
            this.sideBarPanelDirector.Panels.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sideBarPanelCalendario,
            this.sideBarPanelProducto,
            this.sideBarPanelAlojamiento,
            this.sideBarPanelInstructores,
            this.sideBarPanelTraslados,
            this.sideBarPanelReporte});
            this.sideBarPanelDirector.Size = new System.Drawing.Size(141, 331);
            this.sideBarPanelDirector.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.sideBarPanelDirector.TabIndex = 0;
            this.sideBarPanelDirector.UsingSystemColors = true;
            // 
            // sideBarPanelCalendario
            // 
            this.sideBarPanelCalendario.BackgroundStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sideBarPanelCalendario.BackgroundStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.sideBarPanelCalendario.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelCalendario.BackgroundStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelCalendario.BackgroundStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelCalendario.ForeColor = System.Drawing.Color.White;
            this.sideBarPanelCalendario.HeaderHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelCalendario.HeaderHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelCalendario.HeaderHotStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelCalendario.HeaderHotStyle.GradientAngle = 90;
            this.sideBarPanelCalendario.HeaderSideHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelCalendario.HeaderSideHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelCalendario.HeaderSideHotStyle.GradientAngle = 90;
            this.sideBarPanelCalendario.HeaderSideStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelCalendario.HeaderSideStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelCalendario.HeaderSideStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelCalendario.HeaderSideStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelCalendario.HeaderSideStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelCalendario.HeaderSideStyle.GradientAngle = 90;
            this.sideBarPanelCalendario.HeaderStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelCalendario.HeaderStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelCalendario.HeaderStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelCalendario.HeaderStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelCalendario.HeaderStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelCalendario.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sideBarPanelCalendario.HeaderStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelCalendario.HeaderStyle.GradientAngle = 90;
            this.sideBarPanelCalendario.Name = "sideBarPanelCalendario";
            this.sideBarPanelCalendario.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemAgregarC,
            this.buttonItemConsultarC,
            this.buttonItemModificarC});
            this.sideBarPanelCalendario.Text = "Calendario";
            // 
            // sideBarPanelProducto
            // 
            this.sideBarPanelProducto.BackgroundStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sideBarPanelProducto.BackgroundStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.sideBarPanelProducto.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelProducto.BackgroundStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelProducto.BackgroundStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelProducto.ForeColor = System.Drawing.Color.White;
            this.sideBarPanelProducto.HeaderHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelProducto.HeaderHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelProducto.HeaderHotStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelProducto.HeaderHotStyle.GradientAngle = 90;
            this.sideBarPanelProducto.HeaderSideHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelProducto.HeaderSideHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelProducto.HeaderSideHotStyle.GradientAngle = 90;
            this.sideBarPanelProducto.HeaderSideStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelProducto.HeaderSideStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelProducto.HeaderSideStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelProducto.HeaderSideStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelProducto.HeaderSideStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelProducto.HeaderSideStyle.GradientAngle = 90;
            this.sideBarPanelProducto.HeaderStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelProducto.HeaderStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelProducto.HeaderStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelProducto.HeaderStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelProducto.HeaderStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelProducto.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sideBarPanelProducto.HeaderStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelProducto.HeaderStyle.GradientAngle = 90;
            this.sideBarPanelProducto.Name = "sideBarPanelProducto";
            this.sideBarPanelProducto.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemAgregarP,
            this.buttonItemConsultarP,
            this.buttonItemModificarP});
            this.sideBarPanelProducto.Text = "Productos";
            // 
            // sideBarPanelAlojamiento
            // 
            this.sideBarPanelAlojamiento.BackgroundStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sideBarPanelAlojamiento.BackgroundStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.sideBarPanelAlojamiento.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelAlojamiento.BackgroundStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelAlojamiento.BackgroundStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelAlojamiento.ForeColor = System.Drawing.Color.White;
            this.sideBarPanelAlojamiento.HeaderHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelAlojamiento.HeaderHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelAlojamiento.HeaderHotStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelAlojamiento.HeaderHotStyle.GradientAngle = 90;
            this.sideBarPanelAlojamiento.HeaderSideHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelAlojamiento.HeaderSideHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelAlojamiento.HeaderSideHotStyle.GradientAngle = 90;
            this.sideBarPanelAlojamiento.HeaderSideStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelAlojamiento.HeaderSideStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelAlojamiento.HeaderSideStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelAlojamiento.HeaderSideStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelAlojamiento.HeaderSideStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelAlojamiento.HeaderSideStyle.GradientAngle = 90;
            this.sideBarPanelAlojamiento.HeaderStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelAlojamiento.HeaderStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelAlojamiento.HeaderStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelAlojamiento.HeaderStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelAlojamiento.HeaderStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelAlojamiento.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sideBarPanelAlojamiento.HeaderStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelAlojamiento.HeaderStyle.GradientAngle = 90;
            this.sideBarPanelAlojamiento.Name = "sideBarPanelAlojamiento";
            this.sideBarPanelAlojamiento.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemAgregarA,
            this.buttonItemConsultarA,
            this.buttonItemModificarA});
            this.sideBarPanelAlojamiento.Text = "Alojamientos";
            // 
            // sideBarPanelInstructores
            // 
            this.sideBarPanelInstructores.BackgroundStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sideBarPanelInstructores.BackgroundStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.sideBarPanelInstructores.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelInstructores.BackgroundStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelInstructores.BackgroundStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelInstructores.ForeColor = System.Drawing.Color.White;
            this.sideBarPanelInstructores.HeaderHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelInstructores.HeaderHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelInstructores.HeaderHotStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelInstructores.HeaderHotStyle.GradientAngle = 90;
            this.sideBarPanelInstructores.HeaderSideHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelInstructores.HeaderSideHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelInstructores.HeaderSideHotStyle.GradientAngle = 90;
            this.sideBarPanelInstructores.HeaderSideStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelInstructores.HeaderSideStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelInstructores.HeaderSideStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelInstructores.HeaderSideStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelInstructores.HeaderSideStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelInstructores.HeaderSideStyle.GradientAngle = 90;
            this.sideBarPanelInstructores.HeaderStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelInstructores.HeaderStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelInstructores.HeaderStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelInstructores.HeaderStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelInstructores.HeaderStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelInstructores.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sideBarPanelInstructores.HeaderStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelInstructores.HeaderStyle.GradientAngle = 90;
            this.sideBarPanelInstructores.Name = "sideBarPanelInstructores";
            this.sideBarPanelInstructores.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemAgregarI,
            this.buttonItemConsultarI,
            this.buttonItemModificarI});
            this.sideBarPanelInstructores.Text = "Instructores";
            // 
            // sideBarPanelTraslados
            // 
            this.sideBarPanelTraslados.BackgroundStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sideBarPanelTraslados.BackgroundStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.sideBarPanelTraslados.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelTraslados.BackgroundStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelTraslados.BackgroundStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelTraslados.ForeColor = System.Drawing.Color.White;
            this.sideBarPanelTraslados.HeaderHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelTraslados.HeaderHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelTraslados.HeaderHotStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelTraslados.HeaderHotStyle.GradientAngle = 90;
            this.sideBarPanelTraslados.HeaderSideHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelTraslados.HeaderSideHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelTraslados.HeaderSideHotStyle.GradientAngle = 90;
            this.sideBarPanelTraslados.HeaderSideStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelTraslados.HeaderSideStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelTraslados.HeaderSideStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelTraslados.HeaderSideStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelTraslados.HeaderSideStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelTraslados.HeaderSideStyle.GradientAngle = 90;
            this.sideBarPanelTraslados.HeaderStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelTraslados.HeaderStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelTraslados.HeaderStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelTraslados.HeaderStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelTraslados.HeaderStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelTraslados.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sideBarPanelTraslados.HeaderStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelTraslados.HeaderStyle.GradientAngle = 90;
            this.sideBarPanelTraslados.Name = "sideBarPanelTraslados";
            this.sideBarPanelTraslados.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemAgregarT,
            this.buttonItemConsultarT,
            this.buttonItemModificarT});
            this.sideBarPanelTraslados.Text = "Traslados";
            // 
            // sideBarPanelReporte
            // 
            this.sideBarPanelReporte.BackgroundStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sideBarPanelReporte.BackgroundStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.sideBarPanelReporte.BackgroundStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelReporte.BackgroundStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelReporte.BackgroundStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelReporte.ForeColor = System.Drawing.Color.White;
            this.sideBarPanelReporte.HeaderHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelReporte.HeaderHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelReporte.HeaderHotStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelReporte.HeaderHotStyle.GradientAngle = 90;
            this.sideBarPanelReporte.HeaderSideHotStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelReporte.HeaderSideHotStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelReporte.HeaderSideHotStyle.GradientAngle = 90;
            this.sideBarPanelReporte.HeaderSideStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelReporte.HeaderSideStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelReporte.HeaderSideStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelReporte.HeaderSideStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelReporte.HeaderSideStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelReporte.HeaderSideStyle.GradientAngle = 90;
            this.sideBarPanelReporte.HeaderStyle.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sideBarPanelReporte.HeaderStyle.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.sideBarPanelReporte.HeaderStyle.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.sideBarPanelReporte.HeaderStyle.BorderColor.Color = System.Drawing.Color.Green;
            this.sideBarPanelReporte.HeaderStyle.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Right | DevComponents.DotNetBar.eBorderSide.Top) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.sideBarPanelReporte.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.sideBarPanelReporte.HeaderStyle.ForeColor.Color = System.Drawing.SystemColors.ControlText;
            this.sideBarPanelReporte.HeaderStyle.GradientAngle = 90;
            this.sideBarPanelReporte.Name = "sideBarPanelReporte";
            this.sideBarPanelReporte.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemProveedores,
            this.buttonItemReservas,
            this.buttonItemVentas});
            this.sideBarPanelReporte.Text = "Reportes";
            // 
            // buttonItemAgregarC
            // 
            this.buttonItemAgregarC.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemAgregarC.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemAgregarC.Name = "buttonItemAgregarC";
            this.buttonItemAgregarC.Text = "Agregar";
            // 
            // buttonItemConsultarC
            // 
            this.buttonItemConsultarC.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemConsultarC.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemConsultarC.Name = "buttonItemConsultarC";
            this.buttonItemConsultarC.Text = "Consultar";
            // 
            // buttonItemModificarC
            // 
            this.buttonItemModificarC.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemModificarC.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemModificarC.Name = "buttonItemModificarC";
            this.buttonItemModificarC.Text = "Modificar";
            // 
            // buttonItemAgregarP
            // 
            this.buttonItemAgregarP.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemAgregarP.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemAgregarP.Name = "buttonItemAgregarP";
            this.buttonItemAgregarP.Text = "Agregar";
            // 
            // buttonItemConsultarP
            // 
            this.buttonItemConsultarP.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemConsultarP.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemConsultarP.Name = "buttonItemConsultarP";
            this.buttonItemConsultarP.Text = "Consultar";
            // 
            // buttonItemModificarP
            // 
            this.buttonItemModificarP.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemModificarP.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemModificarP.Name = "buttonItemModificarP";
            this.buttonItemModificarP.Text = "Modificar";
            // 
            // buttonItemAgregarA
            // 
            this.buttonItemAgregarA.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemAgregarA.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemAgregarA.Name = "buttonItemAgregarA";
            this.buttonItemAgregarA.Text = "Agregar";
            // 
            // buttonItemConsultarA
            // 
            this.buttonItemConsultarA.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemConsultarA.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemConsultarA.Name = "buttonItemConsultarA";
            this.buttonItemConsultarA.Text = "Consultar";
            // 
            // buttonItemModificarA
            // 
            this.buttonItemModificarA.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemModificarA.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemModificarA.Name = "buttonItemModificarA";
            this.buttonItemModificarA.Text = "Modificar";
            // 
            // buttonItemAgregarI
            // 
            this.buttonItemAgregarI.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemAgregarI.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemAgregarI.Name = "buttonItemAgregarI";
            this.buttonItemAgregarI.Text = "Agregar";
            // 
            // buttonItemConsultarI
            // 
            this.buttonItemConsultarI.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemConsultarI.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemConsultarI.Name = "buttonItemConsultarI";
            this.buttonItemConsultarI.Text = "Consultar";
            // 
            // buttonItemModificarI
            // 
            this.buttonItemModificarI.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemModificarI.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemModificarI.Name = "buttonItemModificarI";
            this.buttonItemModificarI.Text = "Modificar";
            // 
            // buttonItemAgregarT
            // 
            this.buttonItemAgregarT.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemAgregarT.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemAgregarT.Name = "buttonItemAgregarT";
            this.buttonItemAgregarT.Text = "Agregar";
            // 
            // buttonItemConsultarT
            // 
            this.buttonItemConsultarT.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemConsultarT.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemConsultarT.Name = "buttonItemConsultarT";
            this.buttonItemConsultarT.Text = "Consultar";
            // 
            // buttonItemModificarT
            // 
            this.buttonItemModificarT.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemModificarT.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemModificarT.Name = "buttonItemModificarT";
            this.buttonItemModificarT.Text = "Modificar";
            // 
            // buttonItemProveedores
            // 
            this.buttonItemProveedores.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemProveedores.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemProveedores.Name = "buttonItemProveedores";
            this.buttonItemProveedores.Text = "Proveedores";
            // 
            // buttonItemReservas
            // 
            this.buttonItemReservas.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemReservas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemReservas.Name = "buttonItemReservas";
            this.buttonItemReservas.Text = "Reservas";
            // 
            // buttonItemVentas
            // 
            this.buttonItemVentas.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItemVentas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItemVentas.Name = "buttonItemVentas";
            this.buttonItemVentas.Text = "Ventas";
            // 
            // Director
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(625, 396);
            this.Controls.Add(this.sideBarPanelDirector);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Director";
            this.Text = "OEV - Director";
            this.TransparencyKey = System.Drawing.Color.White;
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SideBar sideBarPanelDirector;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelCalendario;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelAlojamiento;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelProducto;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelInstructores;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelReporte;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelTraslados;
        private DevComponents.DotNetBar.ButtonItem buttonItemAgregarI;
        private DevComponents.DotNetBar.ButtonItem buttonItemConsultarI;
        private DevComponents.DotNetBar.ButtonItem buttonItemModificarI;
        private DevComponents.DotNetBar.ButtonItem buttonItemAgregarC;
        private DevComponents.DotNetBar.ButtonItem buttonItemConsultarC;
        private DevComponents.DotNetBar.ButtonItem buttonItemModificarC;
        private DevComponents.DotNetBar.ButtonItem buttonItemAgregarP;
        private DevComponents.DotNetBar.ButtonItem buttonItemConsultarP;
        private DevComponents.DotNetBar.ButtonItem buttonItemModificarP;
        private DevComponents.DotNetBar.ButtonItem buttonItemAgregarA;
        private DevComponents.DotNetBar.ButtonItem buttonItemConsultarA;
        private DevComponents.DotNetBar.ButtonItem buttonItemModificarA;
        private DevComponents.DotNetBar.ButtonItem buttonItemAgregarT;
        private DevComponents.DotNetBar.ButtonItem buttonItemConsultarT;
        private DevComponents.DotNetBar.ButtonItem buttonItemModificarT;
        private DevComponents.DotNetBar.ButtonItem buttonItemProveedores;
        private DevComponents.DotNetBar.ButtonItem buttonItemReservas;
        private DevComponents.DotNetBar.ButtonItem buttonItemVentas;


    }
}