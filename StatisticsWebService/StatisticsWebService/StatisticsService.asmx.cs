using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Xml; 

namespace StatisticsWebService
{
    /// <summary>
    /// Summary description for StatisticsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StatisticsService : System.Web.Services.WebService
    {
        /// <summary>
        /// Uploads Specified CSV File
        /// </summary>
        /// <param name="f"></param>
        /// <param name="fileName"></param>
        [WebMethod]
        public void UploadFile(byte[] f, string fileName)
        {
            try
            {

                MemoryStream ms = new MemoryStream(f);
                string cacheName = "csvtable";
                string serverPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UploadedFile/") + fileName;
                FileStream fs = new FileStream(serverPath, FileMode.Create);

                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();

                Helper.CacheData(serverPath, cacheName);

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Returs a List<Statistics> type of cached object 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Statistics> GetStatistics()
        {
            List<Statistics> listCache= new List<Statistics>();
            if (HttpRuntime.Cache["csvtable"] != null)
            {
                listCache = (List<Statistics>)HttpRuntime.Cache["csvtable"];
            }
            return listCache;
        }

        /// <summary>
        /// Creates JSON from cached object
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStatisticsJSON(string no="0")
        {
            List<Statistics> listCache = new List<Statistics>();
            List<Statistics> listNew = new List<Statistics>();
            if (HttpRuntime.Cache["csvtable"] != null)
            {
                listCache = (List<Statistics>)HttpRuntime.Cache["csvtable"];

                listNew = Helper.QueryList(listCache, no);

                //Statistics[] statarry = listNew.ToArray();
                return new JavaScriptSerializer().Serialize(listNew);
            }
            return string.Empty;
        }
        /// <summary>
        /// Creates XML from cached obect
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetStatisticsXML(string no = "0")
        {
            List<Statistics> listCache = new List<Statistics>();
            List<Statistics> listNew = new List<Statistics>();
            if (HttpRuntime.Cache["csvtable"] != null)
            {
                listCache = (List<Statistics>)HttpRuntime.Cache["csvtable"];

                listNew = Helper.QueryList(listCache, no);


                StringWriter stringWriter = new StringWriter();

                 XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

                 XmlSerializer serializer = new XmlSerializer(listNew.GetType());
                 serializer.Serialize(xmlWriter, listNew);
  
                 string xmlResult = stringWriter.ToString();



                 return xmlResult;
            }
            return null;
        }


    }
}
