using System;
using System.Windows.Forms;

namespace Cercuri
{
    /*
    Cercuri cu număr maxim de puncte în interiorul lor
    Se dă o mulțime de puncte ȋn plan, și se cere găsirea a k cercuri disjuncte care să conțină un număr
    maxim de puncte.
    Această rezolvare optimizează cele k cercuri printr-un algoritm genetic, o soluție reprezentând
    coordonatele acestora. Ȋn această aplicație se poate urmări efectul mutației adaptive, care după un
    anumit timp se reglează căutând cercuri noi ȋn vecinătatea cercurilor până atunci găsite, nu pe tot spațiul.
    */

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.pictureBox = pictureBox1;
            Engine.listBox = listBox1;
            Engine.maxPoints = MaxPoints;

            Engine.Init();
            Engine.GeneratePoints();

            Engine.pictureBox.Image = Engine.bitmap;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            Generate.Enabled = false;
            Engine.Do();
        }
    }
}
