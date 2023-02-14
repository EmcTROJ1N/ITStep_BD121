using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace Paint
{
	delegate void LayersChanged(BitmapLayers layers);

	class BitmapLayers
	{
		int width, height;                  // размеры слоёв

		public int Height                   // высота
		{
			get { return height; }
			set { height = value; }
		}

		public int Width                    // ширина
		{
			get { return width; }
			set { width = value; }
		}

		public event LayersChanged OnLayersChanged;

		public int LayerCount { get => layers.Count; }

		public List<Layer> layers = new List<Layer>();      // список слоёв

		PictureBox container = null;

		public BitmapLayers(int w, int h, int count)        // конструктор задаёт размеры и количество слоёв
		{
			width = w;
			height = h;
			for (int i = 0; i < count; i++)
			{
				layers.Add(new Layer(w, h, 1, $"Layer {i}"));             // добавление слоя в список
			}
		}

		public void LayersRefresh()
		{
			OnLayersChanged?.Invoke(this);
		}

		public void Add(float tranparency, string name = "")      // метод добавляет слой в список
		{
			Layer layer = new Layer(Width, Height, tranparency, name.Length == 0 ? $"Layer {layers.Count}" : name);
			layers.Add(layer);

			OnLayersChanged?.Invoke(this);
		}

		// удаление слоя
		public void Remove(int n)                           // удаление слоя по номеру
		{
			if (n < layers.Count) layers.RemoveAt(n);

			OnLayersChanged?.Invoke(this);
		}

		// объединение слоёв
		Bitmap GetResultImage()
		{
			Bitmap res = new Bitmap(width, height);         // создание результирующей картинки
			Graphics resgr = Graphics.FromImage(res);       // graphics для результирующей картинки

			ImageAttributes attr = new ImageAttributes();   // создание атрибутов изображения

			// обеспечение прозрачности слоёв за счёт замены белого цвета на прозрачный
			attr.SetColorKey(Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255)); // белый цвет делаем прозрачным

			// рисование фона на картинке
			if (layers[0].Visible)
				resgr.DrawImage(layers[0].img, new Rectangle(0, 0, 400, 300), 0, 0, 400, 300, GraphicsUnit.Pixel);

			// отображение всех слоёв на результирующей картинке с учётом прозрачности
			for (int k = 1; k < layers.Count; k++)
			{
				if (layers[k].Visible)
				{
					// матрица цветов задаёт прозрачноть для каждого слоя
					ColorMatrix myColorMatrix = new ColorMatrix();
					myColorMatrix.Matrix00 = 1.00f;
					myColorMatrix.Matrix11 = 1.00f;
					myColorMatrix.Matrix22 = 1.00f;
					myColorMatrix.Matrix33 = layers[k].Transparency;

					attr.SetColorMatrix(myColorMatrix); // применение матрицы

					// отображение слоя
					resgr.DrawImage(layers[k].img, new Rectangle(0, 0, 400, 300), 0, 0, 400, 300, GraphicsUnit.Pixel, attr);
				}
			}

			resgr.Dispose();

			return res;
		}

		// обновление картинки на контейнерном PictureBox
		public void Invalidate()                            // показ слоёв в picturebox
		{
			Bitmap bitmap = GetResultImage();

			// выбор результирующей картинки для показа в picturebox
			container.Image = bitmap;
		}

		public void Save(string filename, ImageFormat format)
		{
			Bitmap bitmap = GetResultImage();
			bitmap.Save(filename, format);
		}

		// привязка слоёв к контейнерному picturebox
		public void Attach(PictureBox pictureBox)
		{
			if (pictureBox != null)
				container = pictureBox;
		}

		// покрасить слой по номеру в указанный цвет
		public void Clear(Color color, int layerNumber)
		{
			if (color == null || layerNumber < 0 || layerNumber >= layers.Count)
				return;

			Image layer = layers[layerNumber].img;
			Graphics graphics = Graphics.FromImage(layer);
			graphics.Clear(color);
			graphics.Dispose();
		}

		// нарисовать зарисованный прямоугольник в слое по номеру
		public void FillRectangle(Brush brush, int x, int y, int width, int height, int layerNumber)
		{
			if (brush == null || layerNumber < 0 || layerNumber >= layers.Count)
				return;

			Image layer = layers[layerNumber].img;
			Graphics graphics = Graphics.FromImage(layer);
			graphics.FillRectangle(brush, x, y, width, height);
			graphics.Dispose();
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height, int layerNumber)
		{
			if (pen == null || layerNumber < 0 || layerNumber >= layers.Count)
				return;

			Image layer = layers[layerNumber].img;
			Graphics graphics = Graphics.FromImage(layer);
			graphics.DrawRectangle(pen, x, y, width, height);
			graphics.Dispose();
		}

		public void FillEllipse(Brush brush, int x, int y, int width, int height, int layerNumber)
		{
			if (brush == null || layerNumber < 0 || layerNumber >= layers.Count)
				return;

			Image layer = layers[layerNumber].img;
			Graphics graphics = Graphics.FromImage(layer);
			graphics.FillEllipse(brush, x, y, width, height);
			graphics.Dispose();
		}

		public void DrawEllipse(Pen pen, int x, int y, int width, int height, int layerNumber)
		{
			if (pen == null || layerNumber < 0 || layerNumber >= layers.Count)
				return;

			Image layer = layers[layerNumber].img;
			Graphics graphics = Graphics.FromImage(layer);
			graphics.DrawEllipse(pen, x, y, width, height);
			graphics.Dispose();
		}
	}
}
