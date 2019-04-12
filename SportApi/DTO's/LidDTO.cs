﻿using ProjectG05.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace SportApi.DTO_s
{
    public class LidDTO
    {
        [Required]
        public string Voornaam { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public string StraatNaam { get; set; }

        [Required]
        public string Huisnummer { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Stad { get; set; }

        [Required]
        public string TelefoonNummer { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Geboortedatum { get; set; }

        [Required]
        public String Nationaliteit { get; set; }

        [Required]
        public String EmailOuder { get; set; }

        [Required]
        public String RijksregisterNummer { get; set; }

        [Required]
        public String GeborenTe { get; set; }

        [Required]
        public string Geslacht { get; set; }

        public DateTime InschrijvingsDatum { get; set; }

        public int Graad { get; set; }
    }
}