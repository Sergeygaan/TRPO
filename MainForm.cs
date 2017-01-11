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

        PaintedObject currObj;//Объект, который в данный момент перемещается
        Point oldPoint;


        private DrawPaint _drawClass;

        //ПЕРЕМЕННЫЕ
        private Point figurestart = new Point();                          //стартовая точка фигуры
        private Point figureend = new Point();                            //конечная точка фигуры
        private static FigureType _currentfigure = FigureType.Line;                 //текущая выбранная фигура
        private static FigureType _previousfigure = FigureType.Line;                //предыдущая выбранная фигура

        //ФЛАГИ
        private bool mouseclick = false;
        
        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            //_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

            _drawClass = new DrawPaint(DrawForm.Width, DrawForm.Height);
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            RefreshBitmap();

            _drawClass.Paint(e, _currentfigure, figurestart, figureend);

        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
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
                            //currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));

                            //currObj.Path.AddRectangle(_ellipse.Show(figurestart, oldPoint));


                            currObj.Path.Reset();
                            currObj.Path.AddLine(currObj.FigureStart, oldPoint);
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

            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                figureend = e.Location;

            }

            DrawForm.Refresh();
            DrawForm.Invalidate();
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (currObj != null)
                {
                    currObj.Pen.Width -= 1;//Возвращаем ширину пера
                    currObj = null;//Убираем ссылку на объект
                }
            }

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
  
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                //Запоминаем положение курсора
                oldPoint = e.Location;
                //Ищем объект, в который попала точка.Если таких несколько, то найден будет первый по списку
                foreach (PaintedObject DrawObject in _drawClass.FiguresList())
                {
                    if (DrawObject.Path.GetBounds().Contains(e.Location))
                    {
                        currObj = DrawObject;//Запоминаем найденный объект
                        currObj.Pen.Width += 1;//Делаем перо жирнее
                        return;
                    }
                }
            }
            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                mouseclick = true;
                figurestart = e.Location;
            }


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
    }
}
