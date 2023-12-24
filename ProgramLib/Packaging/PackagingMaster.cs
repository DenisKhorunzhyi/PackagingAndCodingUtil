using System;
using System.Collections.Generic;
using System.IO;

namespace ProgramLib.Packaging {
    public class PackagingMaster {
        UpdateProgressBar progress;
        public PackagingMaster(UpdateProgressBar progress) => this.progress = progress;

        /// <summary>
        /// Виконання розпакування списку файлів
        /// </summary>
        /// <param name="pathToFiles">список файлів для розпакування</param>
        public void Unpackaging(List<string> pathToFiles) {
            progress.Initialize(pathToFiles.Count);
            foreach(string path in pathToFiles) {
                try {
                    var obj = GetPackagingObject(GetTypeFile(path));
                    obj.Unpackaging(path);
                    progress.Increment();
                } catch { }
            }
        }
        /// <summary>
        /// Виконання упакування списку файлів
        /// </summary>
        /// <param name="pathToFiles">список файлів для упакування</param>
        public void Packaging(List<string> pathToFiles) {
            progress.Initialize(pathToFiles.Count);
            foreach (string path in pathToFiles) {
                try {
                    var obj = GetPackagingObject(GetTypeFile(path));
                    obj.Packaging(path);
                    progress.Increment();
                }
                catch { }
            }
        }

        /// <summary>
        /// В залежності від типу файлу створюється об'єкт 
        /// </summary>
        /// <param name="type">тип файлу</param>
        /// <returns>об'єкт для виконання операції із упакування/розпакування</returns>
        public BaseTypeFile GetPackagingObject(string type) {
            BaseTypeFile result = null;
            switch (type.ToLower()) {
                case "js":
                    result = new Js();
                    break;
                case "css":
                    result = new Css.Css();
                    break;
                case "html":
                    result = new Html.Html();
                    break;
                default :
                    throw new FormatException();
            }
            return result;
        }
        /// <summary>
        /// Виявлення типу файлу
        /// </summary>
        /// <param name="pathToFile">посилання на файл</param>
        /// <returns>тип файлу</returns>
        private string GetTypeFile(string pathToFile) {
            return new FileInfo(pathToFile).Extension.Substring(1);
        }
    }
}
