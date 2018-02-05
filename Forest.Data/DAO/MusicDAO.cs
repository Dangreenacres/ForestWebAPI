using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.IDAO;
using Forest.Data.BEANS;
using System.Data.Entity.Validation;

namespace Forest.Data.DAO
{
    public class MusicDAO : IMusicDAO
    {
        private ForestEntities _context;
        public MusicDAO()
        {
            _context = new ForestEntities();
        }

        public IList<Music_Category> GetMusicCategories()
        {
            IQueryable<Music_Category> _categories;
            _categories = from category
                          in _context.Music_Category
                          select category;
            return _categories.ToList();
        }

        public void AddMusicCategory(Music_Category category)
        {
            _context.Music_Category.Add(category);
            _context.SaveChanges();
        }

        public IList<MusicBEAN> GetMusicRecordings(int genre)
        {
            IQueryable<MusicBEAN> _musicBEANs = from recs in _context.Music_Recording
                                                from cats in _context.Music_Category
                                                where recs.Genre == cats.Id
                                                where cats.Id == genre
                                                select new MusicBEAN
                                                {
                                                    Id = recs.Id,
                                                    Artist = recs.Artist,
                                                    Title = recs.Title,
                                                    Genre = cats.Genre,
                                                    Image_Name = recs.Image_Name,
                                                    Num_Tracks = recs.Num_Tracks,
                                                    Price = recs.Price,
                                                    Stock_Count = recs.Stock_Count,
                                                    Released = recs.Released
                                                    //Url = recs.Url
                                                };
            return _musicBEANs.ToList();
        }

        public IList<Music_Recording> GetMusicRecordings()
        {

            IQueryable<Music_Recording> recordings;

            recordings = from rec in _context.Music_Recording
                         select rec;

            if (recordings.Count() != 0)
                return recordings.ToList();
            else
                return null;
        }

        private bool MusicRecordingCheck(int id)
        {
            IQueryable<int> idList = from recs
                                     in _context.Music_Recording
                                     select recs.Id;
            if (idList.ToList().Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Music_Recording GetMusicRecording(int id)
        {
            if (MusicRecordingCheck(id) == true)
            {
                IQueryable<Music_Recording> _recording;
                _recording = from recording
                             in _context.Music_Recording
                             where recording.Id == id
                             select recording;
                return _recording.ToList().First();
            }
            else
            {
                return null;
            }
        }
        public bool DeleteMusicRecording(Music_Recording recording)
        {
            if (MusicRecordingCheck(recording.Id) == true)
            {
                _context.Music_Recording.Remove(recording);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddMusicRecording(Music_Recording recording)
        {
            try
            {
                _context.Music_Recording.Add(recording);
                _context.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{ 0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                return false;
                throw;
            }
        }

        public void EditMusicRecording(Music_Recording recording)
        {
            Music_Recording record = GetMusicRecording(recording.Id);

            record.Artist = recording.Artist;
            record.Genre = recording.Genre;
            record.Image_Name = recording.Image_Name;
            record.Num_Tracks = recording.Num_Tracks;
            record.Price = recording.Price;
            record.Released = recording.Released;
            record.Stock_Count = recording.Stock_Count;
            record.Title = recording.Title;
            //record.Url = recording.Url;
            _context.SaveChanges();
        }

        //public void AddMusicRecording(Music_Recording recording)
        //{
        //    _context.Music_Recording.Add(recording);
        //    _context.SaveChanges();
        //}

        //public void DeleteMusicRecording(Music_Recording recording)
        //{
        //    _context.Music_Recording.Remove(recording);
        //    _context.SaveChanges();
        //}
    }
}
