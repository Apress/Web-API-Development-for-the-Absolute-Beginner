namespace ConferenceApi.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public virtual ICollection<Talk> Talks { get; set; }

    }
}
