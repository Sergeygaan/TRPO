using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPaint.ObjectType;
using NUnit.Framework;
using Telerik.JustMock;

namespace McoKTest.СonstructioObject
{
    class SupportObjectFugureTest
    {
        public delegate void MyDelegate(SupportObjectFugure foo);

        [Test, TestCaseSource("Cases")]
        public void TestSupportObjectFugure(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<SupportObjectFugure>();
            fooDelegat(foo);
        }

        static public void IdFigure(SupportObjectFugure foo)
        {
            Mock.Assert(() => foo.IdFigure, Occurs.Never());
        }

        static public void Path(SupportObjectFugure foo)
        {
            Mock.Assert(() => foo.Path, Occurs.Never());
        }

        static public void ControlPointF(SupportObjectFugure foo)
        {
            Mock.Assert(() => foo.ControlPointF, Occurs.Never());
        }

        static public void SupportObjectFugure(SupportObjectFugure foo)
        {
            foo.Clone();
            Mock.Assert(() => foo.Clone(), Occurs.AtLeastOnce());
        }
        static public void @Pen(SupportObjectFugure foo)
        {
            Mock.Assert(() => foo.@Pen, Occurs.Never());
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
                new MyDelegate(ControlPointF)
            },
            new object[]
            {
                new MyDelegate(SupportObjectFugure)
            },
            new object[]
            {
                new MyDelegate(@Pen)
            },


        };
    }
}
