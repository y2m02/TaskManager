﻿namespace TaskManagerApp.Models.ViewModels
{
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        
        //[DisplayName("Nombre")]
        //[Required(ErrorMessage = "Campo {0} requerido.")]
        public string Name { get; set; }
    }
}