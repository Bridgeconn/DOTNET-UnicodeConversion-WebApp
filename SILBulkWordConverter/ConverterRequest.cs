namespace SILConvertersWordML
{
    public class ConverterRequest
    {
        public ConverterRequest()
        {
            ConverterType = ConverterType.Unknown;
        }

        public ConverterType ConverterName { get; set; }
            
        public ConverterType ConverterType { get; set; }

        public string CodePage { get; set; }

        public string Language { get; set; }

        public string LHEncodingField { get; set; }

        public string RHEncodingField { get; set; }

        public bool IsLegacyToUnicode { get; set; }
    }
}