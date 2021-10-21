using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace techy.Models
{
    public class Item : BaseTable<Item>
    {
        [PrimaryKey, AutoIncrement]
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Time { get; set; }
        public string CompletionStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string System { get; set; }


        public Item() : base()
        {
        }

        // This is required to trigger the base constructor to create the table if necessary
        static Item()
        {
            var init = (new Item());
        }

        public static async Task<List<Item>> GetItemsAsync()
        {
            return await Database.Table<Item>().ToListAsync().ConfigureAwait(false);
        }

        public static async Task<Item> GetItemAsync(int id)
        {
            return await Database.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public static async Task<int> SaveItemAsync(Item item)
        {
            if (item.ID != null && item.ID > 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public static async Task<int> DeleteItemByIdAsync(int id)
        {
            var currentFinding = await GetItemAsync(id);
            if (currentFinding != null)
            {
                return await Database.DeleteAsync(currentFinding);
            }

            return 0;
        }

        public static async Task<int> DeleteItemAsync(Item item)
        {
            return await Database.DeleteAsync(item);
        }
    }
}