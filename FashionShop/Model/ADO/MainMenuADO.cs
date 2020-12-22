using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.ADO
{
    public class MainMenuADO
    {
        DbFashionShop db = null;
        public MainMenuADO()
        {
            db = new DbFashionShop();
        }

        // Get list main menu by id of type_menu
        public List<menu> GetListMenuByMenuTypeId(int id)
        {
            return db.menu.Where(x => x.menu_type_id == id).ToList();
        }
    }
}
