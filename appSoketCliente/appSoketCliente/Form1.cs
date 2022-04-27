using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace appSoketCliente
{
    public partial class Form1 : Form   
    {
        //definicion del soket 
        Socket servidor;
        public Form1()
        {
            InitializeComponent();
        }
        void conectarServidor()
        {
            //creamos la instacia del socket de  comunicacion con el servidor 
            servidor = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            //definoir la dirccion ip de comunicacion con el servidor
            IPEndPoint address = new IPEndPoint(IPAddress.Parse("192.168.137.215"), 2012);

            //conectar con el servidor 
            txtEvento.AppendText("Solicitando coneccion con el servidor"+ Environment.NewLine);

            servidor.Connect(address);

            txtEvento.AppendText("conexion establecida....." + Environment.NewLine);

        }
        void enviarMensaje()
        {
            byte[] bytesToServer = new byte[1024];
            bytesToServer = System.Text.Encoding.UTF8.GetBytes(txtSmj.Text);
            servidor.Send(bytesToServer);
            txtEvento.AppendText("mensaje enviado al servidor"+txtSmj.Text+Environment.NewLine);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectarServidor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            enviarMensaje();
        }
    }
}
