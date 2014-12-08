using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace TheDoWithMe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpHandler())
            {
                var values = new NameValueCollection
                    {
                        { "mb_id", "rnb0044" },
                        { "mb_password", "rnb0044" },
                    };
                // Authenticate
                client.UploadValues("http://ausung.or.kr/bbs/login_check.php", values);
                // Download desired page
                String body = client.DownloadString("http://ausung.or.kr/menu05/rate2.php?code=auedu_7&gisu=1417166186&cat1=23");
                
                MessageBox.Show(body);
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mb_password.Visible = false;
                label2.Visible = false;
                mb_password.Text = "";
            }
            else
            {
                mb_password.Visible = true;
                label2.Visible = true;
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 help = new Form2();
            help.ShowDialog();

        }
    }
}
