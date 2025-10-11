public abstract class AutoBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double CostBase { get; set; }
    public abstract double GetCost();

    public override string ToString()
    {
        string s = String.Format(
            "Ваш автомобиль: \n{0} \nОписание: {1} \nСтоимость {2}\n",
            Name,
            Description,
            GetCost()
        );
        return s;
    }
}

class Renault : AutoBase
{
    public Renault(string name, string info, double costbase)
    {
        Name = name;
        Description = info;
        CostBase = costbase;
    }

    public override double GetCost()
    {
        return CostBase * 1.18;
    }
}

class Toyota : AutoBase
{
    public Toyota(string name, string info, double costbase)
    {
        Name = name;
        Description = info;
        CostBase = costbase;
    }

    public override double GetCost()
    {
        return CostBase * 2.05;
    }
}

abstract class DecoratorOptions : AutoBase
{
    public AutoBase AutoProperty { protected get; set; }
    public string Title { get; set; }

    public DecoratorOptions(AutoBase au, string tit)
    {
        AutoProperty = au;
        Title = tit;
    }
}

class MediaNAV : DecoratorOptions
{
    public MediaNAV(AutoBase p, string t)
        : base(p, t)
    {
        AutoProperty = p;
        Name = p.Name + ". Современный";
        Description =
            p.Description
            + ". "
            + this.Title
            + ". Обновленная мультимедийная навигационная система";
    }

    public override double GetCost()
    {
        return AutoProperty.GetCost() + 15.99;
    }
}

class SystemSecurity : DecoratorOptions
{
    public SystemSecurity(AutoBase p, string t)
        : base(p, t)
    {
        AutoProperty = p;
        Name = p.Name + ". Повышенной безопасности";
        Description =
            p.Description
            + ". "
            + this.Title
            + ". Передние боковые подушки безопасности, ESP - система динамической стабилизации автомобиля";
    }

    public override double GetCost()
    {
        return AutoProperty.GetCost() + 20.99;
    }
}

class PressureSensors : DecoratorOptions
{
    public PressureSensors(AutoBase p, string t)
        : base(p, t)
    {
        AutoProperty = p;
        Name = p.Name + ". С датчиками давления в шинах";
        Description =
            p.Description
            + ". "
            + this.Title
            + ". Датчики давления в шинах, система обнаружения пониженного давления в шинах";
    }

    public override double GetCost()
    {
        return AutoProperty.GetCost() + 5.99;
    }
}

class CruiseСontrol : DecoratorOptions
{
    public CruiseСontrol(AutoBase p, string t)
        : base(p, t)
    {
        AutoProperty = p;
        Name = p.Name + ". С круизом-контролем";
        Description =
            p.Description
            + ". "
            + this.Title
            + ". Круиз-контроль — система автоматической поддержки скорости";
    }

    public override double GetCost()
    {
        return AutoProperty.GetCost() + 15.99;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Toyota toyota = new Toyota("Toyota", "Toyota Camry", 799.0);
        Print(toyota);
        AutoBase toyota1 = new PressureSensors(
            new MediaNAV(toyota, "Навигация"),
            "Датчики давления в шинах"
        );
        Print(toyota1);
        AutoBase toyota2 = new CruiseСontrol(new MediaNAV(toyota, "Навигация"), "Круиз-контроль");
        Print(toyota2);
    }

    private static void Print(AutoBase av)
    {
        Console.WriteLine(av.ToString());
    }
}
