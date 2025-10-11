abstract class RouteStrategy
{
    public abstract void FindRoute();
};

class AutoRouteStrategy : RouteStrategy
{
    public override void FindRoute()
    {
        Console.WriteLine("Поиск автомобильного маршрута");
    }
};

class FootRouteStrategy : RouteStrategy
{
    public override void FindRoute()
    {
        Console.WriteLine("Поиск пешего маршрута");
    }
};

class CycleRouteStrategy : RouteStrategy
{
    public override void FindRoute()
    {
        Console.WriteLine("Поиск велосипедного маршрута");
    }
};

class PublicTransportRouteStrategy : RouteStrategy
{
    public override void FindRoute()
    {
        Console.WriteLine("Поиск маршрута на общественном транспорте");
    }
};

class AttractionRouteStrategy : RouteStrategy
{
    public override void FindRoute()
    {
        Console.WriteLine("Поиск маршрута с достопримечательностями");
    }
};

class Navigator
{
    public void ShowMap()
    {
        Console.WriteLine("Показ карты");
    }

    public void Search()
    {
        Console.WriteLine("Поиск точек");
    }

    public void FindRoute(RouteStrategy stragegy)
    {
        stragegy.FindRoute();
    }
}

class Program
{
    public static void Main()
    {
        Navigator navigator = new Navigator();
        navigator.ShowMap();
        navigator.Search();
        navigator.FindRoute(new AutoRouteStrategy());
        navigator.FindRoute(new FootRouteStrategy());
        navigator.FindRoute(new PublicTransportRouteStrategy());
        navigator.FindRoute(new AttractionRouteStrategy());
    }
}
