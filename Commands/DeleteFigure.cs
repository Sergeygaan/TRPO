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
    /// Класс, выполняющий удаление выбранных фигур
    /// </summary>
    class DeleteFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая список выделенных фигур
        /// </summary>
        private List<Object> _seleckResult;

        /// <summary>
        /// Переменная, хранящая список удаленных фигур. Которые будут использоваться для восстановления изначального списка фигур.
        /// </summary>
        private List<Object> _saveFigure;

        /// <summary>
        /// Переменная, хранящая ссылку на список фигур.
        /// </summary>
        private List<Object> _figure;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "Figures">Переменная, хранящая ссылку на список фигур.</para>
        public DeleteFigure(List<Object> SeleckResult, List<Object> Figures)
        {
            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            _saveFigure = Figures.GetRange(0, Figures.Count);

            _figure = Figures;

            foreach (Object SelectObject in _seleckResult)
            {
                _figure.RemoveAt(SelectObject.IdFigure);
                int i = 0;
                foreach (Object DrawObject in _figure)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }

            }
            _operatorValue = "Удаление выделенных фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void Redo()
        {
            foreach (Object SelectObject in _seleckResult)
            {
                _figure.RemoveAt(SelectObject.IdFigure);
                int i = 0;
                foreach (Object DrawObject in _figure)
                {
                    DrawObject.IdFigure = i;
                    i++;
                }
            }
            _operatorValue = "Удаление выделенных фигур";
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void Undo()
        {
            _figure.Clear();
            _figure.InsertRange(0, _saveFigure);


            int i = 0;
            foreach (Object DrawObject in _figure)
            {
                DrawObject.IdFigure = i;
                i++;
            }
            _operatorValue = "Восстановление выделенных фигур";
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
