using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class Booking : System.Web.UI.Page
    {
        //TODO : 1. 예약하기버튼 기능 활성화
        //       2. 이전에 예약되었던 좌석 체크표시 + enable; 
        Member LoginedMember;
        DBManager dbManager;
        String SelectedMovieID = "";
        String SelectedPlayDate = "";
        String SelectedTheater = "";
        int remainTicket = 0;

       
        //페이지로드
        protected void Page_Load(object sender, EventArgs e)
        {
            SelectedTheater = DropDownList_Theater.SelectedValue.Trim();

            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBAdmin"];

            //사용자가 아무 영화를 클릭하지 않은 초기화 상태라면
            if(DropDownList_MovieName.SelectedValue == "")
            {
                String Command = "Select * From Movie";
                SqlDataReader reader = dbManager.GetDataList(Command, new List<Tuple<string, object>>());
                List<Movie> Movies = Movie.SqlDataReaderToMember(reader);

                DropDownList_MovieName.DataTextField = "Moviename";
                DropDownList_MovieName.DataValueField = "MovieID";
                DropDownList_MovieName.DataSource = Movies;
                DropDownList_MovieName.DataBind();
            }
            //영화를 선택하였으니 해당 영화에 맞는 시간을 보여줌.
            else
            {
                SelectedMovieID = DropDownList_MovieName.SelectedValue.Trim();
                String Command = "Select * From Movieschedule Where MovieID = @MovieID";
                List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                Params.Add(new Tuple<string, object>("@MovieID", SelectedMovieID));

                SqlDataReader reader = dbManager.GetDataList(Command, Params);
                List<Movieschedule> PlayTimes = Movieschedule.SqlDataReaderToMember(reader);

                DropDownList_PlayDate.DataTextField = "Playtime";
                DropDownList_PlayDate.DataValueField = "Playtime";
                DropDownList_PlayDate.DataSource = PlayTimes;
                DropDownList_PlayDate.DataBind();
                Params.Clear();
                //상영관을 선택하지 않았음.
                if (DropDownList_PlayDate.SelectedValue == "")
                {
                    ;
                }
                //상영관을 선택하였으면 관이랑 남은 좌석수 표시
                else
                {
                    SelectedPlayDate = DropDownList_PlayDate.SelectedValue.Trim();

                    DropDownList_Theater.DataTextField = "RemaindSeatMent";
                    DropDownList_Theater.DataValueField = "TheaterID";
                    DropDownList_Theater.DataSource = PlayTimes;
                    DropDownList_Theater.DataBind();
                    Params.Clear();

                    if(TextBox_TicketCount.Text != "")
                        remainTicket = Convert.ToInt32(TextBox_TicketCount.Text);
                    if (remainTicket != 0)
                    {
                        if (SelectedMovieID == "" || SelectedPlayDate == "" || SelectedTheater == "")
                            return;
                        SetTable();
                    }
                }

            }
        }
        public void SetTable()
        {
            String Command;
            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            //TODO: 영화관 좌석상태를 표시하기위해 이미 예약된 좌석정보를 가져오도록합니다.
            //      기예약된 좌석은 enable = false, checked = true;로 설정하여 사용자가 예약할수 없도록 합니다.
            //      영화관ID와 시작시간이 같은 좌석들을 Row->number 각각 오름차순 순으로 받아옵니다.
            Command = "Select * From Seat Where TheaterID = @TheaterID AND Playtime = @Playtime Order by Seatrow, Seatnumber";
            Params.Add(new Tuple<string, object>("@TheaterID", SelectedTheater));
            Params.Add(new Tuple<string, object>("@Playtime", SelectedPlayDate));
            SqlDataReader reader = dbManager.GetDataList(Command, Params);
            List<Seat> Seats = Seat.SqlDataReaderToSeat(reader);
            if (Seats == null || Seats.Count == 0)
                return;
            Params.Clear();
            //TODO: 사용자가 영화 셋팅을 다하고 예약인원을 설정하였으면
            //      영화관 관람좌석테이블을 생성합니다. 모두 체크박스로 넣어주도록 합니다.
            //      이미 예약된 좌석은 체크 표시로 하고 미예약은 언체크로 표시합니다.

            TableRow tr = null;
            TableCell td = null;
            String preRow = "";
            foreach (Seat seat in Seats)
            {
                //Row열이 바뀌면 테이블에 Row를 생성.
                if (preRow != seat.Seatrow)
                {
                    if (tr != null)
                        Table1.Rows.Add(tr);
                    tr = new TableRow();
                    preRow = seat.Seatrow;
                }

                td = new TableCell();
                CheckBox chkbox = new CheckBox();
                chkbox.ID = seat.Seatrow + "열" + seat.Seatnumber + "번";
                if (seat.Isbooked == "True")
                {
                    chkbox.Enabled = false;
                    chkbox.Checked = true;
                }
                td.Controls.Add(chkbox);
                tr.Cells.Add(td);
            }
            if (tr != null)
                Table1.Rows.Add(tr);
        }
        //인원수 클릭하고 좌석고르기
        protected void Button1_Click(object sender, EventArgs e)
        {
            //이벤트가 pageLoad를 강제 실행하므로 여기서 해아할 일들을 page_load에 옮김(함수명 SetTable())
            ;
        }


        //예약하기
        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Tuple<string, string>> TryBookingSeats = new List<Tuple<string, string>>();
            #region 예매정보없거나 지정한 인원와 다르면 리턴
            if (SelectedMovieID == "" || SelectedPlayDate == "" || SelectedTheater == "")
                return;
            //원래 chkbox에 changed on 일때 remainTicket의 값을 변동해서 0인지 검사하려고했지만
            //동적으로 생성된 컨트롤에대해 이벤트를 받는게 해결이 되지 않아 임시방편으로
            //enable상태이면서 checked숫자로 비교한다.
            int checkedBoxCount = 0;
            foreach(TableRow tableRow in Table1.Rows)
            {
                foreach(TableCell tableCell in tableRow.Cells)
                {
                    if(tableCell.Controls.Count == 1)
                    {
                        CheckBox chkBox = (CheckBox)tableCell.Controls[0];
                        if (chkBox.Enabled && chkBox.Checked)
                        {
                            checkedBoxCount++;
                            string[] RowAndNumber = chkBox.ID.Split(new char[] { '열', '번'});
                            TryBookingSeats.Add(new Tuple<string, string>(RowAndNumber[0], RowAndNumber[1]));
                        }
                    }
                }
            }
            //checkbox가 더 많거나 적으므로 예매를 취소하고 확인해달라는 메시지를 보내야함.
            if (remainTicket != checkedBoxCount)
                return;
            #endregion

            //TODO : 예약 객체 생성시 점검사항들 
            //      1. 포인트가 사람수만큼 결제가능한지 확인합니다.
            //      2. 예약하는동안 선택한 좌석(들)이 예매가 되었는지 확인.
            //      3. 관람등급과 고객나이 확인
            //      4. 예매객체 생성
            //      5. 포인트 사용내역 객체 생성
            //      6. Seat의 예약상태를 True로 변경한다.
            //      7. 예약할경우 MovieSchedule의 RemainSeat값도 줄여줘야함.

            string Command = "";
            List<Tuple<string, object>> Params;
            SqlDataReader reader;
            List<Movie> SelcetedMovieObject = null;
            int LastlyRemaindPoint = 0;
            #region 1
            Command = "SELECT * FROM Point WHERE ID=@id ORDER BY Occuredatetime DESC";
            Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@id", LoginedMember.ID));

            reader = dbManager.GetDataList(Command, Params);
            List<dataSet.Point> points = dataSet.Point.SqlDataReaderToMember(reader);
            if (points == null || points.Count == 0)
                return;
            int totalPrice = remainTicket * Constant.MoviePrice;
            if (Convert.ToInt32(points[0].Remainvalue) <  totalPrice)
                return;
            LastlyRemaindPoint = Convert.ToInt32(points[0].Remainvalue);
            Params.Clear();
            #endregion
            #region 2
            Command = "Select * From Seat Where "+
                "TheaterID = @TheaterID AND Playtime = @Playtime AND Seatrow = @Seatrow AND Seatnumber = @Seatnumber";
            foreach(Tuple<string, string> TryBookingSeat in TryBookingSeats)
            {
                Params.Add(new Tuple<string, object>("@TheaterID", SelectedTheater));
                Params.Add(new Tuple<string, object>("@Playtime", SelectedPlayDate));
                Params.Add(new Tuple<string, object>("@Seatrow", TryBookingSeat.Item1));
                Params.Add(new Tuple<string, object>("@Seatnumber", TryBookingSeat.Item2));
                reader = dbManager.GetDataList(Command, Params);
                List<Seat> seat = Seat.SqlDataReaderToSeat(reader);
                if (seat == null || seat.Count == 0 || seat[0].Isbooked == "True")
                    return;
                Params.Clear();
            }
            #endregion
            #region 3
            Command = "Select * From Movie Where MovieID = @MovieID";
            Params.Add(new Tuple<string, object>("@MovieId", SelectedMovieID));
            reader = dbManager.GetDataList(Command, Params);
            SelcetedMovieObject = Movie.SqlDataReaderToMember(reader);
            if (SelcetedMovieObject == null || SelcetedMovieObject.Count == 0)
                return;
            if (Convert.ToInt32(LoginedMember.Age) < Convert.ToInt32(SelcetedMovieObject[0].Viewingclass))
                return;
            Params.Clear();
            #endregion
            #region 4
            Command = "insert into Booking(ID, MovieID, TheaterID, Playdatetime, Seatrow, Seatnumber, Moviename, Bookedcount, Viewingclass)" +
                    "values(@ID, @MovieID, @TheaterID, @Playdatetime, @Seatrow, @Seatnumber, @Moviename, @Bookedcount, @Viewingclass)";
            foreach (Tuple<string, string> TryBookingSeat in TryBookingSeats)
            {
                Params.Add(new Tuple<string, object>("@ID", LoginedMember.ID.Trim()));
                Params.Add(new Tuple<string, object>("@MovieID", SelectedMovieID.Trim()));
                Params.Add(new Tuple<string, object>("@TheaterID", SelectedTheater.Trim()));
                Params.Add(new Tuple<string, object>("@Playdatetime", SelectedPlayDate));
                Params.Add(new Tuple<string, object>("@Seatrow", TryBookingSeat.Item1));
                Params.Add(new Tuple<string, object>("@Seatnumber", TryBookingSeat.Item2));
                Params.Add(new Tuple<string, object>("@Moviename", SelcetedMovieObject[0].Moviename));
                Params.Add(new Tuple<string, object>("@Bookedcount", remainTicket));
                Params.Add(new Tuple<string, object>("@Viewingclass", SelcetedMovieObject[0].Viewingclass));
                if (!dbManager.DoCommand(Command, Params))
                    ;//TODO : 예약 실패 로직(미완성).
                Params.Clear();
            }
            #endregion
            #region 5
            Command = "insert into Point(ID, Occuredatetime, Usedvalue, Rechargedvalue, Remainvalue)" +
                    "values(@ID, @Occuredatetime, @Usedvalue, @Rechargedvalue, @Remainvalue)";
            Params.Add(new Tuple<string, object>("@ID", LoginedMember.ID));
            Params.Add(new Tuple<string, object>("@Occuredatetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Params.Add(new Tuple<string, object>("@Usedvalue", remainTicket * Constant.MoviePrice));
            Params.Add(new Tuple<string, object>("@Rechargedvalue", 0));
            Params.Add(new Tuple<string, object>("@Remainvalue", LastlyRemaindPoint - remainTicket * Constant.MoviePrice));
            if (!dbManager.DoCommand(Command, Params))
                ;//TODO : 예약 실패 로직(미완성).
            Params.Clear();
            #endregion
            #region 6
            Command = Command = "Update Seat Set Isbooked = @Isbooked " +
                "Where TheaterID = @TheaterID AND Seatrow = @Seatrow AND Seatnumber = @Seatnumber AND Playtime = @Playtime";
            foreach (Tuple<string, string> TryBookingSeat in TryBookingSeats)
            {
                Params.Add(new Tuple<string, object>("@Isbooked", "True"));
                Params.Add(new Tuple<string, object>("@TheaterID", SelectedTheater));
                Params.Add(new Tuple<string, object>("@Seatrow", TryBookingSeat.Item1));
                Params.Add(new Tuple<string, object>("@Seatnumber", TryBookingSeat.Item2));
                Params.Add(new Tuple<string, object>("@Playtime", SelectedPlayDate));
                if (!dbManager.DoCommand(Command, Params))
                    ;//TODO : 예약 실패 로직(미완성).
                Params.Clear();
            }
            #endregion
            #region 7
            //일단 기존남은 좌석 데이터를 가져와야함.
            int RemainSeatCount = 0;
            Command = "Select * From Movieschedule " +
                "Where MovieID = @MovieId AND TheaterID = @TheaterID AND Playtime = @Playtime";
            Params.Add(new Tuple<string, object>("@MovieId", SelectedMovieID));
            Params.Add(new Tuple<string, object>("@TheaterID", SelectedTheater));
            Params.Add(new Tuple<string, object>("@Playtime", SelectedPlayDate));
            reader = dbManager.GetDataList(Command, Params);
            List<Movieschedule> movieschedule = Movieschedule.SqlDataReaderToMember(reader);
            if (movieschedule == null || movieschedule.Count != 1)
                return;
            RemainSeatCount = Convert.ToInt32(movieschedule[0].Seatremained);
            if (RemainSeatCount == 0)
                return;

            Params.Clear();
            Command = "Update Movieschedule Set Seatremained = @Seatremained " +
                "Where MovieID = @MovieId AND TheaterID = @TheaterID AND Playtime = @Playtime";
            Params.Add(new Tuple<string, object>("@Seatremained", RemainSeatCount - remainTicket));
            Params.Add(new Tuple<string, object>("@MovieId", SelectedMovieID));
            Params.Add(new Tuple<string, object>("@TheaterID", SelectedTheater));
            Params.Add(new Tuple<string, object>("@Playtime", SelectedPlayDate));
            if (!dbManager.DoCommand(Command, Params))
                ;//TODO : 예약 실패 로직(미완성).
            else
            {
                Response.Redirect("BookingList.aspx");
            }
            #endregion
        }

    }
}