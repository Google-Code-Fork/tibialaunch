﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTibiaX.IPChanger.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GraphicsEngine {
            get {
                return ((string)(this["GraphicsEngine"]));
            }
            set {
                this["GraphicsEngine"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\program files\\tibia\\tibia.exe")]
        public string TibiaPath {
            get {
                return ((string)(this["TibiaPath"]));
            }
            set {
                this["TibiaPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChangeFPS {
            get {
                return ((bool)(this["ChangeFPS"]));
            }
            set {
                this["ChangeFPS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("login01.tibia.com")]
        public string LoginServer {
            get {
                return ((string)(this["LoginServer"]));
            }
            set {
                this["LoginServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7171")]
        public int LoginPort {
            get {
                return ((int)(this["LoginPort"]));
            }
            set {
                this["LoginPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1247104594")]
        public string StringVersion {
            get {
                return ((string)(this["StringVersion"]));
            }
            set {
                this["StringVersion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tÿ×øt3Àë¸")]
        public string MCVersion {
            get {
                return ((string)(this["MCVersion"]));
            }
            set {
                this["MCVersion"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("40")]
        public uint FPSValue {
            get {
                return ((uint)(this["FPSValue"]));
            }
            set {
                this["FPSValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableMC {
            get {
                return ((bool)(this["EnableMC"]));
            }
            set {
                this["EnableMC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool WriteRSA {
            get {
                return ((bool)(this["WriteRSA"]));
            }
            set {
                this["WriteRSA"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413")]
        public string RSAKey {
            get {
                return ((string)(this["RSAKey"]));
            }
            set {
                this["RSAKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ChangeGraphics {
            get {
                return ((bool)(this["ChangeGraphics"]));
            }
            set {
                this["ChangeGraphics"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool DistinctMaps {
            get {
                return ((bool)(this["DistinctMaps"]));
            }
            set {
                this["DistinctMaps"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::KTibiaX.IPChanger.Data.LoginServerCollection ServerList {
            get {
                return ((global::KTibiaX.IPChanger.Data.LoginServerCollection)(this["ServerList"]));
            }
            set {
                this["ServerList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::KTibiaX.IPChanger.Data.ClientPathCollection ClientList {
            get {
                return ((global::KTibiaX.IPChanger.Data.ClientPathCollection)(this["ClientList"]));
            }
            set {
                this["ClientList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Seven")]
        public string AppSkin {
            get {
                return ((string)(this["AppSkin"]));
            }
            set {
                this["AppSkin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string OTMapPath {
            get {
                return ((string)(this["OTMapPath"]));
            }
            set {
                this["OTMapPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::KTibiaX.IPChanger.Data.TibiaCFGCollection ConfigFiles {
            get {
                return ((global::KTibiaX.IPChanger.Data.TibiaCFGCollection)(this["ConfigFiles"]));
            }
            set {
                this["ConfigFiles"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ConfigFiles")]
        public string ConfigFilesDir {
            get {
                return ((string)(this["ConfigFilesDir"]));
            }
            set {
                this["ConfigFilesDir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CloseAfterStart {
            get {
                return ((bool)(this["CloseAfterStart"]));
            }
            set {
                this["CloseAfterStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Culture {
            get {
                return ((string)(this["Culture"]));
            }
            set {
                this["Culture"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AppConfig")]
        public string AppConfigDir {
            get {
                return ((string)(this["AppConfigDir"]));
            }
            set {
                this["AppConfigDir"] = value;
            }
        }
    }
}
