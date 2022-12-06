var dataStream = File.ReadAllText("datastream.txt");

for (var i = 3; i < dataStream.Length; i++)
{
    var subString = dataStream.Substring(i - 3, 4);
    if (RemoveDuplicates(subString).Length == 4)
    {
        Console.WriteLine(i + 1);
        break;
    }
}

for (var i = 13; i < dataStream.Length; i++)
{
    var subString = dataStream.Substring(i - 13, 14);
    if (RemoveDuplicates(subString).Length == 14)
    {
        Console.WriteLine(i + 1);
        break;
    }
}

string RemoveDuplicates(string input)
{
    var result = "";
    foreach (var c in input)
    {
        if (!result.Contains(c))
        {
            result += c;
        }
    }
    return result;
}