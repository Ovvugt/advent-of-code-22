namespace Day7;

internal class Program
{
    public static void Main()
    {
        var filesString = System.IO.File.ReadAllText("files.txt");

        var fileSystem = FileSystem.GetInstance();
        
        foreach (var line in filesString.Split("\n"))
        {
            if (line.StartsWith('$'))
            {
                fileSystem.HandleCommand(line);
            }
            else if (line.StartsWith("dir"))
            {
                fileSystem.CreateDirectory(line.Split()[1]);
            }
            else
            {
                var lineSplit = line.Split();
                fileSystem.CreateFile(int.Parse(lineSplit[0]), lineSplit[1]);
            }
        }
        
        fileSystem.CalculateUsedSpace();
        Console.WriteLine(fileSystem.Sum);
        fileSystem.CalculateUsedSpace();
        Console.WriteLine(fileSystem.SmallestDirectoryToRemove);
    }
}

internal class FileSystem
{
    private readonly Folder _rootDirectory;
    private Folder CurrentDirectory { get; set; }
    private int Capacity { get; }
    public int UsedSpace { get; private set; }
    public int AvailableSpace => Capacity - UsedSpace;

    public int Sum { get; set; }
    public int SmallestDirectoryToRemove = int.MaxValue;

    private static FileSystem? _instance;

    private FileSystem(int capacity)
    {
        _rootDirectory = new Folder("/", null);
        CurrentDirectory = _rootDirectory;
        Capacity = capacity;
    }

    public static FileSystem GetInstance()
    {
        return _instance ??= new FileSystem(70000000);
    }

    public void CalculateUsedSpace()
    {
        UsedSpace = _rootDirectory.GetSize();
    }

    public void CreateDirectory(string folderName)
    {
        CurrentDirectory.Files.Add(new Folder(folderName, CurrentDirectory));
    }

    public void CreateFile(int size, string fileName)
    {
        CurrentDirectory.Files.Add(new File(fileName, size));
    }

    private void ChangeDirectory(string folderName)
    {
        if (folderName == "/")
        {
            CurrentDirectory = _rootDirectory;
            return;
        }

        if (folderName == "..")
        {
            CurrentDirectory = CurrentDirectory.Parent ?? CurrentDirectory;
            return;
        }
        
        CurrentDirectory = (CurrentDirectory.Files.First(file => file.Name == folderName && file is Folder) as Folder)!;
    }
    
    public void HandleCommand(string line)
    {
        var lineSplit = line.Split();
        var command = lineSplit[1];
        
        if (command == "ls")
        {
            return;
        }

        var fileName = lineSplit[2];
        ChangeDirectory(fileName);
    }
}

internal class Folder : File
{
    public List<File> Files { get; }
    public Folder? Parent { get; }
    internal Folder(string name, Folder? parent) : base(name, 0)
    {
        Parent = parent;
        Files = new List<File>();
    }

    public override int GetSize()
    {
        var totalSize = 0;
        foreach (var file in Files)
        {
            totalSize += file.GetSize();
        }
        
        var fs = FileSystem.GetInstance();

        if (totalSize <= 100000)
        {
            fs.Sum += totalSize;
        }

        if (fs.UsedSpace != 0)
        {
            var newAvailableSize = fs.AvailableSpace + totalSize;
            if (newAvailableSize >= 30000000 && newAvailableSize < fs.SmallestDirectoryToRemove)
            {
                fs.SmallestDirectoryToRemove = totalSize;
            }
        }

        return totalSize;
    }
}

internal class File
{
    public string Name { get; set; }
    private readonly int _size;

    public virtual int GetSize()
        => _size;

    internal File(string name, int size)
    {
        _size = size;
        Name = name;
    }
}