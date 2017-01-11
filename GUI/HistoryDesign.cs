using PaintedObjectsMoving.CORE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving.GUI
{
    /// <summary>
    /// Класс, выполняющий вывод истории проекта.
    /// </summary>
    public partial class HistoryDesign : Form
    {
        /// <summary>
        /// Переменная, хранащая список комманд
        /// </summary>
        private List<IFigureCommand> _listCommandHistory;

        /// <summary>
        /// Переменная, хранащая колличество комманд в списке.
        /// </summary>
        private int _indexCommand;

        /// <summary>
        /// Переменная, хранащая колличество удаленных комманд.
        /// </summary>
        private int _indexDelete = 0;

        /// <summary>
        /// Метод, создающий рабочую область, и инициализирующий остальные объекты.
        /// </summary>
        public HistoryDesign()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, выполняющий вывод списка комманд на экран
        /// </summary>
        /// <para name = "ObjectCommand">Переменная, хранящая список команд.</para>
        public void ListBox(object ObjectCommand, int IndexCommand)
        {
            _indexCommand = IndexCommand;

            List<IFigureCommand> ListCommand = (List<IFigureCommand>)ObjectCommand;

            _indexDelete = ListCommand.Count() - IndexCommand - 1;

            _listCommandHistory = ListCommand;

            foreach (IFigureCommand SelectObject in ListCommand)
            {
                listBox1.Items.Add(SelectObject.Operation());
            }

        }


        public int IndexCommand()
        {
            return _indexCommand;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int SummCommand = 0;

            if ((listBox1.SelectedIndex != -1) && (_indexCommand >= listBox1.SelectedIndex))
            {
                SummCommand = _listCommandHistory.Count - listBox1.SelectedIndex - _indexDelete;

                for (int i = 0; i < SummCommand; i++)
                {
                    if (_indexCommand >= 0)
                    {
                        _listCommandHistory[_indexCommand].Undo();
                        _indexDelete += 1;
                        _indexCommand -= 1;
                    }
                }
            }

            listBox1.Items.Clear();

            foreach (IFigureCommand SelectObject in _listCommandHistory)
            {
                listBox1.Items.Add(SelectObject.Operation());
            }

        }

    }
}
