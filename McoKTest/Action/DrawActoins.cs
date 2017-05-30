using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using NUnit.Framework;
using Telerik.JustMock;
using System.Windows.Forms;
using Core;
using MyPaint.Build;
using MyPaint.Core;

namespace McoKTest.Figure
{
    class DrawActoins
    {

        public delegate void MyDelegate(MyPaint.Actions.DrawActoins foo);


        [Test, TestCaseSource("Cases")]
        public void TestDeleteBackgroundFigure(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<MyPaint.Actions.DrawActoins>();

            fooDelegat(foo);
        }

        static public void MouseMove(MyPaint.Actions.DrawActoins foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);

            foo.MouseMove(mouse, 1, 1);
            Mock.Assert(() => foo.MouseMove(mouse, 1, 1), Occurs.AtLeastOnce());
        }

        static public void MouseUp(MyPaint.Actions.DrawActoins foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);

            foo.MouseUp(mouse, 1, Color.Black, 1, DashStyle.Dash, Color.Black, true);

            Mock.Assert(() => foo.MouseUp(mouse, 1, Color.Black, 1, DashStyle.Dash, Color.Black, true), Occurs.AtLeastOnce());
        }

        static public void MouseDown(MyPaint.Actions.DrawActoins foo)
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
