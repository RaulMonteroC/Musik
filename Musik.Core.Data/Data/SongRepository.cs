using Musik.Core.Portable.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musik.Core.Data.Data
{
    public class SongRepository
    {
        private ApiConnector connecor;
        private string baseUrl;

        public SongRepository()
        {
            connecor = new ApiConnector();
            baseUrl = "httP://localhost:8085/";
        }

        public IEnumerable<Song> GetAll()
        {
            var url = baseUrl + "api/song";
            var songs = connecor.Get<IEnumerable<Song>>(url);

            return songs;
        }
    }
}
