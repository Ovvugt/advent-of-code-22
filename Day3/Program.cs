var rucksacks = File.ReadAllText("rucksacks.txt").Split('\n');
const string alphabet = "abcdefghijklmnopqrstuvwxyz";

//Solution part 1
var score = 0;

foreach (var rucksack in rucksacks)
{
    var firstHalf = rucksack[..(rucksack.Length/2)];
    var secondHalf = rucksack[(rucksack.Length / 2)..];
    foreach (var character in firstHalf.Where(character => secondHalf.Contains(character)))
    {
        score += alphabet.IndexOf(character.ToString().ToLower(), StringComparison.Ordinal) + 1 + (char.IsUpper(character) ? 26 : 0);
        break;
    }
}

Console.WriteLine(score);

//Solution part 2
score = 0;

for (var i = 0; i < rucksacks.Length; i += 3)
{
    var elf1 = rucksacks[i];
    var elf2 = rucksacks[i + 1];
    var elf3 = rucksacks[i + 2];
    foreach (var character in elf1.Where(character => elf2.Contains(character) && elf3.Contains(character)))
    {
        score += alphabet.IndexOf(character.ToString().ToLower(), StringComparison.Ordinal) + 1 + (char.IsUpper(character) ? 26 : 0);
        break;
    }
}

Console.WriteLine(score);