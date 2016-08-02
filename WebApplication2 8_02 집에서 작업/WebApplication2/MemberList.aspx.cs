using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using WebApplication2.dataSet;

namespace WebApplication2
{
    public partial class MemberList : System.Web.UI.Page
    {

        DBManager dbManager;
        Member LoginedMember;

        protected void Page_Load(object sender, EventArgs e)
        {
            //init
            LoginedMember = (Member)Session["MEMBER"];
            dbManager = (DBManager)Session["DBadmin"];

            int ColCount = 4;
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            tr.Cells.Add(td);
            List<string> TableNames = Theater.GetMemberNameToSting();

            //테이블 제목열
            //개선필요(isjeong 7. 27)
            for(int i =0; i < ColCount; i++)
            {
                td = new TableCell();
                switch(i)
                {
                    case 0:
                        td.Text = "ID";
                        break;
                    case 1:
                        td.Text = "이름";
                        break;
                    case 2:
                        td.Text = "나이";
                        break;
                    case 3:
                        td.Text = "잔여포인트";
                        break;
                }
                
                tr.Cells.Add(td);
            }


            tr.BackColor = Color.FromName("#ccccff");
            Table1.Rows.Add(tr);

            //멤버 목록 호출
            string Command = "SELECT * FROM Member WHERE ID <> @id";

            List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
            Params.Add(new Tuple<string, object>("@ID", "admin"));

            SqlDataReader reader = dbManager.GetDataList(Command, Params);
            List<dataSet.Member> Members = dataSet.Member.SqlDataReaderToMember(reader);

            foreach (var member in Members)
            {
                tr = new TableRow();
                td = new TableCell();

                //테이블의 열마다 체크박스를 추가하고 ID를 열의 ID와 매핑시킨다.
                CheckBox chkbox = new CheckBox();
                chkbox.ID = member.ID.ToString();
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
                            td.Text = member.ID;
                            break;
                        case 1:
                            td.Text = member.Name;
                            break;
                        case 2:
                            td.Text = member.Age;
                            break;
                        case 3:
                            td.Text = member.Point;
                            break;
                    }

                    tr.Cells.Add(td);
                }

                tr.BackColor = Color.White;
                Table1.Rows.Add(tr);
            }
        }
       
        //멤버삭제
        protected void Button_DeleteMember_Click(object sender, EventArgs e)
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
                    CheckBox chkbox = (CheckBox)tr.Cells[0].Controls[0];
                    if (chkbox.Checked)
                    {
                        string Command = "DELETE FROM Member where ID=@id";
                        List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                        Params.Add(new Tuple<string, object>("@id", chkbox.ID));
                        dbManager.DoCommand(Command, Params);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Data);
                    return;
                }
            }
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        //이전페이지
        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }
    }
}