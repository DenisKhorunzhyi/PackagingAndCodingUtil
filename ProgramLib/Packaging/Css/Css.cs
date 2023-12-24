using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProgramLib.Packaging.Css {
    public class Css : BaseTypeFile, IPackagingText {
        /// <summary>
        /// Реалізація методу упакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public override void Packaging(string pathSource, string pathDestination) {
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
                    remainder += blockOfLine;
                    remainder = SplitText(remainder);
                    WriteToFile(DeleteComments(ref remainder), pathDestination);
                    counter++;
                }
            }
        }
        /// <summary>
        /// Виконання упакування тексту
        /// </summary>
        /// <param name="data">текст для роботи</param>
        /// <returns>результат упакування</returns>
        public string PackagingText(string data) {
            string result = SplitText(data);
            return DeleteComments(ref result);
        }
        
        /// <summary>
        /// Реалізація методу розпакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public override void Unpackaging(string pathSource, string pathDestination) {
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
                    remainder += blockOfLine;
                    WriteToFile(Unpack(ref remainder), pathDestination);
                    counter++;
                }
            }
        }
        /// <summary>
        /// Виконання розпакування тексту
        /// </summary>
        /// <param name="data">текст для роботи</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        /// <returns>результат розпакування</returns>
        public string UnpackagingText(string data, int deepLevel) {
            string result = "";
            string unpackaged = Unpack(ref data);
            var array = unpackaged.Split('\n');
            foreach (var node in array) {
                result += new StringBuilder().Append('\t', deepLevel) + node + "\n";
            }
            return result;
        }
        
        /// <summary>
        /// Розпакування
        /// </summary>
        /// <param name="data">текст для роботи</param>
        /// <returns>розпакований текст</returns>
        private string Unpack(ref string data) {
            List<BaseCssNode> cssNodes = new List<BaseCssNode>();
            do {
                data = data.Trim();
                int indexOpenStyle = data.IndexOf("{");
                int indexOpenComment = data.IndexOf("/*");
                int indexSemicolon = data.IndexOf(";");

                if ((indexOpenStyle < indexOpenComment || indexOpenComment == -1) && 
                    (indexOpenStyle < indexSemicolon || indexSemicolon == -1) && indexOpenStyle != -1){//работа со стилем
                    var cssRule = MakeCssRule(indexOpenStyle, ref data);
                    if (cssRule == null){
                        return CreateCssText(cssNodes);
                    }
                    cssNodes.Add(cssRule);
                } else if ((indexOpenComment < indexSemicolon || indexSemicolon == -1) && indexOpenComment != -1){//обработка комментария
                    int indexCloseComment = data.IndexOf("*/");
                    if (indexCloseComment == -1) return CreateCssText(cssNodes);

                    var node = RuptureDetection(indexOpenComment, indexCloseComment, ref data);
                    if(node == null) {
                        return CreateCssText(cssNodes);
                    }
                    cssNodes.Add(node);
                } else if ((indexSemicolon < indexOpenComment || indexOpenComment == -1) && indexSemicolon != -1){//работа с импортом
                    string content = data.Substring(0, indexSemicolon).Trim();
                    data = data.Remove(0, indexSemicolon + 1);
                    cssNodes.Add(new MainLine(content));
                } else {//в данных отсутствует признак комментария, стиля и импорта
                    return CreateCssText(cssNodes);
                }
            } while (data.Length > 0);
            return CreateCssText(cssNodes);
        }
        /// <summary>
        /// Определение разрывает ли комментарий блок стиля или импорта
        /// </summary>
        /// <param name="indexOpenComment"></param>
        /// <param name="indexCloseComment"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private BaseCssNode RuptureDetection(int indexOpenComment, int indexCloseComment, ref string text) {
            //с комментария начинается строка значит точно ничего не разрывает
            if(indexOpenComment == 0) {
                string comment = text.Substring(0, indexCloseComment + 2).Replace("/*", "").Replace("*/", "").Trim();
                text = text.Remove(0, indexCloseComment + 2);
                return new Comment(comment);
            }
            //движение в право для определение какому блоку принадлежит комментарий если его разрывает
            if ((indexCloseComment + 2) < text.Length) {
                for(int i = indexCloseComment + 2; i < text.Length; i++) {
                    if ((i + 1) < text.Length) {
                        string para = text[i] + "" + text[i + 1];
                        if (para.Equals("/*")) {
                            string comment = text.Substring(0, i).Replace("/*", "").Replace("*/", "").Trim();
                            text = text.Remove(0, i);
                            return new Comment(comment);
                        } else if (text[i].Equals(';')) {
                            string mainLine = text.Substring(0, i + 1).Remove(i).Trim();
                            text = text.Remove(0, i + 1);
                            return new MainLine(mainLine);
                        } else if (text[i].Equals('{')) {
                            return MakeCssRule(i, ref text);
                        }
                    }
                    else break;
                }
            }
            return null;
        }
        /// <summary>
        /// Створення об'єкту стилю Css з починаючи з вказаного індексу
        /// </summary>
        /// <param name="indexStart">початковий індекс для виявлення стилю</param>
        /// <param name="text">текст для роботи</param>
        /// <returns>об'єкт стилю Css</returns>
        private CssRule MakeCssRule(int indexStart, ref string text) {
            var cssRule = new CssRule(text.Substring(0, indexStart).Trim());
            bool isCommentFirstLevel = false;
            int indexFinish = -1;
            int indexStartProperty = indexStart + 1;
            for(int i = indexStart + 1; i < text.Length; i++){//поиск названия свойстра
                if (text[i].Equals('}') && !isCommentFirstLevel) {
                    indexFinish = i;
                    break;
                } else if (text[i].Equals(':') && !isCommentFirstLevel) {
                    string property = text.Substring(indexStartProperty, i - indexStartProperty).Trim();
                    string value = "";
                    bool isCommentSecondLevel = false;
                    for (int j = i + 1; j < text.Length; j++) {//поиск значения свойства
                        if ((j + 1) < text.Length) {
                            string para2 = text[j] + "" + text[j + 1];
                            if (para2.Equals("/*")) {
                                isCommentSecondLevel = true;
                            } else if (para2.Equals("*/")) {
                                isCommentSecondLevel = false;
                            } else if (text[j].Equals(';') && !isCommentSecondLevel) {
                                value = text.Substring(i + 1, j - i - 1);
                                indexStartProperty = j + 1;
                                i = j;
                                cssRule.DeclarationBlocks.Add(new CssRule.DeclarationBlock(property, value));
                                break;
                            }
                        }
                    }
                } else if ((i + 1) < text.Length) {
                    string para = text[i] + "" + text[i + 1];
                    if (para.Equals("/*")) {
                        isCommentFirstLevel = true;
                    } else if (para.Equals("*/")) {
                        isCommentFirstLevel = false;
                    }
                } else {//если мы попадаем сюда то это значит был обрыв блока, и стиль заканчивается в следующем блоке
                    return null;
                }
            }
            text = text.Remove(0, indexFinish + 1);
            return cssRule;
        }

        /// <summary>
        /// Створення тексту Css
        /// </summary>
        /// <param name="nodes">набір складових файлу Css</param>
        /// <returns>текст Css</returns>
        private string CreateCssText(List<BaseCssNode> nodes) {
            string result = "";
            foreach (BaseCssNode node in nodes) result += node.ToString();
            return result;
        }
        
        /// <summary>
        /// Видалення коментарів
        /// </summary>
        /// <param name="text">текст для аналізу</param>
        /// <returns>текст без коментарів</returns>
        private string DeleteComments(ref string text) {
            do {
                int indexOpenComments = text.IndexOf("/*");
                int indexCloseComments = text.IndexOf("*/");
                if (indexOpenComments >= 0 && indexCloseComments > 0) {//комментарий полностью присутствует
                    text = text.Remove(indexOpenComments, indexCloseComments - indexOpenComments + 2);
                }
                else if(indexCloseComments == -1 && indexOpenComments >= 0) {//комментарий начинается, но конец в следующем блоке данных (обрыв)
                    string tmp = text.Substring(0, indexOpenComments);
                    text = text.Remove(0, indexOpenComments);
                    return tmp;
                }
                else if(indexOpenComments == -1 && indexCloseComments == -1) {//комментарии закончились
                    string tmp = text;
                    text = "";
                    return tmp;
                }
                else {//индекс конца есть а начала нет, такое поведение свойственно ошибке в файле со стилями
                    throw new System.Exception("Файл css має помилки");
                }
            }
            while (text.Length > 0);
            return text;
        }
    }
}