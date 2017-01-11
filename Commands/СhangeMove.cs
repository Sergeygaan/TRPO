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
    class СhangeMove : IFigureCommand
    {
        private List<Object> _seleckResult;
      
        private GraphicsPath [] _pathUndo;
        private GraphicsPath [] _pathRedo;

        private string _operatorValue;

        public СhangeMove(List<Object> SeleckResult)
        {
            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            _pathRedo = new GraphicsPath[_seleckResult.Count];

            _pathUndo = new GraphicsPath[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObjectResult in _seleckResult)
            {
                _pathUndo[i] = (GraphicsPath)SelectObjectResult.PathClone.Clone();
                i++;
            }

            _operatorValue = "Перемещение объеста";

        }

        public void СhangeMoveEnd(List<Object> SeleckResult)
        {

            _pathRedo = new GraphicsPath[SeleckResult.Count];

            int i = 0;
            foreach (Object SelectObject in SeleckResult)
            {
                _pathRedo[i] = (GraphicsPath)SelectObject.PathClone.Clone();

                i++;
            }

            _operatorValue = "Перемещение объеста";

        }


        public void Redo()
        {

            int i = 0;
            foreach (Object ObjectRedo in _seleckResult)
            {
                ObjectRedo.Path = (GraphicsPath)_pathRedo[i].Clone();
              
                i++;
            }

            _operatorValue = "Отмена перемещение объеста";

        }

        public void Undo()
        {

            int i = 0;
            foreach (Object ObjectUndo in _seleckResult)
            {
                ObjectUndo.Path = (GraphicsPath)_pathUndo[i].Clone();
               
                i++;

            }
        }


        public string Operation()
        {
            return _operatorValue;
        }
    }
}
