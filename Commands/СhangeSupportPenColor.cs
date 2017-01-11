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
    class СhangeSupportPenColor : IFigureCommand
    {
        private List<Object> _seleckResult;
        private Color _nextColor;
        private Color[] _penColor;
        private string _operatorValue;

        public СhangeSupportPenColor(Color NextColor, List<Object> SeleckResult)
        {
            _nextColor = NextColor;


            _penColor = new Color[SeleckResult.Count * 4];

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                foreach (SupportObject SuppportObject in SelectObject.SelectListFigure())
                {
                    _penColor[i] = SuppportObject.Pen.Color;
                    i++;
                }
     
            }

            _operatorValue = "Изменение цвета линии опорных точек";
        }

        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                foreach (SupportObject SuppportObject in SelectObject.SelectListFigure())
                {
                    SuppportObject.Pen.Color = _nextColor;
                    
                }

            }

            _operatorValue = "Изменение цвета линии опорных точек";
        }

        public void Undo()
        {
            int i = 0;
            foreach (Object SelectObject in _seleckResult)
            {
                foreach (SupportObject SuppportObject in SelectObject.SelectListFigure())
                {
                    SuppportObject.Pen.Color = _penColor[i];
                    i++;
                }

            }

            _operatorValue = "Отмена изменения цвета линии опорных точек";
        }


        public string Operation()
        {
            return _operatorValue;
        }

    }
}
