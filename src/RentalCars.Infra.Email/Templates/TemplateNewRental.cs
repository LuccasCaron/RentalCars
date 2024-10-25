using RentalCars.Domain.Entities;
using RentalCars.Infra.Email.DTOs;

namespace RentalCars.Infra.Email.Templates;

public static class TemplateNewRental
{

    public static string GenerateRentalConfirmation(NewRentalDto newRentalDto)
    {
        return $@"
        <html>
            <body>
                <h1>Olá {newRentalDto.Nome},</h1>
                <p>Recebemos sua reserva na RentalCars!</p>
                <h2>Segue os detalhes do pedido:</h2>
                <ul>
                    <li><strong>Modelo:</strong> {newRentalDto.Car.Model}</li>
                    <li><strong>Ano:</strong> {newRentalDto.Car.Year}</li>
                    <li><strong>Marca:</strong> {newRentalDto.Car.Brand}</li>
                    <li><strong>Valor da diária:</strong> {newRentalDto.Rental.AppliedDailyPrice.ToString("C")}</li>
                <li><strong>Data de início:</strong> {newRentalDto.Rental.RentalStartDate.ToString("dd/MM/yyyy")}</li>
                <li><strong>Data prevista de entrega:</strong> {newRentalDto.Rental.RentalEndDate.ToString("dd/MM/yyyy")}</li>
                </ul>
                <p>Obrigado por escolher a RentalCars!</p>
            </body>
        </html>";
    }

}
