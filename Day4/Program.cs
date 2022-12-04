var ids = File.ReadAllText("ids.txt").Split('\n');

var containingIds = 0;
var overlappingIds = 0;

foreach (var id in ids)
{
    var ranges = id.Split(',');
    var rangeOne = ranges[0].Split('-').Select(int.Parse).ToArray();
    var rangeTwo = ranges[1].Split('-').Select(int.Parse).ToArray();

    if (rangeOne[0] <= rangeTwo[0] && rangeTwo[0] <= rangeOne[1] ||
        rangeOne[0] <= rangeTwo[1] && rangeTwo[1] <= rangeOne[1] ||
        rangeTwo[0] <= rangeOne[0] && rangeOne[0] <= rangeTwo[1] ||
        rangeTwo[0] <= rangeOne[1] && rangeOne[1] <= rangeTwo[1])
    {
        overlappingIds++;
    }
    else continue;

    if (rangeOne[0] >= rangeTwo[0] && rangeOne[1] <= rangeTwo[1] ||
    rangeTwo[0] >= rangeOne[0] && rangeTwo[1] <= rangeOne[1])
    {
        containingIds++;
    }
}

Console.WriteLine(containingIds);
Console.WriteLine(overlappingIds);


