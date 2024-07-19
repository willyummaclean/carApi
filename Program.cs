using System.Runtime.Serialization;
using CarbuilderAPI.Models;
using CarbuilderAPI.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
              {
                  options.AllowAnyOrigin();
                  options.AllowAnyMethod();
                  options.AllowAnyHeader();
              });
}
List<Interior> interiors = new List<Interior>()
{
     new Interior()
    {
        Id = 1,
        Price = 32.11M,
        Material = "Beige Fabric"
    },
    new Interior()
    {
        Id = 2,
        Price = 45.32M,
        Material = "Charcoal Fabric"
    },
    new Interior()
    {
        Id = 3,
        Price = 61.77M,
        Material = "White Leather"
    },
     new Interior()
    {
        Id = 4,
        Price = 88.11M,
        Material = "Black Leather"
    }

};
List<Paint> paints = new List<Paint>()
{
     new Paint()
    {
        Id = 1,
        Price = 32.11M,
        Color = "Silver"
    },
    new Paint()
    {
        Id = 2,
        Price = 45.32M,
        Color = "Midnight Blue"
    },
    new Paint()
    {
        Id = 3,
        Price = 61.77M,
        Color = "Firebrick Red"
    },
     new Paint()
    {
        Id = 4,
        Price = 88.11M,
        Color = "Spring Green"
    }

};

List<Technology> technologies = new List<Technology>()
{
    new Technology()
    {
        Id = 1,
        Price = 1000.00M,
        Package = "Basic Package",
    },
    new Technology()
    {
        Id = 2,
        Price = 1500.00M,
        Package = "Navigation Package",
    },
    new Technology()
    {
        Id = 3,
        Price = 2000.00M,
        Package = "Visibility Package",
    },
    new Technology()
    {
        Id = 4,
        Price = 2500.00M,
        Package = "Ultra Package",
    }
};

List<Wheels> wheels = new List<Wheels>()
{
    new Wheels()
    {
        Id = 1,
        Price = 32.11M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2,
        Price = 45.32M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 61.77M,
        Style = "18-inch Pair Spoke Silver"
    },
     new Wheels()
    {
        Id = 4,
        Price = 88.11M,
        Style = "18-inch Pair Spoke Black"
    }

};

List<Order> orders = new List<Order>()
{
    new Order()
    {
        Id = 1,
        WheelId = 2,
        TechnologyId = 3,
        InteriorId = 2,
        PaintId = 4
    }
};

app.MapGet("/wheels", () =>
{
    return Results.Ok(wheels.Select(w => new WheelsDTO
    {
        Id = w.Id,
        Price = w.Price,
        Style = w.Style

    })
    );

});

app.MapGet("/interiors", () =>
{
    return Results.Ok(interiors.Select(i => new InteriorDTO
    {
        Id = i.Id,
        Price = i.Price,
        Material = i.Material
    }));
});

app.MapGet("/technologies", () =>
{
    return Results.Ok(technologies.Select(t => new TechnologyDTO
    {
        Id = t.Id,
        Price = t.Price,
        Package = t.Package
    }));
});

app.MapGet("/paintcolors", () =>
{
    return Results.Ok(paints.Select(p => new PaintDTO
    {
        Id = p.Id,
        Price = p.Price,
        Color = p.Color
    }));
});
app.MapGet("/orders", () =>
{
    return Results.Ok(orders.Select(o => new OrderDTO
    {
        Id = o.Id,
        InteriorId = o.InteriorId,
        TechnologyId = o.TechnologyId,
        PaintId = o.PaintId,
        WheelId = o.WheelId,
        Timestamp = o.Timestamp,
        Interior = interiors.Where(i => i.Id == o.InteriorId).Select(i =>
        new InteriorDTO
        {
            Id = i.Id,
            Price = i.Price,
            Material = i.Material
        })
        .FirstOrDefault(),
        Paint = paints.Where(p => p.Id == o.PaintId).Select(p =>
        new PaintDTO
        {
            Id = p.Id,
            Price = p.Price,
            Color = p.Color
        })
        .FirstOrDefault(),
        Wheel = wheels.Where(w => w.Id == o.WheelId).Select(w =>
        new WheelsDTO
        {
            Id = w.Id,
            Price = w.Price,
            Style = w.Style
        })
        .FirstOrDefault(),
        Technology = technologies.Where(t => t.Id == o.TechnologyId).Select(t =>
        new TechnologyDTO
        {
            Id = t.Id,
            Price = t.Price,
            Package = t.Package
        })
        .FirstOrDefault()

    }));
});

app.MapPost("/orders", (Order order) =>
{
    order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
    order.Timestamp = DateTime.Now;
    orders.Add(order);
    return Results.Ok(orders.Where(o => o.Id == order.Id).Select(o => new OrderDTO
    {
        Id = o.Id,
        Timestamp = o.Timestamp,
        InteriorId = o.InteriorId,
        TechnologyId = o.TechnologyId,
        PaintId = o.PaintId,
        WheelId = o.WheelId,
        Completed = o.Timestamp,
        Interior = interiors.Where(i => i.Id == o.InteriorId).Select(i =>
        new InteriorDTO
        {
            Id = i.Id,
            Price = i.Price,
            Material = i.Material
        })
        .FirstOrDefault(),
        Paint = paints.Where(p => p.Id == o.PaintId).Select(p =>
        new PaintDTO
        {
            Id = p.Id,
            Price = p.Price,
            Color = p.Color
        })
        .FirstOrDefault(),
        Wheel = wheels.Where(w => w.Id == o.WheelId).Select(w =>
        new WheelsDTO
        {
            Id = w.Id,
            Price = w.Price,
            Style = w.Style
        })
        .FirstOrDefault(),
        Technology = technologies.Where(t => t.Id == o.TechnologyId).Select(t =>
        new TechnologyDTO
        {
            Id = t.Id,
            Price = t.Price,
            Package = t.Package
        })
        .FirstOrDefault()

    }));
});

app.MapPost("/orders/orderId/fulfill", (int orderId, List<Order> orders) =>
{
    var order = orders.FirstOrDefault(o => o.Id == orderId);
    if (order != null)
    {
        order.Completed = DateTime.Now;
        return Results.Ok(order);
    }
    return Results.NotFound();
});

app.Run();

