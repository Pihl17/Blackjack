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
        public void PrintHand_PrintsOutThePlayersCurrentHand()
        {
            string expected = "Your current hand is: 13 1";
            Player player = new Player() { 
                hand = new List<Card>() { 
                    new Card(13), 
                    new Card(1) 
                } 
            };
            Console.WriteLine();
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            player.PrintHand();

            string result = stringWriter.ToString().Trim();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Hit_AutoEndsTurnOnBust()
        {
            Assert.Fail();
        }

    }
}
