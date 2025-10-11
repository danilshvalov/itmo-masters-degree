abstract class CarFactory
{
    public abstract AbstractCar CreateCar();
    public abstract AbstractEngine CreateEngine();
    public abstract AbstractBody CreateBody();
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

abstract class AbstractBody
{
    public string name { get; set; }
}

class SedanBody : AbstractBody
{
    public SedanBody()
    {
        name = "Седан";
    }
}

class CrossoverBody : AbstractBody
{
    public CrossoverBody()
    {
        name = "Кроссовер";
    }
}

class FordFactory : CarFactory
{
    public override AbstractCar CreateCar()
    {
        return new FordCar("Форд");
    }

    public override AbstractEngine CreateEngine()
    {
        return new FordEngine();
    }

    public override AbstractBody CreateBody()
    {
        return new SedanBody();
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

class AudiFactory : CarFactory
{
    public override AbstractCar CreateCar()
    {
        return new AudiCar("Ауди");
    }

    public override AbstractEngine CreateEngine()
    {
        return new AudiEngine();
    }

    public override AbstractBody CreateBody()
    {
        return new CrossoverBody();
    }
}

class AudiCar : AbstractCar
{
    public AudiCar(string name)
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

class AudiEngine : AbstractEngine
{
    public AudiEngine()
    {
        max_speed = 300;
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
        abstractBody = car_factory.CreateBody();
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
        CarFactory ford_car = new FordFactory();
        Client c1 = new Client(ford_car);
        Console.WriteLine(
            "Максимальная скорость {0} составляет {1} км/час",
            c1.ToString(),
            c1.RunMaxSpeed()
        );

        CarFactory audi_car = new AudiFactory();
        Client c2 = new Client(audi_car);
        Console.WriteLine(
            "Максимальная скорость {0} составляет {1} км/час",
            c2.ToString(),
            c2.RunMaxSpeed()
        );
    }
}
