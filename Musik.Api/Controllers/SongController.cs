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
    }
}
