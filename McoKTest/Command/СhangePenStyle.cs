using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint.ObjectType;
using NUnit.Framework;
using Telerik.JustMock;

namespace McoKTest.Figure
{
    class СhangePenStyle
    {
       
        public delegate void MyDelegate(MyPaint.Command.СhangePenStyle foo);


        [Test, TestCaseSource("Cases")]
        public void TestDeleteBackgroundFigure(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<MyPaint.Command.СhangePenStyle>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void AddFigure(MyPaint.Command.СhangePenStyle foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());

            foo.AddFigure(objectFugure, new List<PointF>(), new List<ObjectFugure>());
            Mock.Assert(() => foo.AddFigure(objectFugure, new List<PointF>(), new List<ObjectFugure>()), Occurs.AtLeastOnce());
        }

        static public void Undo(MyPaint.Command.СhangePenStyle foo)
        {
            foo.Undo();
            Mock.Assert(() => foo.Undo(), Occurs.AtLeastOnce());
        }

        static public void Redo(MyPaint.Command.СhangePenStyle foo)
        {
            foo.Redo();
            Mock.Assert(() => foo.Redo(), Occurs.AtLeastOnce());
        }

        static public void Operation(MyPaint.Command.СhangePenStyle foo)
        {
            foo.Operation();
            Mock.Assert(() => foo.Operation(), Occurs.AtLeastOnce());
        }

        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(AddFigure)
            },
            new object[]
            {
                new MyDelegate(Undo)
            },
            new object[]
            {
                new MyDelegate(Redo)
            },
            new object[]
            {
                new MyDelegate(Operation)
            },

        };
    }
}
