﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    /// <summary>
    /// Класс, выполняющий изменение положения выделенных фигур.
    /// </summary>
    class EditObject
    {
        /// <summary>
        /// Переменная, хранящая класс для построеня структуры фигур.
        /// </summary>
        private СonstructionFigure _figureBuild = new СonstructionFigure();

        /// <summary>
        /// Переменная, хранящая координаты начальной точки.
        /// </summary>
        private PointF _pointStart;

        /// <summary>
        /// Переменная, хранящая координаты конечной точки.
        /// </summary>
        private PointF _pointEnd;

        /// <summary>
        /// Метод, выполняющий перемещение объекта.
        /// </summary>
        /// <para name = "CurrObj">Переменная, хранящая объект перемещения.</para>
        /// <para name = "DeltaX">Переменная, хранящая значение дельта X.</para>
        /// <para name = "DeltaY">Переменная, хранящая значение дельта Y.</para>
        public void MoveObject(Object CurrObj, int DeltaX, int DeltaY)
        {
            CurrObj.Path.Transform(new Matrix(1, 0, 0, 1, DeltaX, DeltaY));

            MoveObjectSupport(CurrObj, DeltaX, DeltaY);
        }

        /// <summary>
        /// Метод, выполняющий перемещение опорных точек.
        /// </summary>
        /// <para name = "CurrObj">Переменная, хранящая объект перемещения.</para>
        /// <para name = "DeltaX">Переменная, хранящая значение дельта X.</para>
        /// <para name = "DeltaY">Переменная, хранящая значение дельта Y.</para>
        public void MoveObjectSupport(Object CurrObj, int DeltaX, int DeltaY)
        {

            foreach (SupportObject SelectObject in CurrObj.SelectListFigure())
            {

                if (CurrObj.CurrentFigure != MainForm.FigureType.Ellipse)
                {
                    for (int i = 0; i < CurrObj.PointSelect.Length; i++)
                    {
                        CurrObj.EditListFigure(i, _figureBuild.SelectFigure(CurrObj.PointSelect[i], CurrObj.Pen.Width));
                    }
                }
                else
                {
                    int k = 0;
                    for (int i = 0; i < CurrObj.PointSelect.Length; i +=3)
                    {
                        CurrObj.EditListFigure(k, _figureBuild.SelectFigure(CurrObj.PointSelect[i], CurrObj.Pen.Width));
                        k++;
                    }
                }
            }

        }


    }
}
