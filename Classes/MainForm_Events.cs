using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            if (ctrlName == "listView_EnabledFlags" || ctrlName == "listView_DisabledFlags"
                || ctrlName == "listView_SetCounts" || ctrlName == "listView_UnsetCounts")
            {
                ListView listView = (ListView)sender;
                List<int> indexes = new List<int>();
                for (int i = 0; i < listView.Items.Count; i++)
                    indexes.Add(i);
                if (indexes.Count > 0)
                    foreach (var index in indexes)
                        listView.Items[index].Selected = true;
            }
        }

        private void Sections_CheckedChanged(object sender, EventArgs e)
        {
            // Update names of items in listViews depending on whether flag sections are enabled
            foreach (var listView in new ListView[] { listView_EnabledFlags, listView_DisabledFlags })
            {
                foreach (ListViewItem item in listView.Items)
                {
                    BitFlag bitFlag = (BitFlag)item.Tag;
                    item.Text = GetFormattedFlag(bitFlag.Id);

                    string name = GetMappedName(bitFlag.Id, settings.flagMappings);
                    if (!string.IsNullOrEmpty(name))
                        item.Text += $" // {name}";
                }
            }
            
        }

        private void SelectedComparison_Changed(object sender, EventArgs e)
        {
            UpdateListViews();
        }

        private void UpdateListViews()
        {
            // Compare flags/counts between selected comparison and previous and update listViews
            if (listBox_Comparisons.SelectedIndices.Count <= 0)
                return;

            // Get currently selected comparison
            Comparison comparison = GetSelectedComparison();

            // Get previous comparison if one exists
            Comparison previousComparison = GetPreviousComparison();

            // If previous comparison has any flag Ids enabled that the latest comparison doesn't, add to listView
            listView_EnabledFlags.Items.Clear();
            foreach (var flag in comparison.EnabledFlags.Where(x => !previousComparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))))
            {
                var listItem = new ListViewItem() { Text = GetFormattedFlag(flag.Id), Tag = flag };

                string name = GetMappedName(flag.Id, settings.flagMappings);
                if (!string.IsNullOrEmpty(name))
                    listItem.Text += $" // {name}";

                listView_EnabledFlags.Items.Add(listItem);
            }
            // If previous comparison has any flag Ids disabled that the latest comparison doesn't, add to listView
            listView_DisabledFlags.Items.Clear();
            foreach (var flag in previousComparison.EnabledFlags.Where(x => !comparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))))
            {
                var listItem = new ListViewItem() { Text = GetFormattedFlag(flag.Id), Tag = flag };

                string name = GetMappedName(flag.Id, settings.flagMappings);
                if (!string.IsNullOrEmpty(name))
                    listItem.Text += $" // {name}";

                listView_DisabledFlags.Items.Add(listItem);
            }
            // If previous comparison has any count Ids set that the latest comparison doesn't, add to listView
            listView_SetCounts.Items.Clear();
            foreach (var count in comparison.SetCounts.Where(x => !previousComparison.SetCounts.Any(y => y.Id.Equals(x.Id)) 
            || previousComparison.SetCounts.Single(y => y.Id.Equals(x.Id)).Value != x.Value))
            {
                var listItem = new ListViewItem() { Text = $"{count.Id}: {count.Value}", Tag = count };

                string name = GetMappedName(count.Id, settings.countMappings);
                if (!string.IsNullOrEmpty(name))
                    listItem.Text += $" // {name}";

                listView_SetCounts.Items.Add(listItem);
            }
            // If previous comparison has any count Ids unset that the latest comparison doesn't, add to listView
            listView_UnsetCounts.Items.Clear();
            foreach (var count in previousComparison.SetCounts.Where(x => !comparison.SetCounts.Any(y => y.Id.Equals(x.Id))))
            {
                var listItem = new ListViewItem() { Text = $"{count.Id}: 0", Tag = count };

                string name = GetMappedName(count.Id, settings.countMappings);
                if (!string.IsNullOrEmpty(name))
                    listItem.Text += $" // {name}";

                listView_UnsetCounts.Items.Add(listItem);
            }
            // Update timestamp
            lbl_TimeStamp.Text = comparison.TimeStamp;

            // Remove current comparison and select latest if all listViews are empty
            if (listView_UnsetCounts.Items.Count == 0 && listView_SetCounts.Items.Count == 0
                && listView_DisabledFlags.Items.Count == 0 && listView_EnabledFlags.Items.Count == 0)
            {
                settings.comparisons.Remove((Comparison)listBox_Comparisons.SelectedItem);
                listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;
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
            foreach (ListViewItem item in listBox_Comparisons.Items)
                comparisons.Add((Comparison)item.Tag);
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
