using MyPaint.ObjectType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint.Build
{
    /// <summary>
    /// Интерфейс класса, выполнящий различные действия над фигурами.
    /// </summary>
    public interface IFigureBuild
    {
        /// <summary>
        /// Метод, выполняющий отрисовку фигур при построении.
        /// </summary>
        /// <para name = "e">Объект хранящий данные для отображения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "PenFigure">Кисть которая будет использоваться в построение эллипса</para>
        void PaintFigure(PaintEventArgs e, List<PointF> Points, Pen PenFigure);

        /// <summary>
        /// Метод, выполняющий сохранение фигур.
        /// </summary>
        /// <para name = "DrawObject">Переменна для хранения эллипса</para>
        /// <para name = "Points">Точки для построения эллипса</para>
        /// <para name = "FiguresBuild">Список комманд для хранения комманды построения эллипса</para>
        /// <para name = "Figures">Список объектов для хранения всех фигур</para>
        void AddFigure(ObjectFugure DrawObject, List<PointF> Points,  List<ObjectFugure> Figures);

        /// <summary>
        /// Метод, выполняющий отрисовку опорных точек.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно построить опорные точки</para>
        void AddSupportPoint(ObjectFugure SelectObject, Color ColorLine);

        /// <summary>
        /// Метод, отвечающий за перемещение и масштабирование фигур.
        /// </summary>
        /// <para name = "SelectObject">Переменная хранащая объект для которого нужно выполнять действия</para>
        /// <para name = "SupportObj">Переменная хранащая опорные точки выбранного объекта</para>
        /// <para name = "DeltaX">Переменная хранащая разницу по координате X</para>
        /// <para name = "DeltaY">Переменная хранащая разницу по координате Y</para>
        /// /// <para name = "EdipParametr">Объекта класса необходимый для выполнения масштабирования</para>
        void ScaleSelectFigure(ObjectFugure SelectObject, SupportObjectFugure SupportObj, int DeltaX, int DeltaY);

        /// <summary>
        /// Метод, выполняющий выделение фигуры
        /// </summary>
        /// <para name = "e">Переменная хранащая значение координат курсора мыши</para>
        /// <para name = "DrawObject">Переменная хранащая объект выделения</para>
        /// <para name = "SelectedFigures">Список выделенных объектов</para>
        void ScaleFigure(MouseEventArgs e, ObjectFugure DrawObject, List<ObjectFugure> SelectedFigures);

    }
}
