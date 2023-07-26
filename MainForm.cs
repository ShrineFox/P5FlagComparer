using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using DarkUI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using static System.Collections.Specialized.BitVector32;

namespace P5FlagCompare
{
    public partial class MainForm : DarkForm
    {
        Settings settings = new Settings();
        public class Settings
        {
            public List<Comparison> comparisons = new List<Comparison>();
            public List<Tuple<int, string>> mappings = new List<Tuple<int, string>>();
        }

        public class Comparison
        {
            public string Name { get; set; } = "Untitled";
            public string TimeStamp { get; set; } = "";
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

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
                DoRename(sender);

            if (e.KeyCode == Keys.Delete)
                DoDelete();
        }

        private void DoDelete()
        {
            if (WinFormsDialogs.YesNoMsgBox("Remove Comparison?",
                "Would you like to remove the currently selected comparison from the list?", MessageBoxIcon.Question))
                RemoveComparison();
        }

        private void RemoveComparison()
        {
            settings.comparisons.Remove(
                settings.comparisons.First(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString())));

            UpdateForm();
        }

        private void DoRename(object sender)
        {
            string ctrlName = ((Control)sender).Name;
            if (ctrlName == "listBox_NewlyEnabled" || ctrlName == "listBox_NewlyDisabled")
            {
                ListBox listBox = (ListBox)sender;
                string selectedFlag = listBox.SelectedItem.ToString().Split('/').First().Trim().Split(' ').Last();
                int flagId = Convert.ToInt32(selectedFlag);
                // Get Flag ID by section if using formatting
                if (listBox.SelectedItem.ToString().StartsWith("Flag.Section"))
                {
                    int flagSection = Convert.ToInt32(listBox.SelectedItem.ToString().Substring(12, 1));
                    flagId = Flag.sRoyalBits[flagSection + 1] - flagId;
                }
                RenameFlag(flagId);
            }
            else
                RenameComparison();
        }

        private void RenameComparison()
        {
            RenameForm renameForm = new RenameForm(listBox_Comparisons.SelectedItem.ToString());
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;
                settings.comparisons.First(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString())).Name = newName;
                UpdateForm();
            }
        }

        private void RenameFlag(int flagId)
        {
            // Get old mapped name if one exists
            string oldName = "";
            bool mappingFound = false;
            if (settings.mappings.Any(x => x.Item1.Equals(flagId)))
            {
                mappingFound = true;
                oldName = settings.mappings.First(x => x.Item1.Equals(flagId)).Item2;
            }

            RenameForm renameForm = new RenameForm(oldName);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;
                // Remove existing mapping if one exists
                if (mappingFound)
                    settings.mappings.Remove(settings.mappings.First(x => x.Item1.Equals(flagId)));
                // Add new mapping
                settings.mappings.Add(new Tuple<int, string>(flagId, newName));
                // Re-order mappings in numerical order by flag ID
                settings.mappings = settings.mappings.OrderBy(x => x.Item1).ToList();

                UpdateForm();
            }
        }

        private void DoComparison()
        {
            // Get last flag dump from clipboard
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
            Comparison comparison = new Comparison() { TimeStamp = DateTime.Now.ToString() };

            for (int x = startLine; x < clipboardLines.Length; x++)
            {
                if (clipboardLines[x].Contains("End Enabled Flag Dump"))
                    break;

                comparison.EnabledFlags.Add(Convert.ToInt32(clipboardLines[x]));
            }

            comparison.EnabledFlags.Sort();

            if (settings.comparisons.Count > 0)
            {
                // If the new enabled flags list is missing a flag from the previous dump, display it as removed
                foreach (var enabledFlag in settings.comparisons.Last().EnabledFlags)
                {
                    if (!comparison.EnabledFlags.Any(x => x.Equals(enabledFlag)))
                        comparison.NewDisabledFlags.Add(enabledFlag);
                }
                // If a newly enabled flag isn't found in the previous flag dump, display it as added
                foreach (var enabledFlag in comparison.EnabledFlags)
                {
                    if (!settings.comparisons.Last().EnabledFlags.Any(x => x.Equals(enabledFlag)))
                        comparison.NewEnabledFlags.Add(enabledFlag);
                }
            }
            else
                comparison.NewEnabledFlags = comparison.EnabledFlags;

            comparison.NewEnabledFlags.Sort();
            comparison.NewDisabledFlags.Sort();

            // Set unique placeholder name
            int i = 1;
            string name = comparison.Name;
            while (settings.comparisons.Any(x => x.Name.Equals(comparison.Name)))
            {
                i++;
                comparison.Name = name + i;
            }

            // Add to comparison list and update form if different from previous comparison
            if (comparison.NewEnabledFlags.Count > 0 || comparison.NewDisabledFlags.Count > 0)
            {
                settings.comparisons.Add(comparison);
                UpdateForm();
            }
        }

        private void UpdateForm()
        {
            listBox_Comparisons.Items.Clear();
            foreach (var comparison in settings.comparisons)
            {
                listBox_Comparisons.Items.Add(comparison.Name);
            }
            ClearFormItems();
            listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;
        }

        private void SelectedComparison_Changed(object sender, EventArgs e)
        {
            ReloadSelection();
        }

        private void ReloadSelection()
        {
            ClearFormItems();

            if (listBox_Comparisons.SelectedItem != null && settings.comparisons.Any(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString())))
            {
                var comparison = settings.comparisons.First(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString()));

                foreach (var enabledFlag in comparison.NewEnabledFlags)
                {
                    string name = GetFormattedFlag(enabledFlag);
                    string mappedName = GetMappedName(enabledFlag);

                    listBox_NewlyEnabled.Items.Add(name + mappedName);
                }
                foreach (var disabledFlag in comparison.NewDisabledFlags)
                {
                    string name = GetFormattedFlag(disabledFlag);
                    string mappedName = GetMappedName(disabledFlag);

                    listBox_NewlyDisabled.Items.Add(name + mappedName);
                }

                foreach (var enabledFlag in comparison.EnabledFlags)
                {
                    string name = GetFormattedFlag(enabledFlag);
                    string mappedName = GetMappedName(enabledFlag);

                    Color highlightColor = rtb_AllEnabledFlags.BackColor;
                    if (comparison.NewEnabledFlags.Any(x => x.Equals(enabledFlag)))
                        highlightColor = Color.FromArgb(255, 60, 93, 65);
                    
                    AppendText($"BIT_ON({name});{mappedName}", highlightColor, true);
                }

                // Update timestamp
                lbl_TimeStamp.Text = comparison.TimeStamp;
            }
        }

        public void AppendText(string text, Color color, bool addNewLine = false)
        {
            //rtb_AllEnabledFlags.SuspendLayout();
            rtb_AllEnabledFlags.SelectionBackColor = color;
            rtb_AllEnabledFlags.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            //rtb_AllEnabledFlags.ScrollToCaret();
            //rtb_AllEnabledFlags.ResumeLayout();
        }

        private void ClearFormItems()
        {
            listBox_NewlyEnabled.Items.Clear();
            listBox_NewlyDisabled.Items.Clear();
            rtb_AllEnabledFlags.Clear();
            lbl_TimeStamp.Text = "";
        }

        private string GetFormattedFlag(int flagId)
        {
            string name = flagId.ToString();

            if (chkBox_Sections.Checked)
            {
                for (int i = 1; i < Flag.sRoyalBits.Length; i++)
                {
                    if (flagId < Flag.sRoyalBits[i])
                    {
                        int section = i - 1;
                        name = $"Flag.Section{section} + {flagId - Flag.sRoyalBits[i - 1]}";
                        break;
                    }
                }
            }

            return $"{name}";
        }

        private string GetMappedName(int flagId)
        {
            string mappedName = "";
            if (settings.mappings.Any(x => x.Item1.Equals(flagId)))
                mappedName = $" // {settings.mappings.First(x => x.Item1.Equals(flagId)).Item2}";
            return mappedName;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var selection = WinFormsEvents.FilePath_Click("Save Project...", true, new string[] { "Project JSON (.json)" }, true);
            if (selection.Count == 0)
                return;

            string outPath = selection.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath += ".json";

            File.WriteAllText(outPath, JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented));
            MessageBox.Show($"Saved project file to:\n{outPath}", "Preset Project Successfully");
        }

        private void Load_Click(object sender, EventArgs e)
        {
            var selection = WinFormsEvents.FilePath_Click("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (selection.Count == 0)
                return;

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(selection.First()));

            UpdateForm();
            ReloadSelection();
        }

        private void RenameToolStrip_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip menu = menuItem.Owner as ContextMenuStrip;

                if (menu != null)
                {
                    Control controlSelected = menu.SourceControl;
                    DoRename(controlSelected);
                }
            }
        }

        private void DeleteToolStrip_Click(object sender, EventArgs e)
        {
            DoDelete();
        }

        private void Sections_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
        }
    }
}