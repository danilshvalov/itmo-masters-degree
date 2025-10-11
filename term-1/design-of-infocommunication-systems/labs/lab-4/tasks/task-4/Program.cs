abstract class CarFactory
{
    public abstract AbstractCar CreateCar();
    public abstract AbstractEngine CreateEngine();
}

abstract class AbstractCar
{
    public string Name { get; set; }
    public abstract int MaxSpeed(AbstractEngine engine);
}

abstract class AbstractEngine
{
    public int max_speed { get; set; }
}

class FordFactory : CarFactory
{
    private FordFactory() { }

    static Lazy<CarFactory> factory = new Lazy<CarFactory>(() => new FordFactory());

    public static CarFactory Factory
    {
        get { return factory.Value; }
    }

    public override AbstractCar CreateCar()
    {
        return new FordCar("Форд");
    }

    public override AbstractEngine CreateEngine()
    {
        return new FordEngine();
    }
}

class FordCar : AbstractCar
{
    public FordCar(string name)
    {
        Name = name;
    }

    public override int MaxSpeed(AbstractEngine engine)
    {
        int ms = engine.max_speed;
        return ms;
    }

    public override string ToString()
    {
        return "Автомобиль " + Name;
    }
}

class FordEngine : AbstractEngine
{
    public FordEngine()
    {
        max_speed = 220;
    }
}

class Client
{
    private AbstractCar abstractCar;
    private AbstractEngine abstractEngine;

    public Client(CarFactory car_factory)
    {
        abstractCar = car_factory.CreateCar();
        abstractEngine = car_factory.CreateEngine();
    }

    public int RunMaxSpeed()
    {
        return abstractCar.MaxSpeed(abstractEngine);
    }

    public override string ToString()
    {
        return abstractCar.ToString();
    }
}

class Program
{
    public static void Main()
    {
        Client c1 = new Client(FordFactory.Factory);
        Console.WriteLine(
            "Максимальная скорость {0} составляет {1} км/час",
            c1.ToString(),
            c1.RunMaxSpeed()
        );
    }
}
