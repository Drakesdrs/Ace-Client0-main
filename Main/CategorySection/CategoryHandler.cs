using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.ModuleSection;

namespace Ace_client.Main.CategorySection
{
    public class Categories : List<Category>
    {
        public void Add(string name)
        {
            base.Add(new Category(name));
        }
        public void AddAll(params string[] names)
        {
            foreach (string c in names)
                this.Add(c);
        }
    }
    public class CategoryHandler
    {
        public static CategoryHandler registry;
        public Categories categories = new Categories();

        public CategoryHandler()
        {
            registry = this;

            Logger.debug("Category registry begin...");

            categories.AddAll
            (
                "Combat",
                "Render",
                "Movement",
                "Player",
                "Exploits"       
            );
            selectedCategory = categories[0];

            Logger.debug("Success!");
        }

        public bool isSelectedCategoryActive = false;
        public Category selectedCategory;

        public void selectNextCategory()
        {
            var index = categories.IndexOf(selectedCategory);
            if(index == categories.Count() - 1)
                index = -1;
            selectedCategory = categories[index + 1];
        }
        public void selectPreviousCategory()
        {
            var index = categories.IndexOf(selectedCategory);
            if(index == 0)
                index = categories.Count();
            selectedCategory = categories[index - 1];
        }
    }  
}
