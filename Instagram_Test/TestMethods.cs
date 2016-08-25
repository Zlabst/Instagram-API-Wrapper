using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninja.Instagram.API.Models;
using Ninja.Instagram.API.Controllers;
using Ninja.Instagram.API;

namespace Instagram_Test
{
    [TestClass]
    public class TestMethods
    {
        private Account _account;
        private User _user;
        private Picture _picture;

        [TestInitialize]
        [TestMethod]
        public void Login()
        {
<<<<<<< HEAD
            //jasdasaa122:keanumats123
            //kaaajsaaan:keanumats123
            _account = AccountController.Login("janisgeenman123", "keanumats");
            _user = UserController.Lookup("we.killed.pepe");
            _picture = PictureController.Lookup("_Nt-0EB907");
=======
            Instagram.Initialize();
            _account = AccountController.Login("username", "password"); //you can add a webproxy object as a parameter here.
            _user = UserController.Lookup("other username");
            _picture = PictureController.Lookup("picture ID"); //found in the URL. For example https://www.instagram.com/p/_Nt-0EB907/. The picture ID here is _Nt-0EB907.
>>>>>>> origin/master
        }

        //[TestMethod]
        public void Register()
        {
            Account account = AccountController.Create("username", "password", "email", "Full Name");
        }
        
        [TestMethod]
        public void UserLookup()
        {
            Assert.AreEqual(Convert.ToUInt64(1547975502), _user.ID);
        }
        
        [TestMethod]
        public void PictureLookup()
        {
            Assert.AreEqual("1138768509397359931", _picture.ID);
        }
        
        [TestMethod]
        public void Follow()
        {
            AccountController.Follow(_account, _user);
        }

        [TestMethod]
        public void UnFollow()
        {
            AccountController.UnFollow(_account, _user);
        }

        [TestMethod]
        public void Like()
        {
            AccountController.Like(_account, _user, _picture);
        }

        [TestMethod]
        public void UnLike()
        {
            AccountController.UnLike(_account, _user, _picture);
        }
    }
}
