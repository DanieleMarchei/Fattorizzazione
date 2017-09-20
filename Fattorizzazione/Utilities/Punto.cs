namespace Fattorizzazione.Utilities
{
    public class Punto
    {
        public ulong X { get; private set; }
        public ulong Y { get; private set; }

        public Punto(ulong x, ulong y)
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