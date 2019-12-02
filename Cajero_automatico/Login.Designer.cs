namespace Cajero_automatico_Online
{
    partial class Cajero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cajero));
            this.labelLogin = new System.Windows.Forms.Label();
            this.panel_line = new System.Windows.Forms.Panel();
            this.LoginBtnNext = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_line2 = new System.Windows.Forms.Panel();
            this.labelpin = new System.Windows.Forms.Label();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.Entrar = new System.Windows.Forms.Button();
            this.tarjetaBox = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLogin.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.Black;
            this.labelLogin.Location = new System.Drawing.Point(258, 139);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(291, 46);
            this.labelLogin.TabIndex = 12;
            this.labelLogin.Text = "Ingrese su tarjeta:";
            this.labelLogin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel_line
            // 
            this.panel_line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(114)))));
            this.panel_line.Location = new System.Drawing.Point(258, 254);
            this.panel_line.Name = "panel_line";
            this.panel_line.Size = new System.Drawing.Size(291, 5);
            this.panel_line.TabIndex = 11;
            // 
            // LoginBtnNext
            // 
            this.LoginBtnNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoginBtnNext.BackColor = System.Drawing.Color.SteelBlue;
            this.LoginBtnNext.FlatAppearance.BorderSize = 0;
            this.LoginBtnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginBtnNext.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginBtnNext.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.LoginBtnNext.Location = new System.Drawing.Point(296, 321);
            this.LoginBtnNext.Name = "LoginBtnNext";
            this.LoginBtnNext.Size = new System.Drawing.Size(213, 35);
            this.LoginBtnNext.TabIndex = 6;
            this.LoginBtnNext.Text = "Continuar";
            this.LoginBtnNext.UseVisualStyleBackColor = false;
            this.LoginBtnNext.Click += new System.EventHandler(this.LoginBtnNext_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel_line2);
            this.panel2.Controls.Add(this.labelpin);
            this.panel2.Controls.Add(this.PassBox);
            this.panel2.Controls.Add(this.Entrar);
            this.panel2.Location = new System.Drawing.Point(645, 114);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(155, 336);
            this.panel2.TabIndex = 5;
            // 
            // panel_line2
            // 
            this.panel_line2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(114)))));
            this.panel_line2.Location = new System.Drawing.Point(258, 254);
            this.panel_line2.Name = "panel_line2";
            this.panel_line2.Size = new System.Drawing.Size(291, 5);
            this.panel_line2.TabIndex = 14;
            // 
            // labelpin
            // 
            this.labelpin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelpin.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpin.ForeColor = System.Drawing.Color.Black;
            this.labelpin.Location = new System.Drawing.Point(-65, 82);
            this.labelpin.Name = "labelpin";
            this.labelpin.Size = new System.Drawing.Size(291, 46);
            this.labelpin.TabIndex = 13;
            this.labelpin.Text = "Ingrese su PIN";
            this.labelpin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PassBox
            // 
            this.PassBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PassBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PassBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassBox.Location = new System.Drawing.Point(-27, 169);
            this.PassBox.Name = "PassBox";
            this.PassBox.PasswordChar = '*';
            this.PassBox.Size = new System.Drawing.Size(213, 24);
            this.PassBox.TabIndex = 2;
            this.PassBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PassBox.UseSystemPasswordChar = true;
            this.PassBox.TextChanged += new System.EventHandler(this.PassBox_TextChanged);
            // 
            // Entrar
            // 
            this.Entrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Entrar.BackColor = System.Drawing.Color.SteelBlue;
            this.Entrar.FlatAppearance.BorderSize = 0;
            this.Entrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Entrar.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Entrar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Entrar.Location = new System.Drawing.Point(-27, 264);
            this.Entrar.Name = "Entrar";
            this.Entrar.Size = new System.Drawing.Size(213, 35);
            this.Entrar.TabIndex = 4;
            this.Entrar.Text = "Entrar";
            this.Entrar.UseVisualStyleBackColor = false;
            this.Entrar.Click += new System.EventHandler(this.Entrar_Click_1);
            // 
            // tarjetaBox
            // 
            this.tarjetaBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tarjetaBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tarjetaBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tarjetaBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tarjetaBox.Location = new System.Drawing.Point(296, 223);
            this.tarjetaBox.Name = "tarjetaBox";
            this.tarjetaBox.Size = new System.Drawing.Size(213, 25);
            this.tarjetaBox.TabIndex = 1;
            this.tarjetaBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Cajero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.tarjetaBox);
            this.Controls.Add(this.panel_line);
            this.Controls.Add(this.LoginBtnNext);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Cajero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Cajero_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.TextBox tarjetaBox;
        private System.Windows.Forms.Button Entrar;
        private System.Windows.Forms.Button LoginBtnNext;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_line;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Panel panel_line2;
        private System.Windows.Forms.Label labelpin;
    }
}

