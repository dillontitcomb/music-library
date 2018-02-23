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
          string genre = Request.Form["genre-dropdown"];
          Track newTrack = new Track(track, artist, album, genre);
          newTrack.Save();
          List<Track> trackList = Track.GetAll();

          return View("Tracks", trackList);
        }
        [HttpPost("/tracks/sort")]
        public ActionResult SortTracks()
        {
          string sort = Request.Form["sort"];
          List<Track> trackList = Track.GetAllSorted(sort);
          return View("Tracks", trackList);
        }

        [HttpGet("/tracks/{id}")]
        public ActionResult FindTracks(int id)
        {
          Track newTrack = Track.Find(id);
          return View(newTrack);
        }

        [HttpGet("/tracks/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
          Track thisTrack = Track.Find(id);
          return View(thisTrack);
        }

        [HttpPost("/tracks/{id}/update")]
        public ActionResult Update(int id)
        {
          Track thisTrack = Track.Find(id);
          thisTrack.Edit(Request.Form["updateTrack"], Request.Form["updateArtist"], Request.Form["updateAlbum"], Request.Form["updateGenre"]);
          return RedirectToAction("Tracks");
        }

        [HttpPost("/tracks/delete")]
        public ActionResult DeleteAllTracks()
        {
          Track.DeleteAll();
          return View("Index");
        }

    }
}
