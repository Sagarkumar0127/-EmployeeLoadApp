using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
 
    public class Logger
    {

        static string dateFormat = "ddMMM";
        public enum LoggerType { Error, Info, Warning }

        /// <summary>
        /// creates a folder for today in the format "01jan"
        /// </summary>
        /// <param name="folderLocation">folder location till the last directort eg: c://users//test</param>
        static string CreateAndOrGetFolderName()
        {
            string folderLocation = "";// get from config
            folderLocation = System.Configuration.ConfigurationManager.AppSettings["LoggerLocation"];
            string folderName = DateTime.Now.ToString(dateFormat);
            string folder = folderLocation + "\\" + folderName;
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

       static string CreateAndeOrGetLogFile(string userName, string folderLocation)
        {

            if (!File.Exists(folderLocation + "\\" + userName + ".html"))
            {
                string content = "  <html><head><title></title><style type=\"text/css\">" +
                      " .GridLines  {    border-top: 1px solid #f0f0f0;    border-bottom: 1px solid #f0f0f0; }" +
                   " .body1 {    background: white;    font-size: 8pt;    font-family: \"Tahoma\";    margin: 0px;    padding: 0px;    color: #696969; } " +
          "  .GridHeader {    background-color: #273c51;    color: white;    font-family: Verdana, Arial, sans-serif;    font-size: 8pt;    font-weight: normal;    height: 20px;    padding: 3px 8px 3px 4px;    text-align: left;    vertical-align: top;    }" +
         " .GridTable {    background-color: gray;    border-top: #f9f9f9 1px solid;    border-top-color: #b5bdc7;    border-right: white 1px solid;    font-family: Verdana, Helvetica, sans-serif;    font-size: 8pt;    font-weight: normal;    height: 22px;    padding-bottom: 1px; } " +
         " .Error { background-color: #e3a8a8;  border-bottom: 1px solid #f0f0f0;  color: black;    }" +
          ".Info { background-color: #c7dec9;  color: Black;     }" +
          " .Warning { background-color: #f7f76c;  color: #000000;    }" +
     " </style></head><body class=\"body1\">   <form id=\"form1\" runat=\"server\" >" +
                    //"  <table width=\"100%\" >  <tr>  <td class=\"GridTable\" > " +
     " <table width=\"100%\" class=\"GridTable\"  cellpadding=\"0\" cellspacing=\"1\" >" +
          "<tr class=\"GridHeader\"  >" +
              "<td style=\"width: 7%\">" +
              "Time  </td> <td style=\"width: 20%\">   Exception Type   </td>   <td>" +
                  "Message, Stack Trace , Information   </td>  </tr>";
                // File.Create(folderLocation + "\\" + userName + ".html");
                File.AppendAllText(folderLocation + "\\" + userName + ".html", content);
            }
            return folderLocation + "\\" + userName + ".html";

        }



        public static void LogErrorToServer(LoggerType errorType, string userName,  Exception ex)
        { 
           
            string folder = CreateAndOrGetFolderName();  
            string file = CreateAndeOrGetLogFile(userName, folder);
            string informationHTML = GetInformationFromFrames(GetAllFramesInfo(ex));
            string contents = CreateRow(errorType, ex.GetType().Name,  ex.Message, informationHTML );
            File.AppendAllText(file, contents);
        }

        private static string GetInformationFromFrames(List<FrameInfo> list)
        {
            string val = "";
            foreach (var item in list)
            {
                string fileName = Path.GetFileName(item.File);
                string directory = Path.GetDirectoryName(item.File);
                string htmlFile = directory + "&nbsp;&#8680;&nbsp;<b>" + fileName;
                val += htmlFile + "</b>&nbsp;&#8680;&nbsp;<b>" + item.Method + "</b> &nbsp;&#8680;&nbsp;" + "<b>" + item.Line + "</b> <br>";
                        
            }
            return val;
        }






        static string CreateRow(LoggerType typeOfError, string sourceFile,   string message, string information)
        {
            
            string temp = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append(@" <tr class=""" + typeOfError.ToString() + @""">");

            // builder.Append(" <tr class=\"GridLines\" >");
            builder.AppendFormat("<td>{0}</td>", string.Format("{0:hh:mm:ss tt}", DateTime.Now));
            builder.AppendFormat(" <td>  <b>  {0}  </b> </td>", sourceFile); 
            builder.AppendFormat("<td> <b>  {0}  </b> <br /> ", message);
            builder.AppendFormat("  {0}   ", information);
            builder.Append(" </td></tr>");
            return builder.ToString();
        }

        static List<FrameInfo> GetAllFramesInfo(Exception ex)
        {
            List<FrameInfo> lst = new List<FrameInfo>();
            StringBuilder sb = new StringBuilder();
            var stackTrace = new StackTrace(ex, true);
            for (int i = stackTrace.FrameCount - 1; i > 0; i--)
            {
                StackFrame frame = stackTrace.GetFrame(i);
                int line = frame.GetFileLineNumber();
                if (line > 0)
                    lst.Add(new FrameInfo() { File = frame.GetFileName(), Method = frame.GetMethod().Name, Line = line });
                     
            }

            return lst;
        }

        public class FrameInfo
        {
            public int Line { get; set; }
            public string File { get; set; }
            public string Method { get; set; }
        }

    }
 
