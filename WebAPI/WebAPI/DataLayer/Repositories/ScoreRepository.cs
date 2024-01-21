using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class ScoreRepository : BaseRepository<Score>
    {
        public Score GetHighestByUsername(string username)
        {
            using (var ctx = new GameContext())
            {
                var result = ctx.Scores
                    .Where(e => e.Username == username)
                    .OrderByDescending(e => int.Parse(e.Points))
                    .FirstOrDefault();

                return result;
            }
        }

        public Score GetHighest()
        {
            using (var ctx = new GameContext())
            {
                var result = ctx.Scores
                    .OrderByDescending(e => int.Parse(e.Points))
                    .FirstOrDefault();

                return result;
            }
        }
    }
}
