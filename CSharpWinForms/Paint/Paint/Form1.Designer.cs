
namespace Paint
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.нарисоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.треугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полуюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заполненнуюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.эллипсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полуюToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.заполненнуюToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.прямоугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.полыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заполненныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьЦветToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьИзображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакJpgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повернутьНа180ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.осветлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.затемнениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.нарисоватьToolStripMenuItem,
            this.изменитьЦветToolStripMenuItem,
            this.открытьИзображениеToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem,
            this.сохранитьКакJpgToolStripMenuItem,
            this.повернутьНа180ToolStripMenuItem,
            this.осветлениеToolStripMenuItem,
            this.затемнениеToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(237, 220);
            // 
            // нарисоватьToolStripMenuItem
            // 
            this.нарисоватьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.треугольникToolStripMenuItem,
            this.эллипсToolStripMenuItem,
            this.прямоугольникToolStripMenuItem});
            this.нарисоватьToolStripMenuItem.Name = "нарисоватьToolStripMenuItem";
            this.нарисоватьToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.нарисоватьToolStripMenuItem.Text = "Нарисовать";
            // 
            // треугольникToolStripMenuItem
            // 
            this.треугольникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.полуюToolStripMenuItem,
            this.заполненнуюToolStripMenuItem});
            this.треугольникToolStripMenuItem.Name = "треугольникToolStripMenuItem";
            this.треугольникToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.треугольникToolStripMenuItem.Text = "Прямоугольник";
            // 
            // полуюToolStripMenuItem
            // 
            this.полуюToolStripMenuItem.Name = "полуюToolStripMenuItem";
            this.полуюToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.полуюToolStripMenuItem.Text = "Полую";
            this.полуюToolStripMenuItem.Click += new System.EventHandler(this.DrawRectToolStripMenuItem_Click);
            // 
            // заполненнуюToolStripMenuItem
            // 
            this.заполненнуюToolStripMenuItem.Name = "заполненнуюToolStripMenuItem";
            this.заполненнуюToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.заполненнуюToolStripMenuItem.Text = "Заполненную";
            this.заполненнуюToolStripMenuItem.Click += new System.EventHandler(this.FillRectToolStripMenuItem_Click);
            // 
            // эллипсToolStripMenuItem
            // 
            this.эллипсToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.полуюToolStripMenuItem1,
            this.заполненнуюToolStripMenuItem1});
            this.эллипсToolStripMenuItem.Name = "эллипсToolStripMenuItem";
            this.эллипсToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.эллипсToolStripMenuItem.Text = "Эллипс";
            // 
            // полуюToolStripMenuItem1
            // 
            this.полуюToolStripMenuItem1.Name = "полуюToolStripMenuItem1";
            this.полуюToolStripMenuItem1.Size = new System.Drawing.Size(189, 26);
            this.полуюToolStripMenuItem1.Text = "Полый";
            this.полуюToolStripMenuItem1.Click += new System.EventHandler(this.DrawEllipseToolStripMenuItem1_Click);
            // 
            // заполненнуюToolStripMenuItem1
            // 
            this.заполненнуюToolStripMenuItem1.Name = "заполненнуюToolStripMenuItem1";
            this.заполненнуюToolStripMenuItem1.Size = new System.Drawing.Size(189, 26);
            this.заполненнуюToolStripMenuItem1.Text = "Заполненный";
            this.заполненнуюToolStripMenuItem1.Click += new System.EventHandler(this.FillEllipseToolStripMenuItem1_Click);
            // 
            // прямоугольникToolStripMenuItem
            // 
            this.прямоугольникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.полыйToolStripMenuItem,
            this.заполненныйToolStripMenuItem});
            this.прямоугольникToolStripMenuItem.Name = "прямоугольникToolStripMenuItem";
            this.прямоугольникToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.прямоугольникToolStripMenuItem.Text = "Треугольник";
            // 
            // полыйToolStripMenuItem
            // 
            this.полыйToolStripMenuItem.Name = "полыйToolStripMenuItem";
            this.полыйToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.полыйToolStripMenuItem.Text = "Полый";
            this.полыйToolStripMenuItem.Click += new System.EventHandler(this.DrawTriangleToolStripMenuItem_Click);
            // 
            // заполненныйToolStripMenuItem
            // 
            this.заполненныйToolStripMenuItem.Name = "заполненныйToolStripMenuItem";
            this.заполненныйToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.заполненныйToolStripMenuItem.Text = "Заполненный";
            // 
            // изменитьЦветToolStripMenuItem
            // 
            this.изменитьЦветToolStripMenuItem.Name = "изменитьЦветToolStripMenuItem";
            this.изменитьЦветToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.изменитьЦветToolStripMenuItem.Text = "Изменить цвет";
            this.изменитьЦветToolStripMenuItem.Click += new System.EventHandler(this.ChangeColorToolStripMenuItem_Click);
            // 
            // открытьИзображениеToolStripMenuItem
            // 
            this.открытьИзображениеToolStripMenuItem.Name = "открытьИзображениеToolStripMenuItem";
            this.открытьИзображениеToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.открытьИзображениеToolStripMenuItem.Text = "Открыть изображение";
            this.открытьИзображениеToolStripMenuItem.Click += new System.EventHandler(this.OpenImageToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.SabeoolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // сохранитьКакJpgToolStripMenuItem
            // 
            this.сохранитьКакJpgToolStripMenuItem.Name = "сохранитьКакJpgToolStripMenuItem";
            this.сохранитьКакJpgToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.сохранитьКакJpgToolStripMenuItem.Text = "Сохранить как jpg";
            this.сохранитьКакJpgToolStripMenuItem.Click += new System.EventHandler(this.SaveAsJpgToolStripMenuItem_Click);
            // 
            // повернутьНа180ToolStripMenuItem
            // 
            this.повернутьНа180ToolStripMenuItem.Name = "повернутьНа180ToolStripMenuItem";
            this.повернутьНа180ToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.повернутьНа180ToolStripMenuItem.Text = "Повернуть на 180";
            this.повернутьНа180ToolStripMenuItem.Click += new System.EventHandler(this.RotateToolStripMenuItem_Click);
            // 
            // осветлениеToolStripMenuItem
            // 
            this.осветлениеToolStripMenuItem.Name = "осветлениеToolStripMenuItem";
            this.осветлениеToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.осветлениеToolStripMenuItem.Text = "Осветление";
            this.осветлениеToolStripMenuItem.Click += new System.EventHandler(this.BrighteningToolStripMenuItem_Click);
            // 
            // затемнениеToolStripMenuItem
            // 
            this.затемнениеToolStripMenuItem.Name = "затемнениеToolStripMenuItem";
            this.затемнениеToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
            this.затемнениеToolStripMenuItem.Text = "Затемнение";
            this.затемнениеToolStripMenuItem.Click += new System.EventHandler(this.BlackoutToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(885, 503);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 503);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem нарисоватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem треугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem эллипсToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прямоугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem полуюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заполненнуюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem полуюToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem заполненнуюToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem полыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заполненныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьЦветToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьИзображениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакJpgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повернутьНа180ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem осветлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem затемнениеToolStripMenuItem;
    }
}

