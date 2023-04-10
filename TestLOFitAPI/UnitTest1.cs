using FluentAssertions;
using LOFitAPI.Controllers.PostModels.Login;
using LOFitAPI.DbControllers.Accounts;
using LOFitAPI.DbModels.Accounts;
using LOFitAPI.Tools;

namespace TestLOFitAPI
{
    public class UnitTest1
    {
        [Fact]
        public void SendTest()
        {
            string answer = SendMail.Send("jadedme99@gmail.com", 123456);

            answer.Should().Be("Ok");
        }

        [Fact]
        public void CodeTest()
        {
            int answer = CodeGenerator.Generate();

            bool isSixDigitCode = answer > 100000 && answer < 999999;

            isSixDigitCode.Should().BeTrue();
        }

        [Fact]
        public void LoginWrongTest()
        {
            LoginPostModel wrongForm = new LoginPostModel() { Email="user", Password="user"};
            int accountType = KontoDbController.IsOkLogin(wrongForm);

            bool answer = accountType == 4;

            answer.Should().BeTrue();
        }
        [Fact]
        public void LoginTest()
        {
            LoginPostModel wrongForm = new LoginPostModel() { Email="user@u", Password="user"};
            int accountType = KontoDbController.IsOkLogin(wrongForm);

            bool answer = accountType == 4;

            answer.Should().BeFalse();
        }
        [Fact]
        public void TrenerListTest()
        {
            List<TrenerModel> list = TrenerDbController.GetAll();

            list.Any().Should().BeFalse();
        }

    }
}