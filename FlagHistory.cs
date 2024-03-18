using MetroSet_UI.Forms;
using P5RFlagComparer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static P5RFlagComparer.MainForm;

namespace P5RFlagComparer
{
    public partial class FlagHistory : MetroSetForm
    {
        List<Comparison> Comparisons;
        List<BitFlag> FlagMappings;
        List<BitFlag> CountMappings;
        bool useSectionName;

        public int FlagFormat { get; private set; }

        public FlagHistory(MainForm.Settings settings)
        {
            InitializeComponent();
            useSectionName = true;
            Comparisons = settings.comparisons;

            PopulateFlagMappings(settings.flagMappings);
            PopulateCountMappings(settings.countMappings);

            UpdateFlagMappingsList();
            UpdateCountMappingsList();
        }

        private List<BitFlag> PopulateCountMappings(List<BitFlag> countMappings)
        {
            CountMappings = new List<BitFlag>();
            for (int i = 0; i < 256; i++)
            {
                if (countMappings.Any(x => x.Id == i))
                    FlagMappings.Add(countMappings.First(x => x.Id == i));
                else
                    FlagMappings.Add(new BitFlag() { Id = i });
            }
            return CountMappings;
        }

        private List<BitFlag> PopulateFlagMappings(List<BitFlag> flagMappings)
        {
            FlagMappings = new List<BitFlag>();
            for (int i = 0; i < 12800; i++)
            {
                if (flagMappings.Any(x => x.Id == i))
                    FlagMappings.Add(flagMappings.First(x => x.Id == i));
                else
                    FlagMappings.Add(new BitFlag() { Id = i });
            }
            return FlagMappings;
        }

        private void UpdateFlagMappingsList()
        {
            BindingSource bs_FlagMappings = new BindingSource();
            bs_FlagMappings.DataSource = FlagMappings;
            listBox_FlagMappings.DataSource = bs_FlagMappings;
            listBox_FlagMappings.FormattingEnabled = true;
            listBox_FlagMappings.Format += MappedFlagFormat;
        }

        private void UpdateCountMappingsList()
        {
            BindingSource bs_CountMappings = new BindingSource();
            bs_CountMappings.DataSource = CountMappings;
            listBox_CountMappings.DataSource = bs_CountMappings;
            listBox_CountMappings.FormattingEnabled = true;
            listBox_CountMappings.Format += MappedCountFormat;
        }

        private object GetFormattedFlag(int id)
        {
            string name = id.ToString();

            if (useSectionName)
            {
                for (int i = 1; i < Flag.sRoyalBits.Length; i++)
                {
                    if (id < Flag.sRoyalBits[i])
                    {
                        int section = i - 1;
                        name = $"Flag.Section{section} + {id - Flag.sRoyalBits[i - 1]}";
                        break;
                    }
                }
            }

            return name;
        }

        private object GetMappedName(int id, List<BitFlag> list)
        {
            if (list.Any(x => x.Id.Equals(id)))
                return list.First(x => x.Id.Equals(id)).Name;

            return "";
        }

        private void MappedFlagFormat(object sender, ListControlConvertEventArgs e)
        {
            var flag = (BitFlag)e.ListItem;

            e.Value = GetFormattedFlag(flag.Id);
            if (!string.IsNullOrEmpty(flag.Name))
            {
                e.Value += $" // {GetMappedName(flag.Id, FlagMappings)}";
            }
        }

        private void MappedCountFormat(object sender, ListControlConvertEventArgs e)
        {
            var flag = (BitFlag)e.ListItem;

            e.Value = flag.Id;
            if (!string.IsNullOrEmpty(flag.Name))
            {
                e.Value += $" // {GetMappedName(flag.Id, CountMappings)}";
            }
        }

        private void SelectedFlag_Changed(object sender, EventArgs e)
        {
            BitFlag flag = (BitFlag)sender;
            listBox_History.Items.Clear();
            for (int i = 0; i < Comparisons.Count; i++)
            {
                if (Comparisons[i].EnabledFlags.Any(x => x.Id == flag.Id))
                    listBox_History.Items.Add($"[ENABLED] {Comparisons[i].Name}");
                else
                {
                    Comparison previousComparison = null;
                    if (i > 0)
                        previousComparison = Comparisons[i - 1];
                    var unsetCounts = previousComparison.EnabledFlags.Where(x => !Comparisons[i].EnabledFlags.Any(y => y.Id.Equals(x.Id))).ToList();

                    if (unsetCounts.Any(x => x.Id == flag.Id))
                        listBox_History.Items.Add($"[DISABLED] {Comparisons[i].Name}");
                }
            }
        }

        private void SelectedCount_Changed(object sender, EventArgs e)
        {
            BitFlag flag = (BitFlag)sender;
            listBox_History.Items.Clear();
            for (int i = 0; i < Comparisons.Count; i++)
            {
                Comparison previousComparison = null;
                if (i > 0)
                    previousComparison = Comparisons[i - 1];

                List<BitFlag> setCounts = Comparisons[i].SetCounts.Where(x => !previousComparison.SetCounts.Any(y => y.Id.Equals(x.Id))
                    || previousComparison.SetCounts.Single(y => y.Id.Equals(x.Id)).Value != x.Value).ToList();

                if (setCounts.Any(x => x.Id == flag.Id))
                    listBox_History.Items.Add($"[SET] {Comparisons[i].Name}");
                else
                {
                    var unsetCounts = previousComparison.SetCounts.Where(x => !Comparisons[i].SetCounts.Any(y => y.Id.Equals(x.Id))).ToList();

                    if (unsetCounts.Any(x => x.Id == flag.Id))
                        listBox_History.Items.Add($"[UNSET] {Comparisons[i].Name}");
                }
            }
        }
    }
}
