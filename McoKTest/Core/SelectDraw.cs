using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using Telerik.JustMock;
using MyPaint.Build;
using MyPaint.Core;
using MyPaint.ObjectType;

namespace McoKTest.Core
{
    class SelectDrawTest
    {
        public delegate void MyDelegate(SelectDraw foo);

        [Test, TestCaseSource("Cases")]
        public void TestSelectFigure(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<SelectDraw>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void MouseUp(SelectDraw foo)
        {
            foo.MouseUp();
            Mock.Assert(() => foo.MouseUp(), Occurs.AtLeastOnce());
        }

        static public void MouseUpSupport(SelectDraw foo)
        {
            foo.MouseUpSupport();
            Mock.Assert(() => foo.MouseUpSupport(), Occurs.AtLeastOnce());
        }

        static public void SavePoint(SelectDraw foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

            foo.SavePoint(mouse);
            Mock.Assert(() => foo.SavePoint(mouse), Occurs.AtLeastOnce());
        }

        static public void MouseDown(SelectDraw foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());
           
            foo.MouseDown(mouse, new Rectangle(), new List<ObjectFugure>(), new int(), new List<IFigureBuild>());
            Mock.Assert(() => foo.MouseDown(mouse, new Rectangle(), new List<ObjectFugure>(), new int(), new List<IFigureBuild>()), Occurs.AtLeastOnce());
        }

        static public void MouseMove(SelectDraw foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

            foo.MouseMove(mouse, new int(), new List<IFigureBuild>());
            Mock.Assert(() => foo.MouseMove(mouse, new int(), new List<IFigureBuild>()), Occurs.AtLeastOnce());
        }

        static public void SeleckResult(SelectDraw foo)
        {
            foo.SeleckResult();
            Mock.Assert(() => foo.SeleckResult(), Occurs.AtLeastOnce());
        }

     

        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(MouseUp)
            },
            new object[]
            {
                new MyDelegate(MouseUpSupport)
            },
            new object[]
            {
                new MyDelegate(SavePoint)
            },
            new object[]
            {
                new MyDelegate(MouseDown)
            },
            new object[]
            {
                new MyDelegate(MouseMove)
            },
            new object[]
            {
                new MyDelegate(SeleckResult)
            },

        };
    }
}
