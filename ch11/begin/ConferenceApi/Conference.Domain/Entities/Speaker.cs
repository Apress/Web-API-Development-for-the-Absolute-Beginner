using System;
using System.Collections.Generic;

namespace Conference.Domain.Entities;

public partial class Speaker
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Website { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;

    public virtual ICollection<Talk> Talks { get; } = new List<Talk>();
}
