using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

/// <summary>
/// Для создания задачи
/// </summary>
public class Progress : BaseEntity
{
    public CategoryOfTask CategoryOfTask { get; set; }

    public int PointsNow { get; set; } = 0;

    public int PointsEnd { get; set;}
}
