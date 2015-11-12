using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere.Core;
using Musik.Core.Data;
using System.Reflection;

namespace Musik.Core.Bussiness
{
    public class Playlist
    {
        private Repository<Song> songRepository;

        public Playlist(Repository<Song> songRepository)
        {
            this.songRepository = songRepository;
        }

        public void Add(Song song)
        {
            if (song != null)
            {
                songRepository.Add(song);
            }
        }

        public void Edit(Song song)
        {
            if(song != null)
            {
                songRepository.Update(song);
            }
        }

        public void Delete(Func<Song,bool> condition)
        {
            songRepository.Delete(condition);
        }

        public IEnumerable<Song> Find(Func<Song,bool> condition)
        {
            return songRepository.Find(condition);
        }

        public IEnumerable<Song> GetAll()
        {
            return songRepository.GetAll();
        }
    }
}
