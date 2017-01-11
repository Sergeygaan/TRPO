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
    class DeleteBackgroundFigure : IFigureCommand
    {
        private List<Object> _seleckResult;


        private Color [] _brush;
        private bool [] _fill;
        private string _operatorValue;

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


        public string Operation()
        {
            return _operatorValue;
        }

    }
}
