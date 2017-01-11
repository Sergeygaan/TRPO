using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    /// <summary>
    /// Класс, выполняющий изменение цвета заливки у выбранных фигур
    /// </summary>
    class СhangeBackgroundFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур.
        /// </summary>
        private List<Object> _seleckResult;

        /// <summary>
        /// Переменная, хранящая цвет заливки до его изменения.
        /// </summary>
        private Color [] _brushColor;

        /// <summary>
        /// Переменная, хранящая цвет новой заливки.
        /// </summary>
        private Color _brushCurrentColor;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Переменная, хранящая знчение об использовании заливки.
        /// </summary>
        private bool [] _fillFigure;

        /// <summary>
        /// Метод, выполняющий изменения цвета заливки у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "CurrentColor">Переменная, хранящая новый цвет заливки фигур.</para>
        public СhangeBackgroundFigure(List<Object> SeleckResult, Color CurrentColor)
        {
            _brushCurrentColor = CurrentColor;

            _brushColor = new Color[SeleckResult.Count];
            _fillFigure = new bool[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _brushColor[i] = SelectObject.BrushColor;
                _fillFigure[i] = SelectObject.Fill;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.BrushColor = _brushCurrentColor;
                    SelectObject.Fill = true;
                }
            }
            _operatorValue = "Изменение фона выделенных фигур";
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
                    SelectObject.BrushColor = _brushCurrentColor;
                    SelectObject.Fill = true;

                }
            }
            _operatorValue = "Изменение фона выделенных фигур";
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
                    SelectObject.BrushColor = _brushColor[i];
                    SelectObject.Fill = _fillFigure[i];
                }
                i++;
            }
            _operatorValue = "Отмена изменения фона выделенных фигур";
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
