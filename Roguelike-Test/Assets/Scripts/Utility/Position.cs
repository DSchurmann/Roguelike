using System;

public class Position
{
    public int x { get; set; }
    public int y { get; set; }

    public Position()
    {
        x = 0;
        y = 0;
    }

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj) => this.Equals(obj as Position);

    public bool Equals(Position p)
    {
        if (p is null)
        {
            return false;
        }

        if(Object.ReferenceEquals(this, p))
        {
            return true;
        }

        if(this.GetType() != p.GetType())
        {
            return false;
        }

        return (x == p.x) && (y == p.y);
    }

    public override int GetHashCode() => (x, y).GetHashCode();

    public static bool operator ==(Position lhs, Position rhs)
    {
        if(lhs is null)
        {
            if(rhs is null)
            {
                return true;
            }

            return false;
        }
        return lhs.Equals(rhs);
    }

    public static bool operator != (Position lhs, Position rhs) => !(lhs == rhs);

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }
}