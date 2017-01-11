using PaintedObjectsMoving.CORE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    class DrawPaint
    {
        private List<IFigureCommand> _iFigureCommand = new List<IFigureCommand>();
        private int _indexFigureCommand = -1;
        private СonstructionFigure _ellipse;

        private Pen _penFigure;
        private Object _drawObject;
        private Rectangle _rect;
        private SolidBrush _brush;
        private MainForm.FigureType _currentfigure;
        private bool _saveProjectClear = false;

        private List<Object> _figures;                          //Список с объектами для прорисовки
        private List<Object> _figuresLoad = new List<Object>();
        private Bitmap _bmp;

        private int _widthDraw;
        private int _heightDraw;

        //Классы комманд
        private СhangePenWidth _penWidth;
        private СhangePenColor _penColor;
        private СhangePenStyle _penStyle;
        private СhangeMove _penMove;
        private СhangeBackgroundFigure _brushColor;
        private DeleteBackgroundFigure _deleteBrush;
        private DeleteFigure _deleteFigure;
        private ReplicationFigure _replicationFigure;
        private СhangeSupportPenColor _supportPenColor;
        private CleanFigure _cleanFigure;

        public DrawPaint(int Width, int Height)
        {
            _widthDraw = Width;
            _heightDraw = Height;

            _figures = new List<Object>();
            _bmp = new Bitmap(Width, Height);

            _ellipse = new СonstructionFigure();

        }

        //Отрисовка фигур и возвращение области выделения
        public void Paint(PaintEventArgs e, MainForm.FigureType Currentfigure, List<PointF> Points, List<IFigureBuild> FiguresBuild)
        {
            _currentfigure = Currentfigure;

            if (Points.Count != 0)
            {
                StyleFigure();

                FiguresBuild[(int)_currentfigure].PaintFigure(e, Points, _penFigure);     // Отрисовка нужной фигуры

                if (Points.Count > 1)
                {
                    _rect = _ellipse.ShowRectangle(Points[0], Points[1]);
                }
            }

            e.Graphics.DrawImage(_bmp, 0, 0);

        }

        //Сохранение фигур
        public void MouseUp(MainForm.FigureType Currentfigure, List<PointF> Points, List<IFigureBuild> FiguresBuild)
        {
            StyleFigure();

            EditFigure();

            if (_currentfigure == MainForm.FigureType.PoliLine)
            {
                _brush = null;
            }

            _drawObject = new Object(_penFigure, new GraphicsPath(), _brush, _currentfigure);

            FiguresBuild[(int)_currentfigure].AddFigure(_drawObject, Points, _iFigureCommand, _figures);

        }


        public void RefreshBitmap()
        {
            if (_bmp != null) _bmp.Dispose();

            _bmp = new Bitmap(_widthDraw, _heightDraw);
            //Прорисовка всех объектов из списка

            using (Graphics DrawList = Graphics.FromImage(_bmp))
            {
                if (_saveProjectClear == true)
                {
                    DrawList.Clear(Color.White);
                    _saveProjectClear = false;
                }

                foreach (Object DrawObject in _figures)
                {
                    DrawList.DrawPath(DrawObject.Pen, DrawObject.Path);

                    if (DrawObject.Brush != null)
                    {
                        DrawList.FillPath(DrawObject.Brush, DrawObject.Path);  //Заливка
                    }

                    foreach (SupportObject SuppportObject in DrawObject.SelectListFigure())
                    {
                        DrawList.DrawPath(SuppportObject.Pen, SuppportObject.Path);
                    }
                }
            }
        }

        //Отрисовка опорных точек
        public void SupportPoint(PaintEventArgs e, List<Object> SeleckResult, List<IFigureBuild> FiguresBuild)
        {
            foreach (Object SelectObject in SeleckResult)
            {
                if (SelectObject.SelectFigure == true)
                {
                    SelectObject.SelectFigure = false;
                    SelectObject.ClearListFigure();

                    FiguresBuild[(int)SelectObject.CurrentFigure].AddSupportPoint(SelectObject);

                }

            }
        }

        //Копирование выбранных фигур
        public void ReplicationFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _replicationFigure = new ReplicationFigure(SeleckResult, _figures);

                _iFigureCommand.Add(_replicationFigure);
            }

        }

        //Удаление выбранных фигуры
        public void DeleteFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _deleteFigure = new DeleteFigure(SeleckResult, _figures);

                _iFigureCommand.Add(_deleteFigure);
            }
            
        }



        //Удаление фона у выбранных фигур
        public void DeleteBackgroundFigure(List<Object> SeleckResult)
        {

            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _deleteBrush = new DeleteBackgroundFigure(SeleckResult);

                _iFigureCommand.Add(_deleteBrush);
            }
        }


        //Изменение фона у выбранных фигур
        public void СhangeBackgroundFigure(List<Object> SeleckResult, Color ColorСhangeBackground)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _brushColor = new СhangeBackgroundFigure(SeleckResult, ColorСhangeBackground);

                _iFigureCommand.Add(_brushColor);
            }

        }

        //Изменение цвета у выбранных фигур
        public void СhangePenColorFigure(List<Object> SeleckResult, Color PenColor)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _penColor = new СhangePenColor(SeleckResult, PenColor);

                _iFigureCommand.Add(_penColor);
            }

        }

        //изменение толщины пера у выбранных фигур
        public void СhangePenWidthFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _penWidth = new СhangePenWidth(SeleckResult, MainForm.FigureProperties.thickness);

                _iFigureCommand.Add(_penWidth);
            }
            
        }

        //изменение положения фигуры при перемещении
        public void СhangeMoveFigure(List<Object> SeleckResult, string Boot)
        {
            if (SeleckResult.Count != 0)
            {
                if (Boot == "Down")
                {
                    EditFigure();

                    _penMove = new СhangeMove(SeleckResult);
                }
                else
                {
                    _penMove.СhangeMoveEnd(SeleckResult);

                    _iFigureCommand.Add(_penMove);
                }
                
            }

        }

        //Изменить стиль линий у выбранных фигур
        public void СhangePenStyleFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _penStyle = new СhangePenStyle(SeleckResult, MainForm.FigureProperties.dashstyle);

                _iFigureCommand.Add(_penStyle);
            }

        }
        //Изменение стиля линй у выделенных фигур
        public void СhangeSupportPenStyleFigure(Color NextColor, List<Object> SeleckResult)
        {

            EditFigure();

            _supportPenColor = new СhangeSupportPenColor(NextColor, SeleckResult);

            _iFigureCommand.Add(_supportPenColor);
            
        }

        public void SaveProject(PictureBox DrawForm)
        {
            _saveProjectClear = true;
            RefreshBitmap();

            DrawForm.Image = _bmp;
        }

        public void ClearProject(PictureBox DrawForm)
        {
            DrawForm.Image = null;
            DrawForm.Invalidate();
        }

        //редактирование стилей для каждой фигуры
        public void StyleFigure()
        {
            _penFigure = new Pen(MainForm.FigureProperties.linecolor, MainForm.FigureProperties.thickness);
            _penFigure.DashStyle = MainForm.FigureProperties.dashstyle;

                if (MainForm.FigureProperties.fill == false)
                {
                    _brush = null;
                }
                else
                {
                    _brush = new SolidBrush(MainForm.FigureProperties.brushcolor);
                }

        }

        // Отчищает список с фигурами
        public void Clear()
        {
            if (_figures.Count != 0)
            {
                EditFigure();

                _cleanFigure = new CleanFigure(_figures, _figuresLoad);

                _iFigureCommand.Add(_cleanFigure);
            }

            //_figures.Clear();
            //_iFigureCommand.Clear();
            //_indexFigureCommand = -1;
        }

        //Возвращение зоны выделения
        public Rectangle SeparationZone()
        {
            return _rect;
        }


        //Дейстие назад
        public void UndoFigure()
        {
            if (_indexFigureCommand >= 0)
            {
                _iFigureCommand[_indexFigureCommand].Undo();
                //_iFigureCommand.RemoveAt(_iFigureCommand.Count - 1);
                _indexFigureCommand -= 1;
            }
 
            
        }
        //Действие вперед
        public void RedoFigure()
        {
            if (_indexFigureCommand < _iFigureCommand.Count - 1)
            {
                if (_indexFigureCommand == 0)
                {
                    _indexFigureCommand += 1;
                    _iFigureCommand[_indexFigureCommand].Redo();
                }
                else
                {
                    _indexFigureCommand += 1;
                    _iFigureCommand[_indexFigureCommand].Redo();
                }
                
            }
        }


        // Проверка списка команд и удаление лишних элементов
        public void EditFigure()
        {
           
            if (_indexFigureCommand != _iFigureCommand.Count - 1)
            {

                int summ = _iFigureCommand.Count - 1 - _indexFigureCommand;

                _iFigureCommand.RemoveRange(_indexFigureCommand + 1, summ);

                _indexFigureCommand = _iFigureCommand.Count - 1;

            }

            _indexFigureCommand += 1;
            
        }


        //Метод возвращяющий список команд в проекте_iFigureCommand;

        public List<IFigureCommand> IFigureCommand
        {
            get { return _iFigureCommand; }
            set { _iFigureCommand = value; }
        }

        //Возвращяет список со всеми фигурами
        public List<Object> FiguresList
        {
            get { return _figures; }
            set { _figures = value; }
        }

        public List<Object> FiguresListObject
        {
            get { return _figures; }
            set
            {
                _figures = value;
                _figuresLoad = value.GetRange(0, value.Count);
            }
        }

    }
}
