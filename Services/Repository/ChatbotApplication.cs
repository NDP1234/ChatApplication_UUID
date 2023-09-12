using dataModels.dbContext;
using dataModels.Entities;
using dataModels.ViewModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ChatbotApplication : IChatbotApplication
    {
        private readonly ChatDbContext _db;
        public ChatbotApplication(ChatDbContext db)
        {
            _db = db;
        }
        

        public List<Inductionuser> getInductionUserList()
        {
            List<Inductionuser> inductionusers = _db.Inductionusers.ToList();
            return inductionusers;
        }

        public List<User> getUserLIst()
        {
            List<User> users = _db.Users.ToList();
            return users;
        }

        public ChatViewModel fetchMessageDetailsForUser(Guid userId)
        {
            var getFirstUser = _db.Users.First();
            var firstGuid = getFirstUser.GuidforUser;

            var creator = getFirstUser.FirstName + ' ' + getFirstUser.LastName;

            var userInfo = _db.Users.Where(user => user.GuidforUser == userId).First();
            ChatViewModel viewModel = new ChatViewModel();
            viewModel.FirstName = userInfo.FirstName;
            viewModel.LastName = userInfo.LastName;
            viewModel.PhoneNumber = userInfo.PhoneNumber;
            viewModel.Userid = userInfo.GuidforUser;
            viewModel.ForFirstUser = firstGuid;
            viewModel.creator = creator;
            viewModel.commonUserModels = _db.Smsmsgtousers
            .Where(user => (user.UserId == userInfo.GuidforUser && user.CreatorId == firstGuid) || (user.UserId == firstGuid && user.CreatorId == userInfo.GuidforUser))
            .OrderBy(user => user.CreationTime)
            .Select(user => new ChatViewModel.CommonUserModel
            {
                UserId = user.UserId,
                Sms = user.Sms,
                IsDelete = user.IsDelete,
                ToUserName  = userInfo.FirstName + " " + userInfo.LastName,
                CreatorId = user.CreatorId,
                ModificationId = user.ModificationId,
                DeletorId = user.DeletorId,
                ModificationTime = user.ModificationTime,
                CreationTime = user.CreationTime
            })
            .ToList();
            return viewModel;
        }

        public ChatViewModel fetchMessageDetailsForInductionUser(Guid userId)
        {
            
            var getFirstUser = _db.Users.First();
            var firstGuid = getFirstUser.GuidforUser;

            var userInfo = _db.Inductionusers.Where(user => user.Inductionuserguid == userId).First();
            ChatViewModel viewModel = new ChatViewModel();
            viewModel.FirstName = userInfo.FirstName;
            viewModel.LastName = userInfo.LastName;
            viewModel.PhoneNumber = userInfo.PhoneNumber;
            viewModel.Userid = userInfo.Inductionuserguid;
            viewModel.ForFirstUser = firstGuid;
            viewModel.creator = getFirstUser.FirstName + " " + getFirstUser.LastName;
            viewModel.commonUserModels = _db.Smsmsgtoinductionusers
            .Where(user => (user.InductionUserId == userInfo.Inductionuserguid) && (user.CreatorId == firstGuid))
            .OrderBy(user => user.CreationTime)
            .Select(user => new ChatViewModel.CommonUserModel
            {
                UserId = user.InductionUserId,
                Sms = user.Sms,
                IsDelete = user.IsDelete,
                CreatorId = firstGuid,
                ModificationId = user.ModificationId,
                DeletorId = user.DeletorId,
                ModificationTime = user.ModificationTime,
                CreationTime = user.CreationTime
            })
            .ToList();
            return viewModel;
        }

        public bool SaveCommentsForUsers(Guid sendToUser, string messageTxt)
        {

            var userInfo = _db.Users.Where(user => user.GuidforUser == sendToUser).FirstOrDefault();


            var firstuserBytes = _db.Users.First();
            Guid FirstUserGuid = firstuserBytes.GuidforUser;


            if (userInfo != null)
            {
                var addCommentForUser = new Smsmsgtouser();
                addCommentForUser.UserId = userInfo.GuidforUser;
                addCommentForUser.Sms = messageTxt;
                addCommentForUser.CreatorId = FirstUserGuid;
                _db.Smsmsgtousers.Add(addCommentForUser);
                _db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveCommnetsForInductionUsers(Guid sendToUserId, string messageText)
        {

            var inductionUserInfo = _db.Inductionusers.Where(iuser => iuser.Inductionuserguid == sendToUserId).FirstOrDefault();

            var firstuserBytes = _db.Users.First();
            Guid FirstUserGuid = firstuserBytes.GuidforUser;

            if (inductionUserInfo != null)
            {
                var addCommentForInductionUser = new Smsmsgtoinductionuser();
                addCommentForInductionUser.InductionUserId = inductionUserInfo.Inductionuserguid;
                addCommentForInductionUser.Sms = messageText;
                addCommentForInductionUser.CreatorId = FirstUserGuid;
                _db.Smsmsgtoinductionusers.Add(addCommentForInductionUser);
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
