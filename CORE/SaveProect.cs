using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintedObjectsMoving.CORE
{
    [Serializable]
    public class SaveProect 
    {

        //public object _figureCommand = new object();

       
        private object _figures = new object();

        public SaveProect()
        {
  
        }

        public void Save(object FigureCommand, object Figures)
        {
            //_figureCommand = FigureCommand;
            _figures = Figures;
        }

    }
}
