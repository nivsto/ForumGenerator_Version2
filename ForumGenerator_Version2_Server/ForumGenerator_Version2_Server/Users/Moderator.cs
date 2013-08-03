using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ForumGenerator_Version2_Server.Users
{
    [DataContract(IsReference = true)]
    public class Moderator
    {
        [Key]
        [DataMember]
        public int moderatorId { get; set; }
        [DataMember]
        public User user { get; private set; }
        [DataMember]
        public virtual modLevel? level { get; set; }

        public enum modLevel
        {
            NONE = 0,
            DEL,
            EDIT,
            ALL
        }

        public Moderator(User user, modLevel level)
        {
            this.user = user;
            this.level = level;
        }

        public Moderator(Moderator m)
        {
            this.moderatorId = m.moderatorId;
            this.user = new User(m.user);
            this.level = m.level;
        }
    }
}
