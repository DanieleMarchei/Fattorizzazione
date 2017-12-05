using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Fattorizzazione.Utilities
{
    class Polinomio
    {
        private BigInteger[] coefficienti;
        public BigInteger[] Coefficienti { get { return coefficienti; } }
        public int Grado { get { return Coefficienti.Length - 1; } }
        public static readonly Polinomio ZERO = new Polinomio(0);

        public Polinomio(params BigInteger[] coefficienti)
        {
            int primoNonZero = coefficienti.ToList().FindIndex(c => c != 0);
            if(primoNonZero != -1)
            {
                List<BigInteger> coefficientiList = coefficienti.ToList().ConvertAll(c => (BigInteger)c);
                coefficientiList.RemoveRange(0, primoNonZero);


                this.coefficienti = coefficientiList.ToArray(); 
            }
            else
            {
                this.coefficienti = new BigInteger[1];
                coefficienti[0] = 0;
            }
        }

        public Polinomio(int grado)
        {
            coefficienti = new BigInteger[grado + 1];
        }

        public BigInteger this[int posizione]
        {
            get
            {
                return coefficienti[Grado - posizione];
            }

            set
            {
                coefficienti[Grado - posizione] = value;
            }
        }

        public BigInteger Eval(BigInteger x, long y = 1)
        {
            BigInteger risultato = 0;
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                risultato += BigInteger.Pow(x, Coefficienti.Length - (i + 1)) * Coefficienti[i] * BigInteger.Pow(y, i);
            }

            return risultato;
        }

        public BigInteger Eval(long x, long y = 1)
        {
            BigInteger risultato = 0;
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                risultato += BigInteger.Pow(x, Coefficienti.Length - (i + 1)) * Coefficienti[i] * BigInteger.Pow(y, i);
            }
            
            return risultato;
        }

        public double Eval(double x, double y = 1)
        {
            double risultato = 0;
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                risultato += Math.Pow(x, Coefficienti.Length - (i + 1)) * (double)Coefficienti[i] * Math.Pow(y, i);
            }

            return risultato;
        }

        public Complesso Eval(Complesso x)
        {
            Complesso risultato = new Complesso(0, 0);
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                risultato += x.Pow(Coefficienti.Length - (i + 1)) * (double)Coefficienti[i];
            }

            return risultato;
        }

        public static Polinomio operator +(Polinomio n, Polinomio d)
        {
            Polinomio max = null;
            Polinomio min = null;
            if (n.Grado > d.Grado)
            {
                max = new Polinomio(n.Coefficienti);
                min = new Polinomio(d.Coefficienti);
            }
            else
            {
                max = new Polinomio(d.Coefficienti);
                min = new Polinomio(n.Coefficienti);
            }

            List<BigInteger> coeff = Enumerable.Repeat<BigInteger>(0, max.Grado - min.Grado).ToList();
            coeff.AddRange(min.Coefficienti.ToList());

            BigInteger[] coeffRisultato = coeff.Zip(max.Coefficienti, (a, b) => a + b).ToArray();

            return new Polinomio(coeffRisultato);
        }

        public static Polinomio operator -(Polinomio n, Polinomio d)
        {
            BigInteger[] coeff = d.Coefficienti.ToList().Select(c => -c).ToArray();
            Polinomio p = new Polinomio(coeff);
            return n + p;
        }

        public static Polinomio operator *(Polinomio n, Polinomio d)
        {
            Polinomio risultato = new Polinomio(n.Grado + d.Grado);

            for (int i = n.Grado; i >= 0 ; i--)
            {
                for (int j = d.Grado; j >= 0 ; j--)
                {
                    risultato[i + j] += n[i] * d[j]; 
                }
            }

            return risultato;
        }

        public static Polinomio operator /(Polinomio n, Polinomio d)
        {
            Polinomio risultato = new Polinomio(n.Grado - d.Grado);
            Polinomio a = new Polinomio(n.Coefficienti);
            Polinomio m = null;

            for (int i = risultato.Grado; i >= 0; i--)
            {
                risultato[i] = a[a.Grado] / d[d.Grado];
                Polinomio temp = new Polinomio(i);
                temp[i] = risultato[i];
                m = d * temp;
                a = a - m;
            }
            
            return risultato;
        }

        public static Polinomio operator %(Polinomio n, Polinomio d)
        {
            Polinomio risultato = new Polinomio(n.Grado - d.Grado);
            Polinomio a = new Polinomio(n.Coefficienti);
            Polinomio m = null;

            for (int i = risultato.Grado; i >= 0; i--)
            {
                risultato[i] = a[a.Grado] / d[d.Grado];
                Polinomio temp = new Polinomio(i);
                temp[i] = risultato[i];
                m = d * temp;
                a = a - m;
            }

            return a;
        }

        public static bool operator ==(Polinomio a, Polinomio b)
        {
            return Enumerable.SequenceEqual(a.Coefficienti, b.Coefficienti);
        }

        public static bool operator !=(Polinomio a, Polinomio b)
        {
            return !Enumerable.SequenceEqual(a.Coefficienti, b.Coefficienti);
        }

        public Polinomio Derivata()
        {
            BigInteger[] coeffDerivata = new BigInteger[Grado];
            for (int e = 0; e < Grado; e++)
            {
                coeffDerivata[e] = Coefficienti[e] * (Grado - e);
            }

            return new Polinomio(coeffDerivata);
        }

        public override string ToString()
        {
            string risultato = "";
            for (int i = 0; i < Coefficienti.Length; i++)
            {
                if(Coefficienti[i] != 0)
                {
                    string append;
                    switch (Coefficienti.Length - (i + 1))
                    {
                        case 1:
                            append = Coefficienti[i] != 1 ? Coefficienti[i] + "x" : "x";
                            break;
                        case 0:
                            append = Coefficienti[i].ToString();
                            break;
                        default:
                            append = Coefficienti[i] != 1 ? Coefficienti[i] + "x^" + (Coefficienti.Length - (i + 1)) : "x^" + (Coefficienti.Length - (i + 1));
                            break;

                    }
                    risultato += append;
                    if (i < Grado)
                    {
                        risultato += Coefficienti[i + 1] > 0 ? " + " : " ";
                    } 
                }
                    
            }

            return risultato;
        }

        public override bool Equals(object obj)
        {
            var polinomio = obj as Polinomio;
            return polinomio != null &&
                   EqualityComparer<BigInteger[]>.Default.Equals(coefficienti, polinomio.coefficienti) &&
                   Grado == polinomio.Grado;
        }
    }
}
