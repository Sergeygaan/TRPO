using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyPaint.ObjectType
{

    /// <summary>
    /// Класс, являющийся основным объектом построения
    /// </summary>
    public class ObjectFugure 
    {
        /// <summary>
        /// Переменная, хранящая основную структуру фигуры.
        /// </summary>
        private GraphicsPath _path;

        /// <summary>
        /// Переменная, хранящая кисть для отрисовки фигуры.
        /// </summary>
        private Pen _pen;

        /// <summary>
        /// Переменная, хранящая набор точек фигуры.
        /// </summary>
        private PointF[] _pointSelect;

        /// <summary>
        /// Переменная, хранящая начальную точку.
        /// </summary>
        private PointF _figureStart = new Point();

        /// <summary>
        /// Переменная, хранящая конечную точку.
        /// </summary>                
        private PointF _figureEnd = new Point();

        /// <summary>
        /// Переменная, хранящая значение выбрана ли фигура.
        /// </summary>                        
        private bool _selectFigure = false;

        /// <summary>
        /// Переменная, хранящая номер фигуры.
        /// </summary>
        private int _idFigure;

        /// <summary>
        /// Переменная, хранящая заливку фигуры.
        /// </summary>
        private SolidBrush _brush;

        /// <summary>
        /// Переменная, хранящая значение заливки.
        /// </summary>
        private bool _fill;

        /// <summary>
        /// Переменная, хранящая тип фигуры.
        /// </summary>
        private int _currentFigure;

        /// <summary>
        /// Переменная, хранящая список опорных точек.
        /// </summary>
        private List<SupportObjectFugure> _supportFigures = new List<SupportObjectFugure>();

        /// <summary>
        /// Метод, выполняющий действия над номером фигуры.
        /// </summary>
        public int IdFigure
        {
            get { return _idFigure; }
            set { _idFigure = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над графическим представлением фигуры.
        /// </summary>
        public GraphicsPath Path {
			get { return _path; }
			set { _path = value; }
        }

        /// <summary>
        /// Метод, выполняющий клонирование фигуры.
        /// </summary>
        public GraphicsPath PathClone
        {
            get { return (GraphicsPath)_path.Clone(); }
            set { _path = value; }
        }

        /// <summary>
        /// Метод, выполняющий добавление опорных точек в список.
        /// </summary>
        public void AddListFigure(SupportObjectFugure AddFigure)
        {
            _supportFigures.Add(AddFigure);
        }

        /// <summary>
        /// Метод, выполняющий перерисовку опорных точек.
        /// </summary>
        public void EditListFigure(int index, Rectangle Rectangles)
        {
            _supportFigures[index].Path.Reset();

            _supportFigures[index].Path.AddEllipse(Rectangles);
        }

        /// <summary>
        /// Метод, выполняющий удаление опорных точек.
        /// </summary>
        public void ClearListFigure()
        {
            _supportFigures.Clear();
        }

        /// <summary>
        /// Метод, выполняющий возврат опорных точек.
        /// </summary>
        public List<SupportObjectFugure> SelectListFigure()
        {
            return _supportFigures;
        }

        /// <summary>
        /// Метод, выполняющий возврат типа фигуры.
        /// </summary>
        public int CurrentFigure
        {
            get { return _currentFigure; }
            set { _currentFigure = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над точками в фигуре.
        /// </summary>
        public PointF[] PointSelect
        {
            get { return _pointSelect; }
            set { _pointSelect = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над выбранной фигурой.
        /// </summary>
        public bool SelectFigure
        {
            get { return _selectFigure; }
            set { _selectFigure = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия начальной координатой.
        /// </summary>
        public PointF FigureStart
        {
            get { return _figureStart; }
            set { _figureStart = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия конечной координатой.
        /// </summary>
        public PointF FigureEnd
        {
            get { return _figureEnd; }
            set { _figureEnd = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия цветом заливки.
        /// </summary>
        public Color BrushColor
        {
            get { return _brush.Color; }
            set { _brush.Color = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия заливкой.
        /// </summary>
        public SolidBrush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия заливкой.
        /// </summary>
        public bool Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия кистью.
        /// </summary>
        public Pen @Pen {
			get { return _pen; }
			set { _pen = value; }
		}

        /// <summary>
        /// Метод, выполняющий создание объекта.
        /// </summary>
        /// <para name = "Pen">Переменная, хранящая кисть.</para>
        /// <para name = "Path">Переменная, хранящая графическое представление фигуры.</para>
        /// <para name = "Brush">Переменная, хранящая заливки.</para>
        /// <para name = "CurrentFigure">Переменная, хранящая тип фигуры.</para>
        /// <para name = "Fill">Переменная, хранящая параметны заливки.</para>
        public ObjectFugure(Pen Pen, GraphicsPath Path, Color Brush, int CurrentFigure, bool Fill)
        {
            _brush = new SolidBrush(Color.Black);
            _path = Path;
            _pen = Pen;
            _brush.Color = Brush;
            _currentFigure = CurrentFigure;
            _fill = Fill;

        }

        /// <summary>
        /// Метод, выполняющий создание клона объекта.
        /// </summary>
        public ObjectFugure CloneObject()
        {
            return new ObjectFugure(Pen, Path.Clone() as GraphicsPath, _brush.Color, _currentFigure, _fill);
        }

    }
}
