using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Bitmap bit;
        Graphics g;
        Graphics g1;
        Graphics g2;
        Graphics graph;
        Rectangle rect;
        Pen p;
        Pen p1;
        Pen p2;
        Brush b;
        Brush bc;
        Brush bb;
        int b_w=10, b_h=10;
        int[][] rysowanie2;
        int[][] wzor;
        int[][] wzor_kolumn;
        float[][] dziesiatka;
        float[][] dziesiatka_k;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            g1 = panel2.CreateGraphics();
            g2 = panel3.CreateGraphics();
            p = new Pen(Color.Red, 10);
            p1 = new Pen(Color.White, 10);
            p2 = new Pen(Color.Black, 1);
            bc = new SolidBrush(Color.Red);
            bb = new SolidBrush(Color.White);


            rysowanie2 = new int[10][];
            wzor = new int[10][];
            wzor_kolumn = new int[10][];
            dziesiatka = new float[10][];
            dziesiatka_k = new float[10][];
            for (int i = 0; i < 10; i++)
            {
                rysowanie2[i] = new int[10];
                wzor[i] = new int[10];
                wzor_kolumn[i] = new int[3];
                dziesiatka[i] = new float[10];
                dziesiatka_k[i] = new float[3];
            }
            //   b = new SolidBrush(Color.Red);
            czytaj_tablice();
            czytaj_tablice2();
        }

        //   private void panel1_MouseMove(object sender, MouseEventArgs e)
        // {
        //  if(e.Button)
        //   g.DrawEllipse(p, e.X, e.Y, 10, 10);
        //}


        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            int x = 0;
            int w = 0, h = 0;
            if (e.Button == MouseButtons.Left) { g.FillEllipse(bc, e.X, e.Y, b_w, b_h); x = 2; }
            else if (e.Button == MouseButtons.Right) { g.FillEllipse(bb, e.X, e.Y, b_w, b_h); x = 1; }

            if (x != 0)
            {
                for (int i = 1; i < 11; i++)
                {
                    if (e.Y < (panel2.Height / 10)) h = 1;
                    else if (e.Y > (panel2.Height / 10) * (i - 1) && e.Y < (panel2.Height / 10) * i) h = i;
                    if (e.X < (panel2.Width / 10)) w = 1;
                    else if (e.X > (panel2.Width / 10) * (i - 1) && e.X < (panel2.Width / 10) * i) w = i;
                }

                
                x = 0;
            }

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            for (float i = panel2.Height / 10; i < panel2.Height; i += panel2.Height / 10)
            {
                g1.DrawLine(p2, 0, i, panel2.Width, i);

            }
            for (float i = panel2.Width / 10; i < panel2.Width; i += panel2.Width / 10)
            {
                g1.DrawLine(p2, i, 0, i, panel2.Height);

            }




        }



        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            
            int x = 0;
            if (e.Button == MouseButtons.Left) { b = new SolidBrush(Color.Red); x = 1; }
            else if (e.Button == MouseButtons.Right) { b = new SolidBrush(Color.White); ; x = 0; }
            if (b != null)
            {
                int w = 0, h = 0;

                // for (int i = 1; i < 11; i++)
                //{
                //  if (e.X < (panel2.Width / 10)) w = 0;
                //    else if (e.X > (panel2.Width / 10) * (i - 1) && e.X < (panel2.Width / 10) * i) w = i; 
                //  }
                for (int i = 1; i < 11; i++)
                {
                    if (e.Y < (panel2.Height / 10)) h = 1;
                    else if (e.Y > (panel2.Height / 10) * (i - 1) && e.Y < (panel2.Height / 10) * i) h = i;
                    if (e.X < (panel2.Width / 10)) w = 1;
                    else if (e.X > (panel2.Width / 10) * (i - 1) && e.X < (panel2.Width / 10) * i) w = i;
                }

                float w1, w2, h1, h2;
                w1 = (panel2.Width * (w - 1)) / 10;
                w2 = panel2.Width / 10;
                h1 = (panel2.Height * (h - 1)) / 10;
                h2 = panel2.Height / 10;
                if (e.Y % (panel2.Height / 10) != 0 && e.X % (panel2.Width / 10) != 0) rysowanie2[h - 1][w - 1] = x;
                g1.FillRectangle(b, w1 + 1, h1 + 1, w2 - 1, h2 - 1);
                b = null;
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != null) richTextBox1.Text="";
            textBox1.Text = "";
            bit = new Bitmap(panel2.Width, panel2.Height);
            graph = Graphics.FromImage(bit);
            rect = panel2.RectangleToScreen(panel2.ClientRectangle);
            graph.CopyFromScreen(rect.Location, Point.Empty, panel2.Size);

            
            Color a = new Color();
            

            string komunikat = "Tablica jedynek:";

            int H = 15;
            
            for (int i = 0; i < 10; i++)
            {
                int W = 15;
                komunikat += System.Environment.NewLine;
                for (int j = 0; j < 10; j++)
                {
                    a = bit.GetPixel(W, H);
                    if (a == Color.FromArgb(255, 255, 0, 0)) rysowanie2[i][j] = 1;
                        string temp;
                    temp = Convert.ToString(rysowanie2[i][j]) + " ";
                    komunikat += temp;
                    W += 30;
                }
                H += 30;
            }

            richTextBox1.Text += System.Environment.NewLine + komunikat;
            sprawdz();
        }
 
        private void button4_Click(object sender, EventArgs e)
        {

            bit = new Bitmap(panel1.Width, panel1.Height);
            graph = Graphics.FromImage(bit);
            rect = panel1.RectangleToScreen(panel1.ClientRectangle);
            graph.CopyFromScreen(rect.Location, Point.Empty, panel1.Size);

            int poczatek_X = 0, koniec_X = 0;
            // Color a;
            //a = new Color();
            int biggest_Y = 0; int smallest_Y = panel1.Width;
            for (int i = 0; i < 300; i += 1)
            {

                bool all_null_X = true;
                for (int j = 0; j < 300; j += 1)
                {


                    Color a = new Color();
                    a = bit.GetPixel(i, j);

                    if (a == Color.FromArgb(255, 255, 0, 0))
                    {
                        if (poczatek_X == 0) { poczatek_X = i; all_null_X = false; }
                        else if (poczatek_X != 0 && all_null_X) koniec_X = i - 1;

                        if (j > biggest_Y) biggest_Y = j;
                        if (j < smallest_Y) smallest_Y = j;
                    }
                   // if (all_null_X && poczatek_X == null) break;
                }


            }

            graph.CopyFromScreen(rect.Location, Point.Empty, panel1.Size);


            int Y = biggest_Y - smallest_Y;
            int X = koniec_X - poczatek_X;
            Size pitagoras = new Size();
            // pitagoras = Math.Sqrt((Y * Y) + (X * X));
            pitagoras.Height = Y;
            pitagoras.Width = X;
            int panel_X=panel1.Left;
            int panel_Y = panel1.Top;

            
            Point punkt_p = new Point(poczatek_X, smallest_Y);

            Point poczatkowy = new Point(rect.X+ poczatek_X, rect.Y+smallest_Y);
            Size startowy_pitagoras = new Size();
            startowy_pitagoras.Height = smallest_Y;
            startowy_pitagoras.Width = poczatek_X;

            Point konicowy = new Point();
            konicowy = poczatkowy + pitagoras;

            Bitmap zeskalowane = new Bitmap(X, Y);
            Graphics graph2 = Graphics.FromImage(zeskalowane);
            graph2.CopyFromScreen(rect.Location + startowy_pitagoras,Point.Empty, pitagoras);//zmienic ten add chyba


            richTextBox1.Text = "Pierwotny rozmiar liczby:"+ System.Environment.NewLine+" W="+X+" H="+Y ;
            int skala = 1;
            for(int i=1; (zeskalowane.Height*skala)<300; i++)
            {
                skala = i;

            }
             Bitmap zeskalowane2 = new Bitmap(zeskalowane,zeskalowane.Width*skala, panel2.Height);

            Graphics graph3 = Graphics.FromImage(zeskalowane);
            graph3.InterpolationMode= System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;


            panel2.BackgroundImageLayout = ImageLayout.Center;
            panel2.BackgroundImage = zeskalowane2;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            panel2.Refresh();

            for (float i = panel2.Height / 10; i < panel2.Height; i += panel2.Height / 10)
            {
                g1.DrawLine(p2, 0, i, panel2.Width, i);

            }
            for (float i = panel2.Width / 10; i < panel2.Width; i += panel2.Width / 10)
            {
                g1.DrawLine(p2, i, 0, i, panel2.Height);

            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    rysowanie2[i][j] = 0;
                }
            }
            panel1.BackgroundImage = null;
            odsiwierz_p2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.BackgroundImage = null;
            panel1.Refresh();
            richTextBox1.Text = "";


            panel2.Refresh();

            for (float i = panel2.Height / 10; i < panel2.Height; i += panel2.Height / 10)
            {
                g1.DrawLine(p2, 0, i, panel2.Width, i);

            }
            for (float i = panel2.Width / 10; i < panel2.Width; i += panel2.Width / 10)
            {
                g1.DrawLine(p2, i, 0, i, panel2.Height);

            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    rysowanie2[i][j] = 0;
                }
            }
            
          //  panel2.Refresh();

        }


        void odsiwierz_p2()
        {
            panel2.Refresh();
        }

        private void czytaj_tablice()
        {
            String input = File.ReadAllText(@"cyfry.txt");

            int i = 0, j = 0;
            
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    wzor[i][j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }

        }


        private void czytaj_tablice2()
        {
            String input = File.ReadAllText(@"cyfry2.txt");

            int i = 0, j = 0;

            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    wzor_kolumn[i][j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }

        }

        private void sprawdz()
        {
            

            int[] narysowany = new int[10];

            for(int i=0;i<10;i++)
            {
                int ile = 0;
                for(int j=0; j<10;j++)
                {
                    if (rysowanie2[i][j] == 1) ile++;
                }
                narysowany[i] = ile;
              
            }


            int[] narysowany2 = new int[3];

            for (int i = 0; i <= 2; i++)
            {
                int ile = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (rysowanie2[j][i+3] == 1) ile++;
                }
                narysowany2[i] = ile;
              

            }

            // int procenty = 100;  
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                   dziesiatka[i][j]= 10 - Math.Abs(narysowany[j] - wzor[i][j]);
                    dziesiatka[i][j] = dziesiatka[i][j] *  0.7f ;
                  //  richTextBox1.Text += + dziesiatka[i][j]+" ";
                }
               // richTextBox1.Text += System.Environment.NewLine;
            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    dziesiatka_k[i][j] = 10 - Math.Abs(narysowany2[j] - wzor_kolumn[i][j]);
                    //richTextBox1.Text += System.Environment.NewLine + dziesiatka_k[j][i];
                    dziesiatka_k[i][j] = dziesiatka_k[i][j] ;
                   //  richTextBox1.Text += dziesiatka_k[i][j]+" ";
                }
              // richTextBox1.Text += System.Environment.NewLine;
            }


            float[] ostatnia_pomocnicza_tabela = new float[10];

            for (int i = 0; i < 10; i++)
            {
                float ile = 0.00f;
                for (int j = 0; j < 3; j++)
                {
                    ile += dziesiatka_k[i][j];
                }
                ostatnia_pomocnicza_tabela[i] = ile;

            }

            for (int i = 0; i < 10; i++)
            {
                float ile = 0.00f;
                for (int j = 0; j < 10; j++)
                {
                    ile+=dziesiatka[i][j];
                }
                ostatnia_pomocnicza_tabela[i] += ile;

            }
            int odczytana = 0;
            string komunikat="";
            for (int i = 1; i < 10; i++)
            {
                if (ostatnia_pomocnicza_tabela[i] > ostatnia_pomocnicza_tabela[odczytana]) {odczytana = i; komunikat = ""; }
                else if (ostatnia_pomocnicza_tabela[odczytana] == ostatnia_pomocnicza_tabela[i]) komunikat += " "+i + " albo ";
            }
            textBox1.Text = "Narysowana cyfra to"+komunikat + " "+odczytana;
            komunikat = "";
            for (int i=0; i<10;i++)
            {

                richTextBox1.Text += System.Environment.NewLine +i+"= "+ostatnia_pomocnicza_tabela[i]+"%";
            }

            for(int i=0; i<10;i++)
            {
                ostatnia_pomocnicza_tabela[i] = 0;
                narysowany[i] = 0;
                for(int j=0;j<10;j++)
                {
                    dziesiatka[i][j] = 0;
                    rysowanie2[i][j] = 0;

                }
            }
             

        }

        private void button_lewo_Click(object sender, EventArgs e)
        {
            if( b_w>5 &&b_h>5)
            {
                b_w--;
                b_h--;
                panel3.Refresh();
            }
        }

        private void button_prawo_Click(object sender, EventArgs e)
        {
            if (b_w < 20 && b_h < 20)
            {
                b_w++;
                b_h++;
                panel3.Refresh();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            g2.FillEllipse(bc,panel3.Height /2 -b_h/2, panel3.Width / 2 -b_w/2, b_w, b_h);
        }
    }

}
