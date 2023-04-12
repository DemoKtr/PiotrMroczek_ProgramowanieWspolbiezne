using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace Logic
{
    public abstract class BallColletionApi
    {
        public abstract void CreateBallCollection(int quantity);
        public abstract List<LogicAbstractApi> GetBallsCollection();
        public static BallColletionApi CreateBallCollectionLogic(DataAbstractAPI dataApi= default(DataAbstractAPI))
        {
            return new BallCollection();
        }
    }
}
