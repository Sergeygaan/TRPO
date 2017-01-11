using MyPaint.CORE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий отрисовку фигур.
    /// </summary>
    class DrawPaint
    {
        /// <summary>
        /// Переменная, хранящая список команд.
        /// </summary>
        private List<IFigureCommand> _iFigureCommand = new List<IFigureCommand>();

        /// <summary>
        /// Переменная, хранящая интекс текущей команды.
        /// </summary>
        private int _indexFigureCommand = -1;

        /// <summary>
        /// Переменная, хранящая класс для построеня структуры фигур.
        /// </summary>
        private СonstructionFigure _figureBuild;

        /// <summary>
        /// Переменная, хранящая параметры кисти для отрисовки фигур.
        /// </summary>
        private Pen _penFigure;

        /// <summary>
        /// Переменная, хранящая объекс для отрисовки фигур.
        /// </summary>
        private Object _drawObject;

        /// <summary>
        /// Переменная, хранящая прямоугольник для выделения фигур.
        /// </summary>
        private Rectangle _rect;

        /// <summary>
        /// Переменная, хранящая значения о заливки фигур.
        /// </summary>
        private bool _brushFill;

        /// <summary>
        /// Переменная, хранящая значения о текущей выбранной фигуры.
        /// </summary>
        private MainForm.FigureType _currentfigure;

        /// <summary>
        /// Переменная, хранящая значения о сохранение проекта.
        /// </summary>
        private bool _saveProjectClear = false;

        /// <summary>
        /// Переменная, хранящая хранящая список со всеми фигурами.
        /// </summary>
        private List<Object> _figures;                          //Список с объектами для прорисовки

        /// <summary>
        /// Переменная, хранящая список с фигурами при загрузке старого проекта.
        /// </summary>
        private List<Object> _figuresLoad = new List<Object>();

        /// <summary>
        /// Переменная, хранящая зону отрисовки фигур.
        /// </summary>
        private Bitmap _bmp;

        /// <summary>
        /// Переменная, хранящая ширину зоны отрисовки.
        /// </summary>
        private int _widthDraw;

        /// <summary>
        /// Переменная, хранящая высоту зоны отрисовки.
        /// </summary>
        private int _heightDraw;

        //Классы комманд
        /// <summary>
        /// Переменная, хранящая класс с командой для изменения размера кисти.
        /// </summary>
        private СhangePenWidth _penWidth;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения цвета кисти.
        /// </summary>
        private СhangePenColor _penColor;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения стиля кисти.
        /// </summary>
        private СhangePenStyle _penStyle;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения перемещения фигур.
        /// </summary>
        private СhangeMove _penMove;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения цвета заливки.
        /// </summary>
        private СhangeBackgroundFigure _brushColor;

        /// <summary>
        /// Переменная, хранящая класс с командой для удаления заливки.
        /// </summary>
        private DeleteBackgroundFigure _deleteBrush;

        /// <summary>
        /// Переменная, хранящая класс с командой для удаления фигур.
        /// </summary>
        private DeleteFigure _deleteFigure;

        /// <summary>
        /// Переменная, хранящая класс с командой для копирования фигур.
        /// </summary>
        private ReplicationFigure _replicationFigure;

        /// <summary>
        /// Переменная, хранящая класс с командой для изменения цвета опорных точек.
        /// </summary>
        private СhangeSupportPenColor _supportPenColor;

        /// <summary>
        /// Переменная, хранящая класс с командой дляотчистки рабочей области.
        /// </summary>
        private CleanFigure _cleanFigure;

        /// <summary>
        /// Метод, создающий рабочую область, и инициализирующий остальные объекты.
        /// </summary>
        /// <para name = "Width">Переменная, хранящая  ширину рабочей области.</para>
        /// <para name = "Height">Переменная, хранящая  высоту рабочей области</para>
        public DrawPaint(int Width, int Height)
        {
            _widthDraw = Width;
            _heightDraw = Height;

            _figures = new List<Object>();
            _bmp = new Bitmap(Width, Height);

            _figureBuild = new СonstructionFigure();

        }

        /// <summary>
        /// Метод, выполняющий отрисовку фигур и возвращение области выделения.
        /// </summary>
        /// <para name = "e">Переменная, хранящая  события отрисовки.</para>
        /// <para name = "Currentfigure">Переменная, хранящая  текущую выбранную фигуру</para>
        /// <para name = "Points">Переменная, хранящая  координаты отрисовки фигуры</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки</para>
        public void Paint(PaintEventArgs e, MainForm.FigureType Currentfigure, List<PointF> Points, List<IFigureBuild> FiguresBuild)
        {
            _currentfigure = Currentfigure;

            if (Points.Count != 0)
            {
                StyleFigure();

                FiguresBuild[(int)_currentfigure].PaintFigure(e, Points, _penFigure);     // Отрисовка нужной фигуры

                if (Points.Count > 1)
                {
                    _rect = _figureBuild.ShowRectangle(Points[0], Points[1]);
                }
            }

            e.Graphics.DrawImage(_bmp, 0, 0);

        }

        /// <summary>
        /// Метод, выполняющий сохранение фигур.
        /// </summary>
        /// <para name = "Currentfigure">Переменная, хранящая  текущую выбранную фигуру</para>
        /// <para name = "Points">Переменная, хранящая  координаты отрисовки фигуры</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки</para>
        public void MouseUp(MainForm.FigureType Currentfigure, List<PointF> Points, List<IFigureBuild> FiguresBuild)
        {
            StyleFigure();

            EditFigure();

            if (_currentfigure == MainForm.FigureType.PoliLine)
            {
                _brushFill = false;
            }
            else
            {
                _brushFill = ChildForm.FigureProperties.fill;
            }

            _drawObject = new Object(_penFigure, new GraphicsPath(), ChildForm.FigureProperties.brushcolor, _currentfigure, _brushFill);

            FiguresBuild[(int)_currentfigure].AddFigure(_drawObject, Points, _iFigureCommand, _figures);

        }


        /// <summary>
        /// Метод, выполняющий отрисовку всех фигур на рабочей области.
        /// </summary>
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

                    if (DrawObject.Fill == true)
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

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "e">Переменная, хранящая  события отрисовки.</para>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки.</para>
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

        /// <summary>
        /// Метод, выполняющий копирование выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void ReplicationFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _replicationFigure = new ReplicationFigure(SeleckResult, _figures);

                _iFigureCommand.Add(_replicationFigure);
            }

        }

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигуры.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void DeleteFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _deleteFigure = new DeleteFigure(SeleckResult, _figures);

                _iFigureCommand.Add(_deleteFigure);
            }
            
        }

        /// <summary>
        /// Метод, выполняющий удаление фона у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        public void DeleteBackgroundFigure(List<Object> SeleckResult)
        {

            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _deleteBrush = new DeleteBackgroundFigure(SeleckResult);

                _iFigureCommand.Add(_deleteBrush);
            }
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета фона у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "ColorСhangeBackground">Переменная, хранящая новый цвет фона.</para>
        public void СhangeBackgroundFigure(List<Object> SeleckResult, Color ColorСhangeBackground)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _brushColor = new СhangeBackgroundFigure(SeleckResult, ColorСhangeBackground);

                _iFigureCommand.Add(_brushColor);
            }

        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "PenColor">Переменная, хранящая новый цвет кисти.</para>
        public void СhangePenColorFigure(List<Object> SeleckResult, Color PenColor)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _penColor = new СhangePenColor(SeleckResult, PenColor);

                _iFigureCommand.Add(_penColor);
            }

        }

        /// <summary>
        /// Метод, выполняющий изменение толщины пера у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangePenWidthFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _penWidth = new СhangePenWidth(SeleckResult, ChildForm.FigureProperties.thickness);

                _iFigureCommand.Add(_penWidth);
            }
            
        }

        /// <summary>
        /// Метод, выполняющий изменение положения фигуры при перемещении.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        /// <para name = "Boot">Переменная, хранящая текущее действие над фигурой.</para>
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

        /// <summary>
        /// Метод, выполняющий изменения стиля линий у выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangePenStyleFigure(List<Object> SeleckResult)
        {
            if (SeleckResult.Count != 0)
            {
                EditFigure();

                _penStyle = new СhangePenStyle(SeleckResult, ChildForm.FigureProperties.dashstyle);

                _iFigureCommand.Add(_penStyle);
            }

        }

        /// <summary>
        /// Метод, выполняющий изменения цвета линий у опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет опорных точек.</para>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур.</para>
        public void СhangeSupportPenStyleFigure(Color NextColor, List<Object> SeleckResult)
        {
            EditFigure();

            _supportPenColor = new СhangeSupportPenColor(NextColor, SeleckResult);

            _iFigureCommand.Add(_supportPenColor);
            
        }

        /// <summary>
        /// Метод, выполняющий импорт изображения.
        /// </summary>
        /// <para name = "DrawForm">Переменная, хранящая ссылку на область отрисовки фигур.</para>
        public void SaveProject(PictureBox DrawForm)
        {
            _saveProjectClear = true;
            RefreshBitmap();

            DrawForm.Image = _bmp;
        }


        /// <summary>
        /// Метод, выполняющий отчистку проекта.
        /// </summary>
        /// <para name = "DrawForm">Переменная, хранящая ссылку на область отрисовки фигур.</para>
        public void ClearProject(PictureBox DrawForm)
        {
            DrawForm.Image = null;
            DrawForm.Invalidate();
        }

        /// <summary>
        /// Метод, выполняющий редактирование стилей для каждой фигуры.
        /// </summary>
        public void StyleFigure()
        {
            _penFigure = new Pen(ChildForm.FigureProperties.linecolor, ChildForm.FigureProperties.thickness);
            _penFigure.DashStyle = ChildForm.FigureProperties.dashstyle;

        }

        /// <summary>
        /// Метод, выполняющий отчищает список с фигурами.
        /// </summary>
        public void Clear()
        {
            if (_figures.Count != 0)
            {
                EditFigure();

                _cleanFigure = new CleanFigure(_figures, _figuresLoad);

                _iFigureCommand.Add(_cleanFigure);
            }
        }

        /// <summary>
        /// Метод, возвращающий зону выделения.
        /// </summary>
        public Rectangle SeparationZone()
        {
            return _rect;
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void UndoFigure()
        {
            if (_indexFigureCommand >= 0)
            {
                _iFigureCommand[_indexFigureCommand].Undo();
                //_iFigureCommand.RemoveAt(_iFigureCommand.Count - 1);
                _indexFigureCommand -= 1;
            }
 
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
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


        /// <summary>
        /// Метод, выполняющий проверку списка команд и удаление лишних элементов.
        /// </summary>
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


        /// <summary>
        /// Метод, возвращяющий список команд в проекте_iFigureCommand.
        /// </summary>
        public List<IFigureCommand> IFigureCommand
        {
            get { return _iFigureCommand; }
            set { _iFigureCommand = value; }
        }

        /// <summary>
        /// Метод, возвращяющий список со всеми фигурами.
        /// </summary>
        public List<Object> FiguresList
        {
            get { return _figures; }
            set { _figures = value; }
        }

        /// <summary>
        /// Метод, возвращяющий и принимающий список с фигурами.
        /// </summary>
        public List<Object> FiguresListObject
        {
            get { return _figures; }
            set
            {
                _figures = value;
                _figuresLoad = value.GetRange(0, value.Count);
            }
        }

        public int IndexCommand
        {
            get { return _indexFigureCommand; }
            set { _indexFigureCommand = value; }
        }

    }
}
