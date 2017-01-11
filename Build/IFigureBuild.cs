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
        void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure);

        void AddFigure(Object DrawObject, List<PointF> Points, List<IFigureCommand> FiguresBuild, List<Object> Figures);

        void AddSupportPoint(Object SelectObject);

        void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr);

        void ScaleFigure(MouseEventArgs e, Object DrawObject, List<Object> SelectedFigures);

    }
}
