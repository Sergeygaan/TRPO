using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace PaintedObjectsMoving
{
    class EditObject
    {
        private СonstructionFigure _constructerFigure = new СonstructionFigure();

        public void EditRectangle()
        {

        }

        public void MoveObject(PaintedObject currObj, int deltaX, int deltaY)
        {
            currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));

          
        }

        public void EditObjectRectangle(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            switch (_supportObj.ControlPointF)
            {
                case 0:

                    currObj.PointSelect[0].X += deltaX;
                    currObj.PointSelect[0].Y += deltaY;
                    currObj.Path.Reset();
                    currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));
                    

                    break;

                case 1:

                    currObj.PointSelect[2].X += deltaX;
                    currObj.PointSelect[0].Y += deltaY;
                    currObj.Path.Reset();
                    currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                    break;

                case 2:

                    currObj.PointSelect[2].X += deltaX;
                    currObj.PointSelect[2].Y += deltaY;
                    currObj.Path.Reset();
                    currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                    break;

                case 3:

                    currObj.PointSelect[0].X += deltaX;
                    currObj.PointSelect[2].Y += deltaY;
                    currObj.Path.Reset();
                    currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                    break;
            }

            MoveObjectSupport(currObj, deltaX, deltaY);
        }


        public void EditObjectLine(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            switch (_supportObj.ControlPointF)
            {
                case 0:

                    currObj.PointSelect[0].X += deltaX;
                    currObj.PointSelect[0].Y += deltaY;
                    currObj.Path.Reset();
                    //currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));
                    currObj.Path.AddLine(currObj.PointSelect[0], currObj.PointSelect[1]);

                    break;

                case 1:

                    currObj.PointSelect[1].X += deltaX;
                    currObj.PointSelect[1].Y += deltaY;
                    currObj.Path.Reset();
                    currObj.Path.AddLine(currObj.PointSelect[0], currObj.PointSelect[1]);

                    break;
            }

            MoveObjectSupport(currObj, deltaX, deltaY);
        }


        public void EditObjectEllepse(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            switch (_supportObj.ControlPointF)
            {
                case 1:

                    //currObj.PointSelect[1].X += deltaX;
                    //currObj.PointSelect[1].Y += deltaY;

                    //currObj.PointSelect[3].X += deltaX;
                    //currObj.PointSelect[3].Y += deltaY;
                    currObj.Path.Reset();
                    
                    currObj.Path.AddEllipse(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[3]));

                    break;

                case 2:

                    //currObj.PointSelect[1].X += deltaX;
                    //currObj.PointSelect[1].Y += deltaY;
                    //currObj.Path.Reset();
                    //currObj.Path.AddLine(currObj.PointSelect[0], currObj.PointSelect[1]);

                    break;
            }

            MoveObjectSupport(currObj, deltaX, deltaY);
        }

        public void MoveObjectSupport(PaintedObject currObj, int deltaX, int deltaY)
        {
            foreach (SupportObject SuppportObject in currObj.SelectListFigure())
            {
                SuppportObject.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));

            }
        }


    }
}
