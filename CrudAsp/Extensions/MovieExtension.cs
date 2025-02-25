
using CrudAsp.Models.app;
using CrudAsp.resource.response;
using CrudAsp.resource.request;
using CrudAsp.Models;
namespace CrudAsp.Extensions;

public static class MovieExntension
{
    public static CinemaResponse ToCinemaResponse(this Cinema cinema)
    => new CinemaResponse
    {
        Id = cinema.Id,
        CinemaName = cinema.CinemaName,
        Location = cinema.Location,
        Halls = cinema.Halls?.Select(h => new HallResponse
        {
            Id      = h.Id,
            HallName = h.HallName,
            SeatCapacity = h.SeatCapacity,
            Shows = h.Shows?.Select(s => new ShowResponse
            {
                MovieId = s.MovieId,
                HallId = s.HallId,
                ShowDate = s.ShowDate,
                TicketPrice = s.TicketPrice
            }).ToList()
        }).ToList()
    };

    public static ShowRequest OnShowRequest(this ShowRequest showRequest)
    => new ShowRequest
    {
        MovieId         = showRequest.MovieId,
        HallId          = showRequest.HallId,
        ShowDate        = showRequest.ShowDate,
        TicketPrice     = showRequest.TicketPrice
    };

    public static MovieResponse ToMovieResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Id          = movie.Id,
            Title       = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            EndDate     = movie.EndDate
        };
    }

    public static UserResponseDTO ToUserResponse(this Users users)
    {
        return new UserResponseDTO
        {
            Id        = users.Id,
            Email      = users.Email,
            FirstName = users.FirstName,
            MiddleName = users.MiddleName,
            LastName = users.LastName,
            Age = users.Age
        };
    }
}
