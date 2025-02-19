﻿namespace BookReview.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public static ResultViewModel Sucess()
           => new();

        public static ResultViewModel Sucess(string message)
          => new(true, message);

        public static ResultViewModel Error(string message)
            => new(false, message);
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSuccees = true, string message = "")
            : base(isSuccees, message)
        {
            Data = data;
        }

        public T? Data { get; private set; }

        public static ResultViewModel<T> Sucess(T data)
           => new(data);

        public static ResultViewModel<T> Error(string message)
            => new(default, false, message);
    }
}
