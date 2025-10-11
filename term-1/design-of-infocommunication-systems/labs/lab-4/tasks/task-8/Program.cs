class Receiver
{
    // банковские переводы
    public bool BankTransfer { get; set; }

    // денежные переводы - WesternUnion, Unistream
    public bool MoneyTransfer { get; set; }

    // перевод через PayPal
    public bool PayPalTransfer { get; set; }

    public Receiver(bool bt, bool mt, bool ppt)
    {
        BankTransfer = bt;
        MoneyTransfer = mt;
        PayPalTransfer = ppt;
    }
}

abstract class PaymentHandler
{
    public PaymentHandler Successor { get; set; }
    public abstract void Handle(Receiver receiver);
}

class BankPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.BankTransfer == true)
            Console.WriteLine("Выполняем банковский перевод");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class MoneyPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.MoneyTransfer == true)
            Console.WriteLine("Выполняем перевод через системы денежных переводов");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class PayPalPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.PayPalTransfer == true)
            Console.WriteLine("Выполняем перевод через PayPal");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class Program
{
    public static void Main()
    {
        Receiver receiver = new Receiver(false, false, true);
        PaymentHandler bankPaymentHandler = new BankPaymentHandler();
        PaymentHandler moneyPaymentHandler = new MoneyPaymentHandler();
        PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();
        moneyPaymentHandler.Successor = bankPaymentHandler;
        bankPaymentHandler.Successor = paypalPaymentHandler;
        moneyPaymentHandler.Handle(receiver);
    }
}
