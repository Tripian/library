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

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            while(true)
            {
                if(TextBox1.Text!=null && TextBox2.Text != null)
                {
                    break;
                }

                else if (TextBox1.Text == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Kullanıcı adı girmediniz!');", true);

                }
            }

            if(baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                cmd.CommandText = "INSERT INTO users(kullanici,sifre) VALUES('" + TextBox1.Text + "' , '" + TextBox2.Text + "')";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglanti.Close();
            }

            Response.Redirect("~/Giriş.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Giriş.aspx");
        }
    }
}