using MetroSet_UI.Forms;
using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace P5RFlagComparer
{
    public partial class MainForm: MetroSetForm
    {
        private void RenameToolStrip_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip menu = menuItem.Owner as ContextMenuStrip;

                if (menu != null)
                {
                    Control controlSelected = menu.SourceControl;
                    RenameSelection(controlSelected);
                }
            }
        }

        private void RenameSelection(object sender)
        {
            string ctrlName = ((Control)sender).Name;
            if (ctrlName == "listView_EnabledFlags" || ctrlName == "listView_DisabledFlags"
                || ctrlName == "listView_SetCounts" || ctrlName == "listView_UnsetCounts")
            {
                ListView listView = (ListView)sender;

                if (listView.SelectedIndices.Count <= 0)
                    return;

                RenameItems(listView, ctrlName.Contains("Count"));
            }
            else
                RenameComparison();
        }

        private void RenameComparison()
        {
            if (listBox_Comparisons.SelectedIndices.Count <= 0)
                return;

            var comparison = GetSelectedComparison();
            
            RenameForm renameForm = new RenameForm("Rename", comparison.Name);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;
                if (newName == "")
                    newName = "Untitled";

                // Set unique placeholder name
                int i = 1;
                string name = newName;
                bool nameTaken = false;
                while (true)
                {
                    foreach (ListViewItem item in listBox_Comparisons.Items)
                    {
                        if (item.Text == name)
                        {
                            i++;
                            name = newName + " " + i;
                            nameTaken = true;
                        }
                    }
                    if (!nameTaken)
                        break;
                }

                // Set comparison in listView to new name
                comparison.Name = name;

                if (chk_AutoRename.Checked && name != "" && name != "Untitled")
                    RenameAllItems(name);
            }
        }

        private void RenameAllItems(string name)
        {
            foreach (ListView listView in new ListView[] { listView_EnabledFlags, listView_DisabledFlags, listView_SetCounts, listView_UnsetCounts })
            {
                bool isCount = listView.Name.Contains("Count");

                foreach (ListViewItem item in listView.Items)
                {
                    if (!item.Text.Contains("//"))
                    {
                        BitFlag flag = (BitFlag)item.Tag;
                        if (!isCount)
                            item.Text = $"{GetFormattedFlag(flag.Id)} // {SetNewName(name, flag.Id, isCount)}";
                        else
                            item.Text = $"{flag.Id}: {flag.Value} // {SetNewName(name, flag.Id, isCount)}";
                    }
                }
            }
        }

        private string SetNewName(string name, int id, bool isCount)
        {
            string newName = name;

            if (!isCount)
            {
                // Remove existing mapping if one exists
                if (settings.flagMappings.Any(x => x.Id.Equals(id)))
                    settings.flagMappings.Remove(settings.flagMappings.First(x => x.Id.Equals(id)));
                if (name != "")
                {
                    // Set unique name
                    int i = 1;
                    while (settings.flagMappings.Any(x => x.Name.Equals(newName)))
                    {
                        i++;
                        newName = name + " " + i;
                    }

                    // Add new mapping
                    settings.flagMappings.Add(new BitFlag() { Id = id, Name = newName });
                    // Re-order flagMappings in numerical order by flag ID
                    settings.flagMappings = settings.flagMappings.OrderBy(x => x.Id).ToList();
                }
            }
            else
            {
                // Remove existing mapping if one exists
                if (settings.countMappings.Any(x => x.Id.Equals(id)))
                    settings.countMappings.Remove(settings.countMappings.First(x => x.Id.Equals(id)));
                if (name != "")
                {
                    // Set unique name
                    int i = 1;
                    while (settings.countMappings.Any(x => x.Name.Equals(newName)))
                    {
                        i++;
                        newName = name + " " + i;
                    }

                    // Add new mapping
                    settings.countMappings.Add(new BitFlag() { Id = id, Name = newName });
                    // Re-order flagMappings in numerical order by flag ID
                    settings.countMappings = settings.countMappings.OrderBy(x => x.Id).ToList();
                }
            }

            return newName;
        }

        private void RenameItems(ListView listView, bool isCount = false)
        {
            if (listView.SelectedIndices.Count <= 0)
                return;

            List<BitFlag> mapping = settings.flagMappings;
            if (isCount)
                mapping = settings.countMappings;

            BitFlag selectedFlag = (BitFlag)listView.Items[listView.SelectedIndices[listBox_Comparisons.SelectedIndices.Count - 1]].Tag;
            RenameForm renameForm = new RenameForm("Rename", GetMappedName(selectedFlag.Id, mapping));
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;

                foreach (int index in listView.SelectedIndices)
                {
                    BitFlag flag = (BitFlag)listView.Items[index].Tag;
                    string name = SetNewName(newName, flag.Id, isCount);

                    if (isCount)
                        listView.Items[index].Text = $"{flag.Id}: {flag.Value}";
                    else
                        listView.Items[index].Text = $"{GetFormattedFlag(flag.Id)}";

                    if (!string.IsNullOrEmpty(name))
                        listView.Items[index].Text += $" // {name}";
                }
            }
        }

        private string GetFormattedFlag(int flagId)
        {
            string name = flagId.ToString();

            if (chkBox_Sections.Checked)
            {
                for (int i = 1; i < Flag.sRoyalBits.Length; i++)
                {
                    if (flagId < Flag.sRoyalBits[i])
                    {
                        int section = i - 1;
                        name = $"Flag.Section{section} + {flagId - Flag.sRoyalBits[i - 1]}";
                        break;
                    }
                }
            }

            return name;
        }

        private string GetMappedName(int id, List<BitFlag> list)
        {
            if (list.Any(x => x.Id.Equals(id)))
                return list.First(x => x.Id.Equals(id)).Name;

            return "";
        }
    }
}
