//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Windows.Forms;
//using NUnit.Framework;
//using Telerik.JustMock;
//using Core;
//using MyPaint.ObjectType;

//namespace McoKTest.Core
//{
//    class ParameterChangesTest
//    {

//        public delegate void MyDelegate(ParameterChanges foo);

//        [Test, TestCaseSource("Cases")]
//        public void TestParametrChanges(MyDelegate fooDelegat)
//        {
//            var foo = Mock.Create<ParameterChanges>(Constructor.Mocked);
//            fooDelegat(foo);
//        }

//        static public void ReplicationFigureTest(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1);

//            List<ObjectFugure> List = new List<ObjectFugure>();
//            List.Add(new ObjectFugure(new Pen(Color.LightBlue),new GraphicsPath(),Color.Aqua, 1, true));

//            foo.ReplicationFigure(List, new MyPaint.Command.ReplicationFigure(List, List));
//            Mock.Assert(() => foo.ReplicationFigure(List, new MyPaint.Command.ReplicationFigure(List, List)), Occurs.AtLeastOnce());
//        }

//        static public void DeleteFigureTest(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

//            foo.DeleteFigure(new List<ObjectFugure>(), new MyPaint.Command.DeleteFigure(new List<ObjectFugure>(), new List<ObjectFugure>()));
//            Mock.Assert(() => foo.DeleteFigure(new List<ObjectFugure>(), new MyPaint.Command.DeleteFigure(new List<ObjectFugure>(), new List<ObjectFugure>())), Occurs.AtLeastOnce());
//        }

//        static public void DeleteBackgroundFigureTest(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

//            foo.DeleteBackgroundFigure(new List<ObjectFugure>(), new MyPaint.Command.DeleteBackgroundFigure(new List<ObjectFugure>()));
//            Mock.Assert(() => foo.DeleteBackgroundFigure(new List<ObjectFugure>(), new MyPaint.Command.DeleteBackgroundFigure(new List<ObjectFugure>())), Occurs.AtLeastOnce());
//        }

//        static public void СhangeBackgroundFigure(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

//            foo.СhangeBackgroundFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangeBackgroundFigure(new List<ObjectFugure>(), new Color()));
//            Mock.Assert(() => foo.СhangeBackgroundFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangeBackgroundFigure(new List<ObjectFugure>(), new Color())), Occurs.AtLeastOnce());
//        }

//        static public void СhangePenColorFigure(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

//            foo.СhangePenColorFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangePenColor(new List<ObjectFugure>(), new Color()));
//            Mock.Assert(() => foo.СhangePenColorFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangePenColor(new List<ObjectFugure>(), new Color())), Occurs.AtLeastOnce());
//        }

//        static public void СhangeMoveFigure(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());

//            foo.СhangeMoveFigure(new List<ObjectFugure>(), "a", new MyPaint.Command.СhangeMove(new List<ObjectFugure>()));
//            Mock.Assert(() => foo.СhangeMoveFigure(new List<ObjectFugure>(), "a", new MyPaint.Command.СhangeMove(new List<ObjectFugure>())), Occurs.AtLeastOnce());
//        }

//        static public void СhangePenStyleFigure(ParameterChanges foo)
//        { 
//           foo.СhangePenStyleFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangePenStyle(new List<ObjectFugure>(), new DashStyle()));

//            Mock.Assert(() => foo.СhangePenStyleFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangePenStyle(new List<ObjectFugure>(), new DashStyle())), Occurs.AtLeastOnce());
//        }
//        static public void СhangePenWidthFigure(ParameterChanges foo)
//        {
//            MouseEventArgs mouse = new MouseEventArgs(MouseButtons.Left, new int(), new int(), new int(), new int());
          
//            foo.СhangePenWidthFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangePenWidth(new List<ObjectFugure>(), new int()));
//            Mock.Assert(() => foo.СhangePenWidthFigure(new List<ObjectFugure>(), new MyPaint.Command.СhangePenWidth(new List<ObjectFugure>(), new int())), Occurs.AtLeastOnce());
//        }

//        static object[] Cases =
//        {
//            new object[]
//            {
//                new MyDelegate(ReplicationFigureTest)
//            },
//            new object[]
//            {
//                new MyDelegate(DeleteFigureTest)
//            },
//            new object[]
//            {
//                new MyDelegate(DeleteBackgroundFigureTest)
//            },
//            new object[]
//            {
//                new MyDelegate(СhangeBackgroundFigure)
//            },
//            new object[]
//            {
//                new MyDelegate(СhangePenColorFigure)
//            },
//            new object[]
//            {
//                new MyDelegate(СhangeMoveFigure)
//            },
//            new object[]
//            {
//                new MyDelegate(СhangePenWidthFigure)
//            },
//            new object[]
//            {
//                new MyDelegate(СhangePenStyleFigure)
//            },

//        };
//    }
//}
