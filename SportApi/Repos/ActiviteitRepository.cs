﻿using Microsoft.EntityFrameworkCore;
using ProjectG05.Data;
using ProjectG05.Models.Domain;
using SportApi.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportApi.Repos
{
    public class ActiviteitRepository : IActiviteit
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Activiteit> _activiteiten;
        private readonly DbSet<GebruikerActiviteit> _gebruikerActiviteit;
        private readonly IGebruiker _gebruikerRepository;

        #endregion Fields

        #region Constructors

        public ActiviteitRepository(ApplicationDbContext context)
        {
            _context = context;
            _activiteiten = _context.Activiteiten;
            _gebruikerActiviteit = _context.GebruikerActiviteit;
        }

        #endregion Constructors

        #region Methods

        public void Add(Activiteit activiteit)
        {
            activiteit.GebruikersApi.ForEach(a =>
            {
                _gebruikerActiviteit.Add(new GebruikerActiviteit(activiteit, a));
            });
            _activiteiten.Add(activiteit);
            SaveChanges();
        }

        public void Update(Activiteit activiteit)
        {
            //alle activiteitleden van deze activiteit verwijderen
            List<GebruikerActiviteit> alleActiviteitLeden = _gebruikerActiviteit.ToList();
            alleActiviteitLeden.ForEach(gebruikerActiviteit =>
            {
                if (gebruikerActiviteit.Activiteit == activiteit)
                {
                    _gebruikerActiviteit.Remove(gebruikerActiviteit);
                }
            });

            //alle activiteitleden van deze activiteit opnieuw toevoegen
            if (activiteit.GebruikersApi != null)
            {
                activiteit.GebruikersApi.ForEach(a =>
                {
                    _gebruikerActiviteit.Add(new GebruikerActiviteit(activiteit, a));
                });
            }

            _activiteiten.Update(activiteit);
            SaveChanges();
        }

        public void Delete(Activiteit activiteit)
        {
            _activiteiten.Remove(activiteit);
            List<GebruikerActiviteit> verwijderen = _gebruikerActiviteit.ToList();
            verwijderen.ForEach(a =>
            {
                if (a.Activiteit == activiteit)
                {
                    _gebruikerActiviteit.Remove(a);
                }
            });
            SaveChanges();
        }

        public IEnumerable<Activiteit> GetAll()
        {
            List<Activiteit> alleActiviteitsen = _activiteiten.ToList();
            alleActiviteitsen.ForEach(act =>
            {
                //    activiteit.GebruikersVoorActiviteit = new List<Gebruiker>();
                ////    _gebruikerActiviteit.Where(k => k.Activiteit == activiteit).Include(l => l.Activiteit).Include(l => l.Gebruiker).ToList().ForEach(t =>
                ////    {
                ////        activiteit.GebruikersVoorActiviteit.Add(t.Gebruiker);
                //});
                _gebruikerActiviteit.Where(a => a.Activiteit == act).Include(i => i.Activiteit).Include(t => t.Gebruiker).ToList().ForEach(t =>
                {
                    act.GebruikersVoorActiviteit = new List<int>();
                    if (t.Gebruiker.IdApi != 0)
                        act.GebruikersVoorActiviteit.Add(t.Gebruiker.IdApi);
                    else
                        act.GebruikersVoorActiviteit.Add(t.Gebruiker.Id);
                    //act.GebruikersApi.Add(t.Gebruiker);
                });
            });
                return alleActiviteitsen;
        }

        public Activiteit GetBy(int id)
        {
            Activiteit act = _activiteiten.SingleOrDefault(s => s.Id == id);

                {
                    act.GebruikersApi = new List<Gebruiker>();
                    _gebruikerActiviteit.Where(a => a.Activiteit == act).Include(i => i.Activiteit).Include(t => t.Gebruiker).ToList().ForEach(t =>
                    {
                        act.GebruikersVoorActiviteit = new List<int>();
                        //Gebruiker gebruiker = new Gebruiker(t.Gebruiker.Voornaam, t.Gebruiker.Naam, t.Gebruiker.Straatnaam, t.Gebruiker.Huisnummer, t.Gebruiker.Busnummer, t.Gebruiker.Postcode, t.Gebruiker.Stad, t.Gebruiker.Telefoonnummer, t.Gebruiker.Email, t.Gebruiker.GeboorteDatum, t.Gebruiker.Geslacht, t.Gebruiker.Type);
                        // Gebruiker gebruiker = _
                        //    if (t.Id != 0)
                        //        gebruiker.Id = t.Id;
                        //    act.GebruikersApi.Add(gebruiker);
                        //    if (gebruiker.Id != 0)
                        //          act.GebruikersVoorActiviteit.Add(gebruiker.Id);
                        //      else
                        if (t.Gebruiker.IdApi != 0)
                            act.GebruikersVoorActiviteit.Add(t.Gebruiker.IdApi);
                        else
                            act.GebruikersVoorActiviteit.Add(t.Gebruiker.Id);
                        act.GebruikersApi.Add(t.Gebruiker);
                    });
                }
          //      _activiteiten.SingleOrDefault(s => s.Id == id);
   //        if (act != null)
            //List<int> ids = new List<int>();
            //foreach(Gebruiker gebruiker in act.GebruikersApi)
            //{
            //    if (gebruiker.Id != 0)
            //        ids.Add(gebruiker.Id);
            //    else
            //        ids.Add(gebruiker.IdApi);
            //}
            //act.GebruikersVoorActiviteit = ids;

            return act;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion Methods
    }
}