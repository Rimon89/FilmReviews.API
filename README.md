# Movie Reviews (API)

En applikation skriven i .net core web api, där bland annat sökta filmer eller recensioner till den specifika filmen sparas i databasen.

## Installation

Efter att man har laddat hem projektet så behöver man ändra i connection strängen i appsettings.json. 
Nästa steg blir att köra **add-migration** för att updatera databasen så att den håller sig i synk med applikationens data modell.
Slutligen köra **update-database**.

## Struktur

Jag har använt mig av Entity Framework för att bygga min databas, med så kallad *code first* metod.
Andra metoder jag har använt är bland annat mediator design pattern, open/closed principle och dependency injection som bygger på dependency inversion principle.

Huvudmålet med *mediator* är att inte tillåta direkt kommunikation mellan mina metoder i controller och databasen. Istället tvingar jag dem kommunicera 
endast genom en mediator eller en medlare.

```cs
    public class Create
    {
        public class Command : IRequest<bool>
        {
            public Movie Movie { get; set; }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IMovieRepository _movieRepo;

            public Handler(IMovieRepository movieRepo)
            {
                _movieRepo = movieRepo;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _movieRepo.Create(request.Movie);
                return success;
            }
        }
    }
```

Fler fördelar med mediator pattern är open/closed principle. Här kan du lägga till flera mediators, i form av query eller command, utan att ändra befintlig kod.

Jag använder dependency injection för att injecta IMediator interface:t i mina controller.

```cs
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

    ...
```

## Testing

Jag har skapat några tester med hjälp av xunit och Moq.

Ett exempel:

```cs
     [Fact]
     public async Task GetReviewAsync_ReturnsOkWithTheReview_WhenReviewIsFoundInDb()
     {
         var id = Guid.NewGuid();
         var review = new Review
         {
            Id = id,
            Name = "a",
            MovieReview = "b",
            MovieTitle = "c",
            Author = "d",
            ImdbId = "e",
            Rating = 1,
            ReviewDate = DateTime.Today
         };
         _mediator.Setup(m => m.Send(It.IsAny<Services.Reviews.Details.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(review);

         var result = await _sut.GetReviewAsync(id);

         Assert.NotNull(result);
         var actionResult = Assert.IsType<OkObjectResult>(result);
         var model = Assert.IsAssignableFrom<Review>(actionResult.Value);
         Assert.Equal(id, model.Id);
   }
```
Här testar jag så att metoden GetReviewAsync returnerar Ok med review som hittades i databasen.

Jag börjar med att skapa ett id som är en guid, samt en review med dummy data och mock:ar upp min send metod. Jag skickar min nyligen skapade id i metoden GetReviewAsync och sparar
returvärdet i variabeln result.

Testar sedan:

- Så att result inte är null. 
- Att result är av typen Ok.
- Att review jag får tillbaks matchar id jag matade in i metoden GetReviewAsync. 

## Project Link

[Movie Reviews API](https://github.com/Rimon89/FilmReviews.API)