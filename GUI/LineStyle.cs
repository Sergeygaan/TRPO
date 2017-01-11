using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий изменения стиля линии.
    /// </summary>
    public partial class LineStyle : Form
    {
        /// <summary>
        /// Метод, выполняющий инициализирующий остальные объекты.
        /// </summary>
        public LineStyle()
        {
            InitializeComponent();
            Styles.SelectedIndex = 0;
        }

        /// <summary>
        /// Метод, выполняющий сохранение стилей.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;        //возвращаем значение"ОК"
            Close();
        }

        /// <summary>
        /// Метод, выполняющий отмену сохранения стилей.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;        //возвращем значение "Canсel"
            Close();
        }

        /// <summary>
        /// Метод, выполняющий сохранения стилей.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void Styles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Point p1 = new Point(162, 42);     //координаты для линии-примера
            Point p2 = new Point(262, 42);
            Pen example = new Pen(ChildForm.FigureProperties.linecolor, ChildForm.FigureProperties.thickness);       //перо для примера
            Graphics linetype = CreateGraphics();
            Styles.DropDownStyle = ComboBoxStyle.DropDownList;     //стиль списка
            switch (Styles.SelectedItem.ToString())      //в зависимости от выбранного значения рисуем рядом определенную линию - пример
            {
                case "Сплошная":
                    linetype.Clear(LineStyle.DefaultBackColor);
                    example.DashStyle = ChildForm.StyleOfLine = System.Drawing.Drawing2D.DashStyle.Solid;
                    linetype.DrawLine(example, p1, p2);
                    break;
                case "Пунктир":
                    linetype.Clear(LineStyle.DefaultBackColor);
                    example.DashStyle = ChildForm.StyleOfLine = System.Drawing.Drawing2D.DashStyle.Dash;
                    linetype.DrawLine(example, p1, p2);
                    break;
                case "Точка":
                    linetype.Clear(LineStyle.DefaultBackColor);
                    example.DashStyle = ChildForm.StyleOfLine = System.Drawing.Drawing2D.DashStyle.Dot;
                    linetype.DrawLine(example, p1, p2);
                    break;
                case "Пунктир точка":
                    linetype.Clear(LineStyle.DefaultBackColor);
                    example.DashStyle = ChildForm.StyleOfLine = System.Drawing.Drawing2D.DashStyle.DashDot;
                    linetype.DrawLine(example, p1, p2);
                    break;
                case "Пунктир точка точка":
                    linetype.Clear(LineStyle.DefaultBackColor);
                    example.DashStyle = ChildForm.StyleOfLine = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                    linetype.DrawLine(example, p1, p2);
                    break;
            }
        }
    }
}
