using DouglasCrockford.JsMin;
using System.IO;
using System.Text;

namespace ProgramLib.Packaging {
    public class Js : BaseTypeFile, IPackagingText {
        /// <summary>
        /// Реалізація методу упакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public override void Packaging(string pathSource, string pathDestination) {
            string js = File.ReadAllText(pathSource);
            File.WriteAllText(pathDestination, new JsMinifier().Minify(js));
        }
        /// <summary>
        /// Виконання упакування тексту
        /// </summary>
        /// <param name="data"></param>
        /// <returns>результат упакування</returns>
        public string PackagingText(string data) {
            return new JsMinifier().Minify(data);
        }

        /// <summary>
        /// Реалізація методу розпакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public override void Unpackaging(string pathSource, string pathDestination) {
            string js = File.ReadAllText(pathSource);
            Jsbeautifier.Beautifier b = new Jsbeautifier.Beautifier(new Jsbeautifier.BeautifierOptions() { });
            string result = b.Beautify(js);
            File.WriteAllText(pathDestination, result);
        }
        /// <summary>
        /// Виконання розпакування тексту
        /// </summary>
        /// <param name="data">текст для роботи</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        /// <returns>результат розпакування</returns>
        public string UnpackagingText(string data, int deepLevel) {
            string result = "";
            if (string.IsNullOrEmpty(data.Trim())) {
                return "";
            }

            Jsbeautifier.Beautifier b = new Jsbeautifier.Beautifier(new Jsbeautifier.BeautifierOptions() { });
            string unpackaged = b.Beautify(data);
            var array = unpackaged.Split('\n');
            string afterText = new StringBuilder().Append('\t', deepLevel).ToString();
            for (int i = 0; i < array.Length; i++) {
                if (!string.IsNullOrEmpty(array[i].Trim()))
                    result += afterText + array[i] + "\n";
            }
            return result;
        }
    }
}
