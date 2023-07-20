namespace P5FlagCompare
{
    partial class MainForm
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
            tlp_Main.SuspendLayout();
            SuspendLayout();
            // 
            // tlp_Main
            // 
            tlp_Main.ColumnCount = 2;
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp_Main.Controls.Add(listBox_Comparisons, 0, 0);
            tlp_Main.Controls.Add(rtb_Output, 1, 0);
            tlp_Main.Dock = DockStyle.Fill;
            tlp_Main.Location = new Point(0, 0);
            tlp_Main.Name = "tlp_Main";
            tlp_Main.RowCount = 3;
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tlp_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlp_Main.Size = new Size(800, 450);
            tlp_Main.TabIndex = 0;
            // 
            // listBox_Comparisons
            // 
            listBox_Comparisons.BackColor = SystemColors.MenuText;
            listBox_Comparisons.BorderStyle = BorderStyle.None;
            listBox_Comparisons.Dock = DockStyle.Fill;
            listBox_Comparisons.ForeColor = SystemColors.InactiveBorder;
            listBox_Comparisons.FormattingEnabled = true;
            listBox_Comparisons.ItemHeight = 20;
            listBox_Comparisons.Location = new Point(3, 3);
            listBox_Comparisons.Name = "listBox_Comparisons";
            tlp_Main.SetRowSpan(listBox_Comparisons, 3);
            listBox_Comparisons.Size = new Size(394, 444);
            listBox_Comparisons.TabIndex = 0;
            listBox_Comparisons.SelectedIndexChanged += SelectedComparison_Changed;
            // 
            // rtb_Output
            // 
            rtb_Output.BackColor = SystemColors.ActiveCaptionText;
            rtb_Output.BorderStyle = BorderStyle.None;
            rtb_Output.Dock = DockStyle.Fill;
            rtb_Output.ForeColor = SystemColors.InactiveBorder;
            rtb_Output.Location = new Point(403, 3);
            rtb_Output.Name = "rtb_Output";
            rtb_Output.ReadOnly = true;
            tlp_Main.SetRowSpan(rtb_Output, 3);
            rtb_Output.Size = new Size(394, 444);
            rtb_Output.TabIndex = 1;
            rtb_Output.Text = "";
            rtb_Output.KeyDown += Output_Keydown;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tlp_Main);
            Name = "Form1";
            Text = "P5 Flag Comparer";
            tlp_Main.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlp_Main;
        private ListBox listBox_Comparisons;
        private RichTextBox rtb_Output;
    }
}