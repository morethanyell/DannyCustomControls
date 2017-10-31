using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System;
using System.Threading;

namespace DannyCustomControls {

    /// <summary>
    /// An implementation of ListView with added one functionality
    /// to automatically layout DataTable onto the UI
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    public class DccListView : ListView {

        #region Variables and Objects

        /// <summary>
        /// Private field representing the datasource used in this context
        /// </summary>
        private DataTable _dtDataSource;

        /// <summary>
        /// Private field used to change the style of the column header
        /// </summary>
        private ColumnHeaderAutoResizeStyle _colHeadAutRezStyle;

        /// <summary>
        /// Default border color used on this rectangle
        /// </summary>
        public static Color DEF_BORDER_COLOR = Color.LightGray;

        /// <summary>
        /// Points to the event that is raised whenever the DataSource is changed
        /// </summary>
        public delegate void DataSourceChangedEventHandler (object sender, EventArgs e);

        /// <summary>
        /// Event raised whenever the DataSource is changed
        /// </summary>
        public event DataSourceChangedEventHandler DataSourceChanged;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public DccListView () {
            this._colHeadAutRezStyle = ColumnHeaderAutoResizeStyle.HeaderSize;
            this._dtDataSource = null;
        }

        /// <summary>
        /// Gets or Sets the ColumnHeaderAutoResizeStyle
        /// </summary>
        public ColumnHeaderAutoResizeStyle ColumnResizeStyle {
            get {
                return _colHeadAutRezStyle;
            }
            set {
                _colHeadAutRezStyle = value;
            }
        }

        /// <summary>
        /// Iterate through the param DataTable to display its items on this listview
        /// </summary>
        /// <param name="dt">The DataTable container of the items needed to be displayed</param>
        public void SetDataSource (DataTable dataSource) {

            if (this.IsDisposed) return;

            if (!this.IsHandleCreated) return;

            Thread t = new Thread(() => {

                try {

                    this.BeginInvoke((MethodInvoker)delegate () {

                        this.Clear();

                        this.View = View.Details;

                        this.Visible = false;

                        #region START OF ITERATION

                        if (dataSource != null) {

                            this._dtDataSource = dataSource.Copy() as DataTable;

                            foreach (DataColumn col in dataSource.Columns) this.Columns.Add(col.ColumnName);

                            ListViewItem lvi;

                            foreach (DataRow row in dataSource.Rows) {

                                lvi = new ListViewItem();

                                foreach (DataColumn col in dataSource.Columns) {

                                    if (row[col.ColumnName] != null && !Convert.IsDBNull(row[col.ColumnName]) &&
                                        !string.IsNullOrEmpty(Convert.ToString(row[col.ColumnName]))) {
                                        if (col.Ordinal == 0) lvi.Text = row[col.ColumnName].ToString();
                                        else lvi.SubItems.Add(row[col.ColumnName].ToString());
                                    } else {
                                        if (col.Ordinal == 0) lvi.Text = string.Empty;
                                        else lvi.SubItems.Add(string.Empty);
                                    }

                                }

                                this.Items.Add(lvi);
                            }
                        }

                        #endregion

                        this.Visible = true;

                        this.AutoResizeColumns(_colHeadAutRezStyle);

                        this.DataSourceChanged?.Invoke(this, EventArgs.Empty);

                    });

                } catch {
                    //supress error
                }

            }) { IsBackground = true };

            t.Start();

        }

        /// <summary>
        /// Returns the DataTable that's used on this context for display
        /// </summary>
        /// <returns>The DataTable displayed</returns>
        public DataTable GetDataSource () => _dtDataSource;

    }
}
