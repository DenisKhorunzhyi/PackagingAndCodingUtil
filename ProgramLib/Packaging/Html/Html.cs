using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProgramLib.Packaging.Html {
    public class Html : BaseTypeFile {
        /// <summary>
        /// Виконання операції пакування чи розпакування для файлу в поточному режимі відповідно до режиму роботи 
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public void ScanAndCreateFile(string pathSource, string pathDestination) {
            deepLevel = 0;
            File.WriteAllText(pathDestination, "");
            using (StreamReader reader = new StreamReader(pathSource)) {
                long fileSize = new FileInfo(pathSource).Length;//Bytes
                int counter = 1;
                string remainder = "   ";
                while (!reader.EndOfStream && remainder.Length > 0) {
                    char[] buffer = (fileSize > (counter * blockSize)) ? new char[blockSize]
                                                                       : new char[((counter - 1) == 0) ? fileSize
                                                                                                       : fileSize - ((counter - 1) * blockSize)];
                    reader.ReadBlock(buffer, 0, (buffer.Length < blockSize) ? buffer.Length : blockSize);
                    string blockOfLine = new string(buffer);
                    remainder += blockOfLine;// SplitText(blockOfLine);
                    DoWork(ref remainder);
                    WriteToFile(result, pathDestination);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Реалізація методу упакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public override void Packaging(string pathSource, string pathDestination) {
            currentMode = Mode.packaging;
            ScanAndCreateFile(pathSource, pathDestination);
        }
        /// <summary>
        /// Реалізація методу розпакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public override void Unpackaging(string pathSource, string pathDestination) {
            currentMode = Mode.unpackaging;
            ScanAndCreateFile(pathSource, pathDestination);
        }


        /// <summary>
        /// Виконання роботи упакування/розпакування для виділеного фрагменту тексту
        /// </summary>
        /// <param name="text">текст для роботи</param>
        public void DoWork(ref string text) {
            result = "";
            do {
                try {
                    if (!CreateOneLevelTreeOfFileStruct(ref text, deepLevel)) return;
                } catch { return; }
            } while(text.Length > 0);
        }

        /// <summary>
        /// режим роботи, упакування чи розпакування
        /// </summary>
        Mode currentMode;
        /// <summary>
        /// Зміна для збереження упакованого чи розпакованого тексту до того часу, поки його не буде записано у файл
        /// </summary>
        string result;
        /// <summary>
        /// рівень позлиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою 
        /// </summary>
        int deepLevel;

        /// <summary>
        /// Виконання операції виявлення тегу та збереження його у вихідний файл
        /// </summary>
        /// <param name="text">текст для роботи</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        /// <returns>true - коректне завершення/вихід із нижчого тегу при скінчені однорівневих тегів, 
        /// false - виявлення розриву у більш глибокому тегу</returns>
        public bool CreateOneLevelTreeOfFileStruct(ref string text, int deepLevel) {
            Regex tagRegex = new Regex("(<|</){1}[^<>]+(>|/>){1}");
            do {
                text = text.Trim();
                Match tag = tagRegex.Match(text);

                if (tag.Index == 0 && (tag.Index != text.Length - 1)) { }
                else if (tag.Index > 0) {//перед тегом был текст который принадлежит родителю
                    string _text = text.Substring(0, tag.Index).Trim();
                    WriteResult(_text, deepLevel);
                    text = text.Remove(0, tag.Index);
                    continue;
                } else if (tag.Index == -1 && text.Length > 0) {//присутствует комбинация текста но тега не обнаружена - обрыв
                    this.deepLevel = deepLevel;
                    throw new IndexOutOfRangeException("");
                } else if (tag.Index == text.Length - 1) {//индекс открывающего тега является последним в тексте - обрыв
                    this.deepLevel = deepLevel;
                    throw new IndexOutOfRangeException("");
                }

                if (tag.Value.IndexOf("</") >= 0) {//обнаружен конец родительского тега
                    text = text.Remove(0, tag.Length);
                    this.deepLevel = deepLevel - 1;
                    WriteResult(tag.Value, deepLevel - 1);
                    return true;
                }

                //при упаковке все комментарии должны исчезнуть
                if (tag.Value.IndexOf("<!--") >= 0){//текущий тег комментарий
                    if (currentMode == Mode.packaging) {
                        text = text.Remove(tag.Index, tag.Length);
                        continue;
                    } else {//unpackaging
                        text = text.Remove(0, tag.Length);
                        WriteResult(tag.Value, deepLevel);
                        continue;
                    }
                }

                //работа с одноуровневыми тегами
                string textTag = "";
                if ((textTag = IsCssOrJsTag(tag.Value)) != null) {//работа со вкроплением css-стилей или js-кода
                    if (!MakeCssOrJsCode(ref text, textTag, deepLevel)) {// обрыв
                        this.deepLevel = deepLevel;
                        throw new IndexOutOfRangeException("");
                    }
                } else if (IsSingleTag(tag.Value)) {//одиночные теги
                    text = text.Remove(0, tag.Length);
                    WriteResult(tag.Value, deepLevel);
                } else {
                    text = text.Remove(0, tag.Length);
                    WriteResult(tag.Value, deepLevel);
                    if (tag.Value.IndexOf(">") >= 0) {
                        try {
                            if (!CreateOneLevelTreeOfFileStruct(ref text, deepLevel + 1)) return false;
                        } catch { return false; }
                    }
                }
            } while (text.Trim().Length > 0);
            return true;
        }
        

        /// <summary>
        /// додання до проміжного результату до глобальної зміної
        /// </summary>
        /// <param name="content">контент</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        private void WriteResult(string content, int deepLevel) {
            switch (currentMode) {
                case Mode.packaging:
                    result += content;
                    break;
                case Mode.unpackaging:
                    result += SetTextOnLevel(content, deepLevel);
                    break;
            }
        }


        /// <summary>
        /// Виконання упакування/розпакування Css або Js
        /// </summary>
        /// <param name="text">текст</param>
        /// <param name="indexOpenTag">індекс початку тегу</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        /// <returns>false - обрив, true - успішне заверешення</returns>
        private bool MakeCssOrJsCode(ref string text, string tag, int deepLevel) {
            Regex tagOpen = new Regex(@"<\s*(" + tag.Trim() + @")(\s*([^\s<>]+)*)*(>|/>){1}");
            Regex tagClose = new Regex(@"(</)(\s){0,}(" + tag.Trim() + @")(\s){0,}(>){1}");

            Match open = tagOpen.Match(text);
            Match close = tagClose.Match(text);

            if(open.Value.Length > 0 && open.Value.IndexOf("/>") >= 0) {//одинарний тег
                WriteResult(open.Value, deepLevel);
                text = text.Remove(0, open.Index + open.Length);
                return true;
            } else if(open.Value.Length > 0 && close.Value.Length > 0 && open.Index < close.Index) {//двойной тег
                string content = text.Substring(open.Index + open.Length, (close.Index) - (open.Index + open.Length)).Trim();
                content = MakeResult(content, tag, deepLevel + 1);
                //write result
                WriteResult(open.Value, deepLevel);
                if(!string.IsNullOrEmpty(content.Trim()))
                    result += content;
                WriteResult(close.Value, deepLevel);
                text = text.Remove(0, close.Index + close.Length);
                return true;
            }
            else {//розрыв
                this.deepLevel = deepLevel;
                return false;
            }
        }
        
        /// <summary>
        /// Виконання упакування/розпакування скрипту чи стилів
        /// </summary>
        /// <param name="content">контент</param>
        /// <param name="jsOrCss">вказання із яким типом контексту необхідно працювати (script/style)</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        /// <returns>упакований/розпакований стиль чи скрипт</returns>
        private string MakeResult(string content, string jsOrCss, int deepLevel) {
            string result = "";
            IPackagingText type = null;
            switch (jsOrCss) {
                case "script":
                    type = new Js();
                    break;
                case "style":
                    type = new Css.Css();
                    break;
            }
            switch (currentMode) {
                case Mode.packaging:
                    result = type.PackagingText(content);
                    break;
                case Mode.unpackaging:
                    result = type.UnpackagingText(content, deepLevel);
                    break;
            }
            return result;
        }

        /// <summary>
        /// Перевірка тегу на те чи відноситься він до скрипту чи стилів
        /// </summary>
        /// <param name="tag">весь тег</param>
        /// <returns>висновок відностно пото чи відноситься він до скрипту чи стилів</returns>
        private string IsCssOrJsTag(string tag) {
            string _tag = tag.Remove(0, 1).Trim().ToLower();
            if (_tag.IndexOf("script") == 0)
                return "script";
            else if (_tag.IndexOf("style") == 0)
                return "style";
            return null;
        }
        /// <summary>
        /// Перевірка тега та приналежність до групи одинарних тегів
        /// </summary>
        /// <param name="tag">текст тега</param>
        /// <returns>висновок відносно того чи є тег одинарним</returns>
        private bool IsSingleTag(string tag) {
            string _tag = tag.Remove(0, 1).Trim().ToLower();
            string[] list = new string[] {
                "!doctype","area","base","br","col","embed","hr","img","input","keygen","link","meta","param","source","track","wbr"
            };
            for (int i = 0; i < list.Length; i++)
                if (_tag.IndexOf(list[i]) == 0) return true;
            return false;
        }
        
        /// <summary>
        /// Встановлення символів табуляції для строчки тексту
        /// </summary>
        /// <param name="text">текст</param>
        /// <param name="level">кількість символів табуляції</param>
        /// <returns>строчка із вставленими символами табуляції</returns>
        private string SetTextOnLevel(string text, int level) {
            int _level = (level < 0) ? 0 : level;
            return new StringBuilder().Append('\t', _level) + text.Trim() + "\n";
        }
    }
    enum Mode {
        packaging, unpackaging
    }
}
