namespace SCCFantasy.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public string ClubName { get; set; }

        public decimal Price { get; set; }

        public string PostionName { get; set; }

        public decimal SelectedPercent { get; set; }
    }
}
