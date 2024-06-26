﻿using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Models;

namespace ConciertosSoloApi.Data
{
    public class ConciertosSoloContext : DbContext
    {
        public ConciertosSoloContext(DbContextOptions<ConciertosSoloContext>
            options) : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Concierto> Conciertos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<PeticionEvento> Peticiones { get; set; }
        public DbSet<Peticion> ListaPeticiones { get; set; }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<UserPubli> PublicacionesUsuarios { get; set; }
        public DbSet<ArtistaConcierto> RelacionesConcierto { get; set; }
        public DbSet<ArtistaGenero> RelacionesGenero { get; set; }
    }
}
