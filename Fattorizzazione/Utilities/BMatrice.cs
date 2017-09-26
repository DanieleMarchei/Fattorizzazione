using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    public class BMatrice
    {
        public int Righe { get; private set; }
        public int Colonne { get; private set; }
        private int[,] Valori;

        public BMatrice(int colonne, int righe)
        {
            Righe = righe;
            Colonne = colonne;
            Valori = new int[colonne, righe];
        }

        public BMatrice(BMatrice mat)
        {
            Valori = new int[mat.Colonne, mat.Righe];
            Colonne = mat.Colonne;
            Righe = mat.Righe;
            for (int r = 0; r < Righe; r++)
            {
                for (int c = 0; c < Colonne; c++)
                {
                    this[c, r] = mat[c, r];
                }
            }
        }

        public int this[int colonna, int riga]
        {
            get
            {
                return Valori[colonna, riga];
            }
            set
            {
                Valori[colonna, riga] = value % 2;
            }
        }

        private void ScambiaRighe(int r1, int r2)
        {
            int[] tempR = new int[Colonne];
            for (int i = 0; i < Colonne; i++)
            {
                tempR[i] = this[i, r1];
                this[i, r1] = this[i, r2];
            }

            for (int i = 0; i < Colonne; i++)
            {
                this[i, r2] = tempR[i];
            }
        }

        private void SommaRighe(int r1, int r2)
        {
            for (int c = 0; c < Colonne; c++)
            {
                this[c, r2] += this[c, r1]; 
            }
        }

        public void EliminazioneGaussiana()
        {
            for (int c = 0; c < Math.Min(Colonne, Righe); c++)
            {
                List<int> righeCon1 = new List<int>();
                for (int r = c; r < Righe; r++)
                {
                    if (this[c, r] == 1)
                        righeCon1.Add(r);
                }

                if(righeCon1.Count > 0)
                {
                    int prima = c;
                    ScambiaRighe(righeCon1[0], c);
                    righeCon1.Remove(righeCon1[0]);
                    foreach (int riga in righeCon1)
                    {
                        SommaRighe(prima, riga);
                    }
                }

                
            }
        }

        public List<int[]> Kernel()
        {
            //EliminazioneGaussiana();
            BMatrice mat = new BMatrice(this);
            Valori = new int[Colonne, Righe + Colonne];
            for (int r = 0; r < Righe; r++)
            {
                for (int c = 0; c < Colonne; c++)
                {
                    this[c, r] = mat[c, r];
                }
            }

            for (int r = Righe, c = 0; c < Colonne; r++, c++)
            {
                this[c, r] = 1;
            }

            Righe = Righe + Colonne;

            EliminazioneGaussiana();

            int[] v = ColonneDipendenti();
            List<int[]> risultato = new List<int[]>();
            foreach (int c in v)
            {
                int[] colonna = new int[Colonne];
                for (int r = 0; r < Colonne; r++)
                {
                    colonna[r] = this[c, r];
                }

                risultato.Add(colonna);
            }
            
            return risultato;
        }

        private int[] ColonneDipendenti()
        {
            List<int> risultato = new List<int>();
            for (int c = 0; c < Colonne; c++)
            {
                List<int> colonna = new List<int>();
                for (int r = 0; r < Righe; r++)
                {
                    colonna.Add(this[c, r]);
                }
                if (colonna.FindAll(n => n == 1).Count > 1)
                    risultato.Add(c);
            }
            return risultato.ToArray();
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Righe; i++)
            {
                for (int k = 0; k < Colonne; k++)
                {
                    s += this[k, i] + " ";
                }
                s += Environment.NewLine;
            }
            return s;
        }
    }
}
