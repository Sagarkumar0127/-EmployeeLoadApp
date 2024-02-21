<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Title="Error-Page"
    Inherits="iSHARE.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <script src="content/jquery-1.7.2.min.js"></script> 
    <link href="content/Site.css" rel="stylesheet" />
    <title></title>
    <link rel="icon" href="Images/favicon.ico" type="image/vnd.microsoft.icon" />
    <script type="text/javascript">
        $(document).ready(function () {
            var div = document.getElementById('divgrid');
            if (div != null) {
                div.style.width = (window.screen.width - 65) + 'px';
                div.style.height = (window.screen.height - 280) + 'px'; // address bar, favorites, tool bar , status bar 
            }
        });
            
    </script>
    <style type="text/css">
        .GridDock
        {
            overflow-x: auto;
            overflow-y: auto;
            padding: 0 0 0 0;
        }
    </style>
</head>
<body style="background-color: white">
    <form id="form1" runat="server" style="background-color: white">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellspacing="0" cellpadding="0" align="center" width="100%" style="padding-right: 0px;
        padding-left: 0px; padding-bottom: 0px; padding-top: 0px; margin: 0px; background-color: #FFFFFF;">
        <tr valign="bottom" style="height: 50px;">
            <td style="width: 100%">
                <div style="background-image: url('img/banner_middle.jpg'); background-repeat: no-repeat;
                    background-color: #a9a9a9; width: 100%; height: 50px">
                    <table width="100%">
                        <tr>
                            <td style="width: 75%; font-size: large; color: #FFFFFF;" valign="middle">
                                <asp:Label Text=" <b> iSHARE </b> Application" runat="server" ID="lbl"></asp:Label>
                            </td>
                            <td align="right" valign="middle" style="width: 25%; padding-right: 1px;">
                                <asp:Image ID="test" Width="235px" Height="40px" runat="server" ImageUrl="~/img/infosys.JPG" />
                                <asp:Label ID="Label3" ForeColor="White" Font-Size="12" Font-Bold="true" Text="iSHARE"
                                    runat="server" />
                                <img src="img/dot.png" height="10" alt="Alternate Text" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <div id="divgrid" class="GridDock" runat="server">
                    <table border="0" cellspacing="0" cellpadding="0" align="center" width="100%" style="height: 424px">
                        <tr>
                            <td style="width: 0px; height: 415px;">
                            </td>
                            <td style="width: 100%; height: 415px; background-color: White" valign="middle">
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <div style="background-color: #f9fcff; border: 3px solid #e6eaf1; height: 250px;
                                                width: 550px">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                            <img src="img/error1.png" alt="Alternate Text" />
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;
                                                                        <asp:Label ID="lbl1" Text="Error" Font-Size="15px" runat="server"> </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblMessage" Text="An Unexpected Error Occured while performing this operation.  
Please Try again later. If problem persists Please contact administrator  " Font-Size="12px" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        Error registered @
                                                                        <asp:Label ID="lblTime" runat="server" Font-Size="12px" Text=""> </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp; &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        Click
                                                                        <asp:HyperLink CssClass="MsLink" ID="HyperLink3" Font-Underline="true" NavigateUrl="~/fpload.aspx"
                                                                            runat="server"> here </asp:HyperLink>
                                                                        to launch the application again.
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 55px">
                                                        </td>
                                                        <td>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 30px" align="left">
                                                                        <img src="img/back.png" alt="Alternate Text" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:LinkButton CssClass="MsLink" ID="btnback" OnClientClick="history.go(-1); return false;"
                                                                            runat="server" Text="Go back to site"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="background-image: url(images/banner_middle.JPG); background-repeat: no-repeat;
                background-color: #a9a9a9; height: 21px" align="center" colspan="3">
                <table>
                    <tr>
                        <td style="width: 650px" align="center">
                            <asp:Label ID="copyright" ForeColor="white" runat="server" Text=" Copyright © 2013 Infosys Limited. All rights reserved."></asp:Label>
                        </td>
                        <td style="width: 200px">
                            &nbsp;
                            <asp:Label ID="Label4" runat="server" Text="|" ForeColor="White" Height="15px"></asp:Label>&nbsp;
                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="http://sparsh/V1/" Target="_blank"
                                ForeColor="White" Font-Underline="false">Sparsh</asp:HyperLink>
                            <asp:Label ID="Label1" runat="server" Text="|" Height="15px" ForeColor="White"></asp:Label>&nbsp;
                            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="http://sparsh/v1/aspx/SparshWebapps.aspx"
                                ForeColor="White" Target="_blank" Font-Underline="false">Webapps</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
