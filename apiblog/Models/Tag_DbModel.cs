using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiblog.Models
{
    /// <summary>
    /// 标签表
    /// </summary>
    [Table("tb_Tag")]
    public class Tag_DbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(32)]
        public string TagName { get; set; }
        
        public int PostNum { get; set; }
    }
}