using NUnit.Framework;

[TestFixture]
public class GameTest {
    private Game game;
    private Player player;

    [SetUp]
    public void Setup()
    {
        player = new PlayerImpl();
        game = new Game(player);
        player.Drops = 0;
    }

    [Test]
    public void NoMoreDropsButBoardNotClear_GameEndWithPlayerLost()
    {
        Assert.IsTrue(game.Ended(), "Game ended");
        Assert.IsFalse(game.Winner(), "The player did not win");
    }

    [Test]
    public void NoMoreDropsAndoardIsClear_GameEndWithPlayerWinner()
    {
        game.CleanBoard();
        Assert.IsTrue(game.Ended(), "Game ended");
        Assert.IsTrue(game.Winner(), "The player did not win");
    }

    [Test]
    public void StillHaveDropsAndBoardNotClear_GameContinue()
    {
        player.Drops = 1;
        Assert.IsFalse(game.Ended(), "Game ended");
        Assert.IsFalse(game.Winner(), "The player did not win");
    }
}
