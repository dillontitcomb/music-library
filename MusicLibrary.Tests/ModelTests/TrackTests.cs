using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicLibrary;
using System.Collections.Generic;
using MusicLibrary.Models;
using System;

namespace MusicLibrary.Tests
{
  [TestClass]
  public class TrackTests
  {
    // public void Dispose()
    // {
    //   Track.DeleteAll();
    // }
    public TrackTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=music_library;";
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
  }
}
