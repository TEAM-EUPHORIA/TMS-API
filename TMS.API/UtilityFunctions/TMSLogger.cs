namespace TMS.API.UtilityFunctions
{
    public static class TMSLogger
    {
        public static void ServiceInjectionFailedAtController(InvalidOperationException ex, ILogger logger, string ControllerName, string method)
        {
            logger.LogCritical($"An Critical error occured in {ControllerName} Controller inside this method {method}. Please check the program.cs. It happend due to failure of Service injection");
            logger.LogTrace(ex.ToString());
        }
        public static void ServiceInjectionFailedAtService(InvalidOperationException ex, ILogger logger, string serviceName, string method)
        {
            logger.LogCritical($"An Critical error occured in {serviceName} Service inside this method {method}. Please check the program.cs. It happend due to failure of Service injection");
            logger.LogTrace(ex.ToString());
        }
        public static void ArgumentExceptionInDictionary(ArgumentException ex, ILogger logger, string serviceName, string method)
        {
            logger.LogInformation($"An argument exception occured in {serviceName} Service inside this method {method}.");
            logger.LogTrace(ex.ToString());
        }
        public static void DbException(Exception ex, ILogger logger,string method)
        {
            logger.LogCritical($"A DB exception occured in IUnitOfWork inside this method {method}.");
            logger.LogTrace(ex.ToString());
        }
        public static void GeneralException(Exception ex, ILogger logger,string method)
        {
            logger.LogCritical($"An exception occured in this method {method}");
            logger.LogTrace(ex.ToString());
        }
    }
}