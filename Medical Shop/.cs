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
    public partial class Salse_entry_report : Form
    {
        DataTable dt = new DataTable();
        connection con = new connection();
        DataSet ds = new DataSet();
        public Salse_entry_report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            string qry = "";
          
            if (rd_no.Checked == true)
            {
                qry = "select *,sales_entry.bill_no,sales_entry.cust_name,sales_entry.salse_date,sales_entry.total,sales_item.item_id,sales_item.item_name,sales_item.qty,sales_item.price,sales_item.total FROM sales_entry,sales_item where sales_entry.bill_no=" + combo_no.Text + " AND sales_item.bill_no=" + combo_no.Text + "";
            }
            else if (rd_nm.Checked == true)
            {
                qry = "select sales_entry.bill_no,sales_entry.cust_name,sales_entry.salse_date,sales_entry.total,sales_item.item_id,sales_item.item_name,sales_item.qty,sales_item.price,sales_item.total FROM sales_entry INNER JOIN sales_item On sales_entry.bill_no=sales_item.bill_no where sales_entry.cust_name='"+combo_nm.Text+"'";
            }
            else if (rd_salse.Checked == true)
            {
                qry= "select sales_entry.bill_no,sales_entry.cust_name,sales_entry.salse_date,sales_entry.total,sales_item.item_id,sales_item.item_name,sales_item.qty,sales_item.price,sales_item.total FROM sales_entry INNER JOIN sales_item On sales_entry.bill_no=sales_item.bill_no where sales_entry.salse_date=#" + date_salse.Value.ToShortDateString() + "#";
            }
            else
            {
                qry = "select sales_entry.bill_no,sales_entry.cust_name,sales_entry.salse_date,sales_entry.total,sales_item.item_id,sales_item.item_name,sales_item.qty,sales_item.price,sales_item.total FROM sales_entry INNER JOIN sales_item On sales_entry.bill_no=sales_item.bill_no";
            }
            if (rd_no.Checked == true)
            {
                Rept_salse_bill pur = new Rept_salse_bill();
                ds = con.Combo_data(qry);
                pur.SetDataSource(ds.Tables[0]);
                r.crystalReportViewer1.ReportSource = pur;
            }
            else
            {
                Rept_salse_all Rept_Stock1 = new Rept_salse_all();
                ds = con.Combo_data(qry);
                Rept_Stock1.SetDataSource(ds.Tables[0]);
                r.crystalReportViewer1.ReportSource = Rept_Stock1;
            }
            r.crystalReportViewer1.RefreshReport();
            r.ShowDialog();

        }

        private void rd_no_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_no.Checked == true)
                combo_no.Visible = true;
            else
                combo_no.Visible = false;
        }

        private void Salse_entry_report_Load(object sender, EventArgs e)
        {
            ds = con.Combo_data("select * from sales_entry");
            combo_no.DataSource = ds.Tables[0];
            combo_no.DisplayMember = "bill_no";
            ds = con.Combo_data("select * from Customer ");
            combo_nm.DataSource = ds.Tables[0];
            combo_nm.DisplayMember = "Cust_name";
        }

        private void rd_nm_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_nm.Checked == true)
                combo_nm.Visible = true;
            else
                combo_nm.Visible = false;
        }

        private void rs_salse_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_salse.Checked == true)
                date_salse.Visible = true;
            else
                date_salse.Visible = false;
        }
    }
}
