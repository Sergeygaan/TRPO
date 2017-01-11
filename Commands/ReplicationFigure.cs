using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    class ReplicationFigure : IFigureCommand
    {
        private List<Object> _seleckFigure;
        private List<Object> _saveFigure;
        private List<Object> _saveResult;
        private List<Object> _figure;
        private int _summFigureSelect;
        private int _summFigureBase;
        private string _operatorValue;
        public ReplicationFigure(List<Object> SeleckResult, List<Object> Figures)
        {
            _summFigureSelect = SeleckResult.Count;
            _summFigureBase = Figures.Count;

            _seleckFigure = SeleckResult.GetRange(0, SeleckResult.Count);

            _saveFigure = Figures.GetRange(0, Figures.Count);

            _figure = Figures;


            foreach (Object SelectObject in _seleckFigure)
            {
                _figure.Add(SelectObject.CloneObject());
                _figure[_figure.Count - 1].IdFigure = _figure.Count - 1;
            }

            _saveResult = _figure.GetRange(0, _figure.Count);
            _operatorValue = "Копирование выделенных фигур";
        }

        public void Redo()
        {
            _figure.Clear();
            _figure.InsertRange(0, _saveResult);

            _operatorValue = "Копирование выделенных фигур";
        }

        public void Undo()
        {
            foreach (Object SelectObject in _seleckFigure)
            {
                _figure.Clear();
                _figure.InsertRange(0, _saveFigure);

                int i = 0;
                foreach (Object DrawObject in _figure)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }

            }

           
            _operatorValue = "Удаление скопированных фигур";
        }

        public string Operation()
        {
            return _operatorValue;
        }

    }
}
