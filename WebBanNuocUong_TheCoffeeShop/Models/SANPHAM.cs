
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebBanNuocUong_TheCoffeeShop.Models
{

using System;
    using System.Collections.Generic;
    
public partial class SANPHAM
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public SANPHAM()
    {

        this.CTDONHANGs = new HashSet<CTDONHANG>();

    }


    public string MASP { get; set; }

    public string TENSP { get; set; }

    public decimal GIASP { get; set; }

    public string ANHSP { get; set; }

    public int SOLUONG { get; set; }

    public string MALOAISP { get; set; }

    public string MOTASP { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CTDONHANG> CTDONHANGs { get; set; }

    public virtual LOAISANPHAM LOAISANPHAM { get; set; }

}

}
