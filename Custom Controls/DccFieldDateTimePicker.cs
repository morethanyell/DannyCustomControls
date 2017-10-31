using System;
using System.Drawing;
using System.Windows.Forms;

namespace DannyCustomControls {

    /// <summary>
    /// A custom control that represents an input DateTimePicker pre-designed with label
    /// </summary>
    public partial class DccFieldDateTimePicker : UserControl {

        /// <summary>
        /// Creates an instance of PFieldTextBox
        /// </summary>
        public DccFieldDateTimePicker() => InitializeComponent();

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the BorderColor of the field
        /// </summary>
        public Color FieldBorderColor {
            get {
                return panInput.BorderColor;
            }
            set {
                panInput.BorderColor = value;
                panInput.Refresh();
            }
        }

        /// <summary>
        /// Private field that defines the field tip message
        /// </summary>
        private string _fieldTipMessage;

        /// <summary>
        /// Gets the tool tip message
        /// </summary>
        public string FieldTipMessage => _fieldTipMessage;

        /// <summary>
        /// Sets the tool tip message
        /// </summary>
        public void SetFieldTipMessage(string message) {
            _fieldTipMessage = message;
            this.mainTip.SetToolTip(this.tlpMain, message);
            this.mainTip.SetToolTip(this.HostedLabel, message);
            this.mainTip.SetToolTip(this.HostedDateTimePicker, message);
        }

        /// <summary>
        /// Gets or sets the FieldText
        /// </summary>
        public DateTime FieldValue {
            get {
                return HostedDateTimePicker.Value;
            }
            set {
                HostedDateTimePicker.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldName
        /// </summary>
        public string FieldLabel {
            get {
                return HostedLabel.Text;
            }
            set {
                HostedLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the Forecolor of the field
        /// </summary>
        public Color FieldForeColor {
            get {
                return HostedDateTimePicker.CalendarForeColor;
            }
            set {
                HostedDateTimePicker.CalendarForeColor = value;
            }
        }

        // <summary>
        /// Just changes the font style of label to bold
        /// </summary>
        public bool FieldIsRequired {
            get {
                return this.HostedLabel.Font.Bold;
            }
            set {
                if (value) {
                    this.HostedLabel.Font = new Font(HostedLabel.Font.Name, HostedLabel.Font.Size, FontStyle.Bold);
                } else {
                    this.HostedLabel.Font = new Font(HostedLabel.Font.Name, HostedLabel.Font.Size, FontStyle.Regular);
                }
            }
        }

        // <summary>
        /// Gets or sets the enableability of the hosted DateTimePicker
        /// </summary>
        public bool FieldIsReadOnly {
            get {
                return this.HostedDateTimePicker.Enabled;
            }
            set {
                this.HostedDateTimePicker.Enabled = !value;
            }
        }

        /// <summary>
        /// Set this field to the name of the SQL Field you it's binded to
        /// </summary>
        public string FieldSQLFieldNameBinding { get; set; }

        /// <summary>
        /// Refers to whether this field is a search field where value
        /// can be used in querying the database
        /// </summary>
        public bool FieldIsKeySearchColumn { get; set; }

        /// <summary>
        /// Gets or sets the control's enable method. Hides or shows the textbox, too
        /// </summary>
        public bool FieldIsEnabled {
            get {
                return this.Enabled;
            }
            set {
                this.Enabled = value;
                this.HostedDateTimePicker.Visible = value;
                this.Refresh();
            }
        }

        #endregion

        #region EVENTS

        /// <summary>
        /// Updates border
        /// </summary>
        private void dtpInput_Leave(object sender, EventArgs e) {
            this.panInput.BackColor = Color.White;
            this.panInput.BorderColor = Color.LightGray;
            this.panInput.Refresh();
        }

        /// <summary>
        /// Updates Border
        /// </summary>
        private void dtpInput_Enter(object sender, EventArgs e) {
            this.panInput.BorderColor = Color.DodgerBlue;
            this.panInput.BackColor = Color.White;
            this.panInput.Refresh();
        }

        #endregion


    }
}
