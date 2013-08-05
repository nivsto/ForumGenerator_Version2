using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ForumGenerator_Version2_Server.Users;
using ForumGenerator_Version2_Server.ForumData;
using ForumGenerator_Version2_Server;

namespace ForumService
{
    [ServiceContract(CallbackContract = typeof(IForumServiceCallback))]
    interface IForumService
    {
        [OperationContract]
        bool subscribe(int forumId, string userName);

        [OperationContract]
        bool unsubscribe();


        [OperationContract]
        User login(int forumId, string userName, string password);

        [OperationContract]
        bool logout(int forumId, string userName, string password);

        [OperationContract]
        SuperUser superUserLogin(string userName, string password);

        [OperationContract]
        bool superUserLogout(string userName, string password);

        [OperationContract]
        User register(int forumId, string userName, string password, string email, string signature);

        [OperationContract]
        List<Forum> getForums();

        [OperationContract]
        List<SubForum> getSubForums(int forumId);

        [OperationContract]
        List<Discussion> getDiscussions(int forumId, int subForumId);

        [OperationContract]
        List<Comment> getComments(int forumId, int subForumId, int discussionId);

        [OperationContract]
        List<User> getUsers(int forumId);

        [OperationContract]
        Forum createNewForum(string userName, string password, string forumName, string adminUserName, string adminPassword, ForumGenerator_Version2_Server.ForumData.Forum.RegPolicy registrationPolicy);

        [OperationContract]
        SubForum createNewSubForum(string userName, string password, int forumId, string subForumTitle);

        [OperationContract]
        Discussion createNewDiscussion(string userName, string password, int forumId, int subForumId, string title, string content);

        [OperationContract]
        Comment createNewComment(string userName, string password, int forumId, int subForumId, int discussionId, string content);

        [OperationContract]
        User changeAdmin(string userName, string password, int forumId, int newAdminUserId);

        // added in version 3:

        [OperationContract]
        Boolean addModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd, ForumGenerator_Version2_Server.Users.Moderator.modLevel level);

        [OperationContract]
        Boolean removeModerator(string modUserName, int forumId, int subForumId, string adderUsrName, string adderPswd);
        
        [OperationContract]
        bool removeSubForum(int forumId, int subForumId, string userName, string password);

        [OperationContract]
        Boolean deleteDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd);

        [OperationContract]
        Discussion editDiscussion(int forumId, int subForumId, int discussionId, string userName, string pswd, string newContent);

        [OperationContract]
        int getNumOfCommentsSingleUser(string reqUserName, string reqPswd, int forumId, string userName);

        [OperationContract]
        int getNumOfCommentsSubForum(string userName, string pswd, int forumId, int subForumId);

        [OperationContract]
        List<User> getResponsersForSingleUser(string reqUserName, string reqPswd, int forumId, string memberUserName);

        [OperationContract]
        List<User> getMutualUsers(string userName, string password, int forumId1, int forumId2);

        [OperationContract]
        List<Moderator> getModerators(int forumId, int subForumId);

        [OperationContract]
        int getUserType(int forumId, string userName);

        [OperationContract]
        int getUserTypeSubForum(int forumId, int subForumId, string userName);

        // Web functions:

        [OperationContract]
        int countDiscussionsPerForum(int forumId);

        [OperationContract]
        int countSubForumsPerForum(int forumId);

        [OperationContract]
        int countDiscussionsPerSubForum(int forumId, int subForumId);

        [OperationContract]
        int countCommentsPerSubForum(int forumId, int subForumId);

        [OperationContract]
        int countCommentsPerDiscussion(int forumId, int subForumId, int discussionId);

        [OperationContract]
        bool confirmUser(int forumId, string userName);

        [OperationContract]
        List<User> getUnconfirmedUsers(int forumId);

        [OperationContract]
        bool changeModLevel(int forumId, int subForumId, string moderatorName, ForumGenerator_Version2_Server.Users.Moderator.modLevel newLevel);
    }

}
