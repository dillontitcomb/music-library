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
        [HttpPost("/tracks")]
        public ActionResult Tracks()
        {
          string track = Request.Form["track"];
          string artist = Request.Form["artist"];
          string album = Request.Form["album"];
          string genre = Request.Form["genre"];
          Track newTrack = new Track(track, album, artist, genre);

          return View();
        }

    }
}
