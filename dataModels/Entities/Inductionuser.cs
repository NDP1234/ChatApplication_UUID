using System;
using System.Collections.Generic;

namespace dataModels.Entities;

public partial class Inductionuser
{
    public Guid Inductionuserguid { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsDelete { get; set; }

    public Guid? CreatorId { get; set; }

    public Guid? ModificationId { get; set; }

    public Guid? DeletorId { get; set; }

    public DateTime? ModificationTime { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual ICollection<Smsmsgtoinductionuser> Smsmsgtoinductionusers { get; set; } = new List<Smsmsgtoinductionuser>();
}
