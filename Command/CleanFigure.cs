using MyPaint.ObjectType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;


namespace MyPaint.Command
{
    /// <summary>
    /// Класс, выполняющий удаление выбранных фигур
    /// </summary>
    public class CleanFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, список с фигурами для отображения.
        /// </summary>
        private List<ObjectFugure> _figure ;

        /// <summary>
        /// Переменная, список фигур при загрузке.
        /// </summary>
        private List<ObjectFugure> _figureLoad;

        /// <summary>
        /// Переменная, список с фигурами для восстановления к изначальному виду.
        /// </summary>
        private List<ObjectFugure> _figureSave;


        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures) { }

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигур.
        /// </summary>
        /// <para name = "Figure">Переменная, список с фигурами для отображения.</para>
        /// <para name = "FigureLoad">Переменная, список фигур при загрузке</para>
        public  CleanFigure(List<ObjectFugure> Figure, List<ObjectFugure> FigureLoad)
        {
            _figureLoad = FigureLoad.GetRange(0, FigureLoad.Count);

            _figureSave = Figure.GetRange(0, Figure.Count);

            _figure = Figure;

            _figure.Clear();

           _operatorValue = "Удаление всех фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            _figure.Clear();

            _operatorValue = "Удаление всех фигур";
        }


        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            _figure.AddRange(_figureLoad);
            _figure.AddRange(_figureSave);

            _operatorValue = "Восстановление удаленных фигур";
        }

        /// <summary>
        /// Метод, возвращающий фигуру над которой проводятся действия.
        /// </summary>
        public string Operation()
        {
            return _operatorValue;
        }

        public ObjectFugure Output()
        {
            return null;
        }
    }
}
