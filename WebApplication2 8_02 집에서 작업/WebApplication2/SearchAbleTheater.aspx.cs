using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class SearchAbleTheater : System.Web.UI.Page
    {
        DBManager dbManager;
        Member LoginedMember;
        public string TheaterID;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBadmin"];

            //쿠키로 이전페이지에서 입력받은 날짜를 받는다.
            string start = Request.Cookies["Start"].Value;
            string end = Request.Cookies["End"].Value;

            /* 가용가능 사용관 지정방법
             * 1. 각 영화의 시간을 검색해서 겹치는 영화를 뽑아낸다.
             * 2. 겹치는 영화가 설정된 상영관을 전체 상영관 목록에서 제거한다.
             */
            #region 사용가능 상영관 추출
            string Command = "SELECT * FROM Movie WHERE Playstartdatetime <= @end AND Playenddatetime >= @start";

            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@start", start));
            Params.Add(new Tuple<string, object>("@end", end));
            SqlDataReader reader = dbManager.GetDataList(Command, Params);

            List<Movie> Movies = Movie.SqlDataReaderToMember(reader);
            List<Theater> AbleTheaters = new List<Theater>();
            // DEBUG : 시간 겹치는 영화만 나오는지 확인해
            if (Movies.Count == 0)
            {
                //기간이 겹치는 영화가 없으므로 모든 상영관을 테이블로 도시한다.
                Command = "SELECT * FROM Theater";
                reader = dbManager.GetDataList(Command, new List<Tuple<string, object>>());
                AbleTheaters = Theater.SqlDataReaderToMember(reader);
            }
            else
            {
                //기간이 겹치는 영화들이 있다.
                //1.영화 스케쥴에 사용되고있는 상영관 리스트를 추출

                //명령어 셋팅
                Command = "SELECT * FROM Movieschedule WHERE MovieID IN (";
                int i = 1;
                foreach(var movie in Movies)
                {
                    if (Movies.Count != i)
                        Command += "@movieID" + Convert.ToString(i) + ", ";
                    else
                        Command += "@movieID" + Convert.ToString(i);
                    i++;
                }
                Command += ")";
                //명령어에 해당하는 변수 설정.
                Params = new List<Tuple<string, object>>();
                i = 1;
                foreach (var movie in Movies)
                {
                    //DB에서 값 가져올때 공백이 생겨서 공백 제거를합니다.
                    Movies[i - 1].MovieID = Movies[i - 1].MovieID.Trim(' ');
                    Params.Add(new Tuple<string, object>("@movieID" + Convert.ToString(i), Movies[i-1].MovieID));
                    i++;
                }
                reader = dbManager.GetDataList(Command, Params);
                //뽑아온 곳은 영화 스케쥴 테이블인데 영화관 객체에 넣으면 안되기때문에 영화관 ID만 받아서 리스트로 생성한다.
                List<string> DisAbleTheaters = new List<string>();
                DisAbleTheaters = Theater.OlnyTheaterID(reader);

                //2. 1.에서 뽑은 상영관을 전체 상영관에서 제거한다.
                Command = "SELECT * FROM Theater WHERE TheaterID NOT IN (";
                i = 1;
                foreach (var Theater in DisAbleTheaters)
                {
                    if (DisAbleTheaters.Count != i)
                        Command += "@TheaterID" + Convert.ToString(i) + ", ";
                    else
                        Command += "@TheaterID" + Convert.ToString(i);
                    i++;
                }
                Command += ")";
                Params = new List<Tuple<string, object>>();

                //사용불가능한 상영관이 없으면 전체 상영관을 불러오는 코드를 수행하고
                //그렇지 않다면 전체 상영관에서 사용불가능한 상영관을 제거한 사용가능상영관을 불러온다.
               if (DisAbleTheaters.Count == 0)
                {
                    Command = "SELECT * FROM Theater";

                    reader = dbManager.GetDataList(Command, new List<Tuple<string, object>>());
                    AbleTheaters = Theater.SqlDataReaderToMember(reader);
                }
                else
                {
                    for (int j = 1; j <= DisAbleTheaters.Count; j++)
                    {
                        //DB에서 값 가져올때 공백이 생겨서 공백 제거를합니다.
                        DisAbleTheaters[j - 1] = DisAbleTheaters[j - 1].Trim(' ');
                        Params.Add(new Tuple<string, object>("@TheaterID" + Convert.ToString(j), DisAbleTheaters[j - 1]));
                    }
                    reader = dbManager.GetDataList(Command, Params);
                    AbleTheaters = Theater.SqlDataReaderToMember(reader);
                }
             
               
            }
            #endregion
            // AbleTheaters 에 값이 들어있고 라디오버튼과 상영관명으로 이루어진 테이블을 셋팅한다.
            #region 테이블 셋팅
            int ColCount = 2;
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
                        td.Text = "상영관 번호";
                        break;
                    case 1:
                        td.Text = "상영관 인원수";
                        break;
                }
                tr.Cells.Add(td);
            }

            tr.BackColor = Color.FromName("#ccccff");
            Table1.Rows.Add(tr);

           
            foreach (var theater in AbleTheaters)
            {
                tr = new TableRow();
                td = new TableCell();

                //테이블의 열마다 체크박스를 추가하고 ID를 열의 ID와 매핑시킨다.
                RadioButton radiobtn = new RadioButton();
                radiobtn.ID = theater.TheaterID.ToString();
                radiobtn.GroupName = "Group1";
                td.Controls.Add(radiobtn);

                tr.Cells.Add(td);
                // 루프를 돌면서 각 셀을 설정한다.
                for (int j = 0; j < ColCount; j++)
                {
                    td = new TableCell();
                    //임시 
                    switch (j)
                    {
                        case 0:
                            td.Text = theater.TheaterID;
                            break;
                        case 1:
                            td.Text = theater.Seatcount;
                            break;
                    }

                    tr.Cells.Add(td);
                }

                tr.BackColor = Color.White;
                Table1.Rows.Add(tr);
            }
            #endregion
        }

        protected void test(object sender, EventArgs e)
        {
            foreach (TableRow tr in Table1.Rows)
            {
                try
                {
                    //Table의 제일 첫번째 열은 제목 열이므로 체크박스 컨트롤이 없다.
                    if (tr.Cells[0].Controls.Count < 1)
                    {
                        continue;
                    }
                    RadioButton radiobtn = (RadioButton)tr.Cells[0].Controls[0];
                    if (radiobtn.Checked)
                    {
                        TheaterID = radiobtn.ID;
                        bunho.Value = TheaterID;
                        movie.Value = TheaterID;
                        string popupAddress = "GoStr();";
                        ClientScript.RegisterStartupScript(this.GetType(), "script", popupAddress, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Data);
                }
            }
        }
        
      
        //취소버튼
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewMovie.aspx");
        }
        
    }
}
 
