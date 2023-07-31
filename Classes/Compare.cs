using DarkUI.Controls;
using DarkUI.Forms;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static P5FlagCompare.MainForm;

namespace P5FlagCompare
{
    public partial class MainForm: DarkForm
    {
        private void CompareFromClipboard()
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {                
                // Get last enabled flag/count dump from clipboard
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                string[] clipboardLines = clipboardText.Split('\n');

                // Add each line of dump to new comparison
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
            // Create new comparison object with the present date/time as timestamp
            Comparison comparison = new Comparison() { TimeStamp = DateTime.Now.ToString() };

            // Get enabled flag/count IDs until end of dump
            for (int x = startLine; x < clipboardLines.Length; x++)
            {
                if (clipboardLines[x].Contains("End Enabled Flag Dump"))
                {
                    if (clipboardLines[x + 1].Contains("Start Count Value Dump"))
                    {
                        int countId = 0;
                        for (int y = x + 2; y < clipboardLines.Length; y++)
                        {
                            if (clipboardLines[y].Contains("End Count Value Dump"))
                                break;
                            int countValue = Convert.ToInt32(clipboardLines[y]);
                            if (countValue != 0)
                                comparison.SetCounts.Add(new BitFlag() { Id = countId, Value = countValue});
                            countId++;
                        }
                    }
                    break;
                }

                comparison.EnabledFlags.Add(new BitFlag() { Id = Convert.ToInt32(clipboardLines[x]) });
            }

            // Set unique placeholder name
            int i = 1;
            string name = comparison.Name;
            while (settings.comparisons.Any(x => x.Name.Equals(comparison.Name)))
            {
                i++;
                comparison.Name = name + i;
            }

            // Add to comparison list and update form if different from previous comparison
            Comparison previousComparison = GetPreviousComparison();
            if (comparison.EnabledFlags.Count > 0 || comparison.SetCounts.Count > 0
                && (!comparison.EnabledFlags.SequenceEqual(previousComparison.EnabledFlags) || !comparison.SetCounts.SequenceEqual(previousComparison.SetCounts)))
            {
                settings.comparisons.Add(comparison);
                listView_Comparisons.Items.Add(new DarkListItem() { Text = comparison.Name, Tag = comparison });
                listView_Comparisons.SelectItem(listView_Comparisons.Items.Count - 1);
            }
        }

        private void RemoveComparison()
        {
            if (listView_Comparisons.SelectedIndices.Count <= 0)
                return;

            if (WinFormsDialogs.YesNoMsgBox("Remove Comparison?",
                "Would you like to remove the currently selected comparison from the list?", MessageBoxIcon.Question))
                    listView_Comparisons.Items.RemoveAt(listView_Comparisons.SelectedIndices.Last());
        }
    }
}
