using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fotodene
{
    public partial class Form1 : Form
    {
        private int controller = 0, pointUsr1 = 0, pointUsr2 = 0, usr1 = 0,usr2 = 1, numOfTry = 0, temp1 = 0, temp2 = 0, remainingTime;
        private List<int> num2 = new List<int>();
        private  dynamic path = "C:\\Users\\ahayd\\OneDrive\\Masaüstü\\dönem 5\\pro3 csharp\\fotodene\\fotodene\\foto\\";
        private List<int> num1 = new List<int>();
        private PictureBox firstClick, secondClick;
        private Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("bu pencereyi kapattığınızda oyun balar", ProductName, MessageBoxButtons.OKCancel);
            
            
            counter();
            label1.BackColor = Color.OrangeRed;

        }

        private void counter()
        {
            remainingTime = 7;
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            create();
            timer.Start();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            counter();
        }

        private void hide() {
            List<PictureBox> pictureBoxes = new List<PictureBox>() {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9,
                pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20
            };

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Image = null;
                pictureBox.BackColor = Color.Gray;


            }
        }

        private void hideOne(PictureBox file)
        {
            
            file.Image = null;
            file.BackColor = Color.Gray;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            remainingTime--;

            
            label3.Text = $"Oyunun Başlamasına Kalan Süre: {remainingTime} saniye";

            if (remainingTime <= 0)
            {
                
                timer.Stop(); 
                hide();
            }
        }

        private void create()
        {
            num2.Clear();
            num1.Clear();
            string[] files = Directory.GetFiles(path, "*.png");

            List<string> imagePaths = files.ToList();
            List<PictureBox> pictureBoxes = new List<PictureBox>() {pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9,
            pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20
            };

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (PictureBox file in pictureBoxes)
            {
                if (imagePaths.Count > 0)
                {
                    Random rand = new Random();
                    int randomIndex = rand.Next(0, numbers.Count);
                    int selectedNumber = numbers[randomIndex];

                    num1.Add(selectedNumber); 
                    numbers.RemoveAt(randomIndex);

                    file.SizeMode = PictureBoxSizeMode.StretchImage;
                    file.Image = Image.FromFile(imagePaths[selectedNumber]);
                }
            }
            this.num2 = new List<int>(num1);  
        }
        private void showAgain()
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>() {
             pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9,
             pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20
            };


            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                if (num2.Count > 0) 
                {
                    show(pictureBoxes[i], num2[i], 0);
                }
            }
        }
        

        private void show(PictureBox file, int selectedNumber, int boxNum)
        {

            
            string[] files = Directory.GetFiles(this.path, "*.png");

            List<string> imagePaths = files.ToList();

            if (selectedNumber >= 0 && selectedNumber < imagePaths.Count)
            {
                file.SizeMode = PictureBoxSizeMode.StretchImage;
                file.Image = Image.FromFile(imagePaths[selectedNumber]);
            }
            else
            {
                
            }
        }
        private void pictureBox_Click(object sender, EventArgs e)
        {

            controller += 1;
            PictureBox clickedPictureBox = sender as PictureBox;


            string pictureBoxName = clickedPictureBox.Name;

            
            int pictureBoxIndex = int.Parse(pictureBoxName.Substring(10)) - 1;

           
            
            int num2Index = pictureBoxIndex;

                
            show(clickedPictureBox, num2[num2Index], pictureBoxIndex);
            if (controller == 1)
            {
                firstClick = clickedPictureBox;
                temp1 = num2[num2Index];
            }else if (controller == 2)
            {
                
                controller = 0;
                secondClick = clickedPictureBox;
                temp2 = num2[num2Index];
                check();
            }
            else
            {
                
            }


        }

        private async void check()
        {
            if(temp1 == temp2)
            {
                numOfTry += 1;
                if (usr2 < usr1)
                {
                    pointUsr2 += 1;
                    label5.Text = $"Puan: {pointUsr2}";
                }
                else if (usr2 > usr1)
                {
                    pointUsr1 += 1;
                    label4.Text = $"Puan: {pointUsr1}";
                    
                }
            }
            else if(temp2 != temp1)
            {
                if (usr2 < usr1) {
                    label1.BackColor = Color.OrangeRed;
                    label2.BackColor = Color.White;
                    usr2 += 3;
                }else if (usr2 > usr1)
                {
                    label1.BackColor = Color.White;
                    label2.BackColor = Color.OrangeRed;
                    usr1 += 3;
                }

                await Task.Delay(1000);
                hideOne(firstClick);
                hideOne(secondClick);
                firstClick = null;
                secondClick = null;
                temp1 = 0;
                temp2 = 0;
            }
            if (numOfTry == 10)
            {


                MessageBox.Show($"tebrikler oyunu bitirdiniz. {numOfTry} denemede bitrdiniz");
                if (pointUsr1 < pointUsr2)
                {
                    MessageBox.Show("tebrikler 2. oyuncu kazandı");
                }
                else if (pointUsr1 > pointUsr2)
                {
                    MessageBox.Show("tebrikler 1. oyuncu kazandı");
                }
                clearEverything();
                button1.Enabled = true;

            }
            

        }
        private void clearEverything()
        {
            controller = 0;
            usr1 = 0;
            numOfTry = 0;
            temp1 = 0;
            temp2 = 0;
            pointUsr1 = 0;
            pointUsr2 = 0;
            num1.Clear();
            num2.Clear();
            label4.Text = label5.Text = "Puan:"; 

            List<PictureBox> pictureBoxes = new List<PictureBox>() {
        pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9,
        pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20
    };

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                pictureBox.Image = null;
                pictureBox.BackColor = Color.Gray;
            }

            firstClick = null;
            secondClick = null;

            
        }







    }
}
