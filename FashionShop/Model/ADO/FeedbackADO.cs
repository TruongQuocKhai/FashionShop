using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class FeedbackADO
    {
        DbFashionShop db = null;
        public FeedbackADO()
        {
            db = new DbFashionShop();
        }

        public int Insert(feedback feedback)
        {
            db.feedback.Add(feedback);
            db.SaveChanges();
            return feedback.feedback_id;
        }
    }
}
