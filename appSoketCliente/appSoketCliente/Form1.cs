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
using System.Threading;

namespace appSoketCliente
{
    public partial class Form1 : Form   
    {
        //definicion del soket 
        Socket servidor;
        Thread hiloCliente;
        delegate void digMostrarMensaje(String msj);
        public Form1()
        {
            InitializeComponent();
        }
        void imprimirMsj(string msj)
        {
            if (this.InvokeRequired)
            {
                digMostrarMensaje de = new digMostrarMensaje(imprimirMsj);
                object[] param = new object[] { msj };
                this.Invoke(de, param);
            }
            else
            {
                txtEvento.AppendText(msj);
            }

        }

        void iniciarHilo()
        {
            ThreadStart delegado1 = new ThreadStart(conectarServidor);
            hiloCliente = new Thread(delegado1);
            hiloCliente.Start();
        }
        void conectarServidor()
        {
            //creamos la instacia del socket de  comunicacion con el servidor 
            servidor = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            //definoir la dirccion ip de comunicacion con el servidor
            IPEndPoint address = new IPEndPoint(IPAddress.Parse("192.168.137.151"), 2012);

            //conectar con el servidor 
            imprimirMsj("Solicitando coneccion con el servidor"+ Environment.NewLine);

            servidor.Connect(address);

            imprimirMsj("conexion establecida....." + Environment.NewLine);
            //Resivir mensajes 
            while (true)
            {
                byte[] bytesFromServe = new byte[1024];
                int tamanio = servidor.Receive(bytesFromServe);
                string msj = System.Text.Encoding.UTF8.GetString(bytesFromServe, 0, tamanio);
                imprimirMsj("Mensaje del servido "+ msj +Environment.NewLine);
            }
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
            iniciarHilo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            enviarMensaje();
        }

        private void txtSmj_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
