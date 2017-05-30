using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint.ObjectType;
using NUnit.Framework;
using Telerik.JustMock;

namespace McoKTest.Figure
{
    class СhangeMove
    {
        public delegate void MyDelegate(MyPaint.Command.СhangeMove foo);


        [Test, TestCaseSource("Cases")]
        public void TestDeleteBackgroundFigure(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<MyPaint.Command.СhangeMove>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void AddFigure(MyPaint.Command.СhangeMove foo)
        {
            ObjectFugure objectFugure = new ObjectFugure(new Pen(Color.Aqua), new GraphicsPath(), new Color(), new int(), new bool());

            foo.AddFigure(objectFugure, new List<PointF>(), new List<ObjectFugure>());
            Mock.Assert(() => foo.AddFigure(objectFugure, new List<PointF>(), new List<ObjectFugure>()), Occurs.AtLeastOnce());
        }

        static public void Undo(MyPaint.Command.СhangeMove foo)
        {
            foo.Undo();
            Mock.Assert(() => foo.Undo(), Occurs.AtLeastOnce());
        }

        static public void Redo(MyPaint.Command.СhangeMove foo)
        {
            foo.Redo();
            Mock.Assert(() => foo.Redo(), Occurs.AtLeastOnce());
        }

        static public void Operation(MyPaint.Command.СhangeMove foo)
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
