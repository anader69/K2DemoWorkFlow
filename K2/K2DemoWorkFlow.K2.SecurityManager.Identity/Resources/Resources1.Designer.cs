﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace K2DemoWorkFlow.K2.SecurityManager.Identity.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("K2DemoWorkFlow.K2.SecurityManager.Identity.Resources.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email {0} is already exist..
        /// </summary>
        public static string DuplicatedEmail {
            get {
                return ResourceManager.GetString("DuplicatedEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Employee Number {0} is already exist..
        /// </summary>
        public static string DuplicatedEmployeeNumber {
            get {
                return ResourceManager.GetString("DuplicatedEmployeeNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role {0} is already exist..
        /// </summary>
        public static string DuplicatedRoleName {
            get {
                return ResourceManager.GetString("DuplicatedRoleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User name {0} is already exist..
        /// </summary>
        public static string DuplicatedUserName {
            get {
                return ResourceManager.GetString("DuplicatedUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User name can&apos;t be null..
        /// </summary>
        public static string UserNameCanNotBeNull {
            get {
                return ResourceManager.GetString("UserNameCanNotBeNull", resourceCulture);
            }
        }
    }
}