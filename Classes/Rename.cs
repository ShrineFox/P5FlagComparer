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
using static P5FlagCompare.MainForm;

namespace P5FlagCompare
{
    public partial class MainForm: DarkForm
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
                DarkListView listView = (DarkListView)sender;

                if (listView.SelectedIndices.Count <= 0)
                    return;

                RenameItem(listView, ctrlName.Contains("Count"));
            }
            else
                RenameComparison();
        }

        private void RenameComparison()
        {
            if (listView_Comparisons.SelectedIndices.Count <= 0)
                return;

            var comparison = GetSelectedComparison();
            
            RenameForm renameForm = new RenameForm(comparison.Name);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;
                if (newName == "")
                    newName = "Untitled";
                // Set unique placeholder name
                int i = 1;
                string name = newName;
                while (listView_Comparisons.Items.Any(x => x.Text.Equals(name)))
                {
                    i++;
                    name = newName + " " + i;
                }
                listView_Comparisons.Items[listView_Comparisons.SelectedIndices.Last()].Text = name;
            }
        }

        private void RenameItem(DarkListView listView, bool isCount = false)
        {
            if (listView.SelectedIndices.Count <= 0)
                return;

            int selectedIndex = listView.SelectedIndices.Last();
            BitFlag flag = (BitFlag)listView.Items[selectedIndex].Tag;

            string newName = RenameById(flag.Id, isCount);

            if (listView.SelectedIndices.Count > 1)
            {
                foreach (int index in listView.SelectedIndices)
                {

                    flag = (BitFlag)listView.Items[index].Tag;
                    SetName(listView, index, flag, newName, isCount);
                }
            }
            else
                SetName(listView, selectedIndex, flag, newName, isCount);
        }

        private void SetName(DarkListView listView, int index, BitFlag flag, string newName, bool isCount = false)
        {
            // Set unique placeholder name
            int i = 1;
            string name = newName;

            while (listView.Items.Any(x => x.Text.Equals(name)))
            {
                i++;
                name = newName + " " + i;
            }

            List<BitFlag> mappings = new List<BitFlag>();
            if (!isCount)
            {
                SetNewFlagName(name, flag.Id);
                mappings = settings.flagMappings;
            }
            else
            {
                SetNewCountName(name, flag.Id);
                mappings = settings.countMappings;
            }

            listView.Items[index].Text = GetMappedName(flag.Id, mappings, chkBox_Sections.Checked);
        }

        private string RenameById(int id, bool isCount = false)
        {
            // Get old mapped name if one exists
            string oldName = "";
            if (!isCount)
            {
                if (settings.flagMappings.Any(x => x.Id.Equals(id)))
                    oldName = settings.flagMappings.First(x => x.Id.Equals(id)).Name;
            }
            else
            {
                if (settings.countMappings.Any(x => x.Id.Equals(id)))
                    oldName = settings.countMappings.First(x => x.Id.Equals(id)).Name;
            }

            RenameForm renameForm = new RenameForm(oldName);
            var result = renameForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = renameForm.RenameText;

                if (!isCount)
                    SetNewFlagName(newName, id);
                else
                    SetNewCountName(newName, id);

                return newName;
            }

            return oldName;
        }

        private void SetNewFlagName(string name, int id)
        {
            // Remove existing mapping if one exists
            if (settings.flagMappings.Any(x => x.Id.Equals(id)))
                settings.flagMappings.Remove(settings.flagMappings.First(x => x.Id.Equals(id)));
            if (name != "")
            {
                // Add new mapping
                settings.flagMappings.Add(new BitFlag() { Id = id, Name = name });
                // Re-order flagMappings in numerical order by flag ID
                settings.flagMappings = settings.flagMappings.OrderBy(x => x.Id).ToList();
            }
        }

        private void SetNewCountName(string name, int id)
        {
            // Remove existing mapping if one exists
            if (settings.countMappings.Any(x => x.Id.Equals(id)))
                settings.countMappings.Remove(settings.countMappings.First(x => x.Id.Equals(id)));
            if (name != "")
            {
                // Add new mapping
                settings.countMappings.Add(new BitFlag() { Id = id, Name = name });
                // Re-order countMappings in numerical order by count ID
                settings.countMappings = settings.countMappings.OrderBy(x => x.Id).ToList();
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

        private string GetMappedName(int flagId, List<BitFlag> list, bool useFlagSections = false)
        {
            string mappedName = "";
            if (useFlagSections)
                mappedName = GetFormattedFlag(flagId);
            else
                mappedName = flagId.ToString();

            if (list.Any(x => x.Id.Equals(flagId)))
            {
                BitFlag bitFlag = list.First(x => x.Id.Equals(flagId));
                mappedName += $" // {bitFlag.Name}";
            }

            return mappedName;
        }
    }
}
