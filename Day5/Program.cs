var input = File.ReadAllText("crate_instructions.txt");

var crateInstructions = input.Split("\r\n\r\n");

var cratesString = crateInstructions[0].Split("\r\n");

Stack<string>[] CratesStack()
{
    var stacks =
        new Stack<string>[int.Parse(cratesString.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries).Last())];

    for (var i = cratesString.Length - 2; i >= 0; i--)
    {
        var line = cratesString[i];
        for (int j = 0, lineCount = 0; j < stacks.Length; j++, lineCount += 4)
        {
            if (i == cratesString.Length - 2)
            {
                stacks[j] = new Stack<string>();
            }

            var crate = line.Substring(lineCount, 3);

            if (!string.IsNullOrWhiteSpace(crate))
            {
                stacks[j].Push(crate);
            }
        }
    }

    return stacks;
}

var cratesStack = CratesStack();
var cratesList = CratesStack().Select(crateStack => crateStack.ToList()).ToArray();

var steps = crateInstructions[1].Split("\r\n").Select(instruction =>
{
    var str = instruction.Split();
    return new int[]
    {
        int.Parse(str[1]),
        int.Parse(str[3]),
        int.Parse(str[5])
    };
});

foreach (var step in steps)
{
    var amount = step[0];
    var from = step[1];
    var to = step[2];
    for (var i = 0; i < amount; i++)
    {
        cratesStack[to - 1].Push(cratesStack[from - 1].Pop());
    }

    var crates = cratesList[from - 1].Take(amount);
    cratesList[from - 1] = cratesList[from - 1].Skip(amount).ToList();
    cratesList[to - 1].InsertRange(0,crates);
}

foreach (var crate in cratesStack)
{
    Console.Write(crate.Peek()[1]);
}
Console.WriteLine();

foreach (var crate in cratesList)
{
    Console.Write(crate.First()[1]);
}
