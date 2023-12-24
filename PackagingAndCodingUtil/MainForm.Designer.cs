namespace PackagingAndCodingUtil
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.formTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numberColoring = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.selectDestinationEncodeTable = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.selectSourceEncodeTable = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.slashR = new System.Windows.Forms.RadioButton();
            this.slashN = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tab = new System.Windows.Forms.RadioButton();
            this.space = new System.Windows.Forms.RadioButton();
            this.doOperationOnFiles = new System.Windows.Forms.Button();
            this.resulText = new System.Windows.Forms.RichTextBox();
            this.textForReplace = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.coloringTextCB = new System.Windows.Forms.CheckBox();
            this.replaceTextCB = new System.Windows.Forms.CheckBox();
            this.spaceOrTabCB = new System.Windows.Forms.CheckBox();
            this.endSeparatorCB = new System.Windows.Forms.CheckBox();
            this.doOperationOnText = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textOut = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textIn = new System.Windows.Forms.RichTextBox();
            this.encodeCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selectedFiles = new System.Windows.Forms.RichTextBox();
            this.chooseFiles = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.unpackaging = new System.Windows.Forms.Button();
            this.packaging = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filesTree = new System.Windows.Forms.TreeView();
            this.chooseDirectory = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.formTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // formTabs
            // 
            this.formTabs.Controls.Add(this.tabPage1);
            this.formTabs.Controls.Add(this.tabPage2);
            this.formTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formTabs.Location = new System.Drawing.Point(0, 0);
            this.formTabs.Name = "formTabs";
            this.formTabs.SelectedIndex = 0;
            this.formTabs.Size = new System.Drawing.Size(671, 576);
            this.formTabs.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.numberColoring);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.doOperationOnFiles);
            this.tabPage1.Controls.Add(this.resulText);
            this.tabPage1.Controls.Add(this.textForReplace);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.coloringTextCB);
            this.tabPage1.Controls.Add(this.replaceTextCB);
            this.tabPage1.Controls.Add(this.spaceOrTabCB);
            this.tabPage1.Controls.Add(this.endSeparatorCB);
            this.tabPage1.Controls.Add(this.doOperationOnText);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textOut);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.textIn);
            this.tabPage1.Controls.Add(this.encodeCB);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.selectedFiles);
            this.tabPage1.Controls.Add(this.chooseFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(663, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Кодування";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numberColoring
            // 
            this.numberColoring.AutoSize = true;
            this.numberColoring.Location = new System.Drawing.Point(375, 278);
            this.numberColoring.Name = "numberColoring";
            this.numberColoring.Size = new System.Drawing.Size(0, 13);
            this.numberColoring.TabIndex = 34;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.selectDestinationEncodeTable);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.selectSourceEncodeTable);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(26, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 37);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            // 
            // selectDestinationEncodeTable
            // 
            this.selectDestinationEncodeTable.FormattingEnabled = true;
            this.selectDestinationEncodeTable.Location = new System.Drawing.Point(492, 11);
            this.selectDestinationEncodeTable.Name = "selectDestinationEncodeTable";
            this.selectDestinationEncodeTable.Size = new System.Drawing.Size(126, 21);
            this.selectDestinationEncodeTable.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(311, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Результуюча таблиця кодування:";
            // 
            // selectSourceEncodeTable
            // 
            this.selectSourceEncodeTable.FormattingEnabled = true;
            this.selectSourceEncodeTable.Location = new System.Drawing.Point(176, 11);
            this.selectSourceEncodeTable.Name = "selectSourceEncodeTable";
            this.selectSourceEncodeTable.Size = new System.Drawing.Size(126, 21);
            this.selectSourceEncodeTable.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Початкова таблиця кодування:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.slashR);
            this.groupBox2.Controls.Add(this.slashN);
            this.groupBox2.Location = new System.Drawing.Point(26, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(111, 31);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            // 
            // slashR
            // 
            this.slashR.AutoSize = true;
            this.slashR.Location = new System.Drawing.Point(68, 9);
            this.slashR.Name = "slashR";
            this.slashR.Size = new System.Drawing.Size(44, 17);
            this.slashR.TabIndex = 15;
            this.slashR.Text = "\\r\\n";
            this.slashR.UseVisualStyleBackColor = true;
            // 
            // slashN
            // 
            this.slashN.AutoSize = true;
            this.slashN.Checked = true;
            this.slashN.Location = new System.Drawing.Point(4, 9);
            this.slashN.Name = "slashN";
            this.slashN.Size = new System.Drawing.Size(36, 17);
            this.slashN.TabIndex = 14;
            this.slashN.TabStop = true;
            this.slashN.Text = "\\n";
            this.slashN.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tab);
            this.groupBox1.Controls.Add(this.space);
            this.groupBox1.Location = new System.Drawing.Point(26, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 31);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // tab
            // 
            this.tab.AutoSize = true;
            this.tab.Checked = true;
            this.tab.Location = new System.Drawing.Point(69, 11);
            this.tab.Name = "tab";
            this.tab.Size = new System.Drawing.Size(73, 17);
            this.tab.TabIndex = 19;
            this.tab.TabStop = true;
            this.tab.Text = "табуляція";
            this.tab.UseVisualStyleBackColor = true;
            // 
            // space
            // 
            this.space.AutoSize = true;
            this.space.Location = new System.Drawing.Point(5, 11);
            this.space.Name = "space";
            this.space.Size = new System.Drawing.Size(57, 17);
            this.space.TabIndex = 18;
            this.space.Text = "пробіл";
            this.space.UseVisualStyleBackColor = true;
            // 
            // doOperationOnFiles
            // 
            this.doOperationOnFiles.Location = new System.Drawing.Point(535, 58);
            this.doOperationOnFiles.Name = "doOperationOnFiles";
            this.doOperationOnFiles.Size = new System.Drawing.Size(120, 40);
            this.doOperationOnFiles.TabIndex = 28;
            this.doOperationOnFiles.Text = "Виконати операцію для файлів";
            this.doOperationOnFiles.UseVisualStyleBackColor = true;
            this.doOperationOnFiles.Click += new System.EventHandler(this.doOperationOnFiles_Click);
            // 
            // resulText
            // 
            this.resulText.Location = new System.Drawing.Point(265, 315);
            this.resulText.Name = "resulText";
            this.resulText.Size = new System.Drawing.Size(218, 52);
            this.resulText.TabIndex = 27;
            this.resulText.Text = "";
            // 
            // textForReplace
            // 
            this.textForReplace.Location = new System.Drawing.Point(35, 315);
            this.textForReplace.Name = "textForReplace";
            this.textForReplace.Size = new System.Drawing.Size(218, 52);
            this.textForReplace.TabIndex = 26;
            this.textForReplace.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 297);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(195, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Фрагмент тексту для заміни/пошуку";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Результуючий";
            // 
            // coloringTextCB
            // 
            this.coloringTextCB.AutoSize = true;
            this.coloringTextCB.Location = new System.Drawing.Point(8, 277);
            this.coloringTextCB.Name = "coloringTextCB";
            this.coloringTextCB.Size = new System.Drawing.Size(361, 17);
            this.coloringTextCB.TabIndex = 23;
            this.coloringTextCB.Text = "пошук всіх збігів із можливістю заміни всіх знайдених результатів";
            this.coloringTextCB.UseVisualStyleBackColor = true;
            // 
            // replaceTextCB
            // 
            this.replaceTextCB.AutoSize = true;
            this.replaceTextCB.Location = new System.Drawing.Point(8, 254);
            this.replaceTextCB.Name = "replaceTextCB";
            this.replaceTextCB.Size = new System.Drawing.Size(202, 17);
            this.replaceTextCB.TabIndex = 20;
            this.replaceTextCB.Text = "Заміна фрагмента тексту на інший";
            this.replaceTextCB.UseVisualStyleBackColor = true;
            // 
            // spaceOrTabCB
            // 
            this.spaceOrTabCB.AutoSize = true;
            this.spaceOrTabCB.Location = new System.Drawing.Point(8, 208);
            this.spaceOrTabCB.Name = "spaceOrTabCB";
            this.spaceOrTabCB.Size = new System.Drawing.Size(218, 17);
            this.spaceOrTabCB.TabIndex = 17;
            this.spaceOrTabCB.Text = "Зміна табуляції на пробіли та навпаки";
            this.spaceOrTabCB.UseVisualStyleBackColor = true;
            // 
            // endSeparatorCB
            // 
            this.endSeparatorCB.AutoSize = true;
            this.endSeparatorCB.Location = new System.Drawing.Point(8, 105);
            this.endSeparatorCB.Name = "endSeparatorCB";
            this.endSeparatorCB.Size = new System.Drawing.Size(165, 17);
            this.endSeparatorCB.TabIndex = 16;
            this.endSeparatorCB.Text = "Змінювати тип кінця рядка ";
            this.endSeparatorCB.UseVisualStyleBackColor = true;
            // 
            // doOperationOnText
            // 
            this.doOperationOnText.Location = new System.Drawing.Point(535, 327);
            this.doOperationOnText.Name = "doOperationOnText";
            this.doOperationOnText.Size = new System.Drawing.Size(120, 40);
            this.doOperationOnText.TabIndex = 13;
            this.doOperationOnText.Text = "Виконати операцію для тексту";
            this.doOperationOnText.UseVisualStyleBackColor = true;
            this.doOperationOnText.Click += new System.EventHandler(this.doOperationOnText_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 376);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Вихідний текст:";
            // 
            // textOut
            // 
            this.textOut.Location = new System.Drawing.Point(335, 392);
            this.textOut.Name = "textOut";
            this.textOut.ReadOnly = true;
            this.textOut.Size = new System.Drawing.Size(321, 148);
            this.textOut.TabIndex = 9;
            this.textOut.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 376);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Вхідний текст:";
            // 
            // textIn
            // 
            this.textIn.Location = new System.Drawing.Point(8, 392);
            this.textIn.Name = "textIn";
            this.textIn.Size = new System.Drawing.Size(321, 148);
            this.textIn.TabIndex = 7;
            this.textIn.Text = "";
            this.textIn.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textIn_KeyUp);
            // 
            // encodeCB
            // 
            this.encodeCB.AutoSize = true;
            this.encodeCB.Location = new System.Drawing.Point(8, 151);
            this.encodeCB.Name = "encodeCB";
            this.encodeCB.Size = new System.Drawing.Size(245, 17);
            this.encodeCB.TabIndex = 6;
            this.encodeCB.Text = "Перекодувати текст із однієї таблиці в іншу";
            this.encodeCB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Обрані файли:";
            // 
            // selectedFiles
            // 
            this.selectedFiles.Location = new System.Drawing.Point(8, 22);
            this.selectedFiles.Name = "selectedFiles";
            this.selectedFiles.ReadOnly = true;
            this.selectedFiles.Size = new System.Drawing.Size(523, 76);
            this.selectedFiles.TabIndex = 3;
            this.selectedFiles.Text = "";
            // 
            // chooseFiles
            // 
            this.chooseFiles.Location = new System.Drawing.Point(535, 22);
            this.chooseFiles.Name = "chooseFiles";
            this.chooseFiles.Size = new System.Drawing.Size(120, 30);
            this.chooseFiles.TabIndex = 0;
            this.chooseFiles.Text = "Обрати файли";
            this.chooseFiles.UseVisualStyleBackColor = true;
            this.chooseFiles.Click += new System.EventHandler(this.chooseFiles_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.progressBar);
            this.tabPage2.Controls.Add(this.unpackaging);
            this.tabPage2.Controls.Add(this.packaging);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.filesTree);
            this.tabPage2.Controls.Add(this.chooseDirectory);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(663, 550);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Упакування";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // unpackaging
            // 
            this.unpackaging.Location = new System.Drawing.Point(473, 94);
            this.unpackaging.Name = "unpackaging";
            this.unpackaging.Size = new System.Drawing.Size(144, 25);
            this.unpackaging.TabIndex = 8;
            this.unpackaging.Text = "Виконати розпакування";
            this.unpackaging.UseVisualStyleBackColor = true;
            this.unpackaging.Click += new System.EventHandler(this.unpackaging_Click);
            // 
            // packaging
            // 
            this.packaging.Location = new System.Drawing.Point(473, 62);
            this.packaging.Name = "packaging";
            this.packaging.Size = new System.Drawing.Size(144, 25);
            this.packaging.TabIndex = 7;
            this.packaging.Text = "Виконати упакування";
            this.packaging.UseVisualStyleBackColor = true;
            this.packaging.Click += new System.EventHandler(this.packaging_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Каталоги:";
            // 
            // filesTree
            // 
            this.filesTree.CheckBoxes = true;
            this.filesTree.Location = new System.Drawing.Point(8, 22);
            this.filesTree.Name = "filesTree";
            this.filesTree.Size = new System.Drawing.Size(459, 491);
            this.filesTree.TabIndex = 4;
            this.filesTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.filesTree_AfterCheck);
            // 
            // chooseDirectory
            // 
            this.chooseDirectory.Location = new System.Drawing.Point(473, 22);
            this.chooseDirectory.Name = "chooseDirectory";
            this.chooseDirectory.Size = new System.Drawing.Size(144, 25);
            this.chooseDirectory.TabIndex = 3;
            this.chooseDirectory.Text = "Вибір робочого каталогу";
            this.chooseDirectory.UseVisualStyleBackColor = true;
            this.chooseDirectory.Click += new System.EventHandler(this.chooseDirectory_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 521);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(647, 23);
            this.progressBar.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 576);
            this.Controls.Add(this.formTabs);
            this.MaximumSize = new System.Drawing.Size(687, 615);
            this.MinimumSize = new System.Drawing.Size(687, 615);
            this.Name = "MainForm";
            this.Text = "Утиліта для упакування та розпакування файлів html, css й js, та перекодування те" +
    "ксту/файлів ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.formTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl formTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton slashR;
        private System.Windows.Forms.RadioButton slashN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton tab;
        private System.Windows.Forms.RadioButton space;
        private System.Windows.Forms.Button doOperationOnFiles;
        private System.Windows.Forms.RichTextBox resulText;
        private System.Windows.Forms.RichTextBox textForReplace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox coloringTextCB;
        private System.Windows.Forms.CheckBox replaceTextCB;
        private System.Windows.Forms.CheckBox spaceOrTabCB;
        private System.Windows.Forms.CheckBox endSeparatorCB;
        private System.Windows.Forms.Button doOperationOnText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox textOut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox textIn;
        private System.Windows.Forms.CheckBox encodeCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selectSourceEncodeTable;
        private System.Windows.Forms.RichTextBox selectedFiles;
        private System.Windows.Forms.Button chooseFiles;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button unpackaging;
        private System.Windows.Forms.Button packaging;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView filesTree;
        private System.Windows.Forms.Button chooseDirectory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox selectDestinationEncodeTable;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label numberColoring;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

