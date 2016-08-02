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
            
            foreach (var booking in Bookings)
            {
                tr = new TableRow();
                td = new TableCell();

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
    }
}