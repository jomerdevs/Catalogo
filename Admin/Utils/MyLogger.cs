using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Utils
{
    public class MyLogger : ILogger
    {

        // Singleton pattern
        private static MyLogger instance;
        private static Logger logger;

        private MyLogger()
        {

        }

        public static MyLogger GetInstance()
        {
            if (instance == null)
                instance = new MyLogger();
            return instance;
        }

        private Logger GetLogger(string theLogger)
        {
            if (MyLogger.logger == null)
                MyLogger.logger = LogManager.GetLogger(theLogger);
            return MyLogger.logger;
        }

        public void Debug(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LoggerRule").Debug(message);
            else
                GetLogger("LoggerRule").Debug(message);
        }

        public void Error(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LoggerRule").Error(message);
            else
                GetLogger("LoggerRule").Error(message);
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LoggerRule").Info(message);
            else
                GetLogger("LoggerRule").Info(message);
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
                GetLogger("LoggerRule").Warn(message);
            else
                GetLogger("LoggerRule").Warn(message);
        }
    }
}