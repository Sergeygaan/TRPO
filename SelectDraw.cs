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


        private PaintedObject currObj = null;//Объект, который в данный момент перемещается
        private SupportObject _supportObj;
        private Point oldPoint;
        private RectangleF _rectangleF;
        private PointF[] PointSelect;
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private EditObject _edipParametr = new EditObject();

        public void MouseUp()
        {
            if (currObj != null)
            {
                //currObj.Pen.Width -= 1;//Возвращаем ширину пера
                currObj.ClearListFigure();
                currObj.PointSelect = null;
                currObj.SelectFigure = false;
                currObj = null;//Убираем ссылку на объект
                _supportObj = null;
                
            }
        }

        public void MouseUpSupport()
        {
            if (_supportObj != null)
            {
                _supportObj.Pen.Width -= 5;
                _supportObj = null;

            }
        }


        public void MouseDown(MouseEventArgs e, List<PaintedObject> _figures)
        {
            //Запоминаем положение курсора
            oldPoint = e.Location;
            int figurestartX, figurestartY, figureendX, figureendY;

            if (currObj == null)
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


                    if (_rectangleF.Contains(e.Location))
                    {
                        currObj = DrawObject;//Запоминаем найденный объект
                        currObj.PointSelect = currObj.Path.PathPoints;
                        currObj.SelectFigure = true;
                        //currObj.Pen.Width += 1;//Делаем перо жирнее

                    }
                }
            }
            else
            {
                
                foreach (SupportObject SupportObjecFigure in currObj.SelectListFigure())
                {

                    _rectangleF = SupportObjecFigure.Path.GetBounds();


                    //_rectangleF.Inflate(100, 50);


                    if (_rectangleF.Contains(e.Location))
                    {
                        _supportObj = SupportObjecFigure;//Запоминаем найденный объект
       
                        _supportObj.Pen.Width += 5;//Делаем перо жирнее

                    }

                }
            }
        }


        public void MouseMove(MouseEventArgs e)
        {
                    //Считаем смещение курсора
                    int deltaX, deltaY;

                    //int figurestartX, figurestartY, figureendX, figureendY;
                    deltaX = e.Location.X - oldPoint.X;
                    deltaY = e.Location.Y - oldPoint.Y;

                    //figurestartX = e.Location.X - oldPoint.X;
                    //figurestartY = e.Location.X - oldPoint.Y;


                    //figureendX = e.Location.X - oldPoint.X;
                    //figureendX = e.Location.X - oldPoint.X;


                    //Смещаем нарисованный объект
                    if ((currObj != null) && (_supportObj != null))
                    {

                        switch (currObj.CurrentFigure)
                        {
                            case MainForm.FigureType.Rectangle:

                                if ((currObj.PointSelect[0].X - currObj.PointSelect[2].X != 0) && (currObj.PointSelect[0].Y - currObj.PointSelect[2].Y != 0))
                                {
                                    currObj.PointSelect = currObj.Path.PathPoints;
                                }
                                _edipParametr.EditObjectRectangle(currObj, _supportObj, deltaX, deltaY);

                                break;

                            case MainForm.FigureType.Line:

                                if ((currObj.PointSelect[0].X - currObj.PointSelect[1].X != 0) && (currObj.PointSelect[0].Y - currObj.PointSelect[1].Y != 0))
                                {
                                    currObj.PointSelect = currObj.Path.PathPoints;
                                }
                                _edipParametr.EditObjectLine(currObj, _supportObj, deltaX, deltaY);

                                break;

                            case MainForm.FigureType.Ellipse:

                                //if ((currObj.PointSelect[0].X - currObj.PointSelect[3].X != 0) && (currObj.PointSelect[0].Y - currObj.PointSelect[1].Y != 0))
                                //{
                                //    currObj.PointSelect = currObj.Path.PathPoints;
                                //}
                                _edipParametr.EditObjectEllepse(currObj, _supportObj, deltaX, deltaY);

                                 currObj.PointSelect = currObj.Path.PathPoints;
                        break;
                        }

              
                        //currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));

                        oldPoint = e.Location;
                       }
        }

        //Вернуть выделенный объект 
        public PaintedObject SeleckResult()
        {
            return currObj;
        }



    }
}
