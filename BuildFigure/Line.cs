﻿using MyPaint.ObjectType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint.Build
{
    /// <summary>
    /// Класс, выполнящий различные действия над линией.
    /// </summary>
    public class Line : IFigureBuild , IMouseEvent
    {
        /// <summary>
        /// Переменная, хранящая класс для построения структуры эллипса.
        /// </summary>
        private СonstructionFigure _сonstructionFigure = new СonstructionFigure();

        /// <summary>
        /// Переменная, хранящая опорные точки.
        /// </summary>
        private SupportObjectFugure _drawSupportObject;

        /// <summary>
        /// Переменная, хранящая класс для построения и создания эллипса.
        /// </summary>
        //private AddLine _addFigureLine;


        /// <summary>
        /// Переменная, хранящая класс с действиями над фигурами.
        /// </summary>
        private EditObject _edipParametr = new EditObject();


        /// <summary>
        /// Метод, выполняющий действие при перемещении мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        public List<PointF> MouseMove(List<PointF> _points, MouseEventArgs e)
        {
            if ((_points != null) && (_points.Count != 0))
            {
                _points[1] = new PointF(e.Location.X, e.Location.Y);
            }

            return _points;
        }

        /// <summary>
        /// Метод, выполняющий действие при нажатии отпукании мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public List<PointF> MouseUp(List<PointF> _points, MouseEventArgs e, int Currentfigure,  List<IFigureBuild> FiguresBuild)
        {
            if ((_points != null) && (_points.Count != 0))
            {
                _points[1] = new PointF(e.Location.X, e.Location.Y);

            }
            return _points;
        }

        /// <summary>
        /// Метод, выполняющий действие при нажатии мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        /// <para name = "Currentfigure">Объект хранящий данные о выбранной фигуре</para>
        /// <para name = "DrawClass">Объект хранящий данные о классе используемом для отрисовки фигур</para>
        /// <para name = "FiguresBuild">Объект хранящий о классах построения</para>
        public void MouseDown(List<PointF> _points, MouseEventArgs e, int Currentfigure, List<IFigureBuild> FiguresBuild)
        {
            _points.Add(new PointF(e.Location.X, e.Location.Y));
            _points.Add(new PointF(e.Location.X, e.Location.Y));
        }

        /// <summary>
        /// Метод, выполняющий отрисовку линии при построении.
        /// </summary>
        /// <para name = "e">Объект хранящий данные для отображения линии</para>
        /// <para name = "Points">Точки для построения линии</para>
        /// <para name = "PenFigure">Кисть которая будет использоваться в построение линии</para>
        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            e.Graphics.DrawLine(PenFigure, Points[0], Points[1]);
        }

        /// <summary>
        /// Метод, выполняющий сохранение линии.
        /// </summary>
        /// <para name = "DrawObject">Переменна для хранения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "FiguresBuild">Список комманд для хранения комманды построения эллипса</para>
        /// <para name = "Figures">Список объектов для хранения всех фигур</para>
        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points,  List<ObjectFugure> Figures)
        {
            //_addFigureLine = new AddLine();
            //_addFigureLine.AddFigure(DrawObject, Points, Figures);
          
            //_addFigureLine.Output().FigureStart = Points[0];
            //_addFigureLine.Output().FigureEnd = Points[1];
            //_addFigureLine.Output().IdFigure = Figures.Count;

            //Figures.Add(_addFigureLine.Output());

            //FiguresCommand.Add(_addFigureLine);
        }

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно построить опорные точки</para>
        public void AddSupportPoint(ObjectFugure SelectObject, Color ColorLine)
        {
            for (int i = 0; i < SelectObject.PointSelect.Length; i++)
            {
                _drawSupportObject = new SupportObjectFugure(new Pen(ColorLine, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_сonstructionFigure.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
                _drawSupportObject.IdFigure = SelectObject.IdFigure;
                _drawSupportObject.ControlPointF = i;

                SelectObject.AddListFigure(_drawSupportObject);
            }
        }

        /// <summary>
        /// Метод, отвечающий за перемещение и масштабирование фигур.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно выполнять действия</para>
        /// <para name = "SupportObj">Переменная хранащая опорные точки выбранного объекта</para>
        /// <para name = "DeltaX">Переменная хранащая разницу по координате X</para>
        /// <para name = "DeltaY">Переменная хранащая разницу по координате Y</para>
        /// /// <para name = "EdipParametr">Объекта класса необходимый для выполнения масштабирования</para>
        public void ScaleSelectFigure(ObjectFugure SelectObject, SupportObjectFugure SupportObj, int DeltaX, int DeltaY)
        {
            if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[1].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[1].Y != 0))
            {
                SelectObject.PointSelect = SelectObject.Path.PathPoints;
            }
            //EdipParametr.EditObjectLine(SelectObject, SupportObj, DeltaX, DeltaY);
            if (SelectObject.IdFigure == SupportObj.IdFigure)
            {
                switch (SupportObj.ControlPointF)
                {
                    case 0:

                        SelectObject.PointSelect[0].X += DeltaX;
                        SelectObject.PointSelect[0].Y += DeltaY;
                        SelectObject.Path.Reset();
                        //CurrObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(CurrObj.PointSelect[0], CurrObj.PointSelect[2]));
                        SelectObject.Path.AddLine(SelectObject.PointSelect[0], SelectObject.PointSelect[1]);

                        break;

                    case 1:

                        SelectObject.PointSelect[1].X += DeltaX;
                        SelectObject.PointSelect[1].Y += DeltaY;
                        SelectObject.Path.Reset();
                        SelectObject.Path.AddLine(SelectObject.PointSelect[0], SelectObject.PointSelect[1]);

                        break;
                }
            }

            _edipParametr.MoveObjectSupport(SelectObject, DeltaX, DeltaY);
        }


        /// <summary>
        /// Метод, выполняющий выделение фигуры
        /// </summary>
        /// <para name = "e">Переменная хранащая значение координат курсора мыши</para>
        /// <para name = "DrawObject">Переменная хранащая объект выделения</para>
        /// <para name = "SelectedFigures">Список выделенных объектов</para>
        public void ScaleFigure(MouseEventArgs e, ObjectFugure DrawObject, List<ObjectFugure> SelectedFigures)
        {
            float LineX, LineY;

            LineY = (-(DrawObject.Path.PathPoints[1].X * DrawObject.Path.PathPoints[0].Y - DrawObject.Path.PathPoints[0].X * DrawObject.Path.PathPoints[1].Y) - ((DrawObject.Path.PathPoints[1].Y - DrawObject.Path.PathPoints[0].Y) * e.Location.X)) / (DrawObject.Path.PathPoints[0].X - DrawObject.Path.PathPoints[1].X);

            LineX = (-(DrawObject.Path.PathPoints[1].X * DrawObject.Path.PathPoints[0].Y - DrawObject.Path.PathPoints[0].X * DrawObject.Path.PathPoints[1].Y) - ((DrawObject.Path.PathPoints[0].X - DrawObject.Path.PathPoints[1].X) * e.Location.Y)) / (DrawObject.Path.PathPoints[1].Y - DrawObject.Path.PathPoints[0].Y);

            if ((e.Location.Y >= LineY - DrawObject.Pen.Width - 2) && (e.Location.Y <= LineY + DrawObject.Pen.Width + 2) || (e.Location.X >= LineX - DrawObject.Pen.Width - 2) && (e.Location.X <= LineX + DrawObject.Pen.Width + 2))
            {
                DrawObject.PointSelect = DrawObject.Path.PathPoints;
                DrawObject.SelectFigure = true;
                SelectedFigures.Add(DrawObject);
            }

        }

    }
}
