using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint.Command;
using MyPaint.ObjectType;
using NUnit.Framework;

namespace Nunit
{
    class AddTest
    {

        [Test, TestCaseSource("DivideCases")]

        public void TestLoad(PointF starPointF, PointF endPointF, PointF result)
        {
            AddBuildFigure addFigure = new AddBuildFigure();
            Rectangle rec = addFigure.ShowRectangle(starPointF, endPointF);

            Assert.AreEqual(result.X, rec.X);

        }
        static object[] DivideCases =
        {
            new object[]
            {
                new PointF(10, 10),
                new PointF(10, 10),
                new PointF(10, 10)
            },
                 new object[]
            {
                new PointF(1, 1),
                new PointF(1, 1),
                new PointF(1, 1)
            },
                new object[]
            {
                new PointF(0, 0),
                new PointF(0, 0),
                new PointF(0, 0)
            },
                new object[]
            {
                new PointF(-1, -1),
                new PointF(-1, -1),
                new PointF(-1, -1)
            }

        };


        [TestCase(0, "прямоугольник", TestName = "Фигура прямоугольник")]
        [TestCase(2, "линия", TestName = "Фигура линия")]
        [TestCase(3, "полилиния", TestName = "Фигура полилиния")]
        [TestCase(4, "многоугольник", TestName = "Фигура многоугольник")]
        [TestCase(5, null, TestName = "Фигура прямоугольник")]

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