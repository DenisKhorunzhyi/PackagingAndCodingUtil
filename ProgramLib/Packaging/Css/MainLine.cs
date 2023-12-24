namespace ProgramLib.Packaging.Css {
    public class MainLine : BaseCssNode {
        /// <summary>
        /// Текст елменту Css
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="text"></param>
        public MainLine(string text) => Text = text;
        public override string ToString() => $"{Text};\n";
    }
}
