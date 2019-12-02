using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
/*using CajeroConnection;*/
using CajeroConnectionOnline;

namespace Cajero_automatico_Online
{
    public partial class Menu : Form
    {
        
        public int retirolimit = 50000;
        public int proveedor = 0;
        public int monto = 0;
        public string bienvenidamenu;
        public string lastconmenu;
        public string tarjeta = Conexion.tarjeta;
        public DateTime fecha = DateTime.Now;
        public Menu()
        {
            InitializeComponent();
            panel_llamada.Location = new Point(0, 71);
            Load += new EventHandler(Menu_Load);            
            panel_llamada2.Location = panel_llamada.Location;
            
            string CMD = string.Format("SELECT nombre, apellido, lastcon FROM persona WHERE persona.nro_tarjeta = '{0}';", tarjeta);
            DataSet ds = Conexion.Ejecutar(CMD);
            string nom = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
            string apellido = ds.Tables[0].Rows[0]["apellido"].ToString().Trim();
            string lastconnection = ds.Tables[0].Rows[0]["lastcon"].ToString().Trim();
            DateTime date = Convert.ToDateTime(lastconnection);

            if (DateTime.Now.Day - date.Day == 0)
            {
                lastcon.Text = "Ultima conexion: Hoy a las " + date.ToString("hh:mm");
            }else if(DateTime.Now.Day - date.Day == 1)
            {
                lastcon.Text = "Ultima conexion: Ayer a las " + date.ToString("hh:mm");
            }
            else
            {
                lastcon.Text = "Ultima conexion: " + date.ToString();
            }
            bienvenida.Text = "Bienvenido " + nom + " " + apellido;            
            bienvenidamenu = "Bienvenido " + nom + " " + apellido;
            lastconmenu = "Ultima conexion: " + fecha;
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            string CMD = string.Format("UPDATE persona SET lastcon = '{0}' WHERE nro_tarjeta = '{1}'", fecha.ToString("yyyy-MM-dd-HH:mm:ss"), tarjeta);
            DataSet ds = Conexion.Ejecutar(CMD);            
            Application.Exit();
        }
        private void Menu_Load(object sender, EventArgs e)
        {            
            LoadingGif.Visible = true;
            LoadingGif.Load("Spin.gif");
            lastcon.Visible = true;
            Ocultar_panels();
        }
        public void Ocultar_panels()
        {
            panel_llamada.Visible = false;
            panel_llamada2.Visible = false;
            panel2.Visible = false;
            lastcon.Visible = false;
            panel_line.Visible = false;
            retiroPers.Visible = false;
            panel3.Visible = false;
            panel_depositar.Visible = false;
            panel_depositar2.Visible = false;
            panel_balance.Visible = false;
            loadpanel.Visible = false;
        }
        private void Back_Click(object sender, EventArgs e)
        {
            Cajero.policia = false;
            retirolimit = 50000;
            this.Hide();
            Cajero cajero = new Cajero
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(this.ClientSize.Width, this.ClientSize.Height),
                Top = this.Top,
                Left = this.Left
            };            
            cajero.Visible = true;
            string CMD = string.Format("UPDATE persona SET lastcon = '{0}' WHERE nro_tarjeta = '{1}'", fecha.ToString("yyyy-MM-dd-HH:mm:ss"), tarjeta);
            DataSet ds = Conexion.Ejecutar(CMD);
        }

        private void Llamada_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            bienvenida.Text = "Comprar tarjetas de llamada";
            panel_llamada.Width = 800;
            panel_llamada.Height = 379;
            panel_llamada.Dock = DockStyle.Fill;
            panel_llamada.Visible = true;            
        }

        private void LlamadasBack_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
            combox_monto.SelectedIndex = -1;
            combox_prov.SelectedIndex = -1;
        }

        private void LlamadaBtn_Click(object sender, EventArgs e)
        {
            string balanceCMD = string.Format("SELECT balance, tipo_cuenta FROM persona WHERE nro_tarjeta = '{0}';", tarjeta);
            DataSet ds = Conexion.Ejecutar(balanceCMD);
            int balance = int.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
            int cuentatipo = int.Parse(ds.Tables[0].Rows[0]["tipo_cuenta"].ToString());

            if (combox_monto.SelectedIndex > -1 && combox_monto.SelectedIndex < 6 && combox_prov.SelectedIndex > -1 && combox_prov.SelectedIndex < 4)
            {
                monto = int.Parse(combox_monto.Text);                
                if (balance >= monto && retirolimit > monto || cuentatipo == 2 && retirolimit > monto)
                {
                    string CMD = string.Format("INSERT INTO recarga (codigo, nro_tarjeta, monto, id_proveedor) VALUES ('1', '{0}', '{1}', '{2}'); UPDATE recarga SET codigo = LPAD(FLOOR(RAND() * 999999.99), 8, '0'); UPDATE persona, recarga SET persona.balance = (persona.balance - recarga.monto) WHERE persona.nro_tarjeta = '{0}'; SELECT codigo FROM recarga WHERE nro_tarjeta = '{0}' AND monto = '{1}' AND id_proveedor = '{2}' ORDER BY fecha DESC;", tarjeta, monto, proveedor);
                    ds = Conexion.Ejecutar(CMD);
                    string codigo = ds.Tables[0].Rows[0]["codigo"].ToString().Trim();
                    label_codigo.Text = "Su codigo es: " + codigo;
                    Ocultar_panels();
                    panel_llamada2.Width = 800;
                    panel_llamada2.Height = 379;
                    panel_llamada2.Dock = DockStyle.Fill;
                    panel_llamada2.Visible = true;
                    combox_monto.SelectedIndex = -1;
                    combox_prov.SelectedIndex = -1; 
                }
                else if (balance < monto || cuentatipo != 2)
                {
                    MessageBox.Show("No tiene suficientes fondos para retirar de su Cuenta Ahorros");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una opcion");
            }
        }

        private void LlamadaBtn2_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
        }

        private void Combox_prov_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (combox_prov.Text)
            {
                case "Altice":
                    proveedor = 1;
                    break;
                case "Claro":
                    proveedor = 2;
                    break;
                case "Viva":
                    proveedor = 3;
                    break;
            }
        }

        private void Retiro_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            bienvenida.Text = "      Seleccione un monto";
            panel2.Width = 800;
            panel2.Height = 379;
            panel2.Dock = DockStyle.Fill;
            panel2.Visible = true;
        }

        private void RetiroBtnBack_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
        }

        private void Retiro(int money)
        {
            if (Cajero.policia == false)
            {
                string balanceCMD = string.Format("SELECT persona.balance, persona.tipo_cuenta, limite.lastcon FROM persona, limite WHERE persona.nro_tarjeta = '{0}' AND limite.nro_tarjeta = '{0}';", tarjeta);
                DataSet ds = Conexion.Ejecutar(balanceCMD);
                int balance = int.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                int cuentatipo = int.Parse(ds.Tables[0].Rows[0]["tipo_cuenta"].ToString());
                DateTime lastcon = Convert.ToDateTime(ds.Tables[0].Rows[0]["lastcon"].ToString());

                if (retirolimit <= 0 && retirolimit < 100)
                {
                    string CMD = string.Format("UPDATE limite, persona SET limite.lastcon = '{1}' WHERE persona.nro_tarjeta = '{0}' AND limite.nro_tarjeta = '{0}';", tarjeta, fecha.ToString("yyyy-MM-dd-HH:mm:ss"));
                    ds = Conexion.Ejecutar(CMD);
                }
                if (fecha.Day - lastcon.Day != 0)
                {
                    if (balance >= money && retirolimit >= money || cuentatipo == 2 && retirolimit >= money)
                    {
                        string CMD = string.Format("UPDATE persona SET balance = (balance - {0}) WHERE nro_tarjeta = '{1}';", money, tarjeta);
                        ds = Conexion.Ejecutar(CMD);
                        retirolimit = retirolimit - money;
                        Ocultar_panels();
                        panel3.Width = 800;
                        panel3.Height = 379;
                        panel3.Dock = DockStyle.Fill;
                        panel3.Visible = true;
                    }
                    else if (balance < money && cuentatipo != 2)
                    {
                        MessageBox.Show("No tiene suficientes fondos para retirar de su Cuenta Ahorros");
                    }
                    else if (retirolimit < money)
                    {
                        MessageBox.Show("El retiro maximo por dia es de RD$50,000");
                    }
                }
                else
                {
                    MessageBox.Show("Ha alcanzado el retiro maximo diario (RD$50,000)");
                } 
            }
        }

        private void RetiroBtn1_Click(object sender, EventArgs e)
        {
            Retiro(100);
        }

        private void RetiroBtn2_Click(object sender, EventArgs e)
        {
            Retiro(200);
        }

        private void RetiroBtn3_Click(object sender, EventArgs e)
        {
            Retiro(500);
        }

        private void RetiroBtn4_Click(object sender, EventArgs e)
        {
            Retiro(1000);
        }

        private void RetiroBtnP_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            retiroPers.Width = 800;
            retiroPers.Height = 379;
            retiroPers.Dock = DockStyle.Fill;
            retiroPers.Visible = true;
            panel_line.Visible = true;
            bienvenida.Text = "       Retiro Personalizado";
        }

        private void RetiroBtnfast_Click(object sender, EventArgs e)
        {
            Retiro(1000);
        }

        private void RetiroNext_Click(object sender, EventArgs e)
        {
            if (Cajero.policia == false)
            {
                string balanceCMD = string.Format("SELECT persona.balance, persona.tipo_cuenta, limite.lastcon FROM persona, limite WHERE persona.nro_tarjeta = '{0}' AND limite.nro_tarjeta = '{0}';", tarjeta);
                DataSet ds = Conexion.Ejecutar(balanceCMD);
                int balance = int.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
                int cuentatipo = int.Parse(ds.Tables[0].Rows[0]["tipo_cuenta"].ToString());
                DateTime lastcon = Convert.ToDateTime(ds.Tables[0].Rows[0]["lastcon"].ToString());
                string p = retiroPersonalizado.Text;

                if (retirolimit <= 0 && retirolimit < 100)
                {
                    string CMD = string.Format("UPDATE limite, persona SET limite.lastcon = '{1}' WHERE persona.nro_tarjeta = '{0}' AND limite.nro_tarjeta = '{0}';", tarjeta, fecha.ToString("yyyy-MM-dd-HH:mm:ss"));
                    ds = Conexion.Ejecutar(CMD);
                }

                if (p != "")
                {
                    if (fecha.Day - lastcon.Day != 0)
                    {
                        int montop = int.Parse(retiroPersonalizado.Text.ToString().Trim());
                        retiroPersonalizado.Clear();
                        if (montop >= 100 && montop <= 10000 && balance >= montop && retirolimit >= montop || montop >= 100 && montop <= 10000 && cuentatipo == 2 && retirolimit >= montop)
                        {
                            string CMD = string.Format("UPDATE persona SET balance = (balance - {0}) WHERE nro_tarjeta = '{1}';", montop, tarjeta);
                            ds = Conexion.Ejecutar(CMD);
                            retirolimit = retirolimit - montop;
                            Ocultar_panels();
                            bienvenida.Text = "Retiro de Efectivo";
                            panel3.Width = 800;
                            panel3.Height = 379;
                            panel3.Dock = DockStyle.Fill;
                            panel3.Visible = true;
                        }
                        else if (montop < 100)
                        {
                            MessageBox.Show("No puede retirar menos de RD$100");
                        }
                        else if (montop > 10000)
                        {
                            MessageBox.Show("No puede retirar mas de RD$10,000 en una sola transaccion");
                        }
                        else if (balance < montop && cuentatipo != 2)
                        {
                            MessageBox.Show("No tiene suficientes fondos para retirar de su Cuenta Ahorros");
                        }
                        else if (retirolimit < montop || retirolimit <= 0)
                        {
                            MessageBox.Show("El retiro maximo por dia es de RD$50,000");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ha alcanzado el retiro maximo diario (RD$50,000)");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un monto");
                    retiroPersonalizado.Clear();
                } 
            }
        }

        private void RetiroNext2_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
        }

        private void RetiroBtnBack2_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            retiroPersonalizado.Clear();
            bienvenida.Text = "      Seleccione un monto";
            panel2.Width = 800;
            panel2.Height = 379;
            panel2.Dock = DockStyle.Fill;
            panel2.Visible = true;
        }
        private void Depositar_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            bienvenida.Text = "                 Depositar";
            panel_depositar.Width = 800;
            panel_depositar.Height = 379;
            panel_depositar.Dock = DockStyle.Fill;
            panel_depositar.Visible = true;
        }

        private void DepositarBtnBack_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
        }

        private void DepositBtnNext_Click(object sender, EventArgs e)
        {
            string p = DepositarBox.Text;            
            DepositarBox.Clear();
            if(p != "")
            {
                try
                {
                    int monto = int.Parse(p);
                    string CMD = string.Format("UPDATE persona SET balance = balance + {0} WHERE nro_tarjeta = '{1}';", monto, tarjeta);
                    DataSet ds = Conexion.Ejecutar(CMD);

                    CMD = string.Format("SELECT balance FROM persona WHERE nro_tarjeta = '{0}'", tarjeta);
                    ds = Conexion.Ejecutar(CMD);

                    string balance = ds.Tables[0].Rows[0]["balance"].ToString();
                    label__depositbalance.Text = "Su nuevo balance es: RD$" + balance;
                    Ocultar_panels();
                    panel_depositar2.Width = 800;
                    panel_depositar2.Height = 379;
                    panel_depositar2.Dock = DockStyle.Fill;
                    panel_depositar2.Visible = true;
                }
                catch(Exception y)
                {
                    MessageBox.Show("El error es: " + y);
                }
            }else
            {
                MessageBox.Show("Ingrese un monto");
            }
        }

        private void DepositBtnNext2_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            lastcon.Visible = true;
            bienvenida.Text = bienvenidamenu;
        }

        private async void Consultar_Click(object sender, EventArgs e)
        {
            Ocultar_panels();
            Task task = new Task(Wait);
            loadpanel.Width = 800;
            loadpanel.Height = 379;
            loadpanel.Dock = DockStyle.Fill;
            loadpanel.Visible = true;
            task.Start();
            await task;
            Ocultar_panels();
            bienvenida.Text = "      Consultar balance";
            panel_balance.Width = 800;
            panel_balance.Height = 379;
            panel_balance.Dock = DockStyle.Fill;
            panel_balance.Visible = true;
            string CMD = string.Format("SELECT balance FROM persona WHERE nro_tarjeta = '{0}';", tarjeta);
            DataSet ds = Conexion.Ejecutar(CMD);

            string balance = ds.Tables[0].Rows[0]["balance"].ToString().Trim();
            label_balance.Text = "Balance: RD$" + balance;
        }
        public void Wait()
        {            
            Thread.Sleep(500);
            
        }
    }
}
