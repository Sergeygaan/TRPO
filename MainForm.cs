using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    public partial class MainForm : Form
    {
        //ПЕРЕЧИСЛЕНИЕ
        /*зададим перечисление, имеющее
         * в качестве значений названия
         * фигур, которые будут использованы
         * для определения, какую именно рисовать
         */
        public enum FigureType
        {
            Rectangle, Square, Ellipse, Circle, Curve, Line
        }

        public enum Actions
        {
           Draw, Move, Scale
        }

        //КЛАССЫ
        private DrawPaint _drawClass;
        private SelectDraw _selectClass;

        //ПЕРЕМЕННЫЕ
        private Point figurestart = new Point();                          //стартовая точка фигуры
        private Point figureend = new Point();                            //конечная точка фигуры
        private static FigureType _currentfigure = FigureType.Rectangle;                 //текущая выбранная фигура
        private static FigureType _previousfigure = FigureType.Rectangle;                //предыдущая выбранная фигура
        private Actions _currentActions = Actions.Draw;
        
        //ФЛАГИ
        private bool mouseclick = false;
        
        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            //_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

            //Инициализация классов
            _drawClass = new DrawPaint(DrawForm.Width, DrawForm.Height);
            _selectClass = new SelectDraw();
        }

        //Отрисовка фигур
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            RefreshBitmap();

            _drawClass.Paint(e, _currentfigure, figurestart, figureend);


            if (_selectClass.SeleckResult() != null)
            {

                _drawClass.SupportPoint(e, _selectClass.SeleckFigure(), _selectClass.SeleckResult());

            }

        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            switch (_currentActions)
            {
                case Actions.Draw:

                    if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
                    {
                        figureend = e.Location;

                    }


                    break;

                case Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseMove(e);
                    }

                    break;

                case Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseMove(e);
                    }

                    break;
            }

            DrawForm.Refresh();
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            switch (_currentActions)
            {
                case Actions.Draw:

                    if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
                    {
                        mouseclick = false;
                        figureend = e.Location;

                        _drawClass.MouseUp(_currentfigure, figurestart, figureend);

                        figurestart.X = 0;
                        figurestart.Y = 0;
                        figureend.X = 0;
                        figureend.Y = 0;

                    }


                    break;

                case Actions.Scale:

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseUp();
                    }

                    break;
            }

        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            switch (_currentActions)
            {
                case Actions.Draw:

                    if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
                    {
                        mouseclick = true;
                        figurestart = e.Location;
                    }


                    break;

                case Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseDown(e, _drawClass.FiguresList());
                    }

                    break;

                case Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseDown(e, _drawClass.FiguresList());
                    }

                    break;
            }

            RefreshBitmap();
        }


        void RefreshBitmap()
        {
            _drawClass.RefreshBitmap();
        }


        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFigure(FigureType.Rectangle);
        }

        private void эллипсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFigure(FigureType.Ellipse);
        }

        private void линияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeFigure(FigureType.Line);
        }

        private void ChangeFigure(FigureType next)
        {
            _previousfigure = _currentfigure;             //указываем предыдущую выбранную фигуру
            _currentfigure = next;
        }

        private void отчиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _drawClass.Clear();
            DrawForm.Invalidate();
        }

        private void рисоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Draw;
        }

        private void масштабироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Scale;
        }

        private void перемещатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Move;
        }
    }
}
