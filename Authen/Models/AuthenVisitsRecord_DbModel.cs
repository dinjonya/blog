using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authen.Models
{
    /// <summary>
    /// 认证访问记录
    /// </summary>
    [Table("tb_AuthenVisitsRecord")]
    public class AuthenVisitsRecord_DbModel
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        /// <returns></returns>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 访问者编号
        /// </summary>
        /// <returns></returns>
        public int AuthenId { get; set; }

        /// <summary>
        /// 访问者Token
        /// </summary>
        /// <returns></returns>
        public int Token { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        /// <returns></returns>
        public string CreateTime { get; set; }

        /// <summary>
        /// 访问方式  创建  续约  销毁  等
        /// </summary>
        /// <returns></returns>
        public string InvokeType { get; set; }
    }
}