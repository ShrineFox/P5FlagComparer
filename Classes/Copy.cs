using DarkUI.Controls;
using DarkUI.Forms;

namespace P5FlagCompare
{
    public partial class MainForm: DarkForm
    {
        private void CopyAllToClipboard()
        {
            if (listView_Comparisons.SelectedIndices.Count <= 0)
                return;

            Comparison comparison = GetSelectedComparison();

            string clipboardText = "";
            foreach (var enabledFlag in comparison.EnabledFlags)
            {
                clipboardText += $"BIT_ON( {GetFormattedFlag(enabledFlag.Id)} );";
                if (!string.IsNullOrEmpty(GetMappedName(enabledFlag.Id, settings.flagMappings)))
                    clipboardText += $" // {GetMappedName(enabledFlag.Id, settings.flagMappings)}";
                clipboardText += "\n";
            }
            foreach (var setCount in comparison.SetCounts)
            {
                clipboardText += $"SET_COUNT( {setCount.Id}, {setCount.Value} );";
                if (!string.IsNullOrEmpty(GetMappedName(setCount.Id, settings.countMappings)))
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
            if (ctrlName == "listView_EnabledFlags" || ctrlName == "listView_DisabledFlags"
                || ctrlName == "listView_SetCounts" || ctrlName == "listView_UnsetCounts")
            {
                DarkListView listView = (DarkListView)sender;

                if (listView.SelectedIndices.Count <= 0)
                    return;

                CopyItems(new DarkListView[] { listView });
            }
            else
                CopyItems(new DarkListView[] { listView_EnabledFlags, listView_DisabledFlags, listView_SetCounts, listView_UnsetCounts });
        }

        private void CopyItems(DarkListView[] listViews)
        {
            string clipboardText = "";

            foreach (var listView in listViews)
            {
                if (listView.SelectedIndices.Count <= 0)
                    return;

                bool isCount = listView.Name.Contains("Count");
                foreach (var index in listView.SelectedIndices)
                {
                    BitFlag bitFlag = (BitFlag)listView.Items[index].Tag;

                    string flagName = "";
                    if (!isCount)
                        flagName = GetMappedName(bitFlag.Id, settings.flagMappings);
                    else
                        flagName = GetMappedName(bitFlag.Id, settings.countMappings);

                    if (listView.Name.Contains("EnabledFlags"))
                        clipboardText += $"BIT_ON( {bitFlag.Id} );";
                    else if (listView.Name.Contains("DisabledFlags"))
                        clipboardText += $"BIT_OFF( {bitFlag.Id} );";
                    else if (listView.Name.Contains("SetCounts"))
                        clipboardText += $"SET_COUNT( {bitFlag.Id}, {bitFlag.Value} );";
                    else if (listView.Name.Contains("UnsetCounts"))
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
