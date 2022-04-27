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
namespace appSocketServidor1
{
    public partial class Form1 : Form
    {
        Socket servidor = null;
        Thread HiloServidor;
        delegate void delegadoEvwnto(string msj);
        public Form1()
        {
            InitializeComponent();
        }
        void imprimirMsj(string msj)
        {
            if(this.InvokeRequired)
            {
                delegadoEvwnto de = new delegadoEvwnto(imprimirMsj);
                object[] param =new object[]{ msj};
                this.Invoke(de, param);
            }
            else
            {
                txtEventos.AppendText(msj);
            }

        }

        void iniciarHilo()
        {
            ThreadStart delegado1 = new ThreadStart(iniciarConexion);
            HiloServidor = new Thread(delegado1);
            HiloServidor.Start();
        }
        void iniciarConexion()
        {
            //Creando la instacia del socket servidor
            servidor = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            //Ip del cliente a establecer comunicación
            IPEndPoint address = new IPEndPoint(IPAddress.Any, 2012);

            // asociar la ip al socket
            servidor.Bind(address);
            //Espera solicitud de conexión de un cliente
            imprimirMsj("Esperando solicitud de conexión del un cliente "
                + Environment.NewLine);
            servidor.Listen(1);
            //Acepta la conexión de un cliente
            Socket cliente=servidor.Accept();
            imprimirMsj("conexión establecida con cliente "
                + Environment.NewLine);

            //escuchar sms
            while(true)
            {
                byte[] bytesFromClient=new byte[1024];
                int tamanio = cliente.Receive(bytesFromClient);
                string msj = System.Text.Encoding.UTF8.GetString(bytesFromClient, 0, tamanio);
                imprimirMsj("mensaje del cliente"+msj+Environment.NewLine);

            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            iniciarHilo();
        }
    }
}
