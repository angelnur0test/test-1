//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class news
    {
        public int id { get; set; }
        public Nullable<System.DateTime> publicationDate { get; set; }
        public string title { get; set; }
        public string previewPicture { get; set; }
        public string bigPicture { get; set; }
        public string newsText { get; set; }
        public string newsTag { get; set; }
    }
}
