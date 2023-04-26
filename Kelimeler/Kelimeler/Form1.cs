using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kelimeler
{
    public partial class Kelime : Form
    {
     
      
        public Kelime()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        int sayac = 0;
        string id;
        string a;
        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "Doğru Cevap : 0";
            {
                con = new SqlConnection("server=.;Initial Catalog =Kelimeler;Integrated Security=SSPI");
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select top 1 id from Kelimeler.dbo.Sayfa1$ order by id desc";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label5.Text = "";
                    label5.Text += dr["id"];

                }
                dr.Close();

                con.Close();
            }
             a = Interaction.InputBox("Lütfen Bugünlük Hedefinizi giriniz", "Tekrardan Hoşgeldiniz", "", 700, 381);
            if (a=="")
            {
                goto sonnas;
            }
          aa = Convert.ToInt32(a);
            MessageBox.Show("Hedefiniz "+a+" Olarak Ayarlanmıştır.");
            label4.Text = "Kalan Kelime :" + aa;
            sonnas:;
        }

        private int aa;
        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("server=.;Initial Catalog =Kelimeler;Integrated Security=SSPI");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select top 1 id,kelime,anlamı from [Kelimeler].[dbo].[Sayfa1$] order by newid()";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
                label1.Text += dr["kelime"];
                label2.Text += dr["anlamı"];
                label3.Text += dr["id"];
            }
            dr.Close();
            con.Close();
            label7.Text = "";

            string[] kelimesayisi= label2.Text.Split(' ');
            label8.Text ="Kelime Sayısı: "+ kelimesayisi.Length.ToString();
            int basamak=label2.Text.Length;

            for (int i = 0; i < basamak; i++)
            {
                label7.Text += "_ ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var hilekodu = "123";
            if (textBox1.Text == label2.Text || textBox1.Text == hilekodu)
            {
                if (a=="")
                {
                    sayac++;
                    label4.Text = "Doğru Cevap :" + sayac;
                }
                else
                {
                    sayac -= 1;
                    label4.Text = "Kalan Kelime :" + (aa + sayac);
                    if ((aa + sayac) == 0)
                    {

                        Application.Exit();
                    }
                }
            }
            else
            {
                MessageBox.Show("Yazdığınız Cevap Yanlıştır. Doğrusu :" + label2.Text);


            }
            textBox1.Clear();
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                con = new SqlConnection("server=.;Initial Catalog =Kelimeler;Integrated Security=SSPI");
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select top 1 id from Kelimeler.dbo.Sayfa1$ order by id desc";

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label5.Text = "";
                    label5.Text += dr["id"];

                }
                dr.Close();

                con.Close();
            }
            int sayi = int.Parse(label5.Text);
            sayi++;
            label5.Text = sayi.ToString();
            string sorgu = "insert into Kelimeler.dbo.Sayfa1$ (id,kelime,anlamı) values (@id,@kelime,@anlamı)";
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@id", sayi);
            cmd.Parameters.AddWithValue("@kelime", label1.Text);
            cmd.Parameters.AddWithValue("@anlamı", label2.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int sayi = int.Parse(label3.Text);
            string sorgu = "delete from Kelimeler.dbo.Sayfa1$ where id=@id";
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@id", sayi);
            cmd.Parameters.AddWithValue("@kelime", label1.Text);
            cmd.Parameters.AddWithValue("@anlamı", label2.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // kelimenin uzunluğunu alıp rastgele bir sayı elde etmek için yazıldı.

                  Random rastgele = new Random();
                  int LengthAsInteger = label2.Text.Length;
            // dongu sayinin 0dan küçük olmaması için kuruldu (eğer 0 olursa almak istediğim değer 1den küçük oluyor)

            dongu:
            int _randomNumber = rastgele.Next(LengthAsInteger + 1);
            if (_randomNumber <= 0)
            {
                goto dongu;
            }
            // aldığım rastgele sayının önce gerçek harf değerini öğrenip sonrasında gizlenmiş metindeki yerine yazıyorum
            var _theHint = label2.Text.Substring(_randomNumber - 1, 1);
            string _newWord = chreplace(label7.Text, (_randomNumber * 2) - 2, _theHint);
            if (label7.Text==_newWord)
            {
                goto dongu;
            }
            label7.Text = _newWord;
        }
        //gizli metindeki indisi değiştirmek için gerekli method
        static string chreplace(string _words, int _indis, string _newLetter)
        {
            _words = _words.Remove(_indis, 1);
            return _words.Insert(_indis, _newLetter);
        }
    }
}
