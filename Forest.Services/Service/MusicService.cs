using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data;
using Forest.Data.IDAO;
using Forest.Data.DAO;
using Forest.Data.BEANS;

namespace Forest.Services.Service
{
    public class MusicService : IService.IMusicService
    {
        private IMusicDAO _musicDAO;

        public MusicService()
        {
            _musicDAO = new MusicDAO();
        }

        public IList<Music_Category> GetMusicCategories()
        {
            return _musicDAO.GetMusicCategories();
        }

        public void AddMusicCategory(Music_Category category)
        {
            _musicDAO.AddMusicCategory(category);
        }

        public IList<MusicBEAN> GetMusicRecordings(int genre)
        {
            return _musicDAO.GetMusicRecordings(genre);
        }

        public IList<Music_Recording> GetMusicRecordings()
        {
            IList<Music_Recording> recordings = _musicDAO.GetMusicRecordings();

            if (recordings != null)
                return recordings;
            else
                return null;
        }

        public Music_Recording GetMusicRecording(int id)
        {
            if (_musicDAO != null)
                return _musicDAO.GetMusicRecording(id);
            else
                return null;
        }

        public bool DeleteMusicRecording(Music_Recording recording)
        {
            if (_musicDAO.DeleteMusicRecording(recording) == true)
                return true;
            else
                return false;
        }

        public bool AddMusicRecording(Music_Recording recording)
        {
            if (_musicDAO.AddMusicRecording(recording) == true)
                return true;
            else
                return false;
        }

        public void EditMusicRecording(Music_Recording recording)
        {
            _musicDAO.EditMusicRecording(recording);
        }

        //public void AddMusicRecording(Music_Recording recording)
        //{
        //    _musicDAO.AddMusicRecording(recording);
        //}

        //public void DeleteMusicRecording(Music_Recording recording)
        //{
        //    _musicDAO.DeleteMusicRecording(recording);
        //}
    }
}
