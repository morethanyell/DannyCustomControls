using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace DannyCustomControls {

    /// <summary>
    /// A custom control that represents an input textbox pre-designed with label
    /// </summary>
    public partial class DccFieldTextBox : UserControl {

        /// <summary>
        /// Tool tip messages used in this context
        /// </summary>
        private ToolTip tip;

        /// <summary>
        /// Creates an instance of PFieldTextBox
        /// </summary>
        public DccFieldTextBox () => InitializeComponent();

        /// <summary>
        /// Allows user to use CTRL+Backspace shortcut
        /// </summary>
        protected override bool ProcessCmdKey (ref Message msg, Keys keyData) {

            const int WM_KEYDOWN = 0x100;

            var keyCode = (Keys)(msg.WParam.ToInt32() &
                                  Convert.ToInt32(Keys.KeyCode));

            if ((msg.Msg == WM_KEYDOWN && keyCode == Keys.A)
                && (ModifierKeys == Keys.Control)
                && HostedTextBox.Focused) {
                HostedTextBox.SelectAll();
                return true;
            } else if (keyData == (Keys.Control | Keys.Back)) {
                SendKeys.SendWait("^+{LEFT}{BACKSPACE}");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the maximum character the HostedTextBox can accept
        /// </summary>
        public int FieldMaxChar {
            get {
                return HostedTextBox.MaxLength;
            }
            set {
                HostedTextBox.MaxLength = value;
            }
        }

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
        public void SetFieldTipMessage (string message) {
            _fieldTipMessage = message;
            this.mainTip.SetToolTip(this.tlpMain, message);
            this.mainTip.SetToolTip(this.HostedLabel, message);
            this.mainTip.SetToolTip(this.HostedTextBox, message);
        }

        /// <summary>
        /// Gets or sets the FieldText
        /// </summary>
        public string FieldText {
            get {
                return HostedTextBox.Text;
            }
            set {
                HostedTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldCueBanner
        /// </summary>
        public string FieldCue {
            get {
                return HostedTextBox.Cue;
            }
            set {
                HostedTextBox.Cue = value;
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
        /// Gets or sets the Read-only property of the
        /// textbox / field
        /// </summary>
        public bool FieldIsReadOnly {
            get {
                HostedTextBox.BackColor = Color.White;
                return HostedTextBox.ReadOnly;
            }
            set {
                HostedTextBox.ReadOnly = value;
                HostedTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Gets or sets the Forecolor of the field
        /// </summary>
        public Color FieldForeColor {
            get {
                return HostedTextBox.ForeColor;
            }
            set {
                HostedTextBox.ForeColor = value;
            }
        }

        // <summary>
        /// Just changes the font style of label to bold
        /// </summary>
        public bool FieldIsRequired {
            get {
                return HostedLabel.Font.Bold;
            }
            set {
                if (value) {
                    HostedLabel.Font = new Font(HostedLabel.Font, FontStyle.Bold);
                } else {
                    HostedLabel.Font = new Font(HostedLabel.Font, FontStyle.Regular);
                }
            }
        }

        /// <summary>
        /// When set to true, the textfield's border will
        /// highlight Red if the textfield does not contain
        /// any value
        /// </summary>
        public bool FieldRequiresTextBeforeLeaving { get; set; }

        /// <summary>
        /// Gets or sets the textfield property to validate if the value is a number
        /// </summary>
        public bool FieldIsNumberOnly { get; set; }

        /// <summary>
        /// Gets or sets the textfield property to validate if the value is a date
        /// </summary>
        public bool FieldIsDateOnly { get; set; }

        /// <summary>
        /// Gets or sets this field to the name of the SQL Field you it's binded to
        /// </summary>
        public string FieldSQLFieldNameBinding { get; set; }

        /// <summary>
        /// Refers to whether this field is a search field where value
        /// can be used in querying the database
        /// </summary>
        public bool FieldIsKeySearchColumn { get; set; }

        /// <summary>
        /// Gets or sets the message displayed on the textbox when a special character is detected.
        /// Note: Message will only be shown if property <see cref="FieldValidatesSpecialCharacters"/> is set to <b>true</b>
        /// </summary>
        public string FieldSpecialCharactersDetectedMessage { get; set; }

        /// <summary>
        /// Gets or sets the property as to wheter this field prompts the user if a
        /// special character is used
        /// </summary>
        public bool FieldValidatesSpecialCharacters { get; set; }

        /// <summary>
        /// Gets or sets the control's enable method. Hides or shows the textbox, too
        /// </summary>
        public bool FieldIsEnabled {
            get {
                return this.Enabled;
            }
            set {
                this.Enabled = value;
                this.HostedTextBox.Visible = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// List of suggestions as user types onto the textbox
        /// </summary>
        public AutoCompleteStringCollection FieldSuggestions {
            get {
                return this.HostedTextBox.AutoCompleteCustomSource;
            }
            set {
                this.HostedTextBox.AutoCompleteCustomSource = value;
            }
        }

        /// <summary>
        /// Hides the property Text
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Deprecated", true)]
        public new string Text { get; set; }

        #endregion

        #region EVENTS

        /// <summary>
        /// Updates border
        /// </summary>
        private void txtInput_Leave (object sender, EventArgs e) {

            if (!this.FieldIsReadOnly) {

                Color p;

                this.HostedTextBox.Text = HostedTextBox.Text.Trim();

                if (this.FieldRequiresTextBeforeLeaving) {
                    if (string.IsNullOrEmpty(this.HostedTextBox.Text)) {
                        p = Color.Crimson;
                    } else {
                        p = Color.LightGray;
                    }
                } else {
                    p = Color.LightGray;
                }

                if (this.FieldValidatesSpecialCharacters) {

                    var m = Regex.Match(this.HostedTextBox.Text, @"^[a-zA-Z0-9\x20]+$");

                    if (!m.Success) {

                        p = Color.DarkOrange;

                        if (tip != null) {
                            tip.Hide(this.HostedTextBox);
                            tip.Dispose();
                            tip = null;
                        }

                        tip = new ToolTip() {
                            ToolTipTitle = this.FieldLabel,
                            ToolTipIcon = ToolTipIcon.Error
                        };

                        tip.Show(this.FieldSpecialCharactersDetectedMessage, this.HostedTextBox, this.HostedTextBox.Width - 10, -50, 2300);

                    }

                }

                if (this.FieldIsNumberOnly) {

                    if (!double.TryParse(this.HostedTextBox.Text, out double parser)) {

                        p = Color.Crimson;

                        if (tip != null) {
                            tip.Hide(this.HostedTextBox);
                            tip.Dispose();
                            tip = null;
                        }

                        tip = new ToolTip() {
                            ToolTipTitle = this.FieldLabel,
                            ToolTipIcon = ToolTipIcon.Error
                        };

                        tip.Show("Not a valid Number!", this.HostedTextBox, this.HostedTextBox.Width - 10, -50, 2300);

                    }

                }

                if (this.FieldIsDateOnly) {

                    if (!DateTime.TryParse(HostedTextBox.Text, out DateTime parse_isfielddate)) {
                        if (tip != null) {
                            tip.Hide(this.HostedTextBox);
                            tip.Dispose();
                            tip = null;
                        }

                        tip = new ToolTip() {
                            ToolTipTitle = this.FieldLabel,
                            ToolTipIcon = ToolTipIcon.Error
                        };

                        tip.Show("Not a valid Date!", this.HostedTextBox, this.HostedTextBox.Width - 10, -50, 2300);
                    }
                }

                panInput.BackColor = Color.White;

                panInput.BorderColor = p;

                panInput.Refresh();

            }
        }

        /// <summary>
        /// Updates Border
        /// </summary>
        private void txtInput_Enter (object sender, EventArgs e) {
            this.panInput.BackColor = Color.White;
            if (!this.FieldIsReadOnly) this.panInput.BorderColor = Color.DodgerBlue;
            this.panInput.Refresh();
        }

        #endregion



    }
}
