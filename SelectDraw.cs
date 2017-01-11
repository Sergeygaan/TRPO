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

        private PaintedObject currObj;//Объект, который в данный момент перемещается
        private Point oldPoint;
        private Size inflateSize = new Size(50, 50);
        private RectangleF _rectangleF;
        private PointF[] PointSelect;

        public void MouseUp()
        {
            if (currObj != null)
            {
                currObj.Pen.Width -= 1;//Возвращаем ширину пера
                currObj = null;//Убираем ссылку на объект
            }
        }


        public void MouseDown(MouseEventArgs e, List<PaintedObject> _figures)
        {
            //Запоминаем положение курсора
            oldPoint = e.Location;
            int figurestartX, figurestartY, figureendX, figureendY;
            
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
                    currObj.Pen.Width += 1;//Делаем перо жирнее

                    

                    return;
                }
                
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
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
                        //Перемещение
                        currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));

                        PointSelect = currObj.Path.PathPoints;

                        //currObj.Path.AddRectangle(_ellipse.Show(figurestart, oldPoint));


                        //currObj.Path.Reset();
                        //currObj.Path.AddLine(currObj.FigureStart, oldPoint);
                        //DrawForm.Refresh();
                        //DrawForm.Invalidate();
                        //currObj.Path.Transform(new Matrix(-1, 0, 1, 0, 0, 0));

                        //Запоминаем новое положение курсора
                        oldPoint = e.Location;
                    }
                    break;
                default:
                    break;
            }
        }


        //Вернуть начальную координату
        public PointF[] SeleckFigure()
        {            
            return PointSelect;
        }

        //Вернуть конечную координату
        public Point SelectFigureEnd()
        {
            return currObj.FigureEnd;
        }

        //Вернуть выделенный объект координату
        public PaintedObject SeleckResult()
        {
            return currObj;
        }



    }
}
