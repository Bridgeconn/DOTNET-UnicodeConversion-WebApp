using NU2UWebApplication.Models;
using SILConvertersWordML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NU2UWebApplication.Controllers
{
    public class FileConversionController : Controller
    {
        private List<string> logMessages = new List<string>();

        // GET: FileConversion
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadSourceFiles(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                List<string> filePaths = new List<string>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var ext = Path.GetExtension(file.FileName);
                        var name = Path.GetFileNameWithoutExtension(file.FileName);
                        string fileName = Path.Combine(Server.MapPath("/InputFiles"), name + "(" + Guid.NewGuid() + ")" + ext);
                        file.SaveAs(fileName);
                        filePaths.Add(fileName);
                    }
                }


                // if all is well - move to convert
                if (filePaths.Count == files.Count())
                {
                    if(InitializeTheConversionProcess(filePaths))
                    {
                        ConvertersModel convertersModel = new ConvertersModel();

                        // from the 
                        if (Session["ProcessManager"] != null)
                        {
                            ProcessManager processManager = (ProcessManager)Session["ProcessManager"];
                            convertersModel.TargetFontList = ConverterFactory.GetInstalledFonts();
                            foreach (string sourceFont in processManager.GetSourceFonts())
                            {
                                var converterModel = new ConverterModel { SourceFont = sourceFont, IsConverterAvailable = processManager.IsConverterDefined(sourceFont) };
                                convertersModel.ConvertersList.Add(converterModel);
                            }
                        }
                        // logMessages could be used to present the user; // signalR for sending updates
                        return View("Conversion", convertersModel);
                    }
                    else
                    {
                        // logMessages could be used to present the user; // signalR for sending updates
                        // return with error
                    }
                }
            }
            catch(Exception exception)
            {
                // return with error
            }

            return View();
        }

        [HttpPost]
        public ActionResult Convert(ConvertersModel model)
        {
            ProcessManager processManager = (ProcessManager)Session["ProcessManager"];

            // update the target fonts
            foreach (ConverterModel converterModel in model.ConvertersList)
            {
                processManager.mapName2Font[converterModel.SourceFont] = converterModel.TargetFont;
            }

            // update converters

            // call the conversion
            if (CarryOutConversion())
            {
                var fileModel = new FilesConversionViewModel();

                if(processManager.mapInputOutputFiles.Count > 0)
                {                    
                    foreach(string inputFile in processManager.mapInputOutputFiles.Keys)
                    {
                        string originalFileName = Path.GetFileName(inputFile);
                        originalFileName = originalFileName.Substring(0, originalFileName.LastIndexOf("("));
                        originalFileName+= inputFile.Substring(inputFile.LastIndexOf(")") + 1);
                        fileModel.InputFileList.Add(originalFileName);
                        fileModel.OutputFileList.Add("/InputFiles/" + processManager.mapInputOutputFiles[inputFile]);
                    }
                }
              
                return View("Download", fileModel);
            }
            else
            {
                //show the error from the log messages

            }

            return View("Download");
        }


        #region private methods

        bool InitializeTheConversionProcess(List<string> filePaths)
        {
            bool result =false;
            try
            {
                bool isProceedToNext = false;
                ProcessRequest processRequest = new ProcessRequest(filePaths.ToArray(), true, new Logger(GetNotified, GetResponseFromUser/*Should be null*/), ExecutionMode.Web, false);
                ProcessManager processManager = new ProcessManager(processRequest);

                Session["ProcessManager"] = processManager;

                // ProcessResult is for the result for the steps & ProcessIntermediateResult is the in between messages
                ProcessResult resultMessage = processManager.Initialize(Server.MapPath("~/bin/Converters.xml"), Server.MapPath("~/bin/maps/"));

                switch (resultMessage.ResultType)
                {
                    case ResultType.Completed:
                        isProceedToNext = true;
                        break;
                    case ResultType.Failed:
                        // Send ErrorMessage
                        break;
                }

                if (isProceedToNext)
                {
                    resultMessage = processManager.LoadInputDocuments(processRequest.InputFiles);
                }

                switch (resultMessage.ResultType)
                {
                    case ResultType.Completed:
                        // get the font selection
                        isProceedToNext = true;
                        break;
                    case ResultType.Failed:
                        // Send ErrorMessage
                        break;
                }

                if (isProceedToNext)
                {
                    
                    result = true;
                    //resultMessage = processManager.AutoChooseConverters();
                }

                
            }
            catch (Exception exception)
            {

            }

            return result;
        }

        bool CarryOutConversion()
        {
            bool result = false;
            ProcessManager processManager = (ProcessManager)Session["ProcessManager"];
            ProcessResult resultMessage = processManager.ConvertAndSaveDocuments();
            if(resultMessage.ResultType == ResultType.Completed)
            {
                // prepare download model
                result = true;
            }
            
            return result;
        }

        void GetNotified(ProcessIntermediateResult processResult)
        {
            logMessages.Add(processResult.ToString());
        }

        UserResponse GetResponseFromUser(UserRequest userRequest)
        {
            Console.WriteLine(userRequest.Message);
            string result = Console.ReadLine();
            if (!string.IsNullOrEmpty(result))
            {
                return new UserResponse { ResultType = ResultType.Completed, Value = result };
            }

            return null;
        }

        #endregion

        /*
        // GET: FileConversion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileConversion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileConversion/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FileConversion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileConversion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FileConversion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileConversion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
