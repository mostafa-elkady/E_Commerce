namespace E_Commerce.Core.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //Navigational Property For Realation Between ApplicationUser And Address
        public AppUser applicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
