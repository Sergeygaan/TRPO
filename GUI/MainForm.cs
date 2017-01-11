using PaintedObjectsMoving.CORE;
using PaintedObjectsMoving.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PaintedObjectsMoving
{
    public partial class MainForm : Form
    {
        public enum FigureType
        {
            Rectangle, Ellipse, Line, PoliLine, Polygon, RectangleSelect
        }

        public enum Actions
        {
           Draw, Move, Scale, SelectRegion, SelectPoint
        }
        public struct Properties
        {
            public Color linecolor;  //цвет линии
            public Color brushcolor; //цвет заливки
            public int thickness;              //толщина линии
            /* стиль линии*/
            public System.Drawing.Drawing2D.DashStyle dashstyle;
            public bool fill; //true - фигура с заливкой, false - без заливки
        }

        public struct PropertiesSupport
        {
            public Color linecolor;  //цвет линии
        }

        //ПЕРЕМЕННЫЕ
        private List<PointF> _points = new List<PointF>();

        private Actions _currentActions = Actions.Draw;
        private static Properties _figureProperties;                        //свойства фигуры
        private static PropertiesSupport _figurePropertiesSupport;          //свойства фигуры

        private static int childcounter = 1;                                            //счетчик дочерних окон для выдачи надписи РИСУНОК1, РИСУНОК2...
        private static int childwidhtsize = 800;                                        //переменная, хранящая заданную ширину дочернего окна
        private static int childheightsize = 600;                                       //переменная, хранящая заданную высоту дочернего окна

        //ФЛАГИ
        private static bool createnewfile = false;                                      //true - создать файл

        private SaveProect _saveProect;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            //Характеристика фигуры
            _figureProperties.brushcolor = Color.White;
            _figureProperties.dashstyle = System.Drawing.Drawing2D.DashStyle.Solid;
            _figureProperties.fill = false;
            _figureProperties.linecolor = Color.Black;
            _figureProperties.thickness = 1;

            //Характеристика опорных точек
            _figurePropertiesSupport.linecolor = Color.Black;

        }

        //Характеристики обычных фигур
        public static Properties FigureProperties
        {
            get { return _figureProperties; }
        }
        public static int Thickness
        {
            set { _figureProperties.thickness = value; }
        }
        public static System.Drawing.Drawing2D.DashStyle StyleOfLine
        {
            set { _figureProperties.dashstyle = value; }
        }

        //Характеристики опорных точек
        public static PropertiesSupport FigurePropertiesSupport
        {
            get { return _figurePropertiesSupport; }
        }

        //Выбор фигуры
        private void ChangeFigure(FigureType next)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.ChangeFigure(next);
            }
            ActiveForm = null;

        }


        private void ChangeActions(Actions next)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.ChangeActions(next);
            }
            ActiveForm = null;

        }

        //Удаление всех нарисованных фигур
        private void отчиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.DeleteFigure();
            }
            ActiveForm = null;
        }

        //Режим рисования
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Draw;
            ChangeActions(Actions.Draw);
            ChangeFigure(FigureType.Line);

            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.DeleteSupportFigure();
            }

            ActiveForm = null;
        }

        //Режим выделения
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.SelectRegion;
            ChangeActions(Actions.SelectRegion);

            ChangeFigure(FigureType.RectangleSelect);
        }
        //Эллипс
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Ellipse);
            }
        }

        //Квадрат
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Rectangle);
            }
        }
        
        //Полилиния
        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.PoliLine);
            }
        }

        //Линия
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if(_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Line);
            }
        }
        // Многоугольник
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (_currentActions == Actions.Draw)
            {
                ChangeFigure(FigureType.Polygon);
            }
        }
        //Перемещение
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Move;
            ChangeActions(Actions.Move);
        }
        //Масштабирование
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.Scale;
            ChangeActions(Actions.Scale);
        }

        //Копирование
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.СopyFigure();
            }
            ActiveForm = null;
        }
        //Удаление
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.DeleteSelectFigure();
            }
            ActiveForm = null;
        }

        // Цвет отрисовки фигур
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _figureProperties.linecolor = colorDialog1.Color; 
            }
        }

        //Цвет отрисовки опорных точек
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _figurePropertiesSupport.linecolor = colorDialog1.Color;

                ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
                if (ActiveForm != null)
                {
                    ActiveForm.СhangeSupportPenStyleFigure(_figurePropertiesSupport.linecolor);
                }
                ActiveForm = null;
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            LineThickness linethicknessform = new LineThickness();  //создаем форму "Толщина линии"
            linethicknessform.Text = "Толщина линии фигуры";               //озаглавливаем форму
            linethicknessform.ShowDialog();                         //отображаем форму
            linethicknessform.Dispose();                            //уничтожаем форму
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            LineStyle linestyleform = new LineStyle();  //создаем форму "Стиль линии"
            linestyleform.Text = "Стиль линии";         //озаглавливаем форму
            linestyleform.ShowDialog();                 //отображаем форму
            linestyleform.Dispose();                    //уничтожаем форму
        }

        //Включить заливку
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _figureProperties.fill = true; 
        }

        //Отключить заливку
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            _figureProperties.fill = false;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                _figureProperties.brushcolor = colorDialog1.Color;
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
                if (ActiveForm != null)
                {
                    ActiveForm.СhangeBackgroundFigure(colorDialog1.Color);
                }
                ActiveForm = null;
            }
          
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.DeleteBackgroundFigure();
            }
            ActiveForm = null;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
                if (ActiveForm != null)
                {
                    ActiveForm.ColorSelectPen(colorDialog1.Color);
                }
                ActiveForm = null;
            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {

            int StaticThickness = _figureProperties.thickness;
            LineThickness linethicknessform = new LineThickness();          //создаем форму "Толщина линии"
            linethicknessform.Text = "Толщина линии фигуры";                //озаглавливаем форму
            linethicknessform.ShowDialog();                                 //отображаем форму
            linethicknessform.Dispose();                                    //уничтожаем форму

            int CurrentThickness = FigureProperties.thickness;

            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.СhangePenWidthFigure();
            }
            ActiveForm = null;

            _figureProperties.thickness = StaticThickness;
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {

            DashStyle StaticThickness = _figureProperties.dashstyle;

            LineStyle linestyleform = new LineStyle();  //создаем форму "Стиль линии"
            linestyleform.Text = "Стиль линии";         //озаглавливаем форму
            linestyleform.ShowDialog();                 //отображаем форму
            linestyleform.Dispose();                    //уничтожаем форму

            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.СhangePenStyleFigure();
            }
            ActiveForm = null;

            _figureProperties.dashstyle = StaticThickness;
        }


        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            _currentActions = Actions.SelectPoint;
            ChangeActions(Actions.SelectPoint);
        }

        public static bool CreateNewFile
        {
            get { return createnewfile; }
            set { createnewfile = value; }
        }
        public static int ChildWidthSize
        {
            get { return childwidhtsize; }
            set { childwidhtsize = value; }
        }
        public static int ChildHeightSize
        {
            get { return childheightsize; }
            set { childheightsize = value; }
        }
        public static int ChildCounter
        {
            get { return childcounter; }
            set { childcounter = value; }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.UndoFigure();
            }
            ActiveForm = null;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                ActiveForm.RedoFigure();
            }
            ActiveForm = null;
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FileDialog = new NewFileDialog();                      //создаем форму диалогового окна
            FileDialog.Text = "Новый файл";                             //задаем заголовок окна
            FileDialog.ShowDialog();                                    //отображаем диалог

            if (createnewfile)  //если в диалоговой форме было нажато ОК, то создаем новый файл
            {
                Form NewForm = new ChildForm();                       //создаем объект - дочернюю форму-рисунок
             
                NewForm.Text = "Рисунок" + ChildCounter.ToString();     //называем ее соответствующе
                ChildCounter++;                                         //увеличиваем счетчик окон на единицу
                NewForm.MdiParent = this;                               //указываем родительскую форму
                NewForm.BackColor = Color.Gray;                         //цвет фона формы - серый
                NewForm.Width = childwidhtsize;                         //задаем значение ширины окна, хранящееся в переменной
                NewForm.Height = childheightsize;                       //задаем значение высоты окна, хранящееся в переменной
               
                NewForm.Show();                                         //отображаем созданную форму
            }
        }

        //Отображение древа проекта
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            HistoryDesign HistiryForm = new HistoryDesign();
            ChildForm ActiveForm = (ChildForm)this.ActiveMdiChild;
            if (ActiveForm != null)
            {
                HistiryForm.Text = "История построения";         //озаглавливаем форму
                HistiryForm.ListBox(ActiveForm.HistoryCommand);

                HistiryForm.ShowDialog();                 //отображаем форму

            }
            ActiveForm = null;

            HistiryForm.Dispose();                    //уничтожаем форму
        }

        //Сохранение проекта
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
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
                        _saveProect = new SaveProect();
                        _saveProect.Save(ActiveForm.HistoryCommand, ActiveForm.HistoryFigure);

                        BinaryFormatter binFormat = new BinaryFormatter();
                        // Сохранить объект в локальном файле.
                        using (FileStream fStream = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate))
                        {
                            binFormat.Serialize(fStream, _saveProect);
                        }
                    }
                }

            }
            ActiveForm = null;
        }

        private void экспортироватьПроектToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
