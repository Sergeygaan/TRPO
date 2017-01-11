using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace PaintedObjectsMoving
{
   
    /*По идее класс Ellipse идентичен классу Rect,
     * только флагом в конструкторе будет не "Квадрат",
     * а "Круг"; и в методе Show появится строка
     * построения эллипса по заданному прямоугольнику
     */
    class СonstructionFigure
    {
        //ПЕРЕМЕННЫЕ
        private int Top;                //верхняя координата эллипса
        private int Left;               //левая координата эллипса
        private int Down;               //нижняя координата эллипса
        private int Right;              //правая координата эллипса
        private int height = 0;         //высота эллипса
        private int width = 0;          //шириина эллипса

        //СВОЙСТВА
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }

        //МЕТОДЫ
        public Rectangle ShowRectangle(Point start, Point end)
        {
            /*          true        false
             * Hide     скрыть      отобразить
             * Dash     пунктиром   выбранным стилем
             */

            /*создаем прямоугольник с заданными координатами,
             * учитывая сдвиг при прокрутке
             */
            Left = (start.X - end.X > 0) ? end.X : start.X;
            Down = (start.Y - end.Y > 0) ? start.Y : end.Y;
            Top = (start.Y - end.Y > 0) ? end.Y : start.Y;
            Right = (start.X - end.X > 0) ? start.X : end.X;


            Rectangle rect = Rectangle.FromLTRB(Left , Top , Right , Down );

            //if (rect == null)
            //{
            //    if (width != height) throw new Exception("Не удалось построить эллипс!");
            //    else throw new Exception("Не удалось построить круг!");
            //}
            /*if (fill) obj.FillEllipse(Brush, rect); */                                  //выполняем заливку если  fill = true
            //obj.DrawEllipse(new Pen(Color.Black), rect);                                               //рисуем прямоугольник

            return rect;
        }

        public Rectangle ShowEllipse(Point start, Point end)
        {
            /*          true        false
             * Hide     скрыть      отобразить
             * Dash     пунктиром   выбранным стилем
             */

            /*создаем прямоугольник с заданными координатами,
             * учитывая сдвиг при прокрутке
             */
            Left = (start.X - end.X > 0) ? end.X : start.X;
            Down = (start.Y - end.Y > 0) ? start.Y : end.Y;
            Top = (start.Y - end.Y > 0) ? end.Y : start.Y;
            Right = (start.X - end.X > 0) ? start.X : end.X;

            //width = Math.Abs(end.X - start.X);              //вычисляем высоту эллипса
            //height = Math.Abs(end.Y - start.Y);             //вычисляем ширину эллипса

            Rectangle rect = Rectangle.FromLTRB(Left, Top, Right, Down);

            //if (rect == null)
            //{
            //    if (width != height) throw new Exception("Не удалось построить эллипс!");
            //    else throw new Exception("Не удалось построить круг!");
            //}
            /*if (fill) obj.FillEllipse(Brush, rect); */                                  //выполняем заливку если  fill = true
            //obj.DrawEllipse(new Pen(Color.Black), rect);                                               //рисуем прямоугольник

            return rect;
        }

        public Rectangle SelectFigure(PointF SelectPoint)
        {

            Left = (int)SelectPoint.X - 5;
            Down = (int)SelectPoint.Y - 5;
            Top = (int)SelectPoint.Y + 5;
            Right = (int)SelectPoint.X + 5;

            Rectangle rect = Rectangle.FromLTRB(Right, Down, Left, Top);

            return rect;
        }


    }
}
