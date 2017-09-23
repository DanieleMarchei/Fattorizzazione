﻿using System;
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

        public BMatrice Riduci()
        {
            ISet<int> righeMarcate = new HashSet<int>();
            int rigaCon1;
            for (int j = 0; j < Colonne; j++)
            {
                rigaCon1 = -1;
                for (int i = 0; i < Righe; i++)
                {
                    if(this[j, i] == 1)
                    {
                        rigaCon1 = i;
                        break;
                    }
                }

                if (rigaCon1 != -1)
                {
                    righeMarcate.Add(rigaCon1);
                    List<int> colonneCon1 = new List<int>();
                    for (int i = 0; i < Colonne; i++)
                    {
                        if(i != j)
                        {
                            if(this[i, rigaCon1] == 1)
                            {
                                colonneCon1.Add(i);
                            }
                        }
                    }

                    foreach (int colonna in colonneCon1)
                    {
                        for (int r = 0; r < Righe; r++)
                        {
                            this[colonna, r] = (this[colonna, r] + this[j, r]) % 2;
                        }
                    }

                }

            }
            return this;

        }

        public int[] RigheDipendenti()
        {
            ISet<int> risultato = new HashSet<int>();

            int primaDipendente = -1;
            for (int i = 0; i < Colonne; i++)
            {
                for (int j = 0; j < Righe; j++)
                {
                    if(primaDipendente != -1)
                    {
                        if(this[i,j] == 1)
                        {
                            risultato.Add(primaDipendente);
                            risultato.Add(j);
                        }
                    }
                    else
                    {
                        if(this[i, j] == 1)
                            primaDipendente = j;
                    }
                }
                primaDipendente = -1;
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