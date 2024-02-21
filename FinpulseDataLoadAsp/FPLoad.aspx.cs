using FinpulseDataLoadModule;
using FinpulseDataLoadModule.Common;
using FinpulseDataLoadModule.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinpulseDataLoadAsp
{
    public partial class FPLoad : BasePage
    {
        private FPUIData objFPUIData;
        private readonly string ExcelUploadFolder = "UploadOperations";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                if (!Page.IsPostBack)
                {
                    objFPUIData = new FPUIData();
                    var lst = objFPUIData.GetUploadItems();
                    ddldataload.DataSource = lst;
                    ddldataload.DataTextField = "Key";
                    ddldataload.DataValueField = "Value";
                    ddldataload.DataSource = lst;
                    ddldataload.DataBind();
                    ddldataload.Items.Insert(0, "<--Select-->");
                }
                 
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                string text = ddldataload.SelectedItem.Text;
                string value = ddldataload.SelectedItem.Value; //File Name
                string fileName = fuFP.FileName;
                if (!this.IsValidLoad(text, value, fileName))
                    return;


                var excelFolder = Server.MapPath(ExcelUploadFolder);
                string path = Path.Combine(excelFolder, fileName);
                System.IO.File.Delete(path);
                fuFP.SaveAs(path);
                IEmployeeLoadFile objLoad = Factory.GetFPLoadObject(text.ToEnum<FPLoadDDLEnum>());
                objLoad.UserID = UserIdentity.CurrentUser;
                objLoad.Load(path);
                DisplayMessage(EnumMessage.Success, "Loaded successfully!!");
            }
            catch (Exception)
            {
                
                throw;
            }
             

        }

        private bool IsValidLoad(string text, string value, string fileName)
        {
            try
            {
                
                if (text == "<--Select-->")
                {  
                    DisplayMessage(EnumMessage.Error, "Please select an Item");
                     return false;
                }
                if (!fuFP.HasFile)
                {
                    DisplayMessage(EnumMessage.Error, "Please select a file");
                    return false;
                }
                    
                if (Path.GetExtension(fileName).ToLower() != ".xlsx")
                {
                    DisplayMessage(EnumMessage.Error, "File Extension should be [.xlsx]");
                    return false;
                }
                   
                if (!fileName.Contains(value.Replace(".xlsx", "")))
                {
                    DisplayMessage(EnumMessage.Error, "Please check the File Name for this item");
                    return false;
                }
                return true;
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void DisplayMessage(EnumMessage type, string content)
        {
            System.Drawing.Color color = System.Drawing.Color.CadetBlue;
            switch (type)
            {
                case EnumMessage.Error:
                    color = System.Drawing.Color.Red;
                    break;
                case EnumMessage.Success:
                    color = System.Drawing.Color.Green;
                    break;
                case EnumMessage.Info:
                    color = System.Drawing.Color.CadetBlue;
                    break;
                default:
                    break;
            }
            lblMessage.ForeColor = color;
            lblMessage.Text = content;


            string script = "<script language=JavaScript>alert('" + content + "');</script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script);

       
        }

        protected void ddldataload_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

       


    }

    public enum EnumMessage
    { Error, Success, Info }
}