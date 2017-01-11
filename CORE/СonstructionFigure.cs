using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий построение основной структуры фигуры.
    /// </summary>
    class СonstructionFigure
    {
        /// <summary>
        /// Переменная, хранящая верхнуую координату фигуры.
        /// </summary>
        private int _top;

        /// <summary>
        /// Переменная, хранящая левую координату фигуры.
        /// </summary>       
        private int _left;

        /// <summary>
        /// Переменная, хранящая нижнюю координату фигуры.
        /// </summary>           
        private int _down;

        /// <summary>
        /// Переменная, хранящая правую координату фигуры.
        /// </summary>        
        private int _right;

        /// <summary>
        /// Метод, выполняющий пострение структуры фигуры.
        /// </summary>
        /// <para name = "Start">Переменная, хранящая начальную координату фигуры.</para>
        /// <para name = "End">Переменная, хранящая конечную координату фигуры.</para>
        public Rectangle ShowRectangle(PointF Start, PointF End)
        {

            _left = (int)((Start.X - End.X > 0) ? End.X : Start.X);
            _down = (int)((Start.Y - End.Y > 0) ? Start.Y : End.Y);
            _top = (int)((Start.Y - End.Y > 0) ? End.Y : Start.Y);
            _right = (int)((Start.X - End.X > 0) ? Start.X : End.X);

            Rectangle rect = Rectangle.FromLTRB(_left, _top, _right, _down);

            return rect;
        }


        /// <summary>
        /// Метод, выполняющий пострение структуры опорных точек.
        /// </summary>
        /// <para name = "SelectPoint">Переменная, хранящая координату опорной точки.</para>
        /// <para name = "Width">Переменная, хранящая толщину линии.</para>
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
