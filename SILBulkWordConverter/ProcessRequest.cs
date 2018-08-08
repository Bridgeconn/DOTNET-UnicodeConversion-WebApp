using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILConvertersWordML
{   
    /*
     * For each conversion request a ProcessRequest is expected.
     * This would have all the settings that are required for the conversion.
     */
    public class ProcessRequest
    {
        string[] inputFiles;
        string[] outputFiles;
        bool leaveXMLFileInFolder;
        bool useLinqConversion;
        bool singleStep;
        bool isLegacyToUnicode;
        ExecutionMode executionMode;

        ConversionElements conversionElements;
        ConversionMode conversionMode;
        IProcessMessenger logger;

        public ProcessRequest(string[] inputFiles, bool isLegacyToUnicode, IProcessMessenger logger, ExecutionMode executionMode, bool leaveXMLFileInFolder = true)
        {
            this.logger = logger;
            this.inputFiles = inputFiles;
            this.leaveXMLFileInFolder = leaveXMLFileInFolder;
            this.useLinqConversion = true;
            this.IsLegacyToUnicode = isLegacyToUnicode;
            this.executionMode = executionMode;
            // ConversionElements.FontsAndStyles TBD
        }

        public string[] InputFiles
        {
            get
            {
                return inputFiles;
            }
        }

        public string[] OutputFiles
        {
            get
            {
                return outputFiles;
            }

            set
            {
                outputFiles = value;
            }
        }

        public bool LeaveXMLFileInFolder
        {
            get
            {
                return leaveXMLFileInFolder;
            }
        }

        public ConversionElements ConversionElements
        {
            get
            {
                return conversionElements;
            }
        }

        public ConversionMode ConversionMode
        {
            get
            {
                return conversionMode;
            }
        }

        public bool UseLinqConversion
        {
            get
            {
                return useLinqConversion;
            }
        }

        public bool SingleStep
        {
            get
            {
                return singleStep;
            }
        }

        public IProcessMessenger processMessenger
        {
            get
            {
                return logger;
            }
        }

        public bool IsLegacyToUnicode
        {
            get
            {
                return isLegacyToUnicode;
            }

            set
            {
                isLegacyToUnicode = value;
            }
        }

        public ExecutionMode ExecutionMode
        {
            get
            {
                return executionMode;
            }
        }
    }
}
