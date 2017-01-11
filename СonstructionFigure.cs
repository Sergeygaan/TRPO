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

        //КОНСТРУКТОРЫ
        /*кроме стартовой точки привязки и конечной точки
         * мы получаем остальные свойства эллипса
         * с помощью структуры, описанной в Methods.
         * Стартовую точку привязки передаем в конструктор
         * родительского класса Figure
         */
        //public Ellipse(Point start, Point end, bool circle) 
        //{
        //    if ((end.X < 0) || (end.Y < 0))
        //    {
        //        end.X = (end.X < 0) ? 0 : end.X;
        //        end.Y = (end.Y < 0) ? 0 : end.Y;
        //    }
        //    else
        //    {
        //        /* если флаг поставлен в значение "Круг",
        //         * то стартовую точку оставим прежней, а
        //         * конечную немного изменим
        //         */
        //        if (circle)
        //        {
        //            int bufwidth = Math.Abs(start.X - end.X);   //вычислим значение высоты круга
        //            /*и зададим ширину фигуры, равную полученной высоте.
        //             * присваиваем значение в зависимости от направления
        //             * движения мыши, которое вычисляем по знаку разности
        //             * стартовой и конечной точки
        //             */
        //            end.Y = (start.Y - end.Y > 0) ? start.Y - bufwidth : start.Y + bufwidth;
        //        }
        //        /* в зависимости от того, как мы поведем мышью,
        //         * левой верхней координатой и правой нижней
        //         * могут оказаться любые из четырех координат,
        //         * соответственно двух точек: начала рисования фигуры и конца.
        //         * Поэтому нам необходимо определить, какая координата, какой является.
        //         */
        //        //if (Figure.EnterCount == 1)
        //        //{
        //        //    if (circle) Methods.AddString("Определение координат круга...");
        //        //    else Methods.AddString("Определение координат эллипса...");
        //        //}
        //        Left = (start.X - end.X > 0) ? end.X : start.X;
        //        Down = (start.Y - end.Y > 0) ? start.Y : end.Y;
        //        Top = (start.Y - end.Y > 0) ? end.Y : start.Y;
        //        Right = (start.X - end.X > 0) ? start.X : end.X;
        //    }
        //    //if (Figure.EnterCount == 1)
        //    //{
        //    //    if (circle) Methods.AddString("Определение свойств круга...");
        //    //    else Methods.AddString("Определение свойств эллипса...");
        //    //}
        //    //linecolor = rectproperties.linecolor;           //задаем цвет линии
        //    //brushcolor = rectproperties.brushcolor;         //задаем цвет заливки
        //    //thickness = rectproperties.thickness;           //задаем толщину линии
        //    //dashstyle = rectproperties.dashstyle;           //задаем стиль линии
        //    //fill = rectproperties.fill;                     //определяем необходимость заливки
        //    width = Math.Abs(end.X - start.X);              //вычисляем высоту эллипса
        //    height = Math.Abs(end.Y - start.Y);             //вычисляем ширину эллипса
        //}

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
