using MetroSet_UI.Forms;
using Newtonsoft.Json;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using System.Xml.Linq;
using static P5RFlagComparer.MainForm;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

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
            bs_newlyEnabledFlags.DataSource = comparison.EnabledFlags.Where(x => !previousComparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))).ToList();
            listBox_EnabledFlags.DataSource = bs_newlyEnabledFlags;
            listBox_EnabledFlags.FormattingEnabled = true;
            listBox_EnabledFlags.Format += FlagFormat;
        }

        private void UpdateNewlyDisabledFlagsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlyDisabledFlags = new BindingSource();
            bs_newlyDisabledFlags.DataSource = previousComparison.EnabledFlags.Where(x => !comparison.EnabledFlags.Any(y => y.Id.Equals(x.Id))).ToList();
            listBox_DisabledFlags.DataSource = bs_newlyDisabledFlags;
            listBox_DisabledFlags.FormattingEnabled = true;
            listBox_DisabledFlags.Format += FlagFormat;
        }

        private void UpdateNewlySetCountsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlySetCounts = new BindingSource();
            bs_newlySetCounts.DataSource = comparison.SetCounts.Where(x => !previousComparison.SetCounts.Any(y => y.Id.Equals(x.Id))
            || previousComparison.SetCounts.Single(y => y.Id.Equals(x.Id)).Value != x.Value).ToList();
            listBox_SetCounts.DataSource = bs_newlySetCounts;
            listBox_SetCounts.FormattingEnabled = true;
            listBox_SetCounts.Format += CountFormat;
        }

        private void UpdateNewlyUnsetCountsList(Comparison comparison, Comparison previousComparison)
        {
            BindingSource bs_newlyUnsetCounts = new BindingSource();
            bs_newlyUnsetCounts.DataSource = previousComparison.SetCounts.Where(x => !comparison.SetCounts.Any(y => y.Id.Equals(x.Id))).ToList();
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
                e.Value += $" // {GetMappedName(flag.Id, settings.countMappings)}";
            }
        }

        private void FlagFormat(object sender, ListControlConvertEventArgs e)
        {
            var flag = (BitFlag)e.ListItem;

            e.Value = GetFormattedFlag(flag.Id);
            if (!string.IsNullOrEmpty(flag.Name))
            {
                e.Value += $" // {GetMappedName(flag.Id, settings.flagMappings)}";
            }
        }

        private Comparison GetSelectedComparison()
        {
            return (Comparison)listBox_Comparisons.SelectedItem;
        }

        private Comparison GetPreviousComparison()
        {
            Comparison previousComparison = new Comparison();

            if (listBox_Comparisons.Items.Count <= 0 || listBox_Comparisons.SelectedIndex <= 0)
                return previousComparison;

            return (Comparison)listBox_Comparisons.Items[listBox_Comparisons.SelectedIndex - 1];
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

            if (listBox_Comparisons.Items.Count > 1)
                listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;

        }

        private void ImportWiki_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Load Wiki Source Text...", true, new string[] { "Text File (.txt)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            string[] wikiLines = File.ReadAllLines(filePaths.First());
            for (int i = 0; i < wikiLines.Count(); i++)
            {
                string line = wikiLines[i];
                string[] split = line.Split(new[] { "||" }, StringSplitOptions.None);
                if (split.Length == 5)
                {
                    int sectionID = Convert.ToInt32(split[0].Replace("|","").Replace("(R only)","").Trim());
                    int flagID = Convert.ToInt32(split[1].Trim());
                    string flagDescription = split[4].Trim();

                    if (!string.IsNullOrEmpty(flagDescription))
                    {
                        flagID = Flag.sRoyalBits[sectionID] + flagID;

                        if (settings.flagMappings.Any(x => x.Id == flagID))
                        {
                            var flagMapping = settings.flagMappings.First(x => x.Id == flagID);
                            if (!flagDescription.Contains("Unused"))
                                flagMapping.Name = $"[Wiki] {flagDescription}";
                        }
                        else
                            settings.flagMappings.Add(new BitFlag() { Id = flagID, Name = $"[Wiki] {flagDescription}" });
                    }
                }
                else if (split.Length == 3)
                {
                    if (!split[1].Contains("(HEX)"))
                    {
                        int countID = Convert.ToInt32(split[0].Replace("|","").Trim());
                        string countDesc = split[2].Trim();

                        if (!string.IsNullOrEmpty(countDesc))
                        {
                            if (settings.countMappings.Any(x => x.Id == countID))
                            {
                                var countMapping = settings.countMappings.First(x => x.Id == countID);
                                if (!countDesc.Contains("Unused"))
                                    countMapping.Name = $"[Wiki] {countDesc}";
                            }
                            else
                                settings.countMappings.Add(new BitFlag() { Id = countID, Name = $"[Wiki] {countDesc}" });

                        }
                    }
                }
            }

            settings.flagMappings = settings.flagMappings.OrderBy(x => x.Id).ToList();
            settings.countMappings = settings.countMappings.OrderBy(x => x.Id).ToList();

            MessageBox.Show($"Flag and Count mappings from the wiki have been imported!", "Wiki Table Imported Successfully");
        }

        private void Export_Click(object sender, EventArgs e)
        {
            for (int s = 0; s < 5; s++)
            {
                string txt = "{| class=\"wikitable sortable" +
                    "\r\n\"! Section !! ID within section !! Sumbits(P5) !! Sumbits(P5R) !! Description\r\n";
                for (int i = 0; i < Flag.sRoyalBits[s + 1]; i++)
                {
                    txt += "|-\r\n";

                    int royalSumBitsID = Flag.sRoyalBits[s + 1] + i;
                    int vanillaSumBitsID = Flag.sVanillaBits[s + 1] + i;

                    string desc = "";
                    string sectionSuffix = "";
                    if (settings.flagMappings.Any(x => x.Id == royalSumBitsID))
                    {
                        desc = settings.flagMappings.First(x => x.Id == royalSumBitsID).Name.Replace("[Wiki] ", "");
                    }
                    if (i > Flag.sVanillaBits[s])
                        sectionSuffix = " (R only)";

                    txt += $"| {s}{sectionSuffix} || {i.ToString("D4")} || {i.ToString("D4")} || {i.ToString("D4")} || {desc}\r\n";
                }
                txt += "|}";

                // Get output path from file select prompt
                var outPaths = WinFormsDialogs.SelectFile($"Save Flag Section {s} Wiki Table...", true, new string[] { "Text File (.txt)" }, true);
                if (outPaths == null || outPaths.Count == 0 || string.IsNullOrEmpty(outPaths.First()))
                    return;

                // Ensure output path ends with .txt
                string outPath = outPaths.First();
                if (!outPath.ToLower().EndsWith(".txt"))
                    outPath += ".txt";

                // Save to .json file
                File.WriteAllText(outPath, txt);
            }

            string countTxt = "[[Category:Persona 5]]\r\n\r\n==Counters==\r\n{| class=\"wikitable\"\r\n! ID (Dec.) || ID (HEX) || Description";
            for (int i = 0; i < 256; i++)
            {
                string desc = "";
                if (settings.countMappings.Any(x => x.Id == i))
                {
                    desc = settings.countMappings.First(x => x.Id == i).Name.Replace("[Wiki] ", "");
                }
                countTxt += "|-\r\n" +
                    $"| {i.ToString("D4")} || {i.ToString("X")} || {desc}";
            }

            countTxt += "|}";

            // Get output path from file select prompt
            var outTxtPaths = WinFormsDialogs.SelectFile($"Save Counters Wiki Table...", true, new string[] { "Text File (.txt)" }, true);
            if (outTxtPaths == null || outTxtPaths.Count == 0 || string.IsNullOrEmpty(outTxtPaths.First()))
                return;

            // Ensure output path ends with .txt
            string outTxtPath = outTxtPaths.First();
            if (!outTxtPath.ToLower().EndsWith(".txt"))
                outTxtPath += ".txt";

            // Save to .txt file
            File.WriteAllText(outTxtPath, countTxt);
        }

        private void ImportMappings_Click(object sender, EventArgs e)
        {
            var filePaths = WinFormsDialogs.SelectFile("Import Mappings From Project...", true, new string[] { "Project JSON (.json)" });
            if (filePaths == null || filePaths.Count == 0 || string.IsNullOrEmpty(filePaths.First()))
                return;

            var tempSettings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(filePaths.First()));
            SetupListbox();

            settings.flagMappings = tempSettings.flagMappings;
            settings.countMappings = tempSettings.countMappings;

            MessageBox.Show($"Flag and Count mappings from the selected Project Json have been imported!", "Json Mappings Imported Successfully");
        }

        private void ExportMappings_Click(object sender, EventArgs e)
        {
            string txt = "";
            for (int s = 0; s < 5; s++)
            {
                txt += $"FLAG SECTION {s}\r\n";
                for (int i = 0; i < Flag.sRoyalBits[s + 1]; i++)
                {
                    int royalSumBitsID = Flag.sRoyalBits[s + 1] + i;
                    int vanillaSumBitsID = Flag.sVanillaBits[s + 1] + i;

                    string desc = "";
                    if (settings.flagMappings.Any(x => x.Id == royalSumBitsID))
                    {
                        desc = settings.flagMappings.First(x => x.Id == royalSumBitsID).Name.Replace("[Wiki] ", "");
                    }

                    txt += $"{s}:{i} ({royalSumBitsID}) // {desc}\r\n";
                }
            }

            txt += $"COUNTERS\r\n";
            for (int i = 0; i < 256; i++)
            {
                string desc = "";
                if (settings.countMappings.Any(x => x.Id == i))
                {
                    desc = settings.countMappings.First(x => x.Id == i).Name.Replace("[Wiki] ", "");
                }
                txt += $"{i} // {desc}\r\n";
            }

            // Get output path from file select prompt
            var outTxtPaths = WinFormsDialogs.SelectFile($"Save Mappings Txt...", true, new string[] { "Text File (.txt)" }, true);
            if (outTxtPaths == null || outTxtPaths.Count == 0 || string.IsNullOrEmpty(outTxtPaths.First()))
                return;

            // Ensure output path ends with .txt
            string outTxtPath = outTxtPaths.First();
            if (!outTxtPath.ToLower().EndsWith(".txt"))
                outTxtPath += ".txt";

            // Save to .txt file
            File.WriteAllText(outTxtPath, txt);

        }

        private void DeleteToolStrip_Click(object sender, EventArgs e)
        {
            RemoveComparison();
        }

        private void PasteFlags_Click(object sender, EventArgs e)
        {
            CompareFromClipboard();
        }

        private void CopyFlags_Click(object sender, EventArgs e)
        {
            CopyAllToClipboard();
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            CopyToClipboard(sender);
        }

        private void ToggleTheme_Click(object sender, EventArgs e)
        {
            if (Theme.ThemeStyle == MetroSet_UI.Enums.Style.Dark)
                Theme.ThemeStyle = MetroSet_UI.Enums.Style.Light;
            else
                Theme.ThemeStyle = MetroSet_UI.Enums.Style.Dark;

            Theme.ApplyToForm(this);
        }
    }
}
