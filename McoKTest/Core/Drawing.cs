using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using Telerik.JustMock;
using Core;
using MyPaint.Build;
using MyPaint.ObjectType;
using Line = McoKTest.Figure.Line;

namespace McoKTest.Core
{
    class DrawingTest
    {
 
        public delegate void MyDelegate(Drawing foo);

        [Test, TestCaseSource("Cases")]
        public void TestDraw(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<Drawing>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void MouseUp(Drawing foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());
           
            foo.MouseUp(new int(), new List<PointF>(), mouse, new Color(), new int(), new int(), new Color(), new bool());
            Mock.Assert(() => foo.MouseUp(new int(), new List<PointF>(), mouse, new Color(), new int(), new int(), new Color(), new bool()), Occurs.AtLeastOnce());
        }

        static public void RefreshBitmap(Drawing foo)
        {
            foo.RefreshBitmap();
            Mock.Assert(() => foo.RefreshBitmap(), Occurs.AtLeastOnce());
        }

        static public void StyleFigure(Drawing foo)
        {

            foo.StyleFigure(new Color(), new int(), new int());
            Mock.Assert(() => foo.StyleFigure(new Color(), new int(), new int()), Occurs.AtLeastOnce());
        }

        static public void SeparationZone(Drawing foo)
        {
            foo.SeparationZone();
            Mock.Assert(() => foo.SeparationZone(), Occurs.AtLeastOnce());
        }

        static public void BitmapReturn(Drawing foo)
        {
            foo.BitmapReturn();
            Mock.Assert(() => foo.BitmapReturn(), Occurs.AtLeastOnce());
        }


        static public void SaveProject(Drawing foo)
        {
            foo.SaveProject();
            Mock.Assert(() => foo.SaveProject(), Occurs.AtLeastOnce());
        }

        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(MouseUp)
            },
            new object[]
            {
                new MyDelegate(RefreshBitmap)
            },
            new object[]
            {
                new MyDelegate(StyleFigure)
            },
            new object[]
            {
                new MyDelegate(SeparationZone)
            },
            new object[]
            {
                new MyDelegate(BitmapReturn)
            },
            new object[]
            {
                new MyDelegate(SaveProject)
            },

        };
    }
}
