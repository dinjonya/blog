using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    /// <summary>
    /// 文章类别表
    /// </summary>
    [Table("tb_PostCategory")]
    public class PostCategory_DbModel
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        /// <returns></returns>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// 类别名称
        /// </summary>
        /// <returns></returns>
        public string CategoryName { get; set; }

        /// <summary>
        /// 类别父级编号
        /// </summary>
        /// <returns></returns>
        public int Pid { get; set; }
    }
}