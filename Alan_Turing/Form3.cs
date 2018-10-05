using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {

        public string[] NOME = new string[15];



        public Form3()
        {
            InitializeComponent();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            Carregar();
        }


        public void Carregar()
        {
            //***********************************************************************************//
            StreamReader arq = new StreamReader("C:\\Alan_turing\\Rank.txt", Encoding.Default);
            Dictionary<string, string> Dados = new Dictionary<string, string>();

            string Linha = "";
            

            try
            {

                
                int I = 0;
                int J = 1;

                while ((Linha = arq.ReadLine().ToString()) != null)
                {
                    string[] dados = Linha.Split(';');
                    //dados[0]= Nome, dados[1]= tempo

                    
                    if (dados[0] != "" && dados[1] != "")
                    {

                        if (NOME != null)
                        {
                            foreach (string NM in NOME)
                            {
                                if (NM == dados[0])
                                {
                                    dados[0] = string.Concat(dados[0], "_", J.ToString());
                                    J++;
                                }
                            }
                     
                        }

                        NOME[I] = dados[0];

                        Dados.Add(dados[0], dados[1]);
                        
                    }

                    I++;

                }


            }
            catch (Exception e)
            {
                string erro = e.ToString();
            }

            finally
            {
                
                Dados = Dados.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                
            }


            int i = 0;

            foreach (string chave in Dados.Keys)
            {
                switch (i)
                {
                    case 0:
                        txtNome1.Text = " " + chave; txtTempo1.Text = Dados[chave];
                        break;
                    case 1:
                        txtNome2.Text = " " + chave; txtTempo2.Text = Dados[chave];
                        break;
                    case 2:
                        txtNome3.Text = " " + chave; txtTempo3.Text = Dados[chave];
                        break;
                    case 3:
                        txtNome4.Text = " " + chave; txtTempo4.Text = Dados[chave];
                        break;
                    case 4:
                        txtNome5.Text = " " + chave; txtTempo5.Text = Dados[chave];
                        break;
                    case 5:
                        txtNome6.Text = " " + chave; txtTempo6.Text = Dados[chave];
                        break;
                    case 6:
                        txtNome7.Text = " " + chave; txtTempo7.Text = Dados[chave];
                        break;
                    case 7:
                        txtNome8.Text = " " + chave; txtTempo8.Text = Dados[chave];
                        break;
                    case 8:
                        txtNome9.Text = " " + chave; txtTempo9.Text = Dados[chave];
                        break;
                    case 9:
                        txtNome10.Text = " " + chave; txtTempo10.Text = Dados[chave];
                        break;
                }
                i++;
            }

        }  


    }
}
