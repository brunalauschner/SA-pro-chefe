//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace telaDeLogin
{
    using System;
    using System.Collections.Generic;
    
    public partial class endereco
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string uf { get; set; }
        public string cidade { get; set; }
        public int id { get; set; }
        public int id_clientsend { get; set; }
    
        public virtual clients clients { get; set; }
    }
}
