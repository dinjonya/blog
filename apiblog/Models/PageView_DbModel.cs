using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    [Table("tb_pageView")]
    public class PageView_DbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        /// <returns></returns>
        public int VisitCount { get; set; }

        /// <summary>
        /// 访问页面
        /// </summary>
        /// <returns></returns>
        [StringLength(64)]
        public string VisitPage { get; set; }
    }
}