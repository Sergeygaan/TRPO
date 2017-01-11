namespace PaintedObjectsMoving
{
    partial class LineStyle
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
            this.Styles = new System.Windows.Forms.ComboBox();
            this.Label = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Styles
            // 
            this.Styles.FormattingEnabled = true;
            this.Styles.Items.AddRange(new object[] {
            "Сплошная",
            "Пунктир",
            "Точка",
            "Пунктир точка",
            "Пунктир точка точка"});
            this.Styles.Location = new System.Drawing.Point(40, 32);
            this.Styles.Name = "Styles";
            this.Styles.Size = new System.Drawing.Size(121, 21);
            this.Styles.TabIndex = 7;
            this.Styles.SelectedIndexChanged += new System.EventHandler(this.Styles_SelectedIndexChanged);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(37, 9);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(113, 13);
            this.Label.TabIndex = 6;
            this.Label.Text = "Выберите тип линии:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(105, 59);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(12, 59);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // LineStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 92);
            this.Controls.Add(this.Styles);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Name = "LineStyle";
            this.Text = "LineStyle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Styles;
        private System.Windows.Forms.Label Label;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
    }
}