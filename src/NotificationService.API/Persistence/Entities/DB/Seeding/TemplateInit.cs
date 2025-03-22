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
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    font-size: 16px;
                                    color: #333;
                                }
                                p {
                                    margin: 0 0 8px 0;
                                }
                            </style>
                        </head>
                        <body>
                            <p style=""padding-bottom: 16px;"">Dobrý den <strong>{name}</strong>,</p>

                            <p>Vítejte v našem sportcentru! Máme velkou radost, že jste se rozhodli stát se součástí naší komunity.</p>
                            <p>Abyste mohli naplno využívat všech možností, doporučujeme se přihlásit a prozkoumat svůj nový účet.</p>
                            <p>Pokud budete mít jakékoli otázky nebo budete potřebovat pomoc, neváhejte nás kontaktovat.</p>
                            <p>Děkujeme za registraci a přejeme mnoho skvělých zážitků!</p>

                            <p style=""padding-top: 16px;"">S pozdravem,<br/>
                            <em>Tým podpory zákazníků</em></p>
                        </body>
                    </html>"
        });
        templates.Add(new Template
        {
            Id = 2,
            Name = "Verification",
            Subject = "Ověření e-mailu",
            Text = @"
                    <html>
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    font-size: 16px;
                                    color: #333;
                                }
                                p {
                                    margin: 0 0 8px 0;
                                }
                            </style>
                        </head>
                        <body>
                            <p style=""padding-bottom: 16px;"">Dobrý den <strong>{name}</strong>,</p>

                            <p>Prosíme o ověření vaší e-mailové adresy kliknutím na následující odkaz:</p>
                            <p><a href=""{link}"" style=""color: #1a73e8;"">{link}</a></p>
                            <p>Pokud jste tuto žádost neodeslali, můžete tento e-mail klidně ignorovat.</p>

                            <p>Děkujeme,</p>
                            <p style=""padding-top: 16px;"">S pozdravem,<br/>
                            <em>Tým podpory zákazníků</em></p>
                        </body>
                    </html>"

        });
        templates.Add(new Template
        {
            Id = 3,
            Name = "PasswordReset",
            Subject = "Obnovení hesla",
            Text = @"
                    <html>
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    font-size: 16px;
                                    color: #333;
                                }
                                p {
                                    margin: 0 0 8px 0;
                                }
                            </style>
                        </head>
                        <body>
                            <p style=""padding-bottom: 16px;"">Dobrý den <strong>{name}</strong>,</p>

                            <p>Pro obnovení hesla klikněte na následující odkaz:</p>
                            <p><a href=""{link}"" style=""color: #1a73e8;"">{link}</a></p>
                            <p>Pokud jste tuto žádost neodeslali, ignorujte tento e-mail.</p>

                            <p>Děkujeme,</p>
                            <p style=""padding-top: 16px;"">S pozdravem,<br/>
                            <em>Tým podpory zákazníků</em></p>
                        </body>
                    </html>"
        });
        templates.Add(new Template
        {
            Id = 4,
            Name = "ReservationConfirmation",
            Subject = "Potvrzení rezervace",
            Text = @"
                    <html>
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    font-size: 16px;
                                    color: #333;
                                }
                                p {
                                    margin: 0 0 8px 0;
                                }
                            </style>
                        </head>
                        <body>
                            <p style=""padding-bottom: 16px;"">Dobrý den <strong>{name}</strong>,</p>

                            <p>Vaše rezervace byla úspěšně vytvořena.</p>
                            <p><strong>Datum a čas:</strong> {datetime}</p>
                            <p>Pokud máte jakékoli dotazy nebo potřebujete změnit rezervaci, neváhejte nás kontaktovat.</p>

                            <p>Děkujeme,</p>
                            <p style=""padding-top: 16px;"">S pozdravem,<br/>
                            <em>Tým podpory zákazníků</em></p>
                        </body>
                    </html>"
        });
        templates.Add(new Template
        {
            Id = 5,
            Name = "ReservationCancellation",
            Subject = "Zrušení rezervace",
            Text = @"
                    <html>
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    font-size: 16px;
                                    color: #333;
                                }
                                p {
                                    margin: 0 0 8px 0;
                                }
                            </style>
                        </head>
                        <body>
                            <p style=""padding-bottom: 16px;"">Dobrý den <strong>{name}</strong>,</p>

                            <p>Vaše rezervace byla zrušena.</p>
                            <p><strong>Datum a čas:</strong> {datetime}</p>
                            <p>Pokud jste tuto žádost neodeslali, ignorujte tento e-mail.</p>

                            <p>Děkujeme,</p>
                            <p style=""padding-top: 16px;"">S pozdravem,<br/>
                            <em>Tým podpory zákazníků</em></p>
                        </body>
                    </html>"
        });
        templates.Add(new Template
        {
            Id = 6,
            Name = "Registration",
            Subject = "Registration Confirmation",
            Text = @"
                    <html>
                        <head>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    font-size: 16px;
                                    color: #333;
                                }
                                p {
                                    margin: 0 0 8px 0;
                                }
                            </style>
                        </head>
                        <body>
                            <p style=""padding-bottom: 16px;"">Hello <strong>{name}</strong>,</p>

                            <p>Welcome to our sports center! We are very excited that you have decided to become part of our community.</p>
                            <p>To fully utilize all the features, we recommend logging in and exploring your new account.</p>
                            <p>If you have any questions or need assistance, please do not hesitate to contact us.</p>

                            <p>Thank you for registering and we wish you many great experiences!</p>

                            <p style=""padding-top: 16px;"">Best regards,<br/>
                            <em>Customer Support Team</em></p>
                        </body>
                    </html>",
            Language = "en"
        });

        return templates;
    }
}
