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
        private int _top;                //верхняя координата эллипса
        private int _left;               //левая координата эллипса
        private int _down;               //нижняя координата эллипса
        private int _right;              //правая координата эллипса
        private int _height = 0;         //высота эллипса
        private int _width = 0;          //шириина эллипса

        //СВОЙСТВА
        public int Height
        {
            get { return _height; }
        }
        public int Width
        {
            get { return _width; }
        }

        //МЕТОДЫ
        public Rectangle ShowRectangle(PointF start, PointF end)
        {

            _left = (int)((start.X - end.X > 0) ? end.X : start.X);
            _down = (int)((start.Y - end.Y > 0) ? start.Y : end.Y);
            _top = (int)((start.Y - end.Y > 0) ? end.Y : start.Y);
            _right = (int)((start.X - end.X > 0) ? start.X : end.X);

            
            Rectangle rect = Rectangle.FromLTRB(_left, _top, _right, _down);

            return rect;
        }

    
        public Rectangle SelectFigure(PointF SelectPoint, float Width)
        {

            _left = (int)(SelectPoint.X - 5 - Width / 2);
            _down = (int)(SelectPoint.Y - 5 - Width / 2);
            _top = (int)(SelectPoint.Y + 5 + Width / 2);
            _right = (int)(SelectPoint.X + 5 + Width / 2);

            Rectangle rect = Rectangle.FromLTRB(_right, _down, _left, _top);

            return rect;
        }


    }
}
