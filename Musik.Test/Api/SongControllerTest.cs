using Musik.Api.Controllers;
using Musik.Core.Bussiness;
using Musik.Core.Data;
using NUnit.Framework;
using Sphere.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Musik.Test.Api
{
    [TestFixture]
    public class SongControllerTest
    {
        private SongController controller;
        private Repository<Song> repository;
        private Playlist playlist;

        [SetUp]
        public void SetUp()
        {
            repository = new FakeRepository<Song>();
            playlist = new Playlist(repository);
            
            controller = new SongController(playlist);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());


            LoadRepository();
        }

        [Test]
        public void GetAllSongs()
        {
            var totalSongs = controller.Get();
            Assert.AreEqual(1, totalSongs.Count());
        }

        [Test]
        public void GetSingleSongSucess()
        {
            var httpStatus = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.OK, httpStatus.StatusCode);
        }

        [Test]
        public void GetSingleSongFailure()
        {
            var httpStatus = controller.Get(2);

            Assert.AreEqual(HttpStatusCode.NotFound, httpStatus.StatusCode);
        }

        [Test]
        public void Post()
        {
            var totalSongsBefore = playlist.GetAll().Count();
            var song = repository.Get(m => m.Id == 1);     
            song.Id = totalSongsBefore;

            var httpStatus = controller.Post(song);
            var totalSongsAfter = playlist.GetAll().Count();

            FailIfNull(m => controller.Post(m));
            FailIfMissingFields(m => controller.Post(m));
            Assert.AreEqual(HttpStatusCode.OK, httpStatus.StatusCode);
            Assert.AreEqual(totalSongsBefore + 1, totalSongsAfter);
        }

        [Test]
        public void Delete()
        {
            var totalSongsBefore = playlist.GetAll().Count();

            var httpStatus = controller.Delete(1);
            var totalSongsAfter = playlist.GetAll().Count();

            FailIfNotFound(m => controller.Delete(m));
            Assert.AreEqual(HttpStatusCode.OK, httpStatus.StatusCode);
            Assert.AreEqual(totalSongsBefore - 1, totalSongsAfter);
        }

        [Test]
        public void Patch()
        {
            var totalSongsBefore = playlist.GetAll().Count();
            var song = repository.Get(m => m.Id == 1);            
            song.Artist += totalSongsBefore;

            var httpStatus = controller.Patch(song,1);
            var songAfter = repository.Get(m => m.Id == 1);

            FailIfNull(m => controller.Patch(m,1));
            FailIfMissingFields(m => controller.Patch(m,1));
            FailIfNotFound(m => controller.Patch(song, m));
            Assert.AreEqual(HttpStatusCode.OK, httpStatus.StatusCode);
            Assert.AreEqual(song.Artist, songAfter.Artist);
        }

        private void LoadRepository()
        {
            repository.Add(new Song()
            {
                Id = 1,
                Artist = "Test Artist",
                Album = "Test Album",
                Title = "Test Title",
                VideoUrl = "http://youtube.com/watch?v=cyWYC6X1X1s"
            });
        }

        private void FailIfNull(Func<Song,HttpResponseMessage> method)
        {
            Song nullSong = null;
            var httpStatus = method(nullSong);

            Assert.AreEqual(HttpStatusCode.BadRequest, httpStatus.StatusCode);
        }

        private void FailIfMissingFields(Func<Song, HttpResponseMessage> method)
        {
            Song emptySong = new Song();
            var httpStatus = method(emptySong);

            Assert.AreEqual(HttpStatusCode.BadRequest, httpStatus.StatusCode);
        }

        public void FailIfNotFound(Func<int, HttpResponseMessage> method)
        {
            var httpStatus = method(5);

            Assert.AreEqual(HttpStatusCode.NotFound, httpStatus.StatusCode);
        }
    }
}
