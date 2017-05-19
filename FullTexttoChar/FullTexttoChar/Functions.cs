using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FullTexttoChar
{
    static class Functions
    {
        public static string LoadFiles(List<string> FileList)
        {
            StringBuilder StrAll = new StringBuilder();
            foreach (string file in FileList)
            {
                StrAll.Append(File.ReadAllText(file));
            }
            return StrAll.ToString();
        }

        public static void WriteXMLFileFolder(string OriginalPath, string OutputPath,List<string> FileNames, Dictionary<char, uint> DictionaryNewCode,bool ReplaceCode)
        {
            string oripath, outpath;
            foreach(string file in FileNames)
            {
                string f = Path.GetFileName(file);
                oripath = OriginalPath + f;
                outpath = OutputPath + f;
                WriteXMLFileWithCode(oripath, outpath, DictionaryNewCode,ReplaceCode);
            }
        }

        public static void WriteXMLFileWithCode(string OriginalPath,string OutputPath,Dictionary<char,uint> DictionaryNewCode, bool ReplaceCode)
        {
            char[] FileCharArray = File.ReadAllText(OriginalPath).ToCharArray();
            StringBuilder outstr = new StringBuilder();
            if (ReplaceCode)
            {
                for (int i = 0; i < FileCharArray.Length; i++)
                {
                    if (DictionaryNewCode.ContainsKey(FileCharArray[i]))
                    {
                        outstr.Append(Convert.ToChar(DictionaryNewCode[FileCharArray[i]]));
                    }
                    else
                    {
                        outstr.Append(FileCharArray[i]);
                    }
                }
            }
            else
            {
                outstr.Append(FileCharArray);
            }
            File.WriteAllText(OutputPath, outstr.ToString());
        }

        public static void WriteFntFileWithCode(string OriginalFntPath,string OutputFilePath,Dictionary<char,uint> DictionaryOldCode, Dictionary<char, uint> DictionaryNewCode,bool ReplaceCode)
        {
            Dictionary<string, string> DictFntStringReplace = new Dictionary<string, string>();
            foreach (char ch in DictionaryOldCode.Keys)
            {
                string pre = "char id=";
                DictFntStringReplace.Add(pre + DictionaryOldCode[ch].ToString()+" ", pre + DictionaryNewCode[ch].ToString() + " ");
            }
            // 根据新旧字典，组成 "char id=xxxx"的新旧字典

            string[] OriginalFntFileLines = File.ReadAllLines(OriginalFntPath);
            StringBuilder NewFile = new StringBuilder();

            for (int i = 0; i <= 99; i++)
            {
                NewFile.AppendLine(OriginalFntFileLines[i]);
            }
            // 前99行照搬，因为是英文。

            foreach (string line in OriginalFntFileLines)
            {
                foreach (string key in DictFntStringReplace.Keys)
                {
                    if (ReplaceCode)
                    {
                        if (line.Contains(key))
                        {
                            NewFile.AppendLine(line.Replace(key, DictFntStringReplace[key]));
                        }
                    }
                    else
                    {
                        if (line.Contains(key))
                        {
                            NewFile.AppendLine(line);
                        }
                    }
                }
            }
            // 按行读取并用字典替换成新的code。

            File.WriteAllText(OutputFilePath, NewFile.ToString());
            // 写入文件
        }

        public static Tuple<Dictionary<char, uint>,Dictionary<char, uint>> MakeOldCodeDict(char[] CharArray)
        {
            Dictionary<char, uint> dictcode = new Dictionary<char, uint>();
            Dictionary<char, uint> dictcount = new Dictionary<char, uint>();
            foreach (char ch in CharArray)
            {
                uint chcode = Convert.ToUInt32(ch);
                if (chcode < 160) { continue; }
                if (!dictcode.ContainsKey(ch))
                {
                    dictcode.Add(ch, chcode);
                    dictcount.Add(ch, 1);
                }
                else
                {
                    uint ls = dictcount[ch];
                    dictcount.Remove(ch);
                    dictcount.Add(ch, ls + 1);
                }
            }
            return new Tuple<Dictionary<char, uint>, Dictionary<char, uint>>(dictcode,dictcount);
        }

        public static Dictionary<char, uint> MakeNewCodeDict(Dictionary<char,uint> OldDict,uint StartCode)
        {
            Dictionary<char, uint> dict = new Dictionary<char, uint>();
            if (StartCode <= 127) { StartCode = 128; }
            foreach (char ch in OldDict.Keys)
            {
                if (!dict.ContainsKey(ch))
                {
                    dict.Add(ch, StartCode);
                }
                StartCode++;
            }
            return dict;
        }
    }
}
