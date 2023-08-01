using DarkUI.Controls;
using DarkUI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;

namespace P5FlagCompare
{
    public partial class MainForm: DarkForm
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
                DarkListView listView = (DarkListView)sender;
                List<int> indexes = new List<int>();
                for (int i = 0; i < listView.Items.Count(); i++)
                    indexes.Add(i);
                if (indexes.Count > 0)
                    listView.SelectItems(indexes);
            }
        }

        private void Sections_CheckedChanged(object sender, EventArgs e)
        {
            // Update names of items in listViews depending on whether flag sections are enabled
            foreach (var listView in new DarkListView[] { listView_EnabledFlags, listView_DisabledFlags })
            {
                foreach (var item in listView.Items)
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
            UpdateDarkListViews();
        }

        private void UpdateDarkListViews()
        {
            // Compare flags/counts between selected comparison and previous and update listViews
            if (listView_Comparisons.SelectedIndices.Count <= 0)
                return;

            // Get currently selected comparison
            Comparison comparison = GetSelectedComparison();

            // Get previous comparison if one exists
            Comparison previousComparison = GetPreviousComparison();

            // If previous comparison has any flag Ids enabled that the latest comparison doesn't, add to listView
            listView_EnabledFlags.Items.Clear();
            foreach (var flag in comparison.EnabledFlags.Where(x => !previousComparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))))
            {
                var listItem = new DarkListItem() { Text = GetFormattedFlag(flag.Id), Tag = flag };

                string name = GetMappedName(flag.Id, settings.flagMappings);
                if (!string.IsNullOrEmpty(name))
                    listItem.Text += $" // {name}";

                listView_EnabledFlags.Items.Add(listItem);
            }
            // If previous comparison has any flag Ids disabled that the latest comparison doesn't, add to listView
            listView_DisabledFlags.Items.Clear();
            foreach (var flag in previousComparison.EnabledFlags.Where(x => !comparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))))
            {
                var listItem = new DarkListItem() { Text = GetFormattedFlag(flag.Id), Tag = flag };

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
                var listItem = new DarkListItem() { Text = $"{count.Id}: {count.Value}", Tag = count };

                string name = GetMappedName(count.Id, settings.countMappings);
                if (!string.IsNullOrEmpty(name))
                    listItem.Text += $" // {name}";

                listView_SetCounts.Items.Add(listItem);
            }
            // If previous comparison has any count Ids unset that the latest comparison doesn't, add to listView
            listView_UnsetCounts.Items.Clear();
            foreach (var count in previousComparison.SetCounts.Where(x => !comparison.SetCounts.Any(y => y.Id.Equals(x.Id))))
            {
                var listItem = new DarkListItem() { Text = $"{count.Id}: 0", Tag = count };

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
                listView_Comparisons.Items.RemoveAt(listView_Comparisons.SelectedIndices.Last());
                listView_Comparisons.SelectItem(listView_Comparisons.Items.Count - 1);
            }
        }

        private Comparison GetSelectedComparison()
        {
            return (Comparison)listView_Comparisons.Items[listView_Comparisons.SelectedIndices.Last()].Tag;
        }

        private Comparison GetPreviousComparison(int selectedIndex = -1)
        {
            Comparison previousComparison = new Comparison();

            if (listView_Comparisons.SelectedIndices.Count <= 0 || listView_Comparisons.SelectedIndices.Last() == 0)
                return previousComparison;

            if (selectedIndex == -1)
                selectedIndex = listView_Comparisons.SelectedIndices.Last();
                
            return (Comparison)listView_Comparisons.Items[selectedIndex - 1].Tag;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            // Replace comparisons object with only data from form
            List<Comparison> comparisons = new List<Comparison>();
            foreach (var item in listView_Comparisons.Items)
                comparisons.Add((Comparison)item.Tag);
            settings.comparisons = comparisons;

            // Get output path from file select prompt
            var outPaths = WinFormsEvents.FilePath_Click("Save Project...", true, new string[] { "Project JSON (.json)" }, true);
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
            var filePaths = WinFormsEvents.FilePath_Click("Load Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filePaths.First()));

            listView_Comparisons.Items.Clear();
            foreach (var comparison in settings.comparisons)
                listView_Comparisons.Items.Add(new DarkListItem() { Text = comparison.Name, Tag = comparison });
            listView_Comparisons.SelectItem(listView_Comparisons.Items.Count - 1);

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
