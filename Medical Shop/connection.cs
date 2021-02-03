using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace Medical_Shop
{
    class connection
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;
        OleDbDataAdapter da;
        DataTable dt;
        DataSet ds = new DataSet();
        public connection()
        {
            con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + "\\medical.accdb";             
            con.Open();
        }
        public bool read(string str)
        {
            cmd = new OleDbCommand(str, con);
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                return true;
            }
            else
                return false;
        }
        public int getid(string qry)
        {
            cmd = new OleDbCommand(qry, con);
            int id = 0;
            if (!Convert.IsDBNull(cmd.ExecuteScalar()))
            {
                id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
            }
            else
                return 0;

        }
        public DataTable display(string qury)
        {
            cmd = new OleDbCommand(qury, con);
            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataSet Combo_data(string qury)
        {
            ds = new DataSet();
            cmd = new OleDbCommand(qury, con);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public bool iud(string query)
        {
            cmd = new OleDbCommand(query, con);
            cmd.ExecuteNonQuery();
            return true;
        }
        public DataSet report_display(string qury)
        {
            Report rpt = new Report();
            ds = new DataSet();
            cmd = new OleDbCommand(qury, con);
            da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
}
