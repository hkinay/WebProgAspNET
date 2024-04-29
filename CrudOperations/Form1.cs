using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudOperations
{
    public partial class Form1 : Form
    {
        private SqlCommand komut;

        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO urunler (urun_ıd, satici_id, urun_ad, urun_fiyat, urun_detay) VALUES('"+ txtUrunID.Text + "', '"+textBox2.Text+"', '"+textBox3.Text +"', @urun_fiyat, '"+textBox5.Text+"')";
            komut = new SqlCommand();
            komut.Parameters.Add("@urun_fiyat", SqlDbType.Float).Value = float.Parse(textBox4.Text);
            Crud.ESG(sql, komut);
            yenile();



        }
        

        void yenile()
        {
            dataGridView1.DataSource = Crud.Liste("Select * From urunler");
        }
    }

    internal class Veritabani
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BilgisayarTeknolojileri;Integrated Security=True");
    }

    internal class Crud
    {
        public static bool ESG(string sql, SqlCommand command)
        {
            if(Veritabani.baglanti.State==System.Data.ConnectionState.Closed)
            {
                Veritabani.baglanti.Open();
            }
            command.Connection = Veritabani.baglanti;
            command.CommandText = sql;
            command.ExecuteNonQuery();


            try
            {
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Veritabani.baglanti.Close();
            }
        }

        public static DataTable Liste(string sql)
        {
            DataTable tbl = new DataTable();

            SqlDataAdapter art = new SqlDataAdapter(sql, Veritabani.baglanti);
            art.Fill(tbl);

            return tbl;



        }


    }



    

}
