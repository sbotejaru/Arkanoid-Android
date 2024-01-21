using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ScoreService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public ScoreService()
        {

        }

        public IList<Score> GetAll()
        {
            var results = unitOfWork.Scores.GetAll();

            return results;
        }

        public bool Add(Score score)
        {
            unitOfWork.Scores.Add(score);
            return true;
        }

        public bool Delete(Score score)
        {
            unitOfWork.Scores.Delete(score);
            return true;
        }

        public bool Delete(int id)
        {
            unitOfWork.Scores.Delete(id);
            return true;
        }

        public bool Update(Score payload)
        {
            if (payload == null || payload.Username == null || payload.Points == null)
                return false;

            var result = unitOfWork.Scores.GetById(payload.Id);
            if (result == null)
                return false;

            unitOfWork.Scores.Update(payload);
            return true;
        }

        public Score GetById(int id)
        {
            return unitOfWork.Scores.GetById(id);
        }

        public Score GetHighscoreByUsername(string username)
        {
            return unitOfWork.Scores.GetHighestByUsername(username);
        }

        public Score GetHighestScore()
        {
            return unitOfWork.Scores.GetHighest();
        }
    }
}
