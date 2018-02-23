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

    public override bool Equals(System.Object otherTrack)
    {
    if (!(otherTrack is Track))
    {
    return false;
    }
    else
    {
        Track newTrack = (Track) otherTrack;
        bool idEquality = (this.GetId() == newTrack.GetId());
        bool trackEquality = (this.GetTrack() == newTrack.GetTrack());
        return (idEquality && trackEquality);
      }
    }

    public string GetTrack()
    {
      return _track;
    }

    public string GetArtist()
    {
      return _artist;
    }

    public string GetAlbum()
    {
      return _album;
    }

    public string GetGenre()
    {
      return _genre;
    }

    public int GetId()
    {
      return _id;
    }

    public void Edit(string newTrack, string newArtist, string newAlbum, string newGenre)
    {
      MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"UPDATE tracks SET track = @trackTrack, artist = @trackArtist, album = @trackAlbum, genre = @trackGenre WHERE id = @trackId;";

       MySqlParameter track = new MySqlParameter();
       track.ParameterName = "@trackTrack";
       track.Value = newTrack;
       cmd.Parameters.Add(track);

       MySqlParameter artist = new MySqlParameter();
       artist.ParameterName = "@trackArtist";
       artist.Value = newArtist;
       cmd.Parameters.Add(artist);

       MySqlParameter album = new MySqlParameter();
       album.ParameterName = "@trackAlbum";
       album.Value = newAlbum;
       cmd.Parameters.Add(album);

       MySqlParameter genre = new MySqlParameter();
       genre.ParameterName = "@trackGenre";
       genre.Value = newGenre;
       cmd.Parameters.Add(genre);

       MySqlParameter trackId = new MySqlParameter();
       trackId.ParameterName = "@trackId";
       trackId.Value = _id;
       cmd.Parameters.Add(trackId);

       cmd.ExecuteNonQuery();
       _track = newTrack;
       _artist = newArtist;
       _album = newAlbum;
       _genre = newGenre;

       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }
    }

    public static Track Find(int id)
    {
      MySqlConnection conn = DB.Connection();
           conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tracks WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
           thisId.ParameterName = "@thisId";
           thisId.Value = id;
           cmd.Parameters.Add(thisId);

           var rdr = cmd.ExecuteReader() as MySqlDataReader;

           int trackId = 0;
           string trackTrack = "";
           string trackArtist = "";
           string trackAlbum = "";
           string trackGenre = "";

           while (rdr.Read())
           {
               trackId = rdr.GetInt32(0);
               trackTrack = rdr.GetString(1);
               trackArtist = rdr.GetString(2);
               trackAlbum= rdr.GetString(3);
               trackGenre = rdr.GetString(4);
           }

           Track foundTrack= new Track(trackTrack, trackArtist, trackAlbum, trackGenre, trackId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

           return foundTrack;

       }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"DELETE FROM tracks;";

           cmd.ExecuteNonQuery();

           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
      }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"Insert INTO tracks (track, artist, album, genre) VALUES (@trackTrack, @trackArtist, @trackAlbum, @trackGenre);";

      MySqlParameter track = new MySqlParameter();
      track.ParameterName = "@trackTrack";
      track.Value = this._track;
      cmd.Parameters.Add(track);

      MySqlParameter artist = new MySqlParameter();
      artist.ParameterName = "@trackArtist";
      artist.Value = this._artist;
      cmd.Parameters.Add(artist);

      MySqlParameter album = new MySqlParameter();
      album.ParameterName = "@trackAlbum";
      album.Value = this._album;
      cmd.Parameters.Add(album);

      MySqlParameter genre = new MySqlParameter();
      genre.ParameterName = "@trackGenre";
      genre.Value = this._genre;
      cmd.Parameters.Add(genre);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        {
          conn.Dispose();
        }

    }
    public static List<Track> GetAll()
    {
      List<Track> allTracks = new List<Track> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tracks ORDER BY track ASC;";
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

      public static List<Track> GetAllSorted(string sortType)
      {
        List<Track> allTracks = new List<Track> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM tracks ORDER BY " + sortType + " ASC;";
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
