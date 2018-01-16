using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public readonly Vector2 Position;
    public int Index;

    public Vertex(Vector2 position, int index)
    {
        Position = position;
        Index = index;
    }

    public Vertex()
    {
        Position = new Vector2();
        Index = Random.Range(0, 30000);
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(Vertex))
            return false;
        return Equals((Vertex)obj);
    }

    public bool Equals(Vertex obj)
    {
        return obj.Position.Equals(Position) && obj.Index == Index;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Position.GetHashCode() * 397) ^ Index;
        }
    }

    public override string ToString()
    {
        return string.Format("{0} ({1})", Position, Index);
    }
}



