using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;

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
            if (!checkIdPass())
            {
                return;
            }
            progressBar1.Value = 0;
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private bool checkIdPass()
        {
            bool flag = true;
            if ("".Equals(mb_id.Text))
            {
                flag = false;
                MessageBox.Show("로그인 아이디를 입력해주세요. ");
            }
            if ((!checkBox1.Checked) && "".Equals(mb_password.Text))
            {
                flag = false;
                MessageBox.Show("로그인 패스워드를 입력해주세요. ");
            }
            return flag;
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var client = new HttpHandler())
            {

                var values = new NameValueCollection
                    {
                        { "mb_id", mb_id.Text },
                        { "mb_password", checkBox1.Checked? mb_id.Text : mb_password.Text },
                    };
                // Authenticate
                client.UploadValues("http://ausung.or.kr/bbs/login_check.php", values);
                // query page
                String url = "";
                if (checkBox2.Checked)
                {
                    Random rand = new Random();

                    for (int i = 1; i <= 23; i++)
                    {
                        System.Threading.Thread.Sleep(rand.Next(20, 120) * 1000);
                        url = "http://ausung.or.kr/menu05/rate2.php?code=auedu_7&gisu=1417166186&cat1=" + i;
                        client.DownloadString(url);
                        backgroundWorker1.ReportProgress(i);
                    }
                }
                else
                {
                    for (int i = 1; i <= 23; i++)
                    {
                        url = "http://ausung.or.kr/menu05/rate2.php?code=auedu_7&gisu=1417166186&cat1=" + i;
                        client.DownloadString(url);
                        backgroundWorker1.ReportProgress(i);
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage; 
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("작업 완료");
            button1.Enabled = true ;
            Process.Start("http://ausung.or.kr");
        }
    }
}
