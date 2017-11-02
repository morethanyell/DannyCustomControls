namespace DannyCustomControls {
    partial class DccActionTextBox {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.components = new System.ComponentModel.Container();
            this.panMain = new DannyCustomControls.DccPanel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.HostedTextBox = new DannyCustomControls.DccCueTextBox();
            this.HostedButton = new DannyCustomControls.Base_Classes.DccUnSelectableButton();
            this.tip = new System.Windows.Forms.ToolTip(this.components);
            this.panMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.White;
            this.panMain.BorderColor = System.Drawing.Color.LightGray;
            this.panMain.BorderThickness = 1;
            this.panMain.Controls.Add(this.tlpMain);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Margin = new System.Windows.Forms.Padding(0);
            this.panMain.Name = "panMain";
            this.panMain.Padding = new System.Windows.Forms.Padding(1);
            this.panMain.Size = new System.Drawing.Size(280, 30);
            this.panMain.TabIndex = 1;
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.White;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.Controls.Add(this.HostedTextBox, 0, 0);
            this.tlpMain.Controls.Add(this.HostedButton, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(1, 1);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(278, 28);
            this.tlpMain.TabIndex = 0;
            // 
            // HostedTextBox
            // 
            this.HostedTextBox.BackColor = System.Drawing.Color.White;
            this.HostedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HostedTextBox.Cue = "Watermark (HostedTextboxCue)";
            this.HostedTextBox.CueForeColor = System.Drawing.Color.Gray;
            this.HostedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostedTextBox.Location = new System.Drawing.Point(6, 7);
            this.HostedTextBox.Margin = new System.Windows.Forms.Padding(6, 7, 6, 3);
            this.HostedTextBox.Name = "HostedTextBox";
            this.HostedTextBox.Size = new System.Drawing.Size(236, 13);
            this.HostedTextBox.TabIndex = 0;
            this.HostedTextBox.UseSystemPasswordChar = true;
            this.HostedTextBox.TextChanged += new System.EventHandler(this.HostedTextBox_TextChanged);
            this.HostedTextBox.Enter += new System.EventHandler(this.HostedTextBox_Enter);
            this.HostedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HostedTextBox_KeyDown);
            this.HostedTextBox.Leave += new System.EventHandler(this.HostedTextBox_Leave);
            // 
            // HostedButton
            // 
            this.HostedButton.BackColor = System.Drawing.Color.White;
            this.HostedButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostedButton.FlatAppearance.BorderSize = 0;
            this.HostedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HostedButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.HostedButton.Location = new System.Drawing.Point(248, 0);
            this.HostedButton.Margin = new System.Windows.Forms.Padding(0);
            this.HostedButton.Name = "HostedButton";
            this.HostedButton.Size = new System.Drawing.Size(30, 28);
            this.HostedButton.TabIndex = 1;
            this.HostedButton.UseVisualStyleBackColor = false;
            this.HostedButton.Enter += new System.EventHandler(this.HostedTextBox_Enter);
            this.HostedButton.Leave += new System.EventHandler(this.HostedTextBox_Leave);
            this.HostedButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HostedButton_MouseClick);
            // 
            // DccActionTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panMain);
            this.Name = "DccActionTextBox";
            this.Size = new System.Drawing.Size(280, 30);
            this.Enter += new System.EventHandler(this.HostedTextBox_Enter);
            this.Leave += new System.EventHandler(this.HostedTextBox_Leave);
            this.panMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        public DccCueTextBox HostedTextBox;
        private DccPanel panMain;
        private Base_Classes.DccUnSelectableButton HostedButton;
        private System.Windows.Forms.ToolTip tip;
    }
}
