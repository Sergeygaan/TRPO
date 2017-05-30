using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ActivForm;


using Microsoft.Practices.Unity;
using Unity;

namespace MyPaint
{
    /// <summary>
    /// Класс, являющийся дочерний формой для отрисовки фигур
    /// </summary>
    public partial class ChildForm : Form
    {
        /// <summary>
        /// Структура, хранящая основные характеристики фигуры.
        /// </summary>
        public struct Properties
        {
            /// <summary>
            /// Переменная, хранящая цвет линии.
            /// </summary>
            public Color linecolor;

            /// <summary>
            /// Переменная, хранящая цвет заливки.
            /// </summary>
            public Color brushcolor;

            /// <summary>
            /// Переменная, хранящая толщину линии.
            /// </summary>
            public int thickness;

            /// <summary>
            /// Переменная, хранящая стиль линии.
            /// </summary>
            public DashStyle dashstyle;

            /// <summary>
            /// Переменная, хранящая значение заливки.
            /// </summary>
            public bool fill;
        }

        /// <summary>
        /// Структура, хранящая основные характеристики опорных точек.
        /// </summary>
        public struct PropertiesSupport
        {
            /// <summary>
            /// Переменная, хранящая цвет линии.
            /// </summary>
            public Color linecolor;
        }

        /// <summary>
        /// Переменная, хранящая ссылку на родительскую форму.
        /// </summary>
        private MainForm _mainForm;

    

        /// <summary>
        /// Переменная, хранящая текущую выбранную фигуру.
        /// </summary>
        private static int _currentfigure = 0;

        /// <summary>
        /// Переменная, хранящая предыдущую выбранную фигуру.
        /// </summary>
        private static int _previousfigure = 0;

        /// <summary>
        /// Переменная, хранящая текущее действое.
        /// </summary>       
        private int _currentActions = 0;

        /// <summary>
        /// Переменная, значения для сохранения файла.
        /// </summary>
        private bool _fileSave = false;

        /// <summary>
        /// Структура, хранящая свойства фигур.
        /// </summary>
        private static Properties _figureProperties;

        /// <summary>
        /// Структура, хранящая свойства опорных точек.
        /// </summary>
        private static PropertiesSupport _figurePropertiesSupport;

        private ActivChildForm _activFormMain;

        private InitializationData _initializatioFormMain;


        /// <summary>
        /// Метод, создающий рабочую область, и инициализирующий остальные объекты.
        /// </summary>
        public ChildForm(MainForm MainForm, InitializationData InitializatioFormMain)
        {
            InitializeComponent();

            var UnityContainerInit = new UnityContainer();

            _mainForm = MainForm;
            

            DrawForm.Width = MainForm.ChildWidthSize;
            DrawForm.Height = MainForm.ChildHeightSize;

            _initializatioFormMain = InitializatioFormMain;
            _activFormMain = _initializatioFormMain.ReturnActivClass();

            AutoScroll = true;                             

            DoubleBuffered = true;

            //Характеристика фигуры
            _figureProperties.brushcolor = Color.White;
            _figureProperties.dashstyle = DashStyle.Solid;
            _figureProperties.fill = false;
            _figureProperties.linecolor = Color.Black;
            //_figureProperties.thickness = 1;

            //Характеристика опорных точек
            _figurePropertiesSupport.linecolor = Color.Black;

        }

        /// <summary>
        /// Метод, обновляющий рабочую область и выполняющий отрисовку фигур.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая действия над областью рисованияы</para>
        private void Child_Paint(object sender, PaintEventArgs e)
        {
            RefreshBitmap();

            _activFormMain.Child_Paint(e, _currentfigure, _figureProperties.linecolor, _figureProperties.thickness, _figureProperties.dashstyle, _figurePropertiesSupport.linecolor);

         
        }

        /// <summary>
        /// Метод, выполняемый при перемещении мыши по рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            //_drawClass.MouseMoveActions();
            _activFormMain.Child_MouseMove(e, _currentfigure, _currentActions);

            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняемый при нажатии мыши на рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        private void Child__MouseUp(object sender, MouseEventArgs e)    // Нажата клавиша 
        {
            _activFormMain.Child_MouseUp(e, _currentfigure, _currentActions, _figureProperties.linecolor, _figureProperties.thickness, _figureProperties.dashstyle, _figureProperties.brushcolor, _figureProperties.fill);
            _fileSave = true;
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняемый при отпускании кнопки мыши на рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        private void Child1_MouseDown(object sender, MouseEventArgs e)  
        {
            _activFormMain.Child1_MouseDown(e, _currentfigure, _currentActions);
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий обновление рабочей области.
        /// </summary>
        public  void RefreshBitmap()
        {
            _activFormMain.RefreshBitmap();
           
        }

        /// <summary>
        /// Метод, выполняющий удаление фигур.
        /// </summary>
        public void DeleteFigure()
        {
            _activFormMain.DeleteFigure();
            DrawForm.Invalidate();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий выбор новой фигур
        /// </summary>
        /// <para name = "NextFigureType">Переменная, хранящая новую фигуру.</para>
        public void ChangeFigure(int NextFigureType)
        {
            _previousfigure = _currentfigure;             
            _currentfigure = NextFigureType;
        }

        /// <summary>
        /// Метод, выполняющий выбор нового действия над фигурой
        /// </summary>
        /// <para name = "NextActions">Переменная, хранящая новое действие.</para>
        public void ChangeActions(int NextActions)
        {

            _currentActions = NextActions;
           
        }

        /// <summary>
        /// Метод, выполняющий удаление опорных точек.
        /// </summary>
        public void DeleteSupportFigure()
        {
            _activFormMain.DeleteSupportFigure();
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий копирование выделенных фигур.
        /// </summary>
        public void СopyFigure()
        {
            _activFormMain.СopyFigure();
             _fileSave = true;
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий удаление выделенных фигур.
        /// </summary>
        public void DeleteSelectFigure()
        {
            _activFormMain.DeleteSelectFigure();
              DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение заливки у выбранных фигур.
        /// </summary>
        public void СhangeBackgroundFigure(Color ColorBlakgroung)
        {
            _activFormMain.СhangeBackgroundFigure(ColorBlakgroung);
            //ChangeActions(LastActions);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий удаление заливки у выбранных фигур.
        /// </summary>
        public void DeleteBackgroundFigure()
        {
            _activFormMain.DeleteBackgroundFigure();

             DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "ColorPen">Переменная, хранящая новый цвет кисти.</para>
        public void ColorSelectPen(Color ColorPen)
        {
            _activFormMain.ColorSelectPen(ColorPen);
            //ChangeActions(LastActions);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение стиля кисти у выбранных фигур.
        /// </summary>
        public void СhangePenStyleFigure()
        {
            _activFormMain.СhangePenStyleFigure(_figureProperties.dashstyle);
            //ChangeActions(LastActions);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение толщины кисти у выбранных фигур.
        /// </summary>
        public void СhangePenWidthFigure()
        {
            _activFormMain.СhangePenWidthFigure(_figureProperties.thickness);
          
            //ChangeActions(LastActions);
            _fileSave = true;

            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void UndoFigure()
        {
            _activFormMain.UndoFigure();
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void RedoFigure()
        {
            _activFormMain.RedoFigure();
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет опорных точек.</para>
        public void СhangeSupportPenStyleFigure(Color NextColor)
        {
            _activFormMain.СhangeSupportPenStyleFigure(NextColor);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий экспортирование рисунка.
        /// </summary>
        public PictureBox SaveProject()
        {
            //_activFormMain.SaveProject();
            return DrawForm;
        
        }

        /// <summary>
        /// Метод, выполняющий отчистку после экспорта.
        /// </summary>
        public void ClearProject()
        {
            //_activFormMain.ClearProject(DrawForm);
            _fileSave = true;
        }


        /// <summary>
        /// Метод, выполняющий действия над списком комманд.
        /// </summary>
        //public object HistoryCommand
        //{
        //    get { return _drawClass.IFigureCommand; }
        //    set { _drawClass.IFigureCommand = (List<IFigureCommand>)value; }
        //}

        /// <summary>
        /// Метод, выполняющий действия над списком фигур.
        /// </summary>
        //public object HistoryFigure
        //{
        //    get
        //    {
        //        _fileSave = false;
        //        return _drawClass.FiguresList;
        //    }
        //    set
        //    {
        //        _fileSave = true;
        //        _drawClass.FiguresList = (List<ObjectFugure>)value;
        //    }
        //}

        /// <summary>
        /// Метод, выполняющий возвращающий индекс комманды.
        /// </summary>
        public int IndexCommand
        {
            get
            {
                return _activFormMain.IndexCommand;
            }
            set
            {
                _activFormMain.IndexCommand = value;
            }
        }

        /// <summary>
        /// Метод, возвращающий характеристики фигур.
        /// </summary>
        public static Properties FigureProperties
        {
            get { return _figureProperties; }
            set { _figureProperties = value; }
        }

        /// <summary>
        /// Метод, принимающий цвет линии опорных точек.
        /// </summary>
        public static Color ColorSupport
        {
            get { return _figurePropertiesSupport.linecolor; }
            set { _figurePropertiesSupport.linecolor = value; }
        }

        /// <summary>
        /// Метод, принимающий цвет линии.
        /// </summary>
        public static Color ColorLine
        {
            get { return _figureProperties.linecolor; }
            set { _figureProperties.linecolor = value; }
        }

        /// <summary>
        /// Метод, принимающий толщину линии.
        /// </summary>
        public static int Thickness
        {
            get { return _figureProperties.thickness; }
            set { _figureProperties.thickness = value; }
        }

        /// <summary>
        /// Метод, принимающий стиль линии.
        /// </summary>
        public static DashStyle StyleOfLine
        {
            get { return _figureProperties.dashstyle; }
            set { _figureProperties.dashstyle = value; }
        }

        /// <summary>
        /// Метод, возвращающий характеристики опорных точек.
        /// </summary>
        public static PropertiesSupport FigurePropertiesSupport
        {
            get { return _figurePropertiesSupport; }
            set { _figurePropertiesSupport = value; }
        }

        /// <summary>
        /// Метод, возвращающий заливку.
        /// </summary>
        public static bool FillFigure
        {
            get { return _figureProperties.fill; }
            set { _figureProperties.fill = value; }
        }

        /// <summary>
        /// Метод, возвращающий цвет заливки.
        /// </summary>
        public static Color FillColorFigure
        {
            get { return _figureProperties.brushcolor; }
            set { _figureProperties.brushcolor = value; }
        }

        /// <summary>
        /// Метод, возвращающий список выделенных фигур.
        /// </summary>
        public bool SelectFigure()
        {
            return _activFormMain.SelectFigure();
         
        }

       
        /// <summary>
        /// Метод, выполняющий сохранение при закрытии файла.
        /// </summary>
        private void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            String text = "Файл " + this.Text + " был изменен. Сохранить изменения?";   //строка диалога
            String title = "Закрытие файла...";                                         //заголовок диалогового окна
            if (_fileSave) text = "Файл " + this.Text + " не был сохранен ранее. Сохранить его?"; //меняем строку диалога, если файл был только что создан
            if (_fileSave)                                                         //если в файле были произведены изменения, то...
            {
                switch (MessageBox.Show(text, title, MessageBoxButtons.YesNoCancel))    //открываем диалог и, в зависимости от выбора пользователя...
                {
                    case DialogResult.No:                                 //нажато "Нет"
                       
                        break;
                    case DialogResult.Cancel:                             //нажато "Отмена"
                        e.Cancel = true;                                                
                        break;
                    case DialogResult.Yes:                                //нажато "Да"

                        _mainForm.SaveProject(sender, e);
                        break;
                }
            }


        }

    }
}
