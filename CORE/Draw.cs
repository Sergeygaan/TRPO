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

        private Pen _penFigure;
        private Pen _penFigureSelect;
        private PaintedObject _drawObject;
        private SupportObject _drawSupportObject;
        private Rectangle _rect;
        private SolidBrush _brush;

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
            _penFigureSelect = new Pen(Color.Black, 1);

        }

        //Отрисовка фигур и возвращение области выделения
        public void Paint(PaintEventArgs e, MainForm.FigureType _currentfigure, List<PointF> _points)
        {
            if (_points.Count != 0)
            {
                StyleFigure();

                switch (_currentfigure)
                {
                    case MainForm.FigureType.Rectangle:

                        e.Graphics.DrawRectangle(_penFigure, _ellipse.ShowRectangle(_points[0], _points[1]));

                        break;

                    case MainForm.FigureType.Line:

                        e.Graphics.DrawLine(_penFigure, _points[0], _points[1]);

                        break;

                    case MainForm.FigureType.Ellipse:

                        e.Graphics.DrawEllipse(_penFigure, _ellipse.ShowEllipse(_points[0], _points[1]));

                        break;

                    case MainForm.FigureType.RectangleSelect:

                        e.Graphics.DrawRectangle(_penFigureSelect, _ellipse.ShowRectangle(_points[0], _points[1]));

                        break;

                    case MainForm.FigureType.PoliLine:

                        if (_points.Count > 1)
                        {
                            PointF[] PointPoliLine = _points.ToArray();

                            e.Graphics.DrawLines(_penFigure, PointPoliLine);
                        }

                        break;

                    case MainForm.FigureType.Polygon:

                        if (_points.Count > 1)
                        {
                            PointF[] PointPolygon = _points.ToArray();

                            e.Graphics.DrawLines(_penFigure, PointPolygon);
                        }
                        break;
                }

                if (_points.Count > 1)
                {
                    _rect = _ellipse.ShowRectangle(_points[0], _points[1]);
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);

        }

        //Сохранение фигур
        public void MouseUp(MainForm.FigureType _currentfigure, List<PointF> _points)
        {
            StyleFigure();

            _drawObject = new PaintedObject(_penFigure, new GraphicsPath(), _brush, _currentfigure);

            switch (_currentfigure)
            {
                case MainForm.FigureType.Rectangle:

                    _drawObject.Path.AddRectangle(_ellipse.ShowRectangle(_points[0], _points[1]));

                    break;

                case MainForm.FigureType.Line:

                    _drawObject.Path.AddLine(_points[0], _points[1]);

                    break;

                case MainForm.FigureType.Ellipse:

                    _drawObject.Path.AddEllipse(_ellipse.ShowEllipse(_points[0], _points[1]));

                    break;

                case MainForm.FigureType.PoliLine:

                    PointF[] PointPoliLine = _points.ToArray();

                    _drawObject.Path.AddLines(PointPoliLine);

                    break;

                case MainForm.FigureType.Polygon:

                    PointF[] PointPolygon = _points.ToArray();

                    _drawObject.Path.AddLines(PointPolygon);

                    _drawObject.Path.CloseFigure();

                    break;

            }

            _drawObject.FigureStart = _points[0];
            _drawObject.FigureEnd = _points[1];
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

                    if (DrawObject.Brush != null)
                    {
                        DrawList.FillPath(DrawObject.Brush, DrawObject.Path);  //Заливка
                    }

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

                                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
                                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                                _drawSupportObject.ControlPointF = i;

                                SelectObject.AddListFigure(_drawSupportObject);
                            }
                        
            
                        break;

                    case MainForm.FigureType.Line:

                            for (int i = 0; i < SelectObject.PointSelect.Length; i++)
                            {
                                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
                                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                                _drawSupportObject.ControlPointF = i;

                                SelectObject.AddListFigure(_drawSupportObject);
                            }
                        

                        break;

                    case MainForm.FigureType.Ellipse:

                            for (int i = 0; i < SelectObject.PointSelect.Length; i = i + 3)
                            {
                                _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                                _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
                                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                                _drawSupportObject.ControlPointF = i;

                                SelectObject.AddListFigure(_drawSupportObject);
                            
                            }

                        break;

                    case MainForm.FigureType.PoliLine:
                    case MainForm.FigureType.Polygon:

                        for (int i = 0; i < SelectObject.PointSelect.Length; i++)
                        {
                            _drawSupportObject = new SupportObject(new Pen(MainForm.FigurePropertiesSupport.linecolor, 1), new GraphicsPath());
                            _drawSupportObject.Path.AddEllipse(_ellipse.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
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
        //Удаление фона у выбранных фигур
        public void DeleteBackgroundFigure(List<PaintedObject> SeleckResult)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {

                SelectObject.Brush = null;
               
            }

        }
        //Изменение фона у выбранных фигур
        public void СhangeBackgroundFigure(List<PaintedObject> SeleckResult, Color ColorСhangeBackground)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                if (SelectObject.CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Brush = new SolidBrush(ColorСhangeBackground);
                }

            }

        }
        public void СhangePenColorFigure(List<PaintedObject> SeleckResult, Color PenColor)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                SelectObject.Pen.Color = PenColor;
                //SelectObject.Brush = new SolidBrush(ColorСhangeBackground);

            }

        }

        public void СhangePenWidthFigure(List<PaintedObject> SeleckResult)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                SelectObject.Pen.Width = MainForm.FigureProperties.thickness;
            }

        }

        public void СhangePenStyleFigure(List<PaintedObject> SeleckResult)
        {
            foreach (PaintedObject SelectObject in SeleckResult)
            {
                SelectObject.Pen.DashStyle = MainForm.FigureProperties.dashstyle;
            }

        }

        //редактирование стилей для каждой фигуры
        public void StyleFigure()
        {
            _penFigure = new Pen(MainForm.FigureProperties.linecolor, MainForm.FigureProperties.thickness);
            _penFigure.DashStyle = MainForm.FigureProperties.dashstyle;

            if (MainForm.FigureProperties.fill == false)
            {
                _brush = null;
            }
            else
            {
                _brush = new SolidBrush(MainForm.FigureProperties.brushcolor);
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

        //Возвращение зоны выделения
        public Rectangle SeparationZone()
        {
            return _rect;
        }
    }
}
