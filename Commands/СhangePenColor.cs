using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class СhangePenColor : IFigureCommand
    {
        private List<Object> _seleckResult;

        public Color _currentColor;

        private Color [] _penColor;

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

        }

        public void Execute()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.Color = _currentColor;
            }
        }

        public void Undo()
        {
            UndoFigure();
        }


        public void UndoFigure()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                SelectObject.Pen.Color = _penColor[i];
                i++;
            }
        }


    }
}
