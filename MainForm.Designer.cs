using DarkUI.Forms;
using DarkUI.Controls;

namespace P5FlagCompare
{
    partial class MainForm : DarkForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlp_Main = new TableLayoutPanel();
            listBox_Comparisons = new ListBox();
            rtb_Output = new RichTextBox();
            groupBox_NewDisabled = new DarkGroupBox();
            listBox_NewlyDisabled = new ListBox();
            groupBox_NewEnabled = new DarkGroupBox();
            listBox_NewlyEnabled = new ListBox();
            tlp_Checkboxes = new TableLayoutPanel();
            chkBox_IDGroups = new DarkCheckBox();
            lbl_TimeStamp = new DarkLabel();
            menuStrip_Main = new DarkMenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            darkContextMenu_RightClick = new DarkContextMenu();
            renameToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            tlp_Main.SuspendLayout();
            groupBox_NewDisabled.SuspendLayout();
            groupBox_NewEnabled.SuspendLayout();
            tlp_Checkboxes.SuspendLayout();
            menuStrip_Main.SuspendLayout();
            darkContextMenu_RightClick.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 3;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlp_Main.Controls.Add(listBox_Comparisons, 0, 1);
            tlp_Main.Controls.Add(rtb_Output, 2, 1);
            tlp_Main.Controls.Add(groupBox_NewDisabled, 1, 2);
            tlp_Main.Controls.Add(groupBox_NewEnabled, 1, 1);
            tlp_Main.Controls.Add(tlp_Checkboxes, 1, 3);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(0, 0);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 4;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlp_Main.Size = new Size(800, 450);
            tlp_Main.TabIndex = 0;
            // 
            // listBox_Comparisons
            // 
            listBox_Comparisons.BackColor = Color.FromArgb(60, 63, 65);
            listBox_Comparisons.BorderStyle = BorderStyle.None;
            listBox_Comparisons.ContextMenuStrip = darkContextMenu_RightClick;
            listBox_Comparisons.Dock = DockStyle.Fill;
            listBox_Comparisons.ForeColor = SystemColors.InactiveBorder;
            listBox_Comparisons.FormattingEnabled = true;
            listBox_Comparisons.ItemHeight = 20;
            listBox_Comparisons.Location = new Point(3, 48);
            listBox_Comparisons.Name = "listBox_Comparisons";
            tlp_Main.SetRowSpan(listBox_Comparisons, 3);
            listBox_Comparisons.Size = new Size(194, 399);
            listBox_Comparisons.TabIndex = 0;
            listBox_Comparisons.SelectedIndexChanged += SelectedComparison_Changed;
            listBox_Comparisons.KeyDown += Output_Keydown;
            // 
            // rtb_Output
            // 
            rtb_Output.BackColor = Color.FromArgb(60, 63, 65);
            rtb_Output.BorderStyle = BorderStyle.None;
            rtb_Output.Dock = DockStyle.Fill;
            rtb_Output.ForeColor = SystemColors.InactiveBorder;
            rtb_Output.Location = new Point(483, 48);
            rtb_Output.Name = "rtb_Output";
            rtb_Output.ReadOnly = true;
            tlp_Main.SetRowSpan(rtb_Output, 3);
            rtb_Output.Size = new Size(314, 399);
            rtb_Output.TabIndex = 1;
            rtb_Output.Text = "";
            rtb_Output.KeyDown += Output_Keydown;
            // 
            // groupBox_NewDisabled
            // 
            groupBox_NewDisabled.BorderColor = Color.FromArgb(51, 51, 51);
            groupBox_NewDisabled.Controls.Add(listBox_NewlyDisabled);
            groupBox_NewDisabled.Dock = DockStyle.Fill;
            groupBox_NewDisabled.Location = new Point(203, 228);
            groupBox_NewDisabled.Name = "groupBox_NewDisabled";
            groupBox_NewDisabled.Size = new Size(274, 174);
            groupBox_NewDisabled.TabIndex = 3;
            groupBox_NewDisabled.TabStop = false;
            groupBox_NewDisabled.Text = "Newly Disabled Flags";
            // 
            // listBox_NewlyDisabled
            // 
            listBox_NewlyDisabled.BackColor = Color.FromArgb(70, 63, 65);
            listBox_NewlyDisabled.BorderStyle = BorderStyle.FixedSingle;
            listBox_NewlyDisabled.ContextMenuStrip = darkContextMenu_RightClick;
            listBox_NewlyDisabled.Dock = DockStyle.Fill;
            listBox_NewlyDisabled.ForeColor = Color.Silver;
            listBox_NewlyDisabled.FormattingEnabled = true;
            listBox_NewlyDisabled.ItemHeight = 20;
            listBox_NewlyDisabled.Location = new Point(3, 23);
            listBox_NewlyDisabled.Name = "listBox_NewlyDisabled";
            listBox_NewlyDisabled.Size = new Size(268, 148);
            listBox_NewlyDisabled.TabIndex = 1;
            listBox_NewlyDisabled.KeyDown += Output_Keydown;
            // 
            // groupBox_NewEnabled
            // 
            groupBox_NewEnabled.BorderColor = Color.FromArgb(51, 51, 51);
            groupBox_NewEnabled.Controls.Add(listBox_NewlyEnabled);
            groupBox_NewEnabled.Dock = DockStyle.Fill;
            groupBox_NewEnabled.Location = new Point(203, 48);
            groupBox_NewEnabled.Name = "groupBox_NewEnabled";
            groupBox_NewEnabled.Size = new Size(274, 174);
            groupBox_NewEnabled.TabIndex = 2;
            groupBox_NewEnabled.TabStop = false;
            groupBox_NewEnabled.Text = "Newly Enabled Flags";
            // 
            // listBox_NewlyEnabled
            // 
            listBox_NewlyEnabled.BackColor = Color.FromArgb(60, 73, 65);
            listBox_NewlyEnabled.BorderStyle = BorderStyle.FixedSingle;
            listBox_NewlyEnabled.ContextMenuStrip = darkContextMenu_RightClick;
            listBox_NewlyEnabled.Dock = DockStyle.Fill;
            listBox_NewlyEnabled.ForeColor = Color.Silver;
            listBox_NewlyEnabled.FormattingEnabled = true;
            listBox_NewlyEnabled.ItemHeight = 20;
            listBox_NewlyEnabled.Location = new Point(3, 23);
            listBox_NewlyEnabled.Name = "listBox_NewlyEnabled";
            listBox_NewlyEnabled.Size = new Size(268, 148);
            listBox_NewlyEnabled.TabIndex = 0;
            listBox_NewlyEnabled.KeyDown += Output_Keydown;
            // 
            // tlp_Checkboxes
            // 
            tlp_Checkboxes.ColumnCount = 2;
            tlp_Checkboxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp_Checkboxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp_Checkboxes.Controls.Add(chkBox_IDGroups, 0, 0);
            tlp_Checkboxes.Controls.Add(lbl_TimeStamp, 1, 0);
            tlp_Checkboxes.Dock = DockStyle.Fill;
            tlp_Checkboxes.Location = new Point(203, 408);
            tlp_Checkboxes.Name = "tlp_Checkboxes";
            tlp_Checkboxes.RowCount = 1;
            tlp_Checkboxes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_Checkboxes.Size = new Size(274, 39);
            tlp_Checkboxes.TabIndex = 4;
            // 
            // chkBox_IDGroups
            // 
            chkBox_IDGroups.AutoSize = true;
            chkBox_IDGroups.Checked = true;
            chkBox_IDGroups.CheckState = CheckState.Checked;
            chkBox_IDGroups.Location = new Point(3, 3);
            chkBox_IDGroups.Name = "chkBox_IDGroups";
            chkBox_IDGroups.Size = new Size(89, 24);
            chkBox_IDGroups.TabIndex = 4;
            chkBox_IDGroups.Text = "ID Groups";
            // 
            // lbl_TimeStamp
            // 
            lbl_TimeStamp.AutoSize = true;
            lbl_TimeStamp.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_TimeStamp.Location = new Point(98, 0);
            lbl_TimeStamp.Name = "lbl_TimeStamp";
            lbl_TimeStamp.Size = new Size(0, 20);
            lbl_TimeStamp.TabIndex = 5;
            // 
            // menuStrip_Main
            // 
            menuStrip_Main.BackColor = Color.FromArgb(60, 63, 65);
            menuStrip_Main.ForeColor = Color.FromArgb(220, 220, 220);
            menuStrip_Main.ImageScalingSize = new Size(20, 20);
            menuStrip_Main.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip_Main.Location = new Point(0, 0);
            menuStrip_Main.Name = "menuStrip_Main";
            menuStrip_Main.Padding = new Padding(3, 2, 0, 2);
            menuStrip_Main.Size = new Size(800, 28);
            menuStrip_Main.TabIndex = 2;
            menuStrip_Main.KeyDown += Output_Keydown;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(125, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += Save_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(125, 26);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += Load_Click;
            // 
            // darkContextMenu_RightClick
            // 
            darkContextMenu_RightClick.BackColor = Color.FromArgb(60, 63, 65);
            darkContextMenu_RightClick.ForeColor = Color.FromArgb(220, 220, 220);
            darkContextMenu_RightClick.ImageScalingSize = new Size(20, 20);
            darkContextMenu_RightClick.Items.AddRange(new ToolStripItem[] { renameToolStripMenuItem, deleteToolStripMenuItem });
            darkContextMenu_RightClick.Name = "darkContextMenu_RightClick";
            darkContextMenu_RightClick.Size = new Size(211, 80);
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            renameToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(210, 24);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += RenameToolStrip_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            deleteToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(210, 24);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += DeleteToolStrip_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip_Main);
            Controls.Add(tlp_Main);
            Name = "MainForm";
            Text = "P5 Flag Comparer";
            KeyDown += Output_Keydown;
            tlp_Main.ResumeLayout(false);
            groupBox_NewDisabled.ResumeLayout(false);
            groupBox_NewEnabled.ResumeLayout(false);
            tlp_Checkboxes.ResumeLayout(false);
            tlp_Checkboxes.PerformLayout();
            menuStrip_Main.ResumeLayout(false);
            menuStrip_Main.PerformLayout();
            darkContextMenu_RightClick.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlp_Main;
        private ListBox listBox_Comparisons;
        private RichTextBox rtb_Output;
        private DarkGroupBox groupBox_NewDisabled;
        private DarkGroupBox groupBox_NewEnabled;
        private DarkMenuStrip menuStrip_Main;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ListBox listBox_NewlyEnabled;
        private ListBox listBox_NewlyDisabled;
        private TableLayoutPanel tlp_Checkboxes;
        private DarkCheckBox chkBox_IDGroups;
        private DarkLabel lbl_TimeStamp;
        private DarkContextMenu darkContextMenu_RightClick;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
    }
}