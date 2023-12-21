using MetroSet_UI.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace P5RFlagComparer
{
    partial class MainForm : MetroSetForm
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
            this.tlp_Main = new System.Windows.Forms.TableLayoutPanel();
            this.metroSetTabControl_FlagType = new MetroSet_UI.Controls.MetroSetTabControl();
            this.tabPage_BitFlags = new System.Windows.Forms.TabPage();
            this.tlp_BitFlags = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_NewDisabled = new System.Windows.Forms.GroupBox();
            this.listBox_DisabledFlags = new System.Windows.Forms.ListBox();
            this.groupBox_NewEnabled = new System.Windows.Forms.GroupBox();
            this.listBox_EnabledFlags = new System.Windows.Forms.ListBox();
            this.tabPage_Count = new System.Windows.Forms.TabPage();
            this.tlp_Count = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_UnsetCount = new System.Windows.Forms.GroupBox();
            this.listBox_UnsetCounts = new System.Windows.Forms.ListBox();
            this.groupBox_NewCount = new System.Windows.Forms.GroupBox();
            this.listBox_SetCounts = new System.Windows.Forms.ListBox();
            this.tlp_Checkboxes = new System.Windows.Forms.TableLayoutPanel();
            this.chk_AutoRename = new System.Windows.Forms.CheckBox();
            this.chkBox_Sections = new System.Windows.Forms.CheckBox();
            this.lbl_TimeStamp = new System.Windows.Forms.Label();
            this.ContextMenu_RightClick = new System.Windows.Forms.ContextMenu();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteFlagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox_Comparisons = new System.Windows.Forms.ListBox();
            this.tlp_Main.SuspendLayout();
            this.metroSetTabControl_FlagType.SuspendLayout();
            this.tabPage_BitFlags.SuspendLayout();
            this.tlp_BitFlags.SuspendLayout();
            this.groupBox_NewDisabled.SuspendLayout();
            this.groupBox_NewEnabled.SuspendLayout();
            this.tabPage_Count.SuspendLayout();
            this.tlp_Count.SuspendLayout();
            this.groupBox_UnsetCount.SuspendLayout();
            this.groupBox_NewCount.SuspendLayout();
            this.tlp_Checkboxes.SuspendLayout();
            this.menuStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Main
            // 
            this.tlp_Main.ColumnCount = 1;
            this.tlp_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Main.Controls.Add(this.metroSetTabControl_FlagType, 0, 0);
            this.tlp_Main.Controls.Add(this.tlp_Checkboxes, 0, 1);
            this.tlp_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Main.Location = new System.Drawing.Point(0, 0);
            this.tlp_Main.Name = "tlp_Main";
            this.tlp_Main.RowCount = 2;
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlp_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_Main.Size = new System.Drawing.Size(460, 470);
            this.tlp_Main.TabIndex = 0;
            // 
            // metroSetTabControl_FlagType
            // 
            this.metroSetTabControl_FlagType.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            this.metroSetTabControl_FlagType.AnimateTime = 200;
            this.metroSetTabControl_FlagType.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.metroSetTabControl_FlagType.Controls.Add(this.tabPage_BitFlags);
            this.metroSetTabControl_FlagType.Controls.Add(this.tabPage_Count);
            this.metroSetTabControl_FlagType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroSetTabControl_FlagType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroSetTabControl_FlagType.IsDerivedStyle = false;
            this.metroSetTabControl_FlagType.ItemSize = new System.Drawing.Size(100, 38);
            this.metroSetTabControl_FlagType.Location = new System.Drawing.Point(3, 3);
            this.metroSetTabControl_FlagType.Name = "metroSetTabControl_FlagType";
            this.metroSetTabControl_FlagType.SelectedIndex = 0;
            this.metroSetTabControl_FlagType.SelectedTextColor = System.Drawing.Color.White;
            this.metroSetTabControl_FlagType.Size = new System.Drawing.Size(454, 393);
            this.metroSetTabControl_FlagType.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroSetTabControl_FlagType.Speed = 100;
            this.metroSetTabControl_FlagType.Style = MetroSet_UI.Enums.Style.Dark;
            this.metroSetTabControl_FlagType.StyleManager = null;
            this.metroSetTabControl_FlagType.TabIndex = 7;
            this.metroSetTabControl_FlagType.TabStyle = MetroSet_UI.Enums.TabStyle.Style2;
            this.metroSetTabControl_FlagType.ThemeAuthor = "Narwin";
            this.metroSetTabControl_FlagType.ThemeName = "MetroDark";
            this.metroSetTabControl_FlagType.UnselectedTextColor = System.Drawing.Color.Gray;
            this.metroSetTabControl_FlagType.UseAnimation = false;
            // 
            // tabPage_BitFlags
            // 
            this.tabPage_BitFlags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabPage_BitFlags.Controls.Add(this.tlp_BitFlags);
            this.tabPage_BitFlags.ForeColor = System.Drawing.Color.Silver;
            this.tabPage_BitFlags.Location = new System.Drawing.Point(4, 42);
            this.tabPage_BitFlags.Name = "tabPage_BitFlags";
            this.tabPage_BitFlags.Size = new System.Drawing.Size(446, 347);
            this.tabPage_BitFlags.TabIndex = 0;
            this.tabPage_BitFlags.Text = "Bit Flags";
            // 
            // tlp_BitFlags
            // 
            this.tlp_BitFlags.ColumnCount = 1;
            this.tlp_BitFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_BitFlags.Controls.Add(this.groupBox_NewDisabled, 0, 1);
            this.tlp_BitFlags.Controls.Add(this.groupBox_NewEnabled, 0, 0);
            this.tlp_BitFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_BitFlags.Location = new System.Drawing.Point(0, 0);
            this.tlp_BitFlags.Name = "tlp_BitFlags";
            this.tlp_BitFlags.RowCount = 2;
            this.tlp_BitFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_BitFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_BitFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_BitFlags.Size = new System.Drawing.Size(446, 347);
            this.tlp_BitFlags.TabIndex = 0;
            // 
            // groupBox_NewDisabled
            // 
            this.groupBox_NewDisabled.Controls.Add(this.listBox_DisabledFlags);
            this.groupBox_NewDisabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_NewDisabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox_NewDisabled.Location = new System.Drawing.Point(0, 173);
            this.groupBox_NewDisabled.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_NewDisabled.Name = "groupBox_NewDisabled";
            this.groupBox_NewDisabled.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_NewDisabled.Size = new System.Drawing.Size(446, 174);
            this.groupBox_NewDisabled.TabIndex = 5;
            this.groupBox_NewDisabled.TabStop = false;
            this.groupBox_NewDisabled.Text = "Newly Disabled Flags";
            // 
            // listBox_DisabledFlags
            // 
            this.listBox_DisabledFlags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listBox_DisabledFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_DisabledFlags.ForeColor = System.Drawing.Color.Silver;
            this.listBox_DisabledFlags.Location = new System.Drawing.Point(0, 19);
            this.listBox_DisabledFlags.Name = "listBox_DisabledFlags";
            this.listBox_DisabledFlags.Size = new System.Drawing.Size(446, 155);
            this.listBox_DisabledFlags.TabIndex = 1;
            // 
            // groupBox_NewEnabled
            // 
            this.groupBox_NewEnabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.groupBox_NewEnabled.Controls.Add(this.listBox_EnabledFlags);
            this.groupBox_NewEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_NewEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox_NewEnabled.Location = new System.Drawing.Point(0, 0);
            this.groupBox_NewEnabled.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_NewEnabled.Name = "groupBox_NewEnabled";
            this.groupBox_NewEnabled.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_NewEnabled.Size = new System.Drawing.Size(446, 173);
            this.groupBox_NewEnabled.TabIndex = 4;
            this.groupBox_NewEnabled.TabStop = false;
            this.groupBox_NewEnabled.Text = "Newly Enabled Flags";
            // 
            // listBox_EnabledFlags
            // 
            this.listBox_EnabledFlags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(73)))), ((int)(((byte)(65)))));
            this.listBox_EnabledFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_EnabledFlags.ForeColor = System.Drawing.Color.Silver;
            this.listBox_EnabledFlags.Location = new System.Drawing.Point(0, 19);
            this.listBox_EnabledFlags.Name = "listBox_EnabledFlags";
            this.listBox_EnabledFlags.Size = new System.Drawing.Size(446, 154);
            this.listBox_EnabledFlags.TabIndex = 0;
            // 
            // tabPage_Count
            // 
            this.tabPage_Count.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabPage_Count.Controls.Add(this.tlp_Count);
            this.tabPage_Count.ForeColor = System.Drawing.Color.Silver;
            this.tabPage_Count.Location = new System.Drawing.Point(4, 42);
            this.tabPage_Count.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage_Count.Name = "tabPage_Count";
            this.tabPage_Count.Size = new System.Drawing.Size(446, 347);
            this.tabPage_Count.TabIndex = 1;
            this.tabPage_Count.Text = "Count";
            // 
            // tlp_Count
            // 
            this.tlp_Count.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tlp_Count.ColumnCount = 1;
            this.tlp_Count.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Count.Controls.Add(this.groupBox_UnsetCount, 0, 1);
            this.tlp_Count.Controls.Add(this.groupBox_NewCount, 0, 0);
            this.tlp_Count.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Count.Location = new System.Drawing.Point(0, 0);
            this.tlp_Count.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_Count.Name = "tlp_Count";
            this.tlp_Count.RowCount = 2;
            this.tlp_Count.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Count.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Count.Size = new System.Drawing.Size(446, 347);
            this.tlp_Count.TabIndex = 0;
            // 
            // groupBox_UnsetCount
            // 
            this.groupBox_UnsetCount.Controls.Add(this.listBox_UnsetCounts);
            this.groupBox_UnsetCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_UnsetCount.Location = new System.Drawing.Point(0, 173);
            this.groupBox_UnsetCount.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_UnsetCount.Name = "groupBox_UnsetCount";
            this.groupBox_UnsetCount.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_UnsetCount.Size = new System.Drawing.Size(446, 174);
            this.groupBox_UnsetCount.TabIndex = 6;
            this.groupBox_UnsetCount.TabStop = false;
            this.groupBox_UnsetCount.Text = "Newly Unset Count";
            // 
            // listBox_UnsetCounts
            // 
            this.listBox_UnsetCounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listBox_UnsetCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_UnsetCounts.ForeColor = System.Drawing.Color.Silver;
            this.listBox_UnsetCounts.Location = new System.Drawing.Point(0, 25);
            this.listBox_UnsetCounts.Name = "listBox_UnsetCounts";
            this.listBox_UnsetCounts.Size = new System.Drawing.Size(446, 149);
            this.listBox_UnsetCounts.TabIndex = 1;
            // 
            // groupBox_NewCount
            // 
            this.groupBox_NewCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.groupBox_NewCount.Controls.Add(this.listBox_SetCounts);
            this.groupBox_NewCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_NewCount.Location = new System.Drawing.Point(0, 0);
            this.groupBox_NewCount.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_NewCount.Name = "groupBox_NewCount";
            this.groupBox_NewCount.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_NewCount.Size = new System.Drawing.Size(446, 173);
            this.groupBox_NewCount.TabIndex = 5;
            this.groupBox_NewCount.TabStop = false;
            this.groupBox_NewCount.Text = "Newly Changed Count";
            // 
            // listBox_SetCounts
            // 
            this.listBox_SetCounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(73)))), ((int)(((byte)(65)))));
            this.listBox_SetCounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_SetCounts.ForeColor = System.Drawing.Color.Silver;
            this.listBox_SetCounts.Location = new System.Drawing.Point(0, 25);
            this.listBox_SetCounts.Name = "listBox_SetCounts";
            this.listBox_SetCounts.Size = new System.Drawing.Size(446, 148);
            this.listBox_SetCounts.TabIndex = 0;
            // 
            // tlp_Checkboxes
            // 
            this.tlp_Checkboxes.ColumnCount = 2;
            this.tlp_Checkboxes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlp_Checkboxes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 264F));
            this.tlp_Checkboxes.Controls.Add(this.chk_AutoRename, 1, 0);
            this.tlp_Checkboxes.Controls.Add(this.chkBox_Sections, 0, 0);
            this.tlp_Checkboxes.Controls.Add(this.lbl_TimeStamp, 0, 1);
            this.tlp_Checkboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Checkboxes.Location = new System.Drawing.Point(3, 402);
            this.tlp_Checkboxes.Name = "tlp_Checkboxes";
            this.tlp_Checkboxes.RowCount = 2;
            this.tlp_Checkboxes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_Checkboxes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_Checkboxes.Size = new System.Drawing.Size(454, 65);
            this.tlp_Checkboxes.TabIndex = 4;
            // 
            // chk_AutoRename
            // 
            this.chk_AutoRename.AutoSize = true;
            this.chk_AutoRename.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk_AutoRename.Location = new System.Drawing.Point(193, 3);
            this.chk_AutoRename.Name = "chk_AutoRename";
            this.chk_AutoRename.Size = new System.Drawing.Size(169, 39);
            this.chk_AutoRename.TabIndex = 6;
            this.chk_AutoRename.Text = "Auto-Rename";
            // 
            // chkBox_Sections
            // 
            this.chkBox_Sections.AutoSize = true;
            this.chkBox_Sections.Checked = true;
            this.chkBox_Sections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBox_Sections.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkBox_Sections.Location = new System.Drawing.Point(3, 3);
            this.chkBox_Sections.Name = "chkBox_Sections";
            this.chkBox_Sections.Size = new System.Drawing.Size(118, 39);
            this.chkBox_Sections.TabIndex = 4;
            this.chkBox_Sections.Text = "Sections";
            // 
            // lbl_TimeStamp
            // 
            this.lbl_TimeStamp.AutoSize = true;
            this.tlp_Checkboxes.SetColumnSpan(this.lbl_TimeStamp, 2);
            this.lbl_TimeStamp.Location = new System.Drawing.Point(3, 45);
            this.lbl_TimeStamp.Name = "lbl_TimeStamp";
            this.lbl_TimeStamp.Size = new System.Drawing.Size(0, 20);
            this.lbl_TimeStamp.TabIndex = 7;
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.renameToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAllToolStripMenuItem});
            this.copyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.copyToolStripMenuItem.Text = "Copy Flowscript";
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.copyAllToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(148, 26);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            // 
            // columnHeader
            // 
            this.columnHeader.Width = 100;
            // 
            // menuStrip_Main
            // 
            this.menuStrip_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.menuStrip_Main.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.menuStrip_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pasteFlagsToolStripMenuItem});
            this.menuStrip_Main.Location = new System.Drawing.Point(2, 0);
            this.menuStrip_Main.Name = "menuStrip_Main";
            this.menuStrip_Main.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip_Main.Size = new System.Drawing.Size(696, 28);
            this.menuStrip_Main.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.Save_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.Load_Click);
            // 
            // pasteFlagsToolStripMenuItem
            // 
            this.pasteFlagsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pasteFlagsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.pasteFlagsToolStripMenuItem.Name = "pasteFlagsToolStripMenuItem";
            this.pasteFlagsToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.pasteFlagsToolStripMenuItem.Text = "Paste Flags";
            this.pasteFlagsToolStripMenuItem.Click += new System.EventHandler(this.PasteFlags_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox_Comparisons);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tlp_Main);
            this.splitContainer1.Size = new System.Drawing.Size(696, 470);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 3;
            // 
            // listBox_Comparisons
            // 
            this.listBox_Comparisons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Comparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Comparisons.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.listBox_Comparisons.FormattingEnabled = true;
            this.listBox_Comparisons.ItemHeight = 22;
            this.listBox_Comparisons.Location = new System.Drawing.Point(0, 0);
            this.listBox_Comparisons.Name = "listBox_Comparisons";
            this.listBox_Comparisons.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_Comparisons.Size = new System.Drawing.Size(232, 470);
            this.listBox_Comparisons.TabIndex = 0;
            this.listBox_Comparisons.SelectedIndexChanged += new System.EventHandler(this.SelectedComparison_Changed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip_Main);
            this.DropShadowEffect = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.HeaderHeight = -40;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Opacity = 0.99D;
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowHeader = true;
            this.ShowLeftRect = false;
            this.ShowTitle = false;
            this.Style = MetroSet_UI.Enums.Style.Dark;
            this.Text = "P5FlagComparer";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.tlp_Main.ResumeLayout(false);
            this.metroSetTabControl_FlagType.ResumeLayout(false);
            this.tabPage_BitFlags.ResumeLayout(false);
            this.tlp_BitFlags.ResumeLayout(false);
            this.groupBox_NewDisabled.ResumeLayout(false);
            this.groupBox_NewEnabled.ResumeLayout(false);
            this.tabPage_Count.ResumeLayout(false);
            this.tlp_Count.ResumeLayout(false);
            this.groupBox_UnsetCount.ResumeLayout(false);
            this.groupBox_NewCount.ResumeLayout(false);
            this.tlp_Checkboxes.ResumeLayout(false);
            this.tlp_Checkboxes.PerformLayout();
            this.menuStrip_Main.ResumeLayout(false);
            this.menuStrip_Main.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tlp_Main;
        private MenuStrip menuStrip_Main;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private TableLayoutPanel tlp_Checkboxes;
        private CheckBox chkBox_Sections;
        private ContextMenu ContextMenu_RightClick;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem pasteFlagsToolStripMenuItem;
        private ColumnHeader columnHeader;
        private CheckBox chk_AutoRename;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem copyAllToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ListBox listBox_Comparisons;
        private MetroSet_UI.Controls.MetroSetTabControl metroSetTabControl_FlagType;
        private TabPage tabPage_BitFlags;
        private TableLayoutPanel tlp_BitFlags;
        private GroupBox groupBox_NewDisabled;
        private ListBox listBox_DisabledFlags;
        private GroupBox groupBox_NewEnabled;
        private ListBox listBox_EnabledFlags;
        private TabPage tabPage_Count;
        private TableLayoutPanel tlp_Count;
        private GroupBox groupBox_UnsetCount;
        private ListBox listBox_UnsetCounts;
        private GroupBox groupBox_NewCount;
        private ListBox listBox_SetCounts;
        private Label lbl_TimeStamp;
    }
}