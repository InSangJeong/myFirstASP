using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class NewMember : System.Web.UI.Page
    {
        //TODO : 유효성검사
        DBManager dbManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = (DBManager)Session["DBadmin"];
        }

        protected void BTN_Submit_Click(object sender, EventArgs e)
        {
            //DBManager가 페이지로드에서 셋팅이 안되어 있을 경우.
            if (dbManager == null)
            {
                return;
            }
            if(dbManager.dbConnection != null)
            {
                //추가할 회원 값 셋팅
                List<Tuple<string,object>> Params = new List<Tuple<string, object>>();
                Params.Add(new Tuple<string, object>("@ID", TXT_ID.Text));
                Params.Add(new Tuple<string, object>("@Pass", TXT_PASS.Text));
                Params.Add(new Tuple<string, object>("@Name", TXT_NAME.Text));
                Params.Add(new Tuple<string, object>("@Age", 27));
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
                if(dbManager.DoCommand(Command, Params))
                {
                    //TODO : 가입 성공 메시지 추가.(미완성)

                    //DB 커넥션을 종료하는데 필요한지?
                    //dbManager.Disconnet();

                    //메인페이지 이동.
                    Response.Redirect(string.Format("Main.aspx"));
                }
                else
                {
                    ;//TODO : 가입 실패 로직(미완성).
                }

                

               
            }
            else
            {
                //DB Connection Error
            }



          }
        protected void CheckRequiredFieldValidatorNullOrEmpty(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    //get which input TextBox will be validated.
            //    RequiredFieldValidator Sender = (RequiredFieldValidator)sender;
            //    TextBox tx = (TextBox)this.FindControl(Sender.ControlToValidate);
            //    if (string.IsNullOrEmpty(tx.Text))
            //    {
            //        Sender.ErrorMessage =
            //            "이 항목은 필수 사항 입니다.";
            //    }
            //}

        }
        protected void CheckRequiredFieldValidatorSamePassword(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    //get which input TextBox will be validated.
            //    RequiredFieldValidator Sender = (RequiredFieldValidator)sender;
            //    TextBox Passtx = (TextBox)this.FindControl(Sender.ControlToValidate);
            //    TextBox PassChecktx = (TextBox)this.FindControl(RequiredFieldValidator3.ControlToValidate);
            //    if (!Passtx.Text.ToString().Equals(PassChecktx.Text.ToString()))
            //    {
            //        Sender.ErrorMessage =
            //            "패스워드가 같지 않습니다..";
                    
            //    }
            //}
        }
    }
}