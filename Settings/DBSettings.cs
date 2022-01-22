
namespace GameInventoryAPI.Settings
{
    public class DBSettings 
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnString { 
            get
            {
                return $"mongodb+srv://{User}:{Password}@{Host}?retryWrites=true&w=majority:{Port}";
            } 
        }
    }    
}