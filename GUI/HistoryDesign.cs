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
    public partial class HistoryDesign : Form
    {
        public HistoryDesign()
        {
            InitializeComponent();
        }

        public void ListBox(object ObjectCommand)
        {

            List<IFigureCommand> ListCommand = (List<IFigureCommand>)ObjectCommand;

            foreach (IFigureCommand SelectObject in ListCommand)
            {
                listBox1.Items.Add(SelectObject.Operation());
            }


        }
    }
}
