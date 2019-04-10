namespace Cinema.DataProcessor
{
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2:F2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            MovieDto[] deserializedMovies = JsonConvert.DeserializeObject<MovieDto[]>(jsonString);

            List<Movie> movies = new List<Movie>();
            foreach (var dto in deserializedMovies)
            {
                bool isGenreValid = Enum.TryParse(dto.Genre, out Genre genre);
                Movie movie = movies.FirstOrDefault(m => m.Title == dto.Title);
                if (!IsValid(dto) || movie != null || !isGenreValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                movie = new Movie()
                {
                    Title = dto.Title,
                    Genre = genre,
                    Duration = dto.Duration,
                    Rating = dto.Rating,
                    Director = dto.Director
                };

                movies.Add(movie);

                sb.AppendLine(String.Format(SuccessfulImportMovie, movie.Title, movie.Genre.ToString(), movie.Rating));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            HallSeatDto[] deserializedHalls = JsonConvert.DeserializeObject<HallSeatDto[]>(jsonString);

            List<Hall> halls = new List<Hall>();
            foreach (var dto in deserializedHalls)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Hall hall = new Hall()
                {
                    Name = dto.Name,
                    Is4Dx = dto.Is4Dx,
                    Is3D = dto.Is3D
                };

                for (int i = 0; i < dto.Seats; i++)
                {
                    hall.Seats.Add(new Seat());
                }

                string projectionType = "";
                if (hall.Is3D == true && hall.Is4Dx == true)
                {
                    projectionType = "4Dx/3D";
                }
                else if (hall.Is3D == true && hall.Is4Dx == false)
                {
                    projectionType = "3D";
                }
                else if (hall.Is3D == false && hall.Is4Dx == true)
                {
                    projectionType = "4Dx";
                }
                else
                {
                    projectionType = "Normal";
                }

                halls.Add(hall);
                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
            }

            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ProjectionDto[]), new XmlRootAttribute("Projections"));

            ProjectionDto[] deserializedProjections =
                (ProjectionDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Projection> projections = new List<Projection>();
            foreach (var dto in deserializedProjections)
            {
                Movie movie = context.Movies.Find(dto.MovieId);
                Hall hall = context.Halls.Find(dto.HallId);

                if (movie == null || hall == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Projection projection = new Projection()
                {
                    Hall = hall,
                    Movie = movie,
                    DateTime = DateTime.ParseExact(dto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                };

                projections.Add(projection);
                sb.AppendLine(String.Format(SuccessfulImportProjection, movie.Title,
                    projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(CustomerTicketDto[]), new XmlRootAttribute("Customers"));

            CustomerTicketDto[] deserializedCustomers =
                (CustomerTicketDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Customer> customers = new List<Customer>();
            foreach (var dto in deserializedCustomers)
            {
                if (!IsValid(dto) || !dto.Tickets.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Customer customer = new Customer()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    Balance = dto.Balance
                };

                bool hasInvalidData = false;
                foreach (var dtoTicket in dto.Tickets)
                {
                    Projection projection = context.Projections.Find(dtoTicket.ProjectionId);
                    if (projection == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidData = true;
                        break;
                    }

                    Ticket ticket = new Ticket()
                    {
                        Projection = projection,
                        Price = dtoTicket.Price
                    };

                    customer.Tickets.Add(ticket);
                }

                if (!hasInvalidData)
                {
                    customers.Add(customer);
                    sb.AppendLine(String.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName,
                        customer.Tickets.Count));
                }
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            ValidationContext context = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();

            bool result = Validator.TryValidateObject(obj, context, results, true);

            return result;
        }
    }
}