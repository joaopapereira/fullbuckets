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
        Assert.AreEqual(bucket.Size, 1);
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
        Assert.AreEqual(bucket.Size, 5);
        Assert.IsTrue(bucket.Full());

    }
}
