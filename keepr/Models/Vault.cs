namespace keepr.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public Profile Creator { get; set; }
    }
    public class ProfileVaultViewModel : Vault
  {
    public string ProfileId { get; set; }
  }
}