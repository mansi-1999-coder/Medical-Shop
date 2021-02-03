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
    public partial class Customer_detail : Form
    {
        connection con = new connection();
        float firstWidth;
        float firstHeight;
        int id, c = 0, s;
        string status = "Activate";
        public Customer_detail()
        {
            InitializeComponent();
        }
        public void disable()
        {
            txt_add.Enabled = true;
            txt_name.Enabled = true;
            txt_no.Enabled = true;
        }
        public void visible()
        {
            txt_add.Enabled = false;
            txt_name.Enabled = false;
            txt_no.Enabled = false;
        }
        public void clear()
        {
            txt_add.Clear();
            txt_name.Clear();
            txt_no.Clear();
        }
        public void display_data()
        {
            dataGridView1.DataSource = con.display("select * from Customer");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Lab_date.Text = DateTime.Now.Date.ToShortDateString();
        }

        private void txt_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Customer_detail_Load(object sender, EventArgs e)
        {
            display_data();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            btn_add.Enabled = false;
            disable();
            id = con.getid("select MAX(cust_id) from Customer");
            id++;
            txt_cust_id.Text = Convert.ToString(id);
            txt_name.Focus();
            btn_save.Enabled = true;
            btn_can.Enabled = true;
            display_data();
            btn_can.Enabled = true;
            s = 0;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "" || txt_no.Text == "" || txt_add.Text == "")
            {
                MessageBox.Show("Fill all Data", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                btn_save.Enabled = true;
            }
            else
            {
                MessageBoxButtons btn = MessageBoxButtons.YesNo;
                DialogResult re = MessageBox.Show("do you want to Save", "Warning", btn, MessageBoxIcon.Question);
                {
                    if (re == DialogResult.Yes)
                    {
                        if (radio_Deactivate.Checked == true)
                            status = "Deactivate";
                        if (s == 0)
                        {
                            con.iud("insert into Customer values(" + txt_cust_id.Text + ",'" + txt_name.Text + "','" + txt_add.Text + "','" + txt_no.Text + "','"+status+"')");
                        }
                        else
                        {
                            con.iud("update Customer set Cust_name='" + txt_name.Text + "',address='" + txt_add.Text + "',mobile_no='" + txt_no.Text + "',status='" + status + "' where cust_id=" + txt_cust_id.Text + "");
                        }

                        clear();

                    }
                    btn_can.Enabled = false;
                    btn_save.Enabled = false;
                    btn_add.Enabled = true;
                    visible();
                    display_data();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_cust_id.Text = row.Cells[0].Value.ToString();
                txt_name.Text = row.Cells[1].Value.ToString();
                txt_add.Text = row.Cells[2].Value.ToString(); ;
                txt_no.Text = row.Cells[3].Value.ToString();
                status = row.Cells[4].Value.ToString();
                if (status == "Activate")
                    radio_Activate.Checked = true;
                else
                    radio_Deactivate.Checked = true;
            }


            btn_can.Enabled = true;
            btn_update.Enabled = true;
            visible();
            btn_del.Enabled = true;
            btn_add.Enabled = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            s = 1;
            btn_update.Enabled = false;
            btn_save.Enabled = true;
            btn_can.Enabled = true;
            btn_add.Enabled = false;
            btn_del.Enabled = false;
            disable();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.YesNo;
            DialogResult re = MessageBox.Show("do you want to Delete Record ?", "Warning", btn, MessageBoxIcon.Question);
            {
                if (re == DialogResult.Yes)
                {

                    con.iud("delete * from Customer where cust_id=" + txt_cust_id.Text + "");
                    display_data();
                    clear();
                }
            }
            btn_add.Enabled = true;
            btn_can.Enabled = false;
            btn_save.Enabled = false;
            visible();
        }

        private void Customer_detail_SizeChanged(object sender, EventArgs e)
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

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("do you want to Exit", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
            clear();
            btn_can.Enabled = false;
            btn_add.Enabled = true;
            btn_save.Enabled = false;
            btn_del.Enabled = false;
            btn_update.Enabled = false;
        }
    }
}
