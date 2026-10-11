using System;

class Program
{
    static void Main()
    {
        int GameCount = 9;
        int ColumnCount = 3;

        int[] games = CreateTictac(GameCount);
        string[] boardState = new string[GameCount];

        int attempts = 0;
        string champion = null;

        while (attempts < games.Length && champion == null)
        {
            DisplayBoard(games, ColumnCount, boardState);

            string player = attempts % 2 == 0 ? "X" : "O";

            int index = ReadGameIndex(player, boardState);

            boardState[index] = player;
            attempts++;

            if (Champion(player, boardState))
            {
                champion = player;
            }
        }
        DisplayBoard(games, ColumnCount, boardState);

        if (champion != null)
        {
            Console.WriteLine($"플레이어 {champion}가 이겼습니다!");
        }
        else
        {
            Console.WriteLine("무승부입니다!");
        }

        WaitEnter();
    }


    static int[] CreateTictac(int GameCount)
    {
        int[] games = new int[GameCount];

        for (int i = 0; i < GameCount; i++)
        {
            games[i] = i + 1;
        }
        return games;
    }

    static void DisplayBoard(int[] games, int ColumnCount, string[] boardState)
    {
        Console.Clear();
        Console.WriteLine("=== 틱택토 게임 1단계 ===");
        Console.WriteLine("두 사람이 X와 O를 번갈아 놓습니다. X가 먼저 시작합니다.\n");

        Console.WriteLine("위치 번호:");
        for (int i = 0; i < games.Length; i++)
        {
            Console.Write($"{games[i],2}");
            if ((i + 1) % ColumnCount == 0)
            {
                Console.WriteLine();

                if (i < boardState.Length - 1)
                    Console.WriteLine("-----------");
            }
            else
            {
                Console.Write(" |");
            }
        }

        Console.WriteLine();
        Console.WriteLine("현재 번호:");
        for (int i = 0; i < boardState.Length; i++)
        {
            string num = boardState[i] == null ? "." : boardState[i];
            Console.Write($"{num,2}");
            if ((i + 1) % ColumnCount == 0)
            {
                Console.WriteLine();

                if (i < boardState.Length - 1)
                    Console.WriteLine("-----------");
            }
            else
            {
                Console.Write(" |");
            }
        }

    }

    static int ReadGameIndex(string player, string[] boardState)
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write($"플레이어 {player}의 차례입니다.\n위치를 선택하세요 (1~9): ");

            string input = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(input, out int number))
            {
                if (number < 1 || number > 9)
                {
                    Console.WriteLine("1부터 9까지의 번호를 입력하세요.");
                    continue;
                }
                int index = number - 1;

                if (boardState[index] != null)
                {
                    Console.WriteLine("이미 선택한 칸입니다. 빈칸을 선택하세요.");
                    continue;
                }
                return index;
            }

            else
            {
                Console.WriteLine("숫자로 위치를 입력하세요.");
                continue;
            }
        }

    }

    static bool Champion(string player, string[] boardState)
    {
        int[,] winCase =
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                { 0, 4, 8 }, { 2, 4, 6 }
            };

        for (int i = 0; i < winCase.GetLength(0); i++)
        {
            int a = winCase[i, 0];
            int b = winCase[i, 1];
            int c = winCase[i, 2];

            if (boardState[a] == player && boardState[b] == player && boardState[c] == player)
            {
                return true;
            }
        }

        return false;
    }

    static void WaitEnter()
    {
        Console.WriteLine("종료하려면 Enter를 누르세요.");
        Console.ReadLine();
    }

}
