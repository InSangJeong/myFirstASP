using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class TheaterList : System.Web.UI.Page
    {
        DBManager dbManager;
        Member LoginedMember;
        protected void Page_Load(object sender, EventArgs e)
        {
            //init
            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBadmin"];

            int ColCount = 4;
            //Table 셋팅
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            tr.Cells.Add(td);
            List<string> TableNames = Theater.GetMemberNameToSting();

            //테이블 제목열
            for (int i = 0; i < ColCount; i++)
            {
                td = new TableCell();
                td.Text = TableNames[i];
                tr.Cells.Add(td);
            }
            tr.BackColor = Color.FromName("#ccccff");
            Table1.Rows.Add(tr);

            //기존 생성된 상영관 목록을 불러와 테이블에 입력한다.
            string Command = "SELECT * FROM Theater ORDER BY TheaterID ASC";

            SqlDataReader reader = dbManager.GetDataList(Command, new List<Tuple<string, object>>());
            List<dataSet.Theater> Theaters = dataSet.Theater.SqlDataReaderToMember(reader);
           
            foreach (var theater in Theaters)
            {
                tr = new TableRow();
                td = new TableCell();

                //테이블의 열마다 체크박스를 추가하고 ID를 열의 ID와 매핑시킨다.
                CheckBox chkbox = new CheckBox();
                chkbox.ID = theater.TheaterID.ToString();
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
                            td.Text = theater.TheaterID;
                            break;
                        case 1:
                            td.Text = theater.Seatcount;
                            break;
                        case 2:
                            td.Text = theater.Seatrowcount;
                            break;
                        case 3:
                            td.Text = theater.Seatnumbercount;
                            break;
                    }

                    tr.Cells.Add(td);
                }

                tr.BackColor = Color.White;
                Table1.Rows.Add(tr);
            }
        }

        //영화관 추가
        protected void Button_NewTheater_Click(object sender, EventArgs e)
        {
            if(TextBox_Row.Text == string.Empty ||
                TextBox_Number.Text == string.Empty)
            {
                //TODO : 열, 번호값이 맞는지 확인
                return;
            }

            //상영관 ID를 받기위해 현재 상영관 ID중 가장 큰 값을 받아온다.
            string Command = "Select top 1 * from Theater order by LEN(TheaterID), TheaterID desc";
            //보낸 명령에 대한 리더 객체를 얻는다.
            SqlDataReader Reader = dbManager.GetDataList(Command, new List<Tuple<string, object>>());
            //리더 객체에 맞는 데이터 셋을 입혀 멤버객체를 받는다.
            List<Theater> Members = Theater.SqlDataReaderToMember(Reader);
            int NewTheaterID = 0;
            if (Members.Count == 1)
            {
                //상영관 ID는 기존 ID중 가장 큰 ID + 1
                NewTheaterID = Convert.ToInt32(Members[0].TheaterID) + 1;
            }
            //상영관 추가 코드
            Command = "insert into Theater(TheaterID, Seatcount, Seatrowcount, Seatnumbercount)" +
                        "values(@TheaterID, @Seatcount, @Seatrowcount, @Seatnumbercount)";
            //아스키 코드값을 이용하여 열의 갯수를 숫자로 변환하고 열당 좌석수 만큼 곱하여 총 좌석수를 지정한다.
            //(int)(Convert.ToChar(TextBox_Row.Text.ToUpper()) - 64);//이전버전
            int Int_row = Common.AlphabetToNumber(TextBox_Row.Text);
            int SeatCountByRow = Convert.ToInt32(TextBox_Number.Text);

            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@TheaterID", NewTheaterID));
            Params.Add(new Tuple<string, object>("@Seatcount", Int_row * SeatCountByRow));
            Params.Add(new Tuple<string, object>("@Seatrowcount", TextBox_Row.Text.ToUpper()));
            Params.Add(new Tuple<string, object>("@Seatnumbercount", SeatCountByRow));

            if (dbManager.DoCommand(Command, Params))
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
        //영화관 삭제
        protected void Button_DeleteTheater_Click(object sender, EventArgs e)
        {
            foreach(TableRow tr in Table1.Rows)
            {
                try
                {
                    //Table의 제일 첫번째 열은 제목 열이므로 체크박스 컨트롤이 없다.
                    if(tr.Cells[0].Controls.Count < 1)
                    {
                        continue;
                    }
                    CheckBox chkbox = (CheckBox)tr.Cells[0].Controls[0];
                    if (chkbox.Checked)
                    {
                        string Command = "DELETE FROM Theater where TheaterID=@TheaterID";
                        List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                        Params.Add(new Tuple<string, object>("@TheaterID", chkbox.ID));
                        dbManager.DoCommand(Command, Params);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Data);
                    return;
                }
            }
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }
    }
}