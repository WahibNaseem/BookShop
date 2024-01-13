namespace BookShop.API.Contracts.V1
{
    public static class APIRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/"+ Version;

        public static class Books
        {
            public const string GetAll = Base + "/books";
            public const string Get = Base + "/books/{bookId}";
            public const string Create = Base +"/books";
            public const string Update = Base + "/books/{bookId}";
            public const string Delete = Base + "/books/{bookId}";
        }

        public static class Categories
        {
            public const string GetAll = Base + "" + "/categories";
            public const string Get = Base + "" + "/categories/{categoryId}";
            public const string Create = Base + "" + "/categories/{category}";
            public const string Update = Base + "" + "/categories/{categoryId}";
            public const string Delete = Base + "" + "/categories/{categoryId}";
        }
    }
}
