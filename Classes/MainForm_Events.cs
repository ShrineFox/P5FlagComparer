using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static P5RFlagComparer.MainForm;

namespace P5RFlagComparer
{
    public partial class MainForm: MetroSetForm
    {
        private void Output_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
                SelectAll(sender);

            if (e.Modifiers == Keys.Control && e.Modifiers == Keys.Shift && e.KeyCode == Keys.C)
                CopyAllToClipboard();
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                CopyToClipboard(sender);

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                CompareFromClipboard();

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
                RenameSelection(sender);

            if (e.KeyCode == Keys.Delete)
                RemoveComparison();
        }

        private void SelectAll(object sender)
        {
            string ctrlName = ((Control)sender).Name;
            if (ctrlName == "listBox_EnabledFlags" || ctrlName == "listBox_DisabledFlags"
                || ctrlName == "listBox_SetCounts" || ctrlName == "listBox_UnsetCounts")
            {
                ListBox listBox = (ListBox)sender;
                List<int> indexes = new List<int>();
                for (int i = 0; i < listBox.Items.Count; i++)
                    indexes.Add(i);
                if (indexes.Count > 0)
                    foreach (var index in indexes)
                        listBox.SelectedItems.Add(listBox.Items[index]);
            }
        }

        private void Sections_CheckedChanged(object sender, EventArgs e)
        {
            UpdateListBoxes();
        }

        private void SelectedComparison_Changed(object sender, EventArgs e)
        {
            UpdateListBoxes();
        }

        private void UpdateListBoxes()
        {
            // Compare flags/counts between selected comparison and previous and update listBoxs
            if (listBox_Comparisons.SelectedIndices.Count <= 0)
                return;

            // Get currently selected comparison
            Comparison comparison = GetSelectedComparison();

            // Get previous comparison if one exists
            Comparison previousComparison = GetPreviousComparison();

            // If previous comparison has any flag Ids enabled that the latest comparison doesn't, add to listBox
            UpdateNewlyEnabledFlagsList(comparison, previousComparison);

            // If previous comparison has any flag Ids disabled that the latest comparison doesn't, add to listBox
            UpdateNewlyDisabledFlagsList(comparison, previousComparison);
            // If previous comparison has any count Ids set that the latest comparison doesn't, add to listBox
            UpdateNewlySetCountsList(comparison, previousComparison);
            // If previous comparison has any count Ids unset that the latest comparison doesn't, add to listBox
            UpdateNewlyUnsetCountsList(comparison, previousComparison);

            // Update timestamp
            lbl_TimeStamp.Text = comparison.TimeStamp;

            // Remove current comparison and select latest if all listBoxs are empty
            if (listBox_UnsetCounts.Items.Count == 0 && listBox_SetCounts.Items.Count == 0
                && listBox_DisabledFlags.Items.Count == 0 && listBox_EnabledFlags.Items.Count == 0)
            {
                settings.comparisons.Remove((Comparison)listBox_Comparisons.SelectedItem);
                listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;
            }
        }

        private void UpdateNewlyEnabledFlagsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlyEnabledFlags = new BindingSource();
            bs_newlyEnabledFlags.DataSource = comparison.EnabledFlags.Where(x => !previousComparison.EnabledFlags.Any(y => y.Id.Equals(x.Id)));
            listBox_EnabledFlags.DataSource = bs_newlyEnabledFlags;
            listBox_EnabledFlags.FormattingEnabled = true;
            listBox_EnabledFlags.Format += FlagFormat;
        }

        private void UpdateNewlyDisabledFlagsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlyDisabledFlags = new BindingSource();
            bs_newlyDisabledFlags.DataSource = previousComparison.EnabledFlags.Where(x => !comparison.EnabledFlags.Any(y => y.Id.Equals(x.Id)));
            listBox_DisabledFlags.DataSource = bs_newlyDisabledFlags;
            listBox_DisabledFlags.FormattingEnabled = true;
            listBox_DisabledFlags.Format += FlagFormat;
        }

        private void UpdateNewlySetCountsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlySetCounts = new BindingSource();
            bs_newlySetCounts.DataSource = comparison.SetCounts.Where(x => !previousComparison.SetCounts.Any(y => y.Id.Equals(x.Id))
            || previousComparison.SetCounts.Single(y => y.Id.Equals(x.Id)).Value != x.Value);
            listBox_SetCounts.DataSource = bs_newlySetCounts;
            listBox_SetCounts.FormattingEnabled = true;
            listBox_SetCounts.Format += CountFormat;
        }

        private void UpdateNewlyUnsetCountsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlyUnsetCounts = new BindingSource();
            bs_newlyUnsetCounts.DataSource = previousComparison.SetCounts.Where(x => !comparison.SetCounts.Any(y => y.Id.Equals(x.Id)));
            listBox_UnsetCounts.DataSource = bs_newlyUnsetCounts;
            listBox_UnsetCounts.FormattingEnabled = true;
            listBox_UnsetCounts.Format += CountFormat;
        }

        private void CountFormat(object sender, ListControlConvertEventArgs e)
        {
            var flag = (BitFlag)e.ListItem;

            e.Value = $"{flag.Id}: {flag.Value}";
            if (!string.IsNullOrEmpty(flag.Name))
            {
                e.Value += $" // {flag.Name}";
            }
        }

        private void FlagFormat(object sender, ListControlConvertEventArgs e)
        {
            var flag = (BitFlag)e.ListItem;

            e.Value = GetFormattedFlag(flag.Id);
            if (!string.IsNullOrEmpty(flag.Name))
            {
                e.Value += $" // {flag.Name}";
            }
        }

        private Comparison GetSelectedComparison()
        {
            return (Comparison)listBox_Comparisons.SelectedItem;
        }

        private Comparison GetPreviousComparison(int selectedIndex = -1)
        {
            Comparison previousComparison = new Comparison();

            if (listBox_Comparisons.SelectedIndices.Count <= 0 || listBox_Comparisons.SelectedIndices[listBox_Comparisons.SelectedIndices.Count - 1] == 0)
                return previousComparison;

            if (selectedIndex == -1)
                selectedIndex = listBox_Comparisons.SelectedIndices[listBox_Comparisons.SelectedIndices.Count - 1];
                
            return (Comparison)listBox_Comparisons.SelectedItem;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            // Replace comparisons object with only data from form
            List<Comparison> comparisons = new List<Comparison>();
            foreach (var item in listBox_Comparisons.Items)
                comparisons.Add((Comparison)item);
            settings.comparisons = comparisons;

            // Get output path from file select prompt
            var outPaths = WinFormsDialogs.SelectFile("Save Project...", true, new string[] { "Project JSON (.json)" }, true);
            if (outPaths == null || outPaths.Count == 0 || string.IsNullOrEmpty(outPaths.First()))
                return;

            // Ensure output path ends with .json
            string outPath = outPaths.First();
            if (!outPath.ToLower().EndsWith(".json"))
                outPath += ".json";

            // Remove default values from serialized objects
            string jsonText = JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);
            jsonText = jsonText.Replace("          \"Name\": \"Untitled\",\r\n", "").Replace("          \"Value\": 0\r\n", "");
            
            // Save to .json file
            File.WriteAllText(outPath, jsonText);
            MessageBox.Show($"Saved project file to:\n{outPath}", "Preset Project Successfully");
        }

        private void Load_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filePaths.First()));
            SetupListbox();

            listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;

        }

        private void DeleteToolStrip_Click(object sender, EventArgs e)
        {
            RemoveComparison();
        }

        private void PasteFlags_Click(object sender, EventArgs e)
        {
            CompareFromClipboard();
        }
    }
}
