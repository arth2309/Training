using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.DataModels;

[Table("Chat")]
public partial class Chat
{
    [Key]
    public int ChatId { get; set; }

    public int SenderId { get; set; }

    public int RecieverId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("chat", TypeName = "character varying")]
    public string Chat1 { get; set; } = null!;

    public int RequestId { get; set; }

    [ForeignKey("RecieverId")]
    [InverseProperty("ChatRecievers")]
    public virtual AspNetUser Reciever { get; set; } = null!;

    [ForeignKey("RequestId")]
    [InverseProperty("Chats")]
    public virtual Request Request { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("ChatSenders")]
    public virtual AspNetUser Sender { get; set; } = null!;
}
