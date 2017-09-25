namespace Fattorizzazione.Utilities
{
    public class Punto
    {
        public long X { get; private set; }
        public long Y { get; private set; }

        public Punto(long x, long y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Punto p, Punto q)
        {
            return (p.X == q.X) && (p.Y == q.Y);
        }

        public static bool operator !=(Punto p, Punto q)
        {
            return !(p == q);
        }

        public override string ToString()
        {
            return "{"+X+" , "+Y+"}";
        }
    }
}