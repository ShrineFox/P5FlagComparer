using DarkUI.Controls;
using DarkUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

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
                if (!string.IsNullOrEmpty(enabledFlag.Name))
                    clipboardText += $" // {enabledFlag.Name}";
                clipboardText += "\n";
            }
            foreach (var setCount in comparison.SetCounts)
            {
                clipboardText += $"SET_COUNT( {setCount.Id}, {setCount.Value} );";
                if (!string.IsNullOrEmpty(setCount.Name))
                    clipboardText += $" // {setCount.Name}";
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
                {
                    Control controlSelected = menu.SourceControl;
                    CopySelection(controlSelected);
                }
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

                    string flagName = bitFlag.Id.ToString();
                    if (!isCount)
                        flagName = GetMappedName(bitFlag.Id, settings.flagMappings, chkBox_Sections.Checked);
                    else
                        flagName = GetMappedName(bitFlag.Id, settings.countMappings);

                    if (listView.Name.Contains("EnabledFlags"))
                        clipboardText += $"BIT_ON( {flagName} );";
                    else if (listView.Name.Contains("DisabledFlags"))
                        clipboardText += $"BIT_OFF( {flagName} );";
                    else if (listView.Name.Contains("SetCounts"))
                        clipboardText += $"SET_COUNT( {flagName}, {bitFlag.Value} );";
                    else if (listView.Name.Contains("UnsetCounts"))
                        clipboardText += $"SET_COUNT( {flagName}, 0 );";

                    if (!string.IsNullOrEmpty(bitFlag.Name) && bitFlag.Name != "")
                        clipboardText += $" // {bitFlag.Name}";

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
