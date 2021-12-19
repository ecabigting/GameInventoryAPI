
namespace GameInventoryAPI.Settings
{
    public class DBSettings 
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnString { 
            get
            {
                return $"mongodb://{Host}:{Port}";
            } 
        }
    }    
}