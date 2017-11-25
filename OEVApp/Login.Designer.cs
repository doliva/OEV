namespace OEVApp
{
    partial class Login
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
            this.imageContainer1 = new Telerik.QuickStart.WinControls.ImageContainer();
            this.txtUsuario = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtClave = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblUsuario = new DevComponents.DotNetBar.LabelX();
            this.lblClave = new DevComponents.DotNetBar.LabelX();
            this.btnLogin = new DevComponents.DotNetBar.ButtonX();
            this.cbbIdioma = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageContainer1
            // 
            this.imageContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageContainer1.Image = global::OEVApp.Properties.Resources.OEV;
            this.imageContainer1.Location = new System.Drawing.Point(37, 13);
            this.imageContainer1.Name = "imageContainer1";
            this.imageContainer1.Size = new System.Drawing.Size(223, 35);
            this.imageContainer1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageContainer1.TabIndex = 6;
            this.imageContainer1.TabStop = false;
            // 
            // txtUsuario
            // 
            // 
            // 
            // 
            this.txtUsuario.Border.Class = "TextBoxBorder";
            this.txtUsuario.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUsuario.Location = new System.Drawing.Point(118, 71);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(142, 20);
            this.txtUsuario.TabIndex = 7;
            // 
            // txtClave
            // 
            // 
            // 
            // 
            this.txtClave.Border.Class = "TextBoxBorder";
            this.txtClave.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtClave.Location = new System.Drawing.Point(118, 108);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(142, 20);
            this.txtClave.TabIndex = 8;
            // 
            // lblUsuario
            // 
            // 
            // 
            // 
            this.lblUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(37, 71);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(75, 23);
            this.lblUsuario.TabIndex = 9;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblClave
            // 
            // 
            // 
            // 
            this.lblClave.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClave.Location = new System.Drawing.Point(37, 104);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(75, 23);
            this.lblClave.TabIndex = 10;
            this.lblClave.Text = "Clave:";
            // 
            // btnLogin
            // 
            this.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLogin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(37, 213);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(223, 23);
            this.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cbbIdioma
            // 
            this.cbbIdioma.DisplayMember = "Text";
            this.cbbIdioma.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIdioma.EnablePopupResize = false;
            this.cbbIdioma.ItemHeight = 14;
            this.cbbIdioma.Location = new System.Drawing.Point(61, 158);
            this.cbbIdioma.Name = "cbbIdioma";
            this.cbbIdioma.Size = new System.Drawing.Size(168, 20);
            this.cbbIdioma.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbIdioma.TabIndex = 12;
            this.cbbIdioma.SelectionChangeCommitted += new System.EventHandler(this.cbbIdioma_SelectionChangeCommitted);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.cbbIdioma);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.imageContainer1);
            this.Name = "Login";
            this.Text = "OEV - Login";
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.QuickStart.WinControls.ImageContainer imageContainer1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUsuario;
        private DevComponents.DotNetBar.Controls.TextBoxX txtClave;
        private DevComponents.DotNetBar.LabelX lblUsuario;
        private DevComponents.DotNetBar.LabelX lblClave;
        private DevComponents.DotNetBar.ButtonX btnLogin;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbIdioma;
    }
}

