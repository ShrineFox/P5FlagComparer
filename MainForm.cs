using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using DarkUI.Controls;
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
            public List<Tuple<int, string>> flagMappings = new List<Tuple<int, string>>();
            public List<Tuple<int, string>> countMappings = new List<Tuple<int, string>>();
        }

        public class Comparison
        {
            public string Name { get; set; } = "Untitled";
            public string TimeStamp { get; set; } = "";
            public List<int> EnabledFlags { get; set; } = new List<int>();
            public List<int> NewEnabledFlags { get; set; } = new List<int>();
            public List<int> NewDisabledFlags { get; set; } = new List<int>();
            public List<Tuple<int, int>> SetCounts { get; set; } = new List<Tuple<int, int>>();
            public List<Tuple<int, int>> NewChangedCounts { get; set; } = new List<Tuple<int, int>>();
            public List<Tuple<int, int>> NewUnsetCounts { get; set; } = new List<Tuple<int, int>>();
        }

        public MainForm()
        {
            InitializeComponent();
            SetMenuStripIcons();
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

        private void PasteFlags_Click(object sender, EventArgs e)
        {
            DoComparison();
        }

        private void DoComparison()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                string[] clipboardLines = clipboardText.Split('\n');
                // Get last enabled flag/count dump from clipboard
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

            // Get enabled flag/count IDs until end of dump
            for (int x = startLine; x < clipboardLines.Length; x++)
            {
                if (clipboardLines[x].Contains("End Enabled Flag Dump"))
                {
                    if (clipboardLines[x + 1].Contains("Start Count Value Dump"))
                    {
                        int countId = 0;
                        for (int y = x + 1; y < clipboardLines.Length; x++)
                        {
                            if (clipboardLines[x].Contains("End Count Value Dump"))
                                break;
                            int countValue = Convert.ToInt32(clipboardLines[x]);
                            if (countValue != 0)
                                comparison.SetCounts.Add(new Tuple<int, int>(countId, countValue));
                            countId++;
                        }
                    }
                    break;
                }

                comparison.EnabledFlags.Add(Convert.ToInt32(clipboardLines[x]));
            }

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
                // If the count value is zero and the value previously wasn't, display it as newly unset
                foreach (var previouslyEnabledCount in settings.comparisons.Last().SetCounts)
                {
                    if (!comparison.SetCounts.Any(x => x.Item1 == previouslyEnabledCount.Item1))
                        comparison.NewUnsetCounts.Add(new Tuple<int, int>(previouslyEnabledCount.Item1, 0));
                }
                // If the count value is different from previous dump, display it as newly changed
                foreach (var newlyDumpedCount in comparison.SetCounts)
                {
                    if (!settings.comparisons.Last().SetCounts.Any(x => x.Equals(newlyDumpedCount)))
                        comparison.NewChangedCounts.Add(newlyDumpedCount);
                }
            }
            else
                comparison.NewEnabledFlags = comparison.EnabledFlags;

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

        private void Sections_CheckedChanged(object sender, EventArgs e)
        {
            UpdateForm();
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
                    string mappedName = GetMappedFlagName(enabledFlag);

                    listBox_NewlyEnabled.Items.Add(name + mappedName);
                }
                foreach (var disabledFlag in comparison.NewDisabledFlags)
                {
                    string name = GetFormattedFlag(disabledFlag);
                    string mappedName = GetMappedFlagName(disabledFlag);

                    listBox_NewlyDisabled.Items.Add(name + mappedName);
                }

                foreach (var enabledFlag in comparison.EnabledFlags)
                {
                    string name = GetFormattedFlag(enabledFlag);
                    string mappedName = GetMappedFlagName(enabledFlag);

                    Color highlightColor = rtb_AllEnabledFlags.BackColor;
                    if (comparison.NewEnabledFlags.Any(x => x.Equals(enabledFlag)))
                        highlightColor = Color.FromArgb(255, 60, 93, 65);

                    AppendText($"BIT_ON({name});{mappedName}", highlightColor, true);
                }

                foreach (var newSetCount in comparison.NewChangedCounts)
                {
                    string mappedName = GetMappedCountName(newSetCount.Item1);
                    listBox_NewCount.Items.Add($"{newSetCount.Item1}: {newSetCount.Item2}{mappedName}");
                }

                foreach (var newUnsetCount in comparison.NewUnsetCounts)
                {
                    string mappedName = GetMappedCountName(newUnsetCount.Item1);
                    listBox_UnsetCount.Items.Add($"{newUnsetCount.Item1}: {newUnsetCount.Item2}{mappedName}");
                }

                foreach (var setCount in comparison.SetCounts)
                {
                    string mappedName = GetMappedCountName(setCount.Item1);

                    Color highlightColor = rtb_AllEnabledFlags.BackColor;
                    if (comparison.NewEnabledFlags.Any(x => x.Equals(setCount.Item1)))
                        highlightColor = Color.FromArgb(255, 60, 93, 65);

                    AppendText($"SET_COUNT({setCount.Item1}, {setCount.Item2});{mappedName}", highlightColor, true);
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
            listBox_NewCount.Items.Clear();
            listBox_UnsetCount.Items.Clear();
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

        private string GetMappedFlagName(int flagId)
        {
            string mappedName = "";
            if (settings.flagMappings.Any(x => x.Item1.Equals(flagId)))
                mappedName = $" // {settings.flagMappings.First(x => x.Item1.Equals(flagId)).Item2}";
            return mappedName;
        }

        private string GetMappedCountName(int countId)
        {
            string mappedName = "";
            if (settings.countMappings.Any(x => x.Item1.Equals(countId)))
                mappedName = $" // {settings.countMappings.First(x => x.Item1.Equals(countId)).Item2}";
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

        private void DoRename(object sender)
        {
            string ctrlName = ((Control)sender).Name;
            if (ctrlName == "listBox_NewlyEnabled" || ctrlName == "listBox_NewlyDisabled"
                || ctrlName == "listBox_NewCount" || ctrlName == "listBox_UnsetCount" )
            {
                bool isCount = false;
                if (ctrlName.Contains("Count"))
                    isCount = true;

                ListBox listBox = (ListBox)sender;

                string selectedName = "";
                if (!isCount)
                    selectedName = listBox.SelectedItem.ToString().Split('/').First().Trim().Split(' ').Last();
                else
                    selectedName = listBox.SelectedItem.ToString().Split(':').First().Trim();
                int id = Convert.ToInt32(selectedName);

                string newName = RenameById(id, isCount);

                if (listBox.SelectedItems.Count > 1)
                {
                    foreach (var item in listBox.SelectedItems)
                    {
                        if (!isCount)
                        {
                            // Get Flag ID by section if using formatting
                            if (listBox.SelectedItem.ToString().StartsWith("Flag.Section"))
                            {
                                int flagSection = Convert.ToInt32(item.ToString().Substring(12, 1));
                                id = Flag.sRoyalBits[flagSection] + id;
                            }

                            // Set unique placeholder name
                            int i = 1;
                            string name = newName;
                            while (settings.flagMappings.Any(x => x.Item2.Equals(name)))
                            {
                                i++;
                                name = newName + i;
                            }

                            SetNewFlagName(name, id);
                        }
                        else
                        {
                            selectedName = item.ToString().Split(':').First().Trim();
                            id = Convert.ToInt32(selectedName);

                            // Set unique placeholder name
                            int i = 1;
                            string name = newName;
                            while (settings.countMappings.Any(x => x.Item2.Equals(name)))
                            {
                                i++;
                                name = newName + i;
                            }

                            SetNewCountName(name, id);
                        }
                    }
                }

                UpdateForm();
            }
            else
                RenameComparison();
        }

        private void RenameComparison()
        {
            if (listBox_Comparisons.SelectedItem == null)
                return;

            RenameForm renameForm = new RenameForm(listBox_Comparisons.SelectedItem.ToString());
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;
                settings.comparisons.First(x => x.Name.Equals(listBox_Comparisons.SelectedItem.ToString())).Name = newName;
                UpdateForm();
            }
        }

        private string RenameById(int id, bool isCount = false)
        {
            // Get old mapped name if one exists
            string oldName = "";
            if (!isCount)
            {
                if (settings.flagMappings.Any(x => x.Item1.Equals(id)))
                    oldName = settings.flagMappings.First(x => x.Item1.Equals(id)).Item2;
            }
            else
            {
                if (settings.countMappings.Any(x => x.Item1.Equals(id)))
                    oldName = settings.countMappings.First(x => x.Item1.Equals(id)).Item2.ToString();
            }

            RenameForm renameForm = new RenameForm(oldName);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;

                if (!isCount)
                    SetNewFlagName(newName, id);
                else
                    SetNewCountName(newName, id);

                return newName;
            }

            return oldName;
        }

        private void SetNewFlagName(string newName, int flagId)
        {
            // Remove existing mapping if one exists
            if (settings.flagMappings.Any(x => x.Item1.Equals(flagId)))
                settings.flagMappings.Remove(settings.flagMappings.First(x => x.Item1.Equals(flagId)));
            // Add new mapping
            settings.flagMappings.Add(new Tuple<int, string>(flagId, newName));
            // Re-order flagMappings in numerical order by flag ID
            settings.flagMappings = settings.flagMappings.OrderBy(x => x.Item1).ToList();
        }

        private void SetNewCountName(string name, int id)
        {
            // Remove existing mapping if one exists
            if (settings.countMappings.Any(x => x.Item1.Equals(id)))
                settings.countMappings.Remove(settings.countMappings.First(x => x.Item1.Equals(id)));
            // Add new mapping
            settings.countMappings.Add(new Tuple<int, string>(id, name));
            // Re-order countMappings in numerical order by count ID
            settings.countMappings = settings.countMappings.OrderBy(x => x.Item1).ToList();
        }

        private void DeleteToolStrip_Click(object sender, EventArgs e)
        {
            DoDelete();
        }

        private void DoDelete()
        {
            if (listBox_Comparisons.SelectedItem == null)
                return;

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

        private void SetMenuStripIcons()
        {
            List<Tuple<string, string>> menuStripIcons = new List<Tuple<string, string>>() {
                new Tuple<string, string>("fileToolStripMenuItem", "disk"),
                new Tuple<string, string>("loadToolStripMenuItem", "folder_page"),
                new Tuple<string, string>("saveToolStripMenuItem", "disk_multiple"),
                new Tuple<string, string>("pasteFlagsToolStripMenuItem", "paste_plain"),
                new Tuple<string, string>("deleteToolStripMenuItem", "delete"),
                new Tuple<string, string>("renameToolStripMenuItem", "textfield_rename"),
            };

            // Context Menu Strips
            foreach (DarkContextMenu menuStrip in new DarkContextMenu[] { darkContextMenu_RightClick })
                ApplyIconsFromList(menuStrip.Items, menuStripIcons);

            // Menu Strip Items
            foreach (DarkMenuStrip menuStrip in this.FlattenChildren<DarkMenuStrip>())
                ApplyIconsFromList(menuStrip.Items, menuStripIcons);
        }

        private void ApplyIconsFromList(ToolStripItemCollection items, List<Tuple<string, string>> menuStripIcons)
        {
            foreach (ToolStripMenuItem tsmi in items)
            {
                // Apply context menu icon
                if (menuStripIcons.Any(x => x.Item1 == tsmi.Name))
                    ApplyIconFromFile(tsmi, menuStripIcons);
                // Apply drop down menu icon
                foreach (ToolStripMenuItem tsmi2 in tsmi.DropDownItems)
                    if (menuStripIcons.Any(x => x.Item1 == tsmi2.Name))
                        ApplyIconFromFile(tsmi2, menuStripIcons);
            }
        }

        private void ApplyIconFromFile(ToolStripMenuItem tsmi, List<Tuple<string, string>> menuStripIcons)
        {
            tsmi.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        $"Icons\\{menuStripIcons.Single(x => x.Item1 == tsmi.Name).Item2}.png"));
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