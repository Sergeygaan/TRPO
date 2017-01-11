using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    [Serializable]
    class EditObject
    {
        private СonstructionFigure _ellipse = new СonstructionFigure();
        private СonstructionFigure _constructerFigure = new СonstructionFigure();
        private PointF _pointStart;
        private PointF _pointEnd;


        public void MoveObject(Object CurrObj, int DeltaX, int DeltaY)
        {
            CurrObj.Path.Transform(new Matrix(1, 0, 0, 1, DeltaX, DeltaY));

            MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }

        public void EditObjectRectangle(Object CurrObj, SupportObject SupportObj, int DeltaX, int DeltaY)
        {

            if (CurrObj.IdFigure == SupportObj.IdFigure)
            {

                switch (SupportObj.ControlPointF)
                {
                    case 0:

                        if (CurrObj.PointSelect[0].X + DeltaX < CurrObj.PointSelect[1].X)
                        {
                            CurrObj.PointSelect[0].X += DeltaX;

                        }

                        if (CurrObj.PointSelect[0].Y + DeltaY < CurrObj.PointSelect[3].Y)
                        {
                            CurrObj.PointSelect[0].Y += DeltaY;

                        }
                        CurrObj.Path.Reset();
                        CurrObj.Path.AddRectangle(_constructerFigure.ShowRectangle(CurrObj.PointSelect[0], CurrObj.PointSelect[2]));


                        break;

                    case 1:

                        if (CurrObj.PointSelect[2].X + DeltaX > CurrObj.PointSelect[0].X)
                        {
                            CurrObj.PointSelect[2].X += DeltaX;

                        }

                        if (CurrObj.PointSelect[0].Y + DeltaY < CurrObj.PointSelect[2].Y)
                        {
                            CurrObj.PointSelect[0].Y += DeltaY;

                        }


                        CurrObj.Path.Reset();
                        CurrObj.Path.AddRectangle(_constructerFigure.ShowRectangle(CurrObj.PointSelect[0], CurrObj.PointSelect[2]));


                        break;

                    case 2:

                        if (CurrObj.PointSelect[2].X + DeltaX > CurrObj.PointSelect[3].X)
                        {
                            CurrObj.PointSelect[2].X += DeltaX;

                        }

                        if (CurrObj.PointSelect[2].Y + DeltaY > CurrObj.PointSelect[1].Y)
                        {
                            CurrObj.PointSelect[2].Y += DeltaY;

                        }

                        CurrObj.Path.Reset();
                        CurrObj.Path.AddRectangle(_constructerFigure.ShowRectangle(CurrObj.PointSelect[0], CurrObj.PointSelect[2]));


                        break;

                    case 3:


                        if (CurrObj.PointSelect[0].X + DeltaX < CurrObj.PointSelect[2].X)
                        {
                            CurrObj.PointSelect[0].X += DeltaX;

                        }

                        if (CurrObj.PointSelect[2].Y + DeltaY > CurrObj.PointSelect[0].Y)
                        {
                            CurrObj.PointSelect[2].Y += DeltaY;

                        }

                        CurrObj.Path.Reset();
                        CurrObj.Path.AddRectangle(_constructerFigure.ShowRectangle(CurrObj.PointSelect[0], CurrObj.PointSelect[2]));


                        break;
                }

            }

            MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }


        public void EditObjectLine(Object CurrObj, SupportObject SupportObj, int DeltaX, int DeltaY)
        {

            if (CurrObj.IdFigure == SupportObj.IdFigure)
            {
                switch (SupportObj.ControlPointF)
                {
                    case 0:

                        CurrObj.PointSelect[0].X += DeltaX;
                        CurrObj.PointSelect[0].Y += DeltaY;
                        CurrObj.Path.Reset();
                        //CurrObj.Path.AddRectangle(_constructerFigure.ShowRectangle1(CurrObj.PointSelect[0], CurrObj.PointSelect[2]));
                        CurrObj.Path.AddLine(CurrObj.PointSelect[0], CurrObj.PointSelect[1]);

                        break;

                    case 1:

                        CurrObj.PointSelect[1].X += DeltaX;
                        CurrObj.PointSelect[1].Y += DeltaY;
                        CurrObj.Path.Reset();
                        CurrObj.Path.AddLine(CurrObj.PointSelect[0], CurrObj.PointSelect[1]);

                        break;
                }
            }

           MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }


        public void EditObjectEllepse(Object CurrObj, SupportObject SupportObj, int DeltaX, int DeltaY)
        {

            if (CurrObj.IdFigure == SupportObj.IdFigure)
            {
                switch (SupportObj.ControlPointF)
                {
                    case 12:

                        if (CurrObj.PointSelect[0].X + DeltaX > CurrObj.PointSelect[6].X)
                        {
                            CurrObj.PointSelect[0].X += DeltaX;

                        }

                        CurrObj.PointSelect[0].Y += DeltaY;

                        _pointStart.X = CurrObj.PointSelect[6].X;
                        _pointStart.Y = CurrObj.PointSelect[9].Y;

                        _pointEnd.X = CurrObj.PointSelect[0].X;
                        _pointEnd.Y = CurrObj.PointSelect[3].Y;

                        CurrObj.Path.Reset();

                        CurrObj.Path.AddEllipse(_constructerFigure.ShowRectangle(_pointEnd, _pointStart));

                        break;


                    case 3:

                        if (CurrObj.PointSelect[3].Y + DeltaY > CurrObj.PointSelect[9].Y)
                        {
                            CurrObj.PointSelect[3].Y += DeltaY;

                        }

                        CurrObj.PointSelect[3].X += DeltaX;

                        _pointStart.X = CurrObj.PointSelect[6].X;
                        _pointStart.Y = CurrObj.PointSelect[9].Y;

                        _pointEnd.X = CurrObj.PointSelect[0].X;
                        _pointEnd.Y = CurrObj.PointSelect[3].Y;

                        //_pointStart.X += DeltaX;
                        //_pointStart.Y += DeltaY;

                        CurrObj.Path.Reset();

                        CurrObj.Path.AddEllipse(_constructerFigure.ShowRectangle(_pointStart, _pointEnd));

                        break;

                    case 6:

                        if (CurrObj.PointSelect[6].X + DeltaX < CurrObj.PointSelect[0].X)
                        {
                            CurrObj.PointSelect[6].X += DeltaX;

                        }

                        CurrObj.PointSelect[6].Y += DeltaY;

                        _pointStart.X = CurrObj.PointSelect[6].X;
                        _pointStart.Y = CurrObj.PointSelect[9].Y;

                        _pointEnd.X = CurrObj.PointSelect[0].X;
                        _pointEnd.Y = CurrObj.PointSelect[3].Y;

                        //_pointStart.X += DeltaX;
                        //_pointStart.Y += DeltaY;

                        CurrObj.Path.Reset();

                        CurrObj.Path.AddEllipse(_constructerFigure.ShowRectangle(_pointStart, _pointEnd));

                        break;

                    case 9:
                        if (CurrObj.PointSelect[9].Y + DeltaY < CurrObj.PointSelect[3].Y)
                        {
                            CurrObj.PointSelect[9].Y += DeltaY;

                        }

                        CurrObj.PointSelect[9].X += DeltaX;

                        _pointStart.X = CurrObj.PointSelect[6].X;
                        _pointStart.Y = CurrObj.PointSelect[9].Y;

                        _pointEnd.X = CurrObj.PointSelect[0].X;
                        _pointEnd.Y = CurrObj.PointSelect[3].Y;

                        //_pointStart.X += DeltaX;
                        //_pointStart.Y += DeltaY;

                        CurrObj.Path.Reset();

                        CurrObj.Path.AddEllipse(_constructerFigure.ShowRectangle(_pointStart, _pointEnd));

                        break;
                }
            }

            MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }

        public void EditObjectPoliLine(Object CurrObj, SupportObject SupportObj, int DeltaX, int DeltaY)
        {

            if (CurrObj.IdFigure == SupportObj.IdFigure)
            {
               
                CurrObj.PointSelect[SupportObj.ControlPointF].X += DeltaX;
                CurrObj.PointSelect[SupportObj.ControlPointF].Y += DeltaY;
                                       
                PointF[] PointF = CurrObj.PointSelect.ToArray();
                CurrObj.Path.Reset();
                CurrObj.Path.AddLines(PointF);

                if (CurrObj.CurrentFigure == MainForm.FigureType.Polygon)
                {
                    CurrObj.Path.CloseFigure();
                }
                
            }
            MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }


        
        public void MoveObjectSupport(Object CurrObj, int DeltaX, int DeltaY)
        {

            foreach (SupportObject SelectObject in CurrObj.SelectListFigure())
            {

                if (CurrObj.CurrentFigure != MainForm.FigureType.Ellipse)
                {
                    for (int i = 0; i < CurrObj.PointSelect.Length; i++)
                    {
                        CurrObj.EditListFigure(i, _ellipse.SelectFigure(CurrObj.PointSelect[i], CurrObj.Pen.Width));
                    }
                }
                else
                {
                    int k = 0;
                    for (int i = 0; i < CurrObj.PointSelect.Length; i +=3)
                    {
                        CurrObj.EditListFigure(k, _ellipse.SelectFigure(CurrObj.PointSelect[i], CurrObj.Pen.Width));
                        k++;
                    }
                }
            }

        }


    }
}
