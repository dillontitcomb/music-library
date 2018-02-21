using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using MusicLibrary;

namespace MusicLibrary.Models
{
  public class Track
  {
    private int _id;
    private string _track;
    private string _artist;
    private string _album;
    private string _genre;

    public Track(string track, string artist, string album, string genre, int id = 0)
    {
      _id = id;
      _track = track;
      _artist = artist;
      _album = album;
      _genre = genre;
    }
    public static List<Track> GetAll()
    {
      List<Track> allTracks = new List<Track> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tracks;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string track = rdr.GetString(1);
        string artist = rdr.GetString(2);
        string album = rdr.GetString(3);
        string genre = rdr.GetString(4);
        Track newTrack = new Track(track, artist, album, genre, id);
        allTracks.Add(newTrack);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allTracks;
    }
  }
}
