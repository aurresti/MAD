//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.Photogram.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public long commentId { get; set; }
        public long imageId { get; set; }
        public long userId { get; set; }
        public string comment1 { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
