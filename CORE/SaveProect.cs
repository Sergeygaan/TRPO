using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace MyPaint.CORE
{
    [Serializable]
    /// <summary>
    /// Класс, выполняющий сохранение списка фигур.
    /// </summary>
    public class SaveProect
    {
        /// <summary>
        /// Переменная, хранящая список с сохранеными объектами.
        /// </summary>
        private List<PropertiesFigure> _figurePropertiesList = new List<PropertiesFigure>();

        [Serializable]
        /// <summary>
        /// Структура, хранящая основные характеристики фигуры.
        /// </summary>
        public struct PropertiesFigure
        {
            /// <summary>
            /// Переменная, хранящая цвет кисти.
            /// </summary>
            public Color _lineColorPen;

            /// <summary>
            /// Переменная, хранящая цвет заливки.
            /// </summary>
            public Color _brushColorPen;

            /// <summary>
            /// Переменная, хранящая толщину кисти.
            /// </summary>
            public float _thicknessPen;

            /// <summary>
            /// Переменная, хранящая стиль кисти.
            /// </summary>
            public System.Drawing.Drawing2D.DashStyle _dashStylePen;

            /// <summary>
            /// Переменная, хранящая параметры заливки.
            /// </summary>
            public bool _fill;

            /// <summary>
            /// Переменная, хранящая набор точек фигуры.
            /// </summary>
            public PointF[] _pointFigure;

            /// <summary>
            /// Переменная, хранящая тип структуры.
            /// </summary>
            public byte[] _typesFigure;

            /// <summary>
            /// Переменная, хранящая номер фигуры.
            /// </summary>
            public int _idFigure;

            /// <summary>
            /// Переменная, хранящая тип фигуры.
            /// </summary>
            public MainForm.FigureType _currentFigure;


        }
        /// <summary>
        /// Переменная, хранящая ширину окна.
        /// </summary>
        private int _childWidhtSize;

        /// <summary>
        /// Переменная, хранящая высоту окна.
        /// </summary>
        private int _childHeightSize;

        /// <summary>
        /// Переменная, хранящая стурктуру для сохранения.
        /// </summary>
        private PropertiesFigure _figureProperties;

        /// <summary>
        /// Метод, выполняющий сериализацию списка фигур.
        /// </summary>
        /// <para name = "Figures">Переменная, хранящая список фигур.</para>
        /// <para name = "ChildWidhtSize">Переменная, хранящая ширину окна.</para>
        /// <para name = "ChildHeightSize">Переменная, хранящая высоту окна.</para>
        public SaveProect(object Figures,int ChildWidhtSize,int ChildHeightSize)
        {
            _childWidhtSize = ChildWidhtSize;
            _childHeightSize = ChildHeightSize;


            List<Object> _figures = new List<Object>();

            _figures = (List<Object>)Figures;

            foreach (Object SelectObjectResult in _figures)
            {
                //Характеристики кисти
                _figureProperties._lineColorPen = SelectObjectResult.Pen.Color;
                _figureProperties._thicknessPen = SelectObjectResult.Pen.Width;
                _figureProperties._dashStylePen = SelectObjectResult.Pen.DashStyle;
                _figureProperties._fill = SelectObjectResult.Fill;

                if (_figureProperties._fill == true)
                {
                    _figureProperties._brushColorPen = SelectObjectResult.BrushColor;

                }

                //Характеристики точек
                _figureProperties._pointFigure = SelectObjectResult.Path.PathPoints;
                _figureProperties._typesFigure = SelectObjectResult.Path.PathTypes;

                //Другие характеристики
                _figureProperties._idFigure = SelectObjectResult.IdFigure;
                _figureProperties._currentFigure = SelectObjectResult.CurrentFigure;

                _figurePropertiesList.Add(_figureProperties);
            }

        }

        /// <summary>
        /// Метод, выполняющий десериализацию списка фигур.
        /// </summary>
        public object LoadProject()
        {
            List<Object> _figures = new List<Object>();

            foreach (PropertiesFigure LoadObject in _figurePropertiesList)
            {

                Pen NewPen = new Pen(LoadObject._lineColorPen, LoadObject._thicknessPen);
                NewPen.DashStyle = LoadObject._dashStylePen;

                GraphicsPath NewPath = new GraphicsPath(LoadObject._pointFigure, LoadObject._typesFigure);


                Object NewObject = new Object(NewPen, NewPath, LoadObject._brushColorPen, LoadObject._currentFigure, LoadObject._fill);


                NewObject.IdFigure = LoadObject._idFigure;

                _figures.Add(NewObject);
            }

            return _figures;

        }

        /// <summary>
        /// Метод, возвращающий список фигур.
        /// </summary>
        public List<PropertiesFigure> FigurePropertiesList()
        {
            return _figurePropertiesList;
        }

        /// <summary>
        /// Метод, возвращающий ширину окна.
        /// </summary>
        public int ChildWidhtSize()
        {
            return _childWidhtSize;
        }

        /// <summary>
        /// Метод, возвращающий высоту окна.
        /// </summary>
        public int ChildHeightSize()
        {
            return _childHeightSize;
        }

    }
}
