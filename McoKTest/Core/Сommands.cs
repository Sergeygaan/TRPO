using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using Telerik.JustMock;
using  Core;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.Core;
using MyPaint.ObjectType;

namespace McoKTest.Core
{
    class СommandsTest
    {
        public delegate void MyDelegate(Сommands foo);

        [Test, TestCaseSource("Cases")]
        public void TestСommandsTest(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<Сommands>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void UndoFigure(Сommands foo)
        {
            foo.UndoFigure();
            Mock.Assert(() => foo.UndoFigure(), Occurs.AtLeastOnce());
        }

        static public void RedoFigure(Сommands foo)
        {
            foo.RedoFigure();
            Mock.Assert(() => foo.RedoFigure(), Occurs.AtLeastOnce());
        }

        static public void EditFigure(Сommands foo)
        {
            foo.EditFigure();
            Mock.Assert(() => foo.EditFigure(), Occurs.AtLeastOnce());
        }


        static public void AddCommand(Сommands foo)
        {
            foo.AddCommand(new List<IFigureCommand>());
            Mock.Assert(() => foo.AddCommand(new List<IFigureCommand>()), Occurs.AtLeastOnce());
        }

        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(UndoFigure)
            },
            new object[]
            {
                new MyDelegate(RedoFigure)
            },
            new object[]
            {
                new MyDelegate(EditFigure)
            },
            new object[]
            {
                new MyDelegate(AddCommand)
            },

        };
    }
}
