using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ChartFromCsv.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public object Import()
        {
            var ListData = new List<Entity>();
            try
            {
                HttpPostedFileBase myFile = Request.Files["file"];
                var pathDefault = Path.Combine(Server.MapPath("/DATA/UploadQueue/"));
                if (myFile != null)
                {
                    string path = Path.Combine(Server.MapPath("/DATA/UploadQueue/"), Path.GetFileName(myFile.FileName));
                    myFile.SaveAs(path);

                }
                var path1 = pathDefault + Path.GetFileName(myFile.FileName);
                //Read the contents of CSV file.  
                string csvData = System.IO.File.ReadAllText(path1);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        int i = 0;
                        var obj = new Entity();
                        //Execute a loop over the columns.  
                        foreach (string cell in row.Split(','))
                        {
                            if (i == 0)
                            {
                                obj.DateTime = cell.Trim().ToString();
                            }
                            else if (i == 1)
                            {
                                obj.CUST = cell.Trim().ToString();
                            }
                            else if (i == 2)
                            {
                                obj.SKU = cell.Trim().ToString();
                            }
                            else if (i == 3)
                            {
                                obj.SALE = cell.Trim().ToString();
                            }
                            else
                            {
                                obj.Forecast_Type = cell.Trim().ToString();
                            }
                            i++;
                        }
                        ListData.Add(obj);
                    }
                }
                var ListSKU = ListData.Select(x => x.SKU).Distinct().ToList();
                var ListCUST = ListData.Select(x => x.CUST).Distinct().ToList();
                return Json(new { Title = "Data retrieved successfully", Error = false, Object = ListData, ObjectSKU = ListSKU, ObjectCUST = ListCUST });
            }
            catch (Exception ex)
            {
                return Json(new { Title = "Data failed to retrieved", Error = true, Object = ex.Message });
            }
        }
        public class Entity
        {
            public string DateTime { set; get; }
            public string CUST { set; get; }
            public string SKU { set; get; }
            public string SALE { set; get; }
            public string Forecast_Type { set; get; }
        }
    }
}