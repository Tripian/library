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
    public partial class Kullanıcı_Girişi : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=veritabanim;User ID=sa;Password=123");
        int connect = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox3.Text=="admin" && TextBox4.Text == "12345")
            {
                Response.Redirect("~/Admin Paneli.aspx");
            }

            else
            {
                Response.Write("<script>alert('Hatalı giriş')</script>");
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox3.Focus();
                
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            
            if(baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "SELECT COUNT(*) FROM users WHERE kullanici like '" + TextBox1.Text + "' and sifre like '" + TextBox2.Text + "' ";
                connect = (Int32)cmd.ExecuteScalar();
                baglanti.Close();
            }

            if (connect == 1)
            {
                Response.Redirect("~/Kullanıcı Paneli.aspx");
                connect = 0;
            }

            else
            {
                Response.Write("<script>alert('Kullanıcı adı veya şifre hatalı!')</script>");
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox1.Focus();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Kayıt Ekranı.aspx");
        }
    }
}