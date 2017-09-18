using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authen.Models
{
    /// <summary>
    /// 接口认证
    /// </summary>
    [Table("tb_InvokeAuthen")]
    public class InvokerAuthen_DbModel
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        /// <returns></returns>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string ProjcetName { get; set; }  

        /// <summary>
        /// 调用者
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string Invoker { get; set; }



        /// <summary>
        /// 秘钥
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string Key  { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        /// <returns></returns>
        [StringLength(256)]
        public string Sale { get; set; }

    }
}