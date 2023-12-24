using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PackagingAndCodingUtil.Encode {
    public static class EncodeMaster {
        private const int blockSize = 1024 * 1024 * 6;//6 MB

        /// <summary>
        /// Виконання запису файлу, заміщення початкового файлу закодованим
        /// </summary>
        /// <param name="path"></param>
        /// <param name="srcEncoding"></param>
        /// <param name="dstEncoding"></param>
        private static void WorkWithFile(string path, Encoding srcEncoding, Encoding dstEncoding) {
            string newName = Path.Combine(Path.GetDirectoryName(path), "~" + Path.GetFileName(path));
            WorkWithFile(path, newName, srcEncoding, dstEncoding);
            File.Copy(newName, path, true);
            File.Delete(newName);
        }
        /// <summary>
        /// Виконання запису файлу
        /// </summary>
        /// <param name="pathSource">посилання на початковий файл</param>
        /// <param name="pathDestination">посилання на результуючий файл</param>
        /// <param name="srcEncoding">початкова кодировка</param>
        /// <param name="dstEncoding">результуюча кодировка</param>
        private static void WorkWithFile(string pathSource, string pathDestination, Encoding srcEncoding, Encoding dstEncoding) {
            File.WriteAllText(pathDestination, "");
            using (StreamReader reader = new StreamReader(pathSource, srcEncoding)) {
                long fileSize = new FileInfo(pathSource).Length;//Bytes
                int counter = 1;
                while (!reader.EndOfStream) {
                    char[] buffer = (fileSize > (counter * blockSize)) ? new char[blockSize]
                                                                       : new char[((counter - 1) == 0) ? fileSize
                                                                                                        : fileSize - ((counter - 1) * blockSize)];
                    reader.ReadBlock(buffer, 0, (buffer.Length < blockSize) ? buffer.Length : blockSize);
                    
                    var writer = new StreamWriter(pathDestination, true, dstEncoding);
                    writer.Write(buffer);
                    writer.Close();
                    counter++;
                }
            }
        }

        /// <summary>
        /// Повернення кодування відповідно перерахування
        /// </summary>
        /// <param name="table">обана таблиця кодування</param>
        /// <returns>об'єкт для кодування</returns>
        private static Encoding GetEncoding(EncodeTable table)
        {
            switch (table)
            {
                case EncodeTable.ASCII:
                    return Encoding.ASCII;
                case EncodeTable.Unicode:
                    return Encoding.Unicode;
                case EncodeTable.UTF8:
                    return Encoding.UTF8;
                case EncodeTable.Windows1251:
                    return Encoding.GetEncoding("Windows-1251");
            }
            return Encoding.Default;
        }

        /// <summary>
        /// Виконання кодування
        /// </summary>
        /// <param name="srcEncoding">початкова таблиця кодування</param>
        /// <param name="dstEncoding">результуюча таблиця кодування</param>
        /// <param name="pathToFiles">список файлів для кодування</param>
        public static void Convert(EncodeTable srcEncoding, EncodeTable dstEncoding, List<string> pathToFiles) {
            Encoding _srcEncoding = GetEncoding(srcEncoding);
            Encoding _dstEncoding = GetEncoding(dstEncoding);
            foreach (string path in pathToFiles) {
                if (File.Exists(path)) {
                    WorkWithFile(path, _srcEncoding, _dstEncoding);
                }
            }
        }
        /// <summary>
        /// Виконання кодування
        /// </summary>
        /// <param name="srcEncoding">початкова таблиця кодування</param>
        /// <param name="dstEncoding">результуюча таблиця кодування</param>
        /// <param name="text">текст для кодування</param>
        /// <returns>закодований текст</returns>
        public static string Convert(EncodeTable srcEncoding, EncodeTable dstEncoding, string text)
        {
            Encoding _srcEncoding = GetEncoding(srcEncoding);
            Encoding _dstEncoding = GetEncoding(dstEncoding);
            byte[] sourceData = _srcEncoding.GetBytes(text);
            byte[] result = Encoding.Convert(_srcEncoding, _dstEncoding, sourceData);
            return _dstEncoding.GetString(result);
        }

        /// <summary>
        /// Заміна роздільника кінця строчки
        /// </summary>
        /// <param name="pathToFiles">посилання на файл</param>
        /// <param name="endLineSeparator">роздільник кінця строчки</param>
        public static void ReplaceEndSeparator(List<string> pathToFiles, EndLineSeparator endLineSeparator) {
            string oldChar, newChar;
            if (endLineSeparator == EndLineSeparator.N) {
                oldChar = "\r\n";
                newChar = "\n";
            } else {
                oldChar = "\n";
                newChar = "\r\n";
                ReplaceAll(pathToFiles, newChar, oldChar);//Приведение в нормальный вид, что бы лишние \r не плодились, в случае если в файле было \r\n
            }
            ReplaceAll(pathToFiles, oldChar, newChar);
        }
        /// <summary>
        /// Заміна роздільника кінця строчки
        /// </summary>
        /// <param name="text">текст</param>
        /// <param name="endLineSeparator">роздільник кінця строчки</param>
        /// <returns>текст із зміненими роздільниками кінця строчки</returns>
        public static string ReplaceEndSeparator(string text, EndLineSeparator endLineSeparator) {
            string result = text;
            switch (endLineSeparator) {
                case EndLineSeparator.N:
                    result = ReplaceAll(result, "\r\n", "\n");
                    break;
                case EndLineSeparator.RN:
                    result = ReplaceAll(result, "\r\n", "\n");
                    result = ReplaceAll(result, "\n", "\r\n");
                    break;
            }
            return result;
        }

        /// <summary>
        /// Заміна пробілів на табуляцію чи навпаки
        /// </summary>
        /// <param name="pathToFiles">посилання на файл</param>
        /// <param name="resultSymbol">на який символ необхідно замінити</param>
        public static void ReplaceSpaceOrTab(List<string> pathToFiles, SpaceOrTab resultSymbol) {
            string oldChar, newChar;
            if (resultSymbol == SpaceOrTab.Space) {
                oldChar = "\t";
                newChar = "   ";
            }
            else {
                newChar = "\t";
                oldChar = "   ";
            }
            ReplaceAll(pathToFiles, oldChar, newChar);
        }
        /// <summary>
        /// Заміна символу табуляції на пробіли чи навпаки
        /// </summary>
        /// <param name="text">початковий текст</param>
        /// <param name="spaceOrTab">на який символ необхідно замінити</param>
        /// <returns>текст із зміненими символами табуляції чи пробілів</returns>
        public static string ReplaceSpaceOrTab(string text, SpaceOrTab spaceOrTab) {
            string result = text;
            switch (spaceOrTab) {
                case SpaceOrTab.Tab:
                    result = ReplaceAll(result, "   ", "\t");
                    break;
                case SpaceOrTab.Space:
                    result = ReplaceAll(result, "\t", "   ");
                    break;
            }
            return result;
        }

        /// <summary>
        /// Заміна старого символу(ів) на новий(і) у вказаних файлах
        /// </summary>
        /// <param name="pathToFiles">посилання на файл</param>
        /// <param name="oldCombination">старий символ(и) для заміни</param>
        /// <param name="newCombination">новий символ(и) на який змінюється</param>
        public static void ReplaceAll(List<string> pathToFiles, string oldCombination, string newCombination) {
            foreach (string path in pathToFiles) {
                if (File.Exists(path)) {
                    string newName = Path.Combine(Path.GetDirectoryName(path), "~" + Path.GetFileName(path));

                    File.WriteAllText(newName, "");
                    using (StreamReader reader = new StreamReader(path)) {
                        long fileSize = new FileInfo(path).Length;//Bytes
                        int counter = 1;
                        string remainder = "";
                        while (!reader.EndOfStream) {
                            char[] buffer = (fileSize > (counter * blockSize)) ? new char[blockSize]
                                                                               : new char[((counter - 1) == 0) ? fileSize
                                                                                                                : fileSize - ((counter - 1) * blockSize)];
                            reader.ReadBlock(buffer, 0, (buffer.Length < blockSize) ? buffer.Length : blockSize);
                            string blockOfLine = new string(buffer);
                            remainder += blockOfLine;

                            bool isEndFile = (fileSize <= (counter * blockSize));
                            string result = ReplaceAll(ref remainder, oldCombination, newCombination, isEndFile);

                            var writer = new StreamWriter(newName, true);
                            writer.Write(result);
                            writer.Close();

                            counter++;
                        }
                    }
                    File.Copy(newName, path, true);
                    File.Delete(newName);
                }
            }
        }
        /// <summary>
        /// Заміна старого символу(ів) на новий(і) у вказаному тексту
        /// </summary>
        /// <param name="text">робочий текст</param>
        /// <param name="oldCombination">старий символ(и) для заміни</param>
        /// <param name="newCombination">новий символ(и) на який змінюється</param>
        /// <returns>результат заміни</returns>
        public static string ReplaceAll(string text, string oldCombination, string newCombination) {
            return ReplaceAll(ref text, oldCombination, newCombination, true);
        }
        /// <summary>
        /// Заміна старого символу(ів) на новий(і) у вказаному тексту
        /// </summary>
        /// <param name="text">робочий текст</param>
        /// <param name="oldCombination">старий символ(и) для заміни</param>
        /// <param name="newCombination">новий символ(и) на який змінюється</param>
        /// <param name="isEndFile">чи є цей текст кінцем потоку</param>
        /// <returns>результат заміни</returns>
        private static string ReplaceAll(ref string text, string oldCombination, string newCombination, bool isEndFile) {
            string result = "";
            if (oldCombination.Length == 0) return text;
            do {
                int indexStartPos = text.IndexOf(oldCombination[0]);
                if(indexStartPos >= 0) {
                    if((text.Length - 1) < (indexStartPos + oldCombination.Length - 1) && !isEndFile) {//обнаружен обрыв найденого фрагмента но это не последний блок
                        return result;
                    }
                    else if((text.Length - 1) < (indexStartPos + oldCombination.Length - 1) && isEndFile) {//обрыв в последнем блоке означает что результат не был найден
                        result += text;
                        text = "";
                        return result;
                    }

                    for(int i = indexStartPos, j = 0; i < text.Length; i++, j++) {
                        if (!text[i].Equals(oldCombination[j])) {
                            result += text.Substring(0, i + 1);
                            text = text.Remove(0, i + 1);
                            break;
                        }
                        if(j == (oldCombination.Length - 1)) {
                            result += text.Substring(0, indexStartPos) + newCombination;
                            text = text.Remove(0, i + 1);
                            break;
                        }
                    }
                } else {
                    result += text;
                    text = "";
                    break;
                }
            } while (text.Length > 0);
            return result;
        }
    }
    public enum EncodeTable {
        UTF8, ASCII, Unicode, Windows1251
    }
    public enum SpaceOrTab {
        Space, Tab
    }
    public enum EndLineSeparator {
        RN, N
    }
}
