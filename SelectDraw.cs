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
                currObj = null;//Убираем ссылку на объект
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

                        //currObj.Pen.Width += 1;//Делаем перо жирнее

                    }
                }
            }
            else
            {
                
                foreach (SupportObject SupportObject in currObj.SelectListFigure())
                {

                    //currObj.SelectListFigure()[1].Pen.Width += 5;
                    //MessageBox.Show(currObj.SelectListFigure().Count().ToString());


                    //_rectangleF = SupportObject.Path.GetBounds();


                    //    _rectangleF.Inflate(100, 50);


                    //if (_rectangleF.Contains(e.Location))
                    //{
                    //    _supportObj = SupportObject;//Запоминаем найденный объект

                    //    MessageBox.Show("asdasd");
                    //    _supportObj.Pen.Width += 5;//Делаем перо жирнее

                    //}

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
                    if (currObj != null)
                    {

                        //PointSelect[0].X += deltaX;
                        //PointSelect[0].Y += deltaY;
                        //currObj.Path.Reset();
                        //currObj.Path.AddRectangle(_ellipse.ShowRectangle1(PointSelect[0], PointSelect[2]));

                        //PointSelect[0].X += deltaX;
                        //PointSelect[2].Y += deltaY;
                        //currObj.Path.Reset();
                        //currObj.Path.AddRectangle(_ellipse.ShowRectangle1(PointSelect[0], PointSelect[2]));

                        //PointSelect[2].X += deltaX;
                        //PointSelect[0].Y += deltaY;
                        //currObj.Path.Reset();
                        //currObj.Path.AddRectangle(_ellipse.ShowRectangle1(PointSelect[0], PointSelect[2]));

                        //Перемещение

                        _edipParametr.MoveObject(currObj, deltaX, deltaY);
                        //currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));

                        //if ((PointSelect[0].X - PointSelect[2].X != 0) && (PointSelect[0].Y - PointSelect[2].Y != 0))
                        //{
                        //    currObj.PointSelect = currObj.Path.PathPoints;
                        //}
                        
                        oldPoint = e.Location;
                        currObj.PointSelect = currObj.Path.PathPoints;
            }
        }

        //Вернуть конечную координату

        //Вернуть выделенный объект 
        public PaintedObject SeleckResult()
        {
            return currObj;
        }



    }
}
