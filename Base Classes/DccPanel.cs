using System.Drawing;
using System.Windows.Forms;

namespace DannyCustomControls {

    /// <summary>
    /// An implementation of <see cref="Panel"/> with additional properties i.e. 
    /// Boder, BorderColor, and BorderThickness
    /// </summary>
    [System.ComponentModel.DesignerCategory("")]
    public class DccPanel : Panel {

        /// <summary>
        /// Default color for the border
        /// </summary>
        public static Color DEF_BORDER_COLOR = Color.LightGray;

        /// <summary>
        /// Determines the color of the panel border that's being drawn
        /// on OnPaint method
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary>
        /// Determines the thickness by pixel of the border. By default, this is set to 1
        /// </summary>
        public int BorderThickness { get; set; }

        /// <summary>
        /// Creates an instance of this Panel
        /// </summary>
        public DccPanel() {

            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            BorderColor = DEF_BORDER_COLOR;
            BorderThickness = 1;

            this.Padding = new Padding(1, 1, 1, 1);
        }

        /// <summary>
        /// Draws a thin light-gray rectangle that serves
        /// as its border
        /// </summary>
        protected override void OnPaint(PaintEventArgs e) {
            using (SolidBrush brush = new SolidBrush(BackColor)) {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }

            e.Graphics.DrawRectangle(new Pen(this.BorderColor, this.BorderThickness), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

            this.Update();
        }


    }
}
