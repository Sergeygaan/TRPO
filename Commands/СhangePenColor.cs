using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    class СhangePenColor : IFigureCommand
    {
        private List<Object> _seleckResult;

        public Color _currentColor;

        private Color [] _penColor;

        private string _operatorValue;

        public СhangePenColor(List<Object> SeleckResult, Color CurrentColor)
        {
            _currentColor = CurrentColor;

            _penColor = new Color[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _penColor[i] = SelectObject.Pen.Color;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in SeleckResult)
            {
                SelectObject.Pen.Color = _currentColor;
            }
            _operatorValue = "Изменение цвета линии";
        }

        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.Color = _currentColor;
            }
            _operatorValue = "Изменение цвета линии";
        }

        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.Color = _penColor[i];
                i++;
            }

            _operatorValue = "Отмена изменения цвета линии";
        }


        public string Operation()
        {
            return _operatorValue;
        }

    }
}
