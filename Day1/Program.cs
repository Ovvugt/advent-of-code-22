var calories = File.ReadAllText("elf_calories.txt").Split("\r\n\r\n");

var elves = calories.Select(elf => elf.Split('\n').Select(int.Parse).Sum()).ToList();
Console.WriteLine(elves.Max()); // Solution Part One

elves.Sort();
Console.WriteLine(elves.TakeLast(3).Sum()); // Solution Part Two