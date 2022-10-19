namespace SdlGames.Engine.Math;

public struct Vector2F
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2F(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vector2F Add(Vector2F other) 
        => new(this.X + other.X, this.Y + other.Y);

    public Vector2F Subdivide(Vector2F other)
        => new(this.X - other.X, this.Y - other.Y);

    public Vector2F Multiply(Vector2F other)
        => new(this.X * other.X, this.Y * other.Y);

    public Vector2F Divide(Vector2F other)
        => new(this.X / other.X, this.Y / other.Y);
    
    public double Magnitude()
        => (float)System.Math.Sqrt(this.X * this.X + this.Y * this.Y);
    
    public Vector2F Normalize()
        => this.Divide(new Vector2F((float)this.Magnitude(), (float)this.Magnitude()));
    
    public static Vector2F operator +(Vector2F left, Vector2F right)
        => left.Add(right);

    public static Vector2F operator -(Vector2F left, Vector2F right)
        => left.Subdivide(right);

    public static Vector2F operator *(Vector2F left, Vector2F right)
        => left.Multiply(right);

    public static Vector2F operator /(Vector2F left, Vector2F right)
        => left.Divide(right);
}

public struct Vector2D
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vector2D(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vector2D Add(Vector2D other) 
        => new(this.X + other.X, this.Y + other.Y);

    public Vector2D Subdivide(Vector2D other)
        => new(this.X - other.X, this.Y - other.Y);

    public Vector2D Multiply(Vector2D other)
        => new(this.X * other.X, this.Y * other.Y);

    public Vector2D Divide(Vector2D other)
        => new(this.X / other.X, this.Y / other.Y);
    
    public double Magnitude()
        => System.Math.Sqrt(this.X * this.X + this.Y * this.Y);
    
    public Vector2D Normalize()
        => this.Divide(new Vector2D(this.Magnitude(), this.Magnitude()));
    
    public static Vector2D operator +(Vector2D left, Vector2D right)
        => left.Add(right);

    public static Vector2D operator -(Vector2D left, Vector2D right)
        => left.Subdivide(right);

    public static Vector2D operator *(Vector2D left, Vector2D right)
        => left.Multiply(right);

    public static Vector2D operator /(Vector2D left, Vector2D right)
        => left.Divide(right);
}

public struct Vector2I
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2I(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vector2I Add(Vector2I other) 
        => new(this.X + other.X, this.Y + other.Y);

    public Vector2I Subdivide(Vector2I other)
        => new(this.X - other.X, this.Y - other.Y);

    public Vector2I Multiply(Vector2I other)
        => new(this.X * other.X, this.Y * other.Y);

    public Vector2I Divide(Vector2I other)
        => new(this.X / other.X, this.Y / other.Y);
    
    public double Magnitude()
        => (int)System.Math.Sqrt(this.X * this.X + this.Y * this.Y);

    public static Vector2I operator +(Vector2I left, Vector2I right)
        => left.Add(right);

    public static Vector2I operator -(Vector2I left, Vector2I right)
        => left.Subdivide(right);

    public static Vector2I operator *(Vector2I left, Vector2I right)
        => left.Multiply(right);

    public static Vector2I operator /(Vector2I left, Vector2I right)
        => left.Divide(right);
}