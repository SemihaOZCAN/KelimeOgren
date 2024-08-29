using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Kelime_Ogren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbSozluk;Integrated Security=True");
        Random rast = new Random();
        int sure = 90;
        int kelime = 0;
        void getir()
        {
            int sayi = rast.Next(1, 101);

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TblSozluk WHERE ID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                txtIngilizce.Text = dr[1].ToString(); // İngilizce kelimenin bulunduğu kolon
                lblCevap.Text = dr[2].ToString(); // Türkçe kelimenin bulunduğu kolon
                lblCevap.Text = lblCevap.Text.ToLower(); // Küçük harfe çevir



                // Rastgele bir arka plan rengi seç
                this.BackColor = Color.FromArgb(rast.Next(256), rast.Next(256), rast.Next(256));
            }
            else
            {
                
                MessageBox.Show("Veritabanında kayıt bulunamadı.");
            }

            baglanti.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Timer interval ayarı 1 saniye
            timer1.Interval = 1000;
            timer1.Start();
            txtTurkce.Focus();
            getir();

        }

        private void txtTurkce_TextChanged(object sender, EventArgs e)
        {
            if (txtTurkce.Text == lblCevap.Text)
            {
                kelime++;
                labelKelime.Text = kelime.ToString();
                getir();
                txtTurkce.Clear();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            labelSure.Text = sure.ToString();
            if (sure <= 0)
            {
                timer1.Stop();
                txtTurkce.Enabled = false;
                txtIngilizce.Enabled = false;
                MessageBox.Show("Süre doldu!");
            }
        }
    }
}
