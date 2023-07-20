using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Xml.Linq;

namespace P5FlagCompare
{
    public partial class MainForm : Form
    {
        List<Comparison> comparisons = new List<Comparison>();
        public class Comparison
        {
            public string Name { get; set; } = "Untitled";
            public List<int> EnabledFlags { get; set; } = new List<int>();
            public List<int> NewEnabledFlags { get; set; } = new List<int>();
            public List<int> NewDisabledFlags { get; set; } = new List<int>();

        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void Output_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                DoComparison();
        }

        private void DoComparison()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                string[] clipboardLines = clipboardText.Split('\n');
                for (int i = clipboardLines.Length - 1; i > -1; i--)
                {
                    if (clipboardLines[i].Contains("Start Enabled Flag Dump"))
                    {
                        AddComparison(clipboardLines, i + 1);
                        break;
                    }
                }
            }
        }

        private void AddComparison(string[] clipboardLines, int startLine)
        {
            Comparison comparison = new Comparison();
            for (int x = startLine; x < clipboardLines.Length; x++)
            {
                if (clipboardLines[x].Contains("End Enabled Flag Dump"))
                    break;
                comparison.EnabledFlags.Add(Convert.ToInt32(clipboardLines[x]));
            }

            if (comparisons.Count > 0)
            {
                // If the new enabled flags list is missing a flag from the previous dump, display it as removed
                foreach (var enabledFlag in comparisons.Last().EnabledFlags)
                {
                    if (!comparison.EnabledFlags.Any(x => x.Equals(enabledFlag)))
                        comparison.NewDisabledFlags.Add(enabledFlag);
                }
                // If a newly enabled flag isn't found in the previous flag dump, display it as added
                foreach (var enabledFlag in comparison.EnabledFlags)
                {
                    if (comparisons.Last().EnabledFlags.Any(x => !x.Equals(enabledFlag)))
                        comparison.NewEnabledFlags.Add(enabledFlag);
                }
            }
            else
                comparison.NewEnabledFlags = comparison.EnabledFlags;
            // Set unique placeholder name
            int i = 1;
            string name = comparison.Name;
            while (comparisons.Any(x => x.Name.Equals(comparison.Name)))
            {
                i++;
                comparison.Name = name + i;
            }

            comparisons.Add(comparison);
            UpdateForm();
        }

        private void UpdateForm()
        {
            listBox_Comparisons.Items.Clear();
            foreach (var comparison in comparisons)
            {
                listBox_Comparisons.Items.Add(comparison.Name);
            }
            rtb_Output.Clear();
            listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;
        }

        private void SelectedComparison_Changed(object sender, EventArgs e)
        {
            rtb_Output.Clear();
            if (listBox_Comparisons.SelectedItem != null && comparisons.Any(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString())))
            {
                var comparison = comparisons.First(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString()));
                foreach (var enabledFlag in comparison.NewDisabledFlags)
                    rtb_Output.Text += enabledFlag + "\n";
                foreach (var disabledFlag in comparison.NewDisabledFlags)
                    rtb_Output.Text += disabledFlag + "\n";
            }
        }
    }
}