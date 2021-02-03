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
    public partial class Salse_entry : Form
    {
        connection con = new connection();
        DataTable dt;
        DataSet ds = new DataSet();
        float firstWidth;
        float firstHeight;
        int id, stock, billno;
        public Salse_entry()
        {
            InitializeComponent();
        }
        public void enable()
        {
            combo_cust.Enabled = true;
            combo_item.Enabled = true;
            txt_qty.Enabled = true;
            txt_rate.Enabled = true;
            Combo_type.Enabled = true;
        }
        public void disable()
        {
            combo_cust.Enabled = false;
            combo_item.Enabled = false;
            txt_qty.Enabled = false;
            txt_rate.Enabled = false;
            Combo_type.Enabled = false;
        }
        public void clear()
        {
          //  combo_cust.Enabled = true;
            combo_item.SelectedIndex = 1 ;
            txt_qty.Clear();
            txt_rate.Clear();
            Combo_type.Text = "cash";
        }
        public void display1()
        {
            dataGridView1.DataSource = con.display("select * from sales_item where bill_no=" + txt_bill_no.Text + "");
        }
        public void display2()
        {
               dataGridView2.DataSource = con.display("select *from sales_entry");
        }
        private void Salse_entry_Load(object sender, EventArgs e)
        {
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height; 
            id = con.getid("select MAX(bill_no) from sales_entry");
            txt_bill_no.Text = Convert.ToString(++id);
            date_salse.MaxDate = DateTime.Now;

            dt = new DataTable();
            dt = con.display("select * from Item_master");
            combo_item.DataSource = dt;
            combo_item.DisplayMember = "Item_name";
            combo_item.ValueMember = "Item_id";

            dt = con.display("select * from Customer");
            combo_cust.DataSource = dt;
            combo_cust.DisplayMember = "Cust_name";
            combo_cust.ValueMember = "cust_id";
            txt_item_id.Text = "1";

            display2();
            btn_add.Focus();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            enable();
            clear();
            id = con.getid("select MAX(bill_no) from sales_entry");
            txt_bill_no.Text = Convert.ToString(++id);
            dataGridView1.DataSource = "";
            combo_cust.Focus();
            btn_additem.Enabled = true;
            btn_save.Enabled = true;
            btn_can.Enabled = false;
        }
        private void combo_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = con.display("select * from Item_master where Item_name='" + combo_item.Text + "'");
            if (dt.Rows.Count > 0)
            {
                txt_item_id.Text = dt.Rows[0][0].ToString();
            }
        }
        private void combo_cust_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = con.display("select * from Customer where Cust_name='" + combo_cust.Text + "'");
            if (dt.Rows.Count > 0)
            {
                txt_no.Text = dt.Rows[0][3].ToString();
                txt_add.Text = dt.Rows[0][3].ToString();
            }
        }
        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void btn_additem_Click(object sender, EventArgs e)
        {
            stock = con.getid("select qty from Item_master where item_id=" + txt_item_id.Text + "");
            con.iud("insert into sales_item values(" + txt_bill_no.Text + "," + txt_item_id.Text + ",'" + combo_item.Text + "'," + txt_qty.Text + "," + txt_rate.Text + "," + txt_amt.Text + ",'" + Combo_type.Text + "')");
            display1();
            stock -= Convert.ToInt32(txt_qty.Text);
            con.iud("update Item_master set qty=" + stock + " where item_id=" + txt_item_id.Text + "");
            int tol = con.getid("select SUM(total) from sales_item where bill_no=" + txt_bill_no.Text + "");
            txt_total.Text = Convert.ToString(tol);
            clear();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (combo_cust.Text == "")
            {
                MessageBox.Show("Enter Name");
            }
            else
            {
                con.iud("insert into sales_entry values(" + txt_bill_no.Text + ",'" + combo_cust.Text + "','"+txt_add.Text+"'," + txt_no.Text + ",'" + date_salse.Value.ToShortTimeString() + "'," + txt_total.Text + ")");
                display2();
                MessageBox.Show("record Insert Sucessfully!!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            btn_save.Enabled = false;
            btn_can.Enabled = true;
            
        }

   

        private void txt_rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btn_can_Click(object sender, EventArgs e)
        {
            string qry = "";
            Report r = new Report();
            qry = "select sales_entry.bill_no,sales_entry.cust_name,sales_entry.salse_date,sales_entry.total,sales_item.item_id,sales_item.item_name,sales_item.qty,sales_item.price,sales_item.total FROM sales_entry,sales_item where sales_entry.bill_no=" + txt_bill_no.Text + " AND sales_item.bill_no=" + txt_bill_no.Text + "";
            Rept_salse_bill pur = new Rept_salse_bill();
            ds = con.Combo_data(qry);
            pur.SetDataSource(ds.Tables[0]);
            r.crystalReportViewer1.ReportSource = pur;
            r.crystalReportViewer1.RefreshReport();
            r.ShowDialog();
            disable();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.YesNo;
            DialogResult re = MessageBox.Show("do you want to Delete Record ?", "Warning", btn, MessageBoxIcon.Question);
            {
                if (re == DialogResult.Yes)
                {
                    stock = con.getid("select qty from Item_master where item_id=" + txt_item_id.Text + "");
                    stock += Convert.ToInt32(txt_qty.Text);
                    con.iud("update Item_master set qty=" + stock + " where item_id=" + txt_item_id.Text + "");
                    con.iud("delete from sales_item where item_id=" + txt_item_id.Text + " and bill_no=" + txt_bill_no.Text + "");
                    display1();
                    MessageBox.Show("record Deleted!!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    int tol = con.getid("select SUM(total) from sales_item where bill_no=" + txt_bill_no.Text + "");
                    txt_total.Text = Convert.ToString(tol);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_item_id.Text = row.Cells["item_id"].Value.ToString();
                combo_item.Text = row.Cells["item_name"].Value.ToString();
                txt_qty.Text = row.Cells["qty"].Value.ToString();
                txt_rate.Text = row.Cells["price"].Value.ToString();
                txt_total.Text = row.Cells["total"].Value.ToString();
                btn_remove.Enabled = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                txt_bill_no.Text = row.Cells["bill_no"].Value.ToString();
                txt_no.Text = row.Cells["cust_no"].Value.ToString();
                combo_cust.Text = row.Cells["cust_name"].Value.ToString();
                date_salse.Text = row.Cells["salse_date"].Value.ToString();
                txt_total.Text = row.Cells["total"].Value.ToString();
                billno = Convert.ToInt32(txt_bill_no.Text);
                btn_can.Enabled = true;
                btn_save.Enabled = false;
                dataGridView1.DataSource = con.display("select * from sales_item where bill_no=" + billno + " ");
            }
        }

      

        private void txt_qty_Leave(object sender, EventArgs e)
        {
            stock = con.getid("select qty from Item_master where item_id=" + txt_item_id.Text + "");
            int qty = Convert.ToInt32(txt_qty.Text.ToString());
            if (stock < qty)
            {
                MessageBox.Show("Opps !! Out of Stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_qty.Text = stock.ToString();
                txt_qty.Focus();
            }
            txt_rate.Focus();
        }

        private void txt_rate_Leave(object sender, EventArgs e)
        {
            int price = Convert.ToInt32(txt_rate.Text.ToString());
            int q = Convert.ToInt32(txt_qty.Text);
            txt_amt.Text = Convert.ToString(price * q);

        }

        private void Salse_entry_SizeChanged(object sender, EventArgs e)
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
            if (MessageBox.Show("do you want to Exit", "configration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();

            }
        }

        private void combo_item_Leave(object sender, EventArgs e)
        {
            txt_qty.Focus();
        }
    }
}
