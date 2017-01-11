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
        private static FigureType _currentfigure = FigureType.Ellipse;                 //текущая выбранная фигура
        private static FigureType _previousfigure = FigureType.Ellipse;                //предыдущая выбранная фигура
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

                _drawClass.SupportPoint(e, _selectClass.SeleckResult());

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
                        _selectClass.MouseMove(e, _currentActions);
                    }

                    break;

                case Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseMove(e, _currentActions);
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

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseUpSupport();
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case Actions.Move:

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;
            }

            DrawForm.Refresh();
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

            DrawForm.Refresh();
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
            _selectClass.MouseUp();
            _drawClass.Clear();
            DrawForm.Invalidate();
        }


        //Отрисовка фигур
        private void рисоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Draw;
            _selectClass.MouseUp();
        }

        //Масштабирование выбранной фигуры
        private void масштабироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Scale;
        }

        // Перемещение выбранной фигуры
        private void перемещатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Move;
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.ReplicationFigure(_selectClass.SeleckResult());
            }
        }

        private void удаитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.DeleteFigure(_selectClass.SeleckResult());
            }
            _selectClass.MouseUp();
        }

    }
}
