using System.IO;

namespace ProgramLib.Packaging {
    public abstract class BaseTypeFile {
        /// <summary>
        /// Максимальна довжина блоку для потокового считування файлу
        /// </summary>
        public const int blockSize = 1024 * 1024 * 6;//6 MB

        /// <summary>
        /// Запис тексту до файлу
        /// </summary>
        /// <param name="text">текст</param>
        /// <param name="path">посилання на файл</param>
        public virtual void WriteToFile(string text, string path) {
            var writer = new StreamWriter(path, true);
            writer.Write(text);
            writer.Close();
        }

        /// <summary>
        /// Видалення із фрагмунту тексту символів переходу на нову строку та табуляції
        /// </summary>
        /// <param name="text">фрагмент тексту</param>
        /// <returns>очищена строка</returns>
        public string SplitText(string text) {
            text = text.Replace("\r", "");
            text = text.Replace("\n", "");
            text = text.Replace("\t", "");

            string outText = "";
            var l = text.Split(' ');
            foreach (var a in l)
                outText += (a.Trim().Length > 0) ? a.Trim() + " " : "";
            return outText.Trim();
        }

        /// <summary>
        /// абстрактний метод реалізації розпакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public abstract void Unpackaging(string pathSource, string pathDestination);
        /// <summary>
        /// абстрактний метод реалізації упакування
        /// </summary>
        /// <param name="pathSource">посилання на файл джерела</param>
        /// <param name="pathDestination">посилання на файл результату</param>
        public abstract void Packaging(string pathSource, string pathDestination);

        /// <summary>
        /// Виконання упакування і заміщення початкового файлу
        /// </summary>
        /// <param name="path">посиланння на файл</param>
        public void Packaging(string path) {
            string newName = Path.Combine(Path.GetDirectoryName(path), "~" + Path.GetFileName(path));
            Packaging(path, newName);
            File.Copy(newName, path, true);
            File.Delete(newName);
        }

        /// <summary>
        /// Виконання розпакування і заміщення початкового файлу
        /// </summary>
        /// <param name="path">посиланння на файл</param>
        public void Unpackaging(string path) {
            string newName = Path.Combine(Path.GetDirectoryName(path), "~" + Path.GetFileName(path));
            Unpackaging(path, newName);
            File.Copy(newName, path, true);
            File.Delete(newName);
        }
    }
}
