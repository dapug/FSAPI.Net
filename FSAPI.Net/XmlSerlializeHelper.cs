// ----------------------------------------------------------------------
// <copyright file="XmlSerlializeHelper.cs" company="nGenesis, LLC">
//     Copyright (c) 2012, nGenesis, LLC. 
//     All rights reserved. This program and the accompanying materials are made available under the terms of the Eclipse Public License v1.0 
//     which accompanies this distribution (Liscense.htm), and is available at http://www.eclipse.org/legal/epl-v10.html 
//     
//     Contributors: 
//         dapug - Initial author, core functionality
// </copyright>
//
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace FSAPI
{
    /// <summary>
    /// Facade to XML serialization and deserialization of an object to/from an XML file.
    /// </summary>
    internal partial class XmlSerlializeHelper
    {
        /// <summary>
        /// Serialization format types.
        /// </summary>
        internal enum SerializedFormat
        {
            /// <summary>
            /// Binary serialization format.
            /// </summary>
            Binary,

            /// <summary>
            /// Document serialization format.
            /// </summary>
            Document
        }	

        /// <summary>
        /// Private contructor to prevent instantiation of 'static' class.
        /// </summary>
        private XmlSerlializeHelper()
        {

        }

        #region Load methods

        /// <summary>
        /// Loads an object from an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static Object Load(Object serializableObject, string path)
        {
            serializableObject = LoadFromDocumentFormat(serializableObject, null, path);
            return serializableObject;
        }

        public static Object Load(Object serializableObject, StreamReader reader)
        {
            serializableObject = LoadFromStream(serializableObject, null, reader);
            return serializableObject;
        }

#if !(SILVERLIGHT || WP7)
        public static Object Load(Object serializableObject, System.Xml.XmlTextReader reader)
        {
            serializableObject = LoadFromXml(serializableObject, null, reader);
            return serializableObject;
        }
#endif

        public static Object Load(Object serializableObject, StringReader reader)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(serializableObject.GetType());

            return xs.Deserialize(reader);
        }//StringReader

        public static Object LoadFromString(Object serializableObject, string xmlFSData)
        {
            return Load(serializableObject, new StringReader(xmlFSData));

        }
#if (SILVERLIGHT || WP7)
        public static Object LoadFromStringV3(Type objType, string xmlData)
        {
            System.Runtime.Serialization.DataContractSerializer ds = new System.Runtime.Serialization.DataContractSerializer(objType);

            Stream s = new MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(xmlData));
 
             return ds.ReadObject(s); ;
        }
#endif

        /// <summary>
        /// Loads an object from an XML file using a specified serialized format.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, @"C:\XMLObjects.xml", SerializedFormat.Binary);
        /// </code>
        /// </example>		
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="serializedFormat">XML serialized format used to load the object.</param>
        /// <returns>Object loaded from an XML file using the specified serialized format.</returns>
        public static Object Load(Object serializableObject, string path, SerializedFormat serializedFormat)
        {
            switch (serializedFormat)
            {


                case SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(serializableObject, null, path);
                    break;
            }

            return serializableObject;
        }

        /// <summary>
        /// Loads an object from an XML file in Document format, supplying extra data types to enable deserialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static Object Load(Object serializableObject, string path, System.Type[] extraTypes)
        {
            serializableObject = LoadFromDocumentFormat(serializableObject, extraTypes, path);
            return serializableObject;
        }

        #endregion

        #region Save methods

        /// <summary>
        /// Saves an object to an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="nameSpace">Namespace to put in root node</param>
        /// <param name="path">Path of the file to save the object to.</param>
        public static void Save(Object serializableObject, string nameSpace, string path)
        {
            SaveToDocumentFormat(serializableObject, nameSpace, null, path);
        }

        public static void Save(Object serializableObject, string path)
        {
            Save(serializableObject, null, path);
        }

        /// <summary>
        /// Saves an object to an XML file using a specified serialized format.
        /// </summary>
        /// <example>
        /// <code>
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, @"C:\XMLObjects.xml", SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="nameSpace">Namespace to put in root node</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="serializedFormat">XML serialized format used to save the object.</param>
        public static void Save(Object serializableObject, string nameSpace, string path, SerializedFormat serializedFormat)
        {
            switch (serializedFormat)
            {

                case SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, nameSpace, null, path);
                    break;
            }
        }

        public static void Save(Object serializableObject, string path, SerializedFormat serializedFormat)
        {
            Save(serializableObject, null, path, serializedFormat);
        }

        /// <summary>
        /// Saves an object to an XML file in Document format, supplying extra data types to enable serialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="nameSpace">Namespace to put in root node</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
        public static void Save(Object serializableObject, string nameSpace, string path, System.Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, nameSpace, extraTypes, path);
        }

        public static void Save(Object serializableObject, string fileName, System.Type[] extraTypes)
        {
            Save(serializableObject, null, fileName, extraTypes);
        }

        #endregion

        #region Private

        private static FileStream CreateFileStream(string path)
        {
            FileStream fileStream = null;

            fileStream = new FileStream(path, FileMode.OpenOrCreate);

            return fileStream;
        }

        private static Object LoadFromDocumentFormat(Object serializableObject, System.Type[] extraTypes, string path)
        {
            using (TextReader textReader = CreateTextReader(path))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, extraTypes);
                serializableObject = xmlSerializer.Deserialize(textReader);
            }

            return serializableObject;
        }

        private static Object LoadFromStream(Object serializableObject, System.Type[] extraTypes, StreamReader reader)
        {
            using (TextReader textReader = CreateTextReader(reader))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, extraTypes);
                serializableObject = xmlSerializer.Deserialize(textReader);
            }

            return serializableObject;
        }

#if !(SILVERLIGHT || WP7)
        private static Object LoadFromXml(Object serializableObject, System.Type[] extraTypes, System.Xml.XmlTextReader reader)
        {
            XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, extraTypes);
            serializableObject = xmlSerializer.Deserialize(reader);

            return serializableObject;
        }
#endif

        private static TextReader CreateTextReader(string path)
        {
            TextReader textReader = null;

            textReader = new StreamReader(path);

            return textReader;
        }

        private static TextReader CreateTextReader(StreamReader reader)
        {
            TextReader textReader = null;

            textReader = reader;

            return textReader;
        }


        private static TextWriter CreateTextWriter(string path)
        {
            TextWriter textWriter = null;

            textWriter = new StreamWriter(path);

            return textWriter;
        }

        private static XmlSerializer CreateXmlSerializer(Object serializableObject, System.Type[] extraTypes)
        {
            Type ObjectType = serializableObject.GetType();

            XmlSerializer xmlSerializer = null;

            if (extraTypes != null)
                xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
            else
                xmlSerializer = new XmlSerializer(ObjectType);

            return xmlSerializer;
        }

        private static void SaveToDocumentFormat(Object serializableObject, string nameSpace, System.Type[] extraTypes, string path)
        {
            using (TextWriter textWriter = CreateTextWriter(path))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, extraTypes);

                //ADDED THIS TO REMOVE DEFAULT NAMESPACE JUNK FROM HEADER

                if (nameSpace != null && nameSpace.Length > 0)
                {
                    //Create our own namespaces for the output
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                    //Add an empty namespace and empty value
                    ns.Add("", nameSpace);

                    xmlSerializer.Serialize(textWriter, serializableObject, ns);
                }
                else
                    xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }

        public static string SaveToString(Object serializableObject, string nameSpace, System.Text.Encoding encoding, string namespaceXsi)
        {
            XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, null);

            //StringWriter sr = new StringWriter();
            StringWriterWithEncoding sw = new StringWriterWithEncoding(encoding);

            //ADDED THIS TO REMOVE DEFAULT NAMESPACE JUNK FROM HEADER

            if (nameSpace != null && nameSpace.Length > 0)
            {
                //Create our own namespaces for the output
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                //Add an empty namespace and empty value


                ns.Add("xsi", namespaceXsi);
                ns.Add("", nameSpace);



                xmlSerializer.Serialize(sw, serializableObject, ns);
            }
            else
                xmlSerializer.Serialize(sw, serializableObject);


            return sw.ToString();
        }


        #endregion
    }

    internal class StringWriterWithEncoding : StringWriter
    {
        System.Text.Encoding encoding;

        public StringWriterWithEncoding(System.Text.Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override System.Text.Encoding Encoding
        {
            get { return encoding; }
        }
    }
}
