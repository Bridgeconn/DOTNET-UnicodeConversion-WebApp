using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILConvertersWordML
{
    /* Each ProcessManager will have one ILogger attached to it. 
       This ILogger object will notify the client's ILogger object.
       This is used as a logger/progress listener so that the client application can have its own logger */
    public interface IProcessMessenger
    {
        void LogMessage(ProcessIntermediateResult resultMessage);
        UserResponse GetResponseFromUser(UserRequest processRequest);
    }
}
