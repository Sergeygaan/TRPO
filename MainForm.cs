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
            Rectangle, Ellipse, Line, RectangleSelect, PoliLine, Polygon
        }

        public enum Actions
        {
           Draw, Move, Scale, SelectRegion, SelectPoint
        }
        public struct Properties
        {
            public Color linecolor;  //цвет линии
            public Color brushcolor; //цвет заливки
            public int thickness;              //толщина линии
            /* стиль линии*/
            public System.Drawing.Drawing2D.DashStyle dashstyle;
            public bool fill; //true - фигура с заливкой, false - без заливки
        }

        public struct PropertiesSupport
        {
            public Color linecolor;  //цвет линии
        }

        //КЛАССЫ
        private DrawPaint _drawClass;
        private SelectDraw _selectClass;

        //ПЕРЕМЕННЫЕ
        private List<PointF> _points = new List<PointF>();

        private static FigureType _currentfigure = FigureType.Line;                 //текущая выбранная фигура
        private static FigureType _previousfigure = FigureType.Line;                //предыдущая выбранная фигура
        private Actions _currentActions = Actions.Draw;
        private static MainForm.Properties _figureProperties;                        //свойства фигуры
        private static MainForm.PropertiesSupport _figurePropertiesSupport;          //свойства фигуры


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

            //Характеристика фигуры
            _figureProperties.brushcolor = Color.White;
            _figureProperties.dashstyle = System.Drawing.Drawing2D.DashStyle.Solid;
            _figureProperties.fill = false;
            _figureProperties.linecolor = Color.Black;
            _figureProperties.thickness = 1;

            //Характеристика опорных точек
            _figurePropertiesSupport.linecolor = Color.Black;

        }

        //Отрисовка фигур
        void Form1_Paint(object sender, PaintEventArgs e)
        {

            RefreshBitmap();

            _drawClass.Paint(e, _currentfigure, _points);

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

                    if ((e.Button == MouseButtons.Left) && (mouseclick == true))            //если нажата левая кнопка мыши
                    {
                        switch (_currentfigure)
                        {

                            case FigureType.Line:
                            case FigureType.Ellipse:
                            case FigureType.Rectangle:

                                _points[1] = new PointF(e.Location.X, e.Location.Y);

                                break;
                        }
                    }

                    break;

                case Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseMove(e, _currentActions);
                    }

                    break;

                case Actions.SelectRegion:

                    if (e.Button == MouseButtons.Left)
                    {
                        _points[1] = new PointF(e.Location.X, e.Location.Y);
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

                        switch (_currentfigure)
                        {

                            case FigureType.Line:
                            case FigureType.Ellipse:
                            case FigureType.Rectangle:

                                mouseclick = false;

                                _points[1] = new PointF(e.Location.X, e.Location.Y);

                                _drawClass.MouseUp(_currentfigure, _points);

                                _points.Clear();

                                break;

                            case FigureType.PoliLine:

                                _points.Add(new PointF(e.Location.X, e.Location.Y));

                                break;

                            case FigureType.Polygon:

                                _points.Add(new PointF(e.Location.X, e.Location.Y));

                                break;
                        }

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

                case Actions.SelectRegion:

                    if (e.Button == MouseButtons.Left)
                    {

                        mouseclick = false;

                        _selectClass.MouseDown(e, _drawClass.SeparationZone(), _drawClass.FiguresList(), Actions.SelectRegion);

                        _points.Clear();

                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case Actions.SelectPoint:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseDown(e, _drawClass.SeparationZone(),  _drawClass.FiguresList(), Actions.SelectPoint);

                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        //_selectClass.MouseDown(e, _drawClass.SeparationZone(), _drawClass.FiguresList());

                    }

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

                        switch (_currentfigure)
                        {

                            case FigureType.Line:
                            case FigureType.Ellipse:
                            case FigureType.Rectangle:
                            case FigureType.PoliLine:
                            case FigureType.Polygon:

                                mouseclick = true;
                                _points.Add(new PointF(e.Location.X, e.Location.Y));
                                _points.Add(new PointF(e.Location.X, e.Location.Y));

                                break;
                        }
   
                    }

                    if (e.Button == MouseButtons.Right)              //если нажата правая кнопка мыши. Сохраняет полилинию
                    {

                        switch (_currentfigure)
                        {
                            case FigureType.PoliLine:

                                if (_points.Count != 0)
                                {
                                    _drawClass.MouseUp(_currentfigure, _points);
                                    _points.Clear();
                                }

                                break;
    
                            case FigureType.Polygon:

                                if (_points.Count != 0)
                                {
                                    _drawClass.MouseUp(_currentfigure, _points);
                                    _points.Clear();
                                }

                                break;
                        }
                    }


                    break;

                case Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.SavePoint(e);
                    }

                    break;

                case Actions.SelectRegion:

                    if (e.Button == MouseButtons.Left)
                    {
                       _selectClass.MouseUp();
                    
                        mouseclick = true;
                        _points.Add(new PointF(e.Location.X, e.Location.Y));
                        _points.Add(new PointF(e.Location.X, e.Location.Y));     
                    }

                    break;

                case Actions.SelectPoint:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseUp();

                    }

                    break;
                case Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.SavePoint(e);
                    }

                    break;
            }

            DrawForm.Refresh();
        }

        //Характеристики обычных фигур
        public static MainForm.Properties FigureProperties
        {
            get { return _figureProperties; }
        }
        public static int Thickness
        {
            set { _figureProperties.thickness = value; }
        }
        public static System.Drawing.Drawing2D.DashStyle StyleOfLine
        {
            set { _figureProperties.dashstyle = value; }
        }

        //Характеристики опорных точек
        public static MainForm.PropertiesSupport FigurePropertiesSupport
        {
            get { return _figurePropertiesSupport; }
        }

        //Обновление рабочей области
        void RefreshBitmap()
        {
            _drawClass.RefreshBitmap();
        }

        private void ChangeFigure(FigureType next)
        {
            _previousfigure = _currentfigure;             //указываем предыдущую выбранную фигуру
            _currentfigure = next;
        }

        //Удаление всех нарисованных фигур
        private void отчиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_points.Count != 0)
            {
                _points.Clear();
            }
            _selectClass.MouseUp();
            _drawClass.Clear();
            DrawForm.Invalidate();
        }

        //Режим рисования
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Draw;
            _selectClass.MouseUp();
            ChangeFigure(FigureType.Line);
        }

        //Режим выделения
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.SelectRegion;
            ChangeFigure(FigureType.RectangleSelect);
        }
        //Эллипс
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Ellipse);
            }
        }

        //Квадрат
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Rectangle);
            }
        }
        
        //Полилиния
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.PoliLine);
            }
        }

        //Линия
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if(_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Line);
            }
        }
        // Многоугольник
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Polygon);
            }
        }
        //Перемещение
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Move;
        }
        //Масштабирование
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Scale;
        }
        //Копирование
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.ReplicationFigure(_selectClass.SeleckResult());
            }
        }
        //Удаление
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.DeleteFigure(_selectClass.SeleckResult());
            }
            _selectClass.MouseUp();
            DrawForm.Refresh();
        }

        // Цвет отрисовки фигур
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _figureProperties.linecolor = colorDialog1.Color; 
            }
        }

        //Цвет отрисовки опорных точек
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _figurePropertiesSupport.linecolor = colorDialog1.Color; 
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            LineThickness linethicknessform = new LineThickness();  //создаем форму "Толщина линии"
            linethicknessform.Text = "Толщина линии фигуры";               //озаглавливаем форму
            linethicknessform.ShowDialog();                         //отображаем форму
            linethicknessform.Dispose();                            //уничтожаем форму
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            LineStyle linestyleform = new LineStyle();  //создаем форму "Стиль линии"
            linestyleform.Text = "Стиль линии";         //озаглавливаем форму
            linestyleform.ShowDialog();                 //отображаем форму
            linestyleform.Dispose();                    //уничтожаем форму
        }

        //Включить заливку
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _figureProperties.fill = true; 
        }

        //Отключить заливку
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            _figureProperties.fill = false;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _figureProperties.brushcolor = colorDialog1.Color;
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _drawClass.СhangeBackgroundFigure(_selectClass.SeleckResult(), colorDialog1.Color);
            }
            DrawForm.Refresh();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.DeleteBackgroundFigure(_selectClass.SeleckResult());
            }
            DrawForm.Refresh();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _drawClass.СhangePenColorFigure(_selectClass.SeleckResult(), colorDialog1.Color);
            }
            DrawForm.Refresh();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            LineThickness linethicknessform = new LineThickness();  //создаем форму "Толщина линии"
            linethicknessform.Text = "Толщина линии фигуры";               //озаглавливаем форму
            linethicknessform.ShowDialog();                         //отображаем форму
            linethicknessform.Dispose();                            //уничтожаем форму

            _drawClass.СhangePenWidthFigure(_selectClass.SeleckResult());
            
            DrawForm.Refresh();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            LineStyle linestyleform = new LineStyle();  //создаем форму "Стиль линии"
            linestyleform.Text = "Стиль линии";         //озаглавливаем форму
            linestyleform.ShowDialog();                 //отображаем форму
            linestyleform.Dispose();                    //уничтожаем форму

            _drawClass.СhangePenStyleFigure(_selectClass.SeleckResult());

            DrawForm.Refresh();
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.SelectPoint;
        }

    }
}
