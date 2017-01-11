using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class Data
    {
        private СonstructionFigure _constructionFigure = new СonstructionFigure();

        private Color _linecolor;  //цвет линии
        private Color _brushcolor; //цвет заливки
        private int _thickness;              //толщина линии
                                            /* стиль линии*/
        private System.Drawing.Drawing2D.DashStyle _dashstyle;
        private bool _fill; //true - фигура с заливкой, false - без заливки


        private List<PointF> _points = new List<PointF>();
        private PaintEventArgs _eventArgs;


        // Параметры кисти



        // Параметры заливки

        public СonstructionFigure СonstructionFigure
        {
            get { return _constructionFigure; }
        }

        public PaintEventArgs EventArgs
        {
            get { return _eventArgs; }
            set { _eventArgs = value; }
        }

        public Color Linecolor
        {
            get { return _linecolor; }
            set { _linecolor = value; }
        }

        public int Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        public System.Drawing.Drawing2D.DashStyle Dashstyle
        {
            get { return _dashstyle; }
            set { _dashstyle = value; }
        }

        public Color Brushcolor
        {
            get { return _brushcolor; }
            set { _brushcolor = value; }
        }

        public bool Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        public List<PointF> Points
        {
            get { return _points; }
            set { _points = value; }
        }

    }
}
