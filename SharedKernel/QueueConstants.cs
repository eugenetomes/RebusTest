namespace SharedKernel
{
    public static class QueueConstants
    {
        public static string ConnectionString = @"Host=localhost;Port=5432;Database=mb2;Username=tomesdev;Password=tomesdev;Include Error Detail=true";
        public static string SchemaName = "bmq";
        public static string QueueName = "tomese-queue";
        public static string MessageTableName = "Messages";

    }
}
