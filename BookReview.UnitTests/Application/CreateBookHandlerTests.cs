using BookReview.Application.Commads.BookCommands.Create;
using BookReview.Core.Entity;
using BookReview.Core.Repositories;
using BookReview.UnitTests.Fakes;
using NSubstitute;
using System.Reflection;

namespace BookReview.UnitTests.Application
{
    public class CreateBookHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Book_Successfully()
        {
            // Arrange
            const int expectedBookId = 1;
                   
            var bookRepository = Substitute.For<IBookRepository>();
                     
            bookRepository
                .When(repo => repo.AddAsync(Arg.Any<Book>()))
                .Do(callInfo =>
                {
                    var bookArg = callInfo.Arg<Book>();

                    // Utiliza reflection para definir o valor de Id, ignorando o setter inacessível
                    var idProperty = bookArg.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.Public);

                    var setMethod = idProperty?.GetSetMethod(true);
                    if (setMethod != null)
                    {
                        setMethod.Invoke(bookArg, new object[] { expectedBookId });
                    }
                });
                      
            bookRepository.SaveChangesAsync().Returns(Task.CompletedTask);
                  
            var command = FakeDataHelper.CreateFakeBookCommand();
                   
            var handler = new CreateBookCommandHandler(bookRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedBookId, result.Data);
        }
    }
}
