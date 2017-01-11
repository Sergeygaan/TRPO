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
    [Serializable]
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
        public Rectangle ShowRectangle(PointF start, PointF end)
        {

            Left = (int)((start.X - end.X > 0) ? end.X : start.X);
            Down = (int)((start.Y - end.Y > 0) ? start.Y : end.Y);
            Top = (int)((start.Y - end.Y > 0) ? end.Y : start.Y);
            Right = (int)((start.X - end.X > 0) ? start.X : end.X);

            
            Rectangle rect = Rectangle.FromLTRB(Left, Top , Right , Down );

            return rect;
        }

    
        public Rectangle SelectFigure(PointF SelectPoint, float Width)
        {
            
            Left = (int)(SelectPoint.X - 5 - Width / 2);
            Down = (int)(SelectPoint.Y - 5 - Width / 2);
            Top = (int)(SelectPoint.Y + 5 + Width / 2);
            Right = (int)(SelectPoint.X + 5 + Width / 2);

            Rectangle rect = Rectangle.FromLTRB(Right, Down, Left, Top);

            return rect;
        }


    }
}
