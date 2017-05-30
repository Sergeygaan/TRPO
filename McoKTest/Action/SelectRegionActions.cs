using System.Drawing;
using System.Drawing.Drawing2D;
using NUnit.Framework;
using Telerik.JustMock;
using System.Windows.Forms;

namespace McoKTest.Figure
{
    class SelectRegionActions
    {
        public delegate void MyDelegate(MyPaint.Actions.SelectRegionActions foo);


        [Test, TestCaseSource("Cases")]
        public void TestDeleteBackgroundFigure(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<MyPaint.Actions.SelectRegionActions>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void MouseMove(MyPaint.Actions.SelectRegionActions foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);

            foo.MouseMove(mouse, 1, 1);
            Mock.Assert(() => foo.MouseMove(mouse, 1, 1), Occurs.AtLeastOnce());
        }

        static public void MouseUp(MyPaint.Actions.SelectRegionActions foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 2, 1, 1);

            foo.MouseUp(mouse, 1, Color.Azure, 2, DashStyle.DashDotDot, Color.Bisque, false);

        }

        static public void MouseDown(MyPaint.Actions.SelectRegionActions foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);

            foo.MouseDown(mouse, 1);
            Mock.Assert(() => foo.MouseDown(mouse, 1), Occurs.AtLeastOnce());
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

        };
    }
}
