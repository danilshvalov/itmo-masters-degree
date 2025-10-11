abstract class TransportService
{
    public string Name { get; set; }

    public TransportService(string name)
    {
        Name = name;
    }

    public abstract double CostTransportation(double distance);
}

abstract class TransportCompany
{
    public string Name { get; set; }

    public TransportCompany(string n)
    {
        Name = n;
    }

    public override string ToString()
    {
        return Name;
    }

    // фабричный метод
    abstract public TransportService Create(string n, int k);
}

class TaxiServices : TransportService
{
    public int Category { get; set; }

    public TaxiServices(string name, int cat)
        : base(name)
    {
        Category = cat;
    }

    public override double CostTransportation(double distance)
    {
        return distance * Category;
    }

    public override string ToString()
    {
        string s = String.Format("Фирма {0}, поездка категории {1}", Name, Category);
        return s;
    }
}

class Shipping : TransportService
{
    public double Tariff { get; set; }

    public Shipping(string name, int taff)
        : base(name)
    {
        Tariff = taff;
    }

    public override double CostTransportation(double distance)
    {
        return distance * Tariff;
    }

    public override string ToString()
    {
        string s = String.Format("Фирма {0}, доставка по тарифу {1}", Name, Tariff);
        return s;
    }
}

class SoberDriver : TransportService
{
    public int Category { get; set; }

    public SoberDriver(string name, int category)
        : base(name)
    {
        Category = category;
    }

    public override double CostTransportation(double distance)
    {
        return distance * (Category + 1);
    }

    public override string ToString()
    {
        string s = String.Format(
            "Фирма {0}, услуга трезвый водитель категории {1}",
            Name,
            Category
        );
        return s;
    }
}

class TaxiTransCom : TransportCompany
{
    public TaxiTransCom(string name)
        : base(name) { }

    public override TransportService Create(string n, int c)
    {
        return new TaxiServices(Name, c);
    }
}

class ShipTransCom : TransportCompany
{
    public ShipTransCom(string name)
        : base(name) { }

    public override TransportService Create(string n, int t)
    {
        return new Shipping(Name, t);
    }
}

class SoberDriverCom : TransportCompany
{
    public SoberDriverCom(string name)
        : base(name) { }

    public override TransportService Create(string n, int t)
    {
        return new SoberDriver(Name, t);
    }
}

class Program
{
    public static void Main()
    {
        TransportCompany trCom = new TaxiTransCom("Служба такси");
        TransportService compService = trCom.Create("Такси", 1);
        double dist = 15.5;
        Print(compService, dist);

        TransportCompany gCom = new ShipTransCom("Служба перевозок");
        compService = gCom.Create("Грузоперевозки", 2);
        double distg = 150.5;
        Print(compService, distg);

        TransportCompany sCom = new SoberDriverCom("Служба трезвых водителей");
        compService = sCom.Create("Трезвый водитель", 3);
        double dists = 12.1;
        Print(compService, dists);
    }

    private static void Print(TransportService compTax, double distg)
    {
        Console.WriteLine(
            "Компания {0}, расстояние {1}, стоимость: {2}",
            compTax.ToString(),
            distg,
            compTax.CostTransportation(distg)
        );
    }
}
