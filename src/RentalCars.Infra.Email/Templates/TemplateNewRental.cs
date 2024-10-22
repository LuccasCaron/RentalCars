using RentalCars.Domain.Entities;

namespace RentalCars.Infra.Email.Templates;

public static class TemplateNewRental
{

    public static string GenerateRentalConfirmation(string nome, Rental rental, Car car)
    {
        return $@"
        <html>
            <body>
                <h1>Olá {nome},</h1>
                <p>Recebemos sua reserva na RentalCars!</p>
                <h2>Segue os detalhes do pedido:</h2>
                <ul>
                    <li><strong>Modelo:</strong> {car.Model}</li>
                    <li><strong>Ano:</strong> {car.Year}</li>
                    <li><strong>Marca:</strong> {car.Brand}</li>
                    <li><strong>Valor da diária:</strong> {rental.AppliedDailyPrice.ToString("C")}</li>
                    <li><strong>Data de início:</strong> {rental.RentalStartDate.ToShortDateString()}</li>
                    <li><strong>Data prevista de entrega:</strong> {rental.RentalEndDate.ToShortDateString()}</li>
                </ul>
                <p>Obrigado por escolher a RentalCars!</p>
            </body>
        </html>";
    }

}
