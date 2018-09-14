using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SilEncConverters40;
using ECInterfaces;
using System.Drawing;

namespace SILConvertersWordML
{
    public static class ConverterFactory
    {
        private static UnicodeConverters xmlUnicodeConverters;
        private static List<DirectableEncConverter> unicodeConverters;
        private static string configurationFilePath;
        private static string configurationMapsPath;

        public static void initialize(string configurationMapFilePath, string configurationMapsFolderPath)
        {
            configurationFilePath = configurationMapFilePath;
            configurationMapsPath = configurationMapsFolderPath;
            // read the xml file, have the XMLUnicodeConverters object populated
            if (!IsConfigurationFilePathValid)
            {
                throw (new FileNotFoundException("ConverterFactory's configuration file is not found!"));
            }

            unicodeConverters = new List<DirectableEncConverter>();

            string xml = File.ReadAllText(configurationFilePath);
            xmlUnicodeConverters = xml.ParseXML<UnicodeConverters>();
        }

        public static bool IsConfigurationFilePathValid
        {
            get
            {
                return File.Exists(configurationFilePath);
            }
        }

        public static bool LoadConverter(ConverterRequest converterRequest)
        {
            DirectableEncConverter encConverter = null;
            bool isFound = false;
            ConverterType converterType = ConverterType.Unknown;

            try
            {
                if (xmlUnicodeConverters == null)
                {
                    //initialize();
                    throw (new Exception("Converter hasn't been loaded yet!"));
                }
                else    // check if the required converter has been already instantiated.
                {
                    string key = converterRequest.LHEncodingField;
                    if (unicodeConverters.Exists(
                        x => x.GetEncConverter.LeftEncodingID.ToLower() == converterRequest.LHEncodingField.ToLower()
                        &&
                        x.GetEncConverter.DirectionForward == converterRequest.IsLegacyToUnicode))
                    {
                        return true; // unicodeConverters[key] as IEncConverter;
                    }
                }

                if (converterRequest.ConverterType == ConverterType.Unknown) // Autosearch
                {
                    if (CreateConverter(converterRequest))
                    {
                        return true;
                    }
                }
                else // user specified
                {
                    if (converterRequest.ConverterType == ConverterType.CP)
                    {
                        return true;
                    }
                    else if (converterRequest.ConverterType == ConverterType.TEC)
                    {

                    }
                }
            }
            catch (Exception exception)
            {
                // log the  exception
            }

            return false;
        }

        public static bool CreateConverter(ConverterRequest converterRequest)
        {
            DirectableEncConverter encConverter = null;
            bool isFound = false;
            // Else create one from the information provided
            foreach (UnicodeConvertersTECConverter unicodeConverter in xmlUnicodeConverters.TECConverters)
            {
                string key = converterRequest.LHEncodingField;
                if (converterRequest.LHEncodingField.ToLower() == unicodeConverter.LHEncoding.ToLower())
                {
                    if (!converterRequest.IsLegacyToUnicode)
                    {
                        if (unicodeConverter.ToAndFro == "True")
                        {
                            isFound = true;
                        }
                    }
                    else
                    {
                        isFound = true;
                    }
                }

                if (isFound)
                {
                    EncConverters aECs = new EncConverters();
                    IEncConverter tecConverter = aECs.InstantiateIEncConverter("SilEncConverters40.TecEncConverter", null);
                    ConvType conversionType = ConvType.Legacy_to_from_Unicode;
                    string lhs = converterRequest.LHEncodingField; //"SD708";
                    string rhs = "Unicode"; //converterRequest.RHEncodingField;
                    int pt = 0;
                    string tecFilePath = System.IO.Path.Combine(configurationMapsPath, unicodeConverter.Path);
                    tecConverter.Initialize(unicodeConverter.ConverterName, tecFilePath, ref lhs, ref rhs, ref conversionType, ref pt, 0, 0, false);
                    tecConverter.DirectionForward = converterRequest.IsLegacyToUnicode;
                    encConverter = new DirectableEncConverter(tecConverter);
                    unicodeConverters.Add(encConverter);

                    return true;
                }
            }

            foreach (UnicodeConvertersCPConverter unicodeConverter in xmlUnicodeConverters.CPConverters)
            {
                string key = converterRequest.LHEncodingField;
                if (converterRequest.LHEncodingField.ToLower() == unicodeConverter.LHEncoding.ToLower())
                {
                    if (!converterRequest.IsLegacyToUnicode)
                    {
                        if (unicodeConverter.ToAndFro == "True")
                        {
                            isFound = true;
                        }
                    }
                    else
                    {
                        isFound = true;
                    }
                }

                if (isFound) // TBD
                {
                    EncConverters aECs = new EncConverters();
                    IEncConverter tecConverter = new TecEncConverter(); //aECs.InstantiateIEncConverter("SilEncConverters40.TecEncConverter", null);
                    ConvType conversionType = ConvType.Legacy_to_from_Unicode;
                    string lhs = null;//converterRequest.LHEncodingField; //"SD708";
                    string rhs = null;//"Unicode"; //converterRequest.RHEncodingField;
                    int pt = (int) ProcessTypeFlags.DontKnow;
                    string tecFilePath = "\"" + System.IO.Path.Combine(configurationMapsPath, unicodeConverter.Path) + "\"";
                    tecConverter.Initialize(unicodeConverter.ConverterName, tecFilePath, ref lhs, ref rhs, ref conversionType, ref pt, 0, 0, true);
                    tecConverter.DirectionForward = converterRequest.IsLegacyToUnicode;
                    encConverter = new DirectableEncConverter(tecConverter);
                    unicodeConverters.Add(encConverter);

                    return true;
                }
            }

            return false;
        }

        ///  Based on the converterRequest 
        ///  1) read from the XMLUnicodeConverters
        ///  2) Instantiate and return the DirectableEncConverter  
        public static DirectableEncConverter GetConverter(ConverterRequest converterRequest)
        {
            DirectableEncConverter converter = null;
            try
            {
                if (xmlUnicodeConverters == null)
                {
                    //initialize();
                    throw (new Exception("Converter hasn't been loaded yet!"));
                    LoadConverter(converterRequest);
                }

                if (unicodeConverters.Exists(
                    x => x.GetEncConverter.LeftEncodingID.ToLower() == converterRequest.LHEncodingField.ToLower()
                    &&
                    x.GetEncConverter.DirectionForward == converterRequest.IsLegacyToUnicode))
                {
                    return unicodeConverters.First(
                    x => x.GetEncConverter.LeftEncodingID.ToLower() == converterRequest.LHEncodingField.ToLower()
                    &&
                    x.GetEncConverter.DirectionForward == converterRequest.IsLegacyToUnicode);
                }
            }
            catch (Exception exception)
            {
                // log the  exception
            }

            return converter;
        }

        public static bool IsConverterDefined(ConverterRequest converterRequest)
        {
            bool isDefined = false;

            isDefined = unicodeConverters.Exists(
                    x => x.GetEncConverter.LeftEncodingID.ToLower() == converterRequest.LHEncodingField.ToLower()
                    &&
                    x.GetEncConverter.DirectionForward == converterRequest.IsLegacyToUnicode);
            if (!isDefined)
            {
                return LoadConverter(converterRequest);
            }

            return isDefined;
        }

        public static bool IsLoaded
        {
            get
            {
                return (xmlUnicodeConverters != null);
            }
        }

        public static List<string> GetSupportedNonUnicodeFonts()
        {
            if (IsConfigurationFilePathValid)
            {
                List<string> nonUnicodeFontList = new List<string>();

                foreach (UnicodeConvertersTECConverter xtecConverter in xmlUnicodeConverters.TECConverters)
                {
                    nonUnicodeFontList.Add(xtecConverter.LHEncoding);
                }

                return nonUnicodeFontList;
            }

            return null;
        }

        public static List<string> GetInstalledFonts()
        {
            List<string> fonts = new List<string>();

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);
            }

            return fonts;
        }

        public static bool SafeConvert(ConverterRequest converterRequest, string inputText, out string resultText)
        {
            bool result = false;
            if(IsConverterDefined(converterRequest))
            {
                var encConverter = GetConverter(converterRequest);

                if (encConverter != null)
                {
                    resultText = encConverter.Convert(inputText);
                    return true;
                }
            }

            // error
            resultText = null;
            return result;
        }

        //public static EncConverters EncConvertersList
        //{
        //    get
        //    {
        //        EncConverters list = new EncConverters();
        //        foreach(string key in unicodeConverters.Keys)
        //        {
        //            list.Add(unicodeConverters[key].Name, unicodeConverters[key]);
        //        }

        //        return  list;
        //    }
        //}

        // This is where the respective EncConverter is mapped for font name
        //public static void DefineConverter(ConverterRequest converterRequest, DirectableEncConverter aEC)
        //{
        //    if (IsConverterDefined(converterRequest))
        //        unicodeConverters.Remove(converterRequest.LHEncodingField);
        //    unicodeConverters.Add(converterRequest.LHEncodingField, aEC);
        //}

        //public static DirectableEncConverter GetConverter(string strFontName)
        //{
        //    return unicodeConverters[strFontName];
        //}
    }
}
