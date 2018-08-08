namespace SILConvertersWordML
{
    public class UserRequest
    {
        private string message;

        public UserRequest(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
        }
    }
}