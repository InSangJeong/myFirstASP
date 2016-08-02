using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class MovieList : System.Web.UI.Page
    {
        DBManager dbManager;
        Member LoginedMember;
        protected void Page_Load(object sender, EventArgs e)
        {
            //init
            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBadmin"];

            int ColCount = 5;
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            tr.Cells.Add(td);

            //테이블 제목열
            //개선필요(isjeong 7. 27)
            for (int i = 0; i < ColCount; i++)
            {
                td = new TableCell();
                switch (i)
                {
                    case 0:
                        td.Text = "영화제목";
                        break;
                    case 1:
                        td.Text = "상영시작일";
                        break;
                    case 2:
                        td.Text = "상영종료일";
                        break;
                    case 3:
                        td.Text = "상영관";
                        break;
                    case 4:
                        td.Text = "관람등급";
                        break;
                }
                tr.Cells.Add(td);
            }

            tr.BackColor = Color.FromName("#ccccff");
            Table1.Rows.Add(tr);

            //영화 목록 호출
            string Command = "SELECT * FROM Movie";

            SqlDataReader reader = dbManager.GetDataList(Command, new List<Tuple<string, object>>());
            List<Movie> Movies = Movie.SqlDataReaderToMember(reader);

            foreach (var Movie in Movies)
            {
                tr = new TableRow();
                td = new TableCell();

                //테이블의 열마다 체크박스를 추가하고 ID를 열의 ID와 매핑시킨다.
                CheckBox chkbox = new CheckBox();
                chkbox.ID = Movie.MovieID.ToString();
                td.Controls.Add(chkbox);

                tr.Cells.Add(td);
                // 루프를 돌면서 각 셀을 설정한다.
                for (int j = 0; j < ColCount; j++)
                {
                    td = new TableCell();
                    //임시 
                    switch (j)
                    {
                        case 0:
                            td.Text = Movie.MovieID;
                            break;
                        case 1:
                            td.Text = Movie.Moviename;
                            break;
                        case 2:
                            td.Text = Movie.Playstartdatetime;
                            break;
                        case 3:
                            td.Text = Movie.Playenddatetime;
                            break;
                        case 4:
                            td.Text = Movie.Viewingclass;
                            break;
                    }

                    tr.Cells.Add(td);
                }

                tr.BackColor = Color.White;
                Table1.Rows.Add(tr);
            }
        }
        //영화추가
        protected void Button_NewMovie_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewMovie.aspx");
        }
        //영화제거
        protected void Button_DeleteMovie_Click(object sender, EventArgs e)
        {

        }
        //돌아가기
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }
    }
}