using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class СhangePenStyleFigure : IFigureCommand
    {
        private List<IFigureCommand> _seleckResult;


        public void AddFigure(List<IFigureCommand> SeleckResult)
        {
            _seleckResult = SeleckResult;
        }

        public void Execute()
        {
            foreach (var SelectObject in _seleckResult)
            {
                SelectObject.Output().Pen.DashStyle = MainForm.FigureProperties.dashstyle;
            }
        }

        public void Undo()
        {

        }

        public Object Output()
        {
            return _seleckResult[0].Output();
        }


    }
}
