using Serilog;
using System;

namespace ServiceSalesProcessor.Business.Functions
{
    public static class SendEmail
    {
        public static void Send(string email, string subject, string message)
        {
            Log.Information($"Sending email to {email}...");
            WaitFor(2);
            Log.Information("Email sended!");
        }

        private static void WaitFor(int seconds)
        {
            var timeout = DateTime.Now.AddSeconds(seconds);
            while (timeout.CompareTo(DateTime.Now) >= 0) { }
        }
    }
}