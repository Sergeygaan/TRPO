using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint;
using MyPaint.Command;
using MyPaint.ObjectType;
using NUnit.Framework;

namespace Nunit
{
    class SaveLoad
    { 

        [TestCase(0, "прямоугольник", TestName = "Фигура прямоугольник")]
        [TestCase(2, "линия", TestName = "Фигура линия")]
        [TestCase(3, "полилиния", TestName = "Фигура полилиния")]
        [TestCase(4, "многоугольник", TestName = "Фигура многоугольник")]
    
        public void TestAddFigure(int index, string figure)
        {
            AddBuildFigure addFigure = new AddBuildFigure();

            ObjectFugure drawObject = new ObjectFugure(new Pen(Color.Red), new GraphicsPath(), new Color(), index, true);

            List<PointF> point = new List<PointF>();
            point.Add(new PointF());
            point.Add(new PointF());


            addFigure.AddFigure(drawObject, point, new List<ObjectFugure>());


            Assert.AreEqual(figure, addFigure.TypeFigure());
        }
    }
}