using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class СhangeBackgroundFigure : IFigureCommand
    {
        private List<Object> _seleckResult;

        public Color _currentColor;

        private SolidBrush [] _brush;

        public СhangeBackgroundFigure(List<Object> SeleckResult, Color CurrentColor)
        {
            _currentColor = CurrentColor;

            _brush = new SolidBrush[SeleckResult.Count];


            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _brush[i] = SelectObject.Brush;
                i++;
                
            }

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Brush = new SolidBrush(_currentColor);
                }
            }

        }

        public void Execute()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Brush = new SolidBrush(_currentColor);
                }
            }
        }

        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Brush = _brush[i];
                }
                i++;
            }
        }

    }
}
