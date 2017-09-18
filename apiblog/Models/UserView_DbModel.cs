using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    /// <summary>
    /// 访问记录表
    /// </summary>
    [Table("tb_userView")]
    public class UserView_DbModel
    {
        /// <summary>
        /// Id 自动编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        [StringLength(32)]
        public string VisitTime { get; set; }

        /// <summary>
        /// 访问cookie
        /// </summary>
        [StringLength(64)]
        public string CookieValue { get; set; }
        
        /// <summary>
        /// 访问页面
        /// </summary>
        [StringLength(64)]
        public string VisitPath { get; set; }
        
    }
}