using System;

namespace scoreGame
{
    class Program
    {

        enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        struct Problem
        {
            public string Message;
            public int Answer;

            public Problem(string message, int answer)
            {
                Message = message;
                Answer = answer;
            }
        }


        static Problem[] GenerateRandomProblems(int numProblem)
        {
            Problem[] randomProblems = new Problem[numProblem];

            Random rnd = new Random();
            int x, y;

            for (int i = 0; i < numProblem; i++)
            {
                x = rnd.Next(50);
                y = rnd.Next(50);
                if (rnd.NextDouble() >= 0.5)
                    randomProblems[i] =
                    new Problem(String.Format("{0} + {1} = ?", x, y), x + y);
                else
                    randomProblems[i] =
                    new Problem(String.Format("{0} - {1} = ?", x, y), x - y);
            }

            return randomProblems;
        }



        static void Main(string[] args)
        {
            double score = 0;
            
            Difficulty Level = Difficulty.Easy|Difficulty.Normal|Difficulty.Hard;
            Level = Difficulty.Easy;
            Console.WriteLine("----------WELCOME TO THE GAME----------");
            Console.Write("Your Process is..... ");
            ShowStatus(score, Level);
            while (true) {
                double Qc = 0;
                double Qa = 0;
                long deltaT = 0;
                Console.Write("\n          Menu \n          (0)Play Game\n          (1)Setting\n          (2)Exit\n");
                int choose = int.Parse(Console.ReadLine());
                if (choose == 0)
                {
                    long startWait = DateTimeOffset.Now.ToUnixTimeSeconds();
                    bool stop = false;
                    Console.Write("Game will started in...");
                    while(true)
                    {
                        long endWait = DateTimeOffset.Now.ToUnixTimeSeconds();
                        long Wait = endWait - startWait;
                        if (Wait == 1&&stop == false) { Console.Write(" 3 ");stop = true; }
                        if (Wait == 2&&stop == true) { Console.Write("2 ");stop = false; }
                        if (Wait == 3&&stop == false) { Console.Write("1 ");stop = true; }
                        if (Wait == 4&&stop == true) { Console.WriteLine("0 ");stop = false; break; }
                        
                    }
                    int numberOfQuestion = 0;
                    Console.WriteLine("\n          Let's Fun Begin!!!!");
                    switch (Level)
                    {
                        case Difficulty.Easy:
                             numberOfQuestion = 3;
                             break;
                        case Difficulty.Normal:
                             numberOfQuestion = 5;
                             break;
                        case Difficulty.Hard:
                             numberOfQuestion = 7;
                            break;
                    }

                    
                    Problem[] QuestionInGame = GenerateRandomProblems(numberOfQuestion);
                    long startGame = DateTimeOffset.Now.ToUnixTimeSeconds();
                    for (int i = 0; i < numberOfQuestion; i++)
                    {
                        Console.WriteLine(QuestionInGame[i].Message);
                        Console.Write("Ans : ");
                        int Ans = int.Parse(Console.ReadLine());
                        if(QuestionInGame[i].Answer == Ans)
                        {
                            Qc += 1;
                        }

                    }
                    long endGame = DateTimeOffset.Now.ToUnixTimeSeconds();
                    deltaT = endGame - startGame;
                    Qa = numberOfQuestion;
                    //Console.WriteLine("Qa = {0} Qc = {1} deltaT = {2} Score = {3} Level = {4}",Qa,Qc,deltaT,score,(int)Level);
                    score = score + ((Qc / Qa) * ((25.00 - Power((double)Level, 2)) / Math.Max((double)deltaT, 25.00 - Power((double)Level, 2))) * (Power(2 * (double)Level + 1, 2)));
                    ShowStatus(score, Level);
                }

                else if(choose == 1){
                    Console.WriteLine("\n          Setting\n          (0)Easy\n          (1)Normal\n          (2)Hard");
                    ShowStatus(score, Level);
                    Console.Write("Please Select Your Difficulty :");
                    int numberLevel = int.Parse(Console.ReadLine());
                    Level = (Difficulty)numberLevel;
                    ShowStatus(score, Level);
                }
                else if(choose == 2)
                {
                    Console.WriteLine("See ya.");
                    break;
                }
                else
                {
                    Console.WriteLine("Please input 0-2.");
                }

            }
        }
        
        static void ShowStatus(double score,Difficulty level) 
        {
            Console.WriteLine("Your Score : {0} Your Difficulty : {1}", score, level);
        }
        static double Power(double number,int pow)
        {
            double numberPow = number;
            if (pow == 0) {
                number = 1;
            }
                for (int i = 1; i < pow; i++)
            {
                number = number * numberPow;
            }
            return number;
        }
    }
}
