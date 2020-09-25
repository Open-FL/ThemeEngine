using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ThemeEngine.Script.Options;

namespace ThemeEngine.Forms
{
    public partial class StyleOptionsMenu : Form
    {

        static StyleOptionsMenu()
        {
            StyleManager.OnReload += StyleManagerOnOnReload;
        }
        private static readonly List<DataGridView> ActiveComponents = new List<DataGridView>();


        public static DataGridView CreateGridView()
        {
            DataGridViewTextBoxColumn OptionName = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn OptionValue = new DataGridViewTextBoxColumn();
            OptionName.HeaderText = "Option Name";
            OptionName.Name = "OptionName";
            OptionName.ReadOnly = true;
            OptionValue.HeaderText = "Option Value";
            OptionValue.Name = "OptionValue";

            DataGridView dgvOptions = new DataGridView();
            dgvOptions.AllowUserToAddRows = false;
            dgvOptions.AllowUserToDeleteRows = false;
            dgvOptions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOptions.Columns.AddRange(OptionName, OptionValue);
            dgvOptions.Dock = DockStyle.Fill;
            dgvOptions.Location = new System.Drawing.Point(0, 0);
            dgvOptions.MultiSelect = false;
            dgvOptions.Name = "dgvOptions";
            dgvOptions.Size = new System.Drawing.Size(690, 490);
            dgvOptions.TabIndex = 0;

            StyleManager.RegisterControl(dgvOptions, "style-options");

            AddView(dgvOptions);

            return dgvOptions;
        }

        private static void AddView(DataGridView dgvOptions)
        {
            dgvOptions.CellEndEdit += SaveGridInputValue;
            ActiveComponents.Add(dgvOptions);
            ReloadView(dgvOptions);
        }
        private static void SaveGridInputValue(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvOptions = (DataGridView)sender;
            string key = dgvOptions[e.ColumnIndex - 1, e.RowIndex].Value.ToString();
            StyleOption option = StyleManager.StyleOptions.FirstOrDefault(x => x.Keyword == key);
            if (option != null)
            {
                string newV = dgvOptions[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (newV != option.Value)
                {
                    option.Value = newV;
                    StyleManager.Reload();
                }
            }
        }
        private static void ReloadView(DataGridView dgvOptions)
        {
            dgvOptions.Rows.Clear();
            foreach (StyleOption styleOption in StyleManager.StyleOptions)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell name = new DataGridViewTextBoxCell { Value = styleOption.Keyword };
                DataGridViewTextBoxCell value = new DataGridViewTextBoxCell { Value = styleOption.Value };
                row.Cells.Add(name);
                row.Cells.Add(value);
                dgvOptions.Rows.Add(styleOption.Keyword, styleOption.Value);
            }
        }

        private static void StyleManagerOnOnReload()
        {
            for (int i = ActiveComponents.Count - 1; i >= 0; i--)
            {
                DataGridView dgvOptions = ActiveComponents[i];
                if (dgvOptions == null || dgvOptions.IsDisposed)
                {
                    ActiveComponents.RemoveAt(i);
                }
                else
                {
                    ReloadView(dgvOptions);
                }
            }
        }

        public StyleOptionsMenu()
        {
            InitializeComponent();
            StyleManager.RegisterControls(this, "style-options");

            AddView(dgvOptions);
        }
        
    }
}