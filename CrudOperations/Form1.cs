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
            komut.Parameters.Add("@urun_fiyat", SqlDbType.Float).Value = float.Parse(txtFiyat.Text);
            Crud.ESG(sql, komut);
            yenile();



        }
        

        void yenile()
        {
            dataGridView1.DataSource = Crud.Liste("Select * From urunler");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bilgisayarTeknolojileriDataSet.urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.bilgisayarTeknolojileriDataSet.urunler);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            //string sql = "UPDATE urunler SET (urun_ıd, satici_id, urun_ad, urun_fiyat, urun_detay) VALUES('" + txtUrunID.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', @urun_fiyat, '" + textBox5.Text + "')";
            //komut = new SqlCommand();
            //komut.Parameters.Add("@urun_fiyat", SqlDbType.Float).Value = float.Parse(txtFiyat.Text);
            //Crud.ESG(sql, komut);

            yenile();
        }

        private void button4_Click(object sender, EventArgs e)

        {
            foreach(Control item in this.panel1.Controls)
                if(item is TextBox)
                {
                    item.Text = "";
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Crud.Liste("SELECT * FROM urunler WHERE urun_ad Like '%" + txtAra.Text + "%'");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUrunID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtFiyat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Kaydı Silmek İstiyor Musunuz ?", "Uyarı", MessageBoxButtons.YesNo);

            if (sonuc == DialogResult.Yes)
            {
                string sql = "DELETE FROM urunler WHERE urun_ıd = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                komut = new SqlCommand();
               Crud.ESG(sql, komut);
                yenile();
            }
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
