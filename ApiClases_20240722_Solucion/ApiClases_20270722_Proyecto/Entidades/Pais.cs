﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClases_20270722_Proyecto.Entidades
{
    [Table("Paises")] 
    public class Pais
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        [StringLength(100)] 
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)] 
        public string Divisa { get; set; }


        [Required]
        [StringLength(3)] 
        public string Iso3 { get; set; }

    }
}
