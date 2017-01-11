using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace PaintedObjectsMoving
{
    class EditObject
    {


        public void EditRectangle()
        {

        }

        public void MoveObject(PaintedObject currObj, int deltaX, int deltaY)
        {
            currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));
        }




    }
}
