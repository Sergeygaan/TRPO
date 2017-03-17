using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyPaint.ObjectType
{
    /// <summary>
    /// Класс, являющийся опорными точками
    /// </summary>
    public class SupportObjectFugure : ICloneable
    {
        /// <summary>
        /// Переменная, хранящая основную структуру опорных точек.
        /// </summary>
        private GraphicsPath _path;

        /// <summary>
        /// Переменная, хранящая кисть для отрисовки опорных точек.
        /// </summary>
        private Pen _pen;

        /// <summary>
        /// Переменная, хранящая номер опорной точки.
        /// </summary>
        private int _controlPointF;

        /// <summary>
        /// Переменная, хранящая номер фигуры.
        /// </summary>
        private int _idFigure;

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
        public GraphicsPath Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над номером опорной точки.
        /// </summary>
        public int ControlPointF
        {
            get { return _controlPointF; }
            set { _controlPointF = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия кистью.
        /// </summary>
        public Pen @Pen
        {
            get { return _pen; }
            set { _pen = value; }
        }

        /// <summary>
        /// Метод, выполняющий создание опорной точки.
        /// </summary>
        /// <para name = "Pen">Переменная, хранящая кисть.</para>
        /// <para name = "Path">Переменная, хранящая графическое представление фигуры.</para>
        public SupportObjectFugure(Pen Pen, GraphicsPath Path)
        {
            _path = Path;
            _pen = Pen;
        }

        #region ICloneable Members

        /// <summary>
        /// Метод, выполняющий клонирование опорной точки.
        /// </summary>
        public object Clone()
        {
            return new SupportObjectFugure(this.Pen, this.Path.Clone() as GraphicsPath);
        }

        #endregion
    }
}
