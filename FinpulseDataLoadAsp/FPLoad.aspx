<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPLoad.aspx.cs" ErrorPage="Error.aspx"   Inherits="FinpulseDataLoadAsp.FPLoad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta name="GENERATOR" content="MSHTML 10.00.9200.17609" />
  
    <link href="content/bootstrap-wysihtml5.css" rel="stylesheet" />
    <link href="content/wysiwyg-color.css" rel="stylesheet" />
    <link href="content/bootstrap.min.css" rel="stylesheet" />
     
    <link href="content/github.css" rel="stylesheet" /> 
     <link href="content/prettify.css" rel="stylesheet" /> 
    <script src="content/jquery-1.10.2.js"></script>
     
    <script src="content/bootstrap.min.js"></script>
    <script src="content/wysihtml5-0.3.0.js"></script>
    <script src="content/bootstrap-wysihtml5.js"></script>
    <script src="content/highlight.pack.js"></script>
    <script src="content/prettify.js"></script>
    <title></title>

    <style type="text/css">
        #MainContent_divInstruction .wysihtml5-toolbar
        {
            display: none;
        }
      
        
        .button
        {
            border: 1px solid red;
            background-color: #f8da92;
            padding: 1px 0px;
            cursor: pointer;
            cursor: hand;
            font-family: Tahoma;
            font-size: 8pt;
            margin-left: 0px;
            margin-top: 0px;
        }
        .mGrid
        {
            width: 100%;
            background-color: #fff; /* margin: 5px 0 10px 0;*/
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Tahoma;
            font-size: 8pt;
        }
        .button:hover
        {
            border-style: solid;
            background-color: #c41502;
            border-color: Black;
            color: White;
            border-width: 1px;
            padding: 1px 0px;
            cursor: pointer;
            cursor: hand;
            font-family: Tahoma;
            font-size: 8pt;
        }
        .FormLabel
        {
            background-color: #f0f0ed;
            font-family: Verdana;
            color: #000000;
            font-size: 10px;
            font-weight: normal;
        }
        .FormControls
        {
            background-color: White;
            font-family: Verdana;
            color: #000000;
            font-size: 10px;
            font-weight: normal;
        }
        .TextBox
        {
            font-family: verdana;
            font-size: 8pt;
        }
   
        #tbldev td
        {
            border: 1px solid black;
        }
        #tblProd td
        {
            border: 1px solid #b12c1a;
        }
        
        
        .FormLabel
        {
            background-color: #f0f0ed;
            font-family: Verdana;
            color: #000000;
            font-size: 10px;
            font-weight: normal;
        }
        .FormControls
        {
            background-color: White;
            font-family: Verdana;
            color: #000000;
            font-size: 10px;
            font-weight: normal;
        }
        .style4
        {
            width: 45%;
        }
        .style5
        {
            font-family: Verdana;
            color: #000000;
            font-size: 10px;
            font-weight: normal;
            width: 299px;
            background-color: White;
        }
        .style6
        {
            width: 299px;
            height: 43px;
        }
        .style7
        {
            font-family: Verdana;
            color: #000000;
            font-size: 10px;
            font-weight: normal;
            height: 43px;
            background-color: White;
        }
        #divInstruction
        {
            width: 1098px;
            height: 156px;
        }
    
        
        .overlay

{

position: fixed;

z-index: 999;

height: 100%;

width: 100%;

top: 0;

background-color: Black;

filter: alpha(opacity=40);

opacity: 0.6;

-moz-opacity: 0.8;

}
        
        .modal {
    position: fixed;
    top: 50%;
    left: 25%!important;
    z-index: 1050;
    overflow: auto;
    width: 1200px!important;
        }
        
       .modal.fade{
-webkit-transition:opacity .3s linear, top .3s ease-out;
-moz-transition:opacity .3s linear, top .3s ease-out;
-ms-transition:opacity .3s linear, top .3s ease-out;
-o-transition:opacity .3s linear, top .3s ease-out;
transition:opacity .3s linear, top .3s ease-out;
top:-25%;}
        .btn.jumbo
        {
            font-size: 20px;
            font-weight: normal;
            
            margin-right: 10px;
            -webkit-border-radius: 6px;
            -moz-border-radius: 6px;
            border-radius: 6px;
        }
        
        
        .container, .navbar-fixed-top .container, .navbar-fixed-bottom .container
        {
            padding:0px!important;
        }
        
        .modal-dialog
        {
            width: 1200px !important;
        }
        .modal
        {
        }
              
        .modal-header
        {
            padding-top: 5px !important;
            
            background-color: gray !important;
            height: 40px !important;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }
        
        
        
        .modal-body
        {
            width:1200px !important;
            padding: 0px !important;
        }
        
        .modal-title
        {
            color: floralwhite !important;
            font-family: Calibri !important;
        }
        .close
        {
            color: White !important;
            background-color: transparent !important;
            font-size: 1em;
        }
    </style>

    
    <script type="text/javascript">

        function showGif()
        {
            $('#imgLoading').show();
        }



        function modal() {

            $('#myModalReport').on('shown.bs.modal', function () {
                $(this).find('.modal-dialog').css({
                    width: '60%',
                    height: 'auto',
                    'max-height': '80%'

                });
            });

            updateInstruction();

            $('#modalhdng').html($('#<%=ddldataload.ClientID %> option:selected').text());

            setTimeout(function () {
                $('#myModalReport').modal('show');
                $('#myModalReport').show();
            }, 200);




        }

        function SaveInstruction() {
            var html = $(".textarea1").val() + '';
            html = html.replace(/\\/g, "\\\\");
            html = html.replace(/"/g, "“");


            $.ajax({
                url: "WebService.asmx/SaveInstruction",
                contentType: "application/json; charset=utf-8",
                type: 'post',
                data: '{instruction: "' + html + '",instructionId:"' + InstructionId + '" }',
                success: function (result) {
                    alert('Instruction updated/saved successfully');
                }
            });
        }

        function Admin() {
            $.ajax({
                url: "WebService.asmx/EnableEdit",
                contentType: "application/json; charset=utf-8",
                type: 'post',
                data: '{}',
                success: function (result) {
                    var s = result.d;
                    if (s == "YES") {
                        $("#pop").css("display", "block");
                    }
                }
            });
        }


        function updateInstruction() {

            var value = $('#<%=ddldataload.ClientID %> option:selected').text();
           

            if (value != "" && value != "<--Select-->") {
                $('#divInstruction').show();
                $.ajax({
                    url: "WebService.asmx/GetInstruction",
                    contentType: "application/json; charset=utf-8",
                    type: 'post',
                    data: '{ApplicationName: "' + value + '" }',
                    success: function (result) {

                        var InstructValue = JSON.parse(result.d)[0].InstructionText;
                        InstructValue = InstructValue.replace(/“/g, "\"");


                        InstructionId = JSON.parse(result.d)[0].sortorder;
                        $('.textarea1').data("wysihtml5").editor.setValue(InstructValue);
                        //                    $('.textarea').data("wysihtml5").editor.setValue(InstructValue);
                        $('.textarea').html(InstructValue);
                    }
                });
            }
            else {
                $('#divInstruction').hide();
            }
            Admin();
        }
        var InstructionId;

        $(function () {
            $('#imgLoading').hide();
            updateInstruction();
            $('#<%=ddldataload.ClientID %>').change(function () { 
                var val = this.value;
                var text = this.options[this.selectedIndex].text;
                updateInstruction();
                $('#lblError').text('')
            });



        });
    </script>


</head>
<body>
    <form id="form1" runat="server">


        
            <div style="padding-left: 20px">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="false" Font-Size="Small"></asp:Label><br />
            </div>
           
            
            <div id="Div1" runat="server" style="height: 503px;">
                <div id="divDev" runat="server" style="height: 23%;" visible="true" align="center">
                    <asp:Panel ID="pnl" class="FormControls" runat="server">
                        <table id="tbldev" style="width: 1150px;" cellpadding="2" cellspacing="1">
                            <tr style="background-color:darkgreen">
                                <td align="center"   colspan="2">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White" Font-Size="21px"
                                        Text="EMPLOYEE UPLOAD (Production)                    "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Data Load:"></asp:Label>
                                </td>
                                <td class="FormControls" align="left">
                                    <asp:DropDownList ID="ddldataload" Font-Names="verdana" AutoPostBack="true" OnSelectedIndexChanged="ddldataload_SelectedIndexChanged" runat="server" CssClass="FormControls"                                        
                                        Height="25px" Width="216px" style="float:left">
                                    </asp:DropDownList>
                                     
                                    
                                </td>
                             
                            </tr>
                            <tr>
                                <td class="style5" align="right">
                                    <asp:Label ID="Label3" runat="server" Text="File:"></asp:Label>
                                </td>
                                 <td class="FormControls" align="left">
                                        <asp:FileUpload ID="fuFP" runat="server" Height="24px" Width="214px" />&nbsp
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="test " ForeColor="White"></asp:Label>
                                </td>
                                <td class="style7" align="Left">
                                    
                                    <asp:Button ID="btnupload" runat="server" CssClass="button" Text=" Upload " Width="88px"  OnClientClick="showGif()"
                                        Height="22px"   OnClick="btnupload_Click"   />&nbsp&nbsp 
                                     <asp:Image ID="imgLoading"   ImageUrl="img/loading.gif" runat="server" />
                                    
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <br />
                <br />
                <div id="divInstruction" runat="server" style="height: 55%;margin-top:10px; margin-left:95px;" visible="true" align="center">
                    <table id="Table1" style="width: 1150px;" cellpadding="2" cellspacing="1">
                        <tr style="background-color:darkgreen">
                            <td align="center"   colspan="3">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" ForeColor="White"
                                    Text="Instructions             "></asp:Label>
                            </td>
                            <td   align="right" style="width:50px">
                                <a id="pop" type="button" onclick="modal()" href="#"  style="color: White">&nbsp;edit&nbsp;&nbsp;
                                </a>
                            </td>
                        </tr>
                        <tr>
                        <td style="width:100%"> <div style="text-align:left!important;border:1px solid gray;width:100%;padding-left:20px;padding-right:15px" class="textarea"></div></td>
                        </tr>
                    </table>
                    <asp:TextBox ID="txtinstruction" Enabled="false" runat="server" Height="249px" TextMode="MultiLine"
                        Width="1093px"  Visible="false"></asp:TextBox>
                </div>
                <br />
            </div>

        
 

   <div id="myModalReport" class="modal" role="dialog" style="display:none" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 id="modalhdng" class="modal-title">
                        Editor</h4>
                </div>
                <div class="modal-body" style="margin-top: 0px!important; margin-left: 0px!important">
                  
                       
                            <textarea class="textarea1" style="width: 99%; height: 300px; line-height: 18px;
                                font-size: 14px;" placeholder="Enter text ...">Enter text ...</textarea>
                        

                  
                    <script>



                        $('.textarea1').wysihtml5({
                            "stylesheets": ["content/wysiwyg-color.css", "content/github.css"],
                            "color": true,
                            "size": 'small',
                            "html": true,
                            "format-code": true
                        });
                         

                    </script>
                </div>
                <div class="modal-footer">
                    <div style="text-align: center; margin: 0px auto">
                      <br />
                        <button class="btn btn-success" onclick="SaveInstruction()">
                            Save</button></div>
                </div>
            </div>
        </div>
    </div>
 

        </form>
   
</body>
</html>
