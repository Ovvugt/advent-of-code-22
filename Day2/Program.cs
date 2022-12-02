var options = new Dictionary<string, int>()
{
    {"A", 1},
    {"X", 1},
    {"B", 2},
    {"Y", 2},
    {"C", 3},
    {"Z", 3},
};

var rounds = File.ReadAllText("strategy_guide.txt").Split('\n');

/*rounds = new[] { "A Y", "B X", "C Z" };*/

var score = 0;
var distinctOptions = options.Count / 2;

// Solution Part 1

foreach (var round in rounds)
{
    var choices = round.Split();
    var opponentChoice = options[choices[0]];
    var ourChoice = options[choices[1]];
    score += ourChoice;
    if (opponentChoice == ourChoice)
    {
        score += 3;
    }
    else if ((ourChoice + 1) % distinctOptions != opponentChoice % distinctOptions)
    {
        score += 6;
    }
}   

Console.WriteLine(score);

// Solution Part 2

score = 0;
foreach (var round in rounds)
{
    var choices = round.Split();
    var opponentChoice = options[choices[0]];
    var ourChoice = options[choices[1]];
    switch (ourChoice)
    {
        case 1:
            var ourPick = opponentChoice - 1;
            if (ourPick == 0) ourPick = 3;
            score += ourPick;
            break;
        case 2:
            score += 3 + opponentChoice;
            break;
        case 3:
            ourPick = (opponentChoice + 1) % distinctOptions;
            if (ourPick == 0) ourPick = 3;
            score += ourPick;
            score += 6;
            break;
    }
}

Console.WriteLine(score);