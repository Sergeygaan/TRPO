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
    class СhangePenWidth : IFigureCommand
    {
        private List<Object> _seleckResult;

        public int _currentThickness;

        private float[] _penWidth;
        private string _operatorValue;

        public СhangePenWidth(List<Object> SeleckResult, int CurrentThickness)
        {
            _currentThickness = CurrentThickness;

            _penWidth = new float[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _penWidth[i] = SelectObject.Pen.Width;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in SeleckResult)
            {
                SelectObject.Pen.Width = _currentThickness;
            }
            _operatorValue = "Изменение толщины линии";
        }

        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.Width = _currentThickness;
            }
            _operatorValue = "Изменение толщины линии";
        }

        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.Width = _penWidth[i];
                i++;
            }
            _operatorValue = "Отмена изменения толщины линии";
        }


        public string Operation()
        {
            return _operatorValue;
        }

    }
}
