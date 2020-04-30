using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IronOcr;
using System.Drawing;

namespace yazlab2._1
{
    public partial class Kullanıcı_Paneli : System.Web.UI.Page
    {

        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=veritabanim;User ID=sa;Password=123");
        int kirala, kiralik, gun;
        string kullanici;
        int kayit = 0;
        bool kontrol = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            kullanici = Request.QueryString["kullanici_adi"];
        }
        Bitmap image;
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "select * from books";
                SqlDataAdapter adaptr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adaptr.Fill(ds, "books");
                GridView1.DataSource = ds.Tables["books"];
                GridView1.DataBind();
                baglanti.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "select * from books where kitap like '" + TextBox1.Text + "' ";
                SqlDataAdapter adaptr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adaptr.Fill(ds, "books");
                GridView1.DataSource = ds.Tables["books"];
                GridView1.DataBind();
                baglanti.Close();
                TextBox1.Text = "";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Giriş.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {

                string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
                string path = Server.MapPath("Images\\");

                FileUpload1.SaveAs(path + FileUpload1.FileName);
                image = new Bitmap(path + FileUpload1.FileName);

                var ocr = new AdvancedOcr()
                {
                    ReadBarCodes = true,
                    Strategy = AdvancedOcr.OcrStrategy.Fast,
                    InputImageType = AdvancedOcr.InputTypes.Document
                };

                var results = ocr.Read(image);

                foreach (var Page in results.Pages)
                {
                    foreach (var Barcode in Page.Barcodes)
                    {
                        TextBox4.Text = Barcode.Value;
                    }
                }
            }

            else
            {
                Response.Write("<script>alert('Dosya seçilmedi!')</script>");
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (TextBox4.Text == "")
            {

                Response.Write("<script>alert('ISBN bulunamadı!')</script>");
                TextBox4.Focus();

            }

            if (TextBox4.Text != "")
            {
                kontrol = true;
            }

            if (kontrol == true)
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = baglanti;
                    cmd.CommandText = "SELECT COUNT(*) FROM books WHERE ISBN like '" + TextBox4.Text + "' and kiralik = 0 ";
                    Int32 count = (Int32)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        Response.Write("<script>alert('Kitap zaten sizde değil!')</script>");
                        TextBox4.Text = "";
                    }

                    else
                    {
                        kayit = 1;
                    }

                    if (kayit == 1)
                    {
                        cmd.CommandText = "UPDATE books SET kiralik=0 WHERE ISBN like '" + TextBox4.Text + "' ";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        cmd.CommandText = "SELECT kitap FROM books WHERE ISBN = '" + TextBox4.Text + "' ";
                        string kitap = (string)cmd.ExecuteScalar();

                        cmd.CommandText = "DELETE FROM odunc WHERE kitap = '" + kitap + "' ";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        baglanti.Close();
                        kayit = 0;
                        TextBox4.Text = "";
                    }

                }
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "select * from books where ISBN like '" + TextBox2.Text + "' ";
                SqlDataAdapter adaptr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adaptr.Fill(ds, "books");
                GridView1.DataSource = ds.Tables["books"];
                GridView1.DataBind();
                baglanti.Close();
                TextBox2.Text = "";
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;

                cmd.CommandText = "SELECT COUNT(*) FROM odunc WHERE kullanici like '" + kullanici + "' and kalan_gun = 0 ";
                gun = (Int32)cmd.ExecuteScalar();

                cmd.CommandText = "SELECT COUNT(*) FROM odunc WHERE kullanici like '" + kullanici + "'  ";
                kiralik = (Int32)cmd.ExecuteScalar();

                cmd.CommandText = "SELECT COUNT(*) FROM books WHERE kitap like '" + TextBox3.Text + "' and kiralik = 0 ";
                kirala = (Int32)cmd.ExecuteScalar();

                if (kirala == 1 && kiralik < 3 && gun == 0)
                {
                    cmd.CommandText = "INSERT INTO odunc(kullanici,kitap,kalan_gun) VALUES('" + kullanici + "' , '" + TextBox3.Text + "' , '7')";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    cmd.CommandText = "UPDATE books SET kiralik=1 WHERE kitap like '" + TextBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    baglanti.Close();
                    kirala = 0;
                    TextBox3.Text = "";
                }

                else if (gun != 0)
                {
                    Response.Write("<script>alert('Kitaplarınızdan birinin süresi dolduğu için iade etmeden kitap alamazsınız!')</script>");
                }

                else if (kiralik == 3)
                {
                    Response.Write("<script>alert('Alabileceğiniz maximum kitap sayısına zaten ulaştınız!')</script>");
                }

                else
                {
                    Response.Write("<script>alert('Almaya çalıştığınız kitap bulunamadı veya ödünç alındı!')</script>");
                }

            }

            
        }
    }
}