namespace BookShop.API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        //public int CategoryId { get; set; }

        ///* EF Relation */
        //public Category Category { get; set; }
    }
}
