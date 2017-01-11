using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    interface IFigureBuild
    {
        void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure);

        void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresBuild);

        void AddSupportPoint(IFigureCommand SelectObject);

        void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr);

        void ScaleFigure(MouseEventArgs e, IFigureCommand DrawObject, List<IFigureCommand> SelectedFigures);

    }
}
