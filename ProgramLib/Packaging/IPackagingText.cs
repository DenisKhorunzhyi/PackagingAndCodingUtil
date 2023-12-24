namespace ProgramLib.Packaging {
    public interface IPackagingText {
        /// <summary>
        /// Метод упакування тексту
        /// </summary>
        /// <param name="data">дані для упакування</param>
        /// <returns>вихідний упакований текст</returns>
        string PackagingText(string data);
        /// <summary>
        /// Метод розпакування тексту
        /// </summary>
        /// <param name="data">дані для розпакування</param>
        /// <param name="deepLevel">рівень поглиблення, необхідний для виявлення кількості символів табуляцій, перед строчкою</param>
        /// <returns>вихідний розпакований текст</returns>
        string UnpackagingText(string data, int deepLevel);
    }
}
