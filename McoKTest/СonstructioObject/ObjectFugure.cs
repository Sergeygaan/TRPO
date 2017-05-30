using System.Drawing;
using System.Drawing.Drawing2D;
using MyPaint.ObjectType;
using NUnit.Framework;
using Telerik.JustMock;

namespace McoKTest.СonstructioObject
{
    class ObjectFugureTest
    {
        public delegate void MyDelegate(ObjectFugure foo);

        [Test, TestCaseSource("Cases")]
        public void TestObjectFugure(MyDelegate fooDelegat)
        {
            //ObjectFugure foo = new ObjectFugure(new Pen(Color.AliceBlue), new GraphicsPath(), Color.Bisque, 1, t );
             var foo = Mock.Create<ObjectFugure>();
            fooDelegat(foo);
        }

        static public void IdFigure(ObjectFugure foo)
        {
            Mock.Assert(() => foo.IdFigure, Occurs.Never());
        }

        static public void Path(ObjectFugure foo)
        {
            Mock.Assert(() => foo.Path, Occurs.Never());
        }

        static public void PathClone(ObjectFugure foo)
        {
            Mock.Assert(() => foo.PathClone, Occurs.Never());
        }


        static public void EditListFigure(ObjectFugure foo)
        {
            foo.EditListFigure(new int(), new Rectangle());
            Mock.Assert(() => foo.EditListFigure(new int(), new Rectangle()), Occurs.AtLeastOnce());
        }

        static public void ClearListFigure(ObjectFugure foo)
        {
            foo.ClearListFigure();
            Mock.Assert(() => foo.ClearListFigure(), Occurs.AtLeastOnce());
        }

        static public void SelectListFigure(ObjectFugure foo)
        {
            foo.SelectListFigure();
            Mock.Assert(() => foo.SelectListFigure(), Occurs.AtLeastOnce());
        }
        static public void CurrentFigure(ObjectFugure foo)
        {
            Mock.Assert(() => foo.CurrentFigure, Occurs.Never());
        }
        static public void PointSelect(ObjectFugure foo)
        {
            Mock.Assert(() => foo.PointSelect, Occurs.Never());
        }
        static public void SelectFigure(ObjectFugure foo)
        {
            Mock.Assert(() => foo.SelectFigure, Occurs.Never());
        }
        static public void FigureStart(ObjectFugure foo)
        {
            Mock.Assert(() => foo.FigureStart, Occurs.Never());
        }
        static public void FigureEnd(ObjectFugure foo)
        {
            Mock.Assert(() => foo.FigureEnd, Occurs.Never());
        }
        static public void BrushColor(ObjectFugure foo)
        {
            Mock.Assert(() => foo.BrushColor, Occurs.Never());
        }
        static public void Brush(ObjectFugure foo)
        {
            Mock.Assert(() => foo.Brush, Occurs.Never());
        }
        static public void Fill(ObjectFugure foo)
        {
            Mock.Assert(() => foo.Fill, Occurs.Never());
        }
        static public void @Pen(ObjectFugure foo)
        {
            Mock.Assert(() => foo.@Pen, Occurs.Never());
        }
        static public void CloneObject(ObjectFugure foo)
        {
            foo.CloneObject();
            Mock.Assert(() => foo.CloneObject(), Occurs.AtLeastOnce());
        
        }

        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(IdFigure)
            },
            new object[]
            {
                new MyDelegate(Path)
            },
            new object[]
            {
                new MyDelegate(PathClone)
            },
            new object[]
            {
                new MyDelegate(EditListFigure)
            },
            new object[]
            {
                new MyDelegate(ClearListFigure)
            },
            new object[]
            {
                new MyDelegate(CurrentFigure)
            },
            new object[]
            {
                new MyDelegate(PointSelect)
            },
            new object[]
            {
                new MyDelegate(SelectFigure)
            },
            new object[]
            {
                new MyDelegate(FigureStart)
            },
            new object[]
            {
                new MyDelegate(FigureEnd)
            },
            new object[]
            {
                new MyDelegate(BrushColor)
            },
            new object[]
            {
                new MyDelegate(Brush)
            },
            new object[]
            {
                new MyDelegate(Fill)
            },
            new object[]
            {
                new MyDelegate(@Pen)
            },
            new object[]
            {
                new MyDelegate(CloneObject)
            },

        };
    }
}
