using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class LoginAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if((Member)Session["MEMBER"] == null || ((Member)Session["MEMBER"]).ID.Trim().ToLower() != "admin")
            {
                Common.ShowMessage(this, @"잘못된 접근입니다.");
                Response.Redirect("Main.aspx");
            }
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Main.aspx");
        }
    }
}