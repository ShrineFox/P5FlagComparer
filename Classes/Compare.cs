using MetroSet_UI.Forms;
using ShrineFox.IO;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace P5RFlagComparer
{
    public partial class MainForm: MetroSetForm
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

            // Add to comparison list
            settings.comparisons.Add(comparison);
            SetupListbox();
            listBox_Comparisons.SelectedIndex = listBox_Comparisons.Items.Count - 1;
        }


        private void RemoveComparison()
        {
            if (listBox_Comparisons.SelectedIndices.Count <= 0)
                return;

            if (WinFormsDialogs.ShowMessageBox("Remove Comparison?",
                "Would you like to remove the currently selected comparison from the list?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                foreach (Comparison comparison in listBox_Comparisons.SelectedItems)
                    settings.comparisons.Remove(comparison);
                SetupListbox();
            }
            
        }
    }
}
