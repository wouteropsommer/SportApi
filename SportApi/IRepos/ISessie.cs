﻿using System;
using System.Collections.Generic;

namespace ProjectG05.Models.Domain
{
    public interface ISessie
    {
        #region Methods

        void Update(Sessie sessie);
        void Add(Sessie sessie);

        void Delete(Sessie sessie);

        IEnumerable<Sessie> GetAll();

        Sessie GetBy(int id);

        void SaveChanges();

        Sessie GetCurrentSession();

        Sessie AddAanwezige(Sessie s, Gebruiker g);

        Sessie RemoveAanwezige(Sessie s, Gebruiker g);

        Boolean noCurrentSession();

        #endregion Methods
    }
}