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
    public partial class Stock_Report : Form
    {
        DataSet ds = new DataSet();
        connection con = new connection();
        float firstWidth;
        float firstHeight;
        public Stock_Report()
        {
            InitializeComponent();
        }

        private void Stock_Report_Load(object sender, EventArgs e)
        {
            combo_no.Visible = false;
            ds = new DataSet();
            ds = con.Combo_data("select * from Item_master where status='Activate'");
            combo_no.DataSource = ds.Tables[0];
            combo_no.DisplayMember = "Item_name";
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string qry = "select * from Item_master";
            if (rd_no.Checked == true)
                qry = "select * from Item_master where Item_name='"+combo_no.Text+"'";
            ds = con.Combo_data(qry);
            Rept_Stock Rept_Stock1 = new Rept_Stock();
            Rept_Stock1.SetDataSource(ds.Tables[0]);
            Report r = new Report();
            
            r.crystalReportViewer1.ReportSource = Rept_Stock1;
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

        private void Stock_Report_SizeChanged(object sender, EventArgs e)
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
