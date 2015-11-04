using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere.Core;
using NUnit.Framework;
using Musik.Core.Bussiness;
using Musik.Core.Data;

namespace Musik.Test
{
    [TestFixture]
    public class PlaylistTest
    {
        private Playlist playlist;

        [SetUp]
        public void SetUp()
        {
            playlist = new Playlist(new FakeRepository<Song>());
        }

        [Test]
        public void FindSong()
        {
            playlist.Add(GetDefaultSong());            
            var exists = IsSongInPlaylist(m=>m.Artist.Contains("Marron"));

            Assert.IsTrue(exists);
        }

        [Test]
        public void AddToPlaylist()
        {
            var totalBeforeAdd = playlist.GetAll().Count();

            playlist.Add(GetDefaultSong());            
            var totalAfterAdd = playlist.GetAll().Count();

            Assert.AreEqual(totalBeforeAdd + 1, totalAfterAdd);
        }

        [Test]
        public void UpdateSong()
        {
            playlist.Add(GetDefaultSong());
            var songFromPlaylist = playlist.GetAll().FirstOrDefault();

            songFromPlaylist.Artist += " Modified";

            playlist.Edit(songFromPlaylist);

            var exists = IsSongInPlaylist(m => m.Artist.Contains("Modified"));

            Assert.IsTrue(exists);
        }

        [Test]
        public void DeleteSong()
        {
            playlist.Add(GetDefaultSong());
            var totalBeforeDelete = playlist.GetAll().Count();

            playlist.Delete(m => !String.IsNullOrEmpty(m.Artist));

            var totalAfterDelete = playlist.GetAll().Count();

            Assert.AreEqual(totalBeforeDelete - 1, totalAfterDelete);
        }

        private Song GetDefaultSong()
        {
            var song = new Song()
            {
                Artist = "Marron 5",
                Title = "This Love",
                Album = "Songs About Jane",
            };

            return song;
        }

        private bool IsSongInPlaylist(Func<Song,bool> condition)
        {
            var songs = playlist.Find(condition);
            var exists = songs != null && songs.Count() > 0;

            return exists;
        }
    }
}
