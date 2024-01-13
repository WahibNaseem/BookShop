namespace BookShop.API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        ///* EF Relations */
        //public IEnumerable<Book> Books { get; set;}
    }
}
