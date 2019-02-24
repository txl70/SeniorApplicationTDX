using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using SeniorApplication.App_Data.Context;
using SeniorApplication.Models;
using SeniorApplication.Shared;
using Validator.Model;
using Validator.ValidatorHandler;

namespace SeniorApplication.Controllers
{
    public class UploadFileController : ApiController
    {
        private readonly SeniorApplicationContext _context;

        public UploadFileController(): this(new SeniorApplicationContext())
        {

        }

        public UploadFileController(SeniorApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            
            ValidationResult validationResult;
            var filename = string.Empty;
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];

                    if (!LogicApi.HasContents(postedFile))
                        throw new Exception($"{postedFile.FileName} File doesn't have content!");

                    var objectDefinitions = Shared.Definitions.GetProperties<Product>();

                    var validationHandler = LogicApi.GetValidator(postedFile.FileName,
                        objectDefinitions);

                    if (validationHandler == null)
                    {
                        throw new Exception("File format not supported!");
                    }

                    // Save file in server if greater max content length
                    if (postedFile.ContentLength > LogicApi.GetMaxContentLength())
                    {
                        filename = LogicApi.CreateFileInServerFolder(postedFile);
                        validationResult = validationHandler.ValidFile(filename, true, false);
                    }
                    // Upload file in memory
                    else
                    {
                        var rows = LogicApi.ConvertContentsInRows(postedFile);
                        validationResult = validationHandler.ValidFile(rows, true, false);
                    }

                    if (validationResult.ValidationError.Any(e => e.ErrorType == ErrorTypeDef.Fatal))
                    {
                        return Content(HttpStatusCode.BadRequest, new {
                                error = true,
                                message = "Fatal error found! The file was not upload!",
                                exception = validationResult.ValidationError
                            });
                    }

                    SaveData(validationResult, objectDefinitions);



                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "File Not Found!");
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
            finally
            {
                if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }

            return Ok(HttpStatusCode.Created);
        }

        private void SaveData(ValidationResult validationResult, List<ObjectDefinition> obj)
        {
            var products = new List<Product>();
            foreach (var row in validationResult.Rows)
            {
                var fields = row.Split(',');
                fields[0] = Guid.NewGuid().ToString();
                var product = new Product()
                {
                    NegociationId = Shared.Conversion.GetValue<Guid>(fields, obj, "NegociationId"),
                    Name = Shared.Conversion.GetValue<string>(fields, obj, "Name"),
                    Company = Shared.Conversion.GetValue<string>(fields, obj, "Company"),
                    CreateDate = Shared.Conversion.GetValue<DateTime>(fields, obj, "CreateDate"),
                    Price = Shared.Conversion.GetValue<double>(fields, obj, "Price"),
                    Quantity = Shared.Conversion.GetValue<int>(fields, obj, "Quantity"),
                };

                products.Add(product);
            }

            try
            {
                _context.Products.AddRange(products);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(
                    "Error was found when try to save information on database. Error message: "
                           + e.Message, e);
            }

        }
    }
}
