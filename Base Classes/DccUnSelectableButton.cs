using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DannyCustomControls.Base_Classes {

    /// <summary>
    /// An implementation of Button which sets its style as non-selectable,
    /// or will never get focus
    /// </summary>
    [DesignerCategory("")]
    public class DccUnSelectableButton : Button {

        /// <summary>
        /// Creates an instance of this non-selectable button
        /// </summary>
        public DccUnSelectableButton() {
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, false);
        }

    }
}
