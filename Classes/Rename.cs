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
            if (ctrlName == "listBox_EnabledFlags" || ctrlName == "listBox_DisabledFlags"
                || ctrlName == "listBox_SetCounts" || ctrlName == "listBox_UnsetCounts")
            {
                ListBox listBox = (ListBox)sender;

                if (listBox.SelectedIndices.Count <= 0)
                    return;

                RenameItems(listBox, ctrlName.Contains("Count"));
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
                    foreach (Comparison item in listBox_Comparisons.Items)
                    {
                        if (item.Name == name)
                        {
                            i++;
                            name = newName + " " + i;
                            nameTaken = true;
                        }
                    }
                    if (!nameTaken)
                        break;
                }

                // Set comparison in listBox to new name
                comparison.Name = name;

                if (chk_AutoRename.Checked && name != "" && name != "Untitled")
                    RenameAllItems(name);

                SetupListbox();
            }
        }

        private void RenameAllItems(string name)
        {
            foreach (ListBox listBox in new ListBox[] { listBox_EnabledFlags, listBox_DisabledFlags, listBox_SetCounts, listBox_UnsetCounts })
            {
                bool isCount = listBox.Name.Contains("Count");

                foreach (BitFlag flag in listBox.Items)
                {
                    SetNewName(name, flag.Id, isCount);
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

        private void RenameItems(ListBox listBox, bool isCount = false)
        {
            if (listBox.SelectedIndices.Count <= 0)
                return;

            List<BitFlag> mapping = settings.flagMappings;
            if (isCount)
                mapping = settings.countMappings;

            BitFlag selectedFlag = (BitFlag)listBox.Items[listBox.SelectedIndices[listBox_Comparisons.SelectedIndices.Count - 1]];
            RenameForm renameForm = new RenameForm("Rename", GetMappedName(selectedFlag.Id, mapping));
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;

                foreach (int index in listBox.SelectedIndices)
                {
                    BitFlag flag = (BitFlag)listBox.Items[index];
                    string name = SetNewName(newName, flag.Id, isCount);
                }
                SetupListbox();
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
