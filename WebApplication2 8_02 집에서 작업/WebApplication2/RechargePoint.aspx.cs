using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class RechargePoint : System.Web.UI.Page
    {
        DBManager dbManager;
        Member LoginedMember;
        int RecentlyRemainPoint = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            int ColCount = 4;

            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBadmin"];

            //Table 셋팅
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            tr.Cells.Add(td);
            //테이블 이름중 1번은 계정명이므로 제외하고 테이블 제목으로 만든다.
            List<string> TableNames = dataSet.Point.GetMemberNameToSting();

            //테이블 제목열
            for(int i = 0; i < ColCount; i++)
            {
                td = new TableCell();
                td.Text = TableNames[i + 1];
                tr.Cells.Add(td);
            }
            tr.BackColor = Color.FromName("#ccccff");
            Table1.Rows.Add(tr);

            //테이블 내용을 채우기 전에 포인트 사용내역을 받아온다.
            string Command = "SELECT * FROM Point WHERE ID=@id ORDER BY Occuredatetime ASC";
            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@id", LoginedMember.ID));

            SqlDataReader reader = dbManager.GetDataList(Command, Params);
            List<dataSet.Point> points = dataSet.Point.SqlDataReaderToMember(reader);
            
            //포인트 사용내역만큼 열을 만들어 채워준다.
            foreach(var point in points)
            {
                tr = new TableRow();
                td = new TableCell();

                tr.Cells.Add(td);
                // 루프를 돌면서 각 셀을 설정한다.
                for (int j = 0; j < ColCount; j++)
                {
                    td = new TableCell();
                    //임시 
                    switch(j)
                    {
                        case 0:
                            td.Text = point.Occuredatetime;
                            break;
                        case 1:
                            td.Text = point.Usedvalue;
                            break;
                        case 2:
                            td.Text = point.Rechargedvalue;
                            break;
                        case 3:
                            td.Text = point.Remainvalue;
                            RecentlyRemainPoint = Convert.ToInt32(td.Text);
                            break;
                    }
                    
                    tr.Cells.Add(td);
                }

                tr.BackColor = Color.White;
                Table1.Rows.Add(tr);
            }

        }
        //포인트 충전
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox1.Text != string.Empty)
            {
                try
                {
                    int RechargeValue = Convert.ToInt32(TextBox1.Text);

                    string Command = "insert into Point(ID, Occuredatetime, Usedvalue, Rechargedvalue, Remainvalue)" + 
                        "values(@ID, @Occuredatetime, @Usedvalue, @Rechargedvalue, @Remainvalue)";

                    List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                    Params.Add(new Tuple<string, object>("@id", LoginedMember.ID));
                    Params.Add(new Tuple<string, object>("@Occuredatetime", Common.CSDateTiemToASPDateTime(DateTime.Now)));
                    Params.Add(new Tuple<string, object>("@Usedvalue", 0));
                    Params.Add(new Tuple<string, object>("@Rechargedvalue", RechargeValue));
                    Params.Add(new Tuple<string, object>("@Remainvalue", RecentlyRemainPoint + RechargeValue));

                    if(dbManager.DoCommand(Command, Params))
                    {
                        Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    }

                    //자동으로 페이지를 갱신한다.
                }
                catch (Exception ex)
                {
                    Console.WriteLine("RechargePointPange : " + ex.Data);
                    return;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("LoginMember.aspx"));
        }
    }
}