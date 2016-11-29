using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StatisticsWebApplication.StatisticsServiceRef;
using System.ComponentModel;

namespace StatisticsWebApplication
{
    public partial class StatisticsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 StatisticsServiceSoapClient serviceclient = new StatisticsServiceSoapClient();
                 PopulateControls(serviceclient.GetStatistics());
            }   
        }
        /// <summary>
        /// Uploads csv file to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    if (FileUploadControl.PostedFile.ContentType == "application/CSV" || FileUploadControl.PostedFile.ContentType == "application/vnd.ms-excel")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 102400)
                        {
                            string filename = Path.GetFileName(FileUploadControl.FileName);
                            string serverPath = Server.MapPath("~/") + filename;

                            FileUploadControl.SaveAs(serverPath);

                            var bytes = File.ReadAllBytes(serverPath);
                            StatisticsServiceSoapClient serviceclient = new StatisticsServiceSoapClient();
                            
                            serviceclient.UploadFile(bytes,filename);  
                            lblStatus.Text = "File uploaded!";
                            List<StatisticsServiceRef.Statistics> lst = serviceclient.GetStatistics();
                            
                            PopulateControls(lst);

                        }
                        else
                            lblStatus.Text = "The file has to be less than 100 kb!";
                    }
                    else
                        lblStatus.Text = "Only CSV files are accepted!";
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "The following error occured: " + ex.Message;
                }
            }
        }
        /// <summary>
        /// Populates the controls of the page
        /// </summary>
        /// <param name="lst"></param>
        private void PopulateControls(List<StatisticsServiceRef.Statistics> lst)
        {
            var listQuery = lst.Select(p => new { No = p.UID, DisplayText = p.Name.ToString() + " " + p.Surname }).Distinct();

            dllPerson.DataSource = listQuery;
            dllPerson.DataTextField = "DisplayText";
            dllPerson.DataValueField = "No";
            lblSelect.Text = "Please Select a User To Find His/Her Statistics";
            
            dllPerson.DataBind();

        }
        /// <summary>
        /// Redirects to ShowResult page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            string qstr = @"?op=" + rblType.SelectedIndex + "&uid=" + dllPerson.SelectedValue;
            Response.Redirect("ShowResult.aspx"+qstr);
        }
    }
}