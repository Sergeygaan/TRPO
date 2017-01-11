using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.CORE
{
    /// <summary>
    /// Класс, выполняющий копирование выбранных фигур
    /// </summary>
    class ReplicationFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая скопированый список выделенных фигур
        /// </summary>
        private List<Object> _seleckFigure;

        /// <summary>
        /// Переменная, хранящая скопированый список всех фигур
        /// </summary>
        private List<Object> _saveFigure;

        /// <summary>
        /// Переменная, хранящая скопированый список скопированных фигур
        /// </summary>
        private List<Object> _saveResult;

        /// <summary>
        /// Переменная, хранящая ссылку на список фигур
        /// </summary>
        private List<Object> _figure;

        /// <summary>
        /// Переменная, хранящая количество элементов в списке с выделенными фигурами.
        /// </summary>
        private int _summFigureSelect;

        /// <summary>
        /// Переменная, хранящая количество элементов в списке со всеми фигурами.
        /// </summary>
        private int _summFigureBase;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Метод, выполняющий копирование выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "Figures">Переменная, хранящая ссылку на список фигур.</para>
        public ReplicationFigure(List<Object> SeleckResult, List<Object> Figures)
        {
            _summFigureSelect = SeleckResult.Count;
            _summFigureBase = Figures.Count;

            _seleckFigure = SeleckResult.GetRange(0, SeleckResult.Count);

            _saveFigure = Figures.GetRange(0, Figures.Count);

            _figure = Figures;


            foreach (Object SelectObject in _seleckFigure)
            {
                _figure.Add(SelectObject.CloneObject());
                _figure[_figure.Count - 1].IdFigure = _figure.Count - 1;
            }

            _saveResult = _figure.GetRange(0, _figure.Count);
            _operatorValue = "Копирование выделенных фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            _figure.Clear();
            _figure.InsertRange(0, _saveResult);

            _operatorValue = "Копирование выделенных фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            foreach (Object SelectObject in _seleckFigure)
            {
                _figure.Clear();
                _figure.InsertRange(0, _saveFigure);

                int i = 0;
                foreach (Object DrawObject in _figure)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }

            }

           
            _operatorValue = "Удаление скопированных фигур";
        }

        /// <summary>
        /// Метод, возвращающий строку с текущим действием.
        /// </summary>
        public string Operation()
        {
            return _operatorValue;
        }

    }
}
