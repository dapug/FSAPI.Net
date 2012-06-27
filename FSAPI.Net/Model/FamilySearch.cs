using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FSAPI.Model
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "2.8.2.2655")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://api.familysearch.org/v1", TypeName = "familySearchProperty")]
    public partial class FamilySearchProperty
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string nameField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string valueField;


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }


        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "2.8.2.2655")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://api.familysearch.org/v1", TypeName = "familySearchError")]
    public partial class FamilySearchError
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string messageField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string detailsField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private ErrorLevel levelField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool levelFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private int subcodeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool subcodeFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private int codeField;


        [System.Xml.Serialization.XmlElement(ElementName = "message")]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }


        [System.Xml.Serialization.XmlElement(ElementName = "details")]
        public string Details
        {
            get
            {
                return this.detailsField;
            }
            set
            {
                this.detailsField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "level")]
        public ErrorLevel Level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
                this.levelFieldSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool LevelSpecified
        {
            get
            {
                return this.levelFieldSpecified;
            }
            set
            {
                this.levelFieldSpecified = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "subcode")]
        public int Subcode
        {
            get
            {
                return this.subcodeField;
            }
            set
            {
                this.subcodeField = value;
                this.subcodeFieldSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool SubcodeSpecified
        {
            get
            {
                return this.subcodeFieldSpecified;
            }
            set
            {
                this.subcodeFieldSpecified = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "code")]
        public int Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "2.8.2.2655")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://api.familysearch.org/v1", TypeName = "errorLevel")]
    public enum ErrorLevel
    {

        /// <remarks/>
        Info,

        /// <remarks/>
        Warn,

        /// <remarks/>
        Error,
    }

    //xmlns:fsapi-v1="http://api.familysearch.org/v1"

    //[KnownType(typeof(Identity.V1.Schema.Identity))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "2.8.2.2655")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://api.familysearch.org/v1", TypeName = "fsapi-v1:familySearchElement")]
    public partial class FamilySearchElement
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private List<FamilySearchError> errorsField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private List<FamilySearchProperty> propertiesField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private int statusCodeField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool statusCodeFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool deprecatedField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool deprecatedFieldSpecified;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string statusMessageField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string versionField;

        /// <summary>
        /// .ctor class constructor
        /// </summary>
        public FamilySearchElement()
        {
            if ((this.propertiesField == null))
            {
                this.propertiesField = new List<FamilySearchProperty>();
            }
            if ((this.errorsField == null))
            {
                this.errorsField = new List<FamilySearchError>();
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("error", IsNullable = false)]
        [System.Xml.Serialization.XmlArray(ElementName = "errors")]
        public List<FamilySearchError> Errors
        {
            get
            {
                return this.errorsField;
            }
            set
            {
                this.errorsField = value;
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("property", IsNullable = false)]
        [System.Xml.Serialization.XmlArray(ElementName = "properties")]
        public List<FamilySearchProperty> Properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "statusCode")]
        public int StatusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
                this.statusCodeFieldSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool StatusCodeSpecified
        {
            get
            {
                return this.statusCodeFieldSpecified;
            }
            set
            {
                this.statusCodeFieldSpecified = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "deprecated")]
        public bool Deprecated
        {
            get
            {
                return this.deprecatedField;
            }
            set
            {
                this.deprecatedField = value;
                this.deprecatedFieldSpecified = true;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool DeprecatedSpecified
        {
            get
            {
                return this.deprecatedFieldSpecified;
            }
            set
            {
                this.deprecatedFieldSpecified = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "statusMessage")]
        public string StatusMessage
        {
            get
            {
                return this.statusMessageField;
            }
            set
            {
                this.statusMessageField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "version")]
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }
}
