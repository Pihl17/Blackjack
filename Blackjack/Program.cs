

internal class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        CardDeck deck = new CardDeck();
        
        while (deck.Count > 0)
        {
            Console.WriteLine(deck.DrawCard().rank);
        }
    }

}