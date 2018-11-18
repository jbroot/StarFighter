using System;

public class Score : IComparable<Score>
{

    public Score(string u, int s)
    {
        this.Username = u;
        this.TotalPoints = s;
    }

    // Properties.
    public string Username { get; set; }
    public int TotalPoints { get; set; }

    public int CompareTo(Score other){
        return this.TotalPoints.CompareTo(other.TotalPoints);
    }
}

