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
namespace TextMiner.ProviderSchema {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/NCBOAnnotator", IsNullable=false)]
    public partial class success {
        
        private string accessedResourceField;
        
        private string accessDateField;
        
        private successData dataField;
        
        /// <remarks/>
        public string accessedResource {
            get {
                return this.accessedResourceField;
            }
            set {
                this.accessedResourceField = value;
            }
        }
        
        /// <remarks/>
        public string accessDate {
            get {
                return this.accessDateField;
            }
            set {
                this.accessDateField = value;
            }
        }
        
        /// <remarks/>
        public successData data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successData {
        
        private successDataAnnotatorResultBean annotatorResultBeanField;
        
        /// <remarks/>
        public successDataAnnotatorResultBean annotatorResultBean {
            get {
                return this.annotatorResultBeanField;
            }
            set {
                this.annotatorResultBeanField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBean {
        
        private string resultIDField;
        
        private successDataAnnotatorResultBeanDictionary dictionaryField;
        
        private successDataAnnotatorResultBeanStatisticsBean[] statisticsField;
        
        private successDataAnnotatorResultBeanParameters parametersField;
        
        private successDataAnnotatorResultBeanAnnotationBean[] annotationsField;
        
        private successDataAnnotatorResultBeanOntologyUsedBean[] ontologiesField;
        
        /// <remarks/>
        public string resultID {
            get {
                return this.resultIDField;
            }
            set {
                this.resultIDField = value;
            }
        }
        
        /// <remarks/>
        public successDataAnnotatorResultBeanDictionary dictionary {
            get {
                return this.dictionaryField;
            }
            set {
                this.dictionaryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("statisticsBean", IsNullable=false)]
        public successDataAnnotatorResultBeanStatisticsBean[] statistics {
            get {
                return this.statisticsField;
            }
            set {
                this.statisticsField = value;
            }
        }
        
        /// <remarks/>
        public successDataAnnotatorResultBeanParameters parameters {
            get {
                return this.parametersField;
            }
            set {
                this.parametersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("annotationBean", IsNullable=false)]
        public successDataAnnotatorResultBeanAnnotationBean[] annotations {
            get {
                return this.annotationsField;
            }
            set {
                this.annotationsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ontologyUsedBean", IsNullable=false)]
        public successDataAnnotatorResultBeanOntologyUsedBean[] ontologies {
            get {
                return this.ontologiesField;
            }
            set {
                this.ontologiesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanDictionary {
        
        private int idField;
        
        private string nameField;
        
        private successDataAnnotatorResultBeanDictionaryDateCreated dateCreatedField;
        
        /// <remarks/>
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public successDataAnnotatorResultBeanDictionaryDateCreated dateCreated {
            get {
                return this.dateCreatedField;
            }
            set {
                this.dateCreatedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanDictionaryDateCreated {
        
        private string classField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class {
            get {
                return this.classField;
            }
            set {
                this.classField = value;
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
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanStatisticsBean {
        
        private string contextNameField;
        
        private int nbAnnotationField;
        
        /// <remarks/>
        public string contextName {
            get {
                return this.contextNameField;
            }
            set {
                this.contextNameField = value;
            }
        }
        
        /// <remarks/>
        public int nbAnnotation {
            get {
                return this.nbAnnotationField;
            }
            set {
                this.nbAnnotationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanParameters {
        
        private bool longestOnlyField;
        
        private bool wholeWordOnlyField;
        
        private bool filterNumberField;
        
        private bool withDefaultStopWordsField;
        
        private bool isStopWordsCaseSenstiveField;
        
        private bool withSynonymsField;
        
        private int minTermSizeField;
        
        private bool scoredField;
        
        private string[] semanticTypesField;
        
        private string[] stopWordsField;
        
        private string[] ontologiesToExpandField;
        
        private string[] ontologiesToKeepInResultField;
        
        private bool isVirtualOntologyIdField;
        
        private int levelMaxField;
        
        private string[] mappingTypesField;
        
        private string textToAnnotateField;
        
        private string outputFormatField;
        
        /// <remarks/>
        public bool longestOnly {
            get {
                return this.longestOnlyField;
            }
            set {
                this.longestOnlyField = value;
            }
        }
        
        /// <remarks/>
        public bool wholeWordOnly {
            get {
                return this.wholeWordOnlyField;
            }
            set {
                this.wholeWordOnlyField = value;
            }
        }
        
        /// <remarks/>
        public bool filterNumber {
            get {
                return this.filterNumberField;
            }
            set {
                this.filterNumberField = value;
            }
        }
        
        /// <remarks/>
        public bool withDefaultStopWords {
            get {
                return this.withDefaultStopWordsField;
            }
            set {
                this.withDefaultStopWordsField = value;
            }
        }
        
        /// <remarks/>
        public bool isStopWordsCaseSenstive {
            get {
                return this.isStopWordsCaseSenstiveField;
            }
            set {
                this.isStopWordsCaseSenstiveField = value;
            }
        }
        
        /// <remarks/>
        public bool withSynonyms {
            get {
                return this.withSynonymsField;
            }
            set {
                this.withSynonymsField = value;
            }
        }
        
        /// <remarks/>
        public int minTermSize {
            get {
                return this.minTermSizeField;
            }
            set {
                this.minTermSizeField = value;
            }
        }
        
        /// <remarks/>
        public bool scored {
            get {
                return this.scoredField;
            }
            set {
                this.scoredField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] semanticTypes {
            get {
                return this.semanticTypesField;
            }
            set {
                this.semanticTypesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] stopWords {
            get {
                return this.stopWordsField;
            }
            set {
                this.stopWordsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] ontologiesToExpand {
            get {
                return this.ontologiesToExpandField;
            }
            set {
                this.ontologiesToExpandField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] ontologiesToKeepInResult {
            get {
                return this.ontologiesToKeepInResultField;
            }
            set {
                this.ontologiesToKeepInResultField = value;
            }
        }
        
        /// <remarks/>
        public bool isVirtualOntologyId {
            get {
                return this.isVirtualOntologyIdField;
            }
            set {
                this.isVirtualOntologyIdField = value;
            }
        }
        
        /// <remarks/>
        public int levelMax {
            get {
                return this.levelMaxField;
            }
            set {
                this.levelMaxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] mappingTypes {
            get {
                return this.mappingTypesField;
            }
            set {
                this.mappingTypesField = value;
            }
        }
        
        /// <remarks/>
        public string textToAnnotate {
            get {
                return this.textToAnnotateField;
            }
            set {
                this.textToAnnotateField = value;
            }
        }
        
        /// <remarks/>
        public string outputFormat {
            get {
                return this.outputFormatField;
            }
            set {
                this.outputFormatField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanAnnotationBean {
        
        private int scoreField;
        
        private successDataAnnotatorResultBeanAnnotationBeanConcept conceptField;
        
        private successDataAnnotatorResultBeanAnnotationBeanContext contextField;
        
        /// <remarks/>
        public int score {
            get {
                return this.scoreField;
            }
            set {
                this.scoreField = value;
            }
        }
        
        /// <remarks/>
        public successDataAnnotatorResultBeanAnnotationBeanConcept concept {
            get {
                return this.conceptField;
            }
            set {
                this.conceptField = value;
            }
        }
        
        /// <remarks/>
        public successDataAnnotatorResultBeanAnnotationBeanContext context {
            get {
                return this.contextField;
            }
            set {
                this.contextField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanAnnotationBeanConcept {
        
        private string idField;
        
        private string localConceptIdField;
        
        private string localOntologyIdField;
        
        private int isTopLevelField;
        
        private string fullIdField;
        
        private string preferredNameField;
        
        private string[] synonymsField;
        
        private string[] definitionsField;
        
        private successDataAnnotatorResultBeanAnnotationBeanConceptSemanticTypeBean[] semanticTypesField;
        
        /// <remarks/>
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string localConceptId {
            get {
                return this.localConceptIdField;
            }
            set {
                this.localConceptIdField = value;
            }
        }
        
        /// <remarks/>
        public string localOntologyId {
            get {
                return this.localOntologyIdField;
            }
            set {
                this.localOntologyIdField = value;
            }
        }
        
        /// <remarks/>
        public int isTopLevel {
            get {
                return this.isTopLevelField;
            }
            set {
                this.isTopLevelField = value;
            }
        }
        
        /// <remarks/>
        public string fullId {
            get {
                return this.fullIdField;
            }
            set {
                this.fullIdField = value;
            }
        }
        
        /// <remarks/>
        public string preferredName {
            get {
                return this.preferredNameField;
            }
            set {
                this.preferredNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] synonyms {
            get {
                return this.synonymsField;
            }
            set {
                this.synonymsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] definitions {
            get {
                return this.definitionsField;
            }
            set {
                this.definitionsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("semanticTypeBean", IsNullable=false)]
        public successDataAnnotatorResultBeanAnnotationBeanConceptSemanticTypeBean[] semanticTypes {
            get {
                return this.semanticTypesField;
            }
            set {
                this.semanticTypesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanAnnotationBeanConceptSemanticTypeBean {
        
        private string idField;
        
        private string semanticTypeField;
        
        private string descriptionField;
        
        /// <remarks/>
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string semanticType {
            get {
                return this.semanticTypeField;
            }
            set {
                this.semanticTypeField = value;
            }
        }
        
        /// <remarks/>
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanAnnotationBeanContext {
        
        private string contextNameField;
        
        private bool isDirectField;
        
        private int fromField;
        
        private int toField;
        
        private successDataAnnotatorResultBeanAnnotationBeanContextTerm termField;
        
        private string classField;
        
        /// <remarks/>
        public string contextName {
            get {
                return this.contextNameField;
            }
            set {
                this.contextNameField = value;
            }
        }
        
        /// <remarks/>
        public bool isDirect {
            get {
                return this.isDirectField;
            }
            set {
                this.isDirectField = value;
            }
        }
        
        /// <remarks/>
        public int from {
            get {
                return this.fromField;
            }
            set {
                this.fromField = value;
            }
        }
        
        /// <remarks/>
        public int to {
            get {
                return this.toField;
            }
            set {
                this.toField = value;
            }
        }
        
        /// <remarks/>
        public successDataAnnotatorResultBeanAnnotationBeanContextTerm term {
            get {
                return this.termField;
            }
            set {
                this.termField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class {
            get {
                return this.classField;
            }
            set {
                this.classField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanAnnotationBeanContextTerm {
        
        private string nameField;
        
        private string localConceptIdField;
        
        private int isPreferredField;
        
        private int dictionaryIdField;
        
        private string classField;
        
        /// <remarks/>
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string localConceptId {
            get {
                return this.localConceptIdField;
            }
            set {
                this.localConceptIdField = value;
            }
        }
        
        /// <remarks/>
        public int isPreferred {
            get {
                return this.isPreferredField;
            }
            set {
                this.isPreferredField = value;
            }
        }
        
        /// <remarks/>
        public int dictionaryId {
            get {
                return this.dictionaryIdField;
            }
            set {
                this.dictionaryIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class {
            get {
                return this.classField;
            }
            set {
                this.classField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/NCBOAnnotator")]
    public partial class successDataAnnotatorResultBeanOntologyUsedBean {
        
        private int localOntologyIdField;
        
        private string nameField;
        
        private string versionField;
        
        private int virtualOntologyIdField;
        
        private int nbAnnotationField;
        
        private int scoreField;
        
        /// <remarks/>
        public int localOntologyId {
            get {
                return this.localOntologyIdField;
            }
            set {
                this.localOntologyIdField = value;
            }
        }
        
        /// <remarks/>
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        public int virtualOntologyId {
            get {
                return this.virtualOntologyIdField;
            }
            set {
                this.virtualOntologyIdField = value;
            }
        }
        
        /// <remarks/>
        public int nbAnnotation {
            get {
                return this.nbAnnotationField;
            }
            set {
                this.nbAnnotationField = value;
            }
        }
        
        /// <remarks/>
        public int score {
            get {
                return this.scoreField;
            }
            set {
                this.scoreField = value;
            }
        }
    }
}
