using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yazlab2._1

{
    public partial class Kayıt_Ekranı : System.Web.UI.Page
    {

        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=veritabanim;User ID=sa;Password=123");
        bool kontrol = false;
        int kayit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (TextBox1.Text.Contains(" ") == true )
            {
                Response.Write("<script>alert('Kullanıcı adına boşluk karakterini girmeyiniz!')</script>");
                TextBox1.Text = "";
                TextBox1.Focus();
            }

            else if (TextBox1.Text == "")
            {

                Response.Write("<script>alert('Kullanıcı adı girmediniz!')</script>");
                TextBox1.Focus();

            }

            else if (TextBox2.Text == "")
            {

                Response.Write("<script>alert('Şifre girmediniz!')</script>");
                TextBox2.Focus();
                
            }

            if (TextBox1.Text != "" && TextBox2.Text != "")
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
                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE kullanici like '" + TextBox1.Text +  "' " ;
                    Int32 count = (Int32)cmd.ExecuteScalar();

                    if ( count == 1 )
                    {
                        Response.Write("<script>alert('Girdiğiniz kullanıcı adı kullanılıyor. Başka bir şey deneyiniz!')</script>");
                        TextBox1.Text = "";
                        TextBox1.Focus();
                    }

                    else
                    {
                        kayit = 1;
                    }

                    if (kayit == 1)
                    {
                        cmd.CommandText = "INSERT INTO users(kullanici,sifre,odunc) VALUES('" + TextBox1.Text + "' , '" + TextBox2.Text + "' , '0')";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        baglanti.Close();
                        kayit = 0;
                        Response.Redirect("~/Giriş.aspx");
                    }
                    
                }
            }

            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Giriş.aspx");
        }

    }
}