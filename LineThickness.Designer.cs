namespace PaintedObjectsMoving
{
    partial class LineThickness
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.ThicknessValue = new System.Windows.Forms.Label();
            this.ThicknessTrackBar = new System.Windows.Forms.TrackBar();
            this.ThicknessLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ThicknessTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(173, 64);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(15, 64);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 14;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ThicknessValue
            // 
            this.ThicknessValue.AutoSize = true;
            this.ThicknessValue.Location = new System.Drawing.Point(235, 24);
            this.ThicknessValue.Name = "ThicknessValue";
            this.ThicknessValue.Size = new System.Drawing.Size(13, 13);
            this.ThicknessValue.TabIndex = 13;
            this.ThicknessValue.Text = "1";
            // 
            // ThicknessTrackBar
            // 
            this.ThicknessTrackBar.Location = new System.Drawing.Point(12, 24);
            this.ThicknessTrackBar.Name = "ThicknessTrackBar";
            this.ThicknessTrackBar.Size = new System.Drawing.Size(203, 45);
            this.ThicknessTrackBar.TabIndex = 12;
            this.ThicknessTrackBar.Scroll += new System.EventHandler(this.ThicknessTrackBar_Scroll);
            // 
            // ThicknessLabel
            // 
            this.ThicknessLabel.AutoSize = true;
            this.ThicknessLabel.Location = new System.Drawing.Point(12, 8);
            this.ThicknessLabel.Name = "ThicknessLabel";
            this.ThicknessLabel.Size = new System.Drawing.Size(106, 13);
            this.ThicknessLabel.TabIndex = 11;
            this.ThicknessLabel.Text = "Выберите толщину:";
            // 
            // LineThickness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 94);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ThicknessValue);
            this.Controls.Add(this.ThicknessTrackBar);
            this.Controls.Add(this.ThicknessLabel);
            this.Name = "LineThickness";
            this.Text = "LineThickness";
            ((System.ComponentModel.ISupportInitialize)(this.ThicknessTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label ThicknessValue;
        private System.Windows.Forms.TrackBar ThicknessTrackBar;
        private System.Windows.Forms.Label ThicknessLabel;
    }
}