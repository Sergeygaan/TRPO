﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyPaint.Build;
using MyPaint.ObjectType;

namespace MyPaint.Build
{
    /// <summary>
    /// Класс, выполнящий различные действия над полилинией.
    /// </summary>
    public class PoliLine : IFigureBuild , IMouseEvent
    { 

        /// <summary>
        /// Переменная, хранящая класс для построения и создания эллипса.
        /// </summary>
       // private AddPoliLine _addFigurePoliLine;

        /// <summary>
        /// Переменная, хранящая класс для построения структуры эллипса.
        /// </summary>
        private СonstructionFigure _сonstructionFigure = new СonstructionFigure();

        /// <summary>
        /// Переменная, хранящая опорные точки.
        /// </summary>
        private SupportObjectFugure _drawSupportObject;

        /// <summary>
        /// Переменная, хранящая класс с действиями над фигурами.
        /// </summary>
        private EditObject _edipParametr = new EditObject();

        //public PoliLine(EditObject _edipParametr)
        //{
        //    this._edipParametr = _edipParametr;
        //}

        /// <summary>
        /// Метод, выполняющий действие при перемещении мыши.
        /// </summary>
        /// <para name = "e">Объект хранящий данные о мыши</para>
        /// <para name = "_points">Объект хранящий данные о точках построения фигурые</para>
        public List<PointF> MouseMove(List<PointF> _points, MouseEventArgs e)
        {
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
        public List<PointF> MouseUp(List<PointF> _points, MouseEventArgs e, int Currentfigure,List<IFigureBuild> FiguresBuild)
        {
                return null;
         
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
            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                _points.Add(new PointF(e.Location.X, e.Location.Y));
              
            }
        }

        /// <summary>
        /// Метод, выполняющий отрисовку полилинии при построении.
        /// </summary>
        /// <para name = "e">Объект хранящий данные для отображения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "PenFigure">Кисть которая будет использоваться в построение эллипса</para>
        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            if ((Points != null) && (Points.Count > 1))
            {
                PointF[] PointPoliLine = Points.ToArray();

                e.Graphics.DrawLines(PenFigure, PointPoliLine);
            }
        }

        /// <summary>
        /// Метод, выполняющий сохранение полилинии.
        /// </summary>
        /// <para name = "DrawObject">Переменна для хранения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "FiguresBuild">Список комманд для хранения комманды построения эллипса</para>
        /// <para name = "Figures">Список объектов для хранения всех фигур</para>
        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points,  List<ObjectFugure> Figures)
        {

            //_addFigurePoliLine = new AddPoliLine();
            //_addFigurePoliLine.AddFigure(DrawObject, Points, Figures);
            
            //_addFigurePoliLine.Output().FigureStart = Points[0];
            //_addFigurePoliLine.Output().FigureEnd = Points[1];
            //_addFigurePoliLine.Output().IdFigure = Figures.Count;

            //Figures.Add(_addFigurePoliLine.Output());
            //FiguresBuild.Add(_addFigurePoliLine);

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
         
            if (SelectObject.IdFigure == SupportObj.IdFigure)
            {

                SelectObject.PointSelect[SupportObj.ControlPointF].X += DeltaX;
                SelectObject.PointSelect[SupportObj.ControlPointF].Y += DeltaY;

                PointF[] PointF = SelectObject.PointSelect.ToArray();
                SelectObject.Path.Reset();
                SelectObject.Path.AddLines(PointF);

                if (SelectObject.CurrentFigure == 4)
                {
                    SelectObject.Path.CloseFigure();
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
            
            for (int i = 1; i < DrawObject.Path.PathPoints.Length; i++)
            {
                float PoliLineX, PoliLineY;

                PoliLineY = (-(DrawObject.Path.PathPoints[i - 1].X * DrawObject.Path.PathPoints[i].Y - DrawObject.Path.PathPoints[i].X * DrawObject.Path.PathPoints[i - 1].Y) - ((DrawObject.Path.PathPoints[i - 1].Y - DrawObject.Path.PathPoints[i].Y) * e.Location.X)) / (DrawObject.Path.PathPoints[i].X - DrawObject.Path.PathPoints[i - 1].X);

                PoliLineX = (-(DrawObject.Path.PathPoints[i - 1].X * DrawObject.Path.PathPoints[i].Y - DrawObject.Path.PathPoints[i].X * DrawObject.Path.PathPoints[i - 1].Y) - ((DrawObject.Path.PathPoints[i].X - DrawObject.Path.PathPoints[i - 1].X) * e.Location.Y)) / (DrawObject.Path.PathPoints[i - 1].Y - DrawObject.Path.PathPoints[i].Y);

                if ((e.Location.Y >= PoliLineY - DrawObject.Pen.Width - 2) && (e.Location.Y <= PoliLineY + DrawObject.Pen.Width + 2) || (e.Location.X >= PoliLineX - DrawObject.Pen.Width - 2) && (e.Location.X <= PoliLineX + DrawObject.Pen.Width + 2))
                {
                    DrawObject.PointSelect = DrawObject.Path.PathPoints;
                    DrawObject.SelectFigure = true;
                    SelectedFigures.Add(DrawObject);
                }
            }
        }


    }
}
