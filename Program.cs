DayOne();

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

