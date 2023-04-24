using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp35
{
    public partial class Form1 : Form
    { //��������� ���������� ��������� � ������ ����������� �������
        private Point PreviousPoint, point; //����� �� ����������� ������� ����
                                            //� ������� �����
        private Bitmap bmp;
        private Pen blackPen;
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            blackPen = new Pen(Color.Black, 4); //�������������� ����
        }
        private void button1_Click(object sender, EventArgs e)
        { //�������� �����
            OpenFileDialog dialog = new OpenFileDialog();
            //������ ���������� ������
            dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG,*.ICO, *.EMF, *.WMF)| *.bmp; *.jpg; *.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)//�������� ���������� ����
            {
                Image image = Image.FromFile(dialog.FileName); //��������� � image
                                                               //����������� �� ���������� �����
                int width = image.Width;
                int height = image.Height;
                pictureBox1.Width = width;
                pictureBox1.Height = height;
                bmp = new Bitmap(image, width, height); //������� � ��������� ��
                                                        //image ����������� � ������� bmp
                pictureBox1.Image = bmp; //���������� ����������� � ������� bmp
                                         //� pictureBox1
                g = Graphics.FromImage(pictureBox1.Image); //�������������� ������
                                                           //Graphics ��� ��������� � pictureBox1
            }
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        { // ���������� ������� ������� ������ �� ����
          // ���������� � ���������� ����� (PreviousPoint) ������� ����������
            PreviousPoint.X = e.X;
            PreviousPoint.Y = e.Y;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {//���������� ������� ����������� ���� �� pictuteBox1
            if (e.Button == MouseButtons.Left) //��������� ������� ����� ������
            { //���������� � point ������� ��������� ������� ����
                point.X = e.X;
                point.Y = e.Y;
                //��������� ������ ���������� ����� � �������
                //������� ��������� ������� ���� ��������� � PreviousPoint
                PreviousPoint.X = point.X;
                PreviousPoint.Y = point.Y;
                pictureBox1.Invalidate();//������������� �������� �����������
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //����� ��� �������� ���� �������� �� �����������
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    int R = bmp.GetPixel(i, j).R;
                    int G = bmp.GetPixel(i, j).G;
                    int B = bmp.GetPixel(i, j).B;
                    int Blue = (B = B + B) / 3;
                    Color p = Color.FromArgb(255, Blue, Blue, 255);
                    bmp.SetPixel(i, j, p);
                }
            Refresh(); //�������� ������� ����������� ����
                       //����� ��� �������� ���� �������� �� �����������
            for (int i = 0; i < bmp.Width - 150; i++)
                for (int j = 0; j < bmp.Height - 100; j++)
                {
                    int R1 = bmp.GetPixel(i, j).R; //��������� ���� �������� �����
                    int G1 = bmp.GetPixel(i, j).G; //��������� ���� �������� �����
                    int B1 = bmp.GetPixel(i, j).B; //��������� ���� ������ �����
                    int Gray1 = (R1 = G1 + B1) / 3; // ����������� �������
                    Color p1 = Color.FromArgb(255, Gray1, Gray1, Gray1); //���������
                    if (i <= j)
                    {
                        bmp.SetPixel(i, j, p1);
                    }
                }
            Refresh(); //�������� ������� ����������� ����
        }

        private void button2_Click(object sender, EventArgs e)
        { //���������� �����
            SaveFileDialog savedialog = new SaveFileDialog();
            //������ �������� ��� savedialog
            savedialog.Title = "��������� �������� ��� ...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
            "Bitmap File(*.bmp)|*.bmp|" +
            "GIF File(*.gif)|*.gif|" +
            "JPEG File(*.jpg)|*.jpg|" +
            "TIF File(*.tif)|*.tif|" +
            "PNG File(*.png)|*.png";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                // � fileName ���������� ������ ���� � �����
                string fileName = savedialog.FileName;
                // ������� �� ����� ��� ��������� ������� (���������� �����)
                string strFilExtn =
                fileName.Remove(0, fileName.Length - 3);
                // ��������� ���� � ������ ������� � � ������ �����������
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private Button button1;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private Button button2;

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(74, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(692, 341);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(136, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(242, 377);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private Button button3;
    }
}