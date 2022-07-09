namespace TMS.API.UtilityFunctions
{
    public static class TMSLogger
    {
        public static void ServiceInjectionFailedAtController(InvalidOperationException ex, ILogger _logger, string ControllerName, string method)
        {
            _logger.LogCritical($"An Critical error occured in {ControllerName} Controller inside this method {method}. Please check the program.cs. It happend due to failure of Service injection");
            _logger.LogTrace(ex.ToString());
        }
        public static void ServiceInjectionFailedAtService(InvalidOperationException ex, ILogger _logger, string serviceName, string method)
        {
            _logger.LogCritical($"An Critical error occured in {serviceName} Service inside this method {method}. Please check the program.cs. It happend due to failure of Service injection");
            _logger.LogTrace(ex.ToString());
        }
        public static void ArgumentExceptionInDictionary(ArgumentException ex, ILogger _logger, string serviceName, string method)
        {
            _logger.LogInformation($"An argument exception occured in {serviceName} Service inside this method {method}.");
            _logger.LogTrace(ex.ToString());
        }
    }
}