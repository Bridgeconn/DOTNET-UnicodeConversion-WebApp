using NU2UWebApplication.Models;
using SILConvertersWordML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NU2UWebApplication.Controllers
{
    public class TextConversionController : Controller
    {
        // GET: TextConversion
        public ActionResult Index(TextConversionViewModel model)
        {
            TextConversionViewModel textConversionViewModel;
            if(model!=null)
            {
                textConversionViewModel = model;
            }
            else
            {
                textConversionViewModel = new TextConversionViewModel();
            }
            
            loadTextViewModelWithFonts(textConversionViewModel);
            return View(textConversionViewModel);
        }

        [HttpPost]
        public ActionResult Convert(TextConversionViewModel textConversionViewModel)
        {
            if (textConversionViewModel != null)
            {
                if (!string.IsNullOrEmpty(textConversionViewModel.NonUnicodeText))
                {
                    string resultText;
                    textConversionViewModel.NonUnicodeText = textConversionViewModel.NonUnicodeText.Trim();

                    ConverterFactory.SafeConvert(new ConverterRequest
                    {
                        IsLegacyToUnicode = true,
                        LHEncodingField = textConversionViewModel.SelectedNonUnicodeFont,
                        RHEncodingField = "Unicode"
                    }, textConversionViewModel.NonUnicodeText, out resultText);

                    if (!string.IsNullOrEmpty(resultText))
                    {
                        textConversionViewModel.UnicodeText = resultText;
                    }

                    return RedirectToAction("Index", textConversionViewModel);
                }
                else if (!string.IsNullOrEmpty(textConversionViewModel.UnicodeText))
                {
                    string resultText;
                    textConversionViewModel.UnicodeText = textConversionViewModel.UnicodeText.Trim();

                    ConverterFactory.SafeConvert(new ConverterRequest
                    {
                        IsLegacyToUnicode = false,
                        LHEncodingField = textConversionViewModel.SelectedNonUnicodeFont,
                        RHEncodingField = "Unicode"
                    }, textConversionViewModel.UnicodeText, out resultText);

                    if (!string.IsNullOrEmpty(resultText))
                    {
                        textConversionViewModel.NonUnicodeText = resultText;
                    }

                    return RedirectToAction("Index", textConversionViewModel);
                }
            }

            // Error - 
           return RedirectToAction("Index");
        }

        // GET: TextConversion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TextConversion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TextConversion/Create
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

        // GET: TextConversion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TextConversion/Edit/5
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

        // GET: TextConversion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TextConversion/Delete/5
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

        #region Private Methods

        private void loadTextViewModelWithFonts(TextConversionViewModel model)
        {
            model.UnicodeFontList = ConverterFactory.GetInstalledFonts();
            if (!ConverterFactory.IsLoaded)
            {
                ConverterFactory.initialize(Server.MapPath("~/bin/Converters.xml"), Server.MapPath("~/bin/maps/"));
            }

            var list = ConverterFactory.GetSupportedNonUnicodeFonts();
            if (list != null)
            {
                model.NonUnicodeFontList.AddRange(list);
            }
        }
        #endregion
    }
}
