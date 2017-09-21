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
    public static class XMLTools
    {
        private static TranslationResult GetTranslationFromBaiduFanyi(string q)
        {
            WebClient wc = new WebClient();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            TranslationResult result = jss.Deserialize<TranslationResult>(wc.DownloadString("http://fanyi.baidu.com/transapi?from=en&to=zh&query=" + WebUtility.UrlEncode(q)));
            return result;
            //解析json
        }

        public static string GetTranslatedTextFromAPI(string TexttoTranslate)
        {
            if (TexttoTranslate != "")
            {
                TranslationResult result = GetTranslationFromBaiduFanyi(TexttoTranslate);
                return result.Data[0].Dst;
            }
            return "Nothing";
        }
        // 用于从baidu 翻译API获取翻译。

        public static Dictionary<string, string> MakeJsonDict()
        {
            Dictionary<uint, string> dicteng = new Dictionary<uint, string>();
            Dictionary<uint, string> dictchn = new Dictionary<uint, string>();
            Dictionary<string, string> dictout = new Dictionary<string, string>();
            if (!File.Exists(StaticVars.JSONEN)| !File.Exists(StaticVars.JSONCN)) { dictout.Add("Failed", "Failed"); return dictout; }

            List<JsonSingle> Jsonlisten = ReadJsonEng(StaticVars.JSONEN);
            foreach (JsonSingle js in Jsonlisten)
            {
                uint id = Convert.ToUInt32(js.ID);
                if (!dicteng.ContainsKey(id)) { dicteng.Add(id, js.KEY); }
            }

            List<JsonSinglecn> Jsonlistcn = ReadJsonChn(StaticVars.JSONCN);
            foreach (JsonSinglecn js in Jsonlistcn)
            {
                uint id = Convert.ToUInt32(js.Base_string_id);
                if (!dictchn.ContainsKey(id)) { dictchn.Add(id, js.TEXT); }
            }

            foreach (KeyValuePair<uint, string> kvp in dicteng)
            {
                if (dictchn.ContainsKey(kvp.Key))
                {
                    if (!dictout.ContainsKey(kvp.Value))
                    {
                        dictout.Add(kvp.Value, dictchn[kvp.Key]);
                    }
                }
            }
            return dictout;
        }

        private static List<JsonSingle> ReadJsonEng(string filepath)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer() { MaxJsonLength = 999999999 };
            List<JsonSingle> result = jss.Deserialize<List<JsonSingle>>(File.ReadAllText(filepath));
            return result;
        }

        private static List<JsonSinglecn> ReadJsonChn(string filepath)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer() { MaxJsonLength = 999999999 };
            List<JsonSinglecn> result = jss.Deserialize<List<JsonSinglecn>>(File.ReadAllText(filepath));
            return result;
        }

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

        public static void OpenWithBrowser(string TextToTranslate, string APIEngine)
        {
            StringBuilder StrOpeninBrowser = new StringBuilder();
            switch (APIEngine)
            {
                case "Google":
                    StrOpeninBrowser.Append("http://translate.google.com/?#auto/zh-CN/");
                    break;
                case "Baidu":
                    StrOpeninBrowser.Append("http://fanyi.baidu.com/?#en/zh/");
                    break;
                case "Help":
                    StrOpeninBrowser.Append("https://github.com/inkitter/DarkestTrans");
                    break;
                default:
                    StrOpeninBrowser.Append("http://fanyi.baidu.com/?#en/zh/");
                    break;
            }
            StrOpeninBrowser.Append(TextToTranslate);
            System.Diagnostics.Process.Start(StrOpeninBrowser.ToString());
        }
        // 用于默认浏览器打开翻译网页

        public static string RemoveReturnMark(string input)
        {
            StringBuilder RemoveReturnText = new StringBuilder();
            RemoveReturnText.Append(input);
            RemoveReturnText.Replace("\r", "");
            RemoveReturnText.Replace("\n", "");
            return RemoveReturnText.ToString();
        }
        // 用于移除换行符。

        public static string RemoveSpace(string input)
        {
            return input.Replace(" ", "");
        }

        public static string ReplaceWithUserDict(string input, Dictionary<string, string> dict)
        {
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                Regex rgx = new Regex(RegexStringWordBoundry(kvp.Key), RegexOptions.IgnoreCase);
                input = rgx.Replace(input, " " + kvp.Value + " ");
            }
            return input;
        }

        public static string ToSimplifiedChinese(string s)
        {
            return Strings.StrConv(s, VbStrConv.SimplifiedChinese, 0);
        }
        public static string ToTraditionalChinese(string s)
        {
            return Strings.StrConv(s, VbStrConv.TraditionalChinese, 0);
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
            XmlNodeList xnlTest = xnRoot.SelectNodes("language");
            XmlNode xnEnglish=xnlTest.Item(0);
            foreach (XmlNode node in xnlTest)
            { 
                if (node.Attributes["id"].Value=="english"){ xnEnglish = node; }
            }
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
            xnlTest = xnRoot.SelectNodes("language");
            foreach (XmlNode node in xnlTest)
            {
                if (node.Attributes["id"].Value == "schinese") { xnEnglish = node; }
            }
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
            reader.Close();

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

                xml.WriteCData(data.ContentEng);

                xml.WriteEndElement(); // entry

            }
            xml.WriteEndElement(); // language

            xml.WriteStartElement("language");
            xml.WriteAttributeString("id", "schinese");
            foreach (XMLdata data in indata)
            {
                xml.WriteStartElement("entry");

                xml.WriteAttributeString("id", data.LabelName);

                xml.WriteCData(data.ContentDest);

                xml.WriteEndElement(); // entry

            }
            xml.WriteEndElement(); // language

            xml.WriteEndElement(); // root
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
