using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// System.Data.Oledb kütüphanesinin tanımlaması
using System.Data.OleDb;
// System.Text.RegularExpression tanımlanması
using System.Text.RegularExpressions;  //paralo gübvenli olması icin
// Giris Cıkıs iliskinin kütüphanelerin tanımlaması
using System.IO;

namespace PersonelTakip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        //veri tabanı dosya yolu ve provider nesnesinin belirlemesi 
        OleDbConnection baglantim = new OleDbConnection
            ("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Kisiler.mdb");

        private void Kullanicilari_goster()
        {

            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_Listele = new OleDbDataAdapter
    ("select tcno AS[TC KİMLİK NO] ,ad AS[ADI], soyad AS[SOYADI] ,yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI], parola AS[PAROLA] from kullanicilar Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_Listele.Fill(dshafiza); //sorgu sonuclarıyla bellekteki alanı doldurduk
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "TK Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }

        }
        private void Personelleri_Goster()
        {

            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_Listele = new OleDbDataAdapter
            ("select tcno AS[TC KİMLİK],ad AS[ADI], soyad AS[SOYADI], cinsiyet AS[CİNSİYETİ],mezuniyet AS[MEZUNİYETİ], dogumtarihi AS[DOĞUM TARİHİ],gorevi AS[GÖREVİ],gorevyeri AS[GÖREV YERİ], maasi AS[MAAŞI] from personeller Order By ad ASC", baglantim);



                DataSet dshafiza = new DataSet();
                personelleri_Listele.Fill(dshafiza); //sorgu sonuclarıyla bellekteki alanı doldurduk
                data2.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "TK Personel Takip Programı",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }






        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // FORM 2 AYARLARI
            pictureBox1.Height = 200;
            pictureBox1.Width = 200;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // resmi ayarla scretch et  

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");

            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");

            }
            // KULLANICI İSLEMLERİ SEKMESİ

            this.Text = "YÖNETİCİ İSLEMLERİ";
            label12.ForeColor = Color.DarkRed;
            label12.Text = Form1.adi + "" + Form1.soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC KİMLİK NO 11 KARAKTER OLMALI !");


            radioButton1.Checked = true;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            Kullanicilari_goster();

            // PERSONEL İSLEMLERİ SEKMESİ 

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100; pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";
            maskedTextBox2.Mask = "LL????????????????????";
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000";
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İLK ÖĞRETİM");
            comboBox1.Items.Add("ORTA ÖĞRETİM");
            comboBox1.Items.Add("LİSE");
            comboBox1.Items.Add("ÜNİVERSİTE");

            comboBox3.Items.Add("YÖNETİCİ");
            comboBox3.Items.Add("MEMUR");
            comboBox3.Items.Add("ŞÖFÖR");
            comboBox3.Items.Add("İŞCİLER");

            comboBox4.Items.Add("ARGE");
            comboBox4.Items.Add("BİLGİ İŞLEM");
            comboBox4.Items.Add("MUHASEBE");
            comboBox4.Items.Add("ÜRETİM");
            comboBox4.Items.Add("PAKETLEME");
            comboBox4.Items.Add("NAKLİYE");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short; // kısa tarih görünsün

            radioButton3.Checked = true;














        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
            {

                errorProvider1.SetError(textBox1, "TC KİMLİK NO 11 KARAKTER OLMALI");
            }
            else
                errorProvider1.Clear();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  //yanıp sönmekteyken her karaktere bastıgınızda tektiklenmek

        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)
                errorProvider1.SetError(textBox4, "Kullanıcı Adı 8 Karakter Olmalı");
            else

                errorProvider1.Clear();

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }


        int parola_skoru = 0;

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            //Regex kütüphanesi ing karakterleri baz aldgından türkce karakterlerde sorun yasamamak icin 
            //sifre string ifadelerini türkce karakterleri dönüstürmemiz gerekiyor
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paralodaki türkce karkterler ingilizce karakterlere dönüştürülmüstür.");

            }
            // 1 kücük harf 10 puan 2 ve üzeri 20 puan olacak 

            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            // 1 büyük harf 10 puan 2 ve üzeri 20 puan olacak 

            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            // 1 rakam 10 puan 2 ve üzeri 20 puan olacak 

            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            // 1 sembol 10 puan ,  2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label22.Text = "Büyük harf , kücük harf , rakam ve sembol mutlaka kullanmalısın !";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label22.Text = "";
            if (parola_skoru < 70)
                parola_seviyesi = "kabule edilemez";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";


            label8.Text = "%" + Convert.ToString(parola_skoru);
            label9.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;






        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "parola tekrarı eşleşmiyor");
            else
                errorProvider1.Clear();

        }



        private void topPage1_temizle()

        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();

        }
        private void topPage2_temizle()
        {
            pictureBox2.Image = null; maskedTextBox1.Clear(); maskedTextBox3.Clear(); maskedTextBox4.Clear(); comboBox1.SelectedIndex = -1; comboBox3.SelectedIndex = -1; comboBox4.SelectedIndex = -1;
        }


        private void button2_Click(object sender, EventArgs e) // kaydet butonu
        {

            string yetki = "";

            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select *from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;


            }
            baglantim.Close();

            if (kayitkontrol == false)
            {

                // TC KİMLİK NO KONTROL 

                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
                // ADI VERİ KONTROLÜ
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;
                // SOYADI VERİ KONTROLÜ
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;
                // KULLANICI ADI VERİ KONTROLÜ
                if (textBox4.Text.Length < 8 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;

                // PAROLA VERİ KONTROLÜ
                if (textBox5.Text == "" || parola_skoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;
                // PAROLA TEKRAR VERİ KONTROLÜ

                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {

                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı oluşturuldu!", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();

                    }
                    catch (Exception hatamsj)
                    {

                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geciriniz ! ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen Tc Kimlik numarası daha önceden kayıtlıdır ! ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand(" select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();

                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;






                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Arana kayıt bulunalamdı !", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                baglantim.Close();

            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli bir tc kimlik no giriniz ! ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }
        }

        private void button3_Click(object sender, EventArgs e) // güncelle butonu
        {
            string yetki = "";

            bool kayitkontrol = false;

            // TC KİMLİK NO KONTROL 

            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;
            // ADI VERİ KONTROLÜ
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
            // SOYADI VERİ KONTROLÜ
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;
            // KULLANICI ADI VERİ KONTROLÜ
            if (textBox4.Text.Length < 8 || textBox4.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;

            // PAROLA VERİ KONTROLÜ
            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;
            // PAROLA TEKRAR VERİ KONTROLÜ

            if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" && textBox3.Text.Length > 1 && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {

                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "Kullanıcı";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + yetki + "',kullaniciadi='" + textBox4.Text + "',parola='" + textBox5.Text + "' where tcno='" + textBox1.Text + "'", baglantim);
                    guncellekomutu.ExecuteNonQuery();  // sonucunu veri tabanına işle
                    baglantim.Close();
                    MessageBox.Show("Kullanıcı bilgileri güncellendi !", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Kullanicilari_goster();


                }
                catch (Exception hatamsj)
                {

                    MessageBox.Show(hatamsj.Message, "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }

            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geciriniz ! ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) // SİL BUTONU
        {

            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {


                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    Kullanicilari_goster();
                    topPage1_temizle();
                    break;



                }

                if (kayit_arama_durumu == false)
                    MessageBox.Show("Silinecek kayıt bulunamadı", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                topPage1_temizle();

            }
            else
                MessageBox.Show("Lütfen 11 karakterden olusan bir tc kimlik no giriniz!", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel Resmi Seciniz!";
            resimsec.Filter = "JPG Dosyalar (*.jpg) | *.jpg";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {

                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayitkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {

                kayitkontrol = true;
                break;

            }
            baglantim.Close();
            if (kayitkontrol == false)
            {
                if (pictureBox2.Image == null)

                    button6.ForeColor = Color.Red;
                else

                    button6.ForeColor = Color.Black;
                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;


                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;

                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label18.ForeColor = Color.Red;
                else
                    label18.ForeColor = Color.Black;
                if (comboBox4.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && maskedTextBox4.MaskCompleted != false)
                {

                    if (radioButton3.Checked == true)
                        cinsiyet = "Bay";
                    else if (radioButton4.Checked == true)
                        cinsiyet = "Bayan";

                    try
                    {
                        baglantim.Open();

                        OleDbCommand eklekomutu = new OleDbCommand("insert into personeller values ('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','" + maskedTextBox4.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler"))  //Application.Startup ( bin debug klasörünü simgeler   //personel adında bir klasör yoksa
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler");

                        pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text + ".jgp");
                        MessageBox.Show("YENİ PERSONEL KAYDI OLUŞTURULDU ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Personelleri_Goster();
                        topPage2_temizle();



                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları gözden geciriniz ! ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen tc kimlik numarası daha önceden kayıtlıdır ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpg");
                    }
                    catch
                    {

                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");
                    }




                    maskedTextBox2.Text = kayitokuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Bay")
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;
                    comboBox1.Text = kayitokuma.GetValue(4).ToString();
                    dateTimePicker1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox3.Text = kayitokuma.GetValue(6).ToString();
                    comboBox4.Text = kayitokuma.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitokuma.GetValue(8).ToString();

                    break;


                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Arana kayıt bulunamadı !", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("11 haneli tc no giriniz", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayitkontrol = false;
          
                if (pictureBox2.Image == null)

                    button6.ForeColor = Color.Red;
                else

                    button6.ForeColor = Color.Black;
                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;


                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;

                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label18.ForeColor = Color.Red;
                else
                    label18.ForeColor = Color.Black;
                if (comboBox4.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (int.Parse(maskedTextBox4.Text) < 1000)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false && comboBox1.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && maskedTextBox4.MaskCompleted != false)
                {

                    if (radioButton3.Checked == true)
                        cinsiyet = "Bay";
                    else if (radioButton4.Checked == true)
                        cinsiyet = "Bayan";

                    try
                    {
                        baglantim.Open();

                    OleDbCommand guncellekomutu = new OleDbCommand("update personeller set ad='" + maskedTextBox2.Text + "',soyad='" + maskedTextBox3.Text + "',cinsiyet='"+cinsiyet+"',mezuniyet='"+comboBox1.Text+"',dogumtarihi='"+dateTimePicker1.Text+"',gorevi='"+comboBox3.Text+"',gorevyeri='"+comboBox4.Text+"',maasi='"+maskedTextBox4.Text+"' where tcno='"+maskedTextBox1.Text+"'", baglantim);

                        guncellekomutu.ExecuteNonQuery();    //sorgu sonucları access tablosuna işlenir
                        baglantim.Close();
                        Personelleri_Goster();
                        topPage2_temizle();
                        maskedTextBox4.Text = "0";



                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();

                    }
                }
               

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted == true)
            {
                bool kayit_arama_durum = false;
                baglantim.Open();
                OleDbCommand arama_sorgusu = new OleDbCommand("select*from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = arama_sorgusu.ExecuteReader(); // sorgu sonucunda gelen kayıtların kayıt okuma isimli datareader aktarılmasını saglıyoruz
                while (kayitokuma.Read())
                {
                    kayit_arama_durum = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from personeller where tcno='" + maskedTextBox1.Text + "'", baglantim);
                    deletesorgu.ExecuteNonQuery(); // sonucları veritabanına işle
                    break;
                }
                if (kayit_arama_durum == false)
                {
                    MessageBox.Show("SİLİNECEK KAYIT BULUNAMADI", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantim.Close();
                Personelleri_Goster();
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            }
            else
            {
                MessageBox.Show("LÜTFEN 11 KARAKTERDEN OLUSAN BİR TC KİMLİK NO GİRİNİZ", "TK PERSONEL TAKİP PROGRAMI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            topPage2_temizle();
        }
    }
}

    

    
    

    

