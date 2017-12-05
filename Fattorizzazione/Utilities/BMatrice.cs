using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fattorizzazione.Utilities
{
    public class BMatrice
    {
        public long Righe { get; private set; }
        public long Colonne { get; private set; }
        private long[,] Valori;

        public BMatrice(long colonne, long righe)
        {
            Righe = righe;
            Colonne = colonne;
            Valori = new long[colonne, righe];
        }

        public BMatrice(BMatrice mat)
        {
            Valori = new long[mat.Colonne, mat.Righe];
            Colonne = mat.Colonne;
            Righe = mat.Righe;
            for (long r = 0; r < Righe; r++)
            {
                for (long c = 0; c < Colonne; c++)
                {
                    this[c, r] = mat[c, r];
                }
            }
        }

        public long this[long colonna, long riga]
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

        private void ScambiaRighe(long r1, long r2)
        {
            long[] tempR = new long[Colonne];
            for (long i = 0; i < Colonne; i++)
            {
                tempR[i] = this[i, r1];
                this[i, r1] = this[i, r2];
            }

            for (long i = 0; i < Colonne; i++)
            {
                this[i, r2] = tempR[i];
            }
        }

        private void SommaRighe(long r1, long r2)
        {
            for (long c = 0; c < Colonne; c++)
            {
                this[c, r2] += this[c, r1];
            }
        }

        public void EliminazioneGaussiana()
        {
            for (long c = 0; c < Math.Min(Colonne, Righe); c++)
            {
                List<long> righeCon1 = new List<long>();
                for (long r = c; r < Righe; r++)
                {
                    if (this[c, r] == 1)
                        righeCon1.Add(r);
                }

                if (righeCon1.Count > 0)
                {
                    long prima = c;
                    ScambiaRighe(righeCon1[0], c);
                    righeCon1.Remove(righeCon1[0]);
                    foreach (long riga in righeCon1)
                    {
                        SommaRighe(prima, riga);
                    }
                }
            }
        }

        public long[] SoluzioneRandom()
        {
            long[] risultato = new long[Colonne];
            for (long i = 0; i < risultato.Length; i++)
            {
                risultato[i] = -1;
            }

            #region rimuovi_righe_vuote

            List<long> righeVuote = new List<long>();
            for (long r = 0; r < Righe; r++)
            {
                bool vuota = true;
                for (long c = 0; c < Colonne; c++)
                {
                    vuota &= this[c, r] == 0;
                }

                if (vuota)
                    righeVuote.Add(r);
            }

            BMatrice mat = new BMatrice(Colonne, Righe - righeVuote.Count);
            List<long> righePiene = Enumerable.Range(0, (int)Righe).ToList().ConvertAll(x => (long)x);
            righePiene.RemoveAll(r => righeVuote.Contains(r));
            #endregion

            List<List<long>> righe = new List<List<long>>();
            for (long r = 0; r < mat.Righe; r++)
            {
                righe.Add(new List<long>());
                for (long c = 0; c < mat.Colonne; c++)
                {
                    mat[c, r] = this[c, righePiene[(int)r]];
                    righe[(int)r].Add(mat[c, r]);
                }
            }

            for (long r = mat.Righe - 1; r >= 0; r--)
            {
                List<long> indiciBloccati = new List<long>();
                List<long> indiciCon1 = new List<long>();

                for (long i = 0; i < mat.Colonne; i++)
                {
                    if (righe[(int)r][(int)i] == 1 && risultato[i] != -1)
                        indiciBloccati.Add(i);

                    if (righe[(int)r][(int)i] == 1)
                        indiciCon1.Add(i);
                }

                List<long> indiciUtilizzabili = indiciCon1.Except(indiciBloccati).ToList();
                long s = 0;
                foreach (long bloccato in indiciBloccati)
                {
                    s = (s + risultato[bloccato]) % 2;
                }
                List<long> valori = Tools.RandomListaConSommaMod2(indiciUtilizzabili.Count, s).ConvertAll(x=>(long)x);
                long k = 0;
                foreach (long index in indiciUtilizzabili)
                {
                    risultato[index] = valori[(int)k];
                    k++;
                }
            }

            return risultato;
        }

        public override string ToString()
        {
            string s = "";
            for (long i = 0; i < Righe; i++)
            {
                for (long k = 0; k < Colonne; k++)
                {
                    s += this[k, i] + " ";
                }
                s += Environment.NewLine;
            }
            return s;
        }



        #region scarto
        //public List<long[]> Kernel1()
        //{
        //    //EliminazioneGaussiana();
        //    BMatrice mat = new BMatrice(this);
        //    Valori = new long[Colonne, Righe + Colonne];
        //    for (long r = 0; r < Righe; r++)
        //    {
        //        for (long c = 0; c < Colonne; c++)
        //        {
        //            this[c, r] = mat[c, r];
        //        }
        //    }

        //    for (long r = Righe, c = 0; c < Colonne; r++, c++)
        //    {
        //        this[c, r] = 1;
        //    }

        //    Righe = Righe + Colonne;

        //    EliminazioneGaussiana();

        //    long[] v = ColonneDipendenti();
        //    List<long[]> risultato = new List<long[]>();
        //    foreach (long c in v)
        //    {
        //        long[] colonna = new long[Colonne];
        //        for (long r = 0; r < Colonne; r++)
        //        {
        //            colonna[r] = this[c, r];
        //        }

        //        risultato.Add(colonna);
        //    }

        //    return risultato;
        //}

        //public List<long[]> Kernel2()
        //{
        //    //EliminazioneGaussiana();
        //    BMatrice mat = new BMatrice(this);
        //    Valori = new long[Colonne + Righe, Righe];
        //    for (long r = 0; r < Righe; r++)
        //    {
        //        for (long c = 0; c < Colonne; c++)
        //        {
        //            this[c, r] = mat[c, r];
        //        }
        //    }

        //    Colonne = Righe + Colonne;

        //    for (long r = 0, c = Colonne; c < Righe; r++, c++)
        //    {
        //        this[c, r] = 1;
        //    }



        //    EliminazioneGaussiana();

        //    //long[] v = ColonneDipendenti();
        //    List<long[]> risultato = new List<long[]>();
        //    //foreach (long c in v)
        //    //{
        //    //    long[] colonna = new long[mat.Colonne];
        //    //    for (long r = 0; r < mat.Colonne; r++)
        //    //    {
        //    //        colonna[r] = this[c, r];
        //    //    }

        //    //    risultato.Add(colonna);
        //    //}

        //    return risultato;
        //}


        //private long[] ColonneDipendenti()
        //{
        //    List<long> risultato = new List<long>();
        //    for (long c = 0; c < Colonne; c++)
        //    {
        //        List<long> colonna = new List<long>();
        //        for (long r = 0; r < Righe; r++)
        //        {
        //            colonna.Add(this[c, r]);
        //        }
        //        if (colonna.FindAll(n => n == 1).Count > 1)
        //            risultato.Add(c);
        //    }
        //    return risultato.ToArray();
        //}
        #endregion
    }
}
