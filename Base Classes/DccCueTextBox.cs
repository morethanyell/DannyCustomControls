using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace DannyCustomControls {

    /// <summary>
    /// TextBox with watermark or formally known as CueBanner
    /// Source: https://stackoverflow.com/questions/12310674/looking-for-a-way-to-do-a-multiline-cue-banner-textbox/19273816#19273816
    /// </summary>
    [DesignerCategory("")]
    public class DccCueTextBox : TextBox {

        #region OTHER PROPERTIES

        /// <summary>
        /// Private field _bitmap
        /// </summary>
        private Bitmap _bitmap;

        /// <summary>
        /// Private field _paintedFirstTime
        /// </summary>
        private bool _paintedFirstTime = false;

        /// <summary>
        /// PInvoke Constant WM_PAINT
        /// </summary>
        const int WM_PAINT = 0x000F;

        /// <summary>
        /// PInvoke Constant WM_PRINT
        /// </summary>
        const int WM_PRINT = 0x0317;

        /// <summary>
        /// PInvoke Constant PRF_CLIENT
        /// </summary>
        const int PRF_CLIENT = 0x00000004;

        /// <summary>
        /// PInvoke Constant PRF_ERASEBKGND
        /// </summary>
        const int PRF_ERASEBKGND = 0x00000008;

        /// <summary>
        /// Private field _cue
        /// </summary>
        private string _cue;

        /// <summary>
        /// Private field DEF_BRUSH
        /// </summary>
        private Color DEF_BRUSH = Color.Gray;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Determines the color of the Cue Banner
        /// </summary>
        public Color CueForeColor { get; set; }

        /// <summary>
        /// Gets or sets the message to be displayed as Cue Banner
        /// </summary>
        public string Cue {
            get { return _cue; }
            set {
                _cue = value;
                Invalidate(true);
            }
        }

        /// <summary>
        /// Determines the border stype of this TextBox
        /// </summary>
        public new BorderStyle BorderStyle {
            get { return base.BorderStyle; }
            set {
                if (_paintedFirstTime)
                    SetStyle(ControlStyles.UserPaint, false);
                base.BorderStyle = value;
                if (_paintedFirstTime)
                    SetStyle(ControlStyles.UserPaint, true);
            }
        }

        /// <summary>
        /// Determines whether or not this TextBox is multiline
        /// </summary>
        public override bool Multiline {
            get { return base.Multiline; }
            set {
                if (_paintedFirstTime)
                    SetStyle(ControlStyles.UserPaint, false);
                base.Multiline = value;
                if (_paintedFirstTime)
                    SetStyle(ControlStyles.UserPaint, true);
            }
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Creates an instance of this TextBox implementation
        /// </summary>
        public DccCueTextBox() : base() {
            this.CueForeColor = DEF_BRUSH;
            SetStyle(ControlStyles.UserPaint, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Platform Invoke SendMessage
        /// </summary>
        [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Implementation if IDispose interface
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            if (_bitmap != null)
                _bitmap.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// An application-defined function that processes messages sent to a window. 
        /// The <b>WNDPROC</b> type defines a pointer to this callback function.
        /// </summary>
        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_PAINT) {
                _paintedFirstTime = true;
                CaptureBitmap();
                SetStyle(ControlStyles.UserPaint, true);
                base.WndProc(ref m);
                SetStyle(ControlStyles.UserPaint, false);
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Captures the cue message as bitmap
        /// </summary>
        private void CaptureBitmap() {
            if (_bitmap != null)
                _bitmap.Dispose();

            _bitmap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, PixelFormat.Format32bppArgb);

            using (var graphics = Graphics.FromImage(_bitmap)) {
                int lParam = PRF_CLIENT | PRF_ERASEBKGND;

                IntPtr hdc = graphics.GetHdc();
                SendMessage(this.Handle, WM_PRINT, hdc, new IntPtr(lParam));
                graphics.ReleaseHdc(hdc);
            }
        }

        /// <summary>
        /// The OnPaint method is called whenever the plug-in window should paint itself. 
        /// This occurs when the plug-in window receives a WM_PAINT message, 
        /// which is mapped to the OnPaint method in the message map 
        /// described earlier. The wizard provides an implementation of this 
        /// method that paints the background black and places the name of the 
        /// plug-in in the plug-in window. The only modification that is necessary 
        /// for the Search UI plug-in is the removal of the code that displays the text.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e) {
            SetStyle(ControlStyles.UserPaint, true);
            if (_bitmap == null)
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, this.ClientRectangle);
            else
                e.Graphics.DrawImageUnscaled(_bitmap, 0, 0);

            if (_cue != null && Text.Length == 0)
                e.Graphics.DrawString(_cue, Font, new SolidBrush(this.CueForeColor), 1.5f, -1.5f);


            SetStyle(ControlStyles.UserPaint, false);
        }

        /// <summary>
        /// The OnKeyDown event handler handles an event that occurs when a key is pressed.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            Invalidate();
        }

        /// <summary>
        /// The OnKeyUp method is called before any event handler for the 
        /// KeyUp event is called. This method allows derived classes to 
        /// handle the KeyUp event without attaching a delegate. This is 
        /// the preferred technique for handling the event in a derived class.
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            Invalidate();
        }

        /// <summary>
        /// The OnMouseDown event handler handles an event that occurs when mouse key is pressed.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            Invalidate();
        }

        /// <summary>
        /// The OnMouseDown event handler handles an event that occurs when mouse key is moved.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Invalidate();
        }

        /// <summary>
        /// The OnMouseDown event handler handles an event that occurs when font is changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e) {
            if (_paintedFirstTime)
                SetStyle(ControlStyles.UserPaint, false);
            base.OnFontChanged(e);
            if (_paintedFirstTime)
                SetStyle(ControlStyles.UserPaint, true);
        }

        #endregion


    }

}

