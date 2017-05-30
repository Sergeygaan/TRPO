using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.ObjectType;
using SDK;

namespace MyPaint.Core
{
    
    /// <summary>
    /// Класс, выполняющий сохранение списка фигур.
    /// </summary>
    [Serializable]
    public class SaveProect : ISaveLoader
    {
        /// <summary>
        /// Переменная, хранящая список с сохранеными объектами.
        /// </summary>
        private List<PropertiesFigure> _figurePropertiesList = new List<PropertiesFigure>();

        /// <summary>
        /// Структура, хранящая основные характеристики фигуры.
        /// </summary>
        /// 
        [Serializable]
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
            public int _currentFigure;


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
        public void Save(object figures,int childWidhtSize,int childHeightSize)
        {
            _childWidhtSize = childWidhtSize;
            _childHeightSize = childHeightSize;


            List<ObjectFugure> _figures = new List<ObjectFugure>();

            _figures = (List<ObjectFugure>)figures;

            foreach (ObjectFugure selectObjectResult in _figures)
            {
                //Характеристики кисти
                _figureProperties._lineColorPen = selectObjectResult.Pen.Color;
                _figureProperties._thicknessPen = selectObjectResult.Pen.Width;
                _figureProperties._dashStylePen = selectObjectResult.Pen.DashStyle;
                _figureProperties._fill = selectObjectResult.Fill;

                if (_figureProperties._fill == true)
                {
                    _figureProperties._brushColorPen = selectObjectResult.BrushColor;

                }

                //Характеристики точек
                _figureProperties._pointFigure = selectObjectResult.Path.PathPoints;
                _figureProperties._typesFigure = selectObjectResult.Path.PathTypes;

                //Другие характеристики
                _figureProperties._idFigure = selectObjectResult.IdFigure;
                _figureProperties._currentFigure = selectObjectResult.CurrentFigure;

                _figurePropertiesList.Add(_figureProperties);
            }

        }

        /// <summary>
        /// Метод, выполняющий десериализацию списка фигур.
        /// </summary>
        public virtual object Load()
        {
            List<ObjectFugure> figures = new List<ObjectFugure>();

            foreach (PropertiesFigure loadObject in _figurePropertiesList)
            {

                Pen newPen = new Pen(loadObject._lineColorPen, loadObject._thicknessPen);
                newPen.DashStyle = loadObject._dashStylePen;

                GraphicsPath newPath = new GraphicsPath(loadObject._pointFigure, loadObject._typesFigure);


                ObjectFugure newObject = new ObjectFugure(newPen, newPath, loadObject._brushColorPen, loadObject._currentFigure, loadObject._fill);


                newObject.IdFigure = loadObject._idFigure;

                figures.Add(newObject);
            }

            return figures;

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
