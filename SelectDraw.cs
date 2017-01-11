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
        List<PaintedObject> _selectedFigures = new List<PaintedObject>();   //Список выбранных фигур

       // private PaintedObject currObj = null;//Объект, который в данный момент перемещается
        private SupportObject _supportObj;
        private Point _oldPoint;
       
        private RectangleF _rectangleF;
        
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private EditObject _edipParametr = new EditObject();

        public void MouseUp()
        {
            foreach (PaintedObject SelectObject in _selectedFigures)
            {
                if (SelectObject != null)
                {
                    SelectObject.Pen.Width -= 1;//Возвращаем ширину пера
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
                foreach (PaintedObject SelectObject in _selectedFigures)
                {
                    foreach (SupportObject SupportObjecFigure in SelectObject.SelectListFigure())
                    {

                        _rectangleF = SupportObjecFigure.Path.GetBounds();

                        if (_rectangleF.Contains(e.Location))
                        {
                            _supportObj = SupportObjecFigure;

                        }

                    }
                }
            }
        }


        public void MouseDown(MouseEventArgs e, Rectangle Rect, List<PaintedObject> _figures)
        {
            //Запоминаем положение курсора
            _oldPoint = e.Location;

            int figurestartX, figurestartY, figureendX, figureendY;

            if (_selectedFigures.Count == 0)
            {

                //Ищем объект, в который попала точка.Если таких несколько, то найден будет первый по списку
                foreach (PaintedObject DrawObject in _figures)
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

                    if (_rectangleF.IntersectsWith(Rect))
                    {

                        DrawObject.PointSelect = DrawObject.Path.PathPoints;
                        DrawObject.SelectFigure = true;
                        DrawObject.Pen.Width += 1;
                        _selectedFigures.Add(DrawObject);


                    }

                }
            }

        }


        public void MouseMove(MouseEventArgs e, MainForm.Actions _currentActions)
        {
            //Считаем смещение курсора
            int deltaX, deltaY;

            deltaX = e.Location.X - _oldPoint.X;
            deltaY = e.Location.Y - _oldPoint.Y;
            foreach (PaintedObject SelectObject in _selectedFigures)
            {
                switch (_currentActions)
                {

                    case MainForm.Actions.Scale:

                        //Смещаем нарисованный объект
                        if ((SelectObject != null) && (_supportObj != null))
                        {

                            switch (SelectObject.CurrentFigure)
                            {
                                case MainForm.FigureType.Rectangle:

                                    if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[2].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[2].Y != 0))
                                    {
                                        SelectObject.PointSelect = SelectObject.Path.PathPoints;
                                    }
                                    _edipParametr.EditObjectRectangle(SelectObject, _supportObj, deltaX, deltaY);

                                    break;

                                case MainForm.FigureType.Line:

                                    if ((SelectObject.PointSelect[0].X - SelectObject.PointSelect[1].X != 0) && (SelectObject.PointSelect[0].Y - SelectObject.PointSelect[1].Y != 0))
                                    {
                                        SelectObject.PointSelect = SelectObject.Path.PathPoints;
                                    }
                                    _edipParametr.EditObjectLine(SelectObject, _supportObj, deltaX, deltaY);

                                    break;

                                case MainForm.FigureType.Ellipse:


                                    _edipParametr.EditObjectEllepse(SelectObject, _supportObj, deltaX, deltaY);

                                    SelectObject.PointSelect = SelectObject.Path.PathPoints;

                                    break;
                            }

                            _oldPoint = e.Location;
                        }

                        break;

                    case MainForm.Actions.Move:

                        if (SelectObject != null)
                        {
                            SelectObject.PointSelect = SelectObject.Path.PathPoints;

                            _edipParametr.MoveObject(SelectObject, deltaX, deltaY);

                            _oldPoint = e.Location;
                        }

                        break;
                }
            }

        }
     

        //Вернуть выделенный объект 
        public List<PaintedObject> SeleckResult()
        {
            return _selectedFigures;
        }



    }
}
