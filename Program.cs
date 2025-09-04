using AdventOfCode_2015.Types;
using System;
using System.Security.Cryptography;
using System.Text;

//DayOne();
//DayTwo();
//DayThree();
//DayFour();
//DayFive();
DaySix();

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

    int smallestArea = new List<int>(){ area1, area2, area3 }.OrderBy(x => x).FirstOrDefault();

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

static void DayFour()
{
    string input = File.ReadAllText("Inputs\\Day4a.txt");
    int minNum = 1, maxNum = int.MaxValue;

    int answer1, answer2;
    answer1 = answer2 = minNum;

    for (int i = minNum; i < maxNum; i++)
    {
        string output = processInput($"{input}{i}");
        if (output.StartsWith("00000") && answer1 == minNum)
        {
            answer1 = i;

            if (answer2 != minNum)
            {
                break;
            }
        }
        else if (output.StartsWith("000000"))
        {
            answer2 = i;

            if (answer1 != minNum)
            {
                break;
            }
        }
    }


    Console.WriteLine($"Answer1: {answer1}");
    Console.WriteLine(processInput($"{input}{answer1}"));
    Console.WriteLine($"Answer2: {answer2}");
    Console.WriteLine(processInput($"{input}{answer2}"));
    Console.ReadLine();
}

static string processInput(string input)
{
    byte[] tmpSource;
    byte[] tmpHash;

    //Create a byte array from source data.
    tmpSource = ASCIIEncoding.ASCII.GetBytes(input);

    //Compute hash based on source data.
    tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

    return ByteArrayToString(tmpHash);
}

static string ByteArrayToString(byte[] arrInput)
{
    int i;
    StringBuilder sOutput = new StringBuilder(arrInput.Length);
    for (i = 0; i < arrInput.Length; i++)
    {
        sOutput.Append(arrInput[i].ToString("X2"));
    }
    return sOutput.ToString();
}

static void DayFive()
{
    List<Rule> rules = new List<Rule>() { Rule1, Rule2, Rule3 };
    List<Rule> rulesPart2 = new List<Rule>() { Rule4, Rule5 };

    List<string> checkList = File.ReadAllLines("Inputs\\Day5a.txt").ToList();
    List<string> niceList = new List<string>();
    List<string> niceListPart2 = new List<string>();
    List<string> naughtyList = new List<string>();

    foreach (string toCheck in checkList)
    {
        for (int i = 0; i < rules.Count; i++)
        {
            if (rules[i](toCheck))
            {
                if (i == rules.Count - 1)
                {
                    niceList.Add(toCheck);
                }
            }
            else
            {
                naughtyList.Add(toCheck);
                break;
            }
        }

        for (int i = 0; i < rulesPart2.Count; i++)
        {
            if (rulesPart2[i](toCheck))
            {
                if (i == rulesPart2.Count - 1)
                {
                    niceListPart2.Add(toCheck);
                }
            }
            else
            {
                break;
            }
        }
    }

    int niceCount = niceList.Count;
    int naughtyCount = naughtyList.Count;
    int nicePart2Count = niceListPart2.Count;

    Console.WriteLine($"NiceCount: {niceCount}");
    Console.WriteLine($"NaughtyCount: {naughtyCount}");
    Console.WriteLine($"NicePart2Count: {nicePart2Count}");
    Console.ReadLine();
}

static bool Rule1(string input)
{
    // It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
    List<char> goodChars = new List<char>() { 'a','e','i','o','u'};
    bool containsVowels = input.Where(x => goodChars.Contains(x)).Count() > 2;
    return containsVowels;
}

static bool Rule2(string input)
{
    bool res = false;
    // It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
    for (int i = 1; i < input.Length; i++)
    {
        if (input[i] == input[i-1])
        {
            res = true;
            break;
        }
        
    }
    return res;
}

static bool Rule3(string input)
{
    // It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
    List<string> badWords = new List<string>() { "ab", "cd", "pq", "xy" };
    bool res = !badWords.Exists(x => input.Contains(x));
    return res;
}

static bool Rule4(string input)
{
    //It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy(xy) or aabcdefgaa(aa), but not like aaa(aa, but it overlaps).
    bool res = false;
    for (int i = 1; i < input.Length - 2; i++)
    {
        string pattern = string.Concat(input[i - 1],input[i]);
        if ((input[i] == input[i + 2])
            && (input[i-1] == input[i+1]))
        {
            res = true;
            break;
        }
        else if (input.Substring(i+1).Contains(pattern))
        {
            res = true;
            break;
        }

    }
    return res;
}

static bool Rule5(string input)
{
    //It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi(efe), or even aaa.
    bool res = false;
    for (int i = 1; i < input.Length - 1; i++)
    {
        if (input[i-1] == input[i + 1])
        {
            res = true;
            break;
        }

    }
    return res;
}

static void DaySix()
{
    int width, height;
    width = height = 1000;
    bool[,] lightsGrid = new bool[width,height];

    List<string> commands = File.ReadAllLines("Inputs\\Day6a.txt").ToList();
    int lightCount = 0;

    int[,] lightBrightnessGrid = new int[width, height];
    int lightBrightness = 0;

    foreach (string command in commands)
    {
        LightCommand lightCommand = GetLightCommand(command);
        ProcessCommand(lightCommand, lightsGrid, ref lightCount);
        ProcessBrightnessCommand(lightCommand, lightBrightnessGrid, ref lightBrightness);
    }

    Console.WriteLine($"LightCount: {lightCount}");
    Console.WriteLine($"LightBrightness: {lightBrightness}");
    Console.ReadLine();
}

static LightCommand GetLightCommand(string command)
{
    LightAction action = LightAction.Unknown;

    CoOrd startPos, endPos;

    if (command.StartsWith("turn on"))
    {
        action = LightAction.On;
        command = command.Replace("turn on", "").Trim();
    }
    else if (command.StartsWith("turn off"))
    {
        action = LightAction.Off;
        command = command.Replace("turn off", "").Trim();
    }
    else if (command.StartsWith("toggle"))
    {
        action = LightAction.Toggle;
        command = command.Replace("toggle", "").Trim();
    }

    string[] remainingCommands = command.Split(" ");
    string startPosCommand = remainingCommands[0];
    string endPosCommand = remainingCommands[2];

    startPos = new CoOrd() { x = int.Parse(startPosCommand.Split(',')[0]), y = int.Parse(startPosCommand.Split(',')[1]) };
    endPos = new CoOrd() { x = int.Parse(endPosCommand.Split(',')[0]), y = int.Parse(endPosCommand.Split(',')[1]) };


    LightCommand cmd = new LightCommand()
    {
        Action = action,
        StartPos = startPos,
        EndPos = endPos
    };

    return cmd;
}

static void ProcessCommand(LightCommand command, bool[,] lightsGrid, ref int lightCount)
{

    for (int x = command.StartPos.x; x <= command.EndPos.x; x++)
    {
        for (int y = command.StartPos.y; y <= command.EndPos.y; y++)
        {
            switch (command.Action)
            {
                case LightAction.Unknown:
                    throw new NotImplementedException();
                    break;
                case LightAction.On:
                    if (!lightsGrid[x, y])
                    {
                        lightCount++;
                    }
                    lightsGrid[x, y] = true;
                    break;
                case LightAction.Off:
                    if (lightsGrid[x, y])
                    {
                        lightCount--;
                    }
                    lightsGrid[x, y] = false;
                    break;
                case LightAction.Toggle:
                    lightCount = lightCount + (lightsGrid[x, y] ? -1 : 1);
                    lightsGrid[x, y] = !lightsGrid[x, y];
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

    }
}

static void ProcessBrightnessCommand(LightCommand command, int[,] lightBrightnessGrid, ref int lightBrightness)
{

    for (int x = command.StartPos.x; x <= command.EndPos.x; x++)
    {
        for (int y = command.StartPos.y; y <= command.EndPos.y; y++)
        {
            switch (command.Action)
            {
                case LightAction.Unknown:
                    throw new NotImplementedException();
                    break;
                case LightAction.On:
                    lightBrightness++;
                    lightBrightnessGrid[x, y]++;
                    break;
                case LightAction.Off:
                    if (lightBrightnessGrid[x, y] > 0)
                    {
                        lightBrightnessGrid[x, y]--;
                        lightBrightness--;
                    }
                    break;
                case LightAction.Toggle:
                    lightBrightness = lightBrightness + 2;
                    lightBrightnessGrid[x, y] = lightBrightnessGrid[x, y] + 2;
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

    }
}