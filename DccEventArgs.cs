using System;

namespace DannyCustomControls {

    /// <summary>
    /// Event args used in <see cref="DccActionTextBox.HostedButtonClicked"/>
    /// </summary>
    public class DccActionTextBoxHostedButtonClickedEventArgs : EventArgs {

        /// <summary>
        /// The text value of the hosted TextBox in the custom 
        /// <see cref="UserControls.PActionTextBox"/> user control
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Creates an instance of this event argument class implementation
        /// </summary>
        public DccActionTextBoxHostedButtonClickedEventArgs (string value) {
            this.Value = value;
        }

        /// <summary>
        /// Creates an instance of this event argument class implementation
        /// without argument
        /// </summary>
        public DccActionTextBoxHostedButtonClickedEventArgs () { }

    }

}
