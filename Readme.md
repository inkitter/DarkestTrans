# Darkest Dungeon Translator
# 黑暗地牢 翻译工具
Github：https://github.com/inkitter/

> 需要安装.NET Framework 4.5。

## 根据之前的项目 pdx-ymltranslator，稍作修改，创建了 Darkest Dungeon 的汉化工具。

### 说明
* 有2个程序DarkestTrans.exe 和 FullTexttoChar.exe。
* DarkestTrans.exe 用来翻译，使用的文件夹是ori\engxml （作为原文）和 ori\chnxml（汉化）。会把翻译后的文本保存在chnxml里面
* FullTexttoChar.exe 用来编码字库。会读取ori\chnxml的文件作为汉化内容，然后根据文本与ori\font\14.fnt的内容对字符重编码，生成新的xml文件，直接保存到设置好的游戏文件夹中。
* 由于字库只支持1000个左右的字符，而160号之前已经被英文等字符占用，所以中文字符数量不能太多，大概只能有840个汉字。因此只能选择性汉化。
* 汉化完运行 游戏目录\localization 之中的localization.bat打包xml，再运行游戏即可。

* 翻译时参考已有的文本 http://www.translate.darkestdungeon.com/translations （我没有完整汉化的xml，如果谁有传给我一个，谢谢）

### 可能用到的dos命令
```
rd ori /s /q
mklink /j ori "D:\git\FullTexttoChar\FullTexttoChar\bin\Release\ori"

```