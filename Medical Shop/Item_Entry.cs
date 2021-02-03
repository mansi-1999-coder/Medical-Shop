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
    public partial class Item_Entry : Form
    {
        connection con = new connection();
        float firstWidth;
        float firstHeight;
        int id, s;
        string status = "Activate";
        public Item_Entry()
        {
            InitializeComponent();
        }
        public void disable()
        {
            txt_hsncd.Enabled = true;
            txt_batch.Enabled = true;
            txt_Itemname.Enabled = true;
            expiry_date.Enabled = true;
       
            txt_rate.Enabled = true;
        }
        public void visible()
        {
            txt_hsncd.Enabled = false;
            txt_batch.Enabled = false;
            txt_Itemname.Enabled = false;
            expiry_date.Enabled = false;
            txt_rate.Enabled = false;
        }
        public void clear()
        {
            txt_hsncd.Clear();
            txt_batch.Clear();
            txt_Itemname.Clear();
            txt_rate.Clear();
            txt_Item_id.Clear();
            expiry_date.Value = DateTime.Today;
        }
        public void display_data()
        {
            dataGridView1.DataSource = con.display("select * from Item_master");
        }
       

        private void btn_add_Click(object sender, EventArgs e)
        {
            btn_add.Enabled = false;
            disable();
            id = con.getid("select MAX(Item_id) from Item_master");
            id++;
            txt_Item_id.Text = Convert.ToString(id);
            txt_Itemname.Focus();
            btn_save.Enabled = true;
            btn_can.Enabled = true;
            display_data();
            btn_can.Enabled = true;
            s = 0;
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
      
        private void btn_save_Click(object sender, EventArgs e)
        {
            
            if (txt_Itemname.Text == "" || txt_hsncd.Text == "" || txt_batch.Text == "" ||  txt_rate.Text == "" || expiry_date.Text == "")
            {
                MessageBox.Show("Fill all Data", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                btn_save.Enabled = true;
            }
            else
            {
                MessageBoxButtons btn = MessageBoxButtons.YesNo;
                DialogResult re = MessageBox.Show("do you want to Save", "Warning", btn,MessageBoxIcon.Question);
                {
                    if (re == DialogResult.Yes)
                    {
                        if (radio_Deactivate.Checked == true)
                            status = "Deactivate";
                        if (s == 0)
                        {
                            con.iud("insert into Item_master values(" + txt_Item_id.Text + ",'" + txt_Itemname.Text + "','" + txt_hsncd.Text + "','" + txt_batch.Text + "','" + expiry_date.Text + "',0 ," + txt_rate.Text + ",'"+status+"')");
                           
                        }
                        else
                        {
                            con.iud("update Item_master set Item_name='" + txt_Itemname.Text + "',hsncode='" + txt_hsncd.Text + "',batchno='" + txt_batch.Text + "',Expiry_date='" + expiry_date.Text + "',rate=" + txt_rate.Text + ",status='" + status + "' where Item_id=" + txt_Item_id.Text + "");
                           
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

        private void txt_rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
        private void Item_Entry_Load(object sender, EventArgs e)
        {
            display_data();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
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
            DialogResult re = MessageBox.Show("do you want to Delete Record ?", "Warning", btn,MessageBoxIcon.Question);
            {
                if (re == DialogResult.Yes)
                {

                    con.iud("delete * from Item_master where Item_id=" + txt_Item_id.Text + "");
                    display_data();
                    clear();
                }
            }
            btn_add.Enabled = true;
            btn_can.Enabled = false;
            btn_save.Enabled = false;
            visible();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Lab_date.Text = DateTime.Now.Date.ToShortDateString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_Item_id.Text = row.Cells["Item_id"].Value.ToString();
                txt_Itemname.Text = row.Cells["Item_name"].Value.ToString();
                txt_hsncd.Text = row.Cells["hsncode"].Value.ToString(); ;
                txt_batch.Text = row.Cells["batchno"].Value.ToString(); ;
                expiry_date.Text = row.Cells["Expiry_date"].Value.ToString();
                txt_rate.Text = row.Cells["rate"].Value.ToString();
                status = row.Cells["status"].Value.ToString();
                if (status == "Activate")
                    radio_Activate.Checked = true;
                else
                    radio_Deactivate.Checked = true;

            }
           
            btn_update.Enabled = true;
            visible();
            btn_add.Enabled = false;
            btn_save.Enabled = false;
            btn_can.Enabled = true;
            btn_del.Enabled = true;
            btn_can.Enabled = true;
        }

        private void Item_Entry_SizeChanged(object sender, EventArgs e)
        {
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            foreach (Control control in this.Controls)
            {
                try
                {
                    control.Font = new Font(control.Font.FontFamily, control.Font.Size * ((size1 + size2) / 2));

                    control.Scale(scale);
                }
                catch { }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
