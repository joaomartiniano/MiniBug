using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniBug
{
    public partial class ConfigureViewForm : Form
    {
        public ConfigureViewForm()
        {
            InitializeComponent();
        }

        private void ConfigureViewForm_Load(object sender, EventArgs e)
        {
            // Suspend the layout logic for the issues DataGridView, while initializing
           GridIssues.SuspendLayout();

            // Initialization of the Issues and Tasks grids
            InitializeGridIssues();
            //InitializeGridTasks();

            // Populate the Issues and Tasks grids
            PopulateGridIssues();
            //PopulateGridTasks();

            // Resume the layout logic
            GridIssues.ResumeLayout();
        }

        #region "Issues"
        /// <summary>
        /// Initialize the issues DataGridView.
        /// </summary>
        private void InitializeGridIssues()
        {
            GridIssues.BorderStyle = BorderStyle.None;
            GridIssues.Dock = DockStyle.Fill;

            GridIssues.ReadOnly = false;
            GridIssues.Enabled = true;
            GridIssues.AllowUserToAddRows = false;
            GridIssues.AllowUserToDeleteRows = false;
            GridIssues.AllowUserToOrderColumns = false;
            GridIssues.AllowUserToResizeColumns = false;
            GridIssues.AllowUserToResizeRows = false;
            GridIssues.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            GridIssues.ColumnHeadersVisible = true;
            GridIssues.RowHeadersVisible = false;
            GridIssues.SelectionMode = DataGridViewSelectionMode.CellSelect;
            GridIssues.MultiSelect = false;
            GridIssues.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            GridIssues.ShowCellToolTips = false;
            
            // Colors of selected cells
            GridIssues.DefaultCellStyle.SelectionBackColor = GridIssues.DefaultCellStyle.BackColor;
            GridIssues.DefaultCellStyle.SelectionForeColor = GridIssues.DefaultCellStyle.ForeColor;

            // Name
            GridIssues.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "name",
                HeaderText = "Name",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 90,
                ReadOnly = true
            });

            // Visible
            GridIssues.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "visible",
                HeaderText = "Visible",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = false,
                ThreeState = false
            });
            GridIssues.Columns["visible"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Sort by first field
            GridIssues.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "sort1",
                HeaderText = "Sort By",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = false,
                ThreeState = false
            });
            GridIssues.Columns["sort1"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Sort by second field
            GridIssues.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "sort2",
                HeaderText = "And By",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                ReadOnly = false,
                ThreeState = false
            });
            GridIssues.Columns["sort2"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Sort order
            GridIssues.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = "sortOrder",
                HeaderText = "Sort Order",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                FlatStyle = FlatStyle.Flat,
                DataSource = new string[] { "Ascending", "Descending" },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });            
            GridIssues.Columns["sortOrder"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Description
            GridIssues.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "description",
                HeaderText = "Description",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            });

            GridIssues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Populate the issues DataGridView.
        /// </summary>
        private void PopulateGridIssues()
        {
            int i = 0;

            foreach (KeyValuePair<IssueFieldsUI, GridColumn> item in ApplicationSettings.GridIssuesColumns)
            {
                i = GridIssues.Rows.Add(new object[] {
                    (item.Key == IssueFieldsUI.Priority) ? "Priority" : item.Value.HeaderText,
                    item.Value.Visible,
                    (ApplicationSettings.GridIssuesSort.FirstColumn == item.Key) ? true : false,
                    (ApplicationSettings.GridIssuesSort.SecondColumn == item.Key) ? true : false,
                    string.Empty,
                    item.Value.Description
                });

                // Sort order
                if (ApplicationSettings.GridIssuesSort.FirstColumn == item.Key)
                {
                    GridIssues["sortOrder", i].Value = (ApplicationSettings.GridIssuesSort.FirstColumnSortOrder == SortOrder.Ascending) ? "Ascending" : "Descending";
                }
                else if (ApplicationSettings.GridIssuesSort.SecondColumn == item.Key)
                {
                    if (ApplicationSettings.GridIssuesSort.SecondColumnSortOrder != null)
                    {
                        GridIssues["sortOrder", i].Value = (ApplicationSettings.GridIssuesSort.SecondColumnSortOrder == SortOrder.Ascending) ? "Ascending" : "Descending";
                    }
                }

                GridIssues.Rows[i].Tag = (int)item.Key;

                // Disable the sort checkboxes if the column is not visible
                if (!item.Value.Visible)
                {
                    GridIssues["sort1", i].ReadOnly = true;
                    GridIssues["sort2", i].ReadOnly = true;
                }

                // Disable the sort order combobox if the column is not visible
                if ((!item.Value.Visible) || ((ApplicationSettings.GridIssuesSort.FirstColumn != item.Key) && (ApplicationSettings.GridIssuesSort.SecondColumn != item.Key)))
                {
                    GridIssues["sortOrder", i].ReadOnly = true;
                }
            }

            // Disable the visibility checkbox for the ID column
            GridIssues["visible", 0].ReadOnly = true;

            GridIssues.Columns["sortOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        /// <summary>
        /// Advanced formatting of cells in the issues DataGridView.
        /// </summary>
        private void GridIssues_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Disable the Visible column for the ID field
            if ((e.ColumnIndex == GridIssues.Columns["visible"].Index) && (e.RowIndex == 0))
            {
                e.PaintBackground(e.CellBounds, true);
                Rectangle r = e.CellBounds;
                r.Width = 15;
                r.Height = 15;
                r.X += (e.CellBounds.Width / 2) - (r.Width / 2) - 1;
                r.Y += (e.CellBounds.Height / 2) - (r.Height / 2) - 1;
                ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Inactive | ButtonState.Flat | ButtonState.Checked);
                e.Handled = true;
            }

            if (((e.ColumnIndex == GridIssues.Columns["sort1"].Index) || (e.ColumnIndex == GridIssues.Columns["sort2"].Index)) &&
            (e.RowIndex > -1) &&
            (e.RowIndex != GridIssues.NewRowIndex))
            {
                if (e.State.HasFlag(DataGridViewElementStates.ReadOnly))
                {
                    e.PaintBackground(e.CellBounds, true);
                    Rectangle r = e.CellBounds;
                    r.Width = 15;
                    r.Height = 15;
                    r.X += (e.CellBounds.Width / 2) - (r.Width / 2) - 1;
                    r.Y += (e.CellBounds.Height / 2) - (r.Height / 2) - 1;
                    ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Inactive | ButtonState.Flat);
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Handle clicks on the checkboxes of the issues DataGridView.
        /// </summary>
        private void GridIssues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool FlagClickSort = false;

            // Control the visibility of columns (the visibility of the ID column cannot be modified)
            if ((e.ColumnIndex == GridIssues.Columns["visible"].Index) && (e.RowIndex > 0))
            {
                // If the user hides a column, then uncheck the sort checkboxes and disable them
                if (!Boolean.Parse(GridIssues["visible", e.RowIndex].EditedFormattedValue.ToString()))
                {
                    GridIssues["sort1", e.RowIndex].Value = false;
                    GridIssues["sort1", e.RowIndex].ReadOnly = true;
                    GridIssues["sort2", e.RowIndex].Value = false;
                    GridIssues["sort2", e.RowIndex].ReadOnly = true;
                    GridIssues["sortOrder", e.RowIndex].Value = string.Empty;
                    GridIssues["sortOrder", e.RowIndex].ReadOnly = true;
                }
                else
                {
                    // Re-enable the sort checkboxes because the column is visible again
                    GridIssues["sort1", e.RowIndex].ReadOnly = false;
                    GridIssues["sort2", e.RowIndex].ReadOnly = false;
                }

                GridIssues.Refresh();
            }
            else if ((e.ColumnIndex == GridIssues.Columns["sort1"].Index) && (e.RowIndex != -1))
            {
                // Only change the controls state if the cell is not readonly
                if (!GridIssues["sort1", e.RowIndex].ReadOnly)
                {
                    if (Boolean.Parse(GridIssues["sort1", e.RowIndex].EditedFormattedValue.ToString()))
                    {
                        GridIssues["sort2", e.RowIndex].Value = false;

                        // Uncheck any other fields in the same column
                        for (int i = 0; i < GridIssues.Rows.Count; ++i)
                        {
                            if (i != e.RowIndex)
                            {
                                if (Boolean.Parse(GridIssues["sort1", i].Value.ToString()))
                                {
                                    GridIssues["sort1", i].Value = false;
                                    GridIssues["sortOrder", i].Value = string.Empty;
                                }
                            }
                        }

                        // Enable the sort order combobox
                        GridIssues["sortOrder", e.RowIndex].ReadOnly = false;
                        GridIssues["sortOrder", e.RowIndex].Value = "Ascending";
                    }

                    // Signal that the user clicked on one of the sort checkboxes
                    FlagClickSort = true;
                }
            }
            else if ((e.ColumnIndex == GridIssues.Columns["sort2"].Index) && (e.RowIndex != -1))
            {
                // Only change the controls state if the cell is not readonly
                if (!GridIssues["sort2", e.RowIndex].ReadOnly)
                {
                    if (Boolean.Parse(GridIssues["visible", e.RowIndex].EditedFormattedValue.ToString()))
                    {
                        GridIssues["sort1", e.RowIndex].Value = false;

                        // Uncheck any other fields in the same column
                        for (int i = 0; i < GridIssues.Rows.Count; ++i)
                        {
                            if (i != e.RowIndex)
                            {
                                if (Boolean.Parse(GridIssues["sort2", i].Value.ToString()))
                                {
                                    GridIssues["sort2", i].Value = false;
                                    GridIssues["sortOrder", i].Value = string.Empty;
                                }
                            }
                        }

                        // Enable the sort order combobox
                        GridIssues["sortOrder", e.RowIndex].ReadOnly = false;
                        GridIssues["sortOrder", e.RowIndex].Value = "Ascending";
                    }

                    // Signal that the user clicked on one of the sort checkboxes
                    FlagClickSort = true;
                }
            }

            // If the user unchecked both sort checkboxes
            if ((FlagClickSort) &&
                ((!Boolean.Parse(GridIssues["sort1", e.RowIndex].EditedFormattedValue.ToString())) &&
                (!Boolean.Parse(GridIssues["sort2", e.RowIndex].EditedFormattedValue.ToString()))))
            {
                GridIssues["sortOrder", e.RowIndex].ReadOnly = true;
                GridIssues["sortOrder", e.RowIndex].Value = string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// Persist the changes made and close this form.
        /// </summary>
        private void btOK_Click(object sender, EventArgs e)
        {
            bool FlagSort1 = true, FlagSort2 = true;

            // Traverse the DataGridView rows, checking the checkboxes
            for (int i = 0; i < GridIssues.Rows.Count; ++i)
            {
                ApplicationSettings.GridIssuesColumns[(IssueFieldsUI)GridIssues.Rows[i].Tag].Visible = Boolean.Parse(GridIssues["visible", i].Value.ToString());

                if ((FlagSort1) && (Boolean.Parse(GridIssues["sort1", i].Value.ToString())))
                {
                    ApplicationSettings.GridIssuesSort.FirstColumn = (IssueFieldsUI)GridIssues.Rows[i].Tag;
                    ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = (GridIssues["sortOrder", i].Value.ToString() == "Ascending") ? SortOrder.Ascending : SortOrder.Descending;
                    FlagSort1 = false;
                }

                if ((FlagSort2) && (Boolean.Parse(GridIssues["sort2", i].Value.ToString())))
                {
                    ApplicationSettings.GridIssuesSort.SecondColumn = (IssueFieldsUI)GridIssues.Rows[i].Tag;
                    ApplicationSettings.GridIssuesSort.SecondColumnSortOrder = (GridIssues["sortOrder", i].Value.ToString() == "Ascending") ? SortOrder.Ascending : SortOrder.Descending;
                    FlagSort2 = false;
                }
            }

            // Set defaults if the user did not choose the first column to sort
            if (FlagSort1)
            {
                ApplicationSettings.GridIssuesSort.FirstColumn = IssueFieldsUI.ID;
                ApplicationSettings.GridIssuesSort.FirstColumnSortOrder = SortOrder.Ascending;
            }

            // Set defaults if the user did not choose the second column to sort
            if (FlagSort2)
            {
                ApplicationSettings.GridIssuesSort.SecondColumn = null;
                ApplicationSettings.GridIssuesSort.SecondColumnSortOrder = null;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Close this form.
        /// </summary>
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
