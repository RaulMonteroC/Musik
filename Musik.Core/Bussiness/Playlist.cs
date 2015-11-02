using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere.Core;
using Musik.Core.Data;

namespace Musik.Core.Bussiness
{
    public class Playlist
    {
        public Repository<Song> SongRepository { get; set; }

        public Playlist(Repository<Song> songRepository)
        {
            this.SongRepository = songRepository;
        }

        public void Add(Song song)
        {
            if (song != null)
            {
                SongRepository.Add(song);
            }
        }

        public void Edit(Song song)
        {
            if(song != null)
            {
                SongRepository.Update(song);
            }
        }

        public void Delete(Func<Song,bool> condition)
        {
            SongRepository.Delete(condition);
        }

        public IEnumerable<Song> Find(Func<Song,bool> condition)
        {
            return SongRepository.Find(condition);
        }

        public IEnumerable<Song> GetAll()
        {
            return SongRepository.GetAll();
        }
    }
}
