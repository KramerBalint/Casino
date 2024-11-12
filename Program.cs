using System;

namespace Casino
{
    internal class Program
    {
        static int balance = 1000;
        static void Main(string[] args)
        {
            Console.WriteLine("Mit szeretnél játszani?");
            Console.WriteLine("1. BlackJack.");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    BlackJack(ref balance);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        static void BlackJack(ref int money)
        {
            bool fut = true;
            List<(int, string)> cards = new List<(int, string)>
            {
                (2, "2"), (3, "3"), (4, "4"), (5, "5"), (6, "6"),
                (7, "7"), (8, "8"), (9, "9"), (10, "10"),
                (10, "Jumbo"), (10, "Dáma"), (10, "Király"), (11, "Ász")
            };

            Random rnd = new Random();

            while (fut && money > 0)
            {
                List<int> Dcards = new List<int>();
                List<int> Mcards = new List<int>();

                var dealerCard1 = cards[rnd.Next(cards.Count)];
                var dealerCard2 = cards[rnd.Next(cards.Count)];
                Dcards.Add(dealerCard1.Item1);
                Dcards.Add(dealerCard2.Item1);

                var playerCard1 = cards[rnd.Next(cards.Count)];
                var playerCard2 = cards[rnd.Next(cards.Count)];
                Mcards.Add(playerCard1.Item1);
                Mcards.Add(playerCard2.Item1);

                int sumD = Dcards.Sum();
                int sumM = Mcards.Sum();

                Console.WriteLine($"Osztó kártyái: {dealerCard1.Item2}, {dealerCard2.Item2} - összérték: {sumD}");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine($"Te kártyáid: {playerCard1.Item2}, {playerCard2.Item2} - összérték: {sumM}");
                Console.WriteLine();

                if (sumD == 21)
                {
                    Console.WriteLine("Az osztónak blackjackje van.");
                    money -= 100;
                }

                bool playerTurn = true;
                while (playerTurn && sumM < 21)
                {
                    Console.WriteLine("1. Hit, 2. Stand");
                    int valasztas = Convert.ToInt32(Console.ReadLine());

                    if (valasztas == 1)
                    {
                        var newCard = cards[rnd.Next(cards.Count)];
                        Mcards.Add(newCard.Item1);
                        sumM = Mcards.Sum();
                        Console.WriteLine($"Új kártya: {newCard.Item2}, összértéked: {sumM}");

                        if (sumM > 21)
                        {
                            money -= 100;
                            Console.WriteLine("Buktál! Az összértéked meghaladja a 21-et. Pénzed:" + money);
                            playerTurn = false;
                        }
                    }
                    else if (valasztas == 2)
                    {
                        playerTurn = false;
                    }
                }

                if (sumM <= 21)
                {
                    while (sumD < 17)
                    {
                        var newCard = cards[rnd.Next(cards.Count)];
                        Dcards.Add(newCard.Item1);
                        sumD = Dcards.Sum();
                        Console.WriteLine($"Az osztó új kártyát húz: {newCard.Item2}, összértéke: {sumD}");
                    }

                    if (sumD > 21 || sumM > sumD)
                    {
                        money += 100;
                        Console.WriteLine("Nyertél! Pénzed:" + money);
                    }
                    else if (sumM < sumD)
                    {
                        money -= 100;
                        Console.WriteLine("Az osztó nyert! Pézned:" + money);
                    }
                    else
                    {
                        Console.WriteLine("Döntetlen! Pénzed:" + money);
                    }
                }

                Console.WriteLine("Szeretnél új játékot játszani? (i/n)");
                string again = Console.ReadLine().ToLower();
                if (again == "i")
                {
                    fut = true;
                }
                else
                {
                    fut = false;
                }
            }

            Console.WriteLine("Játék vége. Egyenleged: " + money);
        }
    }
}
