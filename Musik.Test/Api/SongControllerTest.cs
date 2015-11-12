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
        private SongController songController;
        private Repository<Song> repository;

        [SetUp]
        public void SetUp()
        {
            repository = new FakeRepository<Song>();
            var playlist = new Playlist(repository);
            
            songController = new SongController(playlist);
            songController.Request = new HttpRequestMessage();
            songController.Request.SetConfiguration(new HttpConfiguration());


            LoadRepository();
        }

        [Test]
        public void GetAllSongs()
        {
            var totalSongs = songController.Get();
            Assert.AreEqual(1, totalSongs.Count());
        }

        [Test]
        public void GetSingleSongSucess()
        {
            var httpStatus = songController.Get(1);

            Assert.AreEqual(HttpStatusCode.OK, httpStatus.StatusCode);
        }

        [Test]
        public void GetSingleSongFailure()
        {
            var httpStatus = songController.Get(2);

            Assert.AreEqual(HttpStatusCode.NotFound, httpStatus.StatusCode);
        }

        [Test]
        public void Post()
        {
            var song = repository.Get(m => m.Id == 1);
            song.Artist += repository.GetAll().Count();

            var httpStatus = songController.Post(song);

            Assert.AreEqual(HttpStatusCode.OK, httpStatus.StatusCode);

        }

        [Test]
        public void PostFailure()
        {

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
    }
}
