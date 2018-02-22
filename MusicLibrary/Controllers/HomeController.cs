using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Models;
using System.Collections.Generic;

namespace MusicLibrary.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
          return View();
        }
        [HttpGet("/tracks")]
        public ActionResult Tracks()
        {
          List<Track> trackList = Track.GetAll();
          return View(trackList);
        }

        [HttpPost("/tracks")]
        public ActionResult AddTracks()
        {
          string track = Request.Form["track"];
          string artist = Request.Form["artist"];
          string album = Request.Form["album"];
          string genre = Request.Form["genre"];
          Track newTrack = new Track(track, artist, album, genre);
          newTrack.Save();
          List<Track> trackList = Track.GetAll();

          return View("Tracks", trackList);
        }
        [HttpGet("/tracks/{id}")]
        public ActionResult FindTracks(int id)
        



        [HttpPost("/tracks/delete")]
        public ActionResult DeleteAllTracks()
        {
          Track.DeleteAll();
          return View("Index");
        }

    }
}
