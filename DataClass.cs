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

    public class JsonSingle
    {
        public string ID { get; set; }
        public string KEY { get; set; }
        public string TEXT { get; set; }
    }
    public class JsonSinglecn
    {
        public string ID { get; set; }
        public string Base_string_id { get; set; }
        public string TEXT { get; set; }
        //public string up_votes { get; set; }
        //public string down_votes { get; set; }
        //public string is_accepted { get; set; }
    }
}
