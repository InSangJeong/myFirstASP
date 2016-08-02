using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class NewMovie : System.Web.UI.Page
    {
        //TODO : error List
        // 1. 영화스케쥴 시간을 등록하면 지정했던 영화관 값이 사라짐.
        // 2. 영화스케쥴 시간이 실제로 존재하는 시간인지 + 상영시작, 종료시간 사이인지 + 영화플레이타임과 겹치지않는지
        // 3. 상영이미지 링크가 제대로 도시가 안될때가 있음
        // 4. 그외 나머지 값들 널일때도 등록이되고
        // 5. 상영 시작, 종료일시도 등록버튼처럼 처리하는게 나을듯.
        //
        DBManager dbManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = (DBManager)Session["DBadmin"];

        }

        protected void Button_cancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("MovieList.aspx");
        }


        protected void Button_Serch_Theater_Click(object sender, EventArgs e)
        {
            if (TextBox_StartMovie.Text == string.Empty || TextBox_endMovie.Text == string.Empty )
            {
                return;
            }
            else
            {
                Response.Cookies["Start"].Value = TextBox_StartMovie.Text;
                Response.Cookies["End"].Value = TextBox_endMovie.Text;

                string popupAddress = "window.open('" + "SearchAbleTheater.aspx" + "', 'popup_window', 'width=400,height=200,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", popupAddress, true);
            }
        }

      
        //새로운 영화 등록
        protected void Button_New_Click(object sender, EventArgs e)
        {
            //TODO List: 
            // 1. 새로운 영화의 ID를 지정하기위해 이전 영화의 MaxID 호출
            // 2. 영화등록
            // 3. 상영관의 좌석수를 얻기위해 값을 받아온다.
            // 4. 영화스케줄 등록
            // 5. 영화 스케쥴당 좌석 생성
            // 6. 이전화면으로 이동
            string command = "Select Top 1 * From Movie Order by MovieID DESC";
            String NewMovieID = "-1";
            String Seatcount = "0";
            String TheaterID = "0"; //상영관 ID
            String TheaterMaxRow = "0";//상영관 최대 열
            String TheaterMaxNumber = "0";//상영관 최대 행
            #region 1. 새로운 영화의 ID를 지정하기위해 이전 영화의 MaxID 호출
            SqlDataReader reader = dbManager.GetDataList(command, new List<Tuple<string, object>>());
            List<Movie> movies = Movie.SqlDataReaderToMember(reader);
            if(movies.Count == 0)
            {
                NewMovieID = "0";
            }
            else if(movies.Count == 1)
            {
                //가장 큰 ID에 +1 해준값이 새로운 영화의 ID
                NewMovieID = (Convert.ToInt32(movies[0].MovieID.Trim(' ')) + 1).ToString();
            }
            else
            {
                //영화 아이디 불러오기 실패
                return;
            }
            #endregion
            #region 2. 영화등록
            command = "insert into Movie(MovieID, Moviename, Playstartdatetime, Playenddatetime, Runningtime, Viewingclass, Movieposter)" +
                    "values(@MovieID, @Moviename, @Playstartdatetime, @Playenddatetime, @Runningtime, @Viewingclass, @Movieposter)";
            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@MovieID", NewMovieID));
            Params.Add(new Tuple<string, object>("@Moviename", TextBox_Name.Text));
            Params.Add(new Tuple<string, object>("@Playstartdatetime", TextBox_StartMovie.Text));
            Params.Add(new Tuple<string, object>("@Playenddatetime", TextBox_endMovie.Text));
            Params.Add(new Tuple<string, object>("@Runningtime", TextBox_RunningTIme.Text));
            Params.Add(new Tuple<string, object>("@Viewingclass", TextBox_ViewingClass.Text));
            if (this.FileUpload1.HasFile)
            {
                this.FileUpload1.SaveAs(HttpRuntime.AppDomainAppPath + "MoviePoster\\"
                    + NewMovieID.Trim() + "_" + TextBox_Name.Text.Trim() + "." + FileUpload1.FileName.Split('.')[1]);
                //this.FileUpload1.SaveAs(Constant.MoviePosterPath + NewMovieID.Trim() + "_" + TextBox_Name.Text.Trim() + "." + FileUpload1.FileName.Split('.')[1]);
            }
            
            Params.Add(new Tuple<string, object>("@Movieposter", NewMovieID.Trim() + "_" + TextBox_Name.Text.Trim() + "." + FileUpload1.FileName.Split('.')[1] ));
            if (!dbManager.DoCommand(command, Params))
            {
                Console.WriteLine("영화 추가 에러");
                return;
            }
            #endregion
            #region 3. 상영관의 좌석수를 얻기위해 값을 받아온다.
            Params.Clear();
            TheaterID = Request[this.TextBox_TheaterNumber.UniqueID].Trim();
            command = "Select * From Theater Where TheaterID = @TheaterID";
            Params.Add(new Tuple<string, object>("@TheaterID", TheaterID));
            reader = dbManager.GetDataList(command, Params);
            List<Theater> Theaters = Theater.SqlDataReaderToMember(reader);
            if(Theaters.Count != 1)
            {
                Console.WriteLine("같은 아이디의 극장이 여러개");
                return;
            }
            Seatcount = Theaters[0].Seatcount;
            TheaterMaxRow = Theaters[0].Seatrowcount;
            TheaterMaxNumber = Theaters[0].Seatnumbercount;
            #endregion
            #region 4. 영화스케줄 등록
            Params.Clear();
            string[] stringSeparators = new string[] { "\r\n" };
            string[] playtimes = TextBox_MoviePlayList.Text.Split(stringSeparators, StringSplitOptions.None);
            
            foreach (string playtime in playtimes)
            {
                command = "insert into Movieschedule(MovieID, TheaterID, Playtime, Seatbooked, Seatremained)" +
                   "values(@MovieID, @TheaterID, @Playtime, @Seatbooked, @Seatremained)";
                if (playtime == "")
                    continue;
                Params.Add(new Tuple<string, object>("@MovieID", NewMovieID));
                Params.Add(new Tuple<string, object>("@TheaterID", TheaterID));
                Params.Add(new Tuple<string, object>("@Seatbooked", "0"));
                Params.Add(new Tuple<string, object>("@Seatremained", Seatcount));
                Params.Add(new Tuple<string, object>("@Playtime", playtime.ToString()));

                //영화스케쥴 생성에 성공하였으니 5번:영화관스케쥴당좌석을 생성한다.
                if(dbManager.DoCommand(command, Params))
                {
                    #region 5. 영화 스케쥴당 좌석 생성
                    command = "insert into Seat(TheaterID, Seatrow, Seatnumber, Isbooked, Playtime)" +
                   "values(@TheaterID, @Seatrow, @Seatnumber, @Isbooked, @Playtime)";
                    //A = 1, B = 2 ..... Z = 26
                    int I_TheaterMaxRow = Common.AlphabetToNumber(TheaterMaxRow);
                    int I_TheaterMaxNumber = Convert.ToInt32(TheaterMaxNumber);
                    for(int Seatrow = 1; Seatrow <= I_TheaterMaxRow; Seatrow++)//좌석 열수만큼
                    {
                        for(int Seatnumber = 1; Seatnumber <= I_TheaterMaxNumber; Seatnumber++)//열당 좌석수만큼
                        {
                            Params.Clear();
                            Params.Add(new Tuple<string, object>("@TheaterID", TheaterID));
                            Params.Add(new Tuple<string, object>("@Seatrow", Common.NumberToAlphabet(Seatrow)));
                            Params.Add(new Tuple<string, object>("@Seatnumber", Seatnumber));
                            Params.Add(new Tuple<string, object>("@Isbooked", "false"));
                            Params.Add(new Tuple<string, object>("@Playtime", playtime.ToString()));
                            dbManager.DoCommand(command, Params);
                        }
                    }

                    #endregion
                }
                Params.Clear();
            }
            #endregion
            #region 6. 이전화면으로 이동
            Response.Redirect(string.Format("MovieList.aspx"));
            #endregion
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String playDayTime = TextBox_playDayTime.Text;
            string formatString = "yyyyMMddHHmmss";
            DateTime dt = DateTime.ParseExact(playDayTime, formatString, null);

            TextBox_MoviePlayList.Text += dt.ToString("yyyy-MM-dd HH':'mm':'ss \r\n");
        }
    }
}