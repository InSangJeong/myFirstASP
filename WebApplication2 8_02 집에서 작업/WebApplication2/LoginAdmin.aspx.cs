using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class LoginAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //상영관 관리
        protected void Button_Theater_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("TheaterList.aspx"));
        }
        //회원관리
        protected void Button_Member_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MemberList.aspx"));
        }
        //상영 영화 관리
        protected void Button_Movie_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("MovieList.aspx"));
        }
    }
}