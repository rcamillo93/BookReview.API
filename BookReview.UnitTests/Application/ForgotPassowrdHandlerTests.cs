using BookReview.Application.Commads.UserCommands.ForgotPassword;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using NSubstitute;

namespace BookReview.UnitTests.Application
{
    public class ForgotPasswordHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Error_When_User_Not_Found()
        {
            // Arrange
            var email = "notfound@example.com";
            var command = new ForgotPassowrdCommand(email);

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();
            var emailService = Substitute.For<IEmailService>();

            // Simula que nenhum usuário foi encontrado para o e-mail
            userRepository.GetUserByEmailAsync(email).Returns(Task.FromResult<User>(null));

            var handler = new ForgotPassowrdHandler(userRepository, authService, emailService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Usuário não encontrado.", result.Message);
        }

        [Fact]
        public async Task Handle_Should_Reset_Password_And_Send_Email_When_User_Found()
        {
            // Arrange
            var email = "user@gmail.com";
            var command = new ForgotPassowrdCommand(email);
                      
            // É necessário que os métodos (ex.: StartRecoveryPassword) sejam virtuais para que o NSubstitute possa interceptá-los.
            var user = Substitute.For<User>("João Silva", email, "senhAntiga123");

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();
            var emailService = Substitute.For<IEmailService>();

            userRepository.GetUserByEmailAsync(email).Returns(Task.FromResult(user));

            // Configura o serviço de autenticação para gerar uma senha temporária e seu hash
            var tempPassword = "temp1234";
            var hashedPassword = "hash_temp1234";
            authService.GenerateTemporaryPassword(8).Returns(tempPassword);
            authService.ComputeSha256Hash(tempPassword).Returns(hashedPassword);
                    
            userRepository.SaveChangesAsync().Returns(Task.CompletedTask);

            // Configura o serviço de e-mail para completar sem erro
            emailService.SendRecoveryPasswordEmailAsync(user, tempPassword, hashedPassword).Returns(Task.CompletedTask);

            var handler = new ForgotPassowrdHandler(userRepository, authService, emailService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(hashedPassword, result.Data);

            // Verifica se o método StartRecoveryPassword foi chamado com o hash da senha temporária
            user.Received(1).StartRecoveryPassword(hashedPassword);

            // Verifica se o repositório salvou as alterações
            await userRepository.Received(1).SaveChangesAsync();

            // Verifica se o serviço de e-mail foi chamado para enviar o e-mail de recuperação
            await emailService.Received(1).SendRecoveryPasswordEmailAsync(user, tempPassword, hashedPassword);
        }
    }
}