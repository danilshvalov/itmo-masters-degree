interface ICelsiusTemperatureSensor
{
    double GetValue();
}

class FahrenheitTemperatureSensor
{
    Random random;

    public FahrenheitTemperatureSensor()
    {
        random = new Random();
    }

    public double GetValue()
    {
        // Случайное число от 32 до 95.
        return random.Next(95) + 32;
    }
}

class AdapterFahrenheitTemperatureSensor : ICelsiusTemperatureSensor
{
    FahrenheitTemperatureSensor sensor;

    public AdapterFahrenheitTemperatureSensor()
    {
        sensor = new FahrenheitTemperatureSensor();
    }

    public double GetValue()
    {
        return (sensor.GetValue() - 32) * 5.0 / 9.0;
    }
}

class Controller
{
    ICelsiusTemperatureSensor sensor;

    public Controller()
    {
        sensor = new AdapterFahrenheitTemperatureSensor();
    }

    public void CheckSensor()
    {
        Console.WriteLine("Сенсор показал {0:f2} градусов", sensor.GetValue());
    }
}

class Program
{
    static void Main(string[] args)
    {
        Controller controller = new Controller();
        controller.CheckSensor();
    }
}
