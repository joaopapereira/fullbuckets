using NUnit.Framework;

public class BucketTest {
    public abstract class BaseBucketTest
    {
        protected Bucket bucket;
        protected Game game;
        protected Player player;

        [SetUp]
        public void Setup()
        {
            player = new PlayerImpl()
            {
                Drops = 10
            };
            game = new Game(player);
            bucket = new Bucket(5, game);

            SetUp();
        }
        protected abstract void SetUp();
    }
    public class ExplosionMock
    {
        private int called = 0;
        public bool ExplosionCallback(Bucket bucket)
        {
            called++;
            return true;
        }

        public bool HasBeenCalled()
        {
            return called > 0;
        }
    }

    [TestFixture]
    public class WhenBucketIsFull: BaseBucketTest
    {
        private ExplosionMock explosionMock;

        protected override void SetUp()
        {
            explosionMock = new ExplosionMock();
            bucket.SetExplosionCallback(explosionMock.ExplosionCallback);
            bucket.PlayerClick();
            bucket.PlayerClick();
            bucket.PlayerClick();
            bucket.PlayerClick();
            bucket.PlayerClick();
        }

        [Test]
        public void AddSixDropToEmptyBucket_BucketExplodeCallFunctionAndResetsDropCount()
        {
            bucket.PlayerClick();
            Assert.AreEqual(0, bucket.Size);
            Assert.IsFalse(bucket.Full());
            Assert.IsTrue(explosionMock.HasBeenCalled());
        }

        [Test]
        public void FiveDropsAndDropReachesBucket_ShouldAllowDropToLandAndBucketExplode()
        {
            Assert.IsTrue(bucket.DropLanded());
            Assert.AreEqual(0, bucket.Size);
            Assert.IsFalse(bucket.Full());
            Assert.IsTrue(explosionMock.HasBeenCalled());
        }
    }

    public class WhenUserClick: BaseBucketTest
    {
        protected override void SetUp()
        {

        }

        [Test]
        public void AddOneDropToEmptyBucket_TotalDropsIs1()
        {
            bucket.PlayerClick();
            Assert.AreEqual(1, bucket.Size);
            Assert.IsFalse(bucket.Full());
            Assert.AreEqual(9, player.Drops);
        }

        [Test]
        public void AddFiveDropToEmptyBucket_BucketIsFull()
        {
            bucket.PlayerClick();
            bucket.PlayerClick();
            bucket.PlayerClick();
            bucket.PlayerClick();
            bucket.PlayerClick();
            Assert.AreEqual(5, bucket.Size);
            Assert.IsTrue(bucket.Full());
            Assert.AreEqual(5, player.Drops);
        }
    }
    public class WhenDropReachBucket: BaseBucketTest
    {
        protected override void SetUp()
        {

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
            bucket.PlayerClick();
            Assert.IsTrue(bucket.DropLanded());
            Assert.AreEqual(2, bucket.Size);
            Assert.AreEqual(9, player.Drops);
        }
    }
}
