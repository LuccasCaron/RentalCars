using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RentalCars.Infra.Data.Context;
using RentalCars.Infra.Email.Services;
using RentalCars.Infra.Email.Templates;
using RentalCars.Messaging.Events;

namespace RentalCars.Messaging.Consumers;

public class RentalCreatedEventConsumer : IConsumer<RentalCreatedEvent>
{

    #region Properties

    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<RentalCreatedEventConsumer> _logger;
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor

    public RentalCreatedEventConsumer(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        ILogger<RentalCreatedEventConsumer> logger,
        IEmailService emailService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    #endregion

    public async Task Consume(ConsumeContext<RentalCreatedEvent> context)
    {
        var rentalId = context.Message.RentalId;

        var rental = await _context.Rentals.FindAsync(rentalId)
                                           .ConfigureAwait(false);

        if (rental is null)
        {
            _logger.LogWarning($"Aluguel com ID {rentalId} não encontrado.");
            return; 
        }

        var car = await _context.Cars.FindAsync(rental.CarId)
                                     .ConfigureAwait(false);

        _logger.LogInformation($"Processando alguel, data prevista para entrega: {rental.RentalEndDate}");

        var user = await _userManager.FindByIdAsync(rental.UserId.ToString());

        _emailService.SendEmail(
            user.Email,
            "Sua reserva na RentalCars foi realizada com sucesso.",
            TemplateNewRental.GenerateRentalConfirmation(user.UserName, rental, car));

        _logger.LogInformation("E-mail enviado com sucesso.");
    }

}
