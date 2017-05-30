using System;
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
    /// Класс, выполнящий различные действия над эллипсом.
    /// </summary>
    public class Ellipses : IFigureBuild , IMouseEvent
    {
        /// <summary>
        /// Переменная, хранящая класс для построения и создания эллипса.
        /// </summary>
        //private AddEllipse _addFigureEllipse;

        /// <summary>
        /// Переменная, хранящая класс для построеня структуры фигур.
        /// </summary>
        private СonstructionFigure _figureBuild = new СonstructionFigure();

        /// <summary>
        /// Переменная, хранящая класс с действиями над фигурами.
        /// </summary>
        private EditObject _edipParametr = new EditObject();

        /// <summary>
        /// Переменная, хранящая координаты начальной точки.
        /// </summary>
        private PointF _pointStart;

        /// <summary>
        /// Переменная, хранящая координаты конечной точки.
        /// </summary>
        private PointF _pointEnd;

        /// <summary>
        /// Переменная, хранящая опорные точки.
        /// </summary>
        private SupportObjectFugure _drawSupportObject;

        //public Ellipses(EditObject _edipParametr)
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
        public void MouseDown(List<PointF> _points, MouseEventArgs e, int Currentfigure,  List<IFigureBuild> FiguresBuild)
        {
            _points.Add(new PointF(e.Location.X, e.Location.Y));
            _points.Add(new PointF(e.Location.X, e.Location.Y));
        }


        /// <summary>
        /// Метод, выполняющий отрисовку эллипса при построении.
        /// </summary>
        /// <para name = "e">Объект хранящий данные для отображения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "PenFigure">Кисть которая будет использоваться в построение эллипса</para>
        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            e.Graphics.DrawEllipse(PenFigure, _figureBuild.ShowRectangle(Points[0], Points[1]));
        }

        /// <summary>
        /// Метод, выполняющий сохранение эллипса.
        /// </summary>
        /// <para name = "DrawObject">Переменна для хранения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "FiguresBuild">Список комманд для хранения комманды построения эллипса</para>
        /// <para name = "Figures">Список объектов для хранения всех фигур</para>
        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures)
        {
            //_addFigureEllipse = new AddEllipse();
            //_addFigureEllipse.AddFigure(DrawObject, Points, Figures);
         

            //Figures.Add(_addFigureEllipse.Output());
            //FiguresBuild.Add(_addFigureEllipse);
        }

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно построить опорные точки</para>
        public void AddSupportPoint(ObjectFugure SelectObject,Color ColorLine)
        {
            for (int i = 0; i < SelectObject.PointSelect.Length; i += 3)
            {
                _drawSupportObject = new SupportObjectFugure(new Pen(ColorLine, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_figureBuild.SelectFigure(SelectObject.PointSelect[i], SelectObject.Pen.Width));
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
          
            SelectObject.PointSelect = SelectObject.Path.PathPoints;

            if (SelectObject.IdFigure == SupportObj.IdFigure)
            {
                switch (SupportObj.ControlPointF)
                {
                    case 12:

                        if (SelectObject.PointSelect[0].X + DeltaX > SelectObject.PointSelect[6].X)
                        {
                            SelectObject.PointSelect[0].X += DeltaX;

                        }

                        SelectObject.PointSelect[0].Y += DeltaY;

                        _pointStart.X = SelectObject.PointSelect[6].X;
                        _pointStart.Y = SelectObject.PointSelect[9].Y;

                        _pointEnd.X = SelectObject.PointSelect[0].X;
                        _pointEnd.Y = SelectObject.PointSelect[3].Y;

                        SelectObject.Path.Reset();

                        SelectObject.Path.AddEllipse(_figureBuild.ShowRectangle(_pointEnd, _pointStart));

                        break;


                    case 3:

                        if (SelectObject.PointSelect[3].Y + DeltaY > SelectObject.PointSelect[9].Y)
                        {
                            SelectObject.PointSelect[3].Y += DeltaY;

                        }

                        SelectObject.PointSelect[3].X += DeltaX;

                        _pointStart.X = SelectObject.PointSelect[6].X;
                        _pointStart.Y = SelectObject.PointSelect[9].Y;

                        _pointEnd.X = SelectObject.PointSelect[0].X;
                        _pointEnd.Y = SelectObject.PointSelect[3].Y;

                        SelectObject.Path.Reset();

                        SelectObject.Path.AddEllipse(_figureBuild.ShowRectangle(_pointStart, _pointEnd));

                        break;

                    case 6:

                        if (SelectObject.PointSelect[6].X + DeltaX < SelectObject.PointSelect[0].X)
                        {
                            SelectObject.PointSelect[6].X += DeltaX;

                        }

                        SelectObject.PointSelect[6].Y += DeltaY;

                        _pointStart.X = SelectObject.PointSelect[6].X;
                        _pointStart.Y = SelectObject.PointSelect[9].Y;

                        _pointEnd.X = SelectObject.PointSelect[0].X;
                        _pointEnd.Y = SelectObject.PointSelect[3].Y;

                        SelectObject.Path.Reset();

                        SelectObject.Path.AddEllipse(_figureBuild.ShowRectangle(_pointStart, _pointEnd));

                        break;

                    case 9:
                        if (SelectObject.PointSelect[9].Y + DeltaY < SelectObject.PointSelect[3].Y)
                        {
                            SelectObject.PointSelect[9].Y += DeltaY;

                        }

                        SelectObject.PointSelect[9].X += DeltaX;

                        _pointStart.X = SelectObject.PointSelect[6].X;
                        _pointStart.Y = SelectObject.PointSelect[9].Y;

                        _pointEnd.X = SelectObject.PointSelect[0].X;
                        _pointEnd.Y = SelectObject.PointSelect[3].Y;

                        SelectObject.Path.Reset();

                        SelectObject.Path.AddEllipse(_figureBuild.ShowRectangle(_pointStart, _pointEnd));

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
            DrawObject.PointSelect = DrawObject.Path.PathPoints;
            DrawObject.SelectFigure = true;
            SelectedFigures.Add(DrawObject);
        }


    }
}
