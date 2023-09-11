// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentitySecurityProvider.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// using System.Web.Security;

namespace K2DemoWorkFlow.k2.SecurityManager
{

    #region usings

    using Microsoft.AspNet.Identity;
    using SourceCode.Hosting.Server.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using IUser = SourceCode.Hosting.Server.Interfaces.IUser;

    using K2DemoWorkFlow.K2.SecurityManager;
    using K2DemoWorkFlow.k2.SecurityManager.Identity.Entities;
    using K2DemoWorkFlow.k2.SecurityManager.Identity.Model;
    using K2DemoWorkFlow.k2.SecurityManager.Identity;

    #endregion

    // https://help.k2.com/onlinehelp/k2five/devref/5.1/default.htm#Extend/Svr/UM-Creating.htm%3FTocPath%3DExtending%2520the%2520K2%2520Platform%7CK2%2520Server%7CCustom%2520User%2520Manager%7C_____2
    /// <summary>
    /// The identity security provider.
    /// </summary>
    public class IdentitySecurityProvider : IHostableSecurityProvider
    {
        /// <summary>
        /// The role manager.
        /// </summary>
        protected ApplicationRoleManager RoleManager;

        /// <summary>
        /// The user manager.
        /// </summary>
        protected ApplicationUserManager UserManager;

        /// <summary>
        /// The _configuration manager.
        /// </summary>
        private IConfigurationManager configurationManager;

        /// <summary>
        /// The _security manager.
        /// </summary>
        private ISecurityManager securityManager;

        /// <summary>
        /// Gets the auth init data.
        /// </summary>
        public string AuthInitData { get; private set; }

        /// <summary>
        /// Gets the security label.
        /// </summary>
        public string SecurityLabel { get; private set; }

        // ---
        /// <summary>
        /// The authenticate user.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="extraData">
        /// The extra data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AuthenticateUser(string userName, string password, string extraData)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("\n*******\n");
                System.Diagnostics.Debug.WriteLine("\nK2.SecurityManager.IdentitySecurityProvider\n");
                System.Diagnostics.Debug.WriteLine("\n*******\n");

                System.Diagnostics.Debug.WriteLine(
                    $"\nAuthenticateUser(string {userName}, string {password}, string extraData)");
                var user = this.UserManager.GetByUserName(userName);
                if (user == null)
                {
                    System.Diagnostics.Debug.WriteLine($"\nK2.SecurityManager.IdentitySecurityProvider::AuthenticateUser can't find the user {userName} in \n");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"\nK2.SecurityManager.IdentitySecurityProvider::AuthenticateUser find the user {userName} in \n");

                //var retCheck = this.UserManager.CheckPassword(user, password);

                //System.Diagnostics.Debug.WriteLine($"\nK2.SecurityManager.IdentitySecurityProvider::AuthenticateUser check user {retCheck} in \n");

                return true;// retCheck;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("\n*******\n");
                System.Diagnostics.Debug.WriteLine(
                    "\nK2.SecurityManager.IdentitySecurityProvider.AuthenticateUser ex : " + e);
                System.Diagnostics.Debug.WriteLine("\n*******\n");
                return false;
            }
        }

        // ---
        /// <summary>
        /// The find groups.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <returns>
        /// The <see cref="IGroupCollection"/>.
        /// </returns>
        public IGroupCollection FindGroups(string userName, IDictionary<string, object> properties)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(
                    $"\nFindGroups(string {userName}, IDictionary<string, object> properties)");

                var k2Groups = new K2Groups();

                var roles = this.RoleManager.Roles.ToList();

                foreach (var item in roles)
                    k2Groups.Add(new K2Group(this.SecurityLabel, item.Name, item.DisplayNameAr));

                return k2Groups;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(
                    "\nFindGroups(string userName, IDictionary<string, object> properties) Ex : " + e);
                return null;
            }
        }

        // ---
        /// <summary>
        /// The find groups.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <param name="extraData">
        /// The extra data.
        /// </param>
        /// <returns>
        /// The <see cref="IGroupCollection"/>.
        /// </returns>
        public IGroupCollection FindGroups(string userName, IDictionary<string, object> properties, string extraData)
        {
            System.Diagnostics.Debug.WriteLine(
                "\nFindGroups(string userName, IDictionary<string, object> properties, string extraData)");

            return this.FindGroups(userName, properties);
        }

        // ---
        /// <summary>
        /// The find users.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <returns>
        /// The <see cref="IUserCollection"/>.
        /// </returns>
        public IUserCollection FindUsers(string groupName, IDictionary<string, object> properties)
        {
            var users = new K2Users();

            // groupName = Utils.RemFQNLabel(groupName).ToLower();
            try
            {
                System.Diagnostics.Debug.WriteLine(
                    $"\nFindUsers(string {groupName}, IDictionary<string, object> properties)");

                List<ApplicationUser> applicationUsers;

                if (properties != null && properties.Any())
                {
                    var userName = properties[K2Utils.K2UserNamePropertyName]?.ToString();
                    var userDisplayName = properties[K2Utils.K2UserDisplayNamePropertyName]?.ToString();
                    var email = properties[K2Utils.K2UserEmailPropertyName]?.ToString();

                    applicationUsers = this.UserManager.Search(
                        new UsersSearchQuery
                        {
                            RolesNames = groupName == null ? new List<string>() : new List<string> { groupName },
                            UserName = userName,
                            Email = email,
                            FullName = userDisplayName,
                            PageNumber = 1,
                            PageSize = 5000
                        });
                }
                else
                {
                    applicationUsers = this.UserManager.Search(
                        new UsersSearchQuery
                        {
                            RolesNames = groupName == null ? new List<string>() : new List<string> { groupName },
                            PageNumber = 1,
                            PageSize = 5000
                        });
                }

                foreach (var user in applicationUsers)
                {
                    if (!user.UserName.Contains("\\"))
                    {
                        try
                        {
                            var k2User =
                                new K2User(this.SecurityLabel, user.UserName, user.FullName, user.Email, string.Empty)
                                {
                                    Properties = {
                                                        ["DisplayName"] = user.FullName, ["CommonName"] = user.FullName
                                                     }
                                };

                            users.Add(k2User);
                        }
                        catch
                        {
                            users.Add(
                                new K2User(this.SecurityLabel, user.UserName, user.FullName, user.Email, string.Empty));
                        }
                    }
                }

                return users;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(
                    "\nFindUsers(string groupName, IDictionary<string, object> properties) EX : " + e);
                if (e.InnerException != null)
                    System.Diagnostics.Debug.WriteLine(
                        "\nFindUsers(string groupName, IDictionary<string, object> properties) Inner Ex : "
                        + e.InnerException);

                users.Add(new K2User(this.SecurityLabel, "Error", "Error Displayname", "Email", string.Empty));
                return users;
            }
        }

        // ---
        /// <summary>
        /// The find users.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <param name="extraData">
        /// The extra data.
        /// </param>
        /// <returns>
        /// The <see cref="IUserCollection"/>.
        /// </returns>
        public IUserCollection FindUsers(string groupName, IDictionary<string, object> properties, string extraData)
        {
            System.Diagnostics.Debug.WriteLine(
                "\nFindUsers(string groupName, IDictionary<string, object> properties, string extraData)");

            return this.FindUsers(groupName, properties);
        }

        // ---
        /// <summary>
        /// The format item name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string FormatItemName(string name)
        {
            System.Diagnostics.Debug.WriteLine("\nFormatItemName(string name)");

            return name;
        }

        /// <summary>
        /// The get group.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IGroup"/>.
        /// </returns>
        public IGroup GetGroup(string name)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"\nGetGroup(string {name})");

                name = Utils.RemFqnLabel(name);

                var k2Groups = new K2Groups();

                var item = this.RoleManager.FindByName(name);

                return new K2Group(this.SecurityLabel, item.Name, item.DisplayNameAr);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\nGetGroup(string {name}) Ex : " + e);
                return null;
            }
        }

        // ---
        /// <summary>
        /// The get group.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="extraData">
        /// The extra data.
        /// </param>
        /// <returns>
        /// The <see cref="IGroup"/>.
        /// </returns>
        public IGroup GetGroup(string name, string extraData)
        {
            System.Diagnostics.Debug.WriteLine("\nGetGroup(string name, string extraData)");

            return this.GetGroup(name);
        }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IUser"/>.
        /// </returns>
        public IUser GetUser(string name)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"\nGetUser(string {name})");
                name = Utils.RemFqnLabel(name);

                var user = this.UserManager.FindByName(name);

                if (user == null)
                    return null;

                try
                {
                    var k2User = new K2User(this.SecurityLabel, user.UserName, user.FullName, user.Email, string.Empty)
                    {
                        Properties =
                                             {
                                                 ["DisplayName"] = user.FullName, ["CommonName"] = user.FullName
                                             }
                    };

                    return k2User;
                }
                catch
                {
                    return new K2User(this.SecurityLabel, user.UserName, string.Empty, user.Email, string.Empty);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"\nGetGroup(string {name}) Ex : " + e);
                return null;
            }
        }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="extraData">
        /// The extra data.
        /// </param>
        /// <returns>
        /// The <see cref="IUser"/>.
        /// </returns>
        public IUser GetUser(string name, string extraData)
        {
            System.Diagnostics.Debug.WriteLine("\nGetUser(string name, string extraData)");

            return this.GetUser(name);
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="ServiceMarshalling">
        /// The service marshalling.
        /// </param>
        /// <param name="ServerMarshaling">
        /// The server marshaling.
        /// </param>
        public void Init(IServiceMarshalling ServiceMarshalling, IServerMarshaling ServerMarshaling)
        {
            System.Diagnostics.Debug.WriteLine("\n*******\n");
            System.Diagnostics.Debug.WriteLine("\nK2.SecurityManager.IdentitySecurityProvider\n");
            System.Diagnostics.Debug.WriteLine("\n*******\n");

            System.Diagnostics.Debug.WriteLine(
                "\nInit(IServiceMarshalling ServiceMarshalling, IServerMarshaling ServerMarshaling)");
            this.configurationManager = ServiceMarshalling.GetConfigurationManagerContext();
            this.securityManager = ServerMarshaling.GetSecurityManagerContext();

            this.UserManager = new ApplicationUserManager();
            this.RoleManager = new ApplicationRoleManager();
            System.Diagnostics.Debug.WriteLine("Initiate User Manager.");
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="label">
        /// The label.
        /// </param>
        /// <param name="authInit">
        /// The auth init.
        /// </param>
        public void Init(string label, string authInit)
        {
            System.Diagnostics.Debug.WriteLine("\n*******\n");
            System.Diagnostics.Debug.WriteLine("\nK2.SecurityManager.IdentitySecurityProvider\n");
            System.Diagnostics.Debug.WriteLine("\n*******\n");
            System.Diagnostics.Debug.WriteLine("\nInit(string label, string authInit)");
            System.Diagnostics.Debug.WriteLine($"\nlabel:{label}\nauthInit:{authInit}\n");
            System.Diagnostics.Debug.WriteLine("\n*******\n");

            this.SecurityLabel = label;
            this.AuthInitData = authInit;
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Login(string connectionString)
        {
            System.Diagnostics.Debug.WriteLine("\n*******\n");
            System.Diagnostics.Debug.WriteLine("\nK2.SecurityManager.IdentitySecurityProvider\n");
            System.Diagnostics.Debug.WriteLine("\n*******\n");

            System.Diagnostics.Debug.WriteLine($"\nLogin(string {connectionString})");

            return string.Empty;
        }

        /// <summary>
        /// The query group properties.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public Dictionary<string, string> QueryGroupProperties()
        {
            System.Diagnostics.Debug.WriteLine("\nQueryGroupProperties()");

            return K2Utils.K2GroupPropertyDefinitions;
        }

        /// <summary>
        /// The query user properties.
        /// </summary>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public Dictionary<string, string> QueryUserProperties()
        {
            System.Diagnostics.Debug.WriteLine("\nQueryUserProperties()");

            return K2Utils.K2UserPropertyDefinitions;
        }

        /// <summary>
        /// The requires authentication.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RequiresAuthentication()
        {
            System.Diagnostics.Debug.WriteLine("\nRequiresAuthentication()");
            return true;
        }

        /// <summary>
        /// The resolve queue.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="ArrayList"/>.
        /// </returns>
        public ArrayList ResolveQueue(string data)
        {
            System.Diagnostics.Debug.WriteLine("\nResolveQueue(string data)");

            return null;
        }

        /// <summary>
        /// The unload.
        /// </summary>
        public void Unload()
        {
            System.Diagnostics.Debug.WriteLine("\nUnload()");
        }
    }
}