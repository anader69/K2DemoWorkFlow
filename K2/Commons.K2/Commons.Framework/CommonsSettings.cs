// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonsSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



namespace Commons.Framework
{
    #region

    using Commons.Framework.Extensions;
    using System.Configuration;
    using System.Data.Entity.Core.EntityClient;


    #endregion

    /// <summary>
    /// The commons settings.
    /// </summary>
    public static class CommonsSettings
    {
        /// <summary>
        /// Gets the application name.
        /// </summary>
        public static string ApplicationName { get; internal set; } ////= "MyApplicationName";

        /// <summary>
        /// Gets the connection string name.
        /// </summary>
        public static string ConnectionStringName { get; internal set; } = "DSCDb";

        /// <summary>
        /// The connstring val.
        /// </summary>
        private static string connstringVal;

        /// <summary>
        /// Gets the connection string value.
        /// </summary>
        public static string ConnectionStringValue
        {
            get
            {
                if (connstringVal != null)
                {
                    return connstringVal;
                }

                ConnectionStringName = ConfigurationManager.AppSettings.GetValue<string>("CommonsDbConnectionStringName") ?? ConnectionStringName;
                connstringVal = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
                //connstringVal = new EntityConnectionStringBuilder(ConfigurationManager.Connectio‌​nStrings["IOFEntities"].Co‌​nnectionString).Prov‌​iderConnectionString;


                if (connstringVal.Contains("metadata=res"))
                {
                    connstringVal = new EntityConnectionStringBuilder(connstringVal).ProviderConnectionString;
                }

                return connstringVal;

            }

            internal set => connstringVal = value;
        }

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        public static string DefaultCulture { get; internal set; } = "ar-SA";

        /// <summary>
        /// Gets the widgets static content version.
        /// </summary>
        public static double WidgetsStaticContentVersion { get; internal set; } = 1.0;

        /// <summary>
        /// Gets GPD service provider.
        /// </summary>
        public static string ServiceProvider
        {
            get
            {
                return "DSC:";
            }
        }
    }
}