using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintedObjectsMoving {
	public partial class MainForm : Form
    {
        //ПЕРЕЧИСЛЕНИЕ
        /*зададим перечисление, имеющее
         * в качестве значений названия
         * фигур, которые будут использованы
         * для определения, какую именно рисовать
         */
        public enum FigureType
        {
            Rectangle, Square, Ellipse, Circle, Curve, Line
        }

        List<PaintedObject> list;//Список с объектами для прорисовки
		PaintedObject currObj;//Объект, который в данный момент перемещается
		Point oldPoint;
		Bitmap bmp;

        Ellipse _ellipse;
    
        //ПЕРЕМЕННЫЕ
        private Point figurestart = new Point();                          //стартовая точка фигуры
        private Point figureend = new Point();                            //конечная точка фигуры

        //ФЛАГИ
        private bool mouseclick = false;


        public MainForm()
        {
			InitializeComponent();
			list = new List<PaintedObject>();
			bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
			//Заполнение списка случайными объектами для прорисовки
			Init();
            //Обновление битмапа
           
			DoubleBuffered = true;

            _ellipse = new Ellipse();

        }

		void Form1_Paint(object sender, PaintEventArgs e)
        {
			if (bmp == null) return;
			RefreshBitmap();

            Pen pen = new Pen(Color.Black);
            e.Graphics.DrawLine(pen, figurestart, figureend);

            //e.Graphics.DrawEllipse(pen, _ellipse.Show(figurestart, figureend));

            e.Graphics.DrawImage(bmp, 0, 0);
          
        }

		void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        //Считаем смещение курсора
                        int deltaX, deltaY;
                        deltaX = e.Location.X - oldPoint.X;
                        deltaY = e.Location.Y - oldPoint.Y;
                        //Смещаем нарисованный объект
                        if (currObj != null)
                        {
                            currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));
                            //Запоминаем новое положение курсора
                            oldPoint = e.Location;
                        }
                        break;
                    default:
                        break;
                }
              
            }

            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                figureend = e.Location;
                
            }

            DrawForm.Refresh();
            DrawForm.Invalidate();
        }

		void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (currObj != null)
                {
                    currObj.Pen.Width -= 1;//Возвращаем ширину пера
                    currObj = null;//Убираем ссылку на объект
                }
            }

            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                mouseclick = false;
                figureend = e.Location;

                PaintedObject po;
                po = new PaintedObject(new Pen(Color.FromArgb(80, 123, 121)), new GraphicsPath());
                po.Path.AddLine(figurestart, figureend);
                //po.Path.AddEllipse(_ellipse.Show(figurestart, figureend));
                list.Add(po);
              
            }
            DrawForm.Refresh();
            DrawForm.Invalidate();
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                //Запоминаем положение курсора
                oldPoint = e.Location;
                //Ищем объект, в который попала точка. Если таких несколько, то найден будет первый по списку
                foreach (PaintedObject po in list)
                {
                    if (po.Path.GetBounds().Contains(e.Location))
                    {
                        currObj = po;//Запоминаем найденный объект
                        currObj.Pen.Width += 1;//Делаем перо жирнее
                        return;
                    }
                }
            }
            if (e.Button == MouseButtons.Left)              //если нажата левая кнопка мыши
            {
                mouseclick = true;
                figurestart = e.Location;
            }
             

		}
		void RefreshBitmap() {

			if (bmp != null) bmp.Dispose();
            bmp = new Bitmap(DrawForm.Width, DrawForm.Height);
            //Прорисовка всех объектов из списка
            using (Graphics g = Graphics.FromImage(bmp)) {
				foreach (PaintedObject po in list) {
					g.DrawPath(po.Pen, po.Path);
				}
			}
		}

		void Init() {
			//PaintedObject po;
			//Random rnd = new Random(DateTime.Now.Millisecond);
			//int w = this.ClientSize.Width,
			//	w1 = this.ClientSize.Width / 2,
			//	h = this.ClientSize.Height,
			//	h1 = this.ClientSize.Height / 2;
			//for (int i = 0; i < 3; i++) {
			//	po = new PaintedObject(new Pen(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))), new GraphicsPath());
			//	po.Path.AddEllipse(Rectangle.FromLTRB(rnd.Next(w1), rnd.Next(h1), rnd.Next(w1, w), rnd.Next(h1, h)));
			//	list.Add(po);
			//}
   //         po = new PaintedObject(new Pen(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))), new GraphicsPath());
   //         po.Path.AddLine(10, 20, 300, 100);
   //         list.Add(po);

   //         po = new PaintedObject(new Pen(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))), new GraphicsPath());
   //         Rectangle rectangle = new Rectangle(1, 2, 35, 35);
   //         po.Path.AddRectangle(rectangle);
   //         list.Add(po);
        }
	}
}
