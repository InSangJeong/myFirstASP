using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication2.dataSet;


namespace WebApplication2
{
    public partial class Main : System.Web.UI.Page
    {
        DBManager dbManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            //DB관리자를 1개 생성하여 세션으로 관리하여 무분별하게 생성되는 것을 막습니다.
            dbManager = new DBManager("server=(local);database=MovieBookingDB;Integrated Security=SSPI");
            Session["DBAdmin"] = dbManager;


        }

        //회원가입 클릭
        protected void BTN_NewMember_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("NewMember.aspx"));
        }
        //로그인 클릭
        protected void BTN_LOGIN_Click(object sender, EventArgs e)
        {
            //ID, Pass 항목 비어있으면 리턴
            if(string.IsNullOrEmpty(TXT_ID.Text) || string.IsNullOrEmpty(TXT_Pass.Text))
            {
                return;
            }
            //DBManager가 페이지로드에서 셋팅이 안되어 있을 경우.
            if(dbManager == null)
            {
                return;
            }
            //DB 매니저 객체 생성후 DB에서 검증후 로그인 결과창 도시.
            if (dbManager.dbConnection != null)
            {
                String Command = "SELECT * FROM Member WHERE ID=@ID AND Pass=@pass";
                List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                Params.Add(new Tuple<string, object>("@ID", TXT_ID.Text));
                Params.Add(new Tuple<string, object>("@Pass", TXT_Pass.Text));

                //보낸 명령에 대한 리더 객체를 얻는다.
                SqlDataReader Reader = dbManager.GetDataList(Command, Params);
                //리더 객체에 맞는 데이터 셋을 입혀 멤버객체를 받는다.
                List<Member> Members = Member.SqlDataReaderToMember(Reader);
                if(Members.Count == 1)
                {
                    //TODO : Member에 대한 정보를 LoginMember.aspx로 넘기고 세션 설정해야함.
                    Session["MEMBER"] = Members[0];

                    //관리자는 관리자 페이지 호출
                    if (Members[0].ID.Trim().ToLower() == "admin")
                    {
                        Response.Redirect(string.Format("LoginAdmin.aspx"));
                    }
                    else
                    {
                        Response.Redirect(string.Format("LoginMember.aspx"));
                    }
                }
                else
                {   
                    //ID가 0개이거나 2개이상이면 에러. 
                    //ID가 기본키라 2개이상은 될수가 없을텐데
                    return;
                }


            }

        }
    }
}