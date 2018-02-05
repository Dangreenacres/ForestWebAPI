using Forest.Data;
using Forest.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ForestWebAPI.Controllers
{
    public class MusicController : ApiController
    {
        public MusicService _musicService;
        // GET: Application
        public MusicController()
        {
            _musicService = new MusicService();
        }

        // GET: api/Music
        public HttpResponseMessage GetMusic()
        {
            IEnumerable<Music_Recording> recordings = _musicService.GetMusicRecordings();
            if (recordings == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, recordings);
                return response;
            }
        }

        //GET: api/Music/1
        public HttpResponseMessage GetMusic(int id)
        {
            Music_Recording recording = _musicService.GetMusicRecording(id);

            if (recording == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, recording);
                return response;
            }
        }

        // POST (Update): api/Music/1
        public HttpResponseMessage PostMusic(Music_Recording recording)
        {
            if (_musicService.AddMusicRecording(recording) == true)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, recording);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/music/" + recording.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotAcceptable, recording);
                return response;
            }
        }

        // PUT: api/Music/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Music/5
        public HttpResponseMessage DeleteMusic(int id)
        {
            Music_Recording recording = _musicService.GetMusicRecording(id);
            if (recording == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, id);
                return response;
            }
            else
            {
                if (_musicService.DeleteMusicRecording(recording))
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, recording);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.InternalServerError, id); // Returns 500
                    return response;
                }
            }
        }
    }
}
