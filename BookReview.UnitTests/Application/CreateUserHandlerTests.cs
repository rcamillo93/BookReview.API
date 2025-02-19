using BookReview.Application.Commads.UserCommands.Create;
using BookReview.Application.Services.Interfaces;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.Core.Services;
using NSubstitute;
using System.Reflection;

namespace BookReview.UnitTests.Application
{
    public class CreateUserHandlerTests
    {

        [Fact]
        public async Task Handle_Should_Create_User_Successfully()
        {
            // Arrange
            var expectedUserId = 1;
            var command = new CreateUserCommand("João Silva", "js@gmail.com", "senha@1234");

            // Cria os mocks das dependências
            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();
            var userService = Substitute.For<IUserService>();

            // Simula que o e-mail não está cadastrado
            userService.ValidateEmail(Arg.Any<string>()).Returns(Task.FromResult(true));

            
            authService.ComputeSha256Hash(Arg.Any<string>()).Returns("hashed_password");

            // Ao adicionar o usuário, simula a atribuição do Id via reflection (caso o setter seja inacessível)
            userRepository.When(repo => repo.AddAsync(Arg.Any<User>()))
                .Do(call =>
                {
                    var userArg = call.Arg<User>();
                    var idProperty = userArg.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.Public);
                    var setMethod = idProperty?.GetSetMethod(true);
                    if (setMethod != null)
                    {
                        setMethod.Invoke(userArg, new object[] { expectedUserId });
                    }
                });
            userRepository.SaveChangesAsync().Returns(Task.CompletedTask);
                       
            var handler = new CreateUserCommandHandler(userRepository, authService, userService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedUserId, result.Data);

            // Verifica se os métodos foram chamados com os parâmetros corretos
            await userService.Received(1).ValidateEmail("js@gmail.com");
            authService.Received(1).ComputeSha256Hash("senha");
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Email_Already_Registered()
        {
            // Arrange
            var command = new CreateUserCommand("João Silva", "js@gmail.com", "senha@1234");

            var userRepository = Substitute.For<IUserRepository>();
            var authService = Substitute.For<IAuthService>();
            var userService = Substitute.For<IUserService>();

            // Simula que o e-mail já está cadastrado
            userService.ValidateEmail(Arg.Any<string>()).Returns(Task.FromResult(false));

            var handler = new CreateUserCommandHandler(userRepository, authService, userService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("E-mail já cadastrado.", result.Message);

            // Verifica que o método de hash não foi chamado, pois a validação do e-mail falhou
            authService.DidNotReceive().ComputeSha256Hash(Arg.Any<string>());
        }
    }
}

