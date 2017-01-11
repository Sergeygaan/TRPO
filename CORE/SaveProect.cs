using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    public class SaveProect
    {
        private List<PropertiesFigure> _figurePropertiesList = new List<PropertiesFigure>();

        [Serializable]
        public struct PropertiesFigure
        {
            public Color _lineColorPen;  
            public Color _brushColorPen; 
            public float _thicknessPen;              
            public System.Drawing.Drawing2D.DashStyle _dashStylePen;
            public bool _fill;
           //

            public PointF[] _pointFigure;
            public byte[] _typesFigure;

            public int _idFigure;
            public MainForm.FigureType _currentFigure;


        }

        //переменные окна
        public int _childWidhtSize;
        public int _childHeightSize;

        private PropertiesFigure _figureProperties;

        //public object _figureCommand = new object();

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

                if (SelectObjectResult.Brush == null)
                {
                    _figureProperties._fill = false;
                    
                }
                else
                {
                    
                    _figureProperties._brushColorPen = SelectObjectResult.Brush.Color;
                    _figureProperties._fill = true;
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

        public object LoadProject()
        {
            List<Object> _figures = new List<Object>();

            foreach (PropertiesFigure LoadObject in _figurePropertiesList)
            {

                Pen NewPen = new Pen(LoadObject._lineColorPen, LoadObject._thicknessPen);

                GraphicsPath NewPath = new GraphicsPath(LoadObject._pointFigure, LoadObject._typesFigure);


                SolidBrush NewBrush;

                if (LoadObject._fill == true)
                {
                    NewBrush = new SolidBrush(LoadObject._brushColorPen);
                }
                else
                {
                    NewBrush = null;
                }

                Object NewObject = new Object(NewPen, NewPath, NewBrush, LoadObject._currentFigure);


                NewObject.IdFigure = LoadObject._idFigure;

                _figures.Add(NewObject);
            }

            return _figures;

        }


        public List<PropertiesFigure> FigurePropertiesList()
        {
            return _figurePropertiesList;
        }


        public int ChildWidhtSize()
        {
            return _childWidhtSize;
        }

        public int ChildHeightSize()
        {
            return _childHeightSize;
        }

    }
}
