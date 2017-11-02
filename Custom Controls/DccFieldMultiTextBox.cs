using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DannyCustomControls {

    /// <summary>
    /// A custom control that represents an input multiline-textbox pre-designed with label
    /// </summary>
    public partial class DccFieldMultiTextBox : UserControl {

        /// <summary>
        /// Creates an instance of PFieldTextBox
        /// </summary>
        public DccFieldMultiTextBox() => InitializeComponent();

        /// <summary>
        /// Allows user to use CTRL+Backspace shortcut
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

            const int WM_KEYDOWN = 0x100;

            var keyCode = (Keys)(msg.WParam.ToInt32() &
                                  Convert.ToInt32(Keys.KeyCode));

            if ((msg.Msg == WM_KEYDOWN && keyCode == Keys.A)
                && (ModifierKeys == Keys.Control)
                && HostedMultilineTextBox.Focused) {
                HostedMultilineTextBox.SelectAll();
                return true;
            } else if (keyData == (Keys.Control | Keys.Back)) {
                SendKeys.SendWait("^+{LEFT}{BACKSPACE}");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Alternative constructor function
        /// </summary>
        public DccFieldMultiTextBox(string field_name) {
            InitializeComponent();

            this.FieldLabel = field_name;
        }

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
                panInput.Update();
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
            this.mainTip.SetToolTip(this.HostedMultilineTextBox, message);
        }

        /// <summary>
        /// Gets or sets the maximum character the HostedTextBox can accept
        /// </summary>
        public int FieldMaxChar {
            get {
                return HostedMultilineTextBox.MaxLength;
            }
            set {
                HostedMultilineTextBox.MaxLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldText
        /// </summary>
        public string FieldText {
            get {
                return HostedMultilineTextBox.Text;
            }
            set {
                HostedMultilineTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldCueBanner
        /// </summary>
        public string FieldCue {
            get {
                return HostedMultilineTextBox.Cue;
            }
            set {
                HostedMultilineTextBox.Cue = value;
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
                HostedMultilineTextBox.BackColor = Color.White;
                return HostedMultilineTextBox.ReadOnly;
            }
            set {
                HostedMultilineTextBox.ReadOnly = value;
                HostedMultilineTextBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Gets or sets the Forecolor of the field
        /// </summary>
        public Color FieldForeColor {
            get {
                return HostedMultilineTextBox.ForeColor;
            }
            set {
                HostedMultilineTextBox.ForeColor = value;
            }
        }

        /// <summary>
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
        /// Set this field to the name of the SQL Field you it's binded to
        /// </summary>
        public string FieldSQLFieldNameBinding { get; set; }

        /// <summary>
        /// Gets or sets the control's enable method. Hides or shows the textbox, too
        /// </summary>
        public bool FieldIsEnabled {
            get {
                return this.Enabled;
            }
            set {
                this.Enabled = value;
                this.HostedMultilineTextBox.Visible = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the lines in this field multiline textbox
        /// </summary>
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public string[] FieldLines {
            get {
                return HostedMultilineTextBox.Lines;
            }
            set {
                HostedMultilineTextBox.Lines = value;
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
        /// Updates border color
        /// </summary>
        private void txtInput_Leave(object sender, EventArgs e) {
            if (!this.FieldIsReadOnly) {

                HostedMultilineTextBox.Text = HostedMultilineTextBox.Text.Trim();

                if (this.FieldRequiresTextBeforeLeaving) {

                    if (string.IsNullOrEmpty(this.HostedMultilineTextBox.Text)) {
                        panInput.BorderColor = Color.Crimson;
                    } else {
                        panInput.BorderColor = Color.LightGray;
                    }

                } else {
                    panInput.BorderColor = Color.LightGray;
                }

                panInput.Refresh();
            }
        }

        /// <summary>
        /// Updates border color
        /// </summary>
        private void txtInput_Enter(object sender, EventArgs e) {
            this.panInput.BackColor = Color.White;
            if (!this.FieldIsReadOnly) this.panInput.BorderColor = Color.DodgerBlue;
            this.panInput.Refresh();
        }

        #endregion





    }
}
