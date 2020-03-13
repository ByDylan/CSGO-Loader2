using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using ManualMapInjection.Injection;

namespace AnonWare_Loadv2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.Sleep(2300);
            Auth.Handler.Initialize();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Auth.Handler.Login_Register_Redeem_With_Key(metroButton1.Text);

            Process target = Process.GetProcessesByName("csgo").FirstOrDefault();

            if (target == null)
            {
                var result = MessageBox.Show("Opening CSGO,Please Wait....", "AnonWare v2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(result == DialogResult.OK)
                {
                    Process.Start("steam://rungameid/730");
                }
                else
                {
                    Close();
                }
               
            }
            else
            {
                WebClient web = new WebClient();
                web.Proxy = null;

                web.Headers["User-Agent"] = "Mozilla";
                byte[] dll = web.DownloadData(Auth.Handler.GetVariable("coon"));
                var inject = new ManualMapInjector(target) { AsyncInjection = true };
                metroLabel1.Text = $"hmodule = 0x{inject.Inject(dll).ToInt64():x8}";
                Thread.Sleep(2300);
                MessageBox.Show("Injected!", "AnonWare v2");
                Close();
            }
        }
    }
}
