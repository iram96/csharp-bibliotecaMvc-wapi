﻿using csharp_bibliotecaMvc.Models;



namespace csharp_bibliotecaMvc.Data
{
    public class DbInitializer
    {
        public static void Initialize(BibliotecaContext context)
        {
            context.Database.EnsureCreated();  //crea il db se non c'è


            if (context.Utentes.Any())
            {
                return;   // DB has been seeded
            }

            var utenti = new Utente[]
            {
            new Utente{Nome="Carlo",Cognome="Magno",Telefono="0153", Email="carlo@email.it",Password="123"},
            new Utente{Nome="Anna",Cognome="Rossi",Telefono="4567", Email="anna@email.it",Password="123", }
            };

            foreach (Utente u in utenti)
            {
                context.Utentes.Add(u);
            }
            context.SaveChanges();

            var autore = new Autore[]
        {
            new Autore{AutoreId = 1, Nome="Carlo",Cognome="Magno" },
            new Autore{AutoreId = 2,Nome="Albert",Cognome="Jo" }

        };

            foreach (Autore a in autore)
            {
                context.Autoris.Add(a);
            }
            context.SaveChanges();

            // per popolare la tabella ponte
            var Magno = context.Autoris.Where(a => a.Cognome == "Magno").First();
            var Jo = context.Autoris.Where(a => a.Cognome == "Magno").First();

            var libri = new Libro[]
           {
            new Libro{Titolo="I promessi sposi",Scaffale="s001", Stato=Stato.Disponibile, Autori = new List<Autore>{Magno}},
            new Libro{Titolo="I Malavoglia",Scaffale="s002", Stato=Stato.Disponibile, Autori = new List<Autore>{Magno}},
            new Libro{Titolo="Dune",Scaffale="s001", Stato=Stato.Prestito, Autori = new List<Autore>{Jo}},

           };

            foreach (Libro l in libri)
            {
                context.Libros.Add(l);
            }
            context.SaveChanges();



            var prestiti = new Prestito[]
         {
            new Prestito{PrestitoID=1,UtenteID=1,LibroID=3,Al=DateTime.Parse("2022-06-05"),Dal=DateTime.Parse("2022-08-05") }

         };

            foreach (Prestito a in prestiti)
            {
                context.Prestitis.Add(a);
            }
            context.SaveChanges();




        }

    }
}
