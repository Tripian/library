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
    public partial class Admin_paneli : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=veritabanim;User ID=sa;Password=123");
        int bosmu;
        bool kontrol = false;
        int kayit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Bitmap image;

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "select * from users";
                SqlDataAdapter adaptr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adaptr.Fill(ds, "users");
                GridView1.DataSource = ds.Tables["users"];
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
                cmd.CommandText = "SELECT COUNT(*) FROM odunc ";
                bosmu = (Int32)cmd.ExecuteScalar();

                if (bosmu == 0)
                {
                    Response.Write("<script>alert('Ödünç alınan kitap yok!')</script>");
                }

                else
                {
                    cmd.CommandText = "UPDATE odunc SET kalan_gun = kalan_gun - " + int.Parse(TextBox1.Text) + "  ";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Response.Write("<script>alert('Zaman "+ TextBox1.Text + " gün atlandı!')</script>");
                }

                baglanti.Close();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == "")
            {

                Response.Write("<script>alert('Kitap adı girmediniz!')</script>");
                TextBox2.Focus();

            }

            else if (TextBox3.Text == "")
            {

                Response.Write("<script>alert('ISBN bulunamadı!')</script>");

            }

            if (TextBox2.Text != "" && TextBox3.Text != "")
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
                    cmd.CommandText = "SELECT COUNT(*) FROM books WHERE kitap like '" + TextBox2.Text + "' and ISBN like '" + TextBox3.Text + "' ";
                    Int32 count = (Int32)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        Response.Write("<script>alert('Girdiğiniz kitabın kaydı zaten bulunmakta!')</script>");
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox2.Focus();
                    }

                    else
                    {
                        kayit = 1;
                    }

                    if (kayit == 1)
                    {
                        cmd.CommandText = "INSERT INTO books(kitap,ISBN,kiralik) VALUES('" + TextBox2.Text + "' , '" + TextBox3.Text + "' , '0')";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        baglanti.Close();
                        kayit = 0;
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                    }

                }
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Giriş.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
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
                        TextBox3.Text = Barcode.Value;
                    }
                }
            }

            else
            {
                Response.Write("<script>alert('Dosya seçilmedi!')</script>");
            }
            
            

        }
    }
}