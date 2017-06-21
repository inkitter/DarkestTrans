using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.IO;
using System.Xml;
using System.Linq;

namespace DarkestTrans
{
    static class StaticVars
    {
        public const string UserDictCSV = "userdict.csv";
        public const string DIREN = @"ori\engxml\";
        public const string DIRCN = @"ori\chnxml\";
        public const string JSONEN = @"ori\en.json";
        public const string JSONCN = @"ori\chn.json";
    }
    public class XMLdata
    {
        private string contentdest, contenteng, keyname;  //3个变量

        private XMLdata()
        {
            contenteng = "";
            contentdest = "";
        }
        public XMLdata(string Key, string Eng) : this()
        {
            contenteng = Eng;
            keyname = Key;
        }

        public string LabelName
        {
            get
            {
                return keyname;
            }
        }
        public string ContentEng
        {
            get
            {
                return contenteng;
            }
        }

        public string ContentDest
        {
            get
            {
                if (contentdest == "") { return ContentEng; }
                return contentdest;
            }
        }

        public void ApplyLine(string ApplyText)
        {
            contentdest = ApplyText;
        }

        public bool SameInToAndFrom()
        {
            if (contenteng != "" && contenteng != contentdest) { return false; }
            return true;
        }
    }
    public static class XMLTools
    {
        public static string RegexGetWith(string RegText, string RegexRule)
        {
            Regex Reggetname = new Regex(RegexRule, RegexOptions.None);
            StringBuilder returnString = new StringBuilder();
            var matches = Reggetname.Matches(RegText);

            foreach (var item in matches)
            {
                returnString.Append(item.ToString());
            }
            return returnString.ToString();
        }
        private static string RegexStringWordBoundry(string input)
        {
            string re = @"(?<=(\W|^))" + input + @"(?=(\W|$))";
            return "(" + re + ")(?!})";
        }
        public static bool RegexContainsWord(string input, string WordToMatch)
        {
            if (Regex.IsMatch(input, RegexStringWordBoundry(WordToMatch), RegexOptions.IgnoreCase)) { return true; }
            return false;
        }
        // 用于截取

        public static string RemoveSpace(string input)
        {
            return input.Replace(" ", "");
        }

        public static List<XMLdata> ReadFile(string filename)
        {
            List<XMLdata> lstXMLdata = new List<XMLdata>();

            XmlDocument xdoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings() { IgnoreComments = true };

            string filepath = StaticVars.DIREN + filename;
            XmlReader reader = XmlReader.Create(filepath, settings);
            xdoc.Load(reader);

            XmlNode xnRoot = xdoc.SelectSingleNode("root");
            XmlNode xnEnglish = xnRoot.SelectSingleNode("language");
            XmlNodeList xnl = xnEnglish.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                lstXMLdata.Add(new XMLdata(xn.Attributes["id"].Value, xn.InnerText.Replace("<![CDATA[", "").Replace("]]>", "")));
            }
            if (lstXMLdata.Count < 2) { lstXMLdata.Add(new XMLdata("a", "a")); }
            // 获取 id，CDATA 将英文载入 List XMLdata

            Dictionary<string, string> DictChn = new Dictionary<string, string>();
            DictChn.Clear();
            filepath = StaticVars.DIRCN + filename;
            reader = XmlReader.Create(filepath, settings);
            xdoc.Load(reader);

            xnRoot = xdoc.SelectSingleNode("root");
            xnEnglish = xnRoot.SelectSingleNode("language");
            xnl = xnEnglish.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                if (!DictChn.ContainsKey(xn.Attributes["id"].Value))
                {
                    DictChn.Add(xn.Attributes["id"].Value, xn.InnerText.Replace("<![CDATA[", "").Replace("]]>", ""));
                }
            }
            // 中文载入字典

            foreach (XMLdata data in lstXMLdata)
            {
                if (DictChn.ContainsKey(data.LabelName))
                {
                    data.ApplyLine(DictChn[data.LabelName]);
                }
            }
            // 将字典读入 List

            return lstXMLdata;
        }

        public static void SaveMod(string filename, List<XMLdata> indata)
        {
            string filepath = StaticVars.DIRCN + filename;

            XmlTextWriter xml = new XmlTextWriter(filepath, Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            xml.WriteStartDocument();
            xml.WriteStartElement("root");
            xml.WriteStartElement("language");
            xml.WriteAttributeString("id", "english");
            foreach (XMLdata data in indata)
            {
                xml.WriteStartElement("entry");

                xml.WriteAttributeString("id", data.LabelName);

                xml.WriteCData(data.ContentDest);

                xml.WriteEndElement();

            }
            xml.WriteEndElement();
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();
        }
        public static List<XMLdata> ReplaceWithDict(List<XMLdata> datalist, Dictionary<string, string> dict)
        {
            foreach (XMLdata da in datalist)
            {
                foreach (string key in dict.Keys)
                {
                    string re = Regex.Replace(da.ContentDest, RegexStringWordBoundry(key), dict[key], RegexOptions.IgnoreCase);
                    da.ApplyLine(re);
                }
            }
            return datalist;
        }
        public static List<XMLdata> ReplaceWithJson(List<XMLdata> datalist, Dictionary<string, string> dict)
        {
            foreach (XMLdata da in datalist)
            {
                if (dict.ContainsKey(da.LabelName))
                {
                    da.ApplyLine(dict[da.LabelName]);
                }
            }
            return datalist;
        }
    }
}
