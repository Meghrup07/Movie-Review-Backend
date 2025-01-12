using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts.Movie;
using API.Contracts.MovieReview;
using API.Contracts.User;
using API.Domain.Models;
using AutoMapper;

namespace API.Comman
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Entity TO DTO
            CreateMap<AppUser, UserDetailsDTO>();
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieReview, MovieReviewDTO>();

            // DTO TO Entity
            CreateMap<RegisterDTO, AppUser>();

            CreateMap<AddMovieDTO, Movie>();
            CreateMap<UpdateMovieDTO, Movie>();

            CreateMap<AddMovieReviewDTO, MovieReview>();
            CreateMap<UpdateMovieReviewDTO, MovieReview>();
        }
    }
}