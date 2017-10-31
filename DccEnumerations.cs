using System;

namespace DannyCustomControls {

    /// <summary>
    /// A collection of Enumerations used in the Pandora Context
    /// </summary>
    public static class DccEnumerations {

        /// <summary>
        /// Enumerates the types of submission the hosted Button in <see cref="DccActionTextBox"/>
        /// </summary>
        public enum DccActionTextBoxSumbitTypes {
            /// <summary>
            /// When this context is used for searching
            /// </summary>
            Search = 0,
            /// <summary>
            /// When this context is used for anything other than searching
            /// </summary>
            Go = 1,
            /// <summary>
            /// When this context is used to launch anything
            /// </summary>
            Launcher = 2,
            /// <summary>
            /// When this context is used to add the field's value
            /// </summary>
            Add = 3,
            /// <summary>
            /// When this context is used as a password textbox
            /// </summary>
            ShowPassword = 4
        }


    }
}
