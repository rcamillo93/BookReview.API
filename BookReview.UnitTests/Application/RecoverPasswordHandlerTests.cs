using BookReview.Application.Commads.UserCommands.RecoveryPassword;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using NSubstitute;
using System.Reflection;

namespace BookReview.UnitTests.Application
{
    public class RecoverPasswordHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Error_When_User_Not_Found()
        {
            // Arrange
            var email = "notfound@email.com";
            var command = new RecoverPasswordCommand(email, "temp", "newpass");

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();

            userRepository.GetUserByEmailAsync(email).Returns(Task.FromResult<User>(null));

            var handler = new RecoverPasswordHandler(userRepository, authService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Usuário não encontrado.", result.Message);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_TemporaryPassword_Is_Invalid()
        {
            // Arrange
            var email = "user@email.com";
            var command = new RecoverPasswordCommand(email, "temp", "newpass");

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();
          
            var user = new User("José Silveira", email, "oldHash");

            // Utiliza reflection para definir a propriedade privada TemporaryPassword
            var userType = typeof(User);
            var tempPassProp = userType.GetProperty("TemporaryPassword", BindingFlags.Instance | BindingFlags.NonPublic);
            tempPassProp.SetValue(user, "expectedHash");

            // Define a validade como uma data futura (não expirada)
            var validateHashProp = userType.GetProperty("ValidateHash", BindingFlags.Instance | BindingFlags.NonPublic);
            validateHashProp.SetValue(user, DateTime.Now.AddMinutes(10));

            userRepository.GetUserByEmailAsync(email).Returns(Task.FromResult(user));

            // Configura o authService para que o hash gerado a partir de "temp" seja diferente do esperado
            authService.ComputeSha256Hash("temp").Returns("wrongHash");
            authService.ComputeSha256Hash("newpass").Returns("hashedNew");

            var handler = new RecoverPasswordHandler(userRepository, authService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Usuário ou senha inválidos.", result.Message);
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_TemporaryPassword_Is_Expired()
        {
            // Arrange
            var email = "user@email.com";
            var command = new RecoverPasswordCommand(email, "temp", "newpass");

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();

            var user = new User("John Doe", email, "oldHash");

            var userType = typeof(User);
            var tempPassProp = userType.GetProperty("TemporaryPassword", BindingFlags.Instance | BindingFlags.NonPublic);

            // Configura o hash esperado para "temp"
            authService.ComputeSha256Hash("temp").Returns("hashedTemp");
            tempPassProp.SetValue(user, "hashedTemp");

            // Define a validade como uma data no passado (expirada)
            var validateHashProp = userType.GetProperty("ValidateHash", BindingFlags.Instance | BindingFlags.NonPublic);
            validateHashProp.SetValue(user, DateTime.Now.AddMinutes(-5));

            userRepository.GetUserByEmailAsync(email).Returns(Task.FromResult(user));

            var handler = new RecoverPasswordHandler(userRepository, authService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Usuário ou senha inválidos.", result.Message);
        }

        [Fact]
        public async Task Handle_Should_Update_Password_When_Data_Is_Valid()
        {
            // Arrange
            var email = "user@email.com";
            var command = new RecoverPasswordCommand(email, "temp", "newpass");

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();

            // Cria uma instância real de User
            var user = new User("José Silveira", email, "oldHash123");

            var userType = typeof(User);

            
            var tempPassProp = userType.GetProperty("TemporaryPassword", BindingFlags.Instance | BindingFlags.NonPublic);

            authService.ComputeSha256Hash("temp").Returns("hashedTemp");
            tempPassProp.SetValue(user, "hashedTemp");

            // Define a validade como uma data futura (válida)
            var validateHashProp = userType.GetProperty("ValidateHash", BindingFlags.Instance | BindingFlags.NonPublic);
            validateHashProp.SetValue(user, DateTime.Now.AddMinutes(10));

            userRepository.GetUserByEmailAsync(email).Returns(Task.FromResult(user));
            userRepository.SaveChangesAsync().Returns(Task.CompletedTask);
                        
            authService.ComputeSha256Hash("newpass").Returns("hashedNew");

            var handler = new RecoverPasswordHandler(userRepository, authService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            // Verifica, via reflection, se a senha foi atualizada (supondo que UpdatePassword atualize a propriedade privada "Password")
            var passwordProp = userType.GetProperty("Password", BindingFlags.Instance | BindingFlags.NonPublic);
            var updatedPassword = passwordProp.GetValue(user) as string;
            Assert.Equal("hashedNew", updatedPassword);

            await userRepository.Received(1).SaveChangesAsync();
        }
    }
}