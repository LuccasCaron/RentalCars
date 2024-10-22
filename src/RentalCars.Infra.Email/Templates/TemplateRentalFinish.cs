using RentalCars.Infra.Email.DTOs;

namespace RentalCars.Infra.Email.Templates;

public static class TemplateRentalFinish
{

    public static string GenerateRentalFinish(RentalFinishDto rentalFinishDto)
    {
        return $@"
        <html>
            <body>
                <h1>Olá {rentalFinishDto.Nome},</h1>
                <p>Obrigado por escolher a RentalCars!</p>
                <h2>Seu aluguel foi finalizado com sucesso!</h2>
                <p>Segue os detalhes do seu aluguel:</p>
                <ul>
                    <li><strong>Modelo:</strong> {rentalFinishDto.Car.Model}</li>
                    <li><strong>Ano:</strong> {rentalFinishDto.Car.Year}</li>
                    <li><strong>Marca:</strong> {rentalFinishDto.Car.Brand}</li>
                    <li><strong>Valor total:</strong> {rentalFinishDto.TotalToPay}</li>
                    <li><strong>Data de devolução:</strong> {DateTime.Now.ToShortDateString()}</li>
                </ul>
                <p>Esperamos que você tenha desfrutado da sua experiência conosco!</p>
                <p>Até a próxima!</p>
            </body>
        </html>";
    }

}
