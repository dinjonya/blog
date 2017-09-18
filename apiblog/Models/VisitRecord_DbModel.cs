using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    /// <summary>
    /// 访问记录表
    /// </summary>
    [Table("tb_VisitRecord")]
    public class VisitRecord_DbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// 访问时间
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string VisitTime { get; set; }

        /// <summary>
        /// 访问cookie
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string CookieValue { get; set; }
        
        /// <summary>
        /// 访问页面
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string VisitPath { get; set; }

        /// <summary>
        /// 访问IP
        /// </summary>
        /// <returns></returns>
        public string VisitIp { get; set; }

    }
}