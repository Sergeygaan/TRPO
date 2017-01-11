using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class СhangeMove : IFigureCommand
    {
        private List<Object> _seleckResultUndo = new List<Object>();
        private List<Object> _seleckResultRedo = new List<Object>();
        private List<Object> _figures;


        public СhangeMove(List<Object> SeleckResult, List<Object> Figures)
        {
            _figures = Figures;

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _seleckResultUndo.Add(SelectObject.CloneObject());
                _seleckResultUndo[i].IdFigure = SelectObject.IdFigure;
                i++;
            }

        }

        public void MouseUpMove(List<Object> SeleckResult)
        {
            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _seleckResultRedo.Add(SelectObject.CloneObject());
                _seleckResultRedo[i].IdFigure = SelectObject.IdFigure;
                i++;
            }

        }

        public void Execute()
        {
            foreach (Object SelectObject in _seleckResultRedo)
            {
                
                _figures[SelectObject.IdFigure] = SelectObject;

            }

        }

        public void Undo()
        {
            UndoFigure();
        }


        public void UndoFigure()
        {
            foreach (Object SelectObject in _seleckResultUndo)
            {
                
                _figures[SelectObject.IdFigure] = SelectObject;
                
            }

        }


    }
}
