using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class СhangePenStyle : IFigureCommand
    {
        private List<Object> _seleckResult;

        private DashStyle [] _penWidth;

        private DashStyle _dashStyle;

        private string _operatorValue;

        public СhangePenStyle(List<Object> SeleckResult, DashStyle DashStyle)
        {
            _dashStyle = DashStyle;

            _penWidth = new DashStyle[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _penWidth[i] = SelectObject.Pen.DashStyle;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in SeleckResult)
            {
                SelectObject.Pen.DashStyle = _dashStyle;
            }
            _operatorValue = "Изменение стиля линии";
        }

        public void Execute()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.DashStyle = _dashStyle;
            }
            _operatorValue = "Изменение стиля линии";
        }

        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.DashStyle = _penWidth[i];
                i++;
            }

            _operatorValue = "Отмена измменения стиля линии";
        }

        public string Operation()
        {
            return _operatorValue;
        }
    }
}
