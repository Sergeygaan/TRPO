using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint.CORE
{

    /// <summary>
    /// Класс, выполняющий удаление заливки у выбранных фигур
    /// </summary>
    class DeleteBackgroundFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая список выделенных фигур
        /// </summary>
        private List<Object> _seleckResult;

        /// <summary>
        /// Переменная, хранящая цвет заливки у выделенных фигур.
        /// </summary>
        private Color [] _brush;

        /// <summary>
        /// Переменная, хранящая значение о заливке фигур.
        /// </summary>
        private bool [] _fill;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Метод, выполняющий удаление заливки у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        public DeleteBackgroundFigure(List<Object> SeleckResult)
        {
 
            _brush = new Color[SeleckResult.Count];
            _fill = new bool[SeleckResult.Count];


            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _fill[i] = SelectObject.Fill;
                _brush[i] = SelectObject.BrushColor;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Fill = false;
                }
            }
            _operatorValue = "Удаление фона у выделенных фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Fill = false;
                }
            }
            _operatorValue = "Удаление фона у выделенных фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.BrushColor = _brush[i];
                    SelectObject.Fill = _fill[i];
                }
                i++;
            }
            _operatorValue = "Восстановление фона у выделенных фигур";
        }

        /// <summary>
        /// Метод, возвращающий строку с текущим действием.
        /// </summary>
        public string Operation()
        {
            return _operatorValue;
        }

    }
}
