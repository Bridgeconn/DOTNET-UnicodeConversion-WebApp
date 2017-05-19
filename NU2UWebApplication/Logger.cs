using SILConvertersWordML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NU2UWebApplication
{
    public class Logger : IProcessMessenger
    {
            public delegate void GetNotifiedDelegate(ProcessIntermediateResult processResult);
            GetNotifiedDelegate GetNotified;

            public delegate UserResponse GetUserResponseDelegate(UserRequest processResult);
            GetUserResponseDelegate GetUserResponse;

            // TBD temporary arragement
            public Logger(GetNotifiedDelegate GetNotified, GetUserResponseDelegate GetUserResponse)
            {
                this.GetNotified = GetNotified;
                this.GetUserResponse = GetUserResponse;

            }
            public void LogMessage(ProcessIntermediateResult processIntermediateResult)
            {
                GetNotified?.Invoke(processIntermediateResult);
            }

            public UserResponse GetResponseFromUser(UserRequest processRequest)
            {
                return GetUserResponse?.Invoke(processRequest);
            }
    }
}