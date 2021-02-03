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
    public partial class Purchse_Entry : Form
    {
        connection con = new connection();
        DataTable dt;
        int id, p, g, stock, billno;
        float firstWidth;
        float firstHeight;
        public Purchse_Entry()
        {
            InitializeComponent();
        }

        private void Purchse_Entry_Load(object sender, EventArgs e)
        {
            date_mrg.MaxDate = DateTime.Today.Date;
            expiry_date.MinDate = DateTime.Today.Date;
            id = con.getid("select MAX(bill_no) from purchse_entry");
            txt_bill_no.Text = Convert.ToString(++id);
            date_pur.MaxDate = DateTime.Now;
            dt = new DataTable();
            dt = con.display("select * from Item_master");
            combo_nm.DataSource = dt;
            combo_nm.DisplayMember = "Item_name";
            combo_nm.ValueMember = "Item_id";
            display2();
            txt_item_id.Text = "1";
            btn_add.Focus();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }
        public void enable()
        {
            combo_nm.Enabled = true;
            txt_gst.Enabled = true;
            txt_qty.Enabled = true;
            txt_rate.Enabled = true;
            txt_agency.Enabled = true;
            txxt_discount.Enabled = true;
            date_mrg.Enabled = true;
            expiry_date.Enabled=true;
        }
        public void disable()
        {
            combo_nm.Enabled = false;
            txt_gst.Enabled = false;
            txt_qty.Enabled = false;
            txt_rate.Enabled = false;
            txt_agency.Enabled = false;
            txxt_discount.Enabled = false;
            date_mrg.Enabled = false;
            expiry_date.Enabled = false;
        }
        public void clear()
        {
            combo_nm.SelectedIndex = 0;
            txt_gst.Text = "6";
            txt_item_id.Text = "1";
            expiry_date.Value=DateTime.Now.AddDays(+1);
            date_mrg.Value=DateTime.Now.AddDays(-1);
            txt_qty.Clear();
            txxt_discount.Text = "0";
            txt_rate.Clear();
        }
        public void display1()
        {
            dataGridView1.DataSource = con.display("select * from Purchase_item where bill_no="+txt_bill_no.Text+"");
        }
        public void display2()
        {
            dataGridView2.DataSource = con.display("select *from purchse_entry");
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            stock = con.getid("select qty from Item_master where Item_id=" + txt_item_id.Text + "");
            con.iud("insert into Purchase_item values(" + txt_bill_no.Text + ",'" + combo_nm.Text + "'," + txt_item_id.Text + ",'" + date_pur.Value.ToShortDateString() + "','" + date_mrg.Value.ToShortDateString() + "','" + expiry_date.Value.ToShortDateString() + "'," + txt_qty.Text + "," + txt_rate.Text + "," + txt_gst.Text + "," + txxt_discount.Text + "," + txt_amt.Text + ")");
            display1();
            stock += Convert.ToInt32(txt_qty.Text);
            con.iud("update Item_master set qty=" + stock + " where Item_id=" + txt_item_id.Text + "");
            int tol = con.getid("select SUM(amount) from Purchase_item where bill_no=" + txt_bill_no.Text + "");
            txt_total.Text = Convert.ToString(tol);
            clear();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            enable();
             clear();
             id = con.getid("select MAX(bill_no) from purchse_entry");
            txt_bill_no.Text = Convert.ToString(++id);
           
           
            dataGridView1.DataSource = "";
            txt_agency.Focus();

            btn_additem.Enabled = true;
            btn_save.Enabled = true;
            //btn_can.Enabled = true;
        }

        private void combo_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_item_id.Text = combo_nm.SelectedValue.ToString();
        }

   

        private void btn_remove_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.YesNo;
            DialogResult re = MessageBox.Show("do you want to Delete Record ?", "Warning", btn, MessageBoxIcon.Question);
            {
                if (re == DialogResult.Yes)
                {
                    con.iud("delete from Purchase_item where Item_id=" + txt_item_id.Text + " and bill_no=" + txt_bill_no.Text + "");
                    display1();
                    stock = con.getid("select qty from Item_master where Item_id=" + txt_item_id.Text + "");
                    stock = stock - Convert.ToInt32(txt_qty.Text);
                    con.iud("update Item_master set qty=" + stock + " where Item_id=" + txt_item_id.Text + "");

                    int tol = con.getid("select SUM(amount) from Purchase_item where bill_no=" + txt_bill_no.Text + "");
                    txt_total.Text = Convert.ToString(tol);
                }
            }
            btn_additem.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            con.iud("insert into purchse_entry values(" + txt_bill_no.Text + ",'" + txt_agency.Text + "','" + date_pur.Value.ToShortDateString() + "'," + txt_total.Text + ")");
            display2();
            MessageBox.Show("record Insert Sucessfully!!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            disable();
            clear();
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
            con.display("delete from purchse_entry where bill_no=" + txt_bill_no.Text + "");
            display2();

            con.display("delete from Purchase_item where bill_no=" + txt_bill_no.Text + "");
            display1();

            int tol = con.getid("select SUM(amount) from Purchase_item");
            txt_total.Text = Convert.ToString(tol);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_item_id.Text = row.Cells[2].Value.ToString();
                combo_nm.Text = row.Cells[1].Value.ToString();
                date_mrg.Text = row.Cells[4].Value.ToString();
                expiry_date.Text = row.Cells[5].Value.ToString();
                txt_qty.Text = row.Cells[6].Value.ToString();
                txt_rate.Text = row.Cells[7].Value.ToString();
                txt_gst.Text = row.Cells[8].Value.ToString();
                txxt_discount.Text = row.Cells[9].Value.ToString();
                txt_amt.Text = row.Cells[10].Value.ToString();
            }
            btn_remove.Enabled = true;
            btn_additem.Enabled = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                txt_bill_no.Text = row.Cells[0].Value.ToString();
                txt_agency.Text = row.Cells[1].Value.ToString();
                date_pur.Text = row.Cells[2].Value.ToString();
                txt_total.Text = row.Cells[3].Value.ToString();
                billno = Convert.ToInt32(txt_bill_no.Text);
            }
          //  btn_can.Enabled = true;
            dt = new DataTable();
            dataGridView1.DataSource = con.display("select * from Purchase_item where bill_no=" + billno + "");
        }

        private void txxt_discount_TextChanged(object sender, EventArgs e)
        {
            txt_amt.Text = Convert.ToString(Convert.ToInt32(txt_amt.Text) - Convert.ToInt32(txxt_discount.Text));
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Purchse_Entry_SizeChanged(object sender, EventArgs e)
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

        private void txt_gst_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("do you want to Exit", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }

        }

      

     
        private void txt_rate_Leave(object sender, EventArgs e)
        {
            int price = Convert.ToInt32(txt_rate.Text);
            int q = Convert.ToInt32(txt_qty.Text);
            txt_amt.Text = Convert.ToString(price * q);
        }

        private void txt_gst_Leave(object sender, EventArgs e)
        {
            p = Convert.ToInt32(txt_amt.Text);
            g = Convert.ToInt32(p * Convert.ToInt32(txt_gst.Text) / 100);
            txt_amt.Text = Convert.ToString(Convert.ToInt32(txt_amt.Text) + g);
        }
    }
}
