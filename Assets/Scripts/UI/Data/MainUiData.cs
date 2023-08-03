using System.Collections.Generic;
using Zong.Test.Gameplay;
using Zong.Test.Views;


namespace Zong.Test.Data
{
    public class MainUiData : UiBaseData
    {
        public ItemCategory Categories;
        public Player Player;
    }

    public class Category
    {
        public string name;
        public List<string> subcategories;
    }

    public class ItemCategory
    {
        public List<Category> categories;
    }
}