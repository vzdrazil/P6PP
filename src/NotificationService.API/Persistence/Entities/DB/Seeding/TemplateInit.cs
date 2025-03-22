using NotificationService.API.Persistence.Entities.DB.Models;

namespace NotificationService.API.Persistence.Entities.DB.Seeding;

public class TemplateInit
{
    public IList<Template> GetTemplates()
    {
        List<Template> templates = new List<Template>();
        templates.Add(new Template
        {
            Id = 1,
            Name = "Registration",
            Subject = "Potvrzení registrace",
            Text = @"
                    <html>
                        <body style=""font-family: Arial, sans-serif; font-size: 16px; color: #333;"">
                            <p>Dobrý den <strong>{name}</strong>,</p>
                            <p>&nbsp;</p>
                            <p>Vítejte v našem sportcentru! Máme velkou radost, že jste se rozhodli stát se součástí naší komunity.</p>
                            <p>Abyste mohli naplno využívat všech možností, doporučujeme se přihlásit a prozkoumat svůj nový účet.</p>
                            <p>Pokud budete mít jakékoli otázky nebo budete potřebovat pomoc, neváhejte nás kontaktovat.</p>
                            <p>Děkujeme za registraci a přejeme mnoho skvělých zážitků!</p>
                            <p>S pozdravem,<br/>
                            <em>Tým podpory zákazníků</em></p>
                        </body>
                    </html>" 
        });
        templates.Add(new Template
        {
            Id = 2,
            Name = "Verification",
            Subject = "Ověření e-mailu",
            Text = "Dobrý den {name},\n\nprosíme o ověření vaší e-mailové adresy kliknutím na následující odkaz:\n" +
                   "{link}\n\n" +
                   "Pokud jste tuto žádost neodeslali, ignorujte tento e-mail.\n\n" +
                   "Děkujeme,\n" +
                   "Tým podpory zákazníků",
        });
        templates.Add(new Template
        {
            Id = 3,
            Name = "PasswordReset",
            Subject = "Obnovení hesla",
            Text = "Dobrý den {name},\n\npro obnovení hesla klikněte na následující odkaz:\n" +
                   "{link}\n\n" +
                   "Pokud jste tuto žádost neodeslali, ignorujte tento e-mail.\n\n" +
                   "Děkujeme,\n" +
                   "Tým podpory zákazníků",
        });
        templates.Add(new Template
        {
            Id = 4,
            Name = "ReservationConfirmation",
            Subject = "Potvrzení rezervace",
            Text = "Dobrý den {name},\n\nvaše rezervace byla úspěšně vytvořena.\n" +
                   "Datum a čas: {datetime}\n\n" +
                   "Pokud máte jakékoli dotazy nebo potřebujete změnit rezervaci, neváhejte nás kontaktovat.\n\n" +
                   "Děkujeme,\n" +
                   "Tým podpory zákazníků",
        });
        templates.Add(new Template
        {
            Id = 5,
            Name = "ReservationCancellation",
            Subject = "Zrušení rezervace",
            Text = "Dobrý den {name},\n\nvaše rezervace byla zrušena.\n" +
                   "Datum a čas: {datetime}\n\n" +
                   "Pokud jste tuto žádost neodeslali, ignorujte tento e-mail.\n\n" +
                   "Děkujeme,\n" +
                   "Tým podpory zákazníků",
        });
        templates.Add(new Template
        {
            Id = 6,
            Name = "Registration",
            Subject = "Registration Confirmation",
            Text = "Hello {name},\n\nwelcome to our sports center." +
                   "We are very excited that you have decided to become part of our community.\n" +
                   "To fully utilize all the features, we recommend logging in and exploring your new account.\n" +
                   "If you have any questions or need assistance, please do not hesitate to contact us.\n" +
                   "Thank you for registering and we wish you many great experiences!\n\n" +
                   "Customer Support Team",
        });
      
        return templates;
    }
}