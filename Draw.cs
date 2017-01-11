using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    class DrawPaint
    {

        private СonstructionFigure _ellipse;

        private Pen _pen;
        private PaintedObject _drawObject;

        List<PaintedObject> _figures;//Список с объектами для прорисовки
        PaintedObject currObj;//Объект, который в данный момент перемещается
        Point oldPoint;
        Bitmap bmp;

        public int _widthDraw;
        public int _heightDraw;

        public enum FigureType
        {
            Rectangle, Square, Ellipse, Circle, Curve, Line
        }

        public DrawPaint(int Width, int Height)
        {
            _widthDraw = Width;
            _heightDraw = Height;

            _figures = new List<PaintedObject>();
            bmp = new Bitmap(Width, Height);

            _ellipse = new СonstructionFigure();
            _pen = new Pen(Color.Black, 1);

        }

        //Отрисовка фигур
        public void Paint(PaintEventArgs e, MainForm.FigureType _currentfigure, Point figurestart, Point figureend)
        {
            switch (_currentfigure)
            {
                case MainForm.FigureType.Rectangle:

                    e.Graphics.DrawRectangle(_pen, _ellipse.ShowRectangle(figurestart, figureend));

                    break;

                case MainForm.FigureType.Line:

                    e.Graphics.DrawLine(_pen, figurestart, figureend);

                    break;

                case MainForm.FigureType.Ellipse:

                    e.Graphics.DrawEllipse(_pen, _ellipse.ShowEllipse(figurestart, figureend));

                    break;

            }

            //foreach (PaintedObject DrawObject in _figures)
            //{
            //    RectangleF rec = DrawObject.Path.GetBounds();

            //    rec.Inflate(10, 0);

            //    e.Graphics.DrawEllipse(_pen, rec);
            //}

            e.Graphics.DrawImage(bmp, 0, 0);
        }

        //Сохранение фигур
        public void MouseUp(MainForm.FigureType _currentfigure, Point figurestart, Point figureend)
        {

            _drawObject = new PaintedObject(new Pen(Color.FromArgb(0, 123, 240), 1), new GraphicsPath());

            switch (_currentfigure)
            {
                case MainForm.FigureType.Rectangle:

                    _drawObject.Path.AddRectangle(_ellipse.ShowRectangle(figurestart, figureend));

                    break;

                case MainForm.FigureType.Line:

                    _drawObject.Path.AddLine(figurestart, figureend);

                    break;

                case MainForm.FigureType.Ellipse:

                    _drawObject.Path.AddEllipse(_ellipse.ShowEllipse(figurestart, figureend));

                    break;

            }
            //Фигура которая рисовалась
            _drawObject.CurrentFigure = _currentfigure;
            //Начальная и конечная координата
            _drawObject.FigureStart = figurestart;
            _drawObject.FigureEnd = figureend;

            _figures.Add(_drawObject);

        }


        public void RefreshBitmap()
        {
            if (bmp != null) bmp.Dispose();
            bmp = new Bitmap(_widthDraw, _heightDraw);
            //Прорисовка всех объектов из списка
            using (Graphics DrawList = Graphics.FromImage(bmp))
            {
                foreach (PaintedObject DrawObject in _figures)
                {
                    DrawList.DrawPath(DrawObject.Pen, DrawObject.Path);
                }
            }
        }


        //Отрисовка опорных точек
        public void SupportPoint(PaintEventArgs e, PointF[] SupporFigureStart, PaintedObject currObj)
        {

            switch (currObj.CurrentFigure)
            {
                case MainForm.FigureType.Rectangle:

                    for (int i = 0; i < SupporFigureStart.Length; i++)
                    {
                        e.Graphics.DrawEllipse(_pen, _ellipse.SelectFigure(SupporFigureStart[i]));
                    }

                    break;

                case MainForm.FigureType.Line:

                    for (int i = 0; i < SupporFigureStart.Length; i++)
                    {
                        e.Graphics.DrawEllipse(_pen, _ellipse.SelectFigure(SupporFigureStart[i]));
                    }

                    break;

                case MainForm.FigureType.Ellipse:

                    for (int i = 0; i < SupporFigureStart.Length; i = i + 3)
                    {
                        e.Graphics.DrawEllipse(_pen, _ellipse.SelectFigure(SupporFigureStart[i]));
                    }

                    break;

            }


        }

        public void Clear()
        {
            _figures.Clear();
        }

        public List<PaintedObject> FiguresList()
        {
            return _figures;
        }

    }
}
