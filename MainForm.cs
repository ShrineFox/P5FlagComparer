using MetroSet_UI.Forms;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace P5RFlagComparer
{
    public partial class MainForm : MetroSetForm
    {
        Settings settings = new Settings();
        public class Settings
        {
            public List<Comparison> comparisons = new List<Comparison>();
            public List<BitFlag> flagMappings = new List<BitFlag>();
            public List<BitFlag> countMappings = new List<BitFlag>();
        }

        public class Comparison
        {
            public string Name { get; set; } = "Untitled";
            public string TimeStamp { get; set; } = "";
            public List<BitFlag> EnabledFlags { get; set; } = new List<BitFlag>();
            public List<BitFlag> SetCounts { get; set; } = new List<BitFlag>();
        }

        public class BitFlag
        {
            public string Name { get; set; } = "Untitled";
            public int Id { get; set; } = 0;

            public int Value { get; set; } = 0;
        }

        public MainForm()
        {
            InitializeComponent();
            Theme.ApplyToForm(this);
            MenuStripHelper.SetMenuStripIcons(MenuStripHelper.GetMenuStripIconPairs("icons.txt"), this);

            SetupListbox();
        }

        private void SetupListbox()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = settings.comparisons;
            listBox_Comparisons.DataSource = bs;
            listBox_Comparisons.DisplayMember = "Name";
            listBox_Comparisons.ValueMember = "Name";
        }

        private void CopyAll_Click(object sender, EventArgs e)
        {
            CopyAllToClipboard();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            CopyToClipboard(sender);
        }
    }

    public static class ControlExtensions
    {
        public static IEnumerable<Control> FlattenChildren<T>(this Control control)
        {
            return control.FlattenChildren().OfType<T>().Cast<Control>();
        }

        public static IEnumerable<Control> FlattenChildren(this Control control)
        {
            var children = control.Controls.Cast<Control>();
            return children.SelectMany(c => FlattenChildren(c)).Concat(children);
        }
    }
}