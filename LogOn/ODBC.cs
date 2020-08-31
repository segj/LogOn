using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace LogOn
{
    public class ODBC
    {
        public string OdbcNavn { get; set; }
        public string OdbcType { get; set; }
        public string LastCreator { get; set; }
        public string LastUser { get; set; }
        public string LastCryptPw { get; set; }
        public string LastPWChange { get; set; }


        public ODBC(string name)
        {
            OdbcNavn = name;
        }
        public ODBC()
        {

        }

        public void gemSetting(ODBC _Odbc, string fRuninfo)
        {
            var path = fRuninfo.Trim();
            System.Xml.Serialization.XmlSerializer writer =
                             new System.Xml.Serialization.XmlSerializer(typeof(ODBC));
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, _Odbc);
            file.Close();
        }
        public ODBC HentSetting(string fnavn)
        {
            var path = fnavn;
            ODBC s1 = new ODBC();
            //ODBC s1 = this;
            if (!File.Exists(path))
            {
                return (s1);
                //throw new System.InvalidOperationException("System.xml cannot be found");
            }
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ODBC));

            StreamReader file = new System.IO.StreamReader(path);
            s1 = (ODBC)reader.Deserialize(file);
            file.Close();
            return (s1);
        }

    }
}
