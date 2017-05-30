using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using MyPaint.Build;
using MyPaint.ObjectType;
using NUnit.Framework;
using Telerik.JustMock;
using MyPaint.ObjectType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace McoKTest.Figure
{
    class RectangleSelect
    {

        public delegate void MyDelegate(MyPaint.Build.RectangleSelect foo);

        [Test, TestCaseSource("Cases")]
        public void TestRectangleSelect(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<MyPaint.Build.RectangleSelect>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void AddFigure(MyPaint.Build.RectangleSelect foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());
            List<PointF> Line = new List<PointF>();
            Line.Add(new PointF(1, 1));
            List<ObjectFugure> objectFugur = new List<ObjectFugure>();
            objectFugur.Add(objectFugure);


            foo.AddFigure(objectFugure, Line, objectFugur);
            Mock.Assert(() => foo.AddFigure(objectFugure, Line, objectFugur), Occurs.AtLeastOnce());
        }

        static public void AddSupportPoint(MyPaint.Build.RectangleSelect foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());

            foo.AddSupportPoint(objectFugure, new Color());
            Mock.Assert(() => foo.AddSupportPoint(objectFugure, new Color()), Occurs.AtLeastOnce());
        }

        static public void ScaleSelectFigure(MyPaint.Build.RectangleSelect foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());
            SupportObjectFugure supportObjectFugure = new SupportObjectFugure(new Pen(Color.Bisque), new GraphicsPath());

            foo.ScaleSelectFigure(objectFugure, supportObjectFugure, new int(), new int());
            Mock.Assert(() => foo.ScaleSelectFigure(objectFugure, supportObjectFugure, new int(), new int()), Occurs.AtLeastOnce());
        }

        static public void ScaleFigure(MyPaint.Build.RectangleSelect foo)
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
