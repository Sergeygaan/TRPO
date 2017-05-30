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
    /// Класс, выполнящий различные действия над многоугольником.
    /// </summary>
    public class Polygon : IFigureBuild , IMouseEvent
    {
        /// <summary>
        /// Переменная, хранящая класс для построения и создания эллипса.
        /// </summary>
       // private AddPolygon _addFigureAddPolygon;

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

        //public Polygon(EditObject _edipParametr)
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
        public List<PointF> MouseUp(List<PointF> _points, MouseEventArgs e, int Currentfigure, List<IFigureBuild> FiguresBuild)
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
        /// Метод, выполняющий отрисовку многоугольника при построении.
        /// </summary>
        /// <para name = "e">Объект хранящий данные для отображения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "PenFigure">Кисть которая будет использоваться в построение эллипса</para>
        public void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure)
        {
            if ((Points != null) && (Points.Count > 1))
            {
                PointF[] PointPolygon = Points.ToArray();

                e.Graphics.DrawLines(PenFigure, PointPolygon);
            }
        }

        /// <summary>
        /// Метод, выполняющий сохранение многоугольника.
        /// </summary>
        /// <para name = "DrawObject">Переменна для хранения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "FiguresBuild">Список комманд для хранения комманды построения эллипса</para>
        /// <para name = "Figures">Список объектов для хранения всех фигур</para>
        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures)
        {
            //_addFigureAddPolygon = new AddPolygon();
            //_addFigureAddPolygon.AddFigure(DrawObject, Points, Figures);
            
            //_addFigureAddPolygon.Output().FigureStart = Points[0];
            //_addFigureAddPolygon.Output().FigureEnd = Points[1];
            //_addFigureAddPolygon.Output().IdFigure = Figures.Count;

            //Figures.Add(_addFigureAddPolygon.Output());
            //FiguresBuild.Add(_addFigureAddPolygon);
        }


        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно построить опорные точки</para>
        public void AddSupportPoint(ObjectFugure selectObject, Color colorLine)
        {
            for (int i = 0; i < selectObject.PointSelect.Length; i++)
            {
                _drawSupportObject = new SupportObjectFugure(new Pen(colorLine, 1), new GraphicsPath());
                _drawSupportObject.Path.AddEllipse(_сonstructionFigure.SelectFigure(selectObject.PointSelect[i], selectObject.Pen.Width));
                _drawSupportObject.IdFigure = selectObject.IdFigure;
                _drawSupportObject.ControlPointF = i;

                selectObject.AddListFigure(_drawSupportObject);
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
        public void ScaleSelectFigure(ObjectFugure selectObject, SupportObjectFugure supportObj, int deltaX, int deltaY)
        {
            if ((selectObject.PointSelect[0].X - selectObject.PointSelect[1].X != 0) && (selectObject.PointSelect[0].Y - selectObject.PointSelect[1].Y != 0))
            {
                selectObject.PointSelect = selectObject.Path.PathPoints;
            }

            if (selectObject.IdFigure == supportObj.IdFigure)
            {

                selectObject.PointSelect[supportObj.ControlPointF].X += deltaX;
                selectObject.PointSelect[supportObj.ControlPointF].Y += deltaY;

                PointF[] pointF = selectObject.PointSelect.ToArray();
                selectObject.Path.Reset();
                selectObject.Path.AddLines(pointF);

                if (selectObject.CurrentFigure == 4)
                {
                    selectObject.Path.CloseFigure();
                }

            }

            _edipParametr.MoveObjectSupport(selectObject, deltaX, deltaY);

        }

        /// <summary>
        /// Метод, выполняющий выделение фигуры
        /// </summary>
        /// <para name = "e">Переменная хранащая значение координат курсора мыши</para>
        /// <para name = "DrawObject">Переменная хранащая объект выделения</para>
        /// <para name = "SelectedFigures">Список выделенных объектов</para>
        public void ScaleFigure(MouseEventArgs e, ObjectFugure drawObject, List<ObjectFugure> selectedFigures)
        {
            drawObject.PointSelect = drawObject.Path.PathPoints;
            drawObject.SelectFigure = true;
            //DrawObject.Pen.Width += 1;
            selectedFigures.Add(drawObject);
        }

    }
}
