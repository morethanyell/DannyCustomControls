using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DannyCustomControls {

    /// <summary>
    /// Represents a Windows combo box control. It enhances the .NET standard combo box control
    /// with a ReadOnly mode.
    /// </summary>
    [ComVisible(false)]
    [DesignerCategory("")]
    public class DccReadOnlyDateTimePicker : DccDateTimePicker {

        #region Member variables

        /// <summary>
        /// The embedded TextBox control that is used for the ReadOnly mode
        /// </summary>
        private TextBox _textbox;

        /// <summary>
        /// True, when the ComboBox is set to ReadOnly
        /// </summary>
        private bool _isReadOnly;

        /// <summary>
        /// True, when the control is visible
        /// </summary>
        private bool _visible = true;

        /// <summary>
        /// A private field for DateAsStringTypes used to 
        /// </summary>
        private DateAsStringTypes _dateAsStringType;
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DccReadOnlyDateTimePicker() {
            _textbox = new TextBox();
            _dateAsStringType = DateAsStringTypes.ShortDate;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets a value indicating whether the control is read-only.
        /// </summary>
        /// <value>
        /// <b>true</b> if the combo box is read-only; otherwise, <b>false</b>. The default is <b>false</b>.
        /// </value>
        /// <remarks>
        /// When this property is set to <b>true</b>, the contents of the control cannot be changed 
        /// by the user at runtime. With this property set to <b>true</b>, you can still set the value
        /// in code. You can use this feature instead of disabling the control with the Enabled
        /// property to allow the contents to be copied.
        /// </remarks>
        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("Controls whether the value in the combobox control can be changed or not")]
        public bool ReadOnly {
            get { return _isReadOnly; }
            set {
                if (value != _isReadOnly) {
                    _isReadOnly = value;
                    ShowControl();
                }
            }
        }

        /// <summary>
        /// Enumeration of Types of Dates to be displayed when the control
        /// transforms into a read-only textbox
        /// </summary>
        public enum DateAsStringTypes {
            /// <summary>
            /// Represents the full formatting of a DateTime
            /// </summary>
            Full = 0,
            /// <summary>
            /// Reresents the date dd/MM/yyyy of a DateTime
            /// </summary>
            ShortDate = 1,
            /// <summary>
            /// Reresents the time HH:mm:ss.fff of a DateTime
            /// </summary>
            TimeOnly
        }

        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("The apperance of the Date Value in the textbox")]
        /// <summary>
        /// The apperance of the Date Value in the textbox
        /// </summary>
        public DateAsStringTypes ReadOnlyDateAppearance {
            get { return _dateAsStringType; }
            set {
                _dateAsStringType = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating wether the control is displayed.
        /// </summary>
        /// <value><b>true</b> if the control is displayed; otherwise, <b>false</b>. 
        /// The default is <b>true</b>.</value>
        public new bool Visible {
            get { return _visible; }
            set {
                _visible = value;
                ShowControl();
            }
        }

        /// <summary>
        /// Conceals the control from the user.
        /// </summary>
        /// <remarks>
        /// Hiding the control is equvalent to setting the <see cref="Visible"/> property to <b>false</b>. 
        /// After the <b>Hide</b> method is called, the <b>Visible</b> property returns a value of 
        /// <b>false</b> until the <see cref="Show"/> method is called.
        /// </remarks>
        public new void Hide() => Visible = false;

        /// <summary>
        /// Displays the control to the user.
        /// </summary>
        /// <remarks>
        /// Showing the control is equivalent to setting the <see cref="Visible"/> property to <b>true</b>.
        /// After the <b>Show</b> method is called, the <b>Visible</b> property returns a value of 
        /// <b>true</b> until the <see cref="Hide"/> method is called.
        /// </remarks>
        public new void Show() => Visible = true;

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Initializes the embedded TextBox with the default values from the ComboBox
        /// </summary>
        private void AddTextbox() {
            _textbox.ReadOnly = true;
            _textbox.Location = this.Location;
            _textbox.BorderStyle = BorderStyle.None;
            _textbox.Size = this.Size;
            _textbox.Dock = this.Dock;
            _textbox.Anchor = this.Anchor;
            _textbox.Visible = !this.Visible;
            _textbox.RightToLeft = this.RightToLeft;
            _textbox.Margin = new Padding(this.Margin.Left + 3, this.Margin.Top + 3, this.Margin.Right + 3, this.Margin.Bottom + 3);
            _textbox.Font = this.Font;
            _textbox.Text = this.Text;
            _textbox.TabStop = this.TabStop;
            _textbox.TabIndex = this.TabIndex;
        }

        /// <summary>
        /// Shows either the DateTimePicer or the TextBox or nothing, depending on the state
        /// of the ReadOnly, Enable and Visible flags.
        /// </summary>
        private void ShowControl() {
            if (_isReadOnly) {

                _textbox.Visible = true;
                base.Visible = false;

                switch (this.ReadOnlyDateAppearance) {
                    case DateAsStringTypes.Full:
                        _textbox.Text = this.Value.ToString("MM/dd/yyyy HH:mm:ss");
                        break;
                    case DateAsStringTypes.ShortDate:
                        _textbox.Text = this.Value.ToShortDateString();
                        break;
                    case DateAsStringTypes.TimeOnly:
                        _textbox.Text = this.Value.TimeOfDay.ToString();
                        break;
                    default:
                        _textbox.Text = this.Value.ToString("MM/dd/yyyy HH:mm:ss");
                        break;
                }

            } else {
                _textbox.Visible = false;
                base.Visible = true;
            }

            _textbox.BackColor = System.Drawing.Color.White;
            _textbox.Refresh();
        }

        #endregion

        #region PROTECTED OVERRIDES

        /// <summary>
        /// This member overrides <see cref="Control.OnParentChanged"/>
        /// </summary>
        protected override void OnParentChanged(EventArgs e) {
            base.OnParentChanged(e);

            if (Parent != null)
                AddTextbox();
            _textbox.Parent = this.Parent;
        }

        /// <summary>
        /// This member overrides <see cref="DateTimePicker.OnValueChanged"/>.
        /// </summary>
        protected override void OnValueChanged(EventArgs e) {
            base.OnValueChanged(e);
            switch (ReadOnlyDateAppearance) {
                case DateAsStringTypes.Full:
                    _textbox.Text = this.Value.ToString("MM/dd/yyyy HH:mm:ss");
                    break;
                case DateAsStringTypes.ShortDate:
                    _textbox.Text = this.Value.ToShortDateString();
                    break;
                case DateAsStringTypes.TimeOnly:
                    _textbox.Text = this.Value.TimeOfDay.ToString();
                    break;
                default:
                    _textbox.Text = this.Value.ToString("MM/dd/yyyy HH:mm:ss");
                    break;
            }
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnFontChanged"/>.
        /// </summary>
        protected override void OnFontChanged(EventArgs e) {
            base.OnFontChanged(e);
            _textbox.Font = this.Font;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnResize"/>.
        /// </summary>
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            _textbox.Size = this.Size;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnDockChanged"/>.
        /// </summary>
        protected override void OnDockChanged(EventArgs e) {
            base.OnDockChanged(e);
            _textbox.Dock = this.Dock;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnEnabledChanged"/>.
        /// </summary>
        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            ShowControl();
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnRightToLeftChanged"/>.
        /// </summary>
        protected override void OnRightToLeftChanged(EventArgs e) {
            base.OnRightToLeftChanged(e);
            _textbox.RightToLeft = this.RightToLeft;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnTextChanged"/>.
        /// </summary>
        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            _textbox.Text = this.Text;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnLocationChanged"/>.
        /// </summary>
        protected override void OnLocationChanged(EventArgs e) {
            base.OnLocationChanged(e);
            _textbox.Location = this.Location;
        }

        /// <summary>
        /// This member overrides <see cref="Control.OnTabIndexChanged"/>.
        /// </summary>
        protected override void OnTabIndexChanged(EventArgs e) {
            base.OnTabIndexChanged(e);
            _textbox.TabIndex = this.TabIndex;
        }

        #endregion


    }
}
