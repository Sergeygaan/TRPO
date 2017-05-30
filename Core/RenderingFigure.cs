using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.ObjectType;
using Core;
using Unity;
using Microsoft.Practices.Unity;

namespace MyPaint.Core
{
    /// <summary>
    /// Класс, выполняющий отрисовку фигур.
    /// </summary>
    public class DrawPaint
    {
        /// <summary>
        /// Переменная, хранящая список команд.
        /// </summary>
        private List<IFigureCommand> _iFigureCommandBuild = new List<IFigureCommand>();            

        /// <summary>
        /// Переменная, хранящая класс с командой для отчистки рабочей области.
        /// </summary>
        private CleanFigure _cleanFigure;

        /// <summary>
        /// Переменная, хранящая класс с командой с коммандами.
        /// </summary>
        private Сommands _commandClass;

        /// <summary>
        /// Переменная, хранящая класс для отрисовки фигур.
        /// </summary>
        private Drawing _drawingClass;

        /// <summary>
        /// Метод, создающий рабочую область, и инициализирующий остальные объекты.
        /// </summary>
        /// <para name = "Width">Переменная, хранящая  ширину рабочей области.</para>
        /// <para name = "Height">Переменная, хранящая  высоту рабочей области</para>
        public DrawPaint(int width, int height, Сommands commandClass)
        {
            var unityContainerInit = new UnityContainer();
            //this._drawingClass = _drawingClass;
            _drawingClass = unityContainerInit.Resolve<Drawing>(new OrderedParametersOverride(new object[] { width, height }));
            //_drawingClass = new Drawing(Width, Height);
            _iFigureCommandBuild.Add(_cleanFigure);
            _commandClass = commandClass;

        }

        /// <summary>
        /// Метод, выполняющий отрисовку фигур и возвращение области выделения.
        /// </summary>
        /// <para name = "e">Переменная, хранящая  события отрисовки.</para>
        /// <para name = "Currentfigure">Переменная, хранящая  текущую выбранную фигуру</para>
        /// <para name = "Points">Переменная, хранящая  координаты отрисовки фигуры</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки</para>
        public void Paint(PaintEventArgs e, int Currentfigure, List<PointF> Points, List<IFigureBuild> FiguresBuild, Color linecolor, int thickness, DashStyle dashstyle)
        {
            _drawingClass.Paint(e, Currentfigure, Points, FiguresBuild, linecolor, thickness, dashstyle);
        }

        /// <summary>
        /// Метод, выполняющий сохранение фигур.
        /// </summary>
        /// <para name = "Currentfigure">Переменная, хранящая  текущую выбранную фигуру</para>
        /// <para name = "Points">Переменная, хранящая  координаты отрисовки фигуры</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки</para>
        public void MouseUp(int Currentfigure, List<PointF> Points, MouseEventArgs e, Color linecolor, int thickness, DashStyle dashstyle, Color brushcolor, bool fill)
        {
            if ((Points != null) && (Points.Count > 1))
            {
                if ((Currentfigure != 3) && (Currentfigure != 4))
                {
                    EditFigure();

                    _iFigureCommandBuild[0] = _drawingClass.MouseUp(Currentfigure, Points, e, linecolor, thickness, dashstyle, brushcolor, fill); 
                    _commandClass.AddCommand(_iFigureCommandBuild);
                    
                }
                else
                {
                    if (e.Button == MouseButtons.Right)             
                    {
                        EditFigure();

                        _iFigureCommandBuild[0] = _drawingClass.MouseUp(Currentfigure, Points, e, linecolor, thickness, dashstyle, brushcolor, fill);
                        _commandClass.AddCommand(_iFigureCommandBuild);
                    }

                }
            }
        }

        /// <summary>
        /// Метод, выполняющий отрисовку всех фигур на рабочей области.
        /// </summary>
        public void RefreshBitmap()
        {
            _drawingClass.RefreshBitmap();
        }

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "e">Переменная, хранящая  события отрисовки.</para>
        /// <para name = "SeleckResult">Переменная, хранящая  список выделенных фигур.</para>
        /// <para name = "FiguresBuild">Переменная, хранящая класс отрисовки.</para>
        public void SupportPoint(List<ObjectFugure> SeleckResult, List<IFigureBuild> FiguresBuild, Color linecolor)
        {
            foreach (ObjectFugure SelectObject in SeleckResult)
            {
                if (SelectObject.SelectFigure == true)
                {
                    SelectObject.SelectFigure = false;
                    SelectObject.ClearListFigure();

                    Color ColorLine = linecolor;

                    FiguresBuild[SelectObject.CurrentFigure].AddSupportPoint(SelectObject, ColorLine);

                }

            }
        }

        /// <summary>
        /// Метод, выполняющий импорт изображения.
        /// </summary>
        /// <para name = "DrawForm">Переменная, хранящая ссылку на область отрисовки фигур.</para>
        public void SaveProject(PictureBox DrawForm)
        {
            _drawingClass.SaveProject();

            DrawForm.Image = _drawingClass.BitmapReturn();
        }


        /// <summary>
        /// Метод, выполняющий отчистку проекта.
        /// </summary>
        /// <para name = "DrawForm">Переменная, хранящая ссылку на область отрисовки фигур.</para>
        public void ClearProject(PictureBox DrawForm)
        {
            DrawForm.Image = null;
            DrawForm.Invalidate();
        }

        /// <summary>
        /// Метод, возвращающий зону выделения.
        /// </summary>
        public Rectangle SeparationZone()
        {
            return _drawingClass.SeparationZone();
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void UndoFigure()
        {
            _commandClass.UndoFigure(); 
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void RedoFigure()
        {
            _commandClass.RedoFigure();
        }


        /// <summary>
        /// Метод, выполняющий проверку списка команд и удаление лишних элементов.
        /// </summary>
        public void EditFigure()
        {
            _commandClass.EditFigure();
        }


        /// <summary>
        /// Метод, возвращяющий список команд в проекте_iFigureCommand.
        /// </summary>
        public List<IFigureCommand> IFigureCommand
        {
            get { return _commandClass.IFigureCommand; }
            set { _commandClass.IFigureCommand = value; }
        }

        /// <summary>
        /// Метод, возвращяющий список со всеми фигурами.
        /// </summary>
        public List<ObjectFugure> FiguresList
        {
            get { return _drawingClass.FiguresList; }
            set { _drawingClass.FiguresList = value; }
        }

        /// <summary>
        /// Метод, возвращяющий индекс из спика комманд.
        /// </summary>
        public int IndexCommand
        {
            get { return _commandClass.IndexCommand; }
            set { _commandClass.IndexCommand = value; }
        }
    }
}
