using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    /// <summary>
    /// 文章表
    /// </summary>
    [Table("tb_Post")]
    public class Post_DbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string PostTitle { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string PostDescription { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        public string PostContent { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        /// <returns></returns>
        public int PostCategoryId { get; set; }


        /// <summary>
        /// 标签集合   1,2,3, 
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string Tags { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        /// <returns></returns>
        [StringLength(32)]
        public string PostTime { get; set; }
    }
}