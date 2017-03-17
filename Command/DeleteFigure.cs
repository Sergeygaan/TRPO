using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.ObjectType;

namespace MyPaint.Command
{

    /// <summary>
    /// Класс, выполняющий удаление выбранных фигур
    /// </summary>
    public class DeleteFigure : IFigureCommand
    {
        /// <summary>
        /// Переменная, хранящая список выделенных фигур
        /// </summary>
        private List<ObjectFugure> _seleckResult;

        /// <summary>
        /// Переменная, хранящая список удаленных фигур. Которые будут использоваться для восстановления изначального списка фигур.
        /// </summary>
        private List<ObjectFugure> _saveFigure;

        /// <summary>
        /// Переменная, хранящая ссылку на список фигур.
        /// </summary>
        private List<ObjectFugure> _figure;

        /// <summary>
        /// Переменная, хранящая строку с текущим действием.
        /// </summary>
        private string _operatorValue;

        public void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures) { }

        /// <summary>
        /// Метод, выполняющий удаление выбранных фигур.
        /// </summary>
        /// <para name = "SeleckResult">Переменная, хранящая список выделенных фигур</para>
        /// <para name = "Figures">Переменная, хранящая ссылку на список фигур.</para>
        public DeleteFigure(List<ObjectFugure> SeleckResult, List<ObjectFugure> Figures)
        {
            _seleckResult = SeleckResult.GetRange(0, SeleckResult.Count);

            _saveFigure = Figures.GetRange(0, Figures.Count);

            _figure = Figures;

            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                _figure.RemoveAt(SelectObject.IdFigure);
                int i = 0;
                foreach (ObjectFugure DrawObject in _figure)
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
            foreach (ObjectFugure SelectObject in _seleckResult)
            {
                _figure.RemoveAt(SelectObject.IdFigure);
                int i = 0;
                foreach (ObjectFugure DrawObject in _figure)
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
            foreach (ObjectFugure DrawObject in _figure)
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

        public ObjectFugure Output()
        {
            return null;
        }
    }
}
