using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medical_Shop
{
    public partial class login : Form
    {
        connection con = new connection();
        float firstWidth;
        float firstHeight;
        public login()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Lab_date.Text = DateTime.Now.Date.ToShortDateString(); 
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_pass.Text != "" && txt_unm.Text != "")
            {
                string qur = "select * from login where usernm='" + txt_unm.Text + "' AND password='" + txt_pass.Text + "'";
                if (con.read(qur) == true)
                {
                    MessageBox.Show(" You are now Logged In! ", " Welcome! ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
                    Dashboard dashboard = new Dashboard();
                    dashboard.WindowState = FormWindowState.Maximized;
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    txt_pass.Clear();
                    txt_pass.Focus();
                    MessageBox.Show("Incorrect Username/Password. Login Denied ", " Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter Username/Password. ", " Warning! ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("do you want to Exit", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_forget_Click(object sender, EventArgs e)
        {
            Forget_password fo = new Forget_password();
            fo.Show();
        }

        private void login_SizeChanged(object sender, EventArgs e)
        {
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            foreach (Control control in this.Controls)
            {

                control.Font = new Font(control.Font.FontFamily, control.Font.Size * ((size1 + size2) / 2));

                control.Scale(scale);

            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }
    }
}
