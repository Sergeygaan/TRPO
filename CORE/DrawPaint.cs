using PaintedObjectsMoving.CORE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    class DrawPaint
    {
        private List<IFigureCommand> _iFigureCommand = new List<IFigureCommand>();
        private List<Object> _figure = new List<Object>();
        private СonstructionFigure _ellipse;

        private Pen _penFigure;
        private Object _drawObject;
        private Rectangle _rect;
        private SolidBrush _brush;


        private СhangePenStyleFigure _penStyleFigure;
        // private List<Object> _figures;//Список с объектами для прорисовки

        Bitmap bmp;

        private int _widthDraw;
        private int _heightDraw;

        public DrawPaint(int Width, int Height)
        {
            _widthDraw = Width;
            _heightDraw = Height;

            
            bmp = new Bitmap(Width, Height);

            _ellipse = new СonstructionFigure();

        }

        //Отрисовка фигур и возвращение области выделения
        public void Paint(PaintEventArgs e, MainForm.FigureType _currentfigure, List<PointF> _points, List<IFigureBuild> FiguresBuild)
        {
            if (_points.Count != 0)
            {
                StyleFigure();

                FiguresBuild[(int)_currentfigure].PaintFigure(e, _points, _penFigure);     // Отрисовка нужной фигуры

                if (_points.Count > 1)
                {
                    _rect = _ellipse.ShowRectangle(_points[0], _points[1]);
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);

        }

        //Сохранение фигур
        public void MouseUp(MainForm.FigureType _currentfigure, List<PointF> _points, List<IFigureBuild> FiguresBuild)
        {
            StyleFigure();

            _drawObject = new Object(_penFigure, new GraphicsPath(), _brush, _currentfigure);

            FiguresBuild[(int)_currentfigure].AddFigure(_drawObject, _points, _iFigureCommand);

         
        }


        public void RefreshBitmap()
        {
            if (bmp != null) bmp.Dispose();

            bmp = new Bitmap(_widthDraw, _heightDraw);
            //Прорисовка всех объектов из списка

            using (Graphics DrawList = Graphics.FromImage(bmp))
            {
                foreach (var DrawObject in _iFigureCommand)
                {
                    DrawList.DrawPath(DrawObject.Output().Pen, DrawObject.Output().Path);

                    if (DrawObject.Output().Brush != null)
                    {
                        DrawList.FillPath(DrawObject.Output().Brush, DrawObject.Output().Path);  //Заливка
                    }

                    foreach (SupportObject SuppportObject in DrawObject.Output().SelectListFigure())
                    {
                        DrawList.DrawPath(SuppportObject.Pen, SuppportObject.Path);
                    }
                }
            }
        }


        //public void RefreshBitmap()
        //{
        //    if (bmp != null) bmp.Dispose();

        //    bmp = new Bitmap(_widthDraw, _heightDraw);
        //    //Прорисовка всех объектов из списка

        //    using (Graphics DrawList = Graphics.FromImage(bmp))
        //    {
        //        foreach (Object DrawObject in _iFigureCommand)
        //        {
        //            DrawList.DrawPath(DrawObject.Pen, DrawObject.Path);

        //            if (DrawObject.Brush != null)
        //            {
        //                DrawList.FillPath(DrawObject.Brush, DrawObject.Path);  //Заливка
        //            }

        //            foreach (SupportObject SuppportObject in DrawObject.SelectListFigure())
        //            {
        //                DrawList.DrawPath(SuppportObject.Pen, SuppportObject.Path);
        //            }
        //        }
        //    }
        //}


        //Отрисовка опорных точек
        public void SupportPoint(PaintEventArgs e, List<IFigureCommand> SeleckResult, List<IFigureBuild> FiguresBuild)
        {
            foreach (var SelectObject in SeleckResult)
            {
                if (SelectObject.Output().SelectFigure == true)
                {
                    SelectObject.Output().SelectFigure = false;
                    SelectObject.Output().ClearListFigure();

                    FiguresBuild[(int)SelectObject.Output().CurrentFigure].AddSupportPoint(SelectObject);

                }

            }
        }

        //Копирование выбранных фигур
        public void ReplicationFigure(List<IFigureCommand> SeleckResult)
        {
            foreach (var SelectObject in SeleckResult)
            {
                //_iFigureCommand.Add(SelectObject.Output().CloneObject());
                //_iFigureCommand[_iFigureCommand.Count - 1].Output().IdFigure = _iFigureCommand.Count - 1;
            }

        }

        //Удаление выбранных фигуры
        public void DeleteFigure(List<IFigureCommand> SeleckResult)
        {
            foreach (var SelectObject in SeleckResult)
            {
                _iFigureCommand.RemoveAt(SelectObject.Output().IdFigure);

                int i = 0;
                foreach (var DrawObject in _iFigureCommand)
                {
                    DrawObject.Output().IdFigure = i;
                    i++;
                }
            }
             
        }
        //Удаление фона у выбранных фигур
        public void DeleteBackgroundFigure(List<IFigureCommand> SeleckResult)
        {
            foreach (var SelectObject in SeleckResult)
            {

                SelectObject.Output().Brush = null;
               
            }

        }
        //Изменение фона у выбранных фигур
        public void СhangeBackgroundFigure(List<IFigureCommand> SeleckResult, Color ColorСhangeBackground)
        {
            foreach (var SelectObject in SeleckResult)
            {
                if (SelectObject.Output().CurrentFigure != MainForm.FigureType.PoliLine)
                {
                    SelectObject.Output().Brush = new SolidBrush(ColorСhangeBackground);
                }

            }

        }
        public void СhangePenColorFigure(List<IFigureCommand> SeleckResult, Color PenColor)
        {
            foreach (var SelectObject in SeleckResult)
            {

                SelectObject.Output().Pen.Color = PenColor;
                //SelectObject.Brush = new SolidBrush(ColorСhangeBackground);

            }

        }

        public void СhangePenWidthFigure(List<IFigureCommand> SeleckResult)
        {
            foreach (var SelectObject in SeleckResult)
            {
                SelectObject.Output().Pen.Width = MainForm.FigureProperties.thickness;
            }

        }

        public void СhangePenStyleFigure(List<IFigureCommand> SeleckResult)
        {
            _penStyleFigure = new СhangePenStyleFigure();
            _penStyleFigure.AddFigure(SeleckResult);
            _penStyleFigure.Execute();

            _iFigureCommand.Add(_penStyleFigure);

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
            _iFigureCommand.Clear();
        }


        //Возвращяет список со всеми фигурами
        public List<IFigureCommand> IFigureCommand()
        {
            return _iFigureCommand;
        }

        //Возвращение зоны выделения
        public Rectangle SeparationZone()
        {
            return _rect;
        }
    }
}
