﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.1432.
// 
namespace TextMiner.Properties.Settings {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/TextMinerService", IsNullable=false)]
    public partial class TextMinerService {
        
        private TextMinerServiceSettings settingsField;
        
        /// <remarks/>
        public TextMinerServiceSettings Settings {
            get {
                return this.settingsField;
            }
            set {
                this.settingsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettings {
        
        private TextMinerServiceSettingsDatabase databaseField;
        
        private bool logSearchInformationField;
        
        private TextMinerServiceSettingsProcess[] processesField;
        
        private TextMinerServiceSettingsMiner[] minersField;
        
        /// <remarks/>
        public TextMinerServiceSettingsDatabase Database {
            get {
                return this.databaseField;
            }
            set {
                this.databaseField = value;
            }
        }
        
        /// <remarks/>
        public bool LogSearchInformation {
            get {
                return this.logSearchInformationField;
            }
            set {
                this.logSearchInformationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Process", IsNullable=false)]
        public TextMinerServiceSettingsProcess[] Processes {
            get {
                return this.processesField;
            }
            set {
                this.processesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Miner", IsNullable=false)]
        public TextMinerServiceSettingsMiner[] Miners {
            get {
                return this.minersField;
            }
            set {
                this.minersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettingsDatabase {
        
        private TextMinerServiceSettingsDatabaseSSH sSHField;
        
        private string connectionStringField;
        
        /// <remarks/>
        public TextMinerServiceSettingsDatabaseSSH SSH {
            get {
                return this.sSHField;
            }
            set {
                this.sSHField = value;
            }
        }
        
        /// <remarks/>
        public string ConnectionString {
            get {
                return this.connectionStringField;
            }
            set {
                this.connectionStringField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettingsDatabaseSSH {
        
        private string hostField;
        
        private string userIDField;
        
        private string passwordField;
        
        private int portField;
        
        private int localPortField;
        
        private string forwardingHostField;
        
        private int remotePortField;
        
        private bool enabledField;
        
        /// <remarks/>
        public string Host {
            get {
                return this.hostField;
            }
            set {
                this.hostField = value;
            }
        }
        
        /// <remarks/>
        public string UserID {
            get {
                return this.userIDField;
            }
            set {
                this.userIDField = value;
            }
        }
        
        /// <remarks/>
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        public int Port {
            get {
                return this.portField;
            }
            set {
                this.portField = value;
            }
        }
        
        /// <remarks/>
        public int LocalPort {
            get {
                return this.localPortField;
            }
            set {
                this.localPortField = value;
            }
        }
        
        /// <remarks/>
        public string ForwardingHost {
            get {
                return this.forwardingHostField;
            }
            set {
                this.forwardingHostField = value;
            }
        }
        
        /// <remarks/>
        public int RemotePort {
            get {
                return this.remotePortField;
            }
            set {
                this.remotePortField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool Enabled {
            get {
                return this.enabledField;
            }
            set {
                this.enabledField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettingsProcess {
        
        private bool enabledField;
        
        private int timeoutField;
        
        private int pollingIntervalField;
        
        private ProcessType typeField;
        
        private int postPollingIntervalField;
        
        private bool postPollingIntervalFieldSpecified;
        
        private int responseTimeoutField;
        
        private bool responseTimeoutFieldSpecified;
        
        private int ontogratorTabField;
        
        private bool ontogratorTabFieldSpecified;
        
        /// <remarks/>
        public bool Enabled {
            get {
                return this.enabledField;
            }
            set {
                this.enabledField = value;
            }
        }
        
        /// <remarks/>
        public int Timeout {
            get {
                return this.timeoutField;
            }
            set {
                this.timeoutField = value;
            }
        }
        
        /// <remarks/>
        public int PollingInterval {
            get {
                return this.pollingIntervalField;
            }
            set {
                this.pollingIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ProcessType Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int PostPollingInterval {
            get {
                return this.postPollingIntervalField;
            }
            set {
                this.postPollingIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PostPollingIntervalSpecified {
            get {
                return this.postPollingIntervalFieldSpecified;
            }
            set {
                this.postPollingIntervalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ResponseTimeout {
            get {
                return this.responseTimeoutField;
            }
            set {
                this.responseTimeoutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ResponseTimeoutSpecified {
            get {
                return this.responseTimeoutFieldSpecified;
            }
            set {
                this.responseTimeoutFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int OntogratorTab {
            get {
                return this.ontogratorTabField;
            }
            set {
                this.ontogratorTabField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OntogratorTabSpecified {
            get {
                return this.ontogratorTabFieldSpecified;
            }
            set {
                this.ontogratorTabFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/TextMinerService")]
    public enum ProcessType {
        
        /// <remarks/>
        Unassigned,
        
        /// <remarks/>
        OntologySubsetWorker,
        
        /// <remarks/>
        PubMed,
        
        /// <remarks/>
        Pubget,
        
        /// <remarks/>
        ClinicalTrialsGov,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettingsMiner {
        
        private bool enabledField;
        
        private string uriField;
        
        private string argumentsField;
        
        private TextMinerServiceSettingsMinerOntology[] ontologiesField;
        
        private MinerName nameField;
        
        private int responseTimeoutField;
        
        private bool responseTimeoutFieldSpecified;
        
        private int retriesOnErrorField;
        
        private bool retriesOnErrorFieldSpecified;
        
        private int intervalOnRetryField;
        
        private bool intervalOnRetryFieldSpecified;
        
        /// <remarks/>
        public bool Enabled {
            get {
                return this.enabledField;
            }
            set {
                this.enabledField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="anyURI")]
        public string Uri {
            get {
                return this.uriField;
            }
            set {
                this.uriField = value;
            }
        }
        
        /// <remarks/>
        public string Arguments {
            get {
                return this.argumentsField;
            }
            set {
                this.argumentsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Ontology", IsNullable=false)]
        public TextMinerServiceSettingsMinerOntology[] Ontologies {
            get {
                return this.ontologiesField;
            }
            set {
                this.ontologiesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public MinerName Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ResponseTimeout {
            get {
                return this.responseTimeoutField;
            }
            set {
                this.responseTimeoutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ResponseTimeoutSpecified {
            get {
                return this.responseTimeoutFieldSpecified;
            }
            set {
                this.responseTimeoutFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int RetriesOnError {
            get {
                return this.retriesOnErrorField;
            }
            set {
                this.retriesOnErrorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RetriesOnErrorSpecified {
            get {
                return this.retriesOnErrorFieldSpecified;
            }
            set {
                this.retriesOnErrorFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IntervalOnRetry {
            get {
                return this.intervalOnRetryField;
            }
            set {
                this.intervalOnRetryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IntervalOnRetrySpecified {
            get {
                return this.intervalOnRetryFieldSpecified;
            }
            set {
                this.intervalOnRetryFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettingsMinerOntology {
        
        private TextMinerServiceSettingsMinerOntologyMatchText[] matchTextField;
        
        private string idField;
        
        private int ontogratorPaneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MatchText")]
        public TextMinerServiceSettingsMinerOntologyMatchText[] MatchText {
            get {
                return this.matchTextField;
            }
            set {
                this.matchTextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int OntogratorPane {
            get {
                return this.ontogratorPaneField;
            }
            set {
                this.ontogratorPaneField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/TextMinerService")]
    public partial class TextMinerServiceSettingsMinerOntologyMatchText {
        
        private string resultTextIDField;
        
        private string dBTextIDField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ResultTextID {
            get {
                return this.resultTextIDField;
            }
            set {
                this.resultTextIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DBTextID {
            get {
                return this.dBTextIDField;
            }
            set {
                this.dBTextIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/TextMinerService")]
    public enum MinerName {
        
        /// <remarks/>
        Unsupported,
        
        /// <remarks/>
        NCBOAnnotator,
        
        /// <remarks/>
        NERCTerminizer,
    }
}