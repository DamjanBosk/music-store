namespace MusicStore.Service.Implementation
{
    using MusicStore.Domain.Domain;
    using MusicStore.Repository.Interface;
    using MusicStore.Service.Interface;
    using System;
    using System.Collections.Generic;

    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> repository;

        public ArtistService(IRepository<Artist> repository)
        {
            this.repository = repository;
        }

        public List<Artist> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public Artist GetById(Guid? id)
        {
            return repository.Get(id);
        }

        public Artist Create(Artist artist)
        {
            return repository.Insert(artist);
        }

        public Artist Update(Artist artist)
        {
            return repository.Update(artist);
        }

        public Artist Delete(Guid? id)
        {
            var artist = this.GetById(id);
            return repository.Delete(artist);
        }
    }
}
