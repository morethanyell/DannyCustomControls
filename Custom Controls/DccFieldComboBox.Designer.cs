namespace DannyCustomControls {
    partial class DccFieldComboBox {
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.HostedLabel = new System.Windows.Forms.Label();
            this.panInput = new DccPanel();
            this.tlpInput = new System.Windows.Forms.TableLayoutPanel();
            this.HostedComboBox = new DccReadOnlyComboBox();
            this.mainTip = new System.Windows.Forms.ToolTip(this.components);
            this.tlpMain.SuspendLayout();
            this.panInput.SuspendLayout();
            this.tlpInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.HostedLabel, 0, 0);
            this.tlpMain.Controls.Add(this.panInput, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(300, 50);
            this.tlpMain.TabIndex = 1;
            // 
            // HostedLabel
            // 
            this.HostedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HostedLabel.AutoSize = true;
            this.HostedLabel.Location = new System.Drawing.Point(3, 2);
            this.HostedLabel.Name = "HostedLabel";
            this.HostedLabel.Size = new System.Drawing.Size(0, 13);
            this.HostedLabel.TabIndex = 0;
            // 
            // panInput
            // 
            this.panInput.BorderColor = System.Drawing.Color.LightGray;
            this.panInput.BorderThickness = 1;
            this.panInput.Controls.Add(this.tlpInput);
            this.panInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panInput.Location = new System.Drawing.Point(3, 18);
            this.panInput.Name = "panInput";
            this.panInput.Padding = new System.Windows.Forms.Padding(1);
            this.panInput.Size = new System.Drawing.Size(294, 29);
            this.panInput.TabIndex = 1;
            // 
            // tlpInput
            // 
            this.tlpInput.ColumnCount = 1;
            this.tlpInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInput.Controls.Add(this.HostedComboBox, 0, 0);
            this.tlpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInput.Location = new System.Drawing.Point(1, 1);
            this.tlpInput.Name = "tlpInput";
            this.tlpInput.RowCount = 1;
            this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpInput.Size = new System.Drawing.Size(292, 27);
            this.tlpInput.TabIndex = 0;
            // 
            // HostedComboBox
            // 
            this.HostedComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostedComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HostedComboBox.FormattingEnabled = true;
            this.HostedComboBox.Location = new System.Drawing.Point(3, 3);
            this.HostedComboBox.Name = "HostedComboBox";
            this.HostedComboBox.Size = new System.Drawing.Size(286, 21);
            this.HostedComboBox.TabIndex = 0;
            this.HostedComboBox.Enter += new System.EventHandler(this.comboBox1_Enter);
            this.HostedComboBox.Leave += new System.EventHandler(this.comboBox1_Leave);
            this.HostedComboBox.Resize += new System.EventHandler(this.cboInput_Resize);
            // 
            // PFieldComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PFieldComboBox";
            this.Size = new System.Drawing.Size(300, 50);
            this.Enter += new System.EventHandler(this.comboBox1_Enter);
            this.Leave += new System.EventHandler(this.comboBox1_Leave);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.panInput.ResumeLayout(false);
            this.tlpInput.ResumeLayout(false);
            this.tlpInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label HostedLabel;
        private DccPanel panInput;
        private System.Windows.Forms.TableLayoutPanel tlpInput;
        public DccReadOnlyComboBox HostedComboBox;
        private System.Windows.Forms.ToolTip mainTip;
    }
}
