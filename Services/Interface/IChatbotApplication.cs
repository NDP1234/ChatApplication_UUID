using dataModels.Entities;
using dataModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IChatbotApplication
    {
        public List<User> getUserLIst();
        public List<Inductionuser> getInductionUserList();

        public ChatViewModel fetchMessageDetailsForUser(Guid userId);

        public ChatViewModel fetchMessageDetailsForInductionUser(Guid userId);

        public bool SaveCommentsForUsers(Guid sendToUser, string messageTxt);

        public bool SaveCommnetsForInductionUsers(Guid sendToUserId, string messageText);
    }
}
