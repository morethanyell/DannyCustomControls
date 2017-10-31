using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DannyCustomControls {


    /// <summary>
    /// Source: https://www.codeproject.com/Articles/6664/DateTimePicker-appears-flat
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    public class DccDateTimePicker : DateTimePicker {

        #region PROPERTIES

        /// <summary>
        /// PInvoke SendMessage
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

        /// <summary>
        /// PInvoke GetWindowDC
        /// </summary>
        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        /// PInvoke ReleaseDC
        /// </summary>
        [DllImport("user32")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// PInvoke Constant WM_ERASEBKGND
        /// </summary>
        const int WM_ERASEBKGND = 0x14;

        /// <summary>
        /// PInvoke Constant WM_PAINT
        /// </summary>
        const int WM_PAINT = 0xF;

        /// <summary>
        /// PInvoke Constant WM_NC_HITTEST
        /// </summary>
        const int WM_NC_HITTEST = 0x84;

        /// <summary>
        /// PInvoke Constant WM_NC_PAINT
        /// </summary>
        const int WM_NC_PAINT = 0x85;

        /// <summary>
        /// PInvoke Constant WM_PRINTCLIENT
        /// </summary>
        const int WM_PRINTCLIENT = 0x318;

        /// <summary>
        /// PInvoke Constant WM_SETCURSOR
        /// </summary>
        const int WM_SETCURSOR = 0x20;

        /// <summary>
        /// Gets or set the color of the border
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Private field Pen used in drawing the border
        /// </summary>
        private Pen BorderPen = new Pen(Color.White, 2);

        /// <summary>
        /// Private field Pen used in drawing the border
        /// </summary>
        private Pen BorderPenControl = new Pen(Color.White, 2);

        /// <summary>
        /// Private field DroppedDown determines that the dropdown is false
        /// </summary>
        private bool DroppedDown = false;

        /// <summary>
        /// Private field InvalidateSince, increments everytime WM_SETCURSOR
        /// </summary>
        private int InvalidateSince = 0;

        #endregion

        /// <summary>
        /// Creates an instance of this PDateTimePicker
        /// </summary>
        public DccDateTimePicker() : base() {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        #region METHODS

        /// <summary>
        /// Overrides <see cref="DateTimePicker.OnValueChanged(EventArgs)"/>
        /// </summary>
        protected override void OnValueChanged(EventArgs eventargs) {
            base.OnValueChanged(eventargs);
            this.Invalidate();
        }

        /// <summary>
        /// Overrides <see cref="WndProc(ref Message)"/>
        /// </summary>
        protected override void WndProc(ref Message m) {
            try {
                IntPtr hDC = IntPtr.Zero;
                Graphics gdc = null;
                switch (m.Msg) {
                    case WM_NC_PAINT:
                        hDC = GetWindowDC(m.HWnd);
                        gdc = Graphics.FromHdc(hDC);
                        SendPrintClientMsg();
                        OverrideControlBorder(gdc);
                        m.Result = (IntPtr)1;
                        ReleaseDC(m.HWnd, hDC);
                        gdc.Dispose();
                        break;
                    case WM_PAINT:
                        base.WndProc(ref m);
                        hDC = GetWindowDC(m.HWnd);
                        gdc = Graphics.FromHdc(hDC);
                        OverrideControlBorder(gdc);
                        ReleaseDC(m.HWnd, hDC);
                        gdc.Dispose();
                        break;
                    case WM_SETCURSOR:
                        base.WndProc(ref m);
                        if (DroppedDown && InvalidateSince < 3) {
                            Invalidate();
                            InvalidateSince++;
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            } catch { }
        }

        /// <summary>
        /// Method on Print Client Message
        /// </summary>
        private void SendPrintClientMsg() {
            Graphics gClient = this.CreateGraphics();
            IntPtr ptrClientDC = gClient.GetHdc();
            gClient.ReleaseHdc(ptrClientDC);
            gClient.Dispose();
        }

        /// <summary>
        /// Drawing border
        /// </summary>
        private void OverrideControlBorder(Graphics g) {
            if (this.Focused == false || this.Enabled == false)
                g.DrawRectangle(BorderPenControl, new Rectangle(0, 0, this.Width, this.Height));
            else
                g.DrawRectangle(BorderPen, new Rectangle(0, 0, this.Width, this.Height));
        }

        /// <summary>
        /// Overrides <see cref="OnCloseUp(EventArgs)"/>
        /// </summary>
        protected override void OnCloseUp(EventArgs eventargs) {
            DroppedDown = false;
            base.OnCloseUp(eventargs);
        }

        /// <summary>
        /// Overrides <see cref="OnLostFocus(EventArgs)"/>
        /// </summary>
        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        /// <summary>
        /// Overrides <see cref="OnGotFocus(EventArgs)"/>
        /// </summary>
        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        /// <summary>
        /// Overrides <see cref="OnResize(EventArgs)"/>
        /// </summary>
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            this.Invalidate();
        }
       
        #endregion

    }
}
