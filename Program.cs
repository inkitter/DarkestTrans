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


namespace DarkestTrans
{
    static class StaticVars
    {
        public const string UserDictCSV = "userdict.csv";
        public const string DIREN = @"ori\engxml\";
        public const string DIRCN = @"ori\chnxml\";
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmTranslator());
        }
    }
    public class XMLdata
    {
        private string contentdest,contenteng, keyname;  //3个变量

        private XMLdata()
        {
            contenteng = "";
            contentdest = "";
        }
        public XMLdata(string Key,string Eng):this()
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

    public class TranslationResultVIP
    {
        //错误码，翻译结果无法正常返回
        public string Error_code { get; set; }
        public string Error_msg { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Query { get; set; }
        //翻译正确，返回的结果
        //这里是数组的原因是百度翻译支持多个单词或多段文本的翻译，在发送的字段q中用换行符（\n）分隔
        public Translation[] Trans_result { get; set; }
    }

    public class TranslationResult
    {
        //public string From { get; set; }
        //public string To { get; set; }
        public Translation[] Data { get; set; }
    }

    public class Translation
    {
        public string Src { get; set; }
        public string Dst { get; set; }
    }

    public class FileExistInfo
    {
        public bool IsExist { get; set; }
        public string FileName { get; set; }
    }

    public class LoadedFileInfo
    {
        public bool IsTranslationExist { get; set; }
        public string FileName { get; set; }
    }

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
        public static string RegexGetName(string RegText)
        {
            return RegexGetWith(RegText, "(^.*?):.*?(?=\")");
        }
        public static string RegexGetValue(string RegText)
        {
            return RegexGetWith(RegText, "(?<=(\\s\")).+(?=\")");
        }
        public static string RegexGetNameOnly(string RegText)
        {
            RegText = RegText.Replace(" ", "");
            return RegexGetWith(RegText, "^.*(?=:)");
        }
        public static string RegexRemoveColorSign(string RegText)
        {
            return RegexGetWith(RegText, "(?<=(§.)).+(?=(§!))");
        }
        private static string RegexStringWordBoundry(string input)
        {
            string re = @"(?<=(\W|^))" + input + @"(?=(\W|$))";
            return "("+re+")(?!})";
        }
        public static bool RegexContainsWord(string input, string WordToMatch)
        {
            if (Regex.IsMatch(input, RegexStringWordBoundry(WordToMatch), RegexOptions.IgnoreCase)) { return true; }
            return false;
        }
        // 用于截取

        public static void OpenWithBrowser(string TextToTranslate,string APIEngine)
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
                    StrOpeninBrowser.Append("https://github.com/inkitter/pdx-ymltranslator");
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
                input = rgx.Replace(input, " "+kvp.Key + "<" + kvp.Value + "> ");
            }
            return input;
        }

        public static Dictionary<string, string> BuildDictionary(List<string> list)
        {
            Dictionary<string, string> returnDict = new Dictionary<string, string>();
            foreach (string line in list)
            {
                string vn = RegexGetNameOnly(RegexGetName(line));
                if (!returnDict.ContainsKey(vn))
                {
                    returnDict.Add(vn, line);
                }
            }
            return returnDict;
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

            XmlDocument xdoc = new XmlDocument() ;
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

            XmlTextWriter xml = new XmlTextWriter(filepath, Encoding.UTF8);

            xml.Formatting = Formatting.Indented;
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
        public static List<XMLdata> ReplaceWithDict(List<XMLdata> datalist,Dictionary<string,string> dict)
        {
            foreach(XMLdata da in datalist)
            {
                foreach(string key in dict.Keys)
                {
                    string re= Regex.Replace(da.ContentDest, RegexStringWordBoundry(key), dict[key], RegexOptions.IgnoreCase);
                    da.ApplyLine(re);
                }
            }
            return datalist;
        }
    }

}
