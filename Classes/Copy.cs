using MetroSet_UI.Forms;
using System.Windows.Forms;

namespace P5RFlagComparer
{
    public partial class MainForm: MetroSetForm
    {
        private void CopyAllToClipboard()
        {
            if (listBox_Comparisons.SelectedIndices.Count <= 0)
                return;

            Comparison comparison = GetSelectedComparison();

            string clipboardText = "";
            foreach (var enabledFlag in comparison.EnabledFlags)
            {
                clipboardText += $"BIT_ON( {GetFormattedFlag(enabledFlag.Id)} );";
                if (!string.IsNullOrEmpty(GetMappedName(enabledFlag.Id, settings.flagMappings)) && chk_CopyNames.Checked)
                    clipboardText += $" // {GetMappedName(enabledFlag.Id, settings.flagMappings)}";
                clipboardText += "\n";
            }
            foreach (var setCount in comparison.SetCounts)
            {
                clipboardText += $"SET_COUNT( {setCount.Id}, {setCount.Value} );";
                if (!string.IsNullOrEmpty(GetMappedName(setCount.Id, settings.countMappings)) && chk_CopyNames.Checked)
                    clipboardText += $" // {GetMappedName(setCount.Id, settings.countMappings)}";
                clipboardText += "\n";
            }

            if (!string.IsNullOrEmpty(clipboardText))
            {
                Clipboard.SetText(clipboardText);
                lbl_TimeStamp.Text = "Copied Text to Clipboard";
            }
        }

        private void CopyToClipboard(object sender)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip menu = menuItem.Owner as ContextMenuStrip;

                if (menu != null)
                    CopySelection(menu.SourceControl);
            }
            else
                CopySelection(sender);
        }

        private void CopySelection(object sender)
        {
            string ctrlName = ((Control)sender).Name;
            if (ctrlName == "listBox_EnabledFlags" || ctrlName == "listBox_DisabledFlags"
                || ctrlName == "listBox_SetCounts" || ctrlName == "listBox_UnsetCounts")
            {
                ListBox listBox = (ListBox)sender;

                if (listBox.SelectedIndices.Count <= 0)
                    return;

                CopyItems(new ListBox[] { listBox });
            }
            else
                CopyItems(new ListBox[] { listBox_EnabledFlags, listBox_DisabledFlags, listBox_SetCounts, listBox_UnsetCounts });
        }

        private void CopyItems(ListBox[] listBoxs)
        {
            string clipboardText = "";

            foreach (var listBox in listBoxs)
            {
                if (listBox.SelectedIndices.Count <= 0)
                    return;

                bool isCount = listBox.Name.Contains("Count");
                foreach (int index in listBox.SelectedIndices)
                {
                    BitFlag bitFlag = (BitFlag)listBox.Items[index];

                    string flagName = "";
                    if (!isCount)
                        flagName = GetMappedName(bitFlag.Id, settings.flagMappings);
                    else
                        flagName = GetMappedName(bitFlag.Id, settings.countMappings);

                    if (listBox.Name.Contains("EnabledFlags"))
                        clipboardText += $"BIT_ON( {bitFlag.Id} );";
                    else if (listBox.Name.Contains("DisabledFlags"))
                        clipboardText += $"BIT_OFF( {bitFlag.Id} );";
                    else if (listBox.Name.Contains("SetCounts"))
                        clipboardText += $"SET_COUNT( {bitFlag.Id}, {bitFlag.Value} );";
                    else if (listBox.Name.Contains("UnsetCounts"))
                        clipboardText += $"SET_COUNT( {bitFlag.Id}, 0 );";

                    if (!string.IsNullOrEmpty(flagName))
                        clipboardText += $" // {flagName}";

                    clipboardText += "\n";
                }
            }

            if (!string.IsNullOrEmpty(clipboardText))
            {
                Clipboard.SetText(clipboardText);
                lbl_TimeStamp.Text = "Copied Text to Clipboard";
            }
        }
    }
}
