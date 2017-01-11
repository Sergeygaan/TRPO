using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    class CleanFigure : IFigureCommand
    {
        private List<Object> _figure ;
        private List<Object> _figureLoad;
        private List<Object> _figureSave;

        private string _operatorValue;

        public CleanFigure(List<Object> Figure, List<Object> FigureLoad)
        {
            _figureLoad = FigureLoad.GetRange(0, FigureLoad.Count);

            _figureSave = Figure.GetRange(0, Figure.Count);

            _figure = Figure;

            _figure.Clear();

           _operatorValue = "Удаление всех фигур";
        }

        public void Redo()
        {
            _figure.Clear();

            _operatorValue = "Удаление всех фигур";
        }

        public void Undo()
        {
            _figure.AddRange(_figureLoad);
            _figure.AddRange(_figureSave);

            _operatorValue = "Восстановление удаленных фигур";
        }

        public string Operation()
        {
            return _operatorValue;
        }

    }
}
