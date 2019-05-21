namespace CommonLibrary.Infrastructure
{
    public class TopLevelActorNames
    {
        public const string TimecopService = "timecopService";
        public const string UserService = "userService";
    }
    public static class TopLevelActorNamesExtensions
    {
        public static string ToActorPath(this string ActorPath)
        {
            //string remoteAddress = string.Empty;

            //if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ActorFactor_RemoteAddress"]) == false)
            //    remoteAddress = ConfigurationManager.AppSettings["ActorFactor_RemoteAddress"];

            //return $"{remoteAddress}/user/{ActorPath}";
            return $"/user/{ActorPath}";
        }
    }
}
