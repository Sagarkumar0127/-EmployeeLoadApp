<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMailer.aspx.cs" Inherits="FinpulseDataLoadAsp.SendMailer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="content/bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" />
 </head>
<body>
    <form id="form1" runat="server">
         <h1 style="padding-left: 30px;font-family: 'Segoe UI'">Employee Mailer</h1>
        <div id="divNotify" style=" height:30px; font-family: 'Segoe UI'; font-size:20px; border:1px solid crimson; width:800px; margin-left:15px">
        &nbsp;    Last Mailer Sent by <b style="color: green" id="bSentBy" runat="server">xxxxxxx</b> on <b style="color: crimson" id="bSentOn" runat="server">04-Jul-2022 03.42 PM</b>
        </div>
        <br /><br /> 
        <div style="margin-left:20px;">
        <asp:Button ID="btnSendMailer" runat="server" Text="Send Mailer" class="btn btn-info" OnClick="btnSendMailer_Click" />
            </div>
      
    </form>
</body>
</html>
