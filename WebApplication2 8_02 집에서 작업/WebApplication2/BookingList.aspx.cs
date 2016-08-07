using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class BookingList : System.Web.UI.Page
    {
        Member LoginedMember;
        DBManager dbManager;

        //페이지로드
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBAdmin"];
            int ColCount = 6;
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
                        td.Text = "상영일시";
                        break;
                    case 1:
                        td.Text = "영화명";
                        break;
                    case 2:
                        td.Text = "상영관";
                        break;
                    case 3:
                        td.Text = "좌석열";
                        break;
                    case 4:
                        td.Text = "좌석번호";
                        break;
                    case 5:
                        td.Text = "관람등급";
                        break;
                }
                tr.Cells.Add(td);
            }

            tr.BackColor = Color.FromName("#ccccff");
            Table1.Rows.Add(tr);

            //영화 목록 호출
            string Command = "SELECT * FROM Booking Where ID = @ID";
            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@ID", LoginedMember.ID));
            SqlDataReader reader = dbManager.GetDataList(Command, Params);
            List<Bookinginfo> Bookings = Bookinginfo.SqlDataReaderToBooking(reader);

            int ID_i = 0;

            foreach (var booking in Bookings)
            {
                tr = new TableRow();
                td = new TableCell();

                //테이블의 열마다 체크박스를 추가하고 ID를 열의 ID와 매핑시킨다.
                CheckBox chkbox = new CheckBox();
                //BookingID가 복합키라서... 설계 오류.
                chkbox.ToolTip = booking.ID.ToString().Trim() + "_" +
                    booking.MovieID.ToString().Trim() + "_" +
                    booking.TheaterID.ToString().Trim() + "_" +
                    booking.Playdatetime.ToString().Trim() + "_" +
                    booking.Seatrow.ToString().Trim() + "_" +
                    booking.Seatnumber.ToString().Trim();
                
                chkbox.ID = "CHECKBOXID_" + ID_i;
                ID_i++;

                td.Controls.Add(chkbox);

                tr.Cells.Add(td);

                //영화 상영시간이 현재시간보다 느리면 예매취소 불가능.
                long TimeNOW = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                long TimeBOOK = Convert.ToInt64(DateTime.ParseExact(booking.Playdatetime, "yyyy-MM-dd tt h:mm:ss", null).ToString("yyyyMMddHHmmss"));
                if (TimeNOW > TimeBOOK)
                {
                    chkbox.Enabled = false;
                }
                // 루프를 돌면서 각 셀을 설정한다.
                for (int j = 0; j < ColCount; j++)
                {
                    td = new TableCell();
                    //임시 
                    switch (j)
                    {
                        case 0:
                            td.Text = booking.Playdatetime;
                            break;
                        case 1:
                            td.Text = booking.Moviename;
                            break;
                        case 2:
                            td.Text = booking.TheaterID + "관";
                            break;
                        case 3:
                            td.Text = booking.Seatrow + "열";
                            break;
                        case 4:
                            td.Text = booking.Seatnumber + "번";
                            break;
                        case 5:
                            td.Text = booking.Viewingclass;
                            break;
                    }

                    tr.Cells.Add(td);
                }

                tr.BackColor = Color.White;
                Table1.Rows.Add(tr);
            }
        }
        //돌아가기
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginMember.aspx");
        }
        //예매취소
        protected void Button2_Click(object sender, EventArgs e)
        {
            //TODO : Movie Schedule의 RemianSeat 반환
            //       Seat에서 예약상태 변경
            //       예약 내용 제거
            //       환불

            int refundCount = 0;
            string RefundID = "";
            string Command = "";
            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            SqlDataReader reader;
            foreach (TableRow tr in Table1.Rows)
            {
                try
                {
                    //Table의 제일 첫번째 열은 제목 열이므로 체크박스 컨트롤이 없다.
                    if (tr.Cells[0].Controls.Count < 1)
                    {
                        continue;
                    }
                    CheckBox chkbox = (CheckBox)tr.Cells[0].Controls[0];
                    if (chkbox.Checked)
                    {
                        //추후 환불금액을 측정하기위해 환불 좌석수 카운트.
                        refundCount++;

                        #region 예약 내용 Get
                        Command = "Select * From Booking Where ID = @ID AND MovieID = @MovieID AND TheaterID = @TheaterID AND " +
                            "Playdatetime = @Playdatetime AND Seatrow = @Seatrow AND Seatnumber = @Seatnumber";
                        string[] keys = chkbox.ToolTip.Split('_');
                        RefundID = keys[0];
                        String PlayTime = DateTime.ParseExact(keys[3], "yyyy-MM-dd tt h:mm:ss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        Params.Add(new Tuple<string, object>("@ID",             keys[0]));
                        Params.Add(new Tuple<string, object>("@MovieID",        keys[1]));
                        Params.Add(new Tuple<string, object>("@TheaterID",      keys[2]));
                        Params.Add(new Tuple<string, object>("@Playdatetime", PlayTime));
                        Params.Add(new Tuple<string, object>("@Seatrow",        keys[4]));
                        Params.Add(new Tuple<string, object>("@Seatnumber",     keys[5]));

                        reader = dbManager.GetDataList(Command, Params);
                        List<Bookinginfo> BookingInfo = Bookinginfo.SqlDataReaderToBooking(reader);
                        if(BookingInfo == null || BookingInfo.Count != 1)
                        {
                            Common.ShowMessage(this, @"예매삭제 실패. 관리자에게 문의하세요.");
                            return;
                        }
                        Params.Clear();
                        #endregion
                        #region MovieSchedule 테이블에서 RemainSeat 반환
                        //
                        //   Update example
                        //   Command = Command = "Update Seat Set Isbooked = @Isbooked " +
                        //   "Where TheaterID = @TheaterID AND Seatrow = @Seatrow AND Seatnumber = @Seatnumber AND Playtime = @Playtime";
                        Command = "Update Movieschedule Set Seatremained = Seatremained+1"+
                                    "Where MovieID = @MovieID AND TheaterID = @TheaterID AND Playtime = @Playtime";
      
                        Params.Add(new Tuple<string, object>("@MovieID", BookingInfo[0].MovieID));
                        Params.Add(new Tuple<string, object>("@TheaterID", BookingInfo[0].TheaterID));
                        Params.Add(new Tuple<string, object>("@Playtime", PlayTime));
                        dbManager.DoCommand(Command, Params);
                        Params.Clear();
                        #endregion

                        #region Seat테이블에서 Seat 반환
                        Command = "Update Seat Set Isbooked = 0" +
                                    "Where TheaterID = @TheaterID AND Playtime = @Playtime AND Seatrow = @Seatrow AND Seatnumber = @Seatnumber ";
                        Params.Add(new Tuple<string, object>("@TheaterID", BookingInfo[0].TheaterID));
                        Params.Add(new Tuple<string, object>("@Playtime", PlayTime));
                        Params.Add(new Tuple<string, object>("@Seatrow", BookingInfo[0].Seatrow));
                        Params.Add(new Tuple<string, object>("@Seatnumber", BookingInfo[0].Seatnumber));
                        dbManager.DoCommand(Command, Params);
                        Params.Clear();
                        #endregion

                        #region 예약 내용 제거
                        // Delete Example
                        // string Command = "DELETE FROM Member where ID=@id";
                        Command = "Delete From Booking Where ID = @ID AND MovieID = @MovieID AND TheaterID = @TheaterID AND " +
                         "Playdatetime = @Playdatetime AND Seatrow = @Seatrow AND Seatnumber = @Seatnumber";
                        Params.Add(new Tuple<string, object>("@ID", keys[0]));
                        Params.Add(new Tuple<string, object>("@MovieID", keys[1]));
                        Params.Add(new Tuple<string, object>("@TheaterID", keys[2]));
                        Params.Add(new Tuple<string, object>("@Playdatetime", PlayTime));
                        Params.Add(new Tuple<string, object>("@Seatrow", keys[4]));
                        Params.Add(new Tuple<string, object>("@Seatnumber", keys[5]));
                        dbManager.DoCommand(Command, Params);
                        Params.Clear();
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Data);
                    return;
                }
            }
            #region 환불
            if (refundCount != 0)
            {
                Command = "SELECT * FROM Point WHERE ID=@id ORDER BY Occuredatetime DESC";
                Params.Add(new Tuple<string, object>("@id", RefundID));
                reader = dbManager.GetDataList(Command, Params);
                List<dataSet.Point> points = dataSet.Point.SqlDataReaderToMember(reader);
                Params.Clear();

                Command = "Insert into Point(ID, Occuredatetime, Usedvalue, Rechargedvalue, Remainvalue)" +
                            "values(@ID, @Occuredatetime, @Usedvalue, @Rechargedvalue, @Remainvalue)";
                Params.Add(new Tuple<string, object>("@ID", RefundID));
                Params.Add(new Tuple<string, object>("@Occuredatetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                Params.Add(new Tuple<string, object>("@Usedvalue", 0));
                Params.Add(new Tuple<string, object>("@Rechargedvalue", 0));
                Params.Add(new Tuple<string, object>("@Remainvalue", Convert.ToInt32(points[0].Remainvalue) + (refundCount * Constant.MoviePrice)));
                dbManager.DoCommand(Command, Params);
            }
            #endregion

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}