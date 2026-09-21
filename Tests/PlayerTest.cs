using Moq;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public sealed class PlayerTest
    {
        [TestMethod]
        public void Hit_AddsCardToHand()
        {
            List<Card> expected = new List<Card>() { new Card(1) };
            Player player = new Player();

            player.Hit(new Card(1));

            CollectionAssert.AreEqual(expected, player.hand);
        }

        [TestMethod]
        [DoNotParallelize]
        [DataRow("K A", 13, 1)]
        [DataRow("J Q", 11, 12)]
        [DataRow("10 6", 10, 6)]
        [DataRow("9 J", 9, 11)]
        public void PrintHand_PrintsOutThePlayersCurrentHand(string expected, params int[] cardRanks)
        {
            Player player = new Player();
            for (int i = 0; i < cardRanks.Length; i++)
                player.hand.Add(new Card(cardRanks[i]));
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            player.PrintHand();

            string result = stringWriter.ToString().Trim();
            Assert.Contains(expected, result);
        }

        [TestMethod]
        [DoNotParallelize]
        [DataRow("21", 1, 13)]
        [DataRow("17", 11, 3, 4)]
        [DataRow("Bust", 10, 11, 12)]
        public void PrintCurrentHandScore_PrintsScore(string expected, params int[] cardRanks)
        {
            Player player = new Player();
            for (int i = 0; i < cardRanks.Length; i++)
                player.hand.Add(new Card(cardRanks[i]));
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            player.PrintCurrentHandScore();

            string result = stringWriter.ToString().Trim();
            Assert.Contains(expected, result);
        }

    }
}
