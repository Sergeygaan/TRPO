﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class Ellipses : IFigureBuild
    {
        private AddEllipse _addFigureEllipse;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> _points, Pen _penFigure)
        {
            e.Graphics.DrawEllipse(_penFigure, _ellipse.ShowRectangle(_points[0], _points[1]));
        }

        public void AddFigure(Object DrawObject, List<PointF> _points, List<IFigureCommand> _figuresBuild, List<Object> _figure)
        {
            _addFigureEllipse = new AddEllipse();
            _addFigureEllipse.AddFigure(DrawObject, _points);
            _addFigureEllipse.Execute();

            _addFigureEllipse.Output().FigureStart = _points[0];
            _addFigureEllipse.Output().FigureEnd = _points[1];
            _addFigureEllipse.Output().IdFigure = _figuresBuild.Count;

            _figure.Add(_addFigureEllipse.Output());
            _figuresBuild.Add(_addFigureEllipse);
        }

        public void AddSupportPoint(IFigureCommand SelectObject)
        {
            for (int i = 0; i < SelectObject.Output().PointSelect.Length; i += 3)
            {
                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.Output().PointSelect[i], SelectObject.Output().Pen.Width));
                _drawSupportObject.IdFigure = SelectObject.Output().IdFigure;
                _drawSupportObject.ControlPointF = i;

                SelectObject.Output().AddListFigure(_drawSupportObject);
            }

        }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr)
        {
            EdipParametr.EditObjectEllepse(SelectObject, SupportObj, DeltaX, DeltaY);

            SelectObject.PointSelect = SelectObject.Path.PathPoints;
        }

        public void ScaleFigure(MouseEventArgs e, IFigureCommand DrawObject, List<IFigureCommand> SelectedFigures)
        {
            DrawObject.Output().PointSelect = DrawObject.Output().Path.PathPoints;
            DrawObject.Output().SelectFigure = true;
            //DrawObject.Pen.Width += 1;
            SelectedFigures.Add(DrawObject);
        }


    }
}
