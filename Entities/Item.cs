namespace Play.Catalog.Service.Entities
{
    public class Item
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public int Price { get; set; }  

        public DateTimeOffset CreatedDate { get; set; }

    }
}
