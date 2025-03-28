﻿using BookReview.Application.Models;
using BookReview.Application.ViewModel;
using MediatR;

namespace BookReview.Application.Queries.UserQueries.GetAll
{
    public class GetAllUsersQuery : IRequest<ResultViewModel<List<UserViewModel>>>
    {
        public GetAllUsersQuery(string? fullName)
        {
            FullName = fullName;
        }

        public string? FullName { get; private set; }
    }
}
