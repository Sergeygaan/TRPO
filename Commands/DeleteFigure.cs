using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class DeleteFigure : IFigureCommand
    {
        private List<Object> _seleckResult;
        private List<Object> _saveFigure;
        private List<Object> _figure;

        public DeleteFigure(List<Object> SeleckResult, List<Object> Figures)
        {
            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            _saveFigure = Figures.GetRange(0, Figures.Count);

            _figure = Figures;

            foreach (Object SelectObject in _seleckResult)
            {
                _figure.RemoveAt(SelectObject.IdFigure);
                int i = 0;
                foreach (Object DrawObject in _figure)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }

            }

        }

        public void Execute()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                _figure.RemoveAt(SelectObject.IdFigure);
                int i = 0;
                foreach (Object DrawObject in _figure)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }
            }

        }

        public void Undo()
        {
            //_figure.RemoveRange(0, _saveFigure.Count - 1);
            _figure.Clear();
            _figure.InsertRange(0, _saveFigure);


            int i = 0;
            foreach (Object DrawObject in _figure)
            {
                DrawObject.IdFigure = i;
                i++;
            }

        }

    }
}
