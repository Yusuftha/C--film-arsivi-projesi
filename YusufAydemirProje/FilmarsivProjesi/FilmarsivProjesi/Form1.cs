using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FilmarsivProjesi
{
    public partial class Film_Arsiv : Form
    {
        public Film_Arsiv()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataAdapter da;

        void filmlistele()
        {
            baglanti = new OleDbConnection ("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=FilmArsivDB.accdb");
            baglanti.Open();
            da = new OleDbDataAdapter("Select * from Filmler order by filmid", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();

        }
        void alicilistele()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=FilmArsivDB.accdb");
            baglanti.Open();
            da = new OleDbDataAdapter("Select * from Alıcılar order by Alici_id", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView3.DataSource = tablo;
            baglanti.Close();

        }
        void stoklistele()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=FilmArsivDB.accdb");
            baglanti.Open();
            da = new OleDbDataAdapter("Select Filmid, Filmadi, Stok from Filmler order by Filmid", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView3.DataSource = tablo;
            baglanti.Close();
        }
        void arama()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=FilmArsivDB.accdb");
            baglanti.Open();
            DataTable tbl = new DataTable();
            OleDbDataAdapter ara = new OleDbDataAdapter("SELECT * FROM Filmler WHERE FilmAdi LIKE '%" + txtarat.Text + "%' ", baglanti);
            OleDbDataAdapter ara2 = new OleDbDataAdapter("SELECT * FROM Filmler WHERE Yonetmen LIKE '%" + txtarat.Text + "%' ", baglanti);
            ara.Fill(tbl);
            ara2.Fill(tbl);
            baglanti.Close();
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            dataGridView1.DataSource = tbl;
        }
            
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Film_Arsiv_Load(object sender, EventArgs e)
        {
            filmlistele();
            alicilistele();
            stoklistele();
            dataGridView1.Visible = false;          
        }

        private void film_kayit_Click(object sender, EventArgs e)
        {
            if (filmad.Text == ("") || tur.Text == ("") || sure.Text == ("") || yonetmen.Text == ("") || fiyat.Text == ("") || stokS.Text == (""))
            {
                DialogResult sonuc;
                sonuc = MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olun!", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string sorgu = "INSERT INTO Filmler(Filmadi,Tur,Sure,Yonetmen,Fiyat,Stok) values (@filmad,@tur,@sureS,@yonetmen,@fiyat,@stokS)";
                komut = new OleDbCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@filmad", filmad.Text);
                komut.Parameters.AddWithValue("@tur", tur.Text);
                komut.Parameters.AddWithValue("@sureS", sure.Text);
                komut.Parameters.AddWithValue("@yonetmen", yonetmen.Text);
                komut.Parameters.AddWithValue("@fiyat", fiyat.Text);
                komut.Parameters.AddWithValue("@stokS", stokS.Text);
                DialogResult sonuc;
                sonuc = MessageBox.Show("Kayıt islemi başarılı!", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                filmad.Text = "";
                tur.Text = "";
                sure.Text = "";
                yonetmen.Text = "";
                fiyat.Text = "";
                stokS.Text = "";
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                filmlistele();
            }
        }

        private void Film_liste_Click(object sender, EventArgs e)
        {
            filmlistele();
            panel1.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
        }

        private void Alici_kayit_Click(object sender, EventArgs e)
        {
            if (Ad.Text == ("") || yas.Text == ("") || sehir.Text == (""))
            {
                DialogResult sonuc;
                sonuc = MessageBox.Show("Lütfen tüm alanları doldurduğunuzdan emin olun!", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
    
            }
            else
            {
                string sorgu = "INSERT INTO Alıcılar(Adi_soyadi,Yas,Sehir) values (@ad,@yas,@sehir)";
                komut = new OleDbCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@ad", Ad.Text);
                komut.Parameters.AddWithValue("@yas", yas.Text);
                komut.Parameters.AddWithValue("@sehir", sehir.Text);
                DialogResult sonuc;
                sonuc = MessageBox.Show("Kayıt islemi başarılı!", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Ad.Text = "";
                yas.Text = "";
                sehir.Text = "";
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                alicilistele();
            }
        }

        private void Alici_liste_Click(object sender, EventArgs e)
        {
            alicilistele();
            panel2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView3.Visible = true;
        }

        private void film_guncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Filmler SET Filmadi=@fad,Tur=@tur,Sure=@sure,Yonetmen=@yonetmen,Fiyat=@fiyat,Stok=@stok WHERE Filmid=@id";
            komut = new OleDbCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@fad", filmad2.Text);
            komut.Parameters.AddWithValue("@tur", tur2.Text);
            komut.Parameters.AddWithValue("@sure", sure2.Text);
            komut.Parameters.AddWithValue("@yonetmen", yonetmen2.Text);
            komut.Parameters.AddWithValue("@fiyat", fiyat2.Text);
            komut.Parameters.AddWithValue("@stok", stok2.Text);
            komut.Parameters.AddWithValue("@id", filmid.Text);
            DialogResult sonuc;
            sonuc = MessageBox.Show("Güncelleme işlemi başarılı!", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            filmlistele();
            
        }
              

        private void arat_Click(object sender, EventArgs e)
        {
            arama();
        }

        private void dataGridView2_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            filmid.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            filmad2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            tur2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            sure2.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            yonetmen2.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            fiyat2.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            stok2.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            filmid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            filmad2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tur2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            sure2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            yonetmen2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            fiyat2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            stok2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void stok_Click(object sender, EventArgs e)
        {
            stoklistele();
            panel2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView3.Visible = true;
        }

        
                
    }
}
