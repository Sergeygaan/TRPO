using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint.Build;
using MyPaint.ObjectType;
using NUnit.Framework;
using Telerik.JustMock;
using System.Windows.Forms;

namespace McoKTest.Figure
{
    [TestFixture]
    class Line
    {
        public delegate void MyDelegate(MyPaint.Build.Line foo);

        [Test, TestCaseSource("Cases")]
        public void TestLine(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<MyPaint.Build.Line>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void MouseMove(MyPaint.Build.Line foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

            foo.MouseMove(new List<PointF>(), mouse);
            Mock.Assert(() => foo.MouseMove(new List<PointF>(), mouse), Occurs.AtLeastOnce());
        }

        static public void MouseUp(MyPaint.Build.Line foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

            foo.MouseUp(new List<PointF>(), mouse, new int(), new List<IFigureBuild>());
            Mock.Assert(() => foo.MouseUp(new List<PointF>(), mouse, new int(), new List<IFigureBuild>()), Occurs.AtLeastOnce());
        }

        static public void MouseDown(MyPaint.Build.Line foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

            foo.MouseDown(new List<PointF>(), mouse, new int(), new List<IFigureBuild>());
            Mock.Assert(() => foo.MouseDown(new List<PointF>(), mouse, new int(), new List<IFigureBuild>()), Occurs.AtLeastOnce());
        }

        static public void AddFigure(MyPaint.Build.Line foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());
            List<PointF> Line = new List<PointF>();
            Line.Add(new PointF(1, 1));
            List<ObjectFugure> objectFugur = new List<ObjectFugure>();
            objectFugur.Add(objectFugure);


            foo.AddFigure(objectFugure, Line, objectFugur);
            Mock.Assert(() => foo.AddFigure(objectFugure, Line, objectFugur), Occurs.AtLeastOnce());
        }

        static public void AddSupportPoint(MyPaint.Build.Line foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());

            foo.AddSupportPoint(objectFugure, new Color());
            Mock.Assert(() => foo.AddSupportPoint(objectFugure, new Color()), Occurs.AtLeastOnce());
        }

        static public void ScaleSelectFigure(MyPaint.Build.Line foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());
            SupportObjectFugure supportObjectFugure = new SupportObjectFugure(new Pen(Color.Bisque), new GraphicsPath());

            foo.ScaleSelectFigure(objectFugure, supportObjectFugure, new int(), new int());
            Mock.Assert(() => foo.ScaleSelectFigure(objectFugure, supportObjectFugure, new int(), new int()), Occurs.AtLeastOnce());
        }

        static public void ScaleFigure(MyPaint.Build.Line foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

            foo.ScaleFigure(mouse, objectFugure, new List<ObjectFugure>());
            Mock.Assert(() => foo.ScaleFigure(mouse, objectFugure, new List<ObjectFugure>()), Occurs.AtLeastOnce());
        }

        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(MouseMove)
            },
            new object[]
            {
                new MyDelegate(MouseUp)
            },
            new object[]
            {
                new MyDelegate(MouseDown)
            },
            new object[]
            {
                new MyDelegate(AddFigure)
            },
            new object[]
            {
                new MyDelegate(AddSupportPoint)
            },
            new object[]
            {
                new MyDelegate(ScaleSelectFigure)
            },
            new object[]
            {
                new MyDelegate(ScaleFigure)
            },

        };
    }
}
