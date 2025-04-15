using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Books.Commands.CreateBookCommands;
using Application.Features.Books.Commands.DeleteBookCommands;
using Application.Features.Books.Commands.UpdateBookCommands;
using Application.Features.Loans.Commands.NewFolder;
using Application.Features.Loans.Commands.UpdateCommand;
using Application.Features.Users.Commands.DeleteUserCommands;
using Application.Features.Users.Commands.UpdateUserCommands;
using AutoMapper;
using Domain.Entities;
namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<Book, BookDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Loan, LoanDTO>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LoanId)) // Asegura que el ID se copie
            .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate));
            #endregion

            #region Commands
            //Books
            CreateMap<CreateBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();
            CreateMap<DeleteBookCommand, Book>();
            //Loans
            CreateMap<CreateLoanCommand, Loan>();
            CreateMap<UpdateLoanCommand, Loan>();
            //Users
            CreateMap<UpdateUserCommand, User>();
            CreateMap<DeleteUserCommand, User>();
            #endregion
        }
    }
}
