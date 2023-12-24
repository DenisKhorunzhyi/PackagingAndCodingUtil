namespace ProgramLib.Packaging.Css {
    public class Comment : BaseCssNode {
        /// <summary>
        /// Текст коментару
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="text">текст коментару</param>
        public Comment(string text) => Text = text;
        public override string ToString() => $"/*{Text}*/\n";
    }
}
