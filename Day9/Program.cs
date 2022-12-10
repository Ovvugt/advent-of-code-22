var transformations = File.ReadAllText("transformations.txt");
/*transformations = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";*/
var map = new Map();
var map2 = new Map();

var lines = transformations.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
map.Rope = new Rope(2);
map2.Rope = new Rope(10);
foreach (var line in lines)
{
    var parts = line.Split();
    map.MoveRope(parts[0], int.Parse(parts[1]));
    map2.MoveRope(parts[0], int.Parse(parts[1]));
}

Console.WriteLine(map.TailVisitedPosition.Count);
Console.WriteLine(map2.TailVisitedPosition.Count);

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position()
    {
        X = 0;
        Y = 0;
    }
}

class Rope
{
    public List<Position> Positions { get; }

    public Rope(int length)
    {
        Positions = new List<Position>();
        for (var i = 0; i < length; i++)
        {
            Positions.Add(new Position());
        }
    }
}

class Map
{
    public Rope Rope { get; set; }
    public List<Position> TailVisitedPosition { get; private set; }

    public Map()
    {
        TailVisitedPosition = new List<Position>();
    }
    
    private bool AreAdjacent(Position pos1, Position pos2)
    {
        return (pos1.X == pos2.X && pos1.Y == pos2.Y) ||
               new [] {0, 1}.Any( x => x == Math.Abs(pos1.X - pos2.X)) && pos1.Y == pos2.Y ||
               new [] {0, 1}.Any( x => x == Math.Abs(pos1.Y - pos2.Y)) && pos1.X == pos2.X ||
               new [] {0, 1}.Any( x => x == Math.Abs(pos1.X - pos2.X)) && new [] {0, 1}.Any( x => x == Math.Abs(pos1.Y - pos2.Y));
    }

    public void MoveRope(string direction, int steps)
    {
        for (var i = 0; i < steps; i++)
        {
            switch (direction)
            {
                case "U":
                    Rope.Positions[0].Y++;
                    break;
                case "D":
                    Rope.Positions[0].Y--;
                    break;
                case "L":
                    Rope.Positions[0].X--;
                    break;
                case "R":
                    Rope.Positions[0].X++;
                    break;
            }

            for (var j = 1; j < Rope.Positions.Count; j++)
            {
                var head = Rope.Positions[j - 1];
                var tail = Rope.Positions[j];
                if (!AreAdjacent(head, tail))
                {
                    if (head.X == tail.X)
                    {
                        if (head.Y > tail.Y)
                        {
                            tail.Y++;
                        }
                        else
                        {
                            tail.Y--;
                        }
                    }
                    else if (head.Y == tail.Y)
                    {
                        if (head.X > tail.X)
                        {
                            tail.X++;
                        }
                        else
                        {
                            tail.X--;
                        }
                    }
                    else
                    {
                        if (head.X > tail.X && head.Y > tail.Y)
                        {
                            tail.X++;
                            tail.Y++;
                        }
                        else if (head.X > tail.X && head.Y < tail.Y)
                        {
                            tail.X++;
                            tail.Y--;
                        }
                        else if (head.X < tail.X && head.Y > tail.Y)
                        {
                            tail.X--;
                            tail.Y++;
                        }
                        else
                        {
                            tail.X--;
                            tail.Y--;
                        }
                    }
                }
            }

            var tails = Rope.Positions.Last();
            if (!TailVisitedPosition.Any(p => p.X == tails.X && p.Y == tails.Y))
            {
                TailVisitedPosition.Add(new Position { X = tails.X, Y = tails.Y });
            }
        }
    }
}