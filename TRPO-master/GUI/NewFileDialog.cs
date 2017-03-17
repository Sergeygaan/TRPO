using System;
using System.Windows.Forms;

namespace MyPaint
{
    /// <summary>
    /// Класс, выполняющий создание новой рабочей области.
    /// </summary>
    public partial class NewFileDialog : Form
    {
        /// <summary>
        /// Переменная, хранащая значение о диапазоне цифр.
        /// </summary>
        private bool inputerror = false;

        /// <summary>
        /// Переменная, хранащая значение о вводе цифр.
        /// </summary>
        private bool notdigit = false;

        /// <summary>
        /// Переменная, хранащая значение о заполении поля.
        /// </summary>
        private bool emptyfield = false;

        /// <summary>
        /// Метод, выполняющий инициализирующий остальные объекты.
        /// </summary>
        public NewFileDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, выполняющий создание новой рабочей области.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void OKbutton_Click(object sender, EventArgs e)
        {
            if (UserSize.Checked)
            {
                /*если хотя бы один из флагов после проверки оказался в true,
                 *то мы не передаем значения ширины и высоты окна, а выдаем ошибку
                 */
                if (notdigit || emptyfield || inputerror) MessageBox.Show("Проверьте правильность и достаточность введенных данных", "Введены неверные или недостаточные данные!");
                else
                {
                    MainForm.ChildWidthSize = Convert.ToInt32(numericUpDown1.Text);
                    MainForm.ChildHeightSize = Convert.ToInt32(numericUpDown2.Text);
                    this.Close();
                }
            }
            else
            {
                /*в зависимости от выбранной кнопки
                 * передаем в переменные соответствующие 
                 * размеры
                 */
                if (Size1.Checked)
                {
                    MainForm.ChildWidthSize = 320;
                    MainForm.ChildHeightSize = 240;
                }
                else if (Size2.Checked)
                {
                    MainForm.ChildWidthSize = 640;
                    MainForm.ChildHeightSize = 480;
                }
                else
                {
                    MainForm.ChildWidthSize = 800;
                    MainForm.ChildHeightSize = 600;
                }
                this.Close();
            }
            MainForm.CreateNewFile = true;
        }

        /// <summary>
        /// Метод, выполняющий отмену создания новой рабочей области.
        /// </summary>
        /// <para name = "sender">Переменная, хранящая объект.</para>
        /// <para name = "e">Переменная, хранящая список событий.</para>
        private void CANCELbutton_Click(object sender, EventArgs e)
        {
            /*так как мы отказываемся от создания файла,
             * переводим флаг создания нового файла в false
             * и закрываем диалог
             */
            MainForm.CreateNewFile = false;
            this.Close();
        }

    }
}
