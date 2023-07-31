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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tlp_Main = new TableLayoutPanel();
            listView_Comparisons = new DarkListView();
            darkContextMenu_RightClick = new DarkContextMenu();
            renameToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            tlp_Checkboxes = new TableLayoutPanel();
            chkBox_Sections = new DarkCheckBox();
            lbl_TimeStamp = new DarkLabel();
            metroSetTabControl_FlagType = new MetroSet_UI.Controls.MetroSetTabControl();
            tabPage_BitFlags = new TabPage();
            tlp_BitFlags = new TableLayoutPanel();
            groupBox_NewDisabled = new DarkGroupBox();
            listView_DisabledFlags = new DarkListView();
            groupBox_NewEnabled = new DarkGroupBox();
            listView_EnabledFlags = new DarkListView();
            tabPage_Count = new TabPage();
            tlp_Count = new TableLayoutPanel();
            groupBox_UnsetCount = new DarkGroupBox();
            listView_UnsetCounts = new DarkListView();
            groupBox_NewCount = new DarkGroupBox();
            listView_SetCounts = new DarkListView();
            columnHeader = new ColumnHeader();
            menuStrip_Main = new DarkMenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            pasteFlagsToolStripMenuItem = new ToolStripMenuItem();
            tlp_Main.SuspendLayout();
            darkContextMenu_RightClick.SuspendLayout();
            tlp_Checkboxes.SuspendLayout();
            metroSetTabControl_FlagType.SuspendLayout();
            tabPage_BitFlags.SuspendLayout();
            tlp_BitFlags.SuspendLayout();
            groupBox_NewDisabled.SuspendLayout();
            groupBox_NewEnabled.SuspendLayout();
            tabPage_Count.SuspendLayout();
            tlp_Count.SuspendLayout();
            groupBox_UnsetCount.SuspendLayout();
            groupBox_NewCount.SuspendLayout();
            menuStrip_Main.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 2;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tlp_Main.Controls.Add(listView_Comparisons, 0, 1);
            tlp_Main.Controls.Add(tlp_Checkboxes, 1, 3);
            tlp_Main.Controls.Add(metroSetTabControl_FlagType, 1, 1);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(0, 0);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 4;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tlp_Main.Size = new Size(682, 453);
            tlp_Main.TabIndex = 0;
            // 
            // listView_Comparisons
            // 
            listView_Comparisons.BackColor = Color.FromArgb(60, 63, 65);
            listView_Comparisons.ContextMenuStrip = darkContextMenu_RightClick;
            listView_Comparisons.Dock = DockStyle.Fill;
            listView_Comparisons.ForeColor = SystemColors.InactiveBorder;
            listView_Comparisons.Location = new Point(3, 48);
            listView_Comparisons.Name = "listView_Comparisons";
            tlp_Main.SetRowSpan(listView_Comparisons, 3);
            listView_Comparisons.Size = new Size(164, 402);
            listView_Comparisons.TabIndex = 0;
            listView_Comparisons.SelectedIndicesChanged += SelectedComparison_Changed;
            listView_Comparisons.KeyDown += Output_Keydown;
            // 
            // darkContextMenu_RightClick
            // 
            darkContextMenu_RightClick.BackColor = Color.FromArgb(60, 63, 65);
            darkContextMenu_RightClick.ForeColor = Color.FromArgb(220, 220, 220);
            darkContextMenu_RightClick.ImageScalingSize = new Size(20, 20);
            darkContextMenu_RightClick.Items.AddRange(new ToolStripItem[] { renameToolStripMenuItem, deleteToolStripMenuItem });
            darkContextMenu_RightClick.Name = "darkContextMenu_RightClick";
            darkContextMenu_RightClick.Size = new Size(133, 52);
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            renameToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(132, 24);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += RenameToolStrip_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            deleteToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(132, 24);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += DeleteToolStrip_Click;
            // 
            // tlp_Checkboxes
            // 
            tlp_Checkboxes.ColumnCount = 2;
            tlp_Checkboxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlp_Checkboxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp_Checkboxes.Controls.Add(chkBox_Sections, 0, 0);
            tlp_Checkboxes.Controls.Add(lbl_TimeStamp, 1, 0);
            tlp_Checkboxes.Dock = DockStyle.Fill;
            tlp_Checkboxes.Location = new Point(173, 410);
            tlp_Checkboxes.Name = "tlp_Checkboxes";
            tlp_Checkboxes.RowCount = 1;
            tlp_Checkboxes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp_Checkboxes.Size = new Size(506, 40);
            tlp_Checkboxes.TabIndex = 4;
            // 
            // chkBox_Sections
            // 
            chkBox_Sections.AutoSize = true;
            chkBox_Sections.Checked = true;
            chkBox_Sections.CheckState = CheckState.Checked;
            chkBox_Sections.Dock = DockStyle.Left;
            chkBox_Sections.Location = new Point(3, 3);
            chkBox_Sections.Name = "chkBox_Sections";
            chkBox_Sections.Size = new Size(86, 34);
            chkBox_Sections.TabIndex = 4;
            chkBox_Sections.Text = "Sections";
            chkBox_Sections.CheckedChanged += Sections_CheckedChanged;
            // 
            // lbl_TimeStamp
            // 
            lbl_TimeStamp.Anchor = AnchorStyles.Left;
            lbl_TimeStamp.AutoSize = true;
            lbl_TimeStamp.ForeColor = Color.FromArgb(220, 220, 220);
            lbl_TimeStamp.Location = new Point(180, 10);
            lbl_TimeStamp.Name = "lbl_TimeStamp";
            lbl_TimeStamp.Size = new Size(0, 20);
            lbl_TimeStamp.TabIndex = 5;
            lbl_TimeStamp.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // metroSetTabControl_FlagType
            // 
            metroSetTabControl_FlagType.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            metroSetTabControl_FlagType.AnimateTime = 200;
            metroSetTabControl_FlagType.BackgroundColor = Color.FromArgb(60, 63, 65);
            metroSetTabControl_FlagType.Controls.Add(tabPage_BitFlags);
            metroSetTabControl_FlagType.Controls.Add(tabPage_Count);
            metroSetTabControl_FlagType.Dock = DockStyle.Fill;
            metroSetTabControl_FlagType.IsDerivedStyle = false;
            metroSetTabControl_FlagType.ItemSize = new Size(100, 38);
            metroSetTabControl_FlagType.Location = new Point(170, 45);
            metroSetTabControl_FlagType.Margin = new Padding(0);
            metroSetTabControl_FlagType.Name = "metroSetTabControl_FlagType";
            tlp_Main.SetRowSpan(metroSetTabControl_FlagType, 2);
            metroSetTabControl_FlagType.SelectedIndex = 0;
            metroSetTabControl_FlagType.SelectedTextColor = Color.White;
            metroSetTabControl_FlagType.Size = new Size(512, 362);
            metroSetTabControl_FlagType.SizeMode = TabSizeMode.Fixed;
            metroSetTabControl_FlagType.Speed = 100;
            metroSetTabControl_FlagType.Style = MetroSet_UI.Enums.Style.Dark;
            metroSetTabControl_FlagType.StyleManager = null;
            metroSetTabControl_FlagType.TabIndex = 6;
            metroSetTabControl_FlagType.TabStyle = MetroSet_UI.Enums.TabStyle.Style2;
            metroSetTabControl_FlagType.ThemeAuthor = "Narwin";
            metroSetTabControl_FlagType.ThemeName = "MetroDark";
            metroSetTabControl_FlagType.UnselectedTextColor = Color.Gray;
            metroSetTabControl_FlagType.UseAnimation = false;
            // 
            // tabPage_BitFlags
            // 
            tabPage_BitFlags.BackColor = Color.FromArgb(60, 63, 65);
            tabPage_BitFlags.Controls.Add(tlp_BitFlags);
            tabPage_BitFlags.ForeColor = Color.Silver;
            tabPage_BitFlags.Location = new Point(4, 42);
            tabPage_BitFlags.Name = "tabPage_BitFlags";
            tabPage_BitFlags.Size = new Size(504, 316);
            tabPage_BitFlags.TabIndex = 0;
            tabPage_BitFlags.Text = "Bit Flags";
            // 
            // tlp_BitFlags
            // 
            tlp_BitFlags.ColumnCount = 1;
            tlp_BitFlags.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_BitFlags.Controls.Add(groupBox_NewDisabled, 0, 1);
            tlp_BitFlags.Controls.Add(groupBox_NewEnabled, 0, 0);
            tlp_BitFlags.Dock = DockStyle.Fill;
            tlp_BitFlags.Location = new Point(0, 0);
            tlp_BitFlags.Name = "tlp_BitFlags";
            tlp_BitFlags.RowCount = 2;
            tlp_BitFlags.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_BitFlags.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_BitFlags.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_BitFlags.Size = new Size(504, 316);
            tlp_BitFlags.TabIndex = 0;
            // 
            // groupBox_NewDisabled
            // 
            groupBox_NewDisabled.BorderColor = Color.FromArgb(51, 51, 51);
            groupBox_NewDisabled.Controls.Add(listView_DisabledFlags);
            groupBox_NewDisabled.Dock = DockStyle.Fill;
            groupBox_NewDisabled.Location = new Point(0, 158);
            groupBox_NewDisabled.Margin = new Padding(0);
            groupBox_NewDisabled.Name = "groupBox_NewDisabled";
            groupBox_NewDisabled.Padding = new Padding(0);
            groupBox_NewDisabled.Size = new Size(504, 158);
            groupBox_NewDisabled.TabIndex = 5;
            groupBox_NewDisabled.TabStop = false;
            groupBox_NewDisabled.Text = "Newly Disabled Flags";
            // 
            // listView_DisabledFlags
            // 
            listView_DisabledFlags.BackColor = Color.FromArgb(70, 63, 65);
            listView_DisabledFlags.ContextMenuStrip = darkContextMenu_RightClick;
            listView_DisabledFlags.Dock = DockStyle.Fill;
            listView_DisabledFlags.ForeColor = Color.Silver;
            listView_DisabledFlags.Location = new Point(0, 20);
            listView_DisabledFlags.MultiSelect = true;
            listView_DisabledFlags.Name = "listView_DisabledFlags";
            listView_DisabledFlags.Size = new Size(504, 138);
            listView_DisabledFlags.TabIndex = 1;
            listView_DisabledFlags.KeyDown += Output_Keydown;
            // 
            // groupBox_NewEnabled
            // 
            groupBox_NewEnabled.BackColor = Color.FromArgb(51, 51, 51);
            groupBox_NewEnabled.BorderColor = Color.FromArgb(51, 51, 51);
            groupBox_NewEnabled.Controls.Add(listView_EnabledFlags);
            groupBox_NewEnabled.Dock = DockStyle.Fill;
            groupBox_NewEnabled.Location = new Point(0, 0);
            groupBox_NewEnabled.Margin = new Padding(0);
            groupBox_NewEnabled.Name = "groupBox_NewEnabled";
            groupBox_NewEnabled.Padding = new Padding(0);
            groupBox_NewEnabled.Size = new Size(504, 158);
            groupBox_NewEnabled.TabIndex = 4;
            groupBox_NewEnabled.TabStop = false;
            groupBox_NewEnabled.Text = "Newly Enabled Flags";
            // 
            // listView_EnabledFlags
            // 
            listView_EnabledFlags.BackColor = Color.FromArgb(60, 73, 65);
            listView_EnabledFlags.ContextMenuStrip = darkContextMenu_RightClick;
            listView_EnabledFlags.Dock = DockStyle.Fill;
            listView_EnabledFlags.ForeColor = Color.Silver;
            listView_EnabledFlags.Location = new Point(0, 20);
            listView_EnabledFlags.MultiSelect = true;
            listView_EnabledFlags.Name = "listView_EnabledFlags";
            listView_EnabledFlags.Size = new Size(504, 138);
            listView_EnabledFlags.TabIndex = 0;
            listView_EnabledFlags.KeyDown += Output_Keydown;
            // 
            // tabPage_Count
            // 
            tabPage_Count.BackColor = Color.FromArgb(60, 63, 65);
            tabPage_Count.Controls.Add(tlp_Count);
            tabPage_Count.ForeColor = Color.Silver;
            tabPage_Count.Location = new Point(4, 42);
            tabPage_Count.Margin = new Padding(0);
            tabPage_Count.Name = "tabPage_Count";
            tabPage_Count.Size = new Size(504, 316);
            tabPage_Count.TabIndex = 1;
            tabPage_Count.Text = "Count";
            // 
            // tlp_Count
            // 
            tlp_Count.BackColor = Color.FromArgb(60, 63, 65);
            tlp_Count.ColumnCount = 1;
            tlp_Count.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Count.Controls.Add(groupBox_UnsetCount, 0, 1);
            tlp_Count.Controls.Add(groupBox_NewCount, 0, 0);
            tlp_Count.Dock = DockStyle.Fill;
            tlp_Count.Location = new Point(0, 0);
            tlp_Count.Margin = new Padding(0);
            tlp_Count.Name = "tlp_Count";
            tlp_Count.RowCount = 2;
            tlp_Count.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Count.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlp_Count.Size = new Size(504, 316);
            tlp_Count.TabIndex = 0;
            // 
            // groupBox_UnsetCount
            // 
            groupBox_UnsetCount.BorderColor = Color.FromArgb(51, 51, 51);
            groupBox_UnsetCount.Controls.Add(listView_UnsetCounts);
            groupBox_UnsetCount.Dock = DockStyle.Fill;
            groupBox_UnsetCount.Location = new Point(0, 158);
            groupBox_UnsetCount.Margin = new Padding(0);
            groupBox_UnsetCount.Name = "groupBox_UnsetCount";
            groupBox_UnsetCount.Padding = new Padding(0);
            groupBox_UnsetCount.Size = new Size(504, 158);
            groupBox_UnsetCount.TabIndex = 6;
            groupBox_UnsetCount.TabStop = false;
            groupBox_UnsetCount.Text = "Newly Unset Count";
            // 
            // listView_UnsetCounts
            // 
            listView_UnsetCounts.BackColor = Color.FromArgb(70, 63, 65);
            listView_UnsetCounts.ContextMenuStrip = darkContextMenu_RightClick;
            listView_UnsetCounts.Dock = DockStyle.Fill;
            listView_UnsetCounts.ForeColor = Color.Silver;
            listView_UnsetCounts.Location = new Point(0, 20);
            listView_UnsetCounts.MultiSelect = true;
            listView_UnsetCounts.Name = "listView_UnsetCounts";
            listView_UnsetCounts.Size = new Size(504, 138);
            listView_UnsetCounts.TabIndex = 1;
            listView_UnsetCounts.KeyDown += Output_Keydown;
            // 
            // groupBox_NewCount
            // 
            groupBox_NewCount.BackColor = Color.FromArgb(51, 51, 51);
            groupBox_NewCount.BorderColor = Color.FromArgb(51, 51, 51);
            groupBox_NewCount.Controls.Add(listView_SetCounts);
            groupBox_NewCount.Dock = DockStyle.Fill;
            groupBox_NewCount.Location = new Point(0, 0);
            groupBox_NewCount.Margin = new Padding(0);
            groupBox_NewCount.Name = "groupBox_NewCount";
            groupBox_NewCount.Padding = new Padding(0);
            groupBox_NewCount.Size = new Size(504, 158);
            groupBox_NewCount.TabIndex = 5;
            groupBox_NewCount.TabStop = false;
            groupBox_NewCount.Text = "Newly Changed Count";
            // 
            // listView_SetCounts
            // 
            listView_SetCounts.BackColor = Color.FromArgb(60, 73, 65);
            listView_SetCounts.ContextMenuStrip = darkContextMenu_RightClick;
            listView_SetCounts.Dock = DockStyle.Fill;
            listView_SetCounts.ForeColor = Color.Silver;
            listView_SetCounts.Location = new Point(0, 20);
            listView_SetCounts.MultiSelect = true;
            listView_SetCounts.Name = "listView_SetCounts";
            listView_SetCounts.Size = new Size(504, 138);
            listView_SetCounts.TabIndex = 0;
            listView_SetCounts.KeyDown += Output_Keydown;
            // 
            // columnHeader
            // 
            columnHeader.Width = 100;
            // 
            // menuStrip_Main
            // 
            menuStrip_Main.BackColor = Color.FromArgb(60, 63, 65);
            menuStrip_Main.ForeColor = Color.FromArgb(220, 220, 220);
            menuStrip_Main.ImageScalingSize = new Size(20, 20);
            menuStrip_Main.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, pasteFlagsToolStripMenuItem });
            menuStrip_Main.Location = new Point(0, 0);
            menuStrip_Main.Name = "menuStrip_Main";
            menuStrip_Main.Padding = new Padding(3, 2, 0, 2);
            menuStrip_Main.Size = new Size(682, 28);
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
            // pasteFlagsToolStripMenuItem
            // 
            pasteFlagsToolStripMenuItem.BackColor = Color.FromArgb(60, 63, 65);
            pasteFlagsToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 220);
            pasteFlagsToolStripMenuItem.Name = "pasteFlagsToolStripMenuItem";
            pasteFlagsToolStripMenuItem.Size = new Size(95, 24);
            pasteFlagsToolStripMenuItem.Text = "Paste Flags";
            pasteFlagsToolStripMenuItem.Click += PasteFlags_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 453);
            Controls.Add(menuStrip_Main);
            Controls.Add(tlp_Main);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "P5 Flag Comparer";
            KeyDown += Output_Keydown;
            tlp_Main.ResumeLayout(false);
            darkContextMenu_RightClick.ResumeLayout(false);
            tlp_Checkboxes.ResumeLayout(false);
            tlp_Checkboxes.PerformLayout();
            metroSetTabControl_FlagType.ResumeLayout(false);
            tabPage_BitFlags.ResumeLayout(false);
            tlp_BitFlags.ResumeLayout(false);
            groupBox_NewDisabled.ResumeLayout(false);
            groupBox_NewEnabled.ResumeLayout(false);
            tabPage_Count.ResumeLayout(false);
            tlp_Count.ResumeLayout(false);
            groupBox_UnsetCount.ResumeLayout(false);
            groupBox_NewCount.ResumeLayout(false);
            menuStrip_Main.ResumeLayout(false);
            menuStrip_Main.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tlp_Main;
        private DarkListView listView_Comparisons;
        private DarkMenuStrip menuStrip_Main;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private TableLayoutPanel tlp_Checkboxes;
        private DarkCheckBox chkBox_Sections;
        private DarkLabel lbl_TimeStamp;
        private DarkContextMenu darkContextMenu_RightClick;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem pasteFlagsToolStripMenuItem;
        private MetroSet_UI.Controls.MetroSetTabControl metroSetTabControl_FlagType;
        private TabPage tabPage_BitFlags;
        private TableLayoutPanel tlp_BitFlags;
        private DarkGroupBox groupBox_NewDisabled;
        private DarkListView listView_DisabledFlags;
        private DarkGroupBox groupBox_NewEnabled;
        private DarkListView listView_EnabledFlags;
        private TabPage tabPage_Count;
        private TableLayoutPanel tlp_Count;
        private DarkGroupBox groupBox_NewCount;
        private DarkListView listView_SetCounts;
        private DarkGroupBox groupBox_UnsetCount;
        private DarkListView listView_UnsetCounts;
        private ColumnHeader columnHeader;
    }
}