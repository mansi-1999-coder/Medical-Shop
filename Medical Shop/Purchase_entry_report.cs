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
    public partial class Purchase_entry_report : Form
    {
        DataTable dt = new DataTable();
        connection con = new connection();
        DataSet ds = new DataSet();
        float firstWidth;
        float firstHeight;
        public Purchase_entry_report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string qry = "select * from Purchase_item";
            Report r = new Report();
            if (rd_no.Checked == true)
            {
                qry = "select * from Purchase_item where bill_no=" + combo_no.Text + "";
            }
            else if (rd_pur_date.Checked == true)
            {
                qry = "select * from Purchase_item where purchase_date between #" + Date_from.Value.Date.ToShortDateString() + "# and #" + Date_to.Value.Date.ToShortDateString() + "#";
            }
            else if (rd_my.Checked == true)
            {
                if (cm.Text != "--Month--" && cy.Text != "--Year--")
                    qry = "select * from Purchase_item where MONTH(purchase_date)=" + cm.SelectedIndex + " AND YEAR(purchase_date)=" + cy.Text + "";
            }

            if (rd_no.Checked == true)
            {
                Rept_pur_bill pur = new Rept_pur_bill();
                ds = con.Combo_data(qry);
                pur.SetDataSource(ds.Tables[0]);
                r.crystalReportViewer1.ReportSource = pur;
            }
            else
            {
                Rept_pur_all Rept_Stock1 = new Rept_pur_all();
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

        private void Purchase_entry_report_Load(object sender, EventArgs e)
        {
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height; 
            combo_no.Visible = false;
            ds = new DataSet();
            ds = con.Combo_data("select * from purchse_entry");
            combo_no.DataSource = ds.Tables[0];
            combo_no.DisplayMember = "bill_no";
        }

        private void rd_pur_date_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_pur_date.Checked == true)
            {
                Date_from.Visible = true;
                Date_to.Visible = true;
                label3.Visible = true;
            }
            else
            {
                Date_from.Visible = false;
                Date_to.Visible = false;
                label3.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cm.SelectedIndex = 0;
            cy.Text = "--Year--";
            if (rd_my.Checked == true)
            {
                cm.Visible = true;
                cy.Visible = true;
            }
            else
            {
                cm.Visible = false;
                cy.Visible = false;
            }
        }

        private void Purchase_entry_report_SizeChanged(object sender, EventArgs e)
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
    }
}
