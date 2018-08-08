namespace SILConvertersWordML
{
    public class UserResponse
    {
        public UserResponse()
        {
            ResultType = ResultType.Failed;
        }

        public ResultType ResultType { get; set; }

        public object Value { get; set; }
    }
}