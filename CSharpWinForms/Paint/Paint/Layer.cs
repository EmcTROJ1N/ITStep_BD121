using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Paint
{
    class Layer
    {
		public Bitmap img;              // картинка со слоем
		private float transparency;     // прозрачность слоя

		public float Transparency       // свойство прозрачночти
		{
			get { return transparency; }
			set { transparency = value; }
		}

		public string Name { get; set; }

		public bool Visible { get; set; }

		public Layer(int w, int h, float tr, string name)    // конструктор задаёт размеры слоя и прозрачность
		{
			transparency = tr;
			img = new Bitmap(w, h);
			Name = name;
			Visible = true;
		}
    }
}
