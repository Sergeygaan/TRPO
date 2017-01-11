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
    class SelectDraw
    {
        List<Object> _selectedFigures = new List<Object>();   //Список выбранных фигур

       // private Object currObj = null;//Объект, который в данный момент перемещается
        private SupportObject _supportObj;
        private Point _oldPoint;
       
        private RectangleF _rectangleF;
        
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private EditObject _edipParametr = new EditObject();



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

        public void MouseDown(MouseEventArgs e, Rectangle Rect, List<Object> _figures, MainForm.Actions _currentActions, List<IFigureBuild> FiguresBuild)
        {
            //Запоминаем положение курсора
            _oldPoint = e.Location;

            float figurestartX, figurestartY, figureendX, figureendY;

            if (_selectedFigures.Count == 0)
            {

                //Ищем объект, в который попала точка.Если таких несколько, то найден будет первый по списку
                foreach (Object DrawObject in _figures)
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

                    switch (_currentActions)
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

        public void MouseMove(MouseEventArgs e, MainForm.Actions _currentActions, List<IFigureBuild> FiguresBuild)
        {
            //Считаем смещение курсора
            int deltaX, deltaY;

            deltaX = e.Location.X - _oldPoint.X;
            deltaY = e.Location.Y - _oldPoint.Y;

            foreach (Object SelectObject in _selectedFigures)
            {
                switch (_currentActions)
                {

                    case MainForm.Actions.Scale:

                        //Масштабирование опорных точек
                        if ((SelectObject != null) && (_supportObj != null))
                        {
                            FiguresBuild[(int)SelectObject.CurrentFigure].ScaleSelectFigure(SelectObject, _supportObj, deltaX, deltaY, _edipParametr);
 
                        }

                        break;
                     
                    // Перемещение фигуры
                    case MainForm.Actions.Move:

                        if (SelectObject != null)
                        {
                            SelectObject.PointSelect = SelectObject.Path.PathPoints;

                            _edipParametr.MoveObject(SelectObject, deltaX, deltaY);

                        }

                        break;
                }

                _oldPoint = e.Location;
            }

        }
     

        //Вернуть выделенный объект 
        public List<Object> SeleckResult()
        {
            return _selectedFigures;
        }



    }
}
