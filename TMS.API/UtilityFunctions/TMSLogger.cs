namespace TMS.API.UtilityFunctions
{
    public static class TMSLogger
    {
        public static void DbRelatedProblemCheckTheConnectionString(InvalidOperationException ex, ILogger logger)
        {
            logger.LogCritical($"Please check the connection string in appsettings.json");
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