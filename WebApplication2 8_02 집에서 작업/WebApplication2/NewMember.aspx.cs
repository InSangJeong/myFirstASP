using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class NewMember : System.Web.UI.Page
    {
        DBManager dbManager;
        //ID확인 했는지 구분하는 변수
        bool isIDChecked;

        protected void Page_Load(object sender, EventArgs e)
        {

            isIDChecked = Convert.ToBoolean(ViewState["IDChecked"]);
            if(isIDChecked == true)
            {
                TXT_ID.Enabled = false;
            }
            dbManager = (DBManager)Session["DBadmin"];

        }
    
        protected bool ChkisValid()
        {
            if (TXT_ID.Text.Equals("")) {
                Common.ShowMessage(this, @"아이디를 입력하세요.");
                return false;
            }
            if(!isIDChecked )
            {
                Common.ShowMessage(this, @"계정확인 버튼을 클릭하세요.");
                return false;
            }
            if(!TXT_PASS.Text.Trim().Equals(TXT_PASSCHECK.Text.Trim()))
            {
                Common.ShowMessage(this, @"패스워드가 같지 않습니다.");
                return false;
            }
            if(TXT_PASS.Text.Length < 5 || TXT_PASS.Text.Length > 16)
            {
                Common.ShowMessage(this, @"패스워드는 4~15자리로 하세요.");
                return false;
            }
            if (TXT_PASS.Text.Split(new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(',')' }).Count() <= 1)
            {
                Common.ShowMessage(this, @"특수문자를 입력하세요.");
                return false;
            }
            if(TXT_NAME.Equals(""))
            {
                Common.ShowMessage(this, @"성명을 입력하세요.");
                return false;
            }
            if(TXT_BIRTHDAY.Text.Length <= 5)
            {
                Common.ShowMessage(this, @"생일을 확인하세요.");
                return false;
            }
            if (TXT_SEX.Text.Equals("1") || TXT_SEX.Text.Equals("2") || TXT_SEX.Text.Equals("3") || TXT_SEX.Text.Equals("4"))
            {
               
                return true;
            }
            else
            {
                Common.ShowMessage(this, @"주민번호 뒷자리를 확인하세요");
                return false;
            }
            
        }

        protected void BTN_Submit_Click(object sender, EventArgs e)
        {
            if (ChkisValid())
            {
                //DBManager가 페이지로드에서 셋팅이 안되어 있을 경우.
                if (dbManager == null)
                {
                    return;
                }
                if (dbManager.dbConnection != null)
                {
                    //추가할 회원 값 셋팅
                    List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                    Params.Add(new Tuple<string, object>("@ID", TXT_ID.Text));
                    Params.Add(new Tuple<string, object>("@Pass", TXT_PASS.Text));
                    Params.Add(new Tuple<string, object>("@Name", TXT_NAME.Text));
                    DateTime dt = DateTime.Now;
                    int bornyear = Convert.ToInt32(TXT_BIRTHDAY.Text.Substring(0, 2));
                    if(Convert.ToInt32(TXT_SEX.Text) > 2)
                    {
                        bornyear += 2000;
                    }
                    else
                    {
                        bornyear += 1900;
                    }
                    Params.Add(new Tuple<string, object>("@Age", dt.Year - bornyear));

                    Params.Add(new Tuple<string, object>("@Birthday", TXT_BIRTHDAY.Text));
                    bool Sex = false;
                    if (TXT_SEX.Text == "1" || TXT_SEX.Text == "3")
                    {
                        Sex = true;
                    }
                    Params.Add(new Tuple<string, object>("@Sex", Sex));
                    Params.Add(new Tuple<string, object>("@Point", 0));
                    Params.Add(new Tuple<string, object>("@Address", TXT_ADDRESS.Text));
                    Params.Add(new Tuple<string, object>("@Phone", TXT_PHON.Text));
                    string Command = "insert into Member(ID, Pass, Name, Age, Birthday, Sex, Point, Address, Phone)" +
                        "values(@ID, @Pass, @Name, @Age, @Birthday, @Sex, @Point, @Address, @Phone)";
                    //추가 명령 전송
                    if (dbManager.DoCommand(Command, Params))
                    {
                        Common.ShowMessage(this, @"가입 완료.");
                        //메인페이지 이동.
                        Response.Redirect(string.Format("Main.aspx"));
                    }
                    else
                    {
                        Common.ShowMessage(this, @"가입 실패. 관리자에게 문의하세요.");
                        return;
                    }
                }
                else
                {
                    //DB Connection Error
                }
            }

        }
        //계정확인
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (dbManager == null)
            {
                return;
            }
            if (dbManager.dbConnection != null)
            {
                string Command = "Select Top 1 * From Member Where ID = @ID";
                List<Tuple<string, object>> Params = new List<Tuple<string, object>>();
                Params.Add(new Tuple<string, object>("@ID", TXT_ID.Text.Trim()));
                SqlDataReader reader = dbManager.GetDataList(Command, Params);
                List<dataSet.Member> Members = dataSet.Member.SqlDataReaderToMember(reader);
                if (Members.Count == 0)
                {
                    Common.ShowMessage(this, @"아이디 확인 완료.");
                    ViewState["IDChecked"] = true;
                }
                else
                {
                    Common.ShowMessage(this, @"중복아이디.");
                }

            }
        }
    }
}