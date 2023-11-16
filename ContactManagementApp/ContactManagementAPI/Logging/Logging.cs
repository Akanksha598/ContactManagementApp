using ContactManagementUtility;

namespace ContactManagementAPI.Logging;

public class Logging : ILogging
{
    public void Log(string message, string type)
    {
        if (type == Constants.Error)
        {
            Console.WriteLine("Error - " + " " + message);
        }
        else
        {
            Console.WriteLine(message);
        }
    }
}
