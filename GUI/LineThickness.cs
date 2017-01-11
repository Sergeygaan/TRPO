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
    /// <summary>
    /// Класс, выполняющий изменения толщины линии.
    /// </summary>
    public partial class LineThickness : Form
    {
        /// <summary>
        /// Метод, выполняющий инициализирующий остальные объекты.
        /// </summary>
        public LineThickness()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, выполняющий сохранения толщины линии.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ThicknessTrackBar_Scroll(object sender, EventArgs e)
        {
            ThicknessTrackBar.Minimum = 1;      //минимальное возможное значение
            ThicknessTrackBar.Maximum = 15;     //максимальное возможное значение
            ThicknessTrackBar.TickFrequency = 1;    //шаг
            ThicknessTrackBar.LargeChange = 2;      //прибавление в случае прокрутки в большую сторону
            ThicknessTrackBar.SmallChange = 2;      //уменьшение в случае прокрутки в меньшую сторону
            ThicknessValue.Text = "" + ThicknessTrackBar.Value.ToString();      //отображение значения
        }

        /// <summary>
        /// Метод, выполняющий сохранение толщины линии.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;        //возвращаем значение"ОК"
            ChildForm.Thickness = ThicknessTrackBar.Value;          //возвращаем значение толщины
            Close();
        }

        /// <summary>
        /// Метод, выполняющий отмену сохранения толщины линии.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;        //возвращаем значение"Cancel"
            Close();
        }
    }

}
