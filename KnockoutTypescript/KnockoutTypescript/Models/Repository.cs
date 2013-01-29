using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutTypescript.Models
{
    /// <summary>
    /// Fake mail repository (used in 004 webmail)
    /// </summary>
    public class Repository
    {
        private static List<Mail> mails;
        private static List<Folder> folders;

        public Repository()
        {
            mails = new List<Mail> {
                new Mail 
                { 
                    Id = 1, 
                    From = "wiewiorka@lysa.skorka", 
                    To = "henki@henki.pl", 
                    Subject = "Życzenia noworoczne", 
                    Date = DateTime.Now,
                    FolderId = 1,
                    MessageContent = "Wszystkiego najlepszego w Nowym Roku 1545 życzą mieszkańcy Borów Tucholskich."
                },
                new Mail 
                { 
                    Id = 2, 
                    From = "benedykt16@vatican.va", 
                    To = "eugeniusz@kowalski.com", 
                    Subject = "Wyniki kontroli", 
                    Date = new DateTime(2012, 12, 12),
                    FolderId = 2
                },
                new Mail 
                { 
                    Id = 3, 
                    From = "eugeniusz@gmail.com", 
                    To = "all@kotlownia.pl", 
                    Subject = "Cała władza w ręce Rad!", 
                    Date = DateTime.Now ,
                    FolderId = 3
                },
                new Mail 
                { 
                    Id = 4, 
                    From = "teresa@starachowice.com", 
                    To = "eugeniusz@kowalski.com", 
                    Subject = "wieści z mleczarni", 
                    Date = new DateTime(2012, 12, 24),
                    FolderId = 4
                }
            };


            folders = new List<Folder>
            {
                new Folder 
                { 
                    Id = 1, 
                    Name = "Inbox",
                    Mails = new List<Mail> { mails[0] }
                },
                new Folder
                { 
                    Id = 2, 
                    Name = "Archive",
                    Mails = new List<Mail> { mails[1] }
                },
                new Folder
                {
                    Id = 3, 
                    Name = "Sent",
                    Mails = new List<Mail> { mails[2] }
                },
                new Folder
                {
                    Id = 4, 
                    Name = "Spam",
                    Mails = new List<Mail> { mails[3] }

                }
            };

        }

        public IEnumerable<Folder> Folders 
        {
            get { return folders; }
        }
        public IEnumerable<Mail> Mails 
        {
            get { return mails; }
        }
    }
}