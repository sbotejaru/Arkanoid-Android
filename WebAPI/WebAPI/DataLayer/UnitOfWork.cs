using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public ScoreRepository Scores { 
            get
            {
                return new ScoreRepository();
            }
        }
    }
}
