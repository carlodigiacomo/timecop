using Akka.Configuration;
using System.IO;

namespace TimecopAPI
{
    public class ConfigurationLoader
    {
        public static Config Load() => LoadConfig("akka.hocon");

        private static Config LoadConfig(string configFile)
        {
            var clusterConfig = ConfigurationFactory.ParseString(File.ReadAllText(configFile));

            var akkaConfig = clusterConfig.GetConfig("akka");

            return akkaConfig;
            //if (File.Exists(configFile))
            //{
            //    string config = File.ReadAllText(configFile);
            //    return ConfigurationFactory.ParseString(config);
            //}


            //return Config.Empty;
        }
    }
}
