using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Polichat_Backend.Database;


[Table("Questions")]
public class Question
{
    [Key]
    public string Text { get; set; }
    public string Type { get; set; }
    
    [Column("P_Strongly_Agree_X")]
    public double StronglyAgreeX { get; set; }
    [Column("P_Strongly_Agree_Y")]
    public double StronglyAgreeY { get; set; }
    
    [Column("P_Agree_X")]
    public double AgreeX { get; set; }
    [Column("P_Agree_Y")]
    public double AgreeY { get; set; } 
    
    [Column("P_Disagree_X")]
    public double DisagreeX { get; set; }
    [Column("P_Disagree_Y")]
    public double DisagreeY { get; set; } 
    
    [Column("P_Strongly_Disagree_X")]
    public double StronglyDisagreeX { get; set; }
    [Column("P_Strongly_Disagree_Y")]
    public double StronglyDisagreeY { get; set; }
}