// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Logging
{
    #region

    using log4net;
    using log4net.Appender;
    using log4net.Config;
    using log4net.Repository.Hierarchy;
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Xml;

    #endregion

    /// <summary>
    ///     The logger.
    /// </summary>
    public class Logger
    {
        /// <summary>
        ///     Initializes static members of the <see cref="Logger" /> class.
        /// </summary>
        static Logger()
        {
            // log4net.Config.XmlConfigurator.Configure();
            //if (!string.IsNullOrEmpty(CommonsSettings.ConnectionStringValue))
            //{
            //    var rootFolder = HttpContext.Current != null
            //                         ? HttpContext.Current.Server.MapPath("~")
            //                         : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //    if (rootFolder != null)
            //    {
            //        SetUpDbConnection(
            //            CommonsSettings.ConnectionStringValue,
            //            Path.Combine(rootFolder, "log4net.config"));
            //    }
            //}
            //else
            //{
            //    XmlConfigurator.Configure();
            //}
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public Logger(Type type)
        {
            this.CurrentType = type;
        }

        /// <summary>
        ///     Gets the current type.
        /// </summary>
        private Type CurrentType { get; }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        public void Error(Exception x)
        {
            if (x is ThreadAbortException)
            {
                return;
            }

            this.PrepareLogData(x);
            LogManager.GetLogger(this.CurrentType).Error(this.GetInformativeExceptionMessage(x), x);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        public void Error(string x)
        {
            this.PrepareLogData();
            LogManager.GetLogger(this.CurrentType).Error(x);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Fatal(string message)
        {
            this.PrepareLogData();
            LogManager.GetLogger(this.CurrentType).Fatal(message);
        }

        /// <summary>
        /// The fatal.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        public void Fatal(Exception x)
        {
            this.PrepareLogData();
            LogManager.GetLogger(this.CurrentType).Fatal(this.GetInformativeExceptionMessage(x), x);
        }

        public void Fatal(string message, Exception x)
        {
            this.PrepareLogData();
            LogManager.GetLogger(this.CurrentType).Fatal(message, x);
        }

        /// <summary>
        /// The info.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Info(string message)
        {
            this.PrepareLogData();
            LogManager.GetLogger(this.CurrentType).Info(message); // "TracingLogger"
        }

        /// <summary>
        /// The warn.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void Warn(string message)
        {
            this.PrepareLogData();
            LogManager.GetLogger(this.CurrentType).Warn(message);
        }

        /// <summary>
        ///     The get error server variables.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        protected string GetErrorServerVariables()
        {
            var sb = new StringBuilder();
            var writer = XmlWriter.Create(sb);

            writer.WriteStartElement("error");

            // -----------------------------------------
            // Server Variables
            // -----------------------------------------
            writer.WriteStartElement("serverVariables");

            //foreach (var key in HttpContext.Current.Request.ServerVariables.AllKeys)
            //{
            //    writer.WriteStartElement("item");
            //    writer.WriteAttributeString("name", key);

            //    writer.WriteStartElement("value");
            //    writer.WriteAttributeString("string", HttpContext.Current.Request.ServerVariables[key]);
            //    writer.WriteEndElement();

            //    writer.WriteEndElement();
            //}

            writer.WriteEndElement();

            // -----------------------------------------
            // Cookies
            // -----------------------------------------            
            writer.WriteStartElement("cookies");

            //foreach (var key in HttpContext.Current.Request.Cookies.AllKeys)
            //{
            //    writer.WriteStartElement("item");
            //    writer.WriteAttributeString("name", key);

            //    writer.WriteStartElement("value");
            //    var httpCookie = HttpContext.Current.Request.Cookies[key];
            //    if (httpCookie != null)
            //    {
            //        writer.WriteAttributeString("string", httpCookie.Value);
            //    }

            //    writer.WriteEndElement();

            //    writer.WriteEndElement();
            //}

            writer.WriteEndElement();

            // -----------------------------------------   
            writer.WriteEndElement();

            // -----------------------------------------   
            writer.Flush();
            writer.Close();

            return sb.ToString();
        }

        /// <summary>
        /// The get informative exception message.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetInformativeExceptionMessage(Exception x)
        {
            var baseException = x.GetBaseException();
            return x.Message + (baseException.Message != x.Message ? "\n" + baseException.Message : string.Empty);
        }

        /// <summary>
        /// The set up db connection.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <param name="logConfig">
        /// The log config.
        /// </param>
        private static void SetUpDbConnection(string connectionString, string logConfig)
        {
            // update connection string for log4net dynamically
            var hier = LogManager.GetRepository() as Hierarchy;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(logConfig));
            if (hier != null)
            {
                var adoNetAppenders = hier.GetAppenders().OfType<AdoNetAppender>();
                foreach (var adoNetAppender in adoNetAppenders)
                {
                    adoNetAppender.ConnectionString = connectionString;
                    adoNetAppender.ActivateOptions();
                }
            }
        }

        /// <summary>
        /// The prepare log data.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        private void PrepareLogData(Exception ex = null)
        {
            ThreadContext.Properties["host"] = Environment.MachineName;
            ThreadContext.Properties["application"] = CommonsSettings.ApplicationName;

            ThreadContext.Properties["logtype"] = $"{this.CurrentType.FullName}.{new StackFrame(2).GetMethod().Name}";

            //if (HttpContext.Current != null)
            //{
            //    // application
            //    ThreadContext.Properties["application"] = CommonsSettings.ApplicationName
            //                                              ?? HttpContext.Current.Request.ServerVariables
            //                                                  ["APPL_MD_PATH"];

            //    var context = HttpContext.Current;
            //    ThreadContext.Properties["url"] = context.Request.Url.ToString();
            //    ThreadContext.Properties["statusCode"] = context.Response.StatusCode;
            //    ThreadContext.Properties["browser"] =
            //        $"{context.Request.Browser.Browser} {context.Request.Browser.MajorVersion}";

            //    if (context.User != null)
            //    {
            //        ThreadContext.Properties["user"] = !string.IsNullOrEmpty(context.User.Identity.Name)
            //                                               ? context.User.Identity.Name
            //                                               : "anonymous";
            //    }

            //    if (ex != null)
            //    {
            //        ThreadContext.Properties["allXml"] = this.GetErrorServerVariables();
            //    }
            //}

            if (ex != null)
            {
                ThreadContext.Properties["exceptionType"] = ex.GetType().FullName;

                if (ex.Data.Count > 0)
                {
                    var exceptiondata = new StringBuilder();

                    foreach (DictionaryEntry dictionaryEntry in ex.Data)
                    {
                        exceptiondata.AppendFormat("\t{0}: {1}; ", dictionaryEntry.Key, dictionaryEntry.Value);
                    }

                    ThreadContext.Properties["exceptiondata"] = exceptiondata.ToString();
                }
            }
        }
    }
}


//namespace System.Web
//{
//    public static class HttpContext
//    {
//        private static Microsoft.AspNetCore.Http.IHttpContextAccessor m_httpContextAccessor;


//        public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
//        {
//            m_httpContextAccessor = httpContextAccessor;
//        }


//        public static Microsoft.AspNetCore.Http.HttpContext Current
//        {
//            get
//            {
//                return m_httpContextAccessor.HttpContext;
//            }
//        }

//    }
//}