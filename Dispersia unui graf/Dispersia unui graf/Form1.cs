using System;
using System.Windows.Forms;

namespace Dispersia_unui_graf
{
    /*
    Dispersia unui graf
    Fișierul de intrare conține un graf (neorientat, ponderat) prin lista de muchii. Se cere o reprezentare
    vizuală a acestuia a.ȋ. distanța ȋntre nodurile desenate să fie aproximativ egală cu valoarea ponderilor
    dintre acestea.
    Această rezolvare construiește o soluție ca fiind coordonatele ȋn plan a nodurilor, iar funcția de
    adecvare este construită ca suma ȋn modul a diferențelor dintre distanța euclidiană a două noduri și
    k*ponderea acestora(unde există). Se construiește un algoritm genetic pentru minimizarea acestei funcții.
    */

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.width = pictureBox1.Width;
            Engine.height = pictureBox1.Height;
            Engine.pictureBox = pictureBox1;
            Engine.listBox = listBox1;
            Engine.generation = Generation;
            Engine.penalty = Penalty;
            Engine.bestPenalty = BestPenalty;
            Engine.bestGeneration = BestGeneration;

            Engine.Init();

            Engine.Do();
        }
    }
}
