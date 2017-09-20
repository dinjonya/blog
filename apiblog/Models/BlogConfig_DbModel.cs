using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    /// <summary>
    /// blog配置表
    /// </summary>
    [Table("tb_BlogConfig")]
    public class BlogConfig_DbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// blog归属人
        /// </summary>
        /// <returns></returns>
        [StringLength(16)]
        public string BlogOwner { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string Salt { get; set; }

        /// <summary>
        /// blog归属人密码
        /// </summary>
        /// <returns></returns>
        [StringLength(256)]
        public string OwnerPwd { get; set; }
        
        /// <summary>
        /// blog名称
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string BlogTitle { get; set; }

        /// <summary>
        /// 关于我
        /// </summary>
        /// <returns></returns>
        public string AboutMe { get; set; }
    }
}