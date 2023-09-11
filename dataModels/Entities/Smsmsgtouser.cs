using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dataModels.Entities;

public partial class Smsmsgtouser
{
    public Smsmsgtouser()
    {
        GuidOfUserMessage = Guid.NewGuid();
    }
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Sms { get; set; } = null!;

    public bool IsDelete { get; set; }

    public Guid CreatorId { get; set; }

    public Guid? ModificationId { get; set; }

    public Guid? DeletorId { get; set; }

    public DateTime? ModificationTime { get; set; }

    public DateTime? CreationTime { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid GuidOfUserMessage { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual User? Deletor { get; set; }

    public virtual User? Modification { get; set; }

    public virtual User User { get; set; } = null!;
}
