using NUnit.Framework;

[TestFixture]
public class BucketTest {
    private Bucket bucket;
    [SetUp]
    public void Setup()
    {
        bucket = new Bucket(5);
    }

    [Test]
    public void AddOneDropToEmptyBucket_TotalDropsIs1()
    {
        bucket.AddDrop();
        Assert.AreEqual(1, bucket.Size);
        Assert.IsFalse(bucket.Full());
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
    }
}
