using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Пауки
{
    public partial class Form1 : Form
    {
        private int score = 0;
        private int time = 0;
        private Timer timer;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000; // Интервал в миллисекундах (1 секунда)
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();

            CreateSpider(); // Создать первого паука при загрузке формы
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++; // Увеличение счетчика времени

            if (AreSpidersPresent() && time >= 10) // Проверка условия поражения (наличие пауков и 10 секунд)
            {
                timer.Stop(); // Остановка таймера
                MessageBox.Show("GAME OVER! Количество очков: " + score.ToString());
            }
        }



        private bool AreSpidersPresent()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Tag?.ToString() == "spider")
                {
                    return true; // Паук найден
                }
            }
            return false; // Пауки отсутствуют
        }


        private void Spider_Click(object sender, EventArgs e)
        {
            PictureBox spider = (PictureBox)sender;
            this.Controls.Remove(spider); // Удаление паука с формы
            score++; // Увеличение счетчика очков
            time = 0; // Сброс счетчика времени

            // Обновление текста элемента Label с новым значением счетчика
            label1.Text = "Очки: " + score.ToString();

            CreateSpider(); // Создать нового паука после исчезновения
        }



        private void CreateSpider()
        {
            // Создание нового паука
            PictureBox spider = new PictureBox();
            spider.SizeMode = PictureBoxSizeMode.StretchImage;
            spider.Width = 50;
            spider.Height = 50;
            spider.Left = GetRandomNumber(0, this.ClientSize.Width - spider.Width);
            spider.Top = GetRandomNumber(0, this.ClientSize.Height - spider.Height);

            // Список изображений пауков
            List<Image> spiderImages = new List<Image>()
             {
               Properties.Resources.spider_image1,
               Properties.Resources.spider_image2,
               Properties.Resources.spider_image3,
             };

            // Выбор случайного изображения паука
            Random random = new Random();
            int index = random.Next(spiderImages.Count);
            spider.Image = spiderImages[index]; // Установка изображения паука

            spider.Tag = "spider"; // Установка атрибута Tag для паука

            // Добавление обработчика события Click для паука
            spider.Click += new EventHandler(Spider_Click);

            // Добавление паука на форму
            this.Controls.Add(spider);
        }





        private int GetRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Сброс всех значений в исходное состояние
            score = 0;
            time = 0;

            // Удаление всех пауков с формы
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Tag?.ToString() == "spider")
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }

            CreateSpider(); // Создание первого паука при загрузке формы
            timer.Start(); // Запуск таймера
        }
    }
}
