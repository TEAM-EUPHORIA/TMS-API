namespace TMS.API.UtilityFunctions
{ 
    public static class ServiceExceptions
    {
        public static void throwArgumentExceptionForId(string methodName)
        {
            throw new ArgumentException($"{methodName} requires a vaild Id");
        }
        public static void throwArgumentExceptionForObject(string methodName, string objectName)
        {
            throw new ArgumentException($"{methodName} requires a valid {objectName}");
        }
    }
}