using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authen.Models
{
    /// <summary>
    /// 接口调用记录
    /// </summary>
    [Table("tb_InvokeRecord")]
    public class InvokeRecord_DbModel
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        /// <returns></returns>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 接口调用者
        /// </summary>
        /// <returns></returns>
        public string InvokeId { get; set; }

        /// <summary>
        /// 调用者秘钥
        /// </summary>
        /// <returns></returns>
        public string InvokeSecret { get; set; }

        /// <summary>
        /// ControllerName
        /// </summary>
        /// <returns></returns>
        [StringLength(512)]
        public string ControllerName { get; set; }

        /// <summary>
        /// ActionName
        /// </summary>
        /// <returns></returns>
        [StringLength(512)]
        public string ActionName { get; set; }

        [StringLength(1024)]
        public string Headers { get; set; }

        /// <summary>
        /// 入参
        /// </summary>
        /// <returns></returns>
        public string InputParams { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        /// <returns></returns>
        public string ReturnValue { get; set; }

        /// <summary>
        /// 调用源
        /// </summary>
        /// <returns></returns>
        [StringLength(8192)]
        public string UserSource { get; set; }

        /// <summary>
        /// 调用者ip
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string RequestIp { get; set; }
        

        /// <summary>
        /// 调用方式
        /// </summary>
        /// <returns></returns>
        [StringLengthAttribute(8)]
        public string InvokeMethod { get; set; }

        /// <summary>
        /// 调用时间
        /// </summary>
        /// <returns></returns>
        public string InvokeTime { get; set; }

    }
}