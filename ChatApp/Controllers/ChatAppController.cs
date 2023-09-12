using dataModels.dbContext;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace ChatApp.Controllers
{
    public class ChatAppController : Controller
    {
        private readonly ChatDbContext _db;
        private readonly IChatbotApplication _chatbotApplication;
        public ChatAppController(ChatDbContext db, IChatbotApplication chatbotApplication)
        {
            _db = db;
            _chatbotApplication = chatbotApplication;
        }
        public IActionResult ChatApp()
        {
            return View();
        }
        public IActionResult userLIst()
        {
            var userList = _chatbotApplication.getUserLIst();
            return PartialView("userList", userList);
        }
        public IActionResult InductionUser()
        {
            var inductionusers = _chatbotApplication.getInductionUserList();
            return PartialView("InductionUserList", inductionusers);
        }
        public IActionResult chatView(Guid userId)
        {
            var myModel = _chatbotApplication.fetchMessageDetailsForUser(userId);
            return PartialView("chatView", myModel);
        }

        public IActionResult chatViewForInductionUser(Guid userId)
        {
            var myModel = _chatbotApplication.fetchMessageDetailsForInductionUser(userId);
            return PartialView("chatViewForInductionUsers", myModel);
        }


        [HttpPost]
        public bool SaveComments(Guid sendToUser, string messageTxt)
        {
            var status = _chatbotApplication.SaveCommentsForUsers(sendToUser, messageTxt);
            return status;
        }

        [HttpPost]
        public bool SaveCommnetsForInductionUser(Guid sendToUserId, string messageText)
        {
            var status = _chatbotApplication.SaveCommnetsForInductionUsers(sendToUserId, messageText);
            return status;
        }
        public IActionResult chatUI() {
        return View();
        }
    }
}
