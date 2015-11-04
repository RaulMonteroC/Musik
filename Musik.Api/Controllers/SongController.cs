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
    }
}
