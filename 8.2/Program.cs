
using System;

interface Money
{
    Money Add(Money money);
    Money Subtract(Money money);
    Money Multiply(int multiplier);
    Money Divide(int divisor);
    bool IsGreaterThan(Money money);
    bool IsLessThan(Money money);
    bool IsEqualTo(Money money);
}

abstract class BaseMoney : Money
{
    public int Rubs { get; set; }
    public int Kops { get; set; }

    public BaseMoney(int rubs, int kops)
    {
        Rubs = rubs;
        Kops = kops;
    }

    public abstract Money Add(Money money);
    public abstract Money Subtract(Money money);
    public abstract Money Multiply(int multiplier);
    public abstract Money Divide(int divisor);
    public abstract bool IsGreaterThan(Money money);
    public abstract bool IsLessThan(Money money);
    public abstract bool IsEqualTo(Money money);
}

class RussianRubles : BaseMoney
{
    public RussianRubles(int rubs, int kops) : base(rubs, kops)
    {
    }

    public override Money Add(Money money)
    {
        RussianRubles rubles = (RussianRubles)money;
        int totalRubs = Rubs + rubles.Rubs;
        int totalKops = Kops + rubles.Kops;
        if (totalKops >= 100)
        {
            totalRubs += totalKops / 100;
            totalKops %= 100;
        }
        return new RussianRubles(totalRubs, totalKops);
    }

    public override Money Subtract(Money money)
    {
        RussianRubles rubles = (RussianRubles)money;
        int totalRubs = Rubs - rubles.Rubs;
        int totalKops = Kops - rubles.Kops;
        if (totalKops < 0)
        {
            totalRubs--;
            totalKops += 100;
        }
        return new RussianRubles(totalRubs, totalKops);
    }

    public override Money Multiply(int multiplier)
    {
        int totalRubs = Rubs * multiplier;
        int totalKops = Kops * multiplier;
        if (totalKops >= 100)
        {
            totalRubs += totalKops / 100;
            totalKops %= 100;
        }
        return new RussianRubles(totalRubs, totalKops);
    }

    public override Money Divide(int divisor)
    {
        int totalRubs = Rubs / divisor;
        int totalKops = Kops / divisor;
        return new RussianRubles(totalRubs, totalKops);
    }

    public override bool IsGreaterThan(Money money)
    {
        RussianRubles rubles = (RussianRubles)money;
        if (Rubs > rubles.Rubs)
            return true;
        else if (Rubs == rubles.Rubs && Kops > rubles.Kops)
            return true;
        else
            return false;
    }

    public override bool IsLessThan(Money money)
    {
        RussianRubles rubles = (RussianRubles)money;
        if (Rubs < rubles.Rubs)
            return true;
        else if (Rubs == rubles.Rubs && Kops < rubles.Kops)
            return true;
        else
            return false;
    }

    public override bool IsEqualTo(Money money)
    {
        RussianRubles rubles = (RussianRubles)money;
        return Rubs == rubles.Rubs && Kops == rubles.Kops;
    }

    public override string ToString()
    {
        return $"{Rubs},{Kops}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Номиналы российских рублей могут принимать значения 1, 2, 5, 10, 50, 100, 500, 1000, 5000.");
        Console.WriteLine("Копейки представить как 0,01 (1 копейка), 0,05 (5 копеек), 0,1 (10 копеек), 0,5 (50 копеек).");
        Console.WriteLine("Введите первую сумму: ");
        string[] input1 = Console.ReadLine().Split(',');
        int rub = int.Parse(input1[0]);
        int kop = input1.Length > 1 ? int.Parse(input1[1]) : 0;

        Console.WriteLine("Введите вторую сумму: ");
        string[] input2 = Console.ReadLine().Split(',');
        int rub2 = int.Parse(input2[0]);
        int kop2 = input2.Length > 1 ? int.Parse(input2[1]) : 0;

        RussianRubles money1 = new RussianRubles(rub, kop);
        RussianRubles money2 = new RussianRubles(rub2, kop2);

        Console.WriteLine("Выберите операцию:");
        Console.WriteLine("1. Сложение");
        Console.WriteLine("2. Вычитание");
        Console.WriteLine("3. Умножение");
        Console.WriteLine("4. Деление");
        Console.WriteLine("5. Сравнение");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Результат сложения: " + money1.Add(money2));
                break;
            case 2:
                Console.WriteLine("Результат вычитания: " + money1.Subtract(money2));
                break;
            case 3:
                Console.WriteLine("Введите множитель:");
                int multiplier = int.Parse(Console.ReadLine());
                Console.WriteLine("Результат умножения: " + money1.Multiply(multiplier));
                break;
            case 4:
                Console.WriteLine("Введите делитель:");
                int divisor = int.Parse(Console.ReadLine());
                Console.WriteLine("Результат деления: " + money1.Divide(divisor));
                break;
            case 5:
                if (money1.IsGreaterThan(money2))
                    Console.WriteLine("Первая сумма больше второй");
                else if (money1.IsLessThan(money2))
                    Console.WriteLine("Первая сумма меньше второй");
                else
                    Console.WriteLine("Суммы равны");
                break;
            default:
                Console.WriteLine("Неверный выбор операции.");
                break;
        }
    }
}
