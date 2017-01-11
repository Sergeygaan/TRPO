using PaintedObjectsMoving.CORE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    /// <summary>
    /// Класс, выполняющий выделение фигур.
    /// </summary>
    class SelectDraw
    {
        /// <summary>
        /// Переменная, хранящая список с выделенными фигурами.
        /// </summary>
        private List<Object> _selectedFigures = new List<Object>();

        /// <summary>
        /// Переменная, хранящая выделенную фигуру.
        /// </summary>
        private SupportObject _supportObj;

        /// <summary>
        /// Переменная, хранящая тукущие координаты мыщи.
        /// </summary>
        private Point _oldPoint;

        /// <summary>
        /// Переменная, хранящая зону выделения.
        /// </summary>
        private RectangleF _rectangleF;

        /// <summary>
        /// Переменная, хранящая класс с действиями над фигурами.
        /// </summary>
        private EditObject _edipParametr = new EditObject();

        /// <summary>
        /// Метод, выполняющий отмену выделения.
        /// </summary>
        public void MouseUp()
        {
            foreach (Object SelectObject in _selectedFigures)
            {
                if (SelectObject != null)
                {
                    //SelectObject.Pen.Width -= 1;//Возвращаем ширину пера
                    SelectObject.ClearListFigure();
                    SelectObject.PointSelect = null;
                    SelectObject.SelectFigure = false;
                    //SelectObject. = null;//Убираем ссылку на объект
                    _supportObj = null;

                }
            }
            _selectedFigures.Clear();
        }

        public void MouseUpSupport()
        {
            if (_supportObj != null)
            {
                //_supportObj.Pen.Width -= 5;
                _supportObj = null;

            }
        }

        public void SavePoint(MouseEventArgs e)
        {
            _oldPoint = e.Location;

            if (_selectedFigures.Count != 0)
            {
                foreach (Object SelectObject in _selectedFigures)
                {
                    foreach (SupportObject SupportObjecFigure in SelectObject.SelectListFigure())
                    {

                        _rectangleF = SupportObjecFigure.Path.GetBounds();

                        if (_rectangleF.Contains(e.Location))
                        {
                            _supportObj = SupportObjecFigure;
                           // MessageBox.Show(_supportObj.ControlPointF.ToString());

                        }

                    }
                }
            }
        }

        /// <summary>
        /// Метод, выполняющий выделение фигур.
        /// </summary>
        /// <para name = "e">Переменная, хранящая координаты мыши.</para>
        /// <para name = "Rect">Переменная, хранящая зону выделения.</para>
        /// <para name = "Figures">Переменная, хранящая список всех фигур.</para>
        /// <para name = "CurrentActions">Переменная, хранящая действие над выбранной фигурой.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая список действий.</para>
        public void MouseDown(MouseEventArgs e, Rectangle Rect, List<Object> Figures, MainForm.Actions CurrentActions, List<IFigureBuild> FiguresBuild)
        {
            //Запоминаем положение курсора
            _oldPoint = e.Location;

            float figurestartX, figurestartY, figureendX, figureendY;

            if (_selectedFigures.Count == 0)
            {

                foreach (Object DrawObject in Figures)
                {

                    figurestartX = DrawObject.FigureStart.X;
                    figurestartY = DrawObject.FigureStart.Y;

                    figureendX = DrawObject.FigureEnd.X;
                    figureendY = DrawObject.FigureEnd.Y;

                    // Получение области выделения
                    _rectangleF = DrawObject.Path.GetBounds();

                    if (figurestartX == figureendX)
                    {
                        _rectangleF.Inflate(10, 5);
                    }

                    if (figurestartY == figureendY)
                    {
                        _rectangleF.Inflate(5, 10);
                    }

                    switch (CurrentActions)
                    {

                        case MainForm.Actions.SelectRegion:

                            if (_rectangleF.IntersectsWith(Rect))
                            {
                                DrawObject.PointSelect = DrawObject.Path.PathPoints;
                                DrawObject.SelectFigure = true;
                                //DrawObject.Pen.Width += 1;
                                _selectedFigures.Add(DrawObject);

                            }

                            break;

                        case MainForm.Actions.SelectPoint:

                            if (_rectangleF.Contains(e.Location))
                            {
                                FiguresBuild[(int)DrawObject.CurrentFigure].ScaleFigure(e, DrawObject, _selectedFigures);
                            }

                            break;
                    }

                }
            }

        }

        /// <summary>
        /// Метод, выполняющий действия над выделенными фигурами.
        /// </summary>
        /// <para name = "e">Переменная, хранящая координаты мыши.</para>
        /// <para name = "CurrentActions">Переменная, хранящая действие над выбранной фигурой.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая список действий.</para>
        public void MouseMove(MouseEventArgs e, MainForm.Actions CurrentActions, List<IFigureBuild> FiguresBuild)
        {
            //Считаем смещение курсора
            int deltaX, deltaY;

            deltaX = e.Location.X - _oldPoint.X;
            deltaY = e.Location.Y - _oldPoint.Y;

            foreach (Object SelectObject in _selectedFigures)
            {
              
                //Масштабирование опорных точек
                if ((SelectObject != null) && (_supportObj != null))
                {
                    FiguresBuild[(int)SelectObject.CurrentFigure].ScaleSelectFigure(SelectObject, _supportObj, deltaX, deltaY, _edipParametr);
 
                }
                else
                {
                    if (SelectObject != null)
                    {
                        SelectObject.PointSelect = SelectObject.Path.PathPoints;

                        _edipParametr.MoveObject(SelectObject, deltaX, deltaY);
                    }
                }

                _oldPoint = e.Location;
            }

        }

        /// <summary>
        /// Метод, возвращающий список выделенных фигур.
        /// </summary>
        public List<Object> SeleckResult()
        {
            return _selectedFigures;
        }
    }
}
