using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yazlab2._1
{
    public partial class Kullanıcı_Girişi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin Paneli.aspx");
            
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Kullanıcı Paneli.aspx");

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Kayıt Ekranı.aspx");
        }
    }
}