using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class LoginMember : System.Web.UI.Page
    {
        Member LoginedMember;
        DBManager dbManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBAdmin"];
            //상영중인 영화를 이미지 객체애 매칭합니다.

            
            string[] filePaths = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "MoviePoster");
            if(filePaths != null)
            {
                //3개의 이미지만 등록한다. 어떤 이미지를 등록할지에 대한 로직은 추후에.(미완성)
                foreach (string path in filePaths)
                {
                    String FileName = Path.GetFileName(path);
                    if(Image1.ImageUrl == "")
                    {
                        Image1.ImageUrl = Constant.MoviePosterPath + FileName;
                    }
                    else if (Image2.ImageUrl == "")
                    {
                        Image2.ImageUrl = Constant.MoviePosterPath + FileName;
                    }
                    else
                    {
                        Image3.ImageUrl = Constant.MoviePosterPath + FileName;
                    }
                }
            }

            LabelName.Text = LoginedMember.Name;
            //테이블 내용을 채우기 전에 포인트 사용내역을 받아온다.
            //string Command = "SELECT * FROM ( SELECT top 1 * FROM Point WHERE ID=@id ORDER BY Occuredatetime DESC)";
            string Command = "Select top 1 * from Point where ID=@id order by Occuredatetime desc";
            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@id", LoginedMember.ID));

            SqlDataReader reader = dbManager.GetDataList(Command, Params);
            List<dataSet.Point> points = dataSet.Point.SqlDataReaderToMember(reader);
            
            if(points.Count == 0)
                LabelPoint.Text = "0";
            else
            {
                LabelPoint.Text = points[0].Remainvalue;
            }
            //LabelPoint.Text = LoginedMember.Point;


        }

        //포인트 충전 버튼
        protected void ButtonRechargePointClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("RechargePoint.aspx"));
        }
        //예매하기
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("Booking.aspx"));
        }
        //예매 내역확인
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("BookingList.aspx"));
        }
    }
}