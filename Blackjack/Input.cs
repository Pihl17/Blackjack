using System;

public class Input
{

    public virtual ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey(true);
    }

}
