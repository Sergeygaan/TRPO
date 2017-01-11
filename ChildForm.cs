using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    public partial class ChildForm : Form
    {
        //КЛАССЫ
        private DrawPaint _drawClass;
        private SelectDraw _selectClass;

        //ПЕРЕМЕННЫЕ
        private List<PointF> _points = new List<PointF>();

        private static MainForm.FigureType _currentfigure = MainForm.FigureType.Line;                 //текущая выбранная фигура
        private static MainForm.FigureType _previousfigure = MainForm.FigureType.Line;                //предыдущая выбранная фигура
        private MainForm.Actions _currentActions = MainForm.Actions.Draw;
        private static MainForm.Properties _figureProperties;                        //свойства фигуры
        private static MainForm.PropertiesSupport _figurePropertiesSupport;          //свойства фигуры
        
        //Флаг
        private bool mouseclick = false;

        public ChildForm()
        {
            InitializeComponent();

            this.AutoScroll = true;                             //разрешаем скроллинг

            DoubleBuffered = true;

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

        private void Child_Paint(object sender, PaintEventArgs e)
        {
            RefreshBitmap();

            _drawClass.Paint(e, _currentfigure, _points);

            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.SupportPoint(e, _selectClass.SeleckResult());
            }
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            switch (_currentActions)
            {
                case MainForm.Actions.Draw:

                    if ((e.Button == MouseButtons.Left) && (mouseclick == true))            //если нажата левая кнопка мыши
                    {
                        switch (_currentfigure)
                        {

                            case MainForm.FigureType.Line:
                            case MainForm.FigureType.Ellipse:
                            case MainForm.FigureType.Rectangle:

                                _points[1] = new PointF(e.Location.X, e.Location.Y);

                                break;
                        }
                    }

                    break;

                case MainForm.Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseMove(e, _currentActions);
                    }

                    break;

                case MainForm.Actions.SelectRegion:

                    if (e.Button == MouseButtons.Left)
                    {
                        _points[1] = new PointF(e.Location.X, e.Location.Y);
                    }

                    break;

                case MainForm.Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseMove(e, _currentActions);
                    }

                    break;
            }

            DrawForm.Refresh();
        }

        private void Child__MouseUp(object sender, MouseEventArgs e)
        {
            switch (_currentActions)
            {
                case MainForm.Actions.Draw:

                    if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
                    {

                        switch (_currentfigure)
                        {

                            case MainForm.FigureType.Line:
                            case MainForm.FigureType.Ellipse:
                            case MainForm.FigureType.Rectangle:

                                mouseclick = false;

                                _points[1] = new PointF(e.Location.X, e.Location.Y);

                                _drawClass.MouseUp(_currentfigure, _points);

                                _points.Clear();

                                break;

                            case MainForm.FigureType.PoliLine:

                                _points.Add(new PointF(e.Location.X, e.Location.Y));

                                break;

                            case MainForm.FigureType.Polygon:

                                _points.Add(new PointF(e.Location.X, e.Location.Y));

                                break;
                        }

                    }

                    break;

                case MainForm.Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseUpSupport();
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case MainForm.Actions.SelectRegion:

                    if (e.Button == MouseButtons.Left)
                    {

                        mouseclick = false;

                        _selectClass.MouseDown(e, _drawClass.SeparationZone(), _drawClass.FiguresList(), MainForm.Actions.SelectRegion);

                        _points.Clear();

                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case MainForm.Actions.SelectPoint:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseDown(e, _drawClass.SeparationZone(), _drawClass.FiguresList(), MainForm.Actions.SelectPoint);

                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        _selectClass.MouseUp();
                    }

                    break;

                case MainForm.Actions.Move:

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

        private void Child1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (_currentActions)
            {
                case MainForm.Actions.Draw:

                    if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
                    {

                        switch (_currentfigure)
                        {

                            case MainForm.FigureType.Line:
                            case MainForm.FigureType.Ellipse:
                            case MainForm.FigureType.Rectangle:
                            case MainForm.FigureType.PoliLine:
                            case MainForm.FigureType.Polygon:

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
                            case MainForm.FigureType.PoliLine:

                                if (_points.Count != 0)
                                {
                                    _drawClass.MouseUp(_currentfigure, _points);
                                    _points.Clear();
                                }

                                break;

                            case MainForm.FigureType.Polygon:

                                if (_points.Count != 0)
                                {
                                    _drawClass.MouseUp(_currentfigure, _points);
                                    _points.Clear();
                                }

                                break;
                        }
                    }


                    break;

                case MainForm.Actions.Scale:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.SavePoint(e);
                    }

                    break;

                case MainForm.Actions.SelectRegion:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseUp();

                        mouseclick = true;
                        _points.Add(new PointF(e.Location.X, e.Location.Y));
                        _points.Add(new PointF(e.Location.X, e.Location.Y));
                    }

                    break;

                case MainForm.Actions.SelectPoint:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.MouseUp();

                    }

                    break;
                case MainForm.Actions.Move:

                    if (e.Button == MouseButtons.Left)
                    {
                        _selectClass.SavePoint(e);
                    }

                    break;
            }

            DrawForm.Refresh();
        }


        //Обновление рабочей области
        void RefreshBitmap()
        {
            _drawClass.RefreshBitmap();
        }

        //Удалить фигуры
        public void DeleteFigure()
        {
            if (_points.Count != 0)
            {
                _points.Clear();
            }
            _selectClass.MouseUp();
            _drawClass.Clear();
            DrawForm.Invalidate();
        }

        //Передача фигуры
        public void ChangeFigure(MainForm.FigureType next)
        {
            _previousfigure = _currentfigure;             //указываем предыдущую выбранную фигуру
            _currentfigure = next;
        }

        //Передача действий над фигурами
        public void ChangeActions(MainForm.Actions next)
        {
            _currentActions = next;
        }

        //Удаление опорных точек
        public void DeleteSupportFigure()
        {
            _selectClass.MouseUp();
            DrawForm.Refresh();
        }

        //копировать выбранную фигуру
        public void СopyFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.ReplicationFigure(_selectClass.SeleckResult());
            }
            DrawForm.Refresh();
        }

        //Удалить выбранную фигуру
        public void DeleteSelectFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.DeleteFigure(_selectClass.SeleckResult());
            }
            _selectClass.MouseUp();
            DrawForm.Refresh();
        }

        //Задать заливку у выделенной фигуры
        public void СhangeBackgroundFigure(Color ColorBlakgroung)
        {
            _drawClass.СhangeBackgroundFigure(_selectClass.SeleckResult(), ColorBlakgroung);
            DrawForm.Refresh();
        }

        //Удалить заливку у выделенной фигуры
        public void DeleteBackgroundFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.DeleteBackgroundFigure(_selectClass.SeleckResult());
            }
            DrawForm.Refresh();
        }

        //Изменить цвет линий у выбранной фигуры
        public void ColorSelectPen(Color ColorPen)
        {
            _drawClass.СhangePenColorFigure(_selectClass.SeleckResult(), ColorPen);

            DrawForm.Refresh();
        }

        public void СhangePenStyleFigure()
        {
            _drawClass.СhangePenStyleFigure(_selectClass.SeleckResult());

            DrawForm.Refresh();
        }

        public void СhangePenWidthFigure()
        {
            _drawClass.СhangePenWidthFigure(_selectClass.SeleckResult());

            DrawForm.Refresh();
        }

    }
}
