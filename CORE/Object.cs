using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintedObjectsMoving {

    class Object 
    {

		private GraphicsPath _path;
        private Pen _pen;
        private PointF[] _pointSelect;
        private PointF _figureStart = new Point();                          //стартовая точка фигуры
        private PointF _figureEnd = new Point();                            //конечная точка фигуры
        private bool _selectFigure = false;
        private int _idFigure;
        private SolidBrush _brush;
        private bool _fill;

        private MainForm.FigureType _currentFigure;

        private List<SupportObject> _supportFigures = new List<SupportObject>();

        public int IdFigure
        {
            get { return _idFigure; }
            set { _idFigure = value; }
        }

        public GraphicsPath Path {
			get { return _path; }
			set { _path = value; }
        }

        public GraphicsPath PathClone
        {
            get { return (GraphicsPath)_path.Clone(); }
            set { _path = value; }
        }

        //Добавить опорную фигуру в список
        public void AddListFigure(SupportObject AddFigure)
        {
            _supportFigures.Add(AddFigure);
        }

        public void EditListFigure(int index, Rectangle Rectangles)
        {
            _supportFigures[index].Path.Reset();

            _supportFigures[index].Path.AddEllipse(Rectangles);
        }

        //Отчистить список опорных фигур
        public void ClearListFigure()
        {
            _supportFigures.Clear();
        }

        //Вернуть список опорных фигур
        public List<SupportObject> SelectListFigure()
        {
            return _supportFigures;
        }

        //Вернуть фигуры
        public MainForm.FigureType CurrentFigure
        {
            get { return _currentFigure; }
            set { _currentFigure = value; }
        }

        //Вернуть координаты
        public PointF[] PointSelect
        {
            get { return _pointSelect; }
            set { _pointSelect = value; }
        }

        public bool SelectFigure
        {
            get { return _selectFigure; }
            set { _selectFigure = value; }
        }

        public PointF FigureStart
        {
            get { return _figureStart; }
            set { _figureStart = value; }
        }

        public PointF FigureEnd
        {
            get { return _figureEnd; }
            set { _figureEnd = value; }
        }

        public Color BrushColor
        {
            get { return _brush.Color; }
            set { _brush.Color = value; }
        }

        public SolidBrush Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }

        public bool Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }
        public Pen @Pen {
			get { return _pen; }
			set { _pen = value; }
		}

		public Object(Pen pen, GraphicsPath path, Color brush, MainForm.FigureType CurrentFigure, bool Fill)
        {
            _brush = new SolidBrush(Color.Black);
            _path = path;
            _pen = pen;
            _brush.Color = brush;
            _currentFigure = CurrentFigure;
            _fill = Fill;

        }

        public Object CloneObject()
        {
            return new Object(Pen, Path.Clone() as GraphicsPath, _brush.Color, _currentFigure, _fill);
        }

    }
}
