using System.Collections.Generic;

namespace ProgramLib.Packaging.Css {
    public class CssRule : BaseCssNode {
        /// <summary>
        /// Селектор стилю
        /// </summary>
        public string Selectors { get; }
        /// <summary>
        /// Блоки стилів
        /// </summary>
        public List<DeclarationBlock> DeclarationBlocks { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="selectors">селектори</param>
        public CssRule(string selectors) {
            DeclarationBlocks = new List<DeclarationBlock>();
            Selectors = selectors;
        }
        /// <summary>
        /// Формування текстового представлення блоків стилю із вирівненням
        /// </summary>
        /// <returns>блоки стилів які було вирівняно</returns>
        private string BlockToString() {
            string result = "";
            foreach (var dataLine in DeclarationBlocks)
                result += dataLine.ToString();
            return result;
        }
        
        public override string ToString() => $"{Selectors} {{\n{BlockToString()}}}\n";

        /// <summary>
        /// Блок стилю
        /// </summary>
        public class DeclarationBlock {
            /// <summary>
            /// Назва стилю
            /// </summary>
            public string Property { get; }
            /// <summary>
            /// Значення стилю
            /// </summary>
            public string Value { get; }
            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="property">назва стилю</param>
            /// <param name="value">значення стилю</param>
            public DeclarationBlock(string property, string value) {
                Property = property;
                Value = value;
            }
            public override string ToString() => $"  {Property} : {Value};\n";
        }
    }
}
