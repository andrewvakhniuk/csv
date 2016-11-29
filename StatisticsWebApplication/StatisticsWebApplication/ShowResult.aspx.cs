using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StatisticsWebApplication.StatisticsServiceRef;

namespace StatisticsWebApplication
{
    public partial class ShowResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetResponse();
            }
        }
        /// <summary>
        /// Gets response from service
        /// </summary>
        private void GetResponse()
        {
            string op = Request.QueryString["op"];
            string no = Request.QueryString["uid"];

            op = (op ?? "0");
            no = (no ?? "0");

            StatisticsServiceSoapClient serviceclient = new StatisticsServiceSoapClient();

            switch (op)
            {
                case "0":
                    txtXml.Text = serviceclient.GetStatisticsJSON(no);
                    break;

                case "1":
                    txtXml.Text = serviceclient.GetStatisticsXML(no);
                    break;

                default:
                    break;
            }

            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatisticsPage.aspx");
        }
    }
}