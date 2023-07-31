using DarkUI.Controls;
using DarkUI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5FlagCompare
{
    public partial class MainForm: DarkForm
    {
        private void Output_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                CopyToClipboard(sender);

            if (e.Modifiers == Keys.Control && e.Modifiers == Keys.Shift && e.KeyCode == Keys.C)
                CopyAllToClipboard();

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                CompareFromClipboard();

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
                RenameSelection(sender);

            if (e.KeyCode == Keys.Delete)
                RemoveComparison();
        }

        private void Sections_CheckedChanged(object sender, EventArgs e)
        {
            // Update names of items in enabled flags list depending on whether flag sections are enabled
            foreach (var item in listView_EnabledFlags.Items)
            {
                BitFlag bitFlag = (BitFlag)item.Tag;
                item.Text = GetMappedName(bitFlag.Id, settings.flagMappings, chkBox_Sections.Checked);
            }
            // Update names of items in disabled flags list depending on whether flag sections are enabled
            foreach (var item in listView_DisabledFlags.Items)
            {
                BitFlag bitFlag = (BitFlag)item.Tag;
                item.Text = GetMappedName(bitFlag.Id, settings.flagMappings, chkBox_Sections.Checked);
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
                listView_EnabledFlags.Items.Add(new DarkListItem() { Text = GetMappedName(flag.Id, settings.flagMappings, chkBox_Sections.Checked), Tag = flag });
            // If previous comparison has any flag Ids disabled that the latest comparison doesn't, add to listView
            listView_DisabledFlags.Items.Clear();
            foreach (var flag in previousComparison.EnabledFlags.Where(x => !comparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))))
                listView_DisabledFlags.Items.Add(new DarkListItem() { Text = GetMappedName(flag.Id, settings.flagMappings, chkBox_Sections.Checked), Tag = flag });
            // If previous comparison has any count Ids set that the latest comparison doesn't, add to listView
            listView_SetCounts.Items.Clear();
            foreach (var count in comparison.SetCounts.Where(x => !previousComparison.SetCounts.Any(y => y.Id.Equals(x.Id)) 
            || previousComparison.SetCounts.Single(y => y.Id.Equals(x.Id)).Value != x.Value))
                listView_SetCounts.Items.Add(new DarkListItem() { Text = GetMappedName(count.Id, settings.countMappings) + $": {count.Value}", Tag = count });
            // If previous comparison has any count Ids unset that the latest comparison doesn't, add to listView
            listView_UnsetCounts.Items.Clear();
            foreach (var count in previousComparison.SetCounts.Where(x => !comparison.SetCounts.Any(y => y.Id.Equals(x.Id))))
                listView_UnsetCounts.Items.Add(new DarkListItem() { Text = GetMappedName(count.Id, settings.countMappings) + $": 0", Tag = count });
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
            var selection = WinFormsEvents.FilePath_Click("Save Project...", true, new string[] { "Project JSON (.json)" }, true);
            if (selection == null || selection.Count == 0 || string.IsNullOrEmpty(selection.First()))
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
            if (selection == null || selection.Count == 0 || string.IsNullOrEmpty(selection.First()))
                return;

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(selection.First()));

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
