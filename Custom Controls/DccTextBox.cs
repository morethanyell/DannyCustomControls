using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DannyCustomControls {
    public partial class DccTextBox : UserControl {

        /// <summary>
        /// Tooltip used privately during validation
        /// </summary>
        private ToolTip _tip;

        /// <summary>
        /// Points to the method called when event the enter key is pressed in the hosted TextBox
        /// </summary>
        public delegate void EnterIsPressedEventArgs(object sender, EventArgs e);

        /// <summary>
        /// Is raised when event the enter key is pressed in the hosted TextBox
        /// </summary>
        public event EnterIsPressedEventArgs EnterKeyDetected;

        /// <summary>
        /// Creates an instance of the APSTextBox
        /// </summary>
        public DccTextBox() => InitializeComponent();

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
        /// Gets or sets the hosted TexxBox's Multiline propert
        /// </summary>
        public bool HostedTextBoxMultiline {
            get {
                return this.HostedTextBox.Multiline;
            }
            set {
                this.HostedTextBox.Multiline = value;
            }
        }

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
        public bool HistedTextBoxReadOnly {
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
