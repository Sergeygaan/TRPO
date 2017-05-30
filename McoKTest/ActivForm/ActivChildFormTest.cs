using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ActivForm;
using NUnit.Framework;
using Telerik.JustMock;

namespace McoKTest.ActivForm
{
    class ActivChildFormTest
    {
        public delegate void MyDelegate(ActivChildForm foo);

        [Test, TestCaseSource("Cases")]
        public void TestActivForms(MyDelegate fooDelegat)
        {
            var foo = Mock.Create<ActivChildForm>(Constructor.Mocked);
            fooDelegat(foo);
        }

        static public void Child_MouseMove(ActivChildForm foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left,1, 1, 1, 1);
          
            foo.Child_MouseMove(mouse, 1, 1);
            Mock.Assert(() => foo.Child_MouseMove(mouse,1, 1), Occurs.AtLeastOnce());
        }

        static public void Child_MouseUp(ActivChildForm foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1,1);
    
            foo.Child_MouseUp(mouse, 1, 1, Color.Black, 1, DashStyle.Dash, Color.Black, true);
            Mock.Assert(() => foo.Child_MouseUp(mouse, 1, 1, Color.Black, 1, DashStyle.Dash, Color.Black, true), Occurs.AtLeastOnce());
        }

        static public void Child1_MouseDown(ActivChildForm foo)
        {
            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);

            foo.Child1_MouseDown(mouse, 1, 1);
            Mock.Assert(() => foo.Child1_MouseDown(mouse, 1, 1), Occurs.AtLeastOnce());
        }

        static public void RefreshBitmap(ActivChildForm foo)
        {

            foo.RefreshBitmap();
            Mock.Assert(() => foo.RefreshBitmap(), Occurs.AtLeastOnce());
        }

        static public void DeleteFigure(ActivChildForm foo)
        {
            foo.DeleteFigure();
            Mock.Assert(() => foo.DeleteFigure(), Occurs.AtLeastOnce());
        }

        static public void DeleteSupportFigure(ActivChildForm foo)
        {
            foo.DeleteSupportFigure();
            Mock.Assert(() => foo.DeleteSupportFigure(), Occurs.AtLeastOnce());
        }



        static public void UndoFigure(ActivChildForm foo)
        {
            foo.UndoFigure();
            Mock.Assert(() => foo.UndoFigure(), Occurs.AtLeastOnce());
        }
        static public void RedoFigure(ActivChildForm foo)
        {
            foo.RedoFigure();
            Mock.Assert(() => foo.RedoFigure(), Occurs.AtLeastOnce());
        }
        static public void СhangeSupportPenStyleFigure(ActivChildForm foo)
        {
            foo.ClearProject();
            Mock.Assert(() => foo.ClearProject(), Occurs.AtLeastOnce());
        }
      
        static public void SelectFigure(ActivChildForm foo)
        {
            foo.SelectFigure();
            Mock.Assert(() => foo.SelectFigure(), Occurs.AtLeastOnce());
        }


        static object[] Cases =
        {
            new object[]
            {
                new MyDelegate(Child_MouseMove)
            },
            new object[]
            {
                new MyDelegate(Child_MouseUp)
            },
            new object[]
            {
                new MyDelegate(Child1_MouseDown)
            },
            new object[]
            {
                new MyDelegate(RefreshBitmap)
            },
            new object[]
            {
                new MyDelegate(DeleteSupportFigure)
            },
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
                new MyDelegate(СhangeSupportPenStyleFigure)
            },
            new object[]
            {
                new MyDelegate(SelectFigure)
            },

        };

    }
}
