using NUnit.Framework;

[TestFixture]
public class BucketTest {
    private Bucket bucket;
    private Game game;
    private Player player;

    [SetUp]
    public void Setup()
    {
        player = new PlayerImpl()
        {
            Drops = 10
        };
        game = new Game(player);
        bucket = new Bucket(5, game);
        
    }

    [Test]
    public void AddOneDropToEmptyBucket_TotalDropsIs1()
    {
        bucket.AddDrop();
        Assert.AreEqual(1, bucket.Size);
        Assert.IsFalse(bucket.Full());
        Assert.AreEqual(9, player.Drops);
    }

    [Test]
    public void AddFiveDropToEmptyBucket_BucketIsFull()
    {
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        Assert.AreEqual(5, bucket.Size);
        Assert.IsTrue(bucket.Full());
        Assert.AreEqual(5, player.Drops);
    }

    [Test]
    public void AddSixDropToEmptyBucket_BucketExplodeCallFunctionAndResetsDropCount()
    {
        bool called = false;
        bucket.SetExplosionCallback((Bucket bucket) => { called = true; return true; });
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        Assert.AreEqual(0, bucket.Size);
        Assert.IsFalse(bucket.Full());
        Assert.IsTrue(called);
        Assert.AreEqual(4, player.Drops);
    }

    [Test]
    public void NoDropsAndDropReachesBucket_ShouldNotAllowDropToLand()
    {
        Assert.IsFalse(bucket.DropLanded());
        Assert.AreEqual(0, bucket.Size);
        Assert.AreEqual(10, player.Drops);
    }

    [Test]
    public void OneDropAndDropReachesBucket_ShouldAllowDropToLandAndIncreaseBucketSize()
    {
        bucket.AddDrop();
        Assert.IsTrue(bucket.DropLanded());
        Assert.AreEqual(2, bucket.Size);
        Assert.AreEqual(9, player.Drops);
    }

    [Test]
    public void FiveDropsAndDropReachesBucket_ShouldAllowDropToLandAndBucketExplode()
    {
        bool called = false;
        bucket.SetExplosionCallback((Bucket bucket) => { called = true; return true; });
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        bucket.AddDrop();
        Assert.IsTrue(bucket.DropLanded());
        Assert.AreEqual(0, bucket.Size);
        Assert.IsFalse(bucket.Full());
        Assert.IsTrue(called);
        Assert.AreEqual(9, player.Drops);
    }
}
