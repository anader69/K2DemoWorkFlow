// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Config.cs" company="SURE International Technology">
//   Copyright © 2015 All Right Reserved
// </copyright>
// <summary>
//   Defines the K2 Config 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ClassLibrarytest
{
    using System;
    using System.Configuration;

    /// <summary>
    /// The K2 configuration class.
    /// </summary>
    public class K2Config
    {
        #region Constants

        /// <summary>
        /// The k 2 connection string key name.
        /// </summary>
        public const string K2ClientConnectionStringKey = "K2Client";

        /// <summary>
        /// The k2 management connection string key
        /// </summary>


        public const string K2ManagementConnectionStringKey = "K2Management";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="K2Config"/> class.
        /// </summary>        
        public K2Config()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="K2Config" /> class.
        /// </summary>
        /// <param name="clientConnectionString">The client connection string.</param>
        /// <param name="managementConnectionString">The management connection string.</param>
        /// <exception cref="System.ArgumentNullException">server
        /// or
        /// connectionString</exception>
        public K2Config(string clientConnectionString, string managementConnectionString)
        {
            if (clientConnectionString == null)
            {
                throw new ArgumentNullException(nameof(clientConnectionString));
            }
            if (managementConnectionString == null)
            {
                throw new ArgumentNullException(nameof(managementConnectionString));
            }

            this.ClientConnectionString = clientConnectionString;
            this.ManagementConnectionString = managementConnectionString;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>        
        public string ClientConnectionString { get; set; }

        /// <summary>
        /// Gets the management connection string.
        /// </summary>
        /// <value>
        /// The management connection string.
        /// </value>
        public string ManagementConnectionString { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the configuration from application settings.
        /// </summary>
        /// <param name="clientKey">The client key.</param>
        /// <param name="managementKey">The management key.</param>
        /// <returns>
        /// The configuration to use for K2 client.
        /// </returns>
        /// <exception cref="System.Configuration.SettingsPropertyNotFoundException">$Cannot find a K2 connection string in appSettings section with key: {connectionStringKey}</exception>
        public static K2Config GetFromAppSettings(string clientKey, string managementKey)
        {
            string clientConnectionString = ConfigurationManager.AppSettings[clientKey];
            if (string.IsNullOrEmpty(clientConnectionString))
            {
                throw new SettingsPropertyNotFoundException($"Cannot find a K2 connection string in appSettings section with key: {clientKey}");
            }

            string managementConnectionString = ConfigurationManager.AppSettings[managementKey];
            if (string.IsNullOrEmpty(clientConnectionString))
            {
                throw new SettingsPropertyNotFoundException($"Cannot find a K2 connection string in appSettings section with key: {managementKey}");
            }

            return new K2Config(clientConnectionString, managementConnectionString);
        }

        /// <summary>
        /// Gets the configuration from connection strings.
        /// </summary>
        /// <param name="clientKey">The client key.</param>
        /// <param name="managementKey">The management key.</param>
        /// <returns>
        /// The configuration to use for K2 client.
        /// </returns>
        /// <exception cref="System.Configuration.SettingsPropertyNotFoundException">Cannot find the connection string with name:  + connectionStringKey</exception>
        public static K2Config GetFromConnectionStrings(string clientKey, string managementKey)
        {
            //if (ConfigurationManager.ConnectionStrings[clientKey] == null)
            //{
            //    throw new SettingsPropertyNotFoundException($"Cannot find a K2 connection string in connectionStrings section with name: {clientKey}");
            //}

            //if (ConfigurationManager.ConnectionStrings[managementKey] == null)
            //{
            //    throw new SettingsPropertyNotFoundException($"Cannot find a K2 connection string in connectionStrings section with name: {managementKey}");
            //}

            var clientConnectionString = "Integrated=False;IsPrimaryLogin=True;Authenticate=True;SecurityLabelName=K2;EncryptedPassword=False;Host=10.2.2.164;Port=5252;UserID=SURE\\mhanna;WindowsDomain=SURE;Password=M@RCOhunter2110;";
            var managementConnectionString = "Integrated=False;IsPrimaryLogin=True;Authenticate=True;SecurityLabelName=K2;EncryptedPassword=False;Host=10.2.2.164;Port=5252;UserID=SURE\\DSC-k2Admin;WindowsDomain=SURE;Password=P@ssw0rd@2023;";

            return new K2Config(clientConnectionString, managementConnectionString);
        }

        /// <summary>
        /// Gets the configuration from application settings. Make sure to have an app setting item with key: K2ConnectionString
        /// Example: <add key="K2ConnectionString" value="Integrated=True;IsPrimaryLogin=True;Authenticate=True;EncryptedPassword=False;Host=SURE-K2;Port=5252"/>
        /// </summary>
        /// <returns>The config for K2 client.</returns>        
        public static K2Config GetFromAppSettings()
        {
            return GetFromAppSettings(K2ClientConnectionStringKey, K2ManagementConnectionStringKey);
        }

        /// <summary>
        /// Gets from connection strings.
        /// </summary>
        /// <returns>The config for K2 client.</returns>
        public static K2Config GetFromConnectionStrings()
        {
            return GetFromConnectionStrings(K2ClientConnectionStringKey, K2ManagementConnectionStringKey);
        }

        #endregion
    }
}