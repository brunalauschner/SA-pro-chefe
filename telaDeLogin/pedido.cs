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
    
    public partial class pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pedido()
        {
            this.itens_pedido = new HashSet<itens_pedido>();
        }
    
        public int id { get; set; }
        public System.DateTime data { get; set; }
        public Nullable<int> id_mesa { get; set; }
        public Nullable<int> id_cliente_ped { get; set; }
        public Nullable<bool> finalizado { get; set; }
        public Nullable<int> id_entregador { get; set; }
    
        public virtual clients clients { get; set; }
        public virtual entregador entregador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<itens_pedido> itens_pedido { get; set; }
        public virtual mesa mesa { get; set; }
    }
}
