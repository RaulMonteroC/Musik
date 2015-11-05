using Musik.Core.Bussiness;
using Musik.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Musik.Api.Controllers
{
    public class SongController : ApiController
    {
        private Playlist playlist;

        public SongController()
        {
            playlist = new Playlist();
        }

        public IEnumerable<Song> Get()
        {
            return playlist.GetAll();
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage message = null;
            var song = playlist.Find(m => m.Id == id).FirstOrDefault();

            if (song == null) 
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No song was found with the given id");
            else
                message = Request.CreateResponse(HttpStatusCode.OK, song);

            return message;
        }

        public HttpResponseMessage Post([FromBody] Song song)
        {
            HttpResponseMessage message = null;

            if (song == null)
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can't save an empty or null song");
            else if (!IsValid(song)) 
            {
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Artist, Title and Album are required fields");
            }
            else
            {
                playlist.Add(song);
                
                message = Request.CreateResponse(HttpStatusCode.OK,"Song added sucessfully");
            }

            return message;
        }

        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage message = null;
            var song = playlist.Find(m => m.Id == id).FirstOrDefault();

            if (song == null)
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No song was found with the given id");
            else
            {
                playlist.Delete(m => m.Id == id);
                message = Request.CreateResponse(HttpStatusCode.OK, "Message deleted successfully");
            }

            return message;
        }

        [HttpPut]
        [HttpPatch]
        public HttpResponseMessage Patch([FromBody] Song song, int id)
        {
            HttpResponseMessage message = null;
            var dbSong = playlist.Find(m => m.Id == id).FirstOrDefault();

            if (song == null)
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Can't update an empty or null song");
            else if (!IsValid(song))
            {
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Artist, Title and Album can't be empty or null");
            }
            else if(dbSong == null)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No song was found with the given id");
            }
            else
            {
                dbSong.Id = id;
                dbSong.Album = song.Album;
                dbSong.Artist = song.Artist;
                dbSong.Title = song.Title;
                dbSong.VideoUrl = song.VideoUrl;

                playlist.Edit(dbSong);

                message = Request.CreateResponse(HttpStatusCode.OK, "Song updated sucessfully");
            }

            return message;
        }
        
        private bool IsValid(Song song)
        {
            bool isValid = false;

            if(song == null) return isValid;

            var isArtistEmpty = String.IsNullOrEmpty(song.Artist);
            var isTitleEmpty = String.IsNullOrEmpty(song.Title);
            var isAlbumEmpty = String.IsNullOrEmpty(song.Album);            

            isValid = !(isAlbumEmpty && isArtistEmpty && isTitleEmpty);

            return isValid;
        }
    }
}
