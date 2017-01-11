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
    class СhangeBackgroundFigure : IFigureCommand
    {
        private List<Object> _seleckResult;

        private Color [] _brushColor;
        private Color _brushCurrentColor;
        private string _operatorValue;
        private bool [] _fillFigure;

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


        public string Operation()
        {
            return _operatorValue;
        }
    }
}
