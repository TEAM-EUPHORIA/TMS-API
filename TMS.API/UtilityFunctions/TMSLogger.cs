namespace TMS.API.UtilityFunctions
{
    public static class TMSLogger
    {
        public static void ServiceInjectionFailed(InvalidOperationException ex, ILogger _logger, string ControllerName, string method)
        {
            _logger.LogCritical($"An Critical error occured in {ControllerName} Controller inside this method {method}. Please check the program.cs. It happend due to failure of Service injection");
            _logger.LogTrace(ex.ToString());
        }
    }
}