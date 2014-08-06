using NUnit.Framework;
using SchoolLibrary.Services.UnregisteredUserManagment;
using System.Linq;

namespace SchoolLibrary.NUnitTests.UnregisteredUserService
{
    [TestFixture]
    public class UnregisteredUserTests
    {
        private IUnregisteredUserService _service;
        private FakeUserManager _fakeUsermanager;
        private FakeReaderManager _fakeReadermanager;

        [SetUp]
        public void SetUp() {
            _fakeUsermanager = new FakeUserManager();
            _fakeReadermanager = new FakeReaderManager();
            _service = new Services.UnregisteredUserManagment.UnregisteredUserService(_fakeUsermanager, _fakeReadermanager);
        }
//Cheking if all returned Users are Unregistered 
        [Test]
        public void GetUnregisteredUsersReturnsUnregisteredOnly() {
            var serviceReturned = _service.GetAllUsers();
            foreach(var user in serviceReturned){
                Assert.AreEqual(user.Role, "Unregistered");
            }
        }
//Compare Manager and Service with UnregisteredUsers
        [Test]
        public void GetUnregisteredUsersReturnsAllUnregistered()
        {
            var managerReturned = _fakeUsermanager.GetAllUsers().Where(u => u.Role == "Unregistered");
            var serviceReturned = _service.GetAllUsers().ToArray();
            foreach (var user in managerReturned)
            {
                Assert.IsTrue(serviceReturned.Any(u => u == user));
            }
        }

        [Test]
        public void CallsConfirmationMethod()
        {
            _fakeUsermanager.CanSubmitProp = true;
            _service.Submit(1, "testRole");
            var confirmCalled = _fakeUsermanager.CalledConfirmation == 1;
            Assert.IsTrue(confirmCalled);
        }

        [Test]
        public void DoesNotCallConfirmationMethodWhenCannotSubmit()
        {
            _fakeUsermanager.CanSubmitProp = false;
            _service.Submit(1, "testRole");
            Assert.AreEqual(0, _fakeUsermanager.CalledConfirmation);
        }

        [Test]
        public void ReturnsFalseWhenCannotSubmit()
        {
            _fakeUsermanager.CanSubmitProp = false;
            var result = _service.Submit(1, "testRole");
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnsTrueWhenCanSubmit()
        {
            _fakeUsermanager.CanSubmitProp = true;
            var result = _service.Submit(1, "testRole");
            Assert.IsTrue(result.Success);
        }

    }
}
