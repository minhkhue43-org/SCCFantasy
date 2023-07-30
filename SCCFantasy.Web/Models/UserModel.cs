namespace SCCFantasy.Web.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public int[] Team { get; set; }

        public string Roles { get; set; }

        public bool IsActive { get; set; }
    }
}
