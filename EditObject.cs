using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    class EditObject
    {
        private СonstructionFigure _constructerFigure = new СonstructionFigure();
        private PointF _pointStart;
        private PointF _pointEnd;


        public void MoveObject(PaintedObject currObj, int deltaX, int deltaY)
        {
            currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));
        }

        public void EditObjectRectangle(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            if (currObj.IdFigure == _supportObj.IdFigure)
            {

                switch (_supportObj.ControlPointF)
                {
                    case 0:

                        if (currObj.PointSelect[0].X + deltaX < currObj.PointSelect[1].X)
                        {
                            currObj.PointSelect[0].X += deltaX;

                        }

                        if (currObj.PointSelect[0].Y + deltaY < currObj.PointSelect[3].Y)
                        {
                            currObj.PointSelect[0].Y += deltaY;

                        }
                        currObj.Path.Reset();
                        currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                        break;

                    case 1:

                        if (currObj.PointSelect[2].X + deltaX > currObj.PointSelect[0].X)
                        {
                            currObj.PointSelect[2].X += deltaX;

                        }

                        if (currObj.PointSelect[0].Y + deltaY < currObj.PointSelect[2].Y)
                        {
                            currObj.PointSelect[0].Y += deltaY;

                        }
                        

                        currObj.Path.Reset();
                        currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                        break;

                    case 2:

                        if (currObj.PointSelect[2].X + deltaX > currObj.PointSelect[3].X)
                        {
                            currObj.PointSelect[2].X += deltaX;

                        }

                        if (currObj.PointSelect[2].Y + deltaY > currObj.PointSelect[1].Y)
                        {
                            currObj.PointSelect[2].Y += deltaY;

                        }

                        currObj.Path.Reset();
                        currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                        break;

                    case 3:


                        if (currObj.PointSelect[0].X + deltaX < currObj.PointSelect[2].X)
                        {
                            currObj.PointSelect[0].X += deltaX;

                        }

                        if (currObj.PointSelect[2].Y + deltaY > currObj.PointSelect[0].Y)
                        {
                            currObj.PointSelect[2].Y += deltaY;

                        }

                        currObj.Path.Reset();
                        currObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(currObj.PointSelect[0], currObj.PointSelect[2]));


                        break;
                }

            }

           // MoveObjectSupport(currObj, deltaX, deltaY);
        }


        public void EditObjectLine(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            if (currObj.IdFigure == _supportObj.IdFigure)
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
            }

           // MoveObjectSupport(currObj, deltaX, deltaY);
        }


        public void EditObjectEllepse(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            if (currObj.IdFigure == _supportObj.IdFigure)
            {
                switch (_supportObj.ControlPointF)
                {
                    case 12:

                        if (currObj.PointSelect[0].X + deltaX > currObj.PointSelect[6].X)
                        {
                            currObj.PointSelect[0].X += deltaX;

                        }

                        currObj.PointSelect[0].Y += deltaY;

                        _pointStart.X = currObj.PointSelect[6].X;
                        _pointStart.Y = currObj.PointSelect[9].Y;

                        _pointEnd.X = currObj.PointSelect[0].X;
                        _pointEnd.Y = currObj.PointSelect[3].Y;

                        currObj.Path.Reset();

                        currObj.Path.AddEllipse(_constructerFigure.ShowRectangle1(_pointEnd, _pointStart));

                        break;


                    case 3:

                        if (currObj.PointSelect[3].Y + deltaY > currObj.PointSelect[9].Y)
                        {
                            currObj.PointSelect[3].Y += deltaY;

                        }

                        currObj.PointSelect[3].X += deltaX;

                        _pointStart.X = currObj.PointSelect[6].X;
                        _pointStart.Y = currObj.PointSelect[9].Y;

                        _pointEnd.X = currObj.PointSelect[0].X;
                        _pointEnd.Y = currObj.PointSelect[3].Y;

                        //_pointStart.X += deltaX;
                        //_pointStart.Y += deltaY;

                        currObj.Path.Reset();

                        currObj.Path.AddEllipse(_constructerFigure.ShowRectangle1(_pointStart, _pointEnd));

                        break;

                    case 6:

                        if (currObj.PointSelect[6].X + deltaX < currObj.PointSelect[0].X)
                        {
                            currObj.PointSelect[6].X += deltaX;

                        }

                        currObj.PointSelect[6].Y += deltaY;

                        _pointStart.X = currObj.PointSelect[6].X;
                        _pointStart.Y = currObj.PointSelect[9].Y;

                        _pointEnd.X = currObj.PointSelect[0].X;
                        _pointEnd.Y = currObj.PointSelect[3].Y;

                        //_pointStart.X += deltaX;
                        //_pointStart.Y += deltaY;

                        currObj.Path.Reset();

                        currObj.Path.AddEllipse(_constructerFigure.ShowRectangle1(_pointStart, _pointEnd));

                        break;

                    case 9:
                        if (currObj.PointSelect[9].Y + deltaY < currObj.PointSelect[3].Y)
                        {
                            currObj.PointSelect[9].Y += deltaY;

                        }

                        currObj.PointSelect[9].X += deltaX;

                        _pointStart.X = currObj.PointSelect[6].X;
                        _pointStart.Y = currObj.PointSelect[9].Y;

                        _pointEnd.X = currObj.PointSelect[0].X;
                        _pointEnd.Y = currObj.PointSelect[3].Y;

                        //_pointStart.X += deltaX;
                        //_pointStart.Y += deltaY;

                        currObj.Path.Reset();

                        currObj.Path.AddEllipse(_constructerFigure.ShowRectangle1(_pointStart, _pointEnd));

                        break;
                }
            }

            // MoveObjectSupport(currObj, deltaX, deltaY);
        }

        public void EditObjectPoliLine(PaintedObject currObj, SupportObject _supportObj, int deltaX, int deltaY)
        {

            if (currObj.IdFigure == _supportObj.IdFigure)
            {
                for (int i = 0; i < 3; i++)
                {
                    currObj.PointSelect[_supportObj.ControlPointF + i].X += deltaX;
                    currObj.PointSelect[_supportObj.ControlPointF + i].Y += deltaY;
                }
                        
                PointF[] PointF = currObj.PointSelect.ToArray();
                currObj.Path.Reset();
                currObj.Path.AddLines(PointF);
                
            }
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
