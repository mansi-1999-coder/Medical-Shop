using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Medical_Shop
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void salseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salse_entry i = new Salse_entry();
          //  i.MdiParent = this;
            i.Show();
          
        }

        private void itemEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item_Entry i = new Item_Entry();
            i.Show();
       
        }

        private void customerEntryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Customer_detail i = new Customer_detail();
            i.Show();
           // i.MdiParent = this;
        }

        private void purshaseEntryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Purchse_Entry p = new Purchse_Entry();
            p.Show();
           // p.MdiParent = this;
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void customerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void purchaseEntryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchase_entry_report i = new Purchase_entry_report();
            i.Show();
            //i.MdiParent = this;
        }

        private void salseEntryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salse_entry_report i = new Salse_entry_report();
            i.Show();
           // i.MdiParent = this;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            new login().Close();
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe");
        }

        private void woedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("wordpad.exe");
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("do you want to Exit", "configration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();

            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
           
                System.Windows.Forms.Application.Exit();

            
        }

        private void stockReprtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Stock_Report().Show();
        }

       
    }
}
