using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Variáveis Globais //

        private string strPathFile = @"C:\Alan_turing\Rank.txt";
        string LOCAL_IMG = "C:\\Alan_turing\\Imagens Principal\\";
        string LOCAL_TXT_FOTOS = "C:\\Alan_Turing\\Imagens Curiosidades\\";
        string LOCAL_IMG_TURING = "C:\\Alan_Turing\\Imagens Turing\\";
        string ITEM_COMP_1 = "";
        string ITEM_COMP_2 = "";
        string EXT_IMG = ".jpg";
        string EXT_TXT = ".txt";
        string ITEM;
        int TESTE = 0;
        int JOGADA = 0;
        int JOGADA_2 = 0;
        int[] TABELA = new int[50];
        int mili = 0, seg = 0, min = 0;
        int contUm = 0;
        int FIM = 0;

        

       // Load Principal //

        private void Form1_Load(object sender, EventArgs e)
        {   

            
            Embaralhar();
            pictureBox_Visual.Image = Bitmap.FromFile("C:\\Alan_turing\\Imagens Curiosidades\\0.jpg");
            textBox_Principal.Text = " Conheça um pouco mais do pai da computação Alan Turing. Na foto ao lado Turing e seus amigos.                                        Regras do jogo:                            Ache os pares correspondentes no quadro ao lado.  A cada click você visualiza uma curiosidade sobre Turing. A cada acerto uma parte da figura será desvendada.";

            foreach (Object COMPONENTE in this.Controls)
            {
                if (COMPONENTE is PictureBox)
                {
                    ITEM = ((PictureBox)COMPONENTE).Tag.ToString();
                    string ENDERECO = string.Concat(LOCAL_IMG, ITEM, EXT_IMG);
                    ((PictureBox)COMPONENTE).Image = Bitmap.FromFile(ENDERECO);
                    
                }
            }




        }

        
        // Metodo Jogada //

        private void Jogada_Click(object COMPONENTE, EventArgs e)
        {

        
            // Tabela Emabaralhada //

            ITEM = ((PictureBox)COMPONENTE).Tag.ToString();
            string ITEM_IMG = TABELA[int.Parse(ITEM)].ToString();
            string ENDERECO_TXT = string.Concat(LOCAL_TXT_FOTOS, ITEM_IMG, EXT_TXT);
            string ENDERECO_IMG = string.Concat(LOCAL_TXT_FOTOS, ITEM_IMG, EXT_IMG);




            // Carregando Imagem e Texto nos componentes //

            ((PictureBox)COMPONENTE).Image = Bitmap.FromFile(ENDERECO_IMG);
            ((PictureBox)COMPONENTE).Enabled = false;

            pictureBox_Visual.Image = Bitmap.FromFile(ENDERECO_IMG);

            StreamReader ARQUIVO = new StreamReader(ENDERECO_TXT, System.Text.ASCIIEncoding.UTF7);

            while (!ARQUIVO.EndOfStream)
            {
                string ARQ_DICA = ARQUIVO.ReadLine();
                textBox_Principal.Text = ARQ_DICA;
            }


            // Teste de Verificação //

            if (TESTE == 0)
            {
                TESTE++;
                JOGADA = TABELA[int.Parse(ITEM)];
                ITEM_COMP_1 = ITEM;
            }
            else
            {

                TESTE = 0;
                ITEM_COMP_2 = ITEM;


                // Pausa Antes da Verificação //          

                                
                Refresh();

                
                Thread.Sleep(1500);
                
               

                if (JOGADA == TABELA[int.Parse(ITEM)])
                {

                                        

                    foreach (Object COMP in this.Controls)
                    {
                        if (COMP is PictureBox)
                        {
                            if (((PictureBox)COMP).Tag.ToString() == ITEM_COMP_1)
                            {
                                string ENDERECO = string.Concat(LOCAL_IMG_TURING, ITEM_COMP_1, EXT_IMG);
                                ((PictureBox)COMP).Image = Bitmap.FromFile(ENDERECO);
                                ((PictureBox)COMP).Enabled = false;
                            }

                            if (((PictureBox)COMP).Tag.ToString() == ITEM_COMP_2)
                            {
                                string ENDERECO = string.Concat(LOCAL_IMG_TURING, ITEM_COMP_2, EXT_IMG);
                                ((PictureBox)COMP).Image = Bitmap.FromFile(ENDERECO);
                                ((PictureBox)COMP).Enabled = false;

                            }
                        }
                    }


                    FIM = 0;

                    foreach (Object COMP in this.Controls)
                    {
                        if (COMP is PictureBox)
                        {
                            if (((PictureBox)COMP).Enabled == true)
                            { 
                                FIM = 1;   
                            }
                        }
                    }


                    if (FIM == 0)
                    {
                        timer.Dispose();
                        FIM = 0;

                        Form2 cadastro = new Form2(labelClock.Text);
                        cadastro.ShowDialog();

                        string nome = cadastro.Name.Replace(" ","_");
                        string tempo = labelClock.Text;

                        if (File.Exists(strPathFile))
                        {
                            using (StreamWriter sw = File.AppendText(strPathFile))
                            {
                                sw.WriteLine("\n" + nome + ";" + tempo);
                            }
                        }
                        
                        Form3 Rank = new Form3();
                        Rank.ShowDialog();
                    }

                  
                }
                else
                {

                    foreach (Object COMP in this.Controls)
                    {
                        if (COMP is PictureBox)
                        {
                            if (((PictureBox)COMP).Tag.ToString() == ITEM_COMP_1)
                            {
                                string ENDERECO = string.Concat(LOCAL_IMG, ITEM_COMP_1, EXT_IMG);
                                ((PictureBox)COMP).Image = Bitmap.FromFile(ENDERECO);
                                ((PictureBox)COMP).Enabled = true;
                            }

                            if (((PictureBox)COMP).Tag.ToString() == ITEM_COMP_2)
                            {
                                string ENDERECO = string.Concat(LOCAL_IMG, ITEM_COMP_2, EXT_IMG);
                                ((PictureBox)COMP).Image = Bitmap.FromFile(ENDERECO);
                                ((PictureBox)COMP).Enabled = true;

                            }
                        }
                    }
                }                
            }           
        }
            

        
        // Metodo Sair //


        private void button_Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Metodo do Botão Reiniciar //

        private void button_Reiniciar_Click(object sender, EventArgs e)
        {

            mili = 0;
            min = 0;
            seg = 0;
            contUm = 0;


            timer.Stop();
            timer.Start();
            Reiniciar();
            Embaralhar();
        }


        // Metodo Reiniciar //

        private void Reiniciar()
        {
            
            pictureBox_Visual.Image = Bitmap.FromFile("C:\\Alan_turing\\Imagens Curiosidades\\0.jpg");
            textBox_Principal.Text = " Conheça um pouco mais do pai da computação Alan Turing. Na foto ao lado Turing e seus amigos.                                        Regras do jogo:                            Ache os pares correspondentes no quadro ao lado.  A cada click você visualiza uma curiosidade sobre Turing. A cada acerto uma parte da figura será desvendada.";

            foreach (Object COMPONENTE in this.Controls)
            {
                if (COMPONENTE is PictureBox)
                {
                    ITEM = ((PictureBox)COMPONENTE).Tag.ToString();
                    string ENDERECO = string.Concat(LOCAL_IMG, ITEM, EXT_IMG);
                    ((PictureBox)COMPONENTE).Image = Bitmap.FromFile(ENDERECO);
                    ((PictureBox)COMPONENTE).Enabled = true;
                }
            }
        }


        // Metodo para embaralhar o tabuleiro //

        private void Embaralhar()        
        {

            Random GERADOR = new Random(DateTime.Now.Millisecond);

            for (int K = 1; K <= 48; K++)
            {
                TABELA[K] = 0;                            
            }
            
            
            int I = 1;
            while ( I <= 48 )
            {

                int QTD = GERADOR.Next() % 24;

                int CONT = 0;

                for (int J = 1; J <= 48; J++)
                {
                    if (TABELA[J] == QTD+1)
                    {
                        CONT++;
                    }                    
                }

                
                if (CONT < 2)
                {
                    TABELA[I] = QTD+1;
                    I++;
                }
            
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            contUm++;
            min = (contUm / 3600);
            seg = (contUm % 3600) / 60;
            mili = (contUm % 3600) % 60;

            labelClock.Text = string.Format("{0:#,0#}:{1:#,0#}:{2:#,0#}", min, seg, mili);

            if (min == 59)
            {
                timer.Dispose();
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 Rank = new Form3();
            Rank.ShowDialog();
        }

        
    }
}
