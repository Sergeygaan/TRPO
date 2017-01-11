using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    public partial class LineThickness : Form
    {
        public LineThickness()
        {
            InitializeComponent();
        }

        private void LineThickness_Load(object sender, EventArgs e)
        {

        }

        private void ThicknessTrackBar_Scroll(object sender, EventArgs e)
        {
            ThicknessTrackBar.Minimum = 1;      //минимальное возможное значение
            ThicknessTrackBar.Maximum = 15;     //максимальное возможное значение
            ThicknessTrackBar.TickFrequency = 1;    //шаг
            ThicknessTrackBar.LargeChange = 2;      //прибавление в случае прокрутки в большую сторону
            ThicknessTrackBar.SmallChange = 2;      //уменьшение в случае прокрутки в меньшую сторону
            ThicknessValue.Text = "" + ThicknessTrackBar.Value.ToString();      //отображение значения
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;        //возвращаем значение"ОК"
            MainForm.Thickness = ThicknessTrackBar.Value;          //возвращаем значение толщины
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;        //возвращаем значение"Cancel"
            this.Close();
        }
    }

}
