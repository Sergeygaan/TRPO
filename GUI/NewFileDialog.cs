using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintedObjectsMoving
{
    public partial class NewFileDialog : Form
    {
        //КОНСТАНТЫ
        const string outofrangetitle = "Вне установленного диапазона!";
        const string outofnumberrange = "Число должно находиться в диапазоне от 10 до 2000.";
        const string notdigittitle = "Введенный символ не является числом!";
        const string notdigitmessage = "Введенные сиволы должны быть цифрами.";

        //ФЛАГИ
        /*                  true                                false
         * inputerror       введена цифра вне диапазона         ввод числа осуществлен правильно
         * notdigit         введена НЕ цифра                    все введенные символы - цифры
         * emptyfield       одно или несколько полей пусты      все необходимые поля заполнены
         */
        private bool inputerror = false;
        private bool notdigit = false;
        private bool emptyfield = false;

        public NewFileDialog()
        {
            InitializeComponent();
        }

        private void NewFileDialog_Load(object sender, EventArgs e)
        {

        }

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
                    MainForm.ChildWidthSize = Convert.ToInt32(UserWidth.Text);
                    MainForm.ChildHeightSize = Convert.ToInt32(UserHeight.Text);
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

        private void CANCELbutton_Click(object sender, EventArgs e)
        {
            /*так как мы отказываемся от создания файла,
             * переводим флаг создания нового файла в false
             * и закрываем диалог
             */
            MainForm.CreateNewFile = false;
            this.Close();
        }

        private void UserSize_CheckedChanged(object sender, EventArgs e)
        {
            /*если поставлена галочка "Выбрать вручную", то
             * нам доступны поля для ручного ввода чисел, если
             * галочка не стоит, то доступны поля выбора заранее
             * установленных размеров
             */
            SizeGroup.Enabled = UserSize.Checked ? false : true;
            UserWidth.Enabled = UserHeight.Enabled = UserSize.Checked ? true : false;
        }

        private void UserWidth_TextChanged(object sender, EventArgs e)
        {
            /*задаем значения параметров объекта, т.е. окна ввода
             */
            UserWidth.CharacterCasing = CharacterCasing.Normal; //регистр символов
            UserWidth.MaxLength = 4;                            //максимальное допустимое число символов
            UserWidth.Multiline = false;                        //многострочность
            UserWidth.CausesValidation = true;                  //проводить проверку вводимых символов
        }

        private void UserHeight_TextChanged(object sender, EventArgs e)
        {
            /*задаем значения параметров объекта, т.е. окна ввода
             */
            UserHeight.CharacterCasing = CharacterCasing.Normal; //регистр символов
            UserHeight.MaxLength = 4;                           //максимальное допустимое число символов
            UserHeight.Multiline = false;                      //многострочность
            UserHeight.CausesValidation = true;               //проводить проверку вводимых символов
        }

        private void UserWidth_Validating(object sender, CancelEventArgs e)
        {
            emptyfield = false;         //в начале проверки флаг всегда в false

            /*UserWidth.Lines получает массив ввденных строк,
             * функция GetLength(ElementNumber) возвращает длину
             * заданной строки (строка является эелементом массива,
             * следовательно, для однострочного поля, она будет первой)
             */
            if (UserWidth.Lines.GetLength(0) != 0)  //если окно не пусто, то запускаем проверку
            {
                notdigit = false;       //в начале проверки флаг всегда в false
                inputerror = false;

                /*проверим, все ли введенные символы
                 * являются цифрами, если находим хотя бы
                 * одну не цифру, то переводим флаг
                 * notdigit в true
                 */
                foreach (char symbol in UserWidth.Text)
                {
                    if (char.IsDigit(symbol)) continue; //если, сивол является цифрой, то переходим к следующему сиволу
                    else            //если символ - не цифра
                    {
                        notdigit = true;    //переводим флаг
                        WidthHelp.ToolTipTitle = notdigittitle;
                        WidthHelp.Show(notdigitmessage, UserWidth);
                        break;              //и прерываем цикл (дальше уже проверять не надо)
                    }
                }
                if (!notdigit)
                {
                    int size = Convert.ToInt32(UserWidth.Text);
                    if (size < 10 || size > 2000)
                    {
                        inputerror = true;
                        WidthHelp.ToolTipTitle = outofrangetitle;
                        WidthHelp.Show(outofnumberrange, UserWidth);
                    }
                }
            }
            else emptyfield = true;
        }

        private void UserHeight_Validating(object sender, CancelEventArgs e)
        {
            emptyfield = false;
            if (UserHeight.Lines.GetLength(0) != 0)
            {
                notdigit = false;
                inputerror = false;
                foreach (char symbol in UserHeight.Text)
                {
                    if (char.IsDigit(symbol)) continue;
                    else
                    {
                        notdigit = true;
                        HeightHelp.ToolTipTitle = notdigittitle;
                        HeightHelp.Show(notdigitmessage, UserHeight);
                        break;
                    }
                }
                if (!notdigit)
                {
                    int size = Convert.ToInt32(UserHeight.Text);
                    if (size < 10 || size > 2000)
                    {
                        inputerror = true;
                        HeightHelp.ToolTipTitle = outofrangetitle;
                        HeightHelp.Show(outofnumberrange, UserHeight);
                    }
                }
            }
            else emptyfield = true;
        }
    }
}
