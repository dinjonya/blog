using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace apiblog.Models
{
    /// <summary>
    /// 回复表
    /// </summary>
    [Table("tb_PostReply")]
    public class PostReply_DbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 回复者名称
        /// </summary>
        /// <returns></returns>
        [StringLength(16)]
        public string ReplyUserName { get; set; }

        /// <summary>
        /// 回复者邮箱地址
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string ReplyUserMail { get; set; }

        /// <summary>
        /// 回复者个人网址
        /// </summary>
        /// <returns></returns>
        [StringLength(128)]
        public string ReplyUserUri { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        /// <returns></returns>
        [StringLength(4096)]
        public string ReplyContent { get; set; }

        /// <summary>
        /// 回复文章编号
        /// </summary>
        /// <returns></returns>
        public int ReplyPostId { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string ReplyTime { get; set; }
    }
}