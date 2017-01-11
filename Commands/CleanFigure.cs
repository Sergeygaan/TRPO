using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint.CORE
{
    /// <summary>
    /// Класс, выполняющий удаление выбранных фигур
    /// </summary>
    class CleanFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, список с фигурами для отображения.
        /// </summary>
        private List<Object> _figure ;

        /// <summary>
        /// Переменная, список фигур при загрузке.
        /// </summary>
        private List<Object> _figureLoad;

        /// <summary>
        /// Переменная, список с фигурами для восстановления к изначальному виду.
        /// </summary>
        private List<Object> _figureSave;


        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигур.
        /// </summary>
        /// <para name = "Figure">Переменная, список с фигурами для отображения.</para>
        /// <para name = "FigureLoad">Переменная, список фигур при загрузке</para>
        public CleanFigure(List<Object> Figure, List<Object> FigureLoad)
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

    }
}
