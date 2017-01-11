namespace PaintedObjectsMoving {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.DrawForm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DrawForm)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawForm
            // 
            this.DrawForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DrawForm.Location = new System.Drawing.Point(0, 12);
            this.DrawForm.Name = "DrawForm";
            this.DrawForm.Size = new System.Drawing.Size(914, 318);
            this.DrawForm.TabIndex = 0;
            this.DrawForm.TabStop = false;
            this.DrawForm.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.DrawForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.DrawForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.DrawForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 328);
            this.Controls.Add(this.DrawForm);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DrawForm)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.PictureBox DrawForm;
    }
}

