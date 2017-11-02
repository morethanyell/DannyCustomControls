/*
 * Copyright 2017. Daniel L. Astillero
 * 
 * 
 * */


using System;
using System.Drawing;
using System.Windows.Forms;
using static DannyCustomControls.DccEnumerations;
using static DannyCustomControls.Helper_Classes.DccSpecialUnicodeCharacters;
using System.ComponentModel;

namespace DannyCustomControls {

    /// <summary>
    /// A custom TextBox with built-in button
    /// </summary>
    public partial class DccActionTextBox : UserControl {

        /// <summary>
        /// Private field representing the <see cref="SubmitType"/>
        /// </summary>
        private DccActionTextBoxSumbitTypes _submitType;

        /// <summary>
        /// Gets or sets the <see cref="DccActionTextBox.PActionTextBoxSumbitTypes"/> of the HostedButton
        /// </summary>
        public DccActionTextBoxSumbitTypes SubmitType {
            get { return this._submitType; }
            set {
                this._submitType = value;
                this.SetHostedButtonIcon(this._submitType);
                this.Invalidate(true);
            }
        }

        /// <summary>
        /// Points to the method called when event HostedButtonClicked is raised
        /// </summary>
        public delegate void HostedButtonClickedEventHandler(object sender, DccActionTextBoxHostedButtonClickedEventArgs e);

        /// <summary>
        /// Is raised whenever the actual <see cref="HostedButton"/> is clicked (a redundancy)
        /// </summary>
        public event HostedButtonClickedEventHandler HostedButtonClicked;

        /// <summary>
        /// Points to the method called when event the enter key is pressed in the hosted TextBox
        /// </summary>
        public delegate void EnterIsPressedEventArgs(object sender, EventArgs e);

        /// <summary>
        /// Is raised when event the enter key is pressed in the hosted TextBox
        /// </summary>
        public event EnterIsPressedEventArgs EnterKeyDetected;

        /// <summary>
        /// Tooltip used privately during validation
        /// </summary>
        private ToolTip _tip;

        /// <summary>
        /// Creates an instance of the APSTextBox
        /// </summary>
        public DccActionTextBox() => InitializeComponent();

        /// <summary>
        /// Allows CTRL+Backspace and Select All
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

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
        /// Gets or sets the HostedButton's color
        /// </summary>
        public Color HostedButtonColor {
            get { return this.HostedButton.BackColor; }
            set {
                this.HostedButton.BackColor = value;
                this.Invalidate(true);
            }
        }

        /// <summary>
        /// Points to the size of the column that contains the HostedButton
        /// </summary>
        private float _hostedButtonColumnWidth => this.tlpMain.ColumnStyles[1].Width;

        /// <summary>
        /// Gets or sets the HostedButton's absolute width
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Will throw an exception if the value
        /// is set more than the width of the parent control</exception>
        public float HostedButtonAbsoluteWidth {
            get {
                return _hostedButtonColumnWidth;
            }
            set {
                if (value > this.Width)
                    throw new ArgumentOutOfRangeException("Width value set for HostedTextBox should be at least 99% of the parent's width");
                this.tlpMain.ColumnStyles[1].Width = value;
                this.tlpMain.Invalidate(true);
            }
        }

        /// <summary>
        /// Determines whethere the HostedButton launches an <see cref="OpenFileDialog"/>
        /// </summary>
        public bool HostedButtonLaunchesOfd { get; set; }

        /// <summary>
        /// Gets or sets the hosted TextBox's Font
        /// </summary>
        public int HostedTextBoxMaxChar {
            get {
                return this.HostedTextBox.MaxLength;
            }
            set {
                this.HostedTextBox.MaxLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the hosted TextBox's Font
        /// </summary>
        public Font HostedTextBoxFont {
            get {
                return this.HostedTextBox.Font;
            }
            set {
                this.HostedTextBox.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets the hosted TextBox's Text
        /// </summary>
        public string HostedTextBoxText {
            get {
                return this.HostedTextBox.Text;
            }
            set {
                this.HostedTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the hosted TextBox's Cue Banner
        /// </summary>
        public string HostedTextBoxCue {
            get {
                return this.HostedTextBox.Cue;
            }
            set {
                this.HostedTextBox.Cue = value;
            }
        }

        /// <summary>
        /// Gets or sets the hosted TextBox's Read Only property
        /// </summary>
        public bool HostedTextBoxReadOnly {
            get {
                return this.HostedTextBox.ReadOnly;
            }
            set {
                this.HostedTextBox.ReadOnly = value;
            }
        }

        /// <summary>
        /// Changes the background color of the whole custom control, including
        /// the controls contained in it, except the HostedTextBox
        /// </summary>
        public new Color BackColor {
            get {
                return base.BackColor;
            }
            set {
                this.SetControlBackColor(value);
                this.Invalidate(true);
            }
        }

        /// <summary>
        /// Acts as the default border color for when the control is focused until changed
        /// </summary>
        public Color _borderColFoc = Color.DodgerBlue;

        /// <summary>
        /// Acts as the default border color for when the control is not focused until changed
        /// </summary>
        public Color _borderColNotFoc = Color.LightGray;

        /// <summary>
        /// Gets or sets the color of the border when this custom control is focused
        /// </summary>
        public Color BorderColorWhenFocused {
            get {
                return this._borderColFoc;
            }
            set {
                this._borderColFoc = value;
                this.Invalidate(true);
            }
        }

        /// <summary>
        /// Gets or sets the color of the border when this custom control is not focused
        /// </summary>
        public Color BorderColorWhenNotFocused {
            get {
                return this._borderColNotFoc;
            }
            set {
                this._borderColNotFoc = value;
                this.Invalidate(true);
            }
        }



        #endregion

        #region DEPRECATIONS

        /// <summary>
        /// Hides the property BorderStyle
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Deprecated", true)]
        public new BorderStyle BorderStyle { get; set; }

        /// <summary>
        /// Hides the property Text
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Deprecated", true)]
        public new string Text { get; set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Gives highlight to this control when the hosted TextBox gets focus
        /// </summary>
        private void HostedTextBox_Enter(object sender, EventArgs e) {
            this.panMain.BorderColor = this._borderColFoc;
            this.panMain.Refresh();
        }

        /// <summary>
        /// Removes highlight from this control when the hosted TextBox lost focus
        /// </summary>
        private void HostedTextBox_Leave(object sender, EventArgs e) {
            this.panMain.BorderColor = this._borderColNotFoc;
            this.panMain.Refresh();
            this.ForceHideToolTip();
        }

        /// <summary>
        /// Wnter is pressed, raise the EnterIsPressed
        /// </summary>
        private void HostedTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) this.EnterKeyDetected?.Invoke(this.HostedTextBox, EventArgs.Empty);
        }

        /// <summary>
        /// Resets whatever border color was implemented by the HostedTextBoxInformationValidated event
        /// </summary>
        private void HostedTextBox_TextChanged(object sender, EventArgs e) {
            this.panMain.BorderColor = this._borderColFoc;
            this.panMain.Refresh();
            this.ForceHideToolTip();

            if (string.IsNullOrWhiteSpace(this.HostedTextBoxText)) return;

            switch (this._submitType) {
                case DccActionTextBoxSumbitTypes.Search:
                    this.SetHostedButtonTip($"Search for \"{this.HostedTextBoxText}\"");
                    break;
            }
        }

        /// <summary>
        /// Main entry-point to raising the event <see cref="HostedButtonClicked"/>
        /// </summary>
        private void HostedButton_MouseClick(object sender, MouseEventArgs e) {
            this.HostedButtonClicked?.Invoke(this,
                new DccActionTextBoxHostedButtonClickedEventArgs(this.HostedTextBox.Text));

            if (this._submitType == DccActionTextBoxSumbitTypes.Launcher && this.HostedButtonLaunchesOfd) {
                using (OpenFileDialog ofd = new OpenFileDialog()) {
                    ofd.FileName = this.HostedTextBox.Text;
                    ofd.Title = this.Parent.ProductName;
                    ofd.ShowDialog();
                    this.HostedTextBoxText = ofd.FileName;
                }
            }

        }

        /// <summary>
        /// UnMasks the password
        /// </summary>
        private void HostedButton_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.HostedTextBox.PasswordChar = char.MinValue;
                this.HostedTextBox.UseSystemPasswordChar = false;
                this.HostedTextBox.Invalidate();
            }
        }

        /// <summary>
        /// Masks the password back
        /// </summary>
        private void HostedButton_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.HostedTextBox.UseSystemPasswordChar = true;
                this.HostedTextBox.PasswordChar = RAISED_DOT_CHAR;
                this.HostedTextBox.Focus();
                this.HostedTextBox.Invalidate();
            }
        }

        /// <summary>
        /// Public method that should be called to 
        /// </summary>
        /// <param name="bordercolor">Nullable <see cref="System.Color"/> type for user to highlight the control </param>
        public virtual void OnHostedTextBoxInformationValidated(string title, string message, Color? bordercolor = null) {

            if (this.IsHandleCreated) {

                if (bordercolor != null) {

                    if (bordercolor.HasValue) {

                        if (this.InvokeRequired) {
                            this.BeginInvoke((MethodInvoker)delegate () {
                                this.panMain.BorderColor = bordercolor.Value;
                                this.panMain.Refresh();
                            });
                        } else {
                            this.panMain.BorderColor = bordercolor.Value;
                            this.panMain.Refresh();
                        }

                    }
                }

                this.ForceHideToolTip();

                this._tip = new ToolTip() {
                    UseAnimation = true, UseFading = true, IsBalloon = true, ToolTipTitle = title
                };

                if (this.InvokeRequired) {
                    this.BeginInvoke((MethodInvoker)delegate () { this._tip.Show(message, this.HostedTextBox, 30, -90, 3500); });
                } else {
                    this._tip.Show(message, this.HostedTextBox, 30, -90);
                }

            }

        }

        /// <summary>
        /// Forces the tooltip, when not null, to be hidden
        /// </summary>
        public void ForceHideToolTip() {
            if (this._tip != null) {
                this._tip.Hide(this.HostedTextBox);
                this._tip.Dispose();
                this._tip = null;
            }
        }

        /// <summary>
        /// Changes the single-char text of the hosted Button
        /// </summary>
        private void SetHostedButtonIcon(DccEnumerations.DccActionTextBoxSumbitTypes submitType) {
            this.HostedButton.MouseDown -= HostedButton_MouseDown;
            this.HostedButton.MouseUp -= HostedButton_MouseUp;
            this.HostedTextBox.PasswordChar = char.MinValue;
            this.HostedTextBox.UseSystemPasswordChar = false;

            switch (submitType) {
                case DccActionTextBoxSumbitTypes.Search:
                    this.HostedButton.Text = LEFTP_MAGGLASS;
                    break;
                case DccActionTextBoxSumbitTypes.Go:
                    this.HostedButton.Text = RIGHTP_TRIANG;
                    break;
                case DccActionTextBoxSumbitTypes.Launcher:
                    this.HostedButton.Text = MID_ELPSIS;
                    break;
                case DccActionTextBoxSumbitTypes.Add:
                    this.HostedButton.Text = HEAVY_PLUS;
                    break;
                case DccActionTextBoxSumbitTypes.ShowPassword:
                    this.HostedTextBox.PasswordChar = RAISED_DOT_CHAR;
                    this.HostedTextBox.UseSystemPasswordChar = true;
                    this.HostedButton.MouseDown += HostedButton_MouseDown;
                    this.HostedButton.MouseUp += HostedButton_MouseUp;
                    this.HostedButton.Text = EYE_CHAR;
                    this.SetHostedButtonTip("Show password");
                    break;
                default:
                    this.HostedButton.Text = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// Sets the tooltip for the hosted Button
        /// </summary>
        public void SetHostedButtonTip(string tipMsg) => this.tip.SetToolTip(this.HostedButton, tipMsg);

        /// <summary>
        /// Sets the BackColor of all the controls except the HostedButton
        /// </summary>
        private void SetControlBackColor(Color color) {
            base.BackColor = color;
            this.panMain.BackColor = color;
            this.HostedTextBox.BackColor = color;
            this.tlpMain.BackColor = color;
            this.Refresh();
        }

        #endregion


    }
}
