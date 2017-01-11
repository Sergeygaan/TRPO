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
        private List<Object> _seleckResult;
        private List<Object> _saveFigure;
        private List<Object> _figure;
        private int _summFigureSelect;
        private int _summFigureBase;
        private string _operatorValue;
        public ReplicationFigure(List<Object> SeleckResult, List<Object> Figures)
        {
            _summFigureSelect = SeleckResult.Count;
            _summFigureBase = Figures.Count;

            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            _saveFigure = Figures.GetRange(0, Figures.Count);

            _figure = Figures;


            foreach (Object SelectObject in _seleckResult)
            {
                _figure.Add(SelectObject.CloneObject());
                _figure[_figure.Count - 1].IdFigure = _figure.Count - 1;
            }

            _operatorValue = "Копирование выделенных фигур";
        }

        public void Redo()
        {

            foreach (Object SelectObject in _seleckResult)
            {
                _figure.Add(SelectObject.CloneObject());
                _figure[_figure.Count - 1].IdFigure = _figure.Count - 1;
            }
            _operatorValue = "Копирование выделенных фигур";
        }

        public void Undo()
        {
            _figure.RemoveRange(_summFigureBase, _summFigureSelect);
            _operatorValue = "Удаление скопированных фигур";
        }

        public string Operation()
        {
            return _operatorValue;
        }

    }
}
