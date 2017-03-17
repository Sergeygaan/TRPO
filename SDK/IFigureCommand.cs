using System.Collections.Generic;
using System.Drawing;
using MyPaint.ObjectType;

namespace MyPaint.Command
{
    /// <summary>
    /// Интерфейс для реализации патерна "Команда".
    /// </summary>
    public interface IFigureCommand
    {
        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        void Redo();

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        void Undo();

        /// <summary>
        /// Метод, возвращающий строку с текущим действием.
        /// </summary>
        string Operation();

        void AddFigure(ObjectFugure DrawObject, List<PointF> Points, List<ObjectFugure> Figures);

        ObjectFugure Output();

    }
}
