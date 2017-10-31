using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace DannyCustomControls {

    /// <summary>
    /// A custom control that represents an input ComboBox pre-designed with label
    /// </summary>
    public partial class DccFieldComboBox : UserControl {

        /// <summary>
        /// Holds the collection of items in this custom ComboBox
        /// </summary>
        private StringCollection _fieldItems;

        /// <summary>
        /// Creates an instance of PFieldTextBox
        /// </summary>
        public DccFieldComboBox() {
            InitializeComponent();
            this._fieldItems = new StringCollection();
        }

        /// <summary>
        /// Allows user to use CTRL+Backspace shortcut
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

            const int WM_KEYDOWN = 0x100;

            var keyCode = (Keys)(msg.WParam.ToInt32() &
                                  Convert.ToInt32(Keys.KeyCode));

            if ((msg.Msg == WM_KEYDOWN && keyCode == Keys.A)
                && (ModifierKeys == Keys.Control)
                && HostedComboBox.Focused) {
                HostedComboBox.SelectAll();
                return true;
            } else if (keyData == (Keys.Control | Keys.Back)) {
                SendKeys.SendWait("^+{LEFT}{BACKSPACE}");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Refreshes the hosted ComboBox everytime it's resized
        /// </summary>
        private void cboInput_Resize(object sender, EventArgs e) => this.HostedComboBox.Refresh();

        #region FIELD METHODS AND PROPERTIES

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
            this.mainTip.SetToolTip(this.HostedComboBox, message);
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
                panInput.Update();
            }
        }

        /// <summary>
        /// Gets or sets the items in the Combobox
        /// This came from StackOverflow. #Pirated
        /// </summary>
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection FieldItems {
            get {
                return _fieldItems;
            }
            set {
                _fieldItems = value;

                foreach (var item in _fieldItems) {
                    HostedComboBox.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Host ComboBox' DropDownStyle
        /// </summary>
        public ComboBoxStyle FieldDropDownStype {
            get {
                return this.HostedComboBox.DropDownStyle;
            }
            set {
                this.HostedComboBox.DropDownStyle = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldText - setting the text of the host ComboBox property
        /// "<see cref="ComboBox.Text"/>"
        /// </summary>
        public string FieldText {
            get {
                return HostedComboBox.Text;
            }
            set {
                HostedComboBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldLabel
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
        /// Just changes the font style of label to bold
        /// </summary>
        public bool FieldIsRequired {
            get {
                return HostedLabel.Font.Bold;
            }
            set {
                if (value) {
                    HostedLabel.Font = new Font(HostedLabel.Font.Name, HostedLabel.Font.Size, FontStyle.Bold);
                } else {
                    HostedLabel.Font = new Font(HostedLabel.Font.Name, HostedLabel.Font.Size, FontStyle.Regular);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Read Only property of the hosted ComboBox
        /// </summary>
        public bool FieldIsReadOnly {
            get {
                HostedComboBox.BackColor = Color.White;
                return HostedComboBox.ReadOnly;
            }
            set {
                HostedComboBox.ReadOnly = value;
                HostedComboBox.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Gets or sets the Forecolor of the field
        /// </summary>
        public Color FieldForeColor {
            get {
                return HostedComboBox.ForeColor;
            }
            set {
                HostedComboBox.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the FieldLabeForeColor of the field
        /// </summary>
        public Color FieldLabeForeColor {
            get {
                return HostedLabel.ForeColor;
            }
            set {
                HostedLabel.ForeColor = value;
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
        /// Gets or sets the control's Enabled method and hides/shows combobox, too
        /// </summary>
        public bool FieldIsEnabled {
            get {
                return this.Enabled;
            }
            set {
                this.HostedComboBox.Visible = value;
                this.Enabled = value;
                this.Refresh();
            }

        }

        #endregion

        #region EVENTS


        /// <summary>
        /// Updates the border color
        /// </summary>
        private void comboBox1_Enter(object sender, EventArgs e) {
            this.panInput.BackColor = Color.White;
            if (!this.FieldIsReadOnly) this.panInput.BorderColor = Color.DodgerBlue;
            this.panInput.Refresh();
        }

        /// <summary>
        /// Updates the border color
        /// </summary>
        private void comboBox1_Leave(object sender, EventArgs e) {
            if (!this.FieldIsReadOnly) {
                HostedComboBox.Text = HostedComboBox.Text.Trim();

                if (this.FieldRequiresTextBeforeLeaving) {

                    if (string.IsNullOrEmpty(this.HostedComboBox.Text) || this.HostedComboBox.SelectedIndex == -1) {
                        this.panInput.BorderColor = Color.Crimson;
                    } else {
                        this.panInput.BorderColor = Color.LightGray;
                    }

                } else {
                    this.panInput.BorderColor = Color.LightGray;
                }

                this.panInput.Refresh();
            }
        }

        #endregion


    }
}
