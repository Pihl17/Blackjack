namespace Tests;

[TestClass]
public class TableTest
{

    [TestMethod]
    public void GetCurrent_InsantiatesAndReturnsSingleton()
    {
        var result = Table.Current;
        Assert.IsInstanceOfType<Table>(result);
    }
    
    [TestMethod]
    public void StartRound_DealsOutStartHands()
    {
        Assert.Fail();
    }
}
