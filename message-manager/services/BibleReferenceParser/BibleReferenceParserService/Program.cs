using BibleReferenceParser.Parsing;
using BibleReferenceParserService;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS for frontend calls
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();


app.MapPost("/api/parse", (ParseModel input) =>
{
    var result = new List<BibleReferenceParserService.Models.BibleReferenceRange>();
    if (input.Value != null)
    {
        var parsedList = Parser.Parse(input.Value);
        foreach (var referenceRange in parsedList)
        {
            var referenceRangeModel = BibleReferenceParserService.Models.BibleReferenceRange.From(referenceRange);
            result.Add(referenceRangeModel);
        }
    }
    return Results.Ok(result);
});

app.MapPost("api/tostring", (BibleReferenceParserService.Models.BibleReferenceRange range) =>
{
    var result = range.ToFriendlyString();
    return Results.Ok(result);
});

app.Run();

record ParseModel(string Value);
