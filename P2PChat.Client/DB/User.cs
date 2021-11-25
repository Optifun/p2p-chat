using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2PChat.Client.DB
{
    [Table("User")]
    public class User
    {
        public Guid UserId { get; set; }

        public string Nickname { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public User(Guid id, string name)
        {
            UserId = id;
            Nickname = name;
            Messages = new List<Message>();
        }
    }
}