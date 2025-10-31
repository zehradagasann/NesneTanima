using System;
using System.Windows.Forms;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace NesneTanima
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private CascadeClassifier faceCascade;
        private CascadeClassifier eyeCascade;
        private System.Windows.Forms.Timer timer;
        private PictureBox pictureBox1;
        private Button btnBaslat;
        private Button btnDurdur;

        public Form1()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeOpenCV();
        }

        private void InitializeFormControls()
        {
            this.Text = "OpenCV Nesne Tanıma";
            this.Size = new System.Drawing.Size(800, 600);

            pictureBox1 = new PictureBox
            {
                Name = "pictureBox1",
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };
            this.Controls.Add(pictureBox1);

            btnBaslat = new Button
            {
                Text = "BAŞLAT",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(100, 40),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnBaslat.Click += BtnBaslat_Click;
            this.Controls.Add(btnBaslat);
            btnBaslat.BringToFront();

            btnDurdur = new Button
            {
                Text = "DURDUR",
                Location = new System.Drawing.Point(120, 10),
                Size = new System.Drawing.Size(100, 40),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnDurdur.Click += BtnDurdur_Click;
            this.Controls.Add(btnDurdur);
            btnDurdur.BringToFront();

            timer = new System.Windows.Forms.Timer { Interval = 33 };
            timer.Tick += Timer_Tick;

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Form yüklendi! Yeşil BAŞLAT butonuna tıklayın.");
        }

        private void InitializeOpenCV()
        {
            try
            {
                string exePath = Application.StartupPath;
                string face1 = System.IO.Path.Combine(exePath, "haarcascade_frontalface_default.xml");
                string eye1 = System.IO.Path.Combine(exePath, "haarcascade_eye.xml");

                faceCascade = new CascadeClassifier(face1);
                eyeCascade = new CascadeClassifier(eye1);

                frame = new Mat();

                if (faceCascade.Empty())
                {
                    MessageBox.Show($"UYARI: haarcascade_frontalface_default.xml dosyası bulunamadı!\nAranılan konum: {face1}\n\nLütfen dosyayı buraya kopyalayın.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OpenCV başlatma hatası: {ex.Message}");
            }
        }

        private void BtnBaslat_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Kamera açılıyor...");

                if (capture == null || !capture.IsOpened())
                {
                    capture = new VideoCapture(0);
                    System.Threading.Thread.Sleep(1000);
                    capture.FrameWidth = 640;
                    capture.FrameHeight = 480;
                }

                if (capture.IsOpened())
                {
                    MessageBox.Show("Kamera başarıyla açıldı!");
                    timer.Start();
                    btnBaslat.Enabled = false;
                    btnDurdur.Enabled = true;
                }
                else
                {
                    MessageBox.Show("HATA: Kamera açılamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void BtnDurdur_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (capture != null && capture.IsOpened())
            {
                capture.Release();
            }
            btnBaslat.Enabled = true;
            btnDurdur.Enabled = false;
            MessageBox.Show("Kamera durduruldu.");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (capture != null && capture.IsOpened())
            {
                capture.Read(frame);

                if (!frame.Empty())
                {
                    Mat grayFrame = new Mat();
                    Cv2.CvtColor(frame, grayFrame, ColorConversionCodes.BGR2GRAY);
                    Cv2.EqualizeHist(grayFrame, grayFrame);

                    Rect[] faces = faceCascade.DetectMultiScale(
                        grayFrame,
                        scaleFactor: 1.1,
                        minNeighbors: 3,
                        flags: HaarDetectionTypes.ScaleImage,
                        minSize: new OpenCvSharp.Size(30, 30)
                    );

                    foreach (var face in faces)
                    {
                        Cv2.Rectangle(frame, face, new Scalar(0, 255, 0), 3);

                        Mat faceROI = new Mat(grayFrame, face);
                        Rect[] eyes = eyeCascade.DetectMultiScale(faceROI);

                        foreach (var eye in eyes)
                        {
                            OpenCvSharp.Point eyeCenter = new OpenCvSharp.Point(
                                face.X + eye.X + eye.Width / 2,
                                face.Y + eye.Y + eye.Height / 2
                            );
                            int radius = (int)Math.Round((eye.Width + eye.Height) * 0.25);
                            Cv2.Circle(frame, eyeCenter, radius, new Scalar(255, 0, 0), 3);
                        }
                    }

                    Cv2.PutText(frame, $"Tespit Edilen Yuz: {faces.Length}",
                        new OpenCvSharp.Point(10, 40),
                        HersheyFonts.HersheySimplex, 1.2, new Scalar(0, 255, 255), 3);

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = BitmapConverter.ToBitmap(frame);

                    grayFrame.Dispose();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer.Stop();
            if (capture != null)
            {
                capture.Release();
                capture.Dispose();
            }
            frame?.Dispose();
            faceCascade?.Dispose();
            eyeCascade?.Dispose();
            base.OnFormClosing(e);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}