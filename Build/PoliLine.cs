﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    class PoliLine : IFigureBuild
    {
        private AddPoliLine _addFigurePoliLine;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private SupportObject _drawSupportObject;

        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            if (Points.Count > 1)
            {
                PointF[] PointPoliLine = Points.ToArray();

                e.Graphics.DrawLines(PenFigure, PointPoliLine);
            }
        }

        public void AddFigure(Object DrawObject, List<PointF> Points, List<IFigureCommand> FiguresBuild, List<Object> Figures)
        {

            _addFigurePoliLine = new AddPoliLine();
            _addFigurePoliLine.AddFigure(DrawObject, Points, Figures);
            
            _addFigurePoliLine.Output().FigureStart = Points[0];
            _addFigurePoliLine.Output().FigureEnd = Points[1];
            _addFigurePoliLine.Output().IdFigure = Figures.Count;

            Figures.Add(_addFigurePoliLine.Output());
            FiguresBuild.Add(_addFigurePoliLine);

        }

        public void AddSupportPoint(Object SelectObject)
        {
            for (int i = 0; i < SelectObject.PointSelect.Length; i++)
            {
                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                _drawSupportObject.ControlPointF = i;

                SelectObject.AddListFigure(_drawSupportObject);
            }
        }

        public void ScaleSelectFigure(Object SelectObject, SupportObject SupportObj, int DeltaX, int DeltaY, EditObject EdipParametr)
        {
            if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[1].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[1].Y != 0))
            {
                SelectObject.PointSelect = SelectObject.Path.PathPoints;
            }
            EdipParametr.EditObjectPoliLine(SelectObject, SupportObj, DeltaX, DeltaY);

        }

        public void ScaleFigure(MouseEventArgs e, Object DrawObject, List<Object> SelectedFigures)
        {
            
            for (int i = 1; i < DrawObject.Path.PathPoints.Length; i++)
            {
                float PoliLineX, PoliLineY;

                PoliLineY = (-(DrawObject.Path.PathPoints[i - 1].X * DrawObject.Path.PathPoints[i].Y - DrawObject.Path.PathPoints[i].X * DrawObject.Path.PathPoints[i - 1].Y) - ((DrawObject.Path.PathPoints[i - 1].Y - DrawObject.Path.PathPoints[i].Y) * e.Location.X)) / (DrawObject.Path.PathPoints[i].X - DrawObject.Path.PathPoints[i - 1].X);

                PoliLineX = (-(DrawObject.Path.PathPoints[i - 1].X * DrawObject.Path.PathPoints[i].Y - DrawObject.Path.PathPoints[i].X * DrawObject.Path.PathPoints[i - 1].Y) - ((DrawObject.Path.PathPoints[i].X - DrawObject.Path.PathPoints[i - 1].X) * e.Location.Y)) / (DrawObject.Path.PathPoints[i - 1].Y - DrawObject.Path.PathPoints[i].Y);

                if ((e.Location.Y >= PoliLineY - DrawObject.Pen.Width - 2) && (e.Location.Y <= PoliLineY + DrawObject.Pen.Width + 2) || (e.Location.X >= PoliLineX - DrawObject.Pen.Width - 2) && (e.Location.X <= PoliLineX + DrawObject.Pen.Width + 2))
                {
                    DrawObject.PointSelect = DrawObject.Path.PathPoints;
                    DrawObject.SelectFigure = true;
                    //DrawObject.Pen.Width += 1;
                    SelectedFigures.Add(DrawObject);
                }
            }
        }


    }
}
