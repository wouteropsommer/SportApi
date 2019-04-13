﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectG05.Models.Domain;
using SportApi.DTO_s;
using SportApi.IRepos;

namespace SportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesController : ControllerBase
    {

        private ILes _lesRepository;
        private IGebruiker _gebruikerRepository;

        public LesController(ILes repo, IGebruiker gebruikerRepository)
        {
            _lesRepository = repo;
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: api/Les
        [HttpGet]
        public IEnumerable<Les> Get()
        {
            return _lesRepository.GetAll();
        }

        // GET: api/Les/5
        [HttpGet("{id}")]
        public ActionResult<Les> Get(int id)
        {
            Les l = _lesRepository.GetBy(id);
            if (l == null) return BadRequest("De les kon niet worden gevonden");
            return l;
        }

        // POST: api/Les
        [HttpPost]
        public ActionResult<Les> Post(LesDTO DTO)
        {
            try
            {
                Gebruiker lesgever = _gebruikerRepository.GetBy(DTO.LesgeverId);
                if (lesgever == null) return BadRequest("Lesgever kon niet worden gevonden");
                if(DTO.Leden == null)
                {
                    DTO.Leden = new List<Lid>();
                }
                Les l = new Les(lesgever, DTO.StartUur, DTO.Duur, DTO.Weekdag, DTO.Leden);
                _lesRepository.Add(l);
                _lesRepository.SaveChanges();
                return l;
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Les/5
        [HttpPut("{id}")]
        public ActionResult<Les> Put(int id, LesDTO DTO)
        {
            try
            {
                Les l = _lesRepository.GetBy(id);
                if (l == null) return BadRequest("Lesgever kon niet worden gevonden");
                Gebruiker lesgever = _gebruikerRepository.GetBy(DTO.LesgeverId);
                if (lesgever == null) return BadRequest("Lesgever kon niet worden gevonden");
                if (DTO.Leden == null)
                {
                    DTO.Leden = new List<Lid>();
                }
                l.Lesgever = lesgever;
                l.StartUur = DTO.StartUur;
                l.Duur = DTO.Duur;
                l.Weekdag = DTO.Weekdag;
                l.LedenVoorLes = DTO.Leden;
                _lesRepository.Update(l);
                _lesRepository.SaveChanges();
                return l;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Les> Delete(int id)
        {
            Les les = _lesRepository.GetBy(id);
            if (les == null) return BadRequest("Les kon niet worden gevonden!");
            _lesRepository.Delete(les);
            return les;
        }
    }
}
