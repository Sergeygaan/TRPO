﻿namespace PaintedObjectsMoving
{
    partial class NewFileDialog
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.UserHeight = new System.Windows.Forms.TextBox();
            this.UserWidth = new System.Windows.Forms.TextBox();
            this.UserSize = new System.Windows.Forms.CheckBox();
            this.SizeGroup = new System.Windows.Forms.GroupBox();
            this.Size3 = new System.Windows.Forms.RadioButton();
            this.Size2 = new System.Windows.Forms.RadioButton();
            this.Size1 = new System.Windows.Forms.RadioButton();
            this.CANCELbutton = new System.Windows.Forms.Button();
            this.OKbutton = new System.Windows.Forms.Button();
            this.WidthHelp = new System.Windows.Forms.ToolTip(this.components);
            this.HeightHelp = new System.Windows.Forms.ToolTip(this.components);
            this.SizeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "х";
            // 
            // UserHeight
            // 
            this.UserHeight.Location = new System.Drawing.Point(193, 36);
            this.UserHeight.Name = "UserHeight";
            this.UserHeight.Size = new System.Drawing.Size(39, 20);
            this.UserHeight.TabIndex = 12;
            // 
            // UserWidth
            // 
            this.UserWidth.Location = new System.Drawing.Point(130, 36);
            this.UserWidth.Name = "UserWidth";
            this.UserWidth.Size = new System.Drawing.Size(39, 20);
            this.UserWidth.TabIndex = 11;
            // 
            // UserSize
            // 
            this.UserSize.AutoSize = true;
            this.UserSize.Location = new System.Drawing.Point(129, 12);
            this.UserSize.Name = "UserSize";
            this.UserSize.Size = new System.Drawing.Size(106, 17);
            this.UserSize.TabIndex = 10;
            this.UserSize.Text = "Ввести вручную";
            this.UserSize.UseVisualStyleBackColor = true;
            // 
            // SizeGroup
            // 
            this.SizeGroup.Controls.Add(this.Size3);
            this.SizeGroup.Controls.Add(this.Size2);
            this.SizeGroup.Controls.Add(this.Size1);
            this.SizeGroup.Location = new System.Drawing.Point(12, 12);
            this.SizeGroup.Name = "SizeGroup";
            this.SizeGroup.Size = new System.Drawing.Size(111, 100);
            this.SizeGroup.TabIndex = 9;
            this.SizeGroup.TabStop = false;
            this.SizeGroup.Text = "Размер рисунка";
            // 
            // Size3
            // 
            this.Size3.AutoSize = true;
            this.Size3.Location = new System.Drawing.Point(7, 67);
            this.Size3.Name = "Size3";
            this.Size3.Size = new System.Drawing.Size(66, 17);
            this.Size3.TabIndex = 2;
            this.Size3.TabStop = true;
            this.Size3.Text = "800x600";
            this.Size3.UseVisualStyleBackColor = true;
            // 
            // Size2
            // 
            this.Size2.AutoSize = true;
            this.Size2.Location = new System.Drawing.Point(7, 44);
            this.Size2.Name = "Size2";
            this.Size2.Size = new System.Drawing.Size(66, 17);
            this.Size2.TabIndex = 1;
            this.Size2.TabStop = true;
            this.Size2.Text = "640x480";
            this.Size2.UseVisualStyleBackColor = true;
            // 
            // Size1
            // 
            this.Size1.AutoSize = true;
            this.Size1.Location = new System.Drawing.Point(7, 20);
            this.Size1.Name = "Size1";
            this.Size1.Size = new System.Drawing.Size(66, 17);
            this.Size1.TabIndex = 0;
            this.Size1.TabStop = true;
            this.Size1.Text = "320х240";
            this.Size1.UseVisualStyleBackColor = true;
            // 
            // CANCELbutton
            // 
            this.CANCELbutton.Location = new System.Drawing.Point(156, 118);
            this.CANCELbutton.Name = "CANCELbutton";
            this.CANCELbutton.Size = new System.Drawing.Size(75, 23);
            this.CANCELbutton.TabIndex = 8;
            this.CANCELbutton.Text = "Отмена";
            this.CANCELbutton.UseVisualStyleBackColor = true;
            this.CANCELbutton.Click += new System.EventHandler(this.CANCELbutton_Click);
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(19, 118);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 7;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            this.OKbutton.Click += new System.EventHandler(this.OKbutton_Click);
            // 
            // WidthHelp
            // 
            this.WidthHelp.IsBalloon = true;
            // 
            // HeightHelp
            // 
            this.HeightHelp.IsBalloon = true;
            // 
            // NewFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 153);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserHeight);
            this.Controls.Add(this.UserWidth);
            this.Controls.Add(this.UserSize);
            this.Controls.Add(this.SizeGroup);
            this.Controls.Add(this.CANCELbutton);
            this.Controls.Add(this.OKbutton);
            this.Name = "NewFileDialog";
            this.Text = "NewFileDialog";
            this.SizeGroup.ResumeLayout(false);
            this.SizeGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserHeight;
        private System.Windows.Forms.TextBox UserWidth;
        private System.Windows.Forms.CheckBox UserSize;
        private System.Windows.Forms.GroupBox SizeGroup;
        private System.Windows.Forms.RadioButton Size3;
        private System.Windows.Forms.RadioButton Size2;
        private System.Windows.Forms.RadioButton Size1;
        private System.Windows.Forms.Button CANCELbutton;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.ToolTip WidthHelp;
        private System.Windows.Forms.ToolTip HeightHelp;
    }
}