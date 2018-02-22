using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicLibrary;
using System.Collections.Generic;
using MusicLibrary.Models;
using System;

namespace MusicLibrary.Tests
{
  [TestClass]
  public class TrackTests : IDisposable
  {
    public void Dispose()
    {
      Track.DeleteAll();
    }
    public TrackTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=music_library_test;";
    }
    [TestMethod]
    public void GetAll_DataBaseAtFirst_0()
    {
      //Arrange, Act
      int result = Track.GetAll().Count;
      Console.WriteLine("This is the number of items in the result list: " + result);

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Track testTrack = new Track("Track is wack","Art tisks","Al's bum","Jean-Ra",0);

      //Act
      testTrack.Save();
      Track savedTrack = Track.GetAll()[0];

      int result = savedTrack.GetId();
      int testId = testTrack.GetId();
      Console.WriteLine(result);
      Console.WriteLine(testId);
      //Assert
      Assert.AreEqual(testId, result);
    }



  }
}
