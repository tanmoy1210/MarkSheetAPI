using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MT
{
    public static class Logger
    {
        private static readonly string LOG_CONFIG_FILE = @"log4net.config";
        private static log4net.ILog _log = GetLogger(typeof(Logger));
        /// <summary>
        /// GetLogger 
        /// </summary>
        /// <param name="type">Logger Type</param>
        /// <returns>ILog</returns>
        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
        /// <summary>
        /// Debug method is used for set Debug type logger
        /// </summary>
        /// <param name="message">Logger message object</param>
        public static void Debug(object message)
        {
            SetLog4NetConfiguration();
            _log.Debug(message);
        }
        /// <summary>
        /// Error method is used for set Error type logger
        /// </summary>
        /// <param name="msg">Logger message object</param>
        public static void Error(object msg)
        {
            SetLog4NetConfiguration();
            _log.Error(msg);
        }
        /// <summary>
        /// Error method is used for set Error type logger
        /// </summary>
        /// <param name="msg">Logger message object</param>
        /// <param name="ex">Exception object</param>
        public static void Error(object msg, Exception ex)
        {
            _log.Error(msg, ex);
        }
        /// <summary>
        /// Error method is used for set only Exception
        /// </summary>
        /// <param name="ex">Exception object</param>
        public static void Error(Exception ex)
        {
            _log.Error(ex.Message, ex);
        }
        /// <summary>
        /// Info method is used for set Info type logger
        /// </summary>
        /// <param name="msg">Logger message object</param>
        public static void Info(object msg)
        {
            _log.Info(msg);
        }

        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }
}
