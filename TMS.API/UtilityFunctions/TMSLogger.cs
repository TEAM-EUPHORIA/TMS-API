namespace TMS.API.UtilityFunctions
{
    public static class TMSLogger
    {
        public static void ServiceInjectionFailed(InvalidOperationException ex, ILogger _logger, string ControllerName, string method)
        {
            _logger.LogCritical($"An Critical error occured in {ControllerName} Controller inside this method {method}. Please check the program.cs. It happend due to failure of Service injection");
            _logger.LogTrace(ex.ToString());
        }
        public static void DbContextInjectionFailed(InvalidOperationException ex, ILogger _logger, string ServiceName, string method)
        {
            _logger.LogCritical($"An Critical error occured in {ServiceName} services inside this method {method}. Please check the program.cs, context class and connection string. It happend due to failure of Dbcontext injection");
            _logger.LogTrace(ex.ToString());
        }
        public static void EfCoreExceptions(Exception ex, ILogger _logger, string ServiceName, string method)
        {
            _logger.LogCritical($"An Critical error occured in {ServiceName} services inside this method {method}. this happend due to some SQL exception. check the log for sepcific exception.");
            _logger.LogTrace(ex.ToString());
        }

    }
}