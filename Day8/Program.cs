var treesString = File.ReadAllText("trees.txt");

var treesSplit = treesString.Split('\n');
var n = treesSplit.Length;
var trees = new Tree[n,n];

for (var i = 0; i < n; i++)
{
    for (var j = 0; j < n; j++)
    {
        trees[i, j] = new Tree(int.Parse(treesSplit[i][j].ToString()));
    }
}

CheckTrees(0, n, (x,y) => x < y, x => ++x, 0, n, (x, y) => x < y, x => ++x, trees);
CheckTrees(0, n, (x,y) => x < y, x => ++x, n - 1, 0, (x, y) => x >= y, x => --x, trees);

CheckTrees(0, n, (x,y) => x < y, x => ++x, 0, n, (x, y) => x < y, x => ++x, trees, false);
CheckTrees(0, n, (x,y) => x < y, x => ++x, n - 1, 0, (x, y) => x >= y, x => --x, trees, false);

var visibleTrees = 0;
var maxScenicScore = 0;

for (var i = 0; i < n; i++)
{
    for (var j = 0; j < n; j++)
    {
        var tree = trees[i, j];
        if (tree.Visibility > 0)
        {
            visibleTrees++;
        }

        var checkLeft = true;
        var countLeft = 0;
        var checkRight = true;
        var countRight = 0;
        var checkUp = true;
        var countUp = 0;
        var checkDown = true;
        var countDown = 0;
        for (var s = 1; s < n; s++)
        {
            if (checkLeft)
            {
                var left = i - s;
                if (left < 0)
                {
                    checkLeft = false;
                }

                if (checkLeft)
                {
                    var leftTree = trees[left, j];
                    if (leftTree.Size >= tree.Size)
                    {
                        checkLeft = false;
                    }

                    countLeft++;
                }
            }

            if (checkRight)
            {
                var right = i + s;
                if (right > n - 1)
                {
                    checkRight = false;
                }

                if (checkRight)
                {
                    var leftTree = trees[right, j];
                    if (leftTree.Size >= tree.Size)
                    {
                        checkRight = false;
                    }

                    countRight++;
                }
            }

            if (checkUp)
            {
                var up = j - s;
                if (up < 0)
                {
                    checkUp = false;
                }

                if (checkUp)
                {
                    var upTree = trees[i, up];
                    if (upTree.Size >= tree.Size)
                    {
                        checkUp = false;
                    }

                    countUp++;
                }
            }

            if (checkDown)
            {
                var down = j + s;
                if (down > n - 1)
                {
                    checkDown = false;
                }

                if (checkDown)
                {
                    var downTree = trees[i, down];
                    if (downTree.Size >= tree.Size)
                    {
                        checkDown = false;
                    }

                    countDown++;
                }
            }
        }
        tree.ScenicScore *= countLeft * countRight * countUp * countDown;
        if (tree.ScenicScore > maxScenicScore)
        {
            maxScenicScore = tree.ScenicScore;
        }
    }
}

Console.WriteLine(visibleTrees);
Console.WriteLine(maxScenicScore);

void CheckTrees(int startpointX, int endpointX, Func<int, int, bool> checkX, Func<int, int> transformationX,
    int startpointY, int endpointY, Func<int, int, bool> checkY, Func<int, int> transformationY, Tree[,] treeMatrix, bool leftRight = true)
{
    for (var x = startpointX; checkX(x, endpointX); x = transformationX(x))
    {
        var highestTree = -1;
        for (var y = startpointY; checkY(y, endpointY); y = transformationY(y))
        {
            var tree = leftRight ? treeMatrix[x, y] : treeMatrix[y, x];
            if (tree.Size <= highestTree)
            {
                tree.Visibility -= 1;
            }
            else
            {
                highestTree = tree.Size;
            }
        }
    }
}
internal class Tree
{
    public int Size { get; }
    public int Visibility { get; set; }
    public int ScenicScore { get; set; }

    public Tree(int size)
    {
        Size = size;
        Visibility = 4;
        ScenicScore = 1;
    }
}