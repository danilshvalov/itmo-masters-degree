abstract class Command
{
    protected ArithmeticUnit unit;
    protected double operand;
    public abstract void Execute();
    public abstract void UnExecute();
}

class ArithmeticUnit
{
    public double Register { get; private set; }

    public void Run(char operationCode, double operand)
    {
        switch (operationCode)
        {
            case '+':
                Register += operand;
                break;
            case '-':
                Register -= operand;
                break;
            case '*':
                Register *= operand;
                break;
            case '/':
                Register /= operand;
                break;
        }
    }
}

class ControlUnit
{
    private List<Command> commands = new List<Command>();
    private int current = 0;

    public void StoreCommand(Command command)
    {
        commands.Add(command);
    }

    public void ExecuteCommand()
    {
        commands[current].Execute();
        current++;
    }

    public void Undo()
    {
        Undo(1);
    }

    public void Undo(int count)
    {
        for (int i = 1; i <= count; ++i)
        {
            commands[current - i].UnExecute();
        }
    }

    public void Redo()
    {
        Redo(1);
    }

    public void Redo(int count)
    {
        for (int i = count; i >= 1; --i)
        {
            commands[current - i].Execute();
        }
    }
}

class Add : Command
{
    public Add(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('+', operand);
    }

    public override void UnExecute()
    {
        unit.Run('-', operand);
    }
}

class Sub : Command
{
    public Sub(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('-', operand);
    }

    public override void UnExecute()
    {
        unit.Run('+', operand);
    }
}

class Mul : Command
{
    public Mul(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('*', operand);
    }

    public override void UnExecute()
    {
        unit.Run('/', operand);
    }
}

class Div : Command
{
    public Div(ArithmeticUnit unit, double operand)
    {
        this.unit = unit;
        this.operand = operand;
    }

    public override void Execute()
    {
        unit.Run('/', operand);
    }

    public override void UnExecute()
    {
        unit.Run('*', operand);
    }
}

class Calculator
{
    ArithmeticUnit arithmeticUnit;
    ControlUnit controlUnit;

    public Calculator()
    {
        arithmeticUnit = new ArithmeticUnit();
        controlUnit = new ControlUnit();
    }

    private double Run(Command command)
    {
        controlUnit.StoreCommand(command);
        controlUnit.ExecuteCommand();
        return arithmeticUnit.Register;
    }

    public double Add(double operand)
    {
        return Run(new Add(arithmeticUnit, operand));
    }

    public double Sub(double operand)
    {
        return Run(new Sub(arithmeticUnit, operand));
    }

    public double Mul(double operand)
    {
        return Run(new Mul(arithmeticUnit, operand));
    }

    public double Div(double operand)
    {
        return Run(new Div(arithmeticUnit, operand));
    }

    public double Undo(int count)
    {
        controlUnit.Undo(count);
        return arithmeticUnit.Register;
    }

    public double Undo()
    {
        return Undo(1);
    }

    public double Redo(int count)
    {
        controlUnit.Redo(count);
        return arithmeticUnit.Register;
    }

    public double Redo()
    {
        return Redo(1);
    }
}

class Program
{
    public static void Main()
    {
        var calculator = new Calculator();
        double result = 0;
        result = calculator.Add(5);
        Console.WriteLine(result);
        result = calculator.Sub(3);
        Console.WriteLine(result);
        result = calculator.Mul(6);
        Console.WriteLine(result);
        result = calculator.Undo(2);
        Console.WriteLine(result);
        result = calculator.Redo(2);
        Console.WriteLine(result);
    }
}
