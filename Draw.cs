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
        private SupportObject _drawSupportObject;

        private List<PaintedObject> _figures;//Список с объектами для прорисовки

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

        //Отрисовка фигур и возвращение области выделения
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

            e.Graphics.DrawImage(bmp, 0, 0);
        }

        //Сохранение фигур
        public void MouseUp(MainForm.FigureType _currentfigure, Point figurestart, Point figureend)
        {
            _drawObject = new PaintedObject(new Pen(Color.FromArgb(0, 123, 240), 1), new GraphicsPath(), _currentfigure);

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

            //Начальная и конечная координата
            _drawObject.FigureStart = figurestart;
            _drawObject.FigureEnd = figureend;
            _drawObject.IdFigure = _figures.Count;
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

                    foreach (SupportObject SuppportObject in DrawObject.SelectListFigure())
                    {
                        DrawList.DrawPath(SuppportObject.Pen, SuppportObject.Path);

                    }
                }
            }
        }


        //Отрисовка опорных точек
        public void SupportPoint(PaintEventArgs e, List<PaintedObject> SeleckResult)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                SelectObject.SelectFigure = false;
                SelectObject.ClearListFigure();

                //_drawSupportObject = new SupportObject(new Pen(Color.FromArgb(0, 123, 240), 1), new GraphicsPath());

                switch (SelectObject.CurrentFigure)
                {
                    case MainForm.FigureType.Rectangle:

                            for (int i = 0; i < SelectObject.PointSelect.Length; i++)
                            {

                                _drawSupportObject = new SupportObject(new Pen(Color.FromArgb(0, 123, 240), 1), new GraphicsPath());
                                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i]));
                                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                                _drawSupportObject.ControlPointF = i;

                                SelectObject.AddListFigure(_drawSupportObject);
                            }
                        
            
                        break;

                    case MainForm.FigureType.Line:

                            for (int i = 0; i < SelectObject.PointSelect.Length; i++)
                            {
                                _drawSupportObject = new SupportObject(new Pen(Color.FromArgb(0, 123, 240), 1), new GraphicsPath());
                                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i]));
                                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                                _drawSupportObject.ControlPointF = i;

                                SelectObject.AddListFigure(_drawSupportObject);
                            }
                        

                        break;

                    case MainForm.FigureType.Ellipse:

                            for (int i = 0; i < SelectObject.PointSelect.Length; i = i + 3)
                            {
                                _drawSupportObject = new SupportObject(new Pen(Color.FromArgb(0, 123, 240), 1), new GraphicsPath());
                                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i]));
                                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                                _drawSupportObject.ControlPointF = i;

                                SelectObject.AddListFigure(_drawSupportObject);
                            
                            }
                        break;

                }
            }

        }

        //Копирование выбранных фигур
        public void ReplicationFigure(List<PaintedObject> SeleckResult)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                _figures.Add(SelectObject.CloneObject());
                _figures[_figures.Count - 1].IdFigure = _figures.Count - 1;
            }

        }

        //Удаление выбранных фигуры
        public void DeleteFigure(List<PaintedObject> SeleckResult)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                _figures.RemoveAt(SelectObject.IdFigure);

                int i = 0;
                foreach (PaintedObject DrawObject in _figures)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }
            }
             
        }

        // Отчищает список с фигурами
        public void Clear()
        {
            _figures.Clear();
        }

        //Возвращяет список со всеми фигурами
        public List<PaintedObject> FiguresList()
        {
            return _figures;
        }

    }
}
