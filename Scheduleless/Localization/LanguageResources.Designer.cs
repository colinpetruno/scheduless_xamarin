﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Scheduleless.Localization {
    using System;
    using System.Reflection;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LanguageResources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LanguageResources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Scheduleless.Localization.LanguageResources", typeof(LanguageResources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string EmailPlaceholder {
            get {
                return ResourceManager.GetString("EmailPlaceholder", resourceCulture);
            }
        }
        
        internal static string ForgotPasswordLabel {
            get {
                return ResourceManager.GetString("ForgotPasswordLabel", resourceCulture);
            }
        }
        
        internal static string LoginButton {
            get {
                return ResourceManager.GetString("LoginButton", resourceCulture);
            }
        }
        
        internal static string PasswordPlaceholder {
            get {
                return ResourceManager.GetString("PasswordPlaceholder", resourceCulture);
            }
        }
        
        internal static string LoginRequiredFieldMissingDialogTitle {
            get {
                return ResourceManager.GetString("LoginRequiredFieldMissingDialogTitle", resourceCulture);
            }
        }
        
        internal static string LoginRequiredFieldMissingDialogMessage {
            get {
                return ResourceManager.GetString("LoginRequiredFieldMissingDialogMessage", resourceCulture);
            }
        }
        
        internal static string LoginRequiredFieldMissingDialogOk {
            get {
                return ResourceManager.GetString("LoginRequiredFieldMissingDialogOk", resourceCulture);
            }
        }
        
        internal static string LoginFailedTitle {
            get {
                return ResourceManager.GetString("LoginFailedTitle", resourceCulture);
            }
        }
        
        internal static string LoginFailedMessage {
            get {
                return ResourceManager.GetString("LoginFailedMessage", resourceCulture);
            }
        }
        
        internal static string LoginFailedOk {
            get {
                return ResourceManager.GetString("LoginFailedOk", resourceCulture);
            }
        }
        
        internal static string LoginSigningInMessage {
            get {
                return ResourceManager.GetString("LoginSigningInMessage", resourceCulture);
            }
        }
        
        internal static string TabbedPageAvailablesShifts {
            get {
                return ResourceManager.GetString("TabbedPageAvailablesShifts", resourceCulture);
            }
        }
        
        internal static string TabbedPageYourSchedule {
            get {
                return ResourceManager.GetString("TabbedPageYourSchedule", resourceCulture);
            }
        }
        
        internal static string TabbedPageYourTrades {
            get {
                return ResourceManager.GetString("TabbedPageYourTrades", resourceCulture);
            }
        }
    }
}
