abstract class Haircut
{
    public void TemplateMethod()
    {
        WashHair();
        SetupSeat();
        MakeHaircut();
        DryHair();
        CleanUp();
    }

    public void WashHair()
    {
        Console.WriteLine("Мытье головы");
    }

    public void SetupSeat()
    {
        Console.WriteLine("Настройка кресла");
    }

    public abstract void MakeHaircut();

    public void DryHair()
    {
        Console.WriteLine("Сушка волос");
    }

    public void CleanUp()
    {
        Console.WriteLine("Уборка");
    }
}

class ShortHaircut : Haircut
{
    public override void MakeHaircut()
    {
        Console.WriteLine("Стрижка волос с помощью машинки");
    }
}

class LongHaircut : Haircut
{
    public override void MakeHaircut()
    {
        Console.WriteLine("Стрижка волос с помощью ножниц");
    }
}

class Program
{
    public static void Main()
    {
        Haircut shortHaircut = new ShortHaircut();
        shortHaircut.TemplateMethod();
        Console.WriteLine("---");
        Haircut longHaircut = new LongHaircut();
        longHaircut.TemplateMethod();
    }
}
