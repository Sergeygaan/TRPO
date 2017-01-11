namespace PaintedObjectsMoving
{
    partial class ChildForm
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
            this.DrawForm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DrawForm)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawForm
            // 
            this.DrawForm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DrawForm.Location = new System.Drawing.Point(-1, -1);
            this.DrawForm.Name = "DrawForm";
            this.DrawForm.Size = new System.Drawing.Size(1200, 1200);
            this.DrawForm.TabIndex = 1;
            this.DrawForm.TabStop = false;
            this.DrawForm.Paint += new System.Windows.Forms.PaintEventHandler(this.Child_Paint);
            this.DrawForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Child1_MouseDown);
            this.DrawForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Child_MouseMove);
            this.DrawForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Child__MouseUp);
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(922, 491);
            this.Controls.Add(this.DrawForm);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "ChildForm";
            this.Text = "ChildForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.DrawForm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawForm;
    }
}