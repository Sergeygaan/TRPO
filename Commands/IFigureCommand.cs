using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPaint.CORE
{
    /// <summary>
    /// Интерфейс для реализации патерна "Команда".
    /// </summary>
    interface IFigureCommand
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

    }
}
