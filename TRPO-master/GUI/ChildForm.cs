using MyPaint.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MyPaint.Core;
using MyPaint.Build;
using MyPaint.Command;
using MyPaint.ObjectType;
using Core;

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
        /// Переменная, хранящая класс с действиями над фигурами.
        /// </summary>
        private EditObject _edipParametr = new EditObject();

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
        /// Переменная, хранящая класс для отрисовки и сохранения фигур.
        /// </summary>
        private DrawPaint _drawClass;

        /// <summary>
        /// Переменная, хранящая класс для выделения.
        /// </summary>
        private SelectDraw _selectClass;

        /// <summary>
        /// Переменная, хранящая список точек для построения фигур.
        /// </summary>
        private List<PointF> _points = new List<PointF>();

        /// <summary>
        /// Переменная, хранящая список классов для построения различных фигур.
        /// </summary>
        private List<IFigureBuild> _figuresBuild = new List<IFigureBuild>();

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


        /// <summary>
        /// Переменная, хранящая список действий для построения различных фигур.
        /// </summary>
        private List<IActoins> _actionsBuild = new List<IActoins>();

        private UndoRedo _commandClass = new UndoRedo();

        private ParameterChanges _parameterChangesClass;

        /// <summary>
        /// Метод, создающий рабочую область, и инициализирующий остальные объекты.
        /// </summary>
        public ChildForm(MainForm MainForm)
        {
            InitializeComponent();

            _mainForm = MainForm;
            

            DrawForm.Width = MainForm.ChildWidthSize;
            DrawForm.Height = MainForm.ChildHeightSize;

            AutoScroll = true;                             //разрешаем скроллинг

            DoubleBuffered = true;

            //Инициализация классов
            _drawClass = new DrawPaint(DrawForm.Width, DrawForm.Height, _commandClass);

            _selectClass = new SelectDraw();

            _parameterChangesClass = new ParameterChanges(_drawClass, _commandClass);

            //Характеристика фигуры
            _figureProperties.brushcolor = Color.White;
            _figureProperties.dashstyle = System.Drawing.Drawing2D.DashStyle.Solid;
            _figureProperties.fill = false;
            _figureProperties.linecolor = Color.Black;
            //_figureProperties.thickness = 1;

            //Характеристика опорных точек
            _figurePropertiesSupport.linecolor = Color.Black;
            
            _figuresBuild.Add(new Rectangles(_edipParametr));
            _figuresBuild.Add(new Ellipses(_edipParametr));
            _figuresBuild.Add(new Line(_edipParametr));
            _figuresBuild.Add(new PoliLine(_edipParametr));
            _figuresBuild.Add(new Polygon(_edipParametr));
            _figuresBuild.Add(new RectangleSelect());


            _actionsBuild.Add(new DrawActoins(_figuresBuild, _selectClass, _drawClass));
            _actionsBuild.Add(new SelectRegionActions(_figuresBuild, _selectClass, _drawClass, _parameterChangesClass));
            _actionsBuild.Add(new SelectPointActions(_figuresBuild, _selectClass, _drawClass, _parameterChangesClass));

        }

        /// <summary>
        /// Метод, обновляющий рабочую область и выполняющий отрисовку фигур.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая действия над областью рисованияы</para>
        private void Child_Paint(object sender, PaintEventArgs e)
        {
            RefreshBitmap();

            _drawClass.Paint(e, _currentfigure, _points, _figuresBuild, _figureProperties.linecolor, _figureProperties.thickness, _figureProperties.dashstyle);

            if (_selectClass.SeleckResult() != null)
            {
                _drawClass.SupportPoint(_selectClass.SeleckResult(), _figuresBuild, _figurePropertiesSupport.linecolor);
            }
        }

        /// <summary>
        /// Метод, выполняемый при перемещении мыши по рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            //_drawClass.MouseMoveActions();
            _points = _actionsBuild[_currentActions].MouseMove(sender, e, _currentfigure, _currentActions);

            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняемый при нажатии мыши на рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        private void Child__MouseUp(object sender, MouseEventArgs e)    // Нажата клавиша 
        {
            _actionsBuild[_currentActions].MouseUp(sender, e, _currentfigure, _figureProperties.linecolor, _figureProperties.thickness, _figureProperties.dashstyle, _figureProperties.brushcolor, _figureProperties.fill);
            _fileSave = true;
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняемый при отпускании кнопки мыши на рабочей области
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая координаты мыщи</para>
        private void Child1_MouseDown(object sender, MouseEventArgs e)  // Нажата отпущена 
        {
            _actionsBuild[_currentActions].MouseDown(sender, e, _currentfigure);
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий обновление рабочей области.
        /// </summary>
        void RefreshBitmap()
        {
            _drawClass.RefreshBitmap();
        }

        /// <summary>
        /// Метод, выполняющий удаление фигур.
        /// </summary>
        public void DeleteFigure()
        {
            if (_points.Count != 0)
            {
                _points.Clear();
            }
            _selectClass.MouseUp();
            _parameterChangesClass.Clear();
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
            _selectClass.MouseUp();
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий копирование выделенных фигур.
        /// </summary>
        public void СopyFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _parameterChangesClass.ReplicationFigure(_selectClass.SeleckResult());
            }
            _fileSave = true;
            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий удаление выделенных фигур.
        /// </summary>
        public void DeleteSelectFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _parameterChangesClass.DeleteFigure(_selectClass.SeleckResult());
            }
            //ChangeActions(LastActions);
            _selectClass.MouseUp();
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение заливки у выбранных фигур.
        /// </summary>
        public void СhangeBackgroundFigure(Color ColorBlakgroung)
        {
            _parameterChangesClass.СhangeBackgroundFigure(_selectClass.SeleckResult(), ColorBlakgroung);
            //ChangeActions(LastActions);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий удаление заливки у выбранных фигур.
        /// </summary>
        public void DeleteBackgroundFigure()
        {
            if (_selectClass.SeleckResult() != null)
            {
                _parameterChangesClass.DeleteBackgroundFigure(_selectClass.SeleckResult());
            }

            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у выбранных фигур.
        /// </summary>
        /// <para name = "ColorPen">Переменная, хранящая новый цвет кисти.</para>
        public void ColorSelectPen(Color ColorPen)
        {
            _parameterChangesClass.СhangePenColorFigure(_selectClass.SeleckResult(), ColorPen);
            //ChangeActions(LastActions);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение стиля кисти у выбранных фигур.
        /// </summary>
        public void СhangePenStyleFigure()
        {
            _parameterChangesClass.СhangePenStyleFigure(_selectClass.SeleckResult(), _figureProperties.dashstyle);
            //ChangeActions(LastActions);
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение толщины кисти у выбранных фигур.
        /// </summary>
        public void СhangePenWidthFigure()
        {
            _parameterChangesClass.СhangePenWidthFigure(_selectClass.SeleckResult(), _figureProperties.thickness);
            //ChangeActions(LastActions);
            _fileSave = true;

            int deltaX = 0;
            int deltaY = 0;

            foreach (ObjectFugure SelectObject in _selectClass.SeleckResult())
            {
                _edipParametr.MoveObjectSupport(SelectObject, deltaX, deltaY);
            }

            DrawForm.Refresh();
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        public void UndoFigure()
        {
            if ((_points != null) && (_points.Count != 0))
            {
                _points.Clear();
            }

            _selectClass.MouseUp();
            _drawClass.UndoFigure();
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        public void RedoFigure()
        {
            _drawClass.RedoFigure();
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий изменение цвета кисти у опорных точек.
        /// </summary>
        /// <para name = "NextColor">Переменная, хранящая новый цвет опорных точек.</para>
        public void СhangeSupportPenStyleFigure(Color NextColor)
        {
            _parameterChangesClass.СhangeSupportPenStyleFigure(NextColor, _selectClass.SeleckResult());
            DrawForm.Refresh();
            _fileSave = true;
        }

        /// <summary>
        /// Метод, выполняющий экспортирование рисунка.
        /// </summary>
        public PictureBox SaveProject()
        {
            _drawClass.SaveProject(DrawForm);
            return DrawForm;
        
        }

        /// <summary>
        /// Метод, выполняющий отчистку после экспорта.
        /// </summary>
        public void ClearProject()
        {
            _drawClass.ClearProject(DrawForm);
            _fileSave = true;
        }


        /// <summary>
        /// Метод, выполняющий действия над списком комманд.
        /// </summary>
        public object HistoryCommand
        {
            get { return _drawClass.IFigureCommand; }
            set { _drawClass.IFigureCommand = (List<IFigureCommand>)value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над списком фигур.
        /// </summary>
        public object HistoryFigure
        {
            get
            {
                _fileSave = false;
                return _drawClass.FiguresList;
            }
            set
            {
                _fileSave = true;
                _drawClass.FiguresList = (List<ObjectFugure>)value;
            }
        }

        /// <summary>
        /// Метод, выполняющий возвращающий индекс комманды.
        /// </summary>
        public int IndexCommand
        {
            get
            {
                if ((_points != null) && (_points.Count != 0))
                {
                    _points.Clear();
                }
                _selectClass.MouseUp();
                DrawForm.Refresh();
                return _drawClass.IndexCommand;
            }
            set
            {
                DrawForm.Refresh();
                _drawClass.IndexCommand = value;
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
            bool ListFigure = false;
            if (_selectClass.SeleckResult().Count != 0)
            {
                ListFigure = true;
            }

            return ListFigure;
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
