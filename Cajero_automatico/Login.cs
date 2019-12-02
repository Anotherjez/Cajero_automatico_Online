using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
/*using CajeroConnection;*/
using CajeroConnectionOnline;
using System.Threading;
using MySql.Data;

namespace Cajero_automatico_Online
{
    public partial class Cajero : Form
    {
        public dynamic reporte = new List<Reporte>();
        public int cont_failed = 0;
        public static bool policia = false;
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public string file = @"\reporte_policial.txt";
        public string txt = "";
        public Cajero()
        {
            InitializeComponent();
            panel2.Location = new Point(0, 0);
            Load += new EventHandler(Cajero_Load);
            if (File.Exists(@path + file))
            {
                try 
                {
                    TextReader sr;
                    sr = new StreamReader(@path + file);
                    txt = sr.ReadToEnd();
                    sr.Close();
                }
                catch
                {
                    throw;
                }
            }

        }

        private void Cajero_Load(object sender, EventArgs e)
        {
            Ocultar_panels();
            
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void Entrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string pinr = PassBox.Text;
                if (pinr != "" && pinr.Length == 4)
                {
                    string pinr2 = "";
                    for (int i = pinr.Length - 1; i >= 0; i--)
                    {
                        pinr2 += Convert.ToString(pinr[i]);
                    }

                    string CMD = string.Format("SELECT login.nro_tarjeta, login.password, persona.estado, persona.nombre, persona.apellido FROM login, persona WHERE login.nro_tarjeta = persona.nro_tarjeta AND login.nro_tarjeta = '{0}' AND login.password = '{1}' OR login.password = '{2}' AND login.nro_tarjeta = persona.nro_tarjeta AND login.nro_tarjeta = '{0}';", tarjetaBox.Text.Trim(), PassBox.Text.Trim(), pinr2);
                    DataSet ds = Conexion.Ejecutar(CMD);
                    string tarjeta = ds.Tables[0].Rows[0]["nro_tarjeta"].ToString().Trim();
                    string password = ds.Tables[0].Rows[0]["password"].ToString().Trim();
                    string estado = ds.Tables[0].Rows[0]["estado"].ToString().Trim();
                    string nombre = ds.Tables[0].Rows[0]["nombre"].ToString().Trim();
                    string apellido = ds.Tables[0].Rows[0]["apellido"].ToString().Trim();

                    if (tarjeta == tarjetaBox.Text && password == PassBox.Text && estado == "1" || tarjeta == tarjetaBox.Text && password == pinr2 && estado == "1")
                    {
                        if (cont_failed < 3)
                        {
                            if (password == pinr2)
                            {
                                policia = true;
                                Reporte info = new Reporte
                                {
                                    Tarjeta = tarjeta,
                                    Nombre = nombre + " " + apellido,
                                    Date = DateTime.Now
                                };
                                reporte.Add(info);
                                StringBuilder txt2 = new StringBuilder();
                                txt2.AppendLine(txt);
                                txt2.Append(info.Date.ToString() + ": REPORTE de " + info.Nombre + " portador@ de la tarjeta " + info.Tarjeta + " esta siendo forzad@ en el cajero ubicado en Av. Winston Churchill.");
                                txt = txt2.ToString();
                                File.WriteAllText(path + file, txt);
                            }
                            Conexion.tarjeta = tarjeta;
                            this.Hide();
                            Menu menu = new Menu
                            {
                                StartPosition = FormStartPosition.Manual,
                                Location = new Point(this.ClientSize.Width, this.ClientSize.Height),
                                Top = this.Top,
                                Left = this.Left
                            };
                            menu.Show(this);
                        }
                        else if (cont_failed >= 3)
                        {
                            CMD = string.Format("UPDATE persona SET estado = 0 WHERE nro_tarjeta = '{0}'", tarjeta);
                            ds = Conexion.Ejecutar(CMD);
                        }
                    }
                    else if (tarjeta == tarjetaBox.Text && password == PassBox.Text && estado != "1")
                    {
                        MessageBox.Show("Su tarjeta se encuentra en estado retenido...");
                    }
                    else
                    {
                        cont_failed++;
                        panel_line2.BackColor = Color.Red;
                        MessageBox.Show("Pin incorrecto...");
                    }
                }else if(pinr.Length != 4)
                {
                    MessageBox.Show("Su pin deberia tener 4 digitos...");
                }
                else
                {
                    MessageBox.Show("Ingrese un pin...");
                }
            }
            catch (Exception error)
            {
                cont_failed++;
                MessageBox.Show("Ha ocurrido un error: " + error.ToString());
            }
        }

        private void Cajero_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtnNext_Click(object sender, EventArgs e)
        {
            string p = tarjetaBox.Text;
            if (p != "")
            {
                try
                {
                    string CMD = string.Format("SELECT estado FROM persona WHERE nro_tarjeta = '{0}';", p);
                    DataSet ds = Conexion.Ejecutar(CMD);

                    string estado = ds.Tables[0].Rows[0]["estado"].ToString();

                    if (estado == "1")
                    {
                        Ocultar_panels();
                        panel2.Width = 800;
                        panel2.Height = 450;
                        panel2.Dock = DockStyle.Fill;
                        panel2.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Su tarjeta se encuentra retenida");
                    }
                }
                catch (Exception error)
                {
                    if (error is MySql.Data.MySqlClient.MySqlException)
                    {
                        MessageBox.Show("No se pudo establecer una conexion con el servidor...");
                    }else if(error is IndexOutOfRangeException)
                    {
                        MessageBox.Show("Esta cuenta no existe...");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Ingrese una tarjeta...");
            }
        }
        public void Ocultar_panels()
        {
            panel2.Visible = false;
        }
        public class Reporte
        {
            public string Tarjeta { get; set; }
            public string Nombre { get; set; }
            public DateTime Date { get; set; }
        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
