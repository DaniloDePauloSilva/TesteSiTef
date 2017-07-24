using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SitefJS.Srv;


namespace SitefJS
{
    public partial class Principal : Form
    {
        BackgroundWorker bw;

        public Principal()
        {
            InitializeComponent();

            ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Sair do Programa", Sair),
                new MenuItem("Restaurar", Restaurar)
            });

            icone.ContextMenu = ContextMenu;

            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
        }

        private void bw_DoWork(object source, DoWorkEventArgs e)
        {
            SrvJs.iniciarListener(new string[] { "http://127.0.0.1:8080/" });
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            icone.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            

            bw.RunWorkerAsync();
           
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            icone.Visible = true;
        }

        void Sair(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void Restaurar(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
        }


    }
}
