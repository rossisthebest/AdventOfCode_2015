//DayOne();
//DayTwo();
DayThree();

static void DayOne()
{
    string input = File.ReadAllText("Inputs\\Day1a.txt");

    int ups = input.Count(x => x.Equals('('));
    int downs = input.Count(x => x.Equals(')'));

    int answer = ups - downs;

    Console.WriteLine(answer);
    Console.ReadLine();

    ////////////////////////////////////////////////////////
    string strTargetPosition = File.ReadAllText("Inputs\\Day1b.txt");
    int targetPosition = 0;
    int.TryParse(strTargetPosition, out targetPosition);


    int currFloor = 0;
    int position = 0;
    for (int i = 0; i < input.Length; i++)
    {
        if (input[i].Equals('('))
        {
            currFloor++;
        }
        else
        {
            currFloor--;
        }

        if (currFloor < targetPosition)
        {
            position = i;
            break;
        }
    }

    int answerDay1Part2 = position + 1; // position is 1 based rather than 0 based

    Console.WriteLine(answerDay1Part2);
    Console.ReadLine();
}

static void DayTwo()
{
    List<string> presentSizes = File.ReadAllLines("Inputs\\Day2a.txt").ToList();

    //List< Tuple<int,int,int> > res = presentSizes
    //                                (
    //                                    x =>
    //                                       x.Select(convertToRectangularPrism(x)
    //                                );

    //Int64 totalArea = presentSizes.SelectMany
    //                                (
    //                                    x =>
    //                                       (rectangularPrism) => convertToRectangularPrism(x)
    //                                ).SelectMany
    //                                (
    //                                    y =>
    //                                        (volume) => presentWrappingArea(y.Item1, y.Item2, y.Item3)
    //                                ).Sum(z => z.volume);

    Int64 answerDay2Part1 = 0;
    Int64 answerDay2Part2 = 0;
    foreach (string item in presentSizes)
    {
        Tuple<int, int, int> rectangularPrism = convertToRectangularPrism(item);
        answerDay2Part1 += presentWrappingArea(rectangularPrism.Item1, rectangularPrism.Item2, rectangularPrism.Item3);
        answerDay2Part2 += ribbonWrappingArea(rectangularPrism.Item1, rectangularPrism.Item2, rectangularPrism.Item3);
    }


    Console.WriteLine(answerDay2Part1);
    Console.WriteLine(answerDay2Part2);
    Console.ReadLine();
}

static Tuple<int,int,int> convertToRectangularPrism(string input)
{
    int length, width, height;
    string[] inputBreakdown = input.Split('x');
    length = int.Parse(inputBreakdown[0]);
    width = int.Parse(inputBreakdown[1]);
    height = int.Parse(inputBreakdown[2]);

    return Tuple.Create(length, width, height);
}

static int presentWrappingArea(int length, int width, int height)
{
    int area1 = length * width;
    int area2 = width * height;
    int area3 = height * length;

    int smallestArea = area1;
    if (area2 < smallestArea)
    {
        smallestArea = area2;
    }
    if (area3 < smallestArea)
    {
        smallestArea = area3;
    }

    int result = (2 * area1) + (2 * area2) + (2 * area3) + smallestArea;

    return result;
}

static int ribbonWrappingArea(int length, int width, int height)
{
    //A present with dimensions 2x3x4 requires 2 + 2 + 3 + 3 = 10 feet of ribbon to wrap the present plus 2 * 3 * 4 = 24 feet of ribbon for the bow, for a total of 34 feet.
    int volume = length * width * height;

    int[] sizes = new List<int>()
    { length, width, height }.OrderBy(x=>x).ToArray();

    int smallest = sizes[0];
    int middle = sizes[1];

    int result = (2 * smallest) + (2 * middle) + volume;

    return result;
}

static void DayThree()
{
    string directions = File.ReadAllText("Inputs\\Day3a.txt");

    Dictionary<CoOrd, int> counts = new Dictionary<CoOrd,int>();

    int numSantas = 2;

    CoOrd[] santaPositions = new CoOrd[numSantas];

    int santaIndex = 0;

    // Init all santa positions to 0,0
    for (int i = 0; i < numSantas; i++)
    {
        santaPositions[i] = new CoOrd() { x = 0, y = 0 };
    }

    counts.Add(santaPositions[santaIndex], 1);

    foreach (char direction in directions)
    {
        switch (direction)
        {
            case '^':
                santaPositions[santaIndex].y++;
                break;
            case '>':
                santaPositions[santaIndex].x++;
                break;
            case 'v':
                santaPositions[santaIndex].y--;
                break;
            case '<':
                santaPositions[santaIndex].x--;
                break;
            default:
                break;
        }

        if (!counts.ContainsKey(santaPositions[santaIndex]))
        {
            counts.Add(santaPositions[santaIndex], 1);
        }
        else
        {
            counts[santaPositions[santaIndex]]++;
        }

        santaIndex++;

        if (santaIndex >= numSantas)
        {
            santaIndex = 0;
        }
    }


    int answerDay3 = counts.Count;

    Console.WriteLine(answerDay3);
    Console.ReadLine();
}

struct CoOrd
{
    public int x, y;
}