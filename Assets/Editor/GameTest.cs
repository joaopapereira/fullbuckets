﻿using System;
using NUnit.Framework;

[TestFixture]
public class GameTest {

    public abstract class BaseTest
    {
        protected Game game;
        protected Player player;
        protected Bucket bucket;

        [SetUp]
        public void Setup()
        {
            player = new PlayerImpl();
            game = new Game(player);

            for(int i = 0; i < game.BoardSize; i++)
            {
                for(int j = 0; j < game.BoardSize; j++)
                {
                    bucket = new Bucket(5, game);
                    game.RegisterBucket(i, j, bucket);
                }
            }
            player.Drops = 0;
            SetUp();
        }

        protected abstract void SetUp();
    }

    public class WinCondition : BaseTest
    {
        protected override void SetUp()
        {
            bucket.Reset(3);
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

        [Test]
        public void NoDropsAndOneFlyingDrop_GameCannotEnd()
        {
            player.Drops = 1;
            game.CleanBoard();
            game.AddFlyingDrop();
            Assert.IsFalse(game.Ended(), "Game ended");
        }
    }
}
