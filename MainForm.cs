using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintedObjectsMoving {
	public partial class MainForm : Form {
		List<PaintedObject> list;//Список с объектами для прорисовки
		PaintedObject currObj;//Объект, который в данный момент перемещается
		Point oldPoint;
		Bitmap bmp;
		public MainForm() {
			InitializeComponent();
			list = new List<PaintedObject>();
			bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			//Заполнение списка случайными объектами для прорисовки
			Init();
			//Обновление битмапа
			RefreshBitmap();
			this.DoubleBuffered = true;
			this.MouseDown += Form1_MouseDown;
			this.MouseUp += Form1_MouseUp;
			this.MouseMove += Form1_MouseMove;
			this.Paint += Form1_Paint;
		}

		void Form1_Paint(object sender, PaintEventArgs e) {
			if (bmp == null) return;
			RefreshBitmap();
			e.Graphics.DrawImage(bmp, 0, 0);
		}

		void Form1_MouseMove(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					//Считаем смещение курсора
					int deltaX, deltaY;
					deltaX = e.Location.X - oldPoint.X;
					deltaY = e.Location.Y - oldPoint.Y;
					//Смещаем нарисованный объект
					currObj.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));
					//Запоминаем новое положение курсора
					oldPoint = e.Location;
					break;
				default:
					break;
			}
			//Обновляем форму
			this.Refresh();
		}

		void Form1_MouseUp(object sender, MouseEventArgs e) {
			currObj.Pen.Width -= 1;//Возвращаем ширину пера
			currObj = null;//Убираем ссылку на объект
		}

		void Form1_MouseDown(object sender, MouseEventArgs e) {
			//Запоминаем положение курсора
			oldPoint = e.Location;
			//Ищем объект, в который попала точка. Если таких несколько, то найден будет первый по списку
			foreach (PaintedObject po in list) {
				if (po.Path.GetBounds().Contains(e.Location)) {
					currObj = po;//Запоминаем найденный объект
					currObj.Pen.Width += 1;//Делаем перо жирнее
					return;
				}
			}
		}
		void RefreshBitmap() {
			if (bmp != null) bmp.Dispose();
			bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			//Прорисовка всех объектов из списка
			using (Graphics g = Graphics.FromImage(bmp)) {
				foreach (PaintedObject po in list) {
					g.DrawPath(po.Pen, po.Path);
				}
			}
		}

		void Init() {
			PaintedObject po;
			Random rnd = new Random(DateTime.Now.Millisecond);
			int w = this.ClientSize.Width,
				w1 = this.ClientSize.Width / 2,
				h = this.ClientSize.Height,
				h1 = this.ClientSize.Height / 2;
			for (int i = 0; i < 3; i++) {
				po = new PaintedObject(new Pen(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))), new GraphicsPath());
				po.Path.AddEllipse(Rectangle.FromLTRB(rnd.Next(w1), rnd.Next(h1), rnd.Next(w1, w), rnd.Next(h1, h)));
				list.Add(po);
			}
		}
	}
}
