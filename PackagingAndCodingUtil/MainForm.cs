using PackagingAndCodingUtil.Encode;
using ProgramLib;
using ProgramLib.Packaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackagingAndCodingUtil {
    public partial class MainForm : Form {
        public MainForm() => InitializeComponent();
        UpdateProgressBar progress;

        /// <summary>
        /// Обробник події завантаження вікна, встановлення таблиць кодуання для випадаючих списків
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e) {
            //указаный порядок дабавления таблиц кодирования не случайный и полностью совпадает с таблицами в перечислении EncodeTable
            selectSourceEncodeTable.Items.Add("UTF-8");
            selectSourceEncodeTable.Items.Add("ASCII");
            selectSourceEncodeTable.Items.Add("Unicode");
            selectSourceEncodeTable.Items.Add("Windows 1251");
            selectSourceEncodeTable.SelectedIndex = 0;

            selectDestinationEncodeTable.Items.Add("UTF-8");
            selectDestinationEncodeTable.Items.Add("ASCII");
            selectDestinationEncodeTable.Items.Add("Unicode");
            selectDestinationEncodeTable.Items.Add("Windows 1251");
            selectDestinationEncodeTable.SelectedIndex = 1;

            progress = new UpdateProgressBar(progressBar);
        }

        /// <summary>
        /// Виконання операцій першої вкладки над текстом
        /// </summary>
        private void doOperationOnText_Click(object sender, EventArgs e) {
            string status = "";
            string text = textIn.Text;
            if (text == null || text.Length <= 0) {
                MessageBox.Show("Введіть текст для операції!");
                return;
            }

            if (endSeparatorCB.Checked) {
                EndLineSeparator endLine = (slashR.Checked) ? EndLineSeparator.RN : EndLineSeparator.N;
                text = EncodeMaster.ReplaceEndSeparator(text, endLine);
                
                status += "* Виконано заміну кінцевого роздільника\n";
            }

            if (spaceOrTabCB.Checked) {
                SpaceOrTab spaceOrTab = (space.Checked) ? SpaceOrTab.Space : SpaceOrTab.Tab;
                text = EncodeMaster.ReplaceSpaceOrTab(text, spaceOrTab);
                status += "* Виконано заміну табуліції і пробілів\n";
            }

            if (replaceTextCB.Checked) {
                if (string.IsNullOrEmpty(textForReplace.Text.Trim())) {
                    MessageBox.Show("Введіть текст для заміни!");
                }
                text = EncodeMaster.ReplaceAll(text, textForReplace.Text, resulText.Text);
                status += "* Виконано заміну текстів\n";
            }

            if (encodeCB.Checked) {
                text = EncodeMaster.Convert((EncodeTable)(selectSourceEncodeTable.SelectedIndex),
                                                    (EncodeTable)(selectDestinationEncodeTable.SelectedIndex),
                                                    text);
                status += "* Виконано кодування\n";
            }

            if (coloringTextCB.Checked) {
                if (string.IsNullOrEmpty(textForReplace.Text)) {
                    MessageBox.Show("Введіть текст для пошуку збігів!");
                    return;
                }
                ChangeSelectionBackColorResult();
                textIn.Select(0, 0);
            }

            if (!string.IsNullOrEmpty(status.Trim()))
                MessageBox.Show(status);
            textOut.Text = text;
        }

        /// <summary>
        /// Обрані файли на першій вкладці
        /// </summary>
        string[] selectedFiles1Tab = null;
        /// <summary>
        /// Вибір файлів для першої вкладки
        /// </summary>
        private void chooseFiles_Click(object sender, EventArgs e) {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            selectedFiles.Text = "";

            if (open.ShowDialog() == DialogResult.OK) { }
            for(int i = 0; i < open.FileNames.Length; i++) {
                selectedFiles.Text += $"{i + 1}. {open.FileNames[i]}\n";
            }
            selectedFiles1Tab = open.FileNames;
        }

        /// <summary>
        /// Виконання операцій першої вкладки над файлами
        /// </summary>
        private void doOperationOnFiles_Click(object sender, EventArgs e) {
            string status = "";
            if(selectedFiles1Tab.Length <= 0) {
                MessageBox.Show("Оберіть хочаб один файл!");
                return;
            }
            List<string> files = selectedFiles1Tab.ToList();

            if (endSeparatorCB.Checked) {
                EndLineSeparator endLine = (slashR.Checked) ? EndLineSeparator.RN : EndLineSeparator.N;
                try {
                    EncodeMaster.ReplaceEndSeparator(files, endLine);
                    status += "* Виконано заміну кінцевого роздільника\n";
                } catch { status += "Проблема доступу до файлу"; return; }
            }

            if (spaceOrTabCB.Checked) {
                SpaceOrTab spaceOrTab = (space.Checked) ? SpaceOrTab.Space : SpaceOrTab.Tab;
                try {
                    EncodeMaster.ReplaceSpaceOrTab(files, spaceOrTab);
                    status += "* Виконано заміну табуліції і пробілів\n";
                } catch { status += "Проблема доступу до файлу"; return; }
            }

            if (replaceTextCB.Checked) {
                if (string.IsNullOrEmpty(textForReplace.Text)) {
                    MessageBox.Show("Введіть текст для заміни!");
                }
                try {
                    EncodeMaster.ReplaceAll(files, textForReplace.Text, resulText.Text);
                    status += "* Виконано заміну текстів\n";
                } catch { status += "Проблема доступу до файлу"; return; }
            }

            if (encodeCB.Checked) {
                try {
                    EncodeMaster.Convert((EncodeTable)(selectSourceEncodeTable.SelectedIndex),
                                         (EncodeTable)(selectDestinationEncodeTable.SelectedIndex),
                                         files);
                    status += "* Виконано кодування\n";
                } catch { status += "Проблема доступу до файлу"; return; }
            }
            if (!string.IsNullOrEmpty(status.Trim()))
                MessageBox.Show(status);
        }

        /// <summary>
        /// Виконання підсвідки тексту із "вхідного тексту" відповідно до "тексту для заміни/пошуку"
        /// </summary>
        private void ChangeSelectionBackColorResult() {
            var currentSelStart = textIn.SelectionStart;
            var currentSelLength = textIn.SelectionLength;

            string pattern = textForReplace.Text;

            textIn.SelectAll();
            
            string text = textIn.Text;
            int counter = 0;
            int index = 0;
            while (text.Length > 0) {
                index = text.IndexOf(pattern, index);
                if(index >= 0) {
                    textIn.Select(index, pattern.Length);
                    textIn.SelectionBackColor = Color.LightBlue;
                    textIn.SelectionColor = Color.White;
                    index++;
                    counter++;
                }
                else break;
            }

            textIn.Select(currentSelStart, currentSelLength);
            textIn.SelectionBackColor = SystemColors.Window;
            textIn.SelectionColor = SystemColors.WindowText;
            numberColoring.Text = $"Кількість збігів {counter}";
        }

        #region 2 Tab
        /// <summary>
        /// Перелік файлів для роботи 2 вкладки
        /// </summary>
        List<string> pathesToPackagingFiles = new List<string>();

        /// <summary>
        /// Вибір робочої папки
        /// </summary>
        private void chooseDirectory_Click(object sender, EventArgs e) {
            FolderBrowserDialog catalog = new FolderBrowserDialog();
            if(catalog.ShowDialog() == DialogResult.OK) {
                ListDirectory(filesTree, catalog.SelectedPath);
            }
        }

        /// <summary>
        /// Побудова дерева
        /// </summary>
        /// <param name="treeView">дерево</param>
        /// <param name="path">корінна директорія</param>
        private static void ListDirectory(TreeView treeView, string path) {
            var types = new List<string>() { ".js", ".css", ".html" };
            treeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0) {
                TreeNode currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories()) {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles()) {
                    if (types.Contains(file.Extension.ToLower()))
                        currentNode.Nodes.Add(new TreeNode(file.Name));
                }
            }
            treeView.Nodes.Add(node);
        }
        /// <summary>
        /// Встановлення галочок при виборі директорії чи файлу
        /// </summary>
        private void filesTree_AfterCheck(object sender, TreeViewEventArgs e) {
            foreach (TreeNode child in e.Node.Nodes) {
                child.Checked = e.Node.Checked;
            }
        }
        
        /// <summary>
        /// Збір обраних файлів у дереві
        /// </summary>
        /// <param name="tree">обраний вузол дерева</param>
        private void CollectFiles(TreeNode tree) {
            if (tree.Checked) {
                string fullPath = "";

                if (tree.Tag == null) fullPath = ((DirectoryInfo)(tree.Parent.Tag)).FullName + Path.DirectorySeparatorChar + tree.Text;
                else if (tree.Tag?.GetType() == typeof(DirectoryInfo)) fullPath = ((DirectoryInfo)(tree.Tag)).FullName;
                else fullPath = tree.FullPath;

                if (File.Exists(fullPath) && !File.GetAttributes(fullPath).HasFlag(FileAttributes.Directory))
                    pathesToPackagingFiles.Add(fullPath);
            }

            if (tree.Nodes != null) {
                foreach (TreeNode n in tree.Nodes) {
                    CollectFiles(n);
                }
            }
        }

        /// <summary>
        /// Виконання розпакування
        /// </summary>
        private async void packaging_Click(object sender, EventArgs e) {
            if (filesTree.Nodes.Count == 0) { MessageBox.Show("Оберіть файли для упакування!"); return; }
            Enabled2Page();
            await Task.Run(() => {
                if (filesTree.Nodes.Count > 0) {
                    pathesToPackagingFiles.Clear();
                    CollectFiles(filesTree.Nodes[0]);
                    new PackagingMaster(progress).Packaging(pathesToPackagingFiles);
                }

                Action action = () => {
                    Enabled2Page();
                    MessageBox.Show("Упакування виконано успішно!");
                };
                if (InvokeRequired) Invoke(action);
            });
        }
        /// <summary>
        /// Виконання упакування
        /// </summary>
        private async void unpackaging_Click(object sender, EventArgs e) {
            if (filesTree.Nodes.Count == 0) { MessageBox.Show("Оберіть файли для розпакування!"); return; }
            Enabled2Page();
            await Task.Run(() => {
                if (filesTree.Nodes.Count > 0) {
                    pathesToPackagingFiles.Clear();
                    CollectFiles(filesTree.Nodes[0]);
                    new PackagingMaster(progress).Unpackaging(pathesToPackagingFiles);
                }

                Action action = () => {
                    Enabled2Page();
                    MessageBox.Show("Розпакування виконано успішно!");
                };
                if (InvokeRequired) Invoke(action);
            });
        }
        /// <summary>
        /// Зміна стану елементів 2 вкладки
        /// </summary>
        private void Enabled2Page() {
            bool isEnable = !packaging.Enabled;
            packaging.Enabled = isEnable;
            unpackaging.Enabled = isEnable;
            chooseDirectory.Enabled = isEnable;
            filesTree.Enabled = isEnable;
        }

        #endregion

        /// <summary>
        /// Зміна виділення тексту (у випадку коли користувач виконав пошук по фрагменту тексту)
        /// </summary>
        private void textIn_KeyUp(object sender, KeyEventArgs e) {
            int indexSelect = textIn.SelectionStart;
            textIn.Select(0, textIn.Text.Length);
            textIn.SelectionBackColor = SystemColors.Window;
            textIn.SelectionColor = Color.Black;

            textIn.Select(indexSelect, 0);
        }
    }
}
