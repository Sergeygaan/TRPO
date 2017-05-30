using ActivForm;
using Microsoft.Practices.Unity;
using MyPaint.GUI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Unity;

namespace MyPaint
{
    /// <summary>
    /// Класс, являющийся гланым в программе.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Переменная, хранящая список фигур.
        /// </summary>
        //public enum FigureType
        //{
        //    Rectangle, Ellipse, Line, PoliLine, Polygon, RectangleSelect
        //}

        /// <summary>
        /// Переменная, хранящая список действий.
        /// </summary>
        //public enum Actions
        //{
        //   Draw, SelectRegion, SelectPoint
        //}

        private int _currentActions = 0;
        /// <summary>
        /// Структура, хранящая текущее действие.
        /// </summary>
        //private Actions _currentActions = Actions.Draw;

        /// <summary>
        /// Структура, хранящая счетчик дочерних окон.
        /// </summary>
        private static int _childCounter = 1;

        /// <summary>
        /// Структура, хранящая хранящая заданную ширину дочернего окна.
        /// </summary>
        private static int _childWidhtSize;

        /// <summary>
        /// Структура, хранящая хранящая заданную высоту дочернего окна.
        /// </summary>
        private static int _childHeightSize;

        /// <summary>
        /// Структура, хранящая хранящая свойства нового файла.
        /// </summary>
        private static bool _createNewFile = false;

        /// <summary>
        /// Структура, хранящая класс для соранения проекта.
        /// </summary>
        //private SaveProect _saveProect;


        InitializationData _initializatioForm;

        /// <summary>
        /// Метод, инициализирующий остальные объекты.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            
        }

        /// <summary>
        /// Метод, выполняющий выбор фигуры.
        /// </summary>
        /// /// <para name = "Next">Переменная, хранящая выбранную фигуру.</para>
        private void ChangeFigure( int Next)
        {
            

            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.ChangeFigure(Next);
            }
            ActiveForm = null;

        }

        /// <summary>
        /// Метод, принимающий или возвращаюий активную форму
        /// </summary>
        public InitializationData InitializatioForm
        {
            get { return _initializatioForm; }
            set { _initializatioForm = value; }
        }

        /// <summary>
        /// Метод, выполняющий выбор действия.
        /// </summary>
        /// /// <para name = "NextActions">Переменная, хранящая выбранное действие.</para>
        private void ChangeActions(int NextActions)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.ChangeActions(NextActions);
            }
            ActiveForm = null;

        }

        /// <summary>
        /// Метод, выполняющий удаление всех нарисованных фигур.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void DeleteProject(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                if (ActiveForm.SelectFigure() == false)
                {
                    ActiveForm.DeleteFigure();
                }
                else
                {
                    ActiveForm.DeleteSelectFigure();
                }
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий выбор действия "Рисование".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void Painting(object sender, EventArgs e)
        {
            _currentActions = 0;
            ChangeActions(0);
            ChangeFigure(0);

            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.DeleteSupportFigure();
            }

            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий выбор действия "Выделение".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void RegionSelect(object sender, EventArgs e)
        {
            _currentActions = 1;
            ChangeActions(1);

            ChangeFigure(5);
        }

        /// <summary>
        /// Метод, выполняющий выбор фигуры "Эллипс".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ConstructEllipse(object sender, EventArgs e)
        {
            if (_currentActions == 0)
            {
                ChangeFigure(1);
            }
        }

        /// <summary>
        /// Метод, выполняющий выбор фигуры "Квадрат".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ConstructRectangle(object sender, EventArgs e)
        {
            if (_currentActions == 0)
            {
                ChangeFigure(0);
            }
        }

        /// <summary>
        /// Метод, выполняющий выбор фигуры "Полилиния".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ConstructPoliline(object sender, EventArgs e)
        {
            if (_currentActions == 0)
            {
                ChangeFigure(3);
            }
        }

        /// <summary>
        /// Метод, выполняющий выбор фигуры "Линия".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ConstructLine(object sender, EventArgs e)
        {
            if(_currentActions == 0)
            {
                ChangeFigure(2);
            }
        }

        /// <summary>
        /// Метод, выполняющий выбор фигуры "Многоугольник".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ConstructRegion(object sender, EventArgs e)
        {
            if (_currentActions == 0)
            {
                ChangeFigure(4);
            }
        }

        /// <summary>
        /// Метод, выполняющий действия "Копирование".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void СopyingFigure(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.СopyFigure();
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Изменение цвета линии".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ColorFigure(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                DialogResult D = colorDialog1.ShowDialog();
                if (D == DialogResult.OK)
                {
                    if (ActiveForm.SelectFigure() == false)
                    {
                        ChildForm.ColorLine = colorDialog1.Color;
                    }
                    else
                    {
                        ActiveForm.ColorSelectPen(colorDialog1.Color);
                    }
                }
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Изменение цвета линии опорных точек".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ColorSupportPoint(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                DialogResult D = colorDialog1.ShowDialog();
                if (D == DialogResult.OK)
                {
                    ChildForm.ColorSupport = colorDialog1.Color;
                    ActiveForm.СhangeSupportPenStyleFigure(ChildForm.ColorSupport);
                }
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Изменение толщины линии".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void FigureThickness(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                if (ActiveForm.SelectFigure() == false)
                {
                    LineThickness linethicknessform = new LineThickness();  //создаем форму "Толщина линии"
                    linethicknessform.Text = "Толщина линии фигуры";               //озаглавливаем форму
                    linethicknessform.ShowDialog();                         //отображаем форму
                    linethicknessform.Dispose();                            //уничтожаем форму
                }
                else
                {
                    int StaticThickness = ChildForm.Thickness;
                    LineThickness linethicknessform = new LineThickness();
                    linethicknessform.Text = "Толщина линии фигуры";
                    linethicknessform.ShowDialog();
                    linethicknessform.Dispose();

                    ActiveForm.СhangePenWidthFigure();

                    ActiveForm = null;

                    ChildForm.Thickness = StaticThickness;
                }
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Изменение стиля линии".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void FigureStyle(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                if (ActiveForm.SelectFigure() == false)
                {
                    LineStyle linestyleform = new LineStyle();  //создаем форму "Стиль линии"
                    linestyleform.Text = "Стиль линии";         //озаглавливаем форму
                    linestyleform.ShowDialog();                 //отображаем форму
                    linestyleform.Dispose();                    //уничтожаем форму
                }
                else
                {
                    DashStyle StaticThickness = ChildForm.StyleOfLine;

                    LineStyle linestyleform = new LineStyle();  //создаем форму "Стиль линии"
                    linestyleform.Text = "Стиль линии";         //озаглавливаем форму
                    linestyleform.ShowDialog();                 //отображаем форму
                    linestyleform.Dispose();                    //уничтожаем форму

                    ActiveForm.СhangePenStyleFigure();

                    ChildForm.StyleOfLine = StaticThickness;
                }
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Включить заливку".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void IncludingFills(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ChildForm.FillFigure = true;
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Отключить заливку".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void OffFill(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ChildForm.FillFigure = false;
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Выбор цвета заливки".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ColorFill(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;

            if (ActiveForm != null)
            {
                DialogResult D = colorDialog1.ShowDialog();
                if (D == DialogResult.OK)
                {
                    if (ActiveForm.SelectFigure() == false)
                    {
                        ChildForm.FillColorFigure = colorDialog1.Color;
                    }
                    else
                    {
                        ActiveForm.СhangeBackgroundFigure(colorDialog1.Color);
                    }
                }
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действия "Удаление заливки".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void DeleteFillColor(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.DeleteBackgroundFigure();
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий выбор действия "Выделеник точкой".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void PointSelect(object sender, EventArgs e)
        {
            _currentActions = 2;
            ChangeActions(2);
        }

        /// <summary>
        /// Метод, выполняющий действия над новым файлом.
        /// </summary>
        public static bool CreateNewFile
        {
            get { return _createNewFile; }
            set { _createNewFile = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над шириной дочернео окна.
        /// </summary>
        public static int ChildWidthSize
        {
            get { return _childWidhtSize; }
            set { _childWidhtSize = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия над высотой дочернео окна.
        /// </summary>
        public static int ChildHeightSize
        {
            get { return _childHeightSize; }
            set { _childHeightSize = value; }
        }

        /// <summary>
        /// Метод, выполняющий действия номером дочернего окна.
        /// </summary>
        public static int ChildCounter
        {
            get { return _childCounter; }
            set { _childCounter = value; }
        }

        /// <summary>
        /// Метод, выполняющий действие "Отменить".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void Undo(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.UndoFigure();
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий действие "Повторить".
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void Redo(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.RedoFigure();
            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, создающий новый проект.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void NewProject(object sender, EventArgs e)
        {
            Form FileDialog = new NewFileDialog();                      //создаем форму диалогового окна
            FileDialog.Text = "Новый файл";                             //задаем заголовок окна
            FileDialog.ShowDialog();                                    //отображаем диалог

            if (_createNewFile)                                         //если в диалоговой форме было нажато ОК, то создаем новый файл
            {
                var UnityContainerInit = new UnityContainer();

                //Activ _activFormMain = UnityContainerInit.Resolve<Activ>(new OrderedParametersOverride(new object[] { Width, Height }));

                //InitializationData InitializatioFormMain = UnityContainerInit.Resolve<InitializationData>(new OrderedParametersOverride(new object[] { Width, Height }));

                Form NewForm = new ChildForm(this, _initializatioForm);                         //создаем объект - дочернюю форму-рисунок
             
                NewForm.Text = "Рисунок" + ChildCounter.ToString();     //называем ее соответствующе
                ChildCounter++;                                         //увеличиваем счетчик окон на единицу
                NewForm.MdiParent = this;                               //указываем родительскую форму
                NewForm.BackColor = Color.Gray;                         //цвет фона формы - серый
                NewForm.Width = _childWidhtSize;                         //задаем значение ширины окна, хранящееся в переменной
                NewForm.Height = _childHeightSize;                       //задаем значение высоты окна, хранящееся в переменной
                
                NewForm.Show();                                         //отображаем созданную форму
            }
        }

        /// <summary>
        /// Метод, отображающий историю изменения дочернего окна.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void HistoryProject(object sender, EventArgs e)
        {
            HistoryDesign HistiryForm = new HistoryDesign();
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                
                HistiryForm.Text = "История построения";
                //HistiryForm.ListBox(ActiveForm.HistoryCommand, ActiveForm.IndexCommand, ActiveForm);

                HistiryForm.ShowDialog();                
                
            }
            ActiveForm = null;

            HistiryForm.Dispose();                    
        }

        /// <summary>
        /// Метод, выполняющий сохранение проекта.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        public void SaveProject(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "dat files (*.dat)|*.dat";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                        //_saveProect = new SaveProect(ActiveForm.HistoryFigure, _childWidhtSize, _childHeightSize);

                        BinaryFormatter binFormat = new BinaryFormatter();
                        // Сохранить объект в локальном файле.
                        using (FileStream fStream = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate))
                        {
                            //binFormat.Serialize(fStream, _saveProect);
                        }
                        
                    }
                }

            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий экспорт проекта.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void ExportProject(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
              
                if (ActiveForm.SaveProject().Image != null) //если в pictureBox есть изображение
                {
                    
                    //создание диалогового окна "Сохранить как..", для сохранения изображения
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.Title = "Сохранить картинку как...";
                    //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                    savedialog.OverwritePrompt = true;
                    //отображать ли предупреждение, если пользователь указывает несуществующий путь
                    savedialog.CheckPathExists = true;
                    //список форматов файла, отображаемый в поле "Тип файла"
                    savedialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                    //отображается ли кнопка "Справка" в диалоговом окне
                    savedialog.ShowHelp = true;
                    if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                    {
                        try
                        {
                            ActiveForm.SaveProject().Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    ActiveForm.ClearProject();
                }



            }
            ActiveForm = null;
        }

        /// <summary>
        /// Метод, выполняющий загрузку проекта.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void LoadProject(object sender, EventArgs e)
        {

        Stream myStream = null;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        openFileDialog1.Filter = "dat files (*.dat)|*.dat";
        openFileDialog1.FilterIndex = 2;
        openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            myStream.Close();

                            BinaryFormatter formatter = new BinaryFormatter();
                            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate))
                            {

                                Form NewForm = new ChildForm(this, _initializatioForm);                       //создаем объект - дочернюю форму-рисунок
                                NewForm.Text = "Рисунок" + ChildCounter.ToString();     //называем ее соответствующе

                               
                                ChildCounter++;                                         //увеличиваем счетчик окон на единицу
                                NewForm.MdiParent = this;                               //указываем родительскую форму

                                NewForm.Width = _childWidhtSize;                         //задаем значение ширины окна, хранящееся в переменной
                                NewForm.Height = _childHeightSize;                       //задаем значение высоты окна, хранящееся в переменной

                                NewForm.Show();                                         //отображаем созданную форму

                                ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
                                if (ActiveForm != null)
                                {
                                    //ActiveForm.HistoryFigure = LoadProject.LoadProject();                            
                                }
                                ActiveForm = null;

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
           }
        }

    }
}
