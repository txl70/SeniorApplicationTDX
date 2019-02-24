using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Validator.Model;
using Validator.ValidatorHandler;

namespace SeniorApplication.Shared
{
    public static class LogicApi
    {
        public static string[] ConvertContentsInRows(HttpPostedFile postedFile)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(postedFile.InputStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        public static string CreateFileInServerFolder(HttpPostedFile postedFile)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/LoadFiles/" + postedFile.FileName);
            postedFile.SaveAs(filePath);
            return filePath;
        }

        public static int GetMaxContentLength()
        {
            int result = 0;
            if (!int.TryParse(WebConfigurationManager.AppSettings["MaxContentSizeMemory"], out result))
            {
                result = 1000000;
            }

            return result;
        }


        public static bool HasContents(HttpPostedFile file)
        {
            return file != null && file.ContentLength > 0;
        }

        public static IValidatorHandler GetValidator(string filename, List<ObjectDefinition> obj)
        {
            if (filename.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
                return new CsvValidatorHandler(obj);
            else
                return null;
        }
    }
}