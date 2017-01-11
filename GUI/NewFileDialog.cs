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
