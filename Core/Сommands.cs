using MyPaint.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Сommands
    {

        /// <summary>
        /// Переменная, хранящая список команд.
        /// </summary>
        private List<IFigureCommand> _iFigureCommand = new List<IFigureCommand>();

        /// <summary>
        /// Переменная, хранящая интекс текущей команды.
        /// </summary>
        private int _indexFigureCommand = -1;

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void UndoFigure()
        {
            if (_indexFigureCommand >= 0)
            {
                _iFigureCommand[_indexFigureCommand].Undo();
                _indexFigureCommand -= 1;
            }

        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void RedoFigure()
        {
            if (_indexFigureCommand < _iFigureCommand.Count - 1)
            {
                if (_indexFigureCommand == 0)
                {
                    _indexFigureCommand += 1;
                    _iFigureCommand[_indexFigureCommand].Redo();
                }
                else
                {
                    _indexFigureCommand += 1;
                    _iFigureCommand[_indexFigureCommand].Redo();
                }

            }
        }

        /// <summary>
        /// Метод, выполняющий проверку списка команд и удаление лишних элементов.
        /// </summary>
        public void EditFigure()
        {

            if (_indexFigureCommand != _iFigureCommand.Count - 1)
            {

                int summ = _iFigureCommand.Count - 1 - _indexFigureCommand;

                _iFigureCommand.RemoveRange(_indexFigureCommand + 1, summ);

                _indexFigureCommand = _iFigureCommand.Count - 1;

            }

            _indexFigureCommand += 1;

        }

        /// <summary>
        /// Метод, возвращяющий и принимающий список команд в проекте_iFigureCommand.
        /// </summary>
        public List<IFigureCommand> IFigureCommand
        {
            get { return _iFigureCommand; }
            set { _iFigureCommand = value; }
        }

        /// <summary>
        /// Метод, возвращяющий и принимающий индекс комманд.
        /// </summary>
        public int IndexCommand
        {
            get { return _indexFigureCommand; }
            set { _indexFigureCommand = value; }
        }

        public void AddCommand(List<IFigureCommand> command)
        {
            _iFigureCommand.Add(command[0]);
        }


        }
}
