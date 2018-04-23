using System;
using System.Globalization;
using System.IO;
using ILogger = Castle.Core.Logging.ILogger;
using NLogCore = NLog;

namespace Z17.Core.Logging
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class LoggerManager : MarshalByRefObject, ILogger
    {
        protected internal NLogCore.ILogger Logger { get; set; }

        private LoggerManager(NLogCore.ILogger logger)
        {
            var fullPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "configs\\nlog.config");
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(fullPath);
            }
            NLogCore.LogManager.Configuration = new NLogCore.Config.XmlLoggingConfiguration(fullPath);

            Logger = logger;
        }

        public static LoggerManager CreateLogger(string name)
        {
            var logger = NLogCore.LogManager.GetLogger(name);
            return new LoggerManager(logger);
        }

        public static LoggerManager CreateLogger<T>() where T : class
        {
            return CreateLogger(typeof(T).FullName);
        }

        public bool IsTraceEnabled => Logger.IsEnabled(NLogCore.LogLevel.Trace);

        public bool IsDebugEnabled => Logger.IsEnabled(NLogCore.LogLevel.Debug);

        public bool IsErrorEnabled => Logger.IsEnabled(NLogCore.LogLevel.Error);

        public bool IsFatalEnabled => Logger.IsEnabled(NLogCore.LogLevel.Fatal);

        public bool IsInfoEnabled => Logger.IsEnabled(NLogCore.LogLevel.Info);

        public bool IsWarnEnabled => Logger.IsEnabled(NLogCore.LogLevel.Warn);

        public ILogger CreateChildLogger(string loggerName)
        {
            return new LoggerManager(NLogCore.LogManager.GetLogger(Logger.Name + "." + loggerName));
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Debug(Func<string> messageFactory)
        {
            Logger.Debug(messageFactory);
        }

        public void Debug(string message, Exception exception)
        {
            Logger.Debug(exception, message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Logger.Debug(CultureInfo.InvariantCulture, format, args);
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            Logger.Debug(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Debug(formatProvider, format, args);
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Debug(exception, formatProvider, format, args);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Error(Func<string> messageFactory)
        {
            Logger.Error(messageFactory);
        }

        public void Error(string message, Exception exception)
        {
            Logger.Error(exception, message);
        }

        public void Error(Exception exception)
        {
            Logger.Error(exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Logger.Error(CultureInfo.InvariantCulture, format, args);
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            Logger.Error(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Error(formatProvider, format, args);
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Error(exception, formatProvider, format, args);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public void Fatal(Func<string> messageFactory)
        {
            Logger.Fatal(messageFactory);
        }

        public void Fatal(string message, Exception exception)
        {
            Logger.Fatal(exception, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Logger.Fatal(CultureInfo.InvariantCulture, format, args);
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            Logger.Fatal(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Fatal(formatProvider, format, args);
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Fatal(exception, formatProvider, format, args);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Info(Func<string> messageFactory)
        {
            Logger.Info(messageFactory);
        }

        public void Info(string message, Exception exception)
        {
            Logger.Info(exception, message);
        }

        public void Info(Exception exception)
        {
            Logger.Info(exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Logger.Info(CultureInfo.InvariantCulture, format, args);
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            Logger.Info(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Info(formatProvider, format, args);
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Info(exception, formatProvider, format, args);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Warn(Func<string> messageFactory)
        {
            Logger.Warn(messageFactory);
        }

        public void Warn(string message, Exception exception)
        {
            Logger.Warn(exception, message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Logger.Warn(CultureInfo.InvariantCulture, format, args);
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            Logger.Warn(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Warn(formatProvider, format, args);
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Warn(exception, formatProvider, format, args);
        }

        public void Trace<T>(T value)
        {
            Logger.Trace(value);
        }
    }
}
