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
    public partial class Admin_paneli : System.Web.UI.Page
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=veritabanim;User ID=sa;Password=123");
        int bosmu;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
            Response.Redirect("~/Kitap Ekleme Paneli.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Giriş.aspx");
        }
    }
}