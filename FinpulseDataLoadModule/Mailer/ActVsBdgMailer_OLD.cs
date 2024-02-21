using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;

namespace FinpulseDataLoadModule.Data
{
	public class ActVsBdgMailerOLD
	{
		public ActVsBdgMailerOLD()
		{
		}

		public static void GenerateMailer()
		{
			string item = ConfigurationManager.AppSettings["EmailCSV"];
			string[] strArrays = item.Split(new char[] { ',' });
			DataTable data = ActVsBdgMailerOLD.GetData();
			string dateTimeStamp = ActVsBdgMailerOLD.GetDateTimeStamp();
			bool flag = data.Rows.OfType<DataRow>().Any<DataRow>((DataRow k) => Convert.ToDouble(k["EmpPu_Diff_Total"]) > 0);
			bool flag1 = data.Rows.OfType<DataRow>().Any<DataRow>((DataRow k) => Convert.ToDouble(k["ProjPu_Diff_Total"]) > 0);
			if ((flag ? false : !flag1))
			{
				Console.WriteLine("No Changes in the Mailer");
			}
			else
			{
				strArrays.ToList<string>().ForEach((string email) => {
					string hTML = ActVsBdgMailerOLD.GetHTML(data, email, dateTimeStamp);
                    ActVsBdgMailerOLD.SendMail(email, string.Concat("Budget vs Actuals Sales support team size as on (", dateTimeStamp, ")"), hTML);
				});
			}
			Console.ForegroundColor = ConsoleColor.Green;
		}

		private static DataTable GetData()
		{
			string item = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select * from Tbl_SalesDuBudget", new SqlConnection(item)));
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			return dataTable;
		}

		private static string GetDateTimeStamp()
		{
			string item = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(" select distinct asofdate from emp  ", new SqlConnection(item)));
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			DateTime dateTime = (DateTime)dataTable.Rows[0][0];
			return dateTime.ToString("dd-MMM-yyyy");
		}

        static string GetPath([CallerFilePath]string fileName = null)
        {
            return fileName;
        }

        private static string GetHTML(DataTable dt, string to, string asonDate)
		{
            string currDir = Path.GetDirectoryName( GetPath());

            string str = Path.Combine(currDir, "mailTemplate.html");
			string str1 = File.ReadAllText(str);
			StringBuilder stringBuilder = new StringBuilder();
			string str2 = "<td>{0}</td>";
			string str3 = "<td style='background-color:#ea2e2e'>{0}</td>";
			foreach (DataRow row in dt.Rows)
			{
				stringBuilder.AppendLine("<tr class='Medium0' > ");
				foreach (DataColumn column in dt.Columns)
				{
					string str4 = str2;
					string str5 = string.Concat(row[column]);
					if (column.ColumnName == "EmpPu_Diff_Total")
					{
						if (Convert.ToDouble(str5) > 0)
						{
							str4 = str3;
						}
					}
					if (column.ColumnName == "ProjPu_Diff_Total")
					{
						if (Convert.ToDouble(str5) > 0)
						{
							str4 = str3;
						}
					}
					stringBuilder.AppendFormat(str4, str5);
				}
				stringBuilder.AppendLine("</tr> ");
			}
			string str6 = stringBuilder.ToString();
			string str7 = str1.Replace("{0000}", to).Replace("{1111}", str6).Replace("{2222}", asonDate);
			return str7;
		}

		private static void SendMail(string To, string subject, string body)
		{
			MailMessage mailMessage = new MailMessage();
			try
			{
				mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
				mailMessage.Subject = subject;
				mailMessage.IsBodyHtml = true;
				mailMessage.Body = body;
				mailMessage.To.Add(new MailAddress(string.Concat(To, "@infosys.com")));
				SmtpClient smtpClient = new SmtpClient()
				{
					Host = ConfigurationManager.AppSettings["Host"],
					EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"])
				};
				NetworkCredential networkCredential = new NetworkCredential()
				{
					UserName = ConfigurationManager.AppSettings["UserName"],
					Password = ConfigurationManager.AppSettings["Password"]
				};
				smtpClient.UseDefaultCredentials = true;
				smtpClient.Credentials = networkCredential;
				smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
				try
				{
					smtpClient.Send(mailMessage);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Mail Sent Successfully to {0}", To);
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(exception.Message);
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
			finally
			{
				if (mailMessage != null)
				{
					((IDisposable)mailMessage).Dispose();
				}
			}
		}
	}
}