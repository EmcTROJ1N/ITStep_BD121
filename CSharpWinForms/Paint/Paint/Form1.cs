using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Paint
{
    public partial class Form1 : Form
    {
        Color _Color;
        Pen _Pen;
        SolidBrush _Brush;
        Point LstPoint;
        bool IsDrawing;
        Point CurrentPos;
        List<KeyValuePair<string,  Point[]>> Points;
        Image _Image;

        public Form1()
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
            _Color = Color.Black;
            _Pen = new Pen(_Color, 20);
            _Brush = new SolidBrush(_Color);
            LstPoint = new Point(-1, -1);
            IsDrawing = false;
            this.Paint += Form1_Paint;
            Points = new List<KeyValuePair<string, Point[]>>();
            
            _Image = Bitmap.FromFile("749448.jpg");
            pictureBox1.Image = _Image;
            pictureBox1.MouseMove += PictureBox_MouseMove;
            pictureBox1.MouseDown += PictureBox_MouseDown;
            pictureBox1.MouseUp += PictureBox_MouseUp;
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            // graphics.DrawImage(_Image, new Rectangle(0, 0, _Image.Width, _Image.Height));

            foreach (var item in Points)
            {
                switch (item.Key)
                    {
                    case "Point":
                        graphics.FillRectangle(_Brush, item.Value[0].X, item.Value[0].Y, 20, 20);
                        break;
                    case "Line":
                        graphics.DrawLine(_Pen, item.Value[0], item.Value[1]);
                        break;
                    case "FillRect":
                        graphics.FillRectangle(_Brush, item.Value[0].X, item.Value[0].Y, 300, 200);
                        break;
                    case "DrawRect":
                        graphics.DrawRectangle(_Pen, item.Value[0].X, item.Value[0].Y, 300, 200);
                        break;
                    case "DrawEllipse":
                        graphics.DrawPie(_Pen, item.Value[0].X, item.Value[0].Y, 100, 100, 0, 360);
                        break;
                    case "FillEllipse":
                        graphics.FillPie(_Brush, item.Value[0].X, item.Value[0].Y, 100, 100, 0, 360);
                        break;
                    case "Triangle":
                        graphics.DrawLines(_Pen, item.Value);
                        break;
                }
            }
            
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e) =>
            IsDrawing = false;

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: IsDrawing = true; break;
                case MouseButtons.Right:
                    if (LstPoint.X == -1)
                        LstPoint = new Point(e.X, e.Y);
                    else
                    {
                        Graphics grIm = Graphics.FromImage(_Image);
                        Point currP = new Point(e.X, e.Y);
                        grIm.DrawLine(_Pen, LstPoint, currP);
                        pictureBox1.Invalidate();
                        Points.Add(new KeyValuePair<string, Point[]>("Line", new Point[] { LstPoint, currP }));
                        LstPoint = new Point(-1, -1);
                        grIm.Dispose();
                    }
                    break;
            }

            if (LstPoint == null) LstPoint = new Point(e.X, e.Y);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentPos = new Point(e.X, e.Y);

            if (IsDrawing == false) return;
            Graphics grIm = Graphics.FromImage(_Image);

            grIm.FillRectangle(_Brush, e.X, e.Y, 20, 20);
            pictureBox1.Invalidate();
            Points.Add(new KeyValuePair<string, Point[]>("Point", new Point[] { new Point(e.X, e.Y) }));

            grIm.Dispose();
        }

        private void DrawRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);
            graphics.DrawRectangle(_Pen, CurrentPos.X, CurrentPos.Y, 300, 200);
            Points.Add(new KeyValuePair<string, Point[]>("DrawRect", new Point[] { CurrentPos }));
            pictureBox1.Invalidate();
            graphics.Dispose();
        }

        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _Color = colorDialog.Color;
                _Pen = new Pen(_Color, 20);
                _Brush = new SolidBrush(_Color);
            }
        }

        private void FillRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);
            graphics.FillRectangle(_Brush, CurrentPos.X, CurrentPos.Y, 300, 200);
            Points.Add(new KeyValuePair<string, Point[]>("FillRect", new Point[] { CurrentPos }));
            graphics.Dispose();
            pictureBox1.Invalidate();
        }

        private void DrawEllipseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);
            graphics.DrawPie(_Pen, CurrentPos.X, CurrentPos.Y, 100, 100, 0, 360);
            Points.Add(new KeyValuePair<string, Point[]>("DrawEllipse", new Point[] { CurrentPos }));
            graphics.Dispose();
            pictureBox1.Invalidate();
        }

        private void FillEllipseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);
            graphics.FillPie(_Brush, CurrentPos.X, CurrentPos.Y, 100, 100, 0, 360);
            Points.Add(new KeyValuePair<string, Point[]>("FillEllipse", new Point[] { CurrentPos }));
            graphics.Dispose();
            pictureBox1.Invalidate();
        }

        private void DrawTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);

            Point point1 = new Point(CurrentPos.X, CurrentPos.Y);
            Point point2 = new Point(CurrentPos.X + 200, CurrentPos.Y);
            Point point3 = new Point(CurrentPos.X + 100, CurrentPos.Y + 100);
            Point[] points = new[] { point1, point2, point3 };

            graphics.DrawLines(_Pen, points );
            Points.Add(new KeyValuePair<string, Point[]>("Triangle", points));
            graphics.Dispose();
            pictureBox1.Invalidate();
        }

        private void OpenImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Bitmap.FromFile(fileDialog.FileName);
            pictureBox1.Invalidate();
        }

        private void SabeoolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, Points);
                fs.Close();
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(fs);
                Points = (List<KeyValuePair<string, Point[]>>)obj;
                fs.Close();
            }

            Graphics graphics = Graphics.FromImage(_Image);

            graphics.Clear(Color.White);
            foreach (var item in Points)
            {
                switch (item.Key)
                    {
                    case "Point":
                        graphics.FillRectangle(_Brush, item.Value[0].X, item.Value[0].Y, 20, 20);
                        break;
                    case "Line":
                        graphics.DrawLine(_Pen, item.Value[0], item.Value[1]);
                        break;
                    case "FillRect":
                        graphics.FillRectangle(_Brush, item.Value[0].X, item.Value[0].Y, 300, 200);
                        break;
                    case "DrawRect":
                        graphics.DrawRectangle(_Pen, item.Value[0].X, item.Value[0].Y, 300, 200);
                        break;
                    case "DrawEllipse":
                        graphics.DrawPie(_Pen, item.Value[0].X, item.Value[0].Y, 100, 100, 0, 360);
                        break;
                    case "FillEllipse":
                        graphics.FillPie(_Brush, item.Value[0].X, item.Value[0].Y, 100, 100, 0, 360);
                        break;
                    case "Triangle":
                        graphics.DrawLines(_Pen, item.Value);
                        break;
                }
            }
            graphics.Dispose();
            pictureBox1.Invalidate();
        }

        private void SaveAsJpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = ".jpg|save as jpg";
            if (sf.ShowDialog() == DialogResult.OK)
                pictureBox1.Image.Save(sf.FileName);
        }

        private void RotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
            pictureBox1.Invalidate();
        }

        private void BrighteningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);
            Bitmap bitmap = new Bitmap(pictureBox1.Image);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr intPtr = bitmapData.Scan0;

            int size = bitmapData.Stride * bitmapData.Height;
            byte[] arr = new byte[size];

            Marshal.Copy(intPtr, arr, 0, size);
            int len = arr.Length;

            for (int i = 0; i < len; i++)
            {
                int res = arr[i] + 50;
                if (res <= 255) 
                    arr[i] += 50;
            }

            Marshal.Copy(arr, 0, intPtr, size);

            bitmap.UnlockBits(bitmapData);

            pictureBox1.Image = bitmap;
            pictureBox1.Invalidate();
            graphics.Dispose();
        }

        private void BlackoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage(_Image);
            Bitmap bitmap = new Bitmap(pictureBox1.Image);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr intPtr = bitmapData.Scan0;

            int size = bitmapData.Stride * bitmapData.Height;
            byte[] arr = new byte[size];

            Marshal.Copy(intPtr, arr, 0, size);
            int len = arr.Length;

            for (int i = 0; i < len; i++)
            {
                int res = arr[i] + 50;
                if (res <= 255) 
                    arr[i] -= 50;
            }

            Marshal.Copy(arr, 0, intPtr, size);

            bitmap.UnlockBits(bitmapData);

            pictureBox1.Image = bitmap;
            pictureBox1.Invalidate();
            graphics.Dispose();

        }
    }
}
